﻿using System;
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
                                string query =  @"SELECT p.Nome, p.Descrizione, p.Prezzo, p.ImmagineURL, d.DescrizioneEstesa, d.QuantitaDisponibile
                                                FROM Prodotti p
                                                INNER JOIN DettagliProdotto d ON p.ProdottoID = d.ProdottoID
                                                WHERE p.ProdottoID = @ProductId";

                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@ProductId", productId);

                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            Nome.Text = reader["Nome"].ToString();
                                            Descrizione.Text = reader["Descrizione"].ToString();
                                            DescrizioneEstesa.Text = reader["DescrizioneEstesa"].ToString();
                                            Prezzo.Text = Convert.ToDecimal(reader["Prezzo"]).ToString("0.00€");
                                            ImgUrl.ImageUrl = reader["ImmagineURL"].ToString();
                                            QuantitaDisponibile.Text = reader["QuantitaDisponibile"].ToString();
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
                    int quantitaDisponibile = GetQuantitaDisponibile(prodottoId);
                    if (quantita > quantitaDisponibile)
                    {
                        ModalContent.Text = "Quantità richiesta non disponibile.";
                        myModal.Visible = true;
                        return; 
                    }
                    try
                    {
                        myModal.Visible = true;
                        ModalContent.Text = "PRODOTTO AGGIUNTO CON SUCCESSO AL CARRELLO!";
                        AggiungiAlCarrello(prodottoId, quantita);
                    }
                    catch (Exception ex)
                    {
                        ModalContent.Text = $"Si è verificato un errore durante l'aggiunta del prodotto al carrello: {ex.Message}";
                        myModal.Visible = true;
                    }
                }
                else
                {
                    ModalContent.Text = "Inserisci una quantità valida";
                    myModal.Visible = true;
                }
            }
            else
            {
                ModalContent.Text = "Errore nell'ID del prodotto";
                myModal.Visible = true;
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
                    query = "SELECT Quantita FROM CarrelloDettaglio WHERE CarrelloID = @CarrelloID AND ProdottoID = @ProdottoID";
                    using (SqlCommand checkCmd = new SqlCommand(query, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@CarrelloID", carrelloId);
                        checkCmd.Parameters.AddWithValue("@ProdottoID", prodottoId);
                        object result = checkCmd.ExecuteScalar();
                        if (result != null)
                        {
                            int quantitaPrecedente = Convert.ToInt32(result);
                            query = "UPDATE CarrelloDettaglio SET Quantita = Quantita + @Quantita WHERE CarrelloID = @CarrelloID AND ProdottoID = @ProdottoID";
                            using (SqlCommand updateCmd = new SqlCommand(query, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@CarrelloID", carrelloId);
                                updateCmd.Parameters.AddWithValue("@ProdottoID", prodottoId);
                                updateCmd.Parameters.AddWithValue("@Quantita", quantita);
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            query = "INSERT INTO CarrelloDettaglio (CarrelloID, ProdottoID, Quantita, Prezzo) VALUES (@CarrelloID, @ProdottoID, @Quantita, @Prezzo)";
                            using (SqlCommand insertCmd = new SqlCommand(query, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@CarrelloID", carrelloId);
                                insertCmd.Parameters.AddWithValue("@ProdottoID", prodottoId);
                                insertCmd.Parameters.AddWithValue("@Quantita", quantita);
                                decimal prezzo = AggiungiPrezzo(prodottoId);
                                insertCmd.Parameters.AddWithValue("@Prezzo", prezzo);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
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
        protected void CloseButton_Click(object sender, EventArgs e)
        {
            myModal.Visible = false;
        }
        private int GetQuantitaDisponibile(int prodottoId)
        {
            int quantitaDisponibile = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT QuantitaDisponibile FROM DettagliProdotto WHERE ProdottoID = @ProdottoID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProdottoID", prodottoId);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        quantitaDisponibile = Convert.ToInt32(result);
                    }
                }
            }
            return quantitaDisponibile;
        }
        protected void MostraDettagli(object sender, EventArgs e)
        {
            MostraAltroButton.Visible = false;
            NascondiButton.Visible = true;
            DescrizioneEstesaDiv.Style["display"] = "block";
        }

        protected void NascondiDettagli(object sender, EventArgs e)
        {
            MostraAltroButton.Visible = true;
            NascondiButton.Visible = false;
            DescrizioneEstesaDiv.Style["display"] = "none";     
        }
    }
}
