using System;

namespace EcommerceBW4
{
    public partial class Premium : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void Search_Click(object sender, EventArgs e)
        {
            string searchText = searchInput.Value.Trim(); // Ottieni il testo di ricerca dall'input
            Response.Redirect($"Default.aspx?search={searchText}");
        }
    }
}