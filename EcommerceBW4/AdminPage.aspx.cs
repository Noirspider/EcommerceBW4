using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceBW4
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProdottiDropDown();

            }
        }

        private void BindProdottiDropDown()
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT ProdottoID, Nome FROM Prodotti";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            DropDownProdotto.DataSource = reader;
                            DropDownProdotto.DataTextField = "Nome";
                            DropDownProdotto.DataValueField = "ProdottoID";
                            DropDownProdotto.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Si è verificato un errore: {ex.Message}");
            }
        }
       

            protected void DropDownProdotto_SelectedIndexChanged(object sender, EventArgs e)
            {
               
                string selectedValue = DropDownProdotto.SelectedValue;

                
                if (!string.IsNullOrEmpty(selectedValue))
                {
                    Card.Style["display"] = "block";

                    ImgCarrello.Src = "ImmagineURL"; 
                    LblNome.InnerText = "Nome Prodotto " + selectedValue;
                    LblPrezzo.InnerText = "Prezzo Prodotto " + selectedValue;
                }
                else
                {
                    
                    Card.Style["display"] = "none";
                }
            }
        }
    }


