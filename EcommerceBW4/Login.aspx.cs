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
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                // string query = "SELECT COUNT(1) FROM Utenti WHERE NomeUtente=@username AND Password=@password";
                string query = "SELECT UtenteID FROM Utenti WHERE NomeUtente=@username AND Password=@password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        // L'utente è autenticato correttamente.
                        int userId = Convert.ToInt32(result);
                        Session["UserId"] = userId;
                        Session["Username"] = username;
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        // Autenticazione fallita.
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
            int eta;

            // Controllo che l'input per l'età sia un numero intero valido
            if (!int.TryParse(txtEta.Text, out eta))
            {
                lblError.Visible = true;
                lblError.Text = "L'età inserita non è valida. Inserisci un numero intero.";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                string query = "INSERT INTO Utenti (NomeUtente, Password, IsAdmin, Eta) VALUES (@username, @password, 0, @Eta)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@Eta", eta);

                    conn.Open();

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Errore durante la registrazione. Riprova.";
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