﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
                PopoloDropDownProdotto();
                PopoloDropDownVenditeAnnue();
                PopoloDropDownVenditeRegione();
            }
        }

        private void PopoloDropDownProdotto()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["YourConnectionStringName"].ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT ProdottoID, Nome FROM Prodotti";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataReader reader = cmd.ExecuteReader();


                    DropDownProdotto.DataSource = reader;
                    DropDownProdotto.DataTextField = "Nome";
                    DropDownProdotto.DataValueField = "ProdottoID";
                    DropDownProdotto.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Si è verificato un errore: {ex.Message}");
            }
        }

    }
}