using System;
using System.Configuration;
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
            decimal totaleCarrello = 0m; // Inizializza il totale del carrello

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

            // Imposta il testo del totale del carrello
            lblTotaleCarrello.Text = "Totale Carrello: " + totaleCarrello.ToString("C"); // Formatta come valuta

            // Imposta l'immagine GIF della spia come immagine del carrello
            imgAnteprimaCarrello.ImageUrl = "https://www.gifanimate.com/data/media/353/spia-immagine-animata-0019.gif";
        }



        // Metodo per completare l'ordine e inserire i dati nella tabella Ordini
        protected void CompletaOrdine_ServerClick(object sender, EventArgs e)
        {
            // Assicurati che l'ID utente sia presente nella sessione
            if (Session["UserId"] == null)
            {
                // Reindirizza al login se l'utente non è loggato
                Response.Redirect("Login.aspx");
                return;
            }

            int utenteId = Convert.ToInt32(Session["UserId"]);

            // Raccogli i dati dal form
            // string nomeDestinatario = nomeDestinatario.Text;
            // string indirizzoDestinatario = indirizzoDestinatario.Text;
            // string cittaDestinatario = cittaDestinatario.Text;
            // string capDestinatario = capDestinatario.Text;
            // string paeseDestinatario = paeseDestinatario.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            int spedizioneId;

            // Inserisci i dati nella tabella Spedizioni e ottieni l'ID della spedizione
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string querySpedizione = @"
            INSERT INTO Spedizioni (UtenteID, NomeDestinatario, IndirizzoDestinatario, CittaDestinatario, CAPDestinatario, PaeseDestinatario, DataSpedizione)
            OUTPUT INSERTED.SpedizioneID
            VALUES (@UtenteID, @NomeDestinatario, @IndirizzoDestinatario, @CittaDestinatario, @CAPDestinatario, @PaeseDestinatario, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(querySpedizione, conn))
                {
                    cmd.Parameters.AddWithValue("@UtenteID", utenteId);
                    cmd.Parameters.AddWithValue("@NomeDestinatario", nomeDestinatario);
                    cmd.Parameters.AddWithValue("@IndirizzoDestinatario", indirizzoDestinatario);
                    cmd.Parameters.AddWithValue("@CittaDestinatario", cittaDestinatario);
                    cmd.Parameters.AddWithValue("@CAPDestinatario", capDestinatario);
                    cmd.Parameters.AddWithValue("@PaeseDestinatario", paeseDestinatario);

                    conn.Open();
                    spedizioneId = (int)cmd.ExecuteScalar(); // Ottiengo l'ID della spedizione appena inserita
                }
            }


            int carrelloId = Convert.ToInt32(Request.QueryString["carrelloId"]);

            // Inserisco i dati nella tabella Ordini
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string queryOrdine = @"
            INSERT INTO Ordini (UtenteID, SpedizioneID, CarrelloID, DataOrdine)
            VALUES (@UtenteID, @SpedizioneID, @CarrelloID, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(queryOrdine, conn))
                {
                    cmd.Parameters.AddWithValue("@UtenteID", utenteId);
                    cmd.Parameters.AddWithValue("@SpedizioneID", spedizioneId);
                    cmd.Parameters.AddWithValue("@CarrelloID", carrelloId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Redirect("OrderConfirmation.aspx");
        }

    }
}
