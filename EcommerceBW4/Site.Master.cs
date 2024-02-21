using System;
using System.Web.UI;

namespace EcommerceBW4
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.Url.ToString();
            if (url.Contains("/Default") || url.Contains("localhost:44321") &&
        !url.Contains("/Carrello") &&
        !url.Contains("/Dettagli") &&
        !url.Contains("Premium") &&
        !url.Contains("OrderConfermation") &&
        !url.Contains("Checkout"))
            {
                bannerWelcome.Visible = true;
            }
            else
            {
                bannerWelcome.Visible = false;
            }
        }

        // Metodo per ricercare il prodotto tramite la barra di ricerca
        protected void Search_Click(object sender, EventArgs e)
        {
            string searchText = searchInput.Value.Trim(); // Ottieni il testo di ricerca dall'input
            Response.Redirect($"Default.aspx?search={searchText}");
        }

        // Metodo per il logout dell'utente
        protected void Logout_Click(object sender, EventArgs e)
        {

            Session.Remove("Username");
            Session.Abandon();

            Response.Redirect("Login.aspx");
        }

    }
}