using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace EcommerceBW4
{
    public partial class Checkout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verifica se l'utente è loggato
                if (Session["UserId"] != null)
                {
                    // Recupera l'ID del carrello dalla query string
                    if (Request.QueryString["carrelloId"] != null)
                    {
                        int carrelloId = Convert.ToInt32(Request.QueryString["carrelloId"]);
                        BindDettagliCarrello(carrelloId);
                    }
                    else
                    {
                        // Gestire il caso in cui l'ID del carrello non sia presente
                        Response.Redirect("Carrello.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        // metodo per recuperare i dettagli del carrello e calcolare il totale del carrello
        private void BindDettagliCarrello(int carrelloId)
        {
            decimal totaleCarrello = 0m;

            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT SUM(cd.Quantita * cd.Prezzo) AS TotaleCarrello
                    FROM CarrelloDettaglio cd
                    WHERE cd.CarrelloID = @CarrelloID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CarrelloID", carrelloId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Calcola il totale del carrello
                            totaleCarrello = reader.IsDBNull(reader.GetOrdinal("TotaleCarrello")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TotaleCarrello"));
                        }
                    }
                }
            }


            lblTotaleCarrello.Text = "Totale Carrello: " + totaleCarrello.ToString("C");


            imgAnteprimaCarrello.ImageUrl = "https://www.gifanimate.com/data/media/353/spia-immagine-animata-0019.gif";
        }

        // Metodo per completare l'ordine e inserire i dati nella tabella Ordini
        protected void CompletaOrdine_ServerClick(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int utenteId = Convert.ToInt32(Session["UserId"]);
            int carrelloId = GetCarrelloId(utenteId); // Assicurati che questa funzione ritorni l'ID del carrello attuale dell'utente.

            if (CarrelloVuoto(carrelloId)) // Assicurati che questa funzione verifichi se ci sono prodotti nel carrello.
            {
                // Usa lo script per mostrare un messaggio di alert
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Il tuo carrello è vuoto! Aggiungi dei prodotti prima di procedere al checkout.');", true);
                return;
            }


            decimal totaleOrdine = CalcolaTotaleCarrello(carrelloId);

            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            int spedizioneId;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string querySpedizione = @"
                        INSERT INTO Spedizioni (UtenteID, NomeDestinatario, IndirizzoDestinatario, CittaDestinatario, CAPDestinatario, PaeseDestinatario, DataSpedizione)
                        OUTPUT INSERTED.SpedizioneID
                        VALUES (@UtenteID, @NomeDestinatario, @IndirizzoDestinatario, @CittaDestinatario, @CAPDestinatario, @PaeseDestinatario, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(querySpedizione, conn))
                {
                    cmd.Parameters.AddWithValue("@UtenteID", utenteId);
                    cmd.Parameters.AddWithValue("@NomeDestinatario", nomeDestinatario.Text);
                    cmd.Parameters.AddWithValue("@IndirizzoDestinatario", indirizzoDestinatario.Text);
                    cmd.Parameters.AddWithValue("@CittaDestinatario", cittaDestinatario.Text);
                    cmd.Parameters.AddWithValue("@CAPDestinatario", capDestinatario.Text);
                    cmd.Parameters.AddWithValue("@PaeseDestinatario", paeseDestinatario.Text);

                    conn.Open();
                    spedizioneId = (int)cmd.ExecuteScalar();
                }

                string queryOrdine = @"
                        INSERT INTO Ordini (UtenteID, SpedizioneID, CarrelloID, DataOrdine, TotaleOrdine)
                        VALUES (@UtenteID, @SpedizioneID, @CarrelloID, GETDATE(), @TotaleOrdine);
                        SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(queryOrdine, conn))
                {
                    cmd.Parameters.AddWithValue("@UtenteID", utenteId);
                    cmd.Parameters.AddWithValue("@SpedizioneID", spedizioneId);
                    cmd.Parameters.AddWithValue("@CarrelloID", carrelloId);
                    cmd.Parameters.AddWithValue("@TotaleOrdine", totaleOrdine);

                    int ordineId = Convert.ToInt32(cmd.ExecuteScalar());
                    Session.Remove("CarrelloVuoto");
                    SvuotaCarrello(GetCarrelloId(Convert.ToInt32(Session["UserId"]))); // Svuota il carrello
                    Response.Redirect("OrderConfirmation.aspx?OrdineID=" + ordineId);

                }
            }
        }

        private bool CarrelloVuoto(int carrelloId)
        {
            bool vuoto = true;
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM CarrelloDettaglio WHERE CarrelloID = @CarrelloID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CarrelloID", carrelloId);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    vuoto = count == 0;
                }
            }
            return vuoto;
        }

        // Metodo per svuotare il carrello dopo aver completato l'ordine
        private void SvuotaCarrello(int carrelloId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SvuotaCarrello", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UtenteID", Convert.ToInt32(Session["UserId"]));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Metodo recupera l'ID del carrello dell'utente
        private int GetCarrelloId(int utenteId)
        {
            // Implementazione del metodo per recuperare l'ID del carrello dell'utente
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            int carrelloId = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT CarrelloID FROM Carrello WHERE UtenteID = @UtenteID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UtenteID", utenteId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            carrelloId = Convert.ToInt32(reader["CarrelloID"]);
                        }
                    }
                }
            }

            return carrelloId;
        }

        // Metodo per calcolare il totale del carrello in base all'ID del carrello
        private decimal CalcolaTotaleCarrello(int carrelloId)
        {
            decimal totaleCarrello = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT SUM(Quantita * Prezzo) AS TotaleCarrello
                    FROM CarrelloDettaglio
                    WHERE CarrelloID = @CarrelloID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CarrelloID", carrelloId);
                    conn.Open();
                    totaleCarrello = (decimal)cmd.ExecuteScalar();
                }
            }
            return totaleCarrello;
        }

    }
}