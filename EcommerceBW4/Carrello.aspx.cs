using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace EcommerceBW4
{
    public partial class Carrello : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isLogged = Session["UserId"] != null;
            if (isLogged)
            {
                if (!IsPostBack)
                {
                    BindCarrello();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void BindCarrello()
        {
            int utenteId = Convert.ToInt32(Session["UserId"]); // Assumi che l'ID dell'utente sia salvato nella sessione al momento del login.
            List<CarrelloItem> itemsDelCarrello = new List<CarrelloItem>();

            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT p.ImmagineURL, p.Nome, cd.Quantita, p.Prezzo, (cd.Quantita * p.Prezzo) AS Totale
                    FROM CarrelloDettaglio cd
                    INNER JOIN Prodotti p ON cd.ProdottoID = p.ProdottoID
                    INNER JOIN Carrello c ON cd.CarrelloID = c.CarrelloID
                    WHERE c.UtenteID = @UtenteID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UtenteID", utenteId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            itemsDelCarrello.Add(new CarrelloItem
                            {
                                ImmagineURL = reader["ImmagineURL"].ToString(),
                                Nome = reader["Nome"].ToString(),
                                Quantita = Convert.ToInt32(reader["Quantita"]),
                                Prezzo = Convert.ToDecimal(reader["Prezzo"]),
                                Totale = Convert.ToDecimal(reader["Totale"])
                            });
                        }
                    }
                }
            }

            carrelloRepeater.DataSource = itemsDelCarrello;
            carrelloRepeater.DataBind();
        }

        // Definisci una classe interna per rappresentare gli elementi del carrello
        protected class CarrelloItem
        {
            public string ImmagineURL { get; set; }
            public string Nome { get; set; }
            public int Quantita { get; set; }
            public decimal Prezzo { get; set; }
            public decimal Totale { get; set; }
        }
    }
}
