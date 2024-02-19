using System;
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
                if (int.TryParse(Request.QueryString["id"], out int productId))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["Prodotti"].ConnectionString;
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
        /* protected void AddCarrello_Click(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["id"], out int productId))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Prodotti"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Prodotti WHERE ProdottoID = @ProductId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Prodotto prodotto = new Prodotto
                                {
                                    IdProdotto = (int)reader["ProdottoID"],
                                };
                                int quantita = Convert.ToInt32(QuantitaDropDown.SelectedValue);
                                Carrello carrello = Session["Carrello"] as Carrello ?? new Carrello();
                                for (int i = 0; i < quantita; i++)
                                {
                                    carrello.AggiungiProdotto(prodotto);
                                }
                                Session["Carrello"] = carrello;
                            }
                        }
                    }
                }
            }
        } */

    }
}