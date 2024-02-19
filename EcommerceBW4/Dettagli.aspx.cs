﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;


namespace EcommerceBW4
{
    public partial class Dettagli : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int productId = 1;
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
        protected void AddCarrello_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                PopupLiteral.Text = "<script>alert('Prodotto aggiunto al carrello!');</script>";
            }
            else
            {
                if (int.TryParse(Request.QueryString["id"], out int prodottoId))
                {
                    int quantita;
                    if (int.TryParse(QuantitaTextBox.Text, out quantita))
                    {
                        AggiungiAlCarrello(prodottoId, quantita);
                    }
                    else
                    {
                        // da completare
                    }
                }
                else
                {
                    // da completare
                }
            }
        }

        private void AggiungiAlCarrello(int prodottoId, int quantita)
        {
            int utenteId = Convert.ToInt32(Session["UserId"]); 

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
                    decimal prezzo = AggiungiPrezzo (prodottoId);
                    cmd.Parameters.AddWithValue("@Prezzo", prezzo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

       
        private decimal AggiungiPrezzo (int prodottoId)
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