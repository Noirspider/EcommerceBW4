using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceBW4
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isLogged = Session["UserId"] != null;
            if (isLogged)
            {
                if (!IsPostBack)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
                    SqlConnection conn = new SqlConnection(connectionString);
                    conn.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM Prodotti", conn);
                    SqlDataReader reader = command.ExecuteReader();

                    prodottiRepeater.DataSource = reader;
                    prodottiRepeater.DataBind();


                    // Ottiene il nome utente da sessione + popola dinamicamente
                    string username = Session["Username"].ToString();
                    helloUser.InnerText = username;

                    reader.Close();
                    conn.Close();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        // metodo per gestire il clicl sul pulsante "Dettagli"
        protected void ToDetail_Command(object sender, CommandEventArgs e)
        {
            string productId = e.CommandArgument.ToString();
            Response.Redirect($"Dettagli.aspx?id={productId}");
        }

        // metodo per gestire il clicl sul pulsante "Aggiungi al carrello"
        protected void AddToCart_Command(object sender, CommandEventArgs e)
        {
            string productId = e.CommandArgument.ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Prodotti WHERE Id = @id", conn);
            command.Parameters.AddWithValue("@id", productId);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string nome = reader["Nome"].ToString();
                string prezzo = reader["Prezzo"].ToString();
                string quantita = "1";
                string username = reader["Username"].ToString();
                string userId = Session["UserId"].ToString();

                reader.Close();

                command = new SqlCommand("INSERT INTO Carrello (Nome, Prezzo, Quantita, Username) VALUES (@nome, @prezzo, @quantita, @username)", conn);
                command.Parameters.AddWithValue("@nome", nome);
                command.Parameters.AddWithValue("@prezzo", prezzo);
                command.Parameters.AddWithValue("@quantita", quantita);
                command.Parameters.AddWithValue("@username", username);
                command.ExecuteNonQuery();
            }

            conn.Close();
        }


        // metodo per la ricerca dei prodotti
        protected void AddCart_OnClickButton(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int prodottoId = Convert.ToInt32(btn.CommandArgument);
            int utenteId = Convert.ToInt32(Session["UserId"]);

            // Ottengo l'ID del carrello esistente o ne creo uno nuovo.
            int carrelloId = GetOrCreateTimeCarrello(utenteId);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString))
            {
                conn.Open();

                // Controllo se il prodotto è già nel carrello.
                if (ProdottoAlreadyInCarrello(conn, carrelloId, prodottoId))
                {
                    // Aggiorno la quantità.
                    UpdateQuantitaProdottoInCarrello(conn, carrelloId, prodottoId);
                }
                else
                {
                    // Inserisco un nuovo dettaglio carrello.
                    InsertNewProdottoInCarrello(conn, carrelloId, prodottoId);
                }
            }
        }

        // Metodo per ottenere l'ID del carrello esistente o crearne uno nuovo.
        private int GetOrCreateTimeCarrello(int utenteId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString))
            {
                conn.Open();
                string query = "SELECT CarrelloID FROM Carrello WHERE UtenteID = @utenteId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@utenteId", utenteId);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO Carrello (UtenteID, DataOra) OUTPUT INSERTED.CarrelloID VALUES (@utenteId, GETDATE())";
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@utenteId", utenteId);
                            return (int)insertCmd.ExecuteScalar();
                        }
                    }
                }
            }
        }


        // Metodo per controllare se un prodotto è già nel carrello.
        private bool ProdottoAlreadyInCarrello(SqlConnection conn, int carrelloId, int prodottoId)
        {
            string query = "SELECT COUNT(*) FROM CarrelloDettaglio WHERE CarrelloID = @carrelloId AND ProdottoID = @prodottoId";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@carrelloId", carrelloId);
                cmd.Parameters.AddWithValue("@prodottoId", prodottoId);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // Metodo per aggiornare la quantità di un prodotto nel carrello.
        private void UpdateQuantitaProdottoInCarrello(SqlConnection conn, int carrelloId, int prodottoId)
        {
            string query = "UPDATE CarrelloDettaglio SET Quantita = Quantita + 1 WHERE CarrelloID = @carrelloId AND ProdottoID = @prodottoId";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@carrelloId", carrelloId);
                cmd.Parameters.AddWithValue("@prodottoId", prodottoId);
                cmd.ExecuteNonQuery();
            }
        }

        // Metodo per inserire un nuovo prodotto nel carrello.
        private void InsertNewProdottoInCarrello(SqlConnection conn, int carrelloId, int prodottoId)
        {
            // Recupera il prezzo del prodotto da aggiungere al carrello
            string priceQuery = "SELECT Prezzo FROM Prodotti WHERE ProdottoID = @prodottoId";
            SqlCommand priceCmd = new SqlCommand(priceQuery, conn);
            priceCmd.Parameters.AddWithValue("@prodottoId", prodottoId);
            var prezzo = (decimal)priceCmd.ExecuteScalar();

            // Inserisci il nuovo prodotto nel carrello
            string query = "INSERT INTO CarrelloDettaglio (CarrelloID, ProdottoID, Quantita, Prezzo) VALUES (@carrelloId, @prodottoId, 1, @prezzo)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@carrelloId", carrelloId);
                cmd.Parameters.AddWithValue("@prodottoId", prodottoId);
                cmd.Parameters.AddWithValue("@prezzo", prezzo);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
