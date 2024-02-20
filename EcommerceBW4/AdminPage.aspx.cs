using System;
using System.Data.SqlClient;
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

                            DropDownProdotto.Items.Insert(0, new ListItem("Scegli il tuo SpyGadget:", ""));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Si è verificato un errore: {ex.Message}");
            }
        }

        protected void DropDownProdottoBoth(object sender, EventArgs e)
        {
            DropDownProdotto_SelectedIndexChanged(sender, e);
            SecondCard(sender, e);

        }
        protected void DropDownProdotto_SelectedIndexChanged(object sender, EventArgs e)

        {
            string selectedValue = DropDownProdotto.SelectedValue;

            if (!string.IsNullOrEmpty(selectedValue))
            {
                try
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT Nome, Prezzo, ImmagineURL FROM Prodotti WHERE ProdottoID = @ProdottoID";

                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@ProdottoID", selectedValue);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    ImgCarrello.ImageUrl = reader["ImmagineURL"].ToString();
                                    LblNome.Text = reader["Nome"].ToString();
                                    LblPrezzo.Text = string.Format("Prezzo: {0:C}", reader["Prezzo"]);
                                    Card.Visible = true;
                                }
                                else
                                {
                                    Card.Visible = false;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Si è verificato un errore: {ex.Message}");
                }
            }
            else
            {
                Card.Visible = false;
            }
        }

        protected void SecondCard(object sender, EventArgs e)
        {
            {
                string selectedValue = DropDownProdotto.SelectedValue;

                if (!string.IsNullOrEmpty(selectedValue))
                {
                    try
                    {
                        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = "SELECT NomeDestinatario, IndirizzoDestinatario FROM Spedizioni WHERE ProdottoID = @ProdottoID";

                            using (SqlCommand cmd = new SqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("ProdottoID", selectedValue);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {

                                        Label1.Text = reader["NomeDestinatario"].ToString();
                                        Label3.Text = reader["IndirizzoDestinatario"].ToString();
                                        Card1.Visible = true;
                                    }
                                    else
                                    {
                                        Card1.Visible = false;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Si è verificato un errore: {ex.Message}");
                    }
                }
                else
                {
                    Card1.Visible = false;
                }
            }
        }
    }
}
