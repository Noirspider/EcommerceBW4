using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EcommerceBW4
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM Prodotti", conn);
                SqlDataReader reader = command.ExecuteReader();

                prodottiRepeater.DataSource = reader;
                prodottiRepeater.DataBind();

                reader.Close();
                conn.Close();
            }
        }

        // Qui i metodi per gestire i click dei bottoni, se necessario
    }
}
