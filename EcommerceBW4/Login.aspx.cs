using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace EcommerceBW4
{
    public partial class Login : Page
    {
        // controllo se l'utente è già loggato
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        // metodo per il login dell'utente 
        protected void Login_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Value;
            string password = txtPassword.Value;

            // connessione dal web.config.
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                string query = "SELECT COUNT(1) FROM Utenti WHERE NomeUtente=@username AND Password=@password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count == 1)
                    {

                        Session["Username"] = username;
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        // errore.
                        lblError.Visible = true;
                        lblError.Text = "Username o password errati";
                    }
                }
            }
        }

        // metodo per il recupero della password
        protected void ForgotPassword_Click(object sender, EventArgs e)
        {

            Response.Redirect("ForgotPasswordPage.aspx");
        }

        // metodo per la registrazione dell'utente nel database
        protected void SignUp_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Value;
            string password = txtPassword.Value;


            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceConnectionString"].ConnectionString;


            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                string query = "INSERT INTO Utenti (NomeUtente, Password, IsAdmin) VALUES (@username, @password, 0)";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);


                    conn.Open();


                    int result = cmd.ExecuteNonQuery();


                    if (result > 0)
                    {

                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        // Errore
                        lblError.Visible = true;
                        lblError.Text = "Username o password errati";
                    }
                }
            }
        }

        // metodo per il logout dell'utente
        protected void Logout_Click(object sender, EventArgs e)
        {

            Session.Remove("Username");
            Session.Abandon();

            Response.Redirect("Login.aspx");
        }
    }
}