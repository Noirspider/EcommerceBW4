using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace EcommerceBW4
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            // Procedere solo se l'utente è loggato.
            bool isAdmin = false;
            int userId = Convert.ToInt32(Session["UserId"]);
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT IsAdmin FROM Utenti WHERE UtenteID = @UtenteID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UtenteID", userId);
                    conn.Open();
                    isAdmin = Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }

            // Se l'utente è amministratore e non è un postback, effettuare il binding dei prodotti.
            if (isAdmin && !IsPostBack)
            {
                BindProdottiDropDown();
            }
            else if (!isAdmin)
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void BindProdottiDropDown()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;

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


        protected void DropDownProdotto_SelectedIndexChanged(object sender, EventArgs e)

        {
            string selectedValue = DropDownProdotto.SelectedValue;

            if (!string.IsNullOrEmpty(selectedValue))
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
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
        protected void InsertItem(object sender, EventArgs e)
        {

            string Nome = TextBox1.Text;
            string Prezzo = TextBox3.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertSql = "INSERT INTO Prodotti (Nome,ImmagineURL, Prezzo) VALUES (@Nome, @ImmagineURL, @Prezzo)";
                SqlCommand insertCommand = new SqlCommand(insertSql, connection);


                insertCommand.Parameters.AddWithValue("@Nome", Nome);
                //insertCommand.Parameters.AddWithValue("@ImmagineURL", ImmagineURL);
                insertCommand.Parameters.AddWithValue("@Prezzo", Prezzo);

                try
                {
                    connection.Open();
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    Console.WriteLine($"Inserted {rowsAffected} row(s)!");

                    string script = "alert('Prodotto Inserito con Successo Bravoh');";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Si è verificato un errore: {ex.Message}");
                }
            }
        }
        protected void DeleteItem(object sender, EventArgs e)
        {
            string selectedValue = DropDownProdotto.SelectedValue;

            if (!string.IsNullOrEmpty(selectedValue))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string deleteSql = "DELETE FROM Prodotti WHERE ProdottoID = @selectedValue";
                    SqlCommand deleteCommand = new SqlCommand(deleteSql, connection);

                    deleteCommand.Parameters.AddWithValue("@selectedValue", selectedValue);

                    try
                    {
                        connection.Open();
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        string script = "alert('Prodotto eliminato con successo! Bravoh');";
                        ClientScript.RegisterStartupScript(GetType(), "alert", script, true);
                        BindProdottiDropDown();

                        if (rowsAffected > 0)
                        {
                            Card.Visible = false;
                            DropDownProdotto.SelectedValue = "";
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        string script = "alert('Non hai eliminato un CAZZO DI NIENTE!!!!!');";
                        ClientScript.RegisterStartupScript(GetType(), "alert", script, true);
                    }
                }
            }
        }
        protected void ModificaItem(object sender, EventArgs e)
        {
            string Nome = TextBox1.Text;
            //string ImmagineURL = TextBox2.Text;
            string Prezzo = TextBox3.Text;
            string selectedValue = DropDownProdotto.SelectedValue;

            if (!string.IsNullOrEmpty(selectedValue))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string updateSql = "UPDATE Prodotti SET Nome = @Nome, ImmagineURL = @ImmagineURL, Prezzo = @Prezzo WHERE ProdottoID = @ProdottoID";
                    SqlCommand updateCommand = new SqlCommand(updateSql, connection);

                    updateCommand.Parameters.AddWithValue("@Nome", Nome);
                    //updateCommand.Parameters.AddWithValue("@ImmagineURL", ImmagineURL);
                    updateCommand.Parameters.AddWithValue("@Prezzo", Prezzo);
                    updateCommand.Parameters.AddWithValue("@ProdottoID", selectedValue);

                    try
                    {
                        connection.Open();
                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        string script = "alert('Prodotto Modificato con Successo');";
                        ClientScript.RegisterStartupScript(GetType(), "alert", script, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        string script = "alert('Non hai modificato Nulla Coglione');";
                        ClientScript.RegisterStartupScript(GetType(), "alert", script, true);
                    }
                }
            }
        }
    }
}
