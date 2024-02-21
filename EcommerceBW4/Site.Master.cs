using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        // Metodo per ricercare il prodotto
        protected void Search_Click(object sender, EventArgs e)
        {
            string searchText = searchInput.Value.Trim(); // Ottieni il testo di ricerca dall'input
            Response.Redirect($"Default.aspx?search={searchText}");
        }

    }
}