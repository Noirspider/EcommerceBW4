using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;


namespace EcommerceBW4
{
    public partial class Dettagli : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            bool isLogged = Session["UserId"] != null;
            if (isLogged)
            {
                if (!IsPostBack)
                {
                    if (!IsPostBack)
                    {
                        if (int.TryParse(Request.QueryString["id"], out int productId))
                        {
                            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                // string query =  @"SELECT p.Nome, p.Descrizione, p.Prezzo, p.ImmagineURL, d.DescrizioneEstesa, d.QuantitàDisponibile
                                //                 FROM Prodotti p
                                //                 INNER JOIN DettagliProdotti d ON p.ProdottoID = d.ProdottoID
                                //                 WHERE p.ProdottoID = @ProductId";

                                string query = "SELECT * FROM Prodotti WHERE ProdottoID = @ProductId";

                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@ProductId", productId);

                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            Nome.Text = reader["Nome"].ToString();
                                            Descrizione.Text = reader["Descrizione"].ToString();
                                            // DescrizioneEstesa.Text = reader["DescrizioneEstesa"].ToString();
                                            Prezzo.Text = Convert.ToDecimal(reader["Prezzo"]).ToString("0.00€");
                                            ImgUrl.ImageUrl = reader["ImmagineURL"].ToString();
                                            // QuantitaDisponibile.Text = reader["QuantitàDisponibile"].ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void AddCarrello_Click(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["id"], out int prodottoId))
            {
                int quantita;
                if (int.TryParse(QuantitaTextBox.Text, out quantita))
                {
                    try
                    {
                        AggiungiAlCarrello(prodottoId, quantita);
                        PopupLiteral.Text = "<script>alert('Prodotto aggiunto al carrello!');</script>";
                    }
                    catch (Exception ex)
                    {
                        PopupLiteral.Text = $"<script>alert('Si è verificato un errore durante l\'aggiunta del prodotto al carrello: {ex.Message}');</script>";
                    }
                }
                else
                {
                    PopupLiteral.Text = "<script>alert('Inserisci una quantità valida');</script>";
                }
            }
            else
            {
                PopupLiteral.Text = "<script>alert('Errore nell\'ID del prodotto');</script>";
            }
        }


        private void AggiungiAlCarrello(int prodottoId, int quantita)
        {
            if (Session["UserId"] != null && int.TryParse(Session["UserId"].ToString(), out int utenteId))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int carrelloId;
                    string query = "SELECT CarrelloID FROM Carrello WHERE UtenteID = @UtenteID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UtenteID", utenteId);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            carrelloId = Convert.ToInt32(result);
                        }
                        else
                        {
                            query = "INSERT INTO Carrello (UtenteID, DataOra) VALUES (@UtenteID, @DataOra); SELECT SCOPE_IDENTITY();";
                            using (SqlCommand insertCmd = new SqlCommand(query, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@UtenteID", utenteId);
                                insertCmd.Parameters.AddWithValue("@DataOra", DateTime.Now);
                                carrelloId = Convert.ToInt32(insertCmd.ExecuteScalar());
                            }
                        }
                    }

                    query = "INSERT INTO CarrelloDettaglio (CarrelloID, ProdottoID, Quantita, Prezzo) VALUES (@CarrelloID, @ProdottoID, @Quantita, @Prezzo)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CarrelloID", carrelloId);
                        cmd.Parameters.AddWithValue("@ProdottoID", prodottoId);
                        cmd.Parameters.AddWithValue("@Quantita", quantita);
                        decimal prezzo = AggiungiPrezzo(prodottoId);
                        cmd.Parameters.AddWithValue("@Prezzo", prezzo);
                        try
                        {
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                PopupLiteral.Text = "<script>alert('Prodotto aggiunto al carrello!');</script>";
                            }
                            else
                            {
                                PopupLiteral.Text = "<script>alert('Nessun prodotto aggiunto al carrello');</script>";
                            }
                        }
                        catch (Exception ex)
                        {
                            PopupLiteral.Text = $"<script>alert('Si è verificato un errore nella funzione AggiungiAlCarrello: {ex.Message}');</script>";

                        }
                    }
                }
            }
            else
            {
                PopupLiteral.Text = "<script>alert('Devi effettuare l\'accesso per aggiungere il prodotto al carrello');</script>";
            }
        }



        private decimal AggiungiPrezzo(int prodottoId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Prezzo FROM Prodotti WHERE ProdottoID = @ProdottoID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProdottoID", prodottoId);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToDecimal(result);
                    }
                    else
                    {

                        return 0;
                    }
                }
            }
        }

    } 
}