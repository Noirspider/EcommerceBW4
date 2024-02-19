﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            int utenteId = Convert.ToInt32(Session["UserId"]);
            List<CarrelloItem> itemsDelCarrello = new List<CarrelloItem>();

            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                   SELECT cd.ProdottoID, p.ImmagineURL, p.Nome, cd.Quantita, p.Prezzo, (cd.Quantita * p.Prezzo) AS Totale
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
                                ProdottoID = Convert.ToInt32(reader["ProdottoID"]),
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

        // Definisco una classe interna per rappresentare gli elementi del carrello
        protected class CarrelloItem
        {
            public int ProdottoID { get; set; }
            public string ImmagineURL { get; set; }
            public string Nome { get; set; }
            public int Quantita { get; set; }
            public decimal Prezzo { get; set; }
            public decimal Totale { get; set; }
        }

        // metodo per gestire il clicl sul pulsante "Rimuovi dal carrello" 
        // e per gestire il clicl sul pulsante "Aggiungi al carrello"

        protected void CarrelloRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int prodottoId = Convert.ToInt32(e.CommandArgument);
            int utenteId = Convert.ToInt32(Session["UserId"]);
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (e.CommandName == "RemoveOne")
                {
                    string query = @"
                UPDATE CarrelloDettaglio
                SET Quantita = Quantita - 1
                WHERE ProdottoID = @ProdottoID AND CarrelloID IN (
                    SELECT CarrelloID
                    FROM Carrello
                    WHERE UtenteID = @UtenteID
                ) AND Quantita > 0";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProdottoID", prodottoId);
                        cmd.Parameters.AddWithValue("@UtenteID", utenteId);
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (e.CommandName == "AddOne")
                {
                    string query = @"
                UPDATE CarrelloDettaglio
                SET Quantita = Quantita + 1
                WHERE ProdottoID = @ProdottoID AND CarrelloID IN (
                    SELECT CarrelloID
                    FROM Carrello
                    WHERE UtenteID = @UtenteID
                )";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProdottoID", prodottoId);
                        cmd.Parameters.AddWithValue("@UtenteID", utenteId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            BindCarrello(); // Aggiorna il carrello a schermo
        }

        // metodo per gestire il clicl sul pulsante "Svuota carrello"
        protected void BtnClearCart_Click(object sender, EventArgs e)
        {
            int utenteId = Convert.ToInt32(Session["UserId"]);
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                DELETE FROM CarrelloDettaglio
                WHERE CarrelloID IN (
                SELECT CarrelloID
                FROM Carrello
                WHERE UtenteID = @UtenteID
            )";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UtenteID", utenteId);
                    cmd.ExecuteNonQuery();
                }
            }

            BindCarrello(); // Aggiorna il carrello a schermo
        }

        // metodo per gestire il clicl sul pulsante "Procedi all'acquisto"
        protected void BtnCompleteOrder_Click(object sender, EventArgs e)
        {
            int utenteId = Convert.ToInt32(Session["UserId"]);
            int carrelloId = GetCarrelloId(utenteId);

            // Redirezione alla pagina di checkout con l'ID del carrello come parametro nella query string
            Response.Redirect($"Checkout.aspx?carrelloId={carrelloId}");
        }

        // metodo per recuperare l'ID del carrello dell'utente
        private int GetCarrelloId(int utenteId)
        {
            // Implementazione del metodo per recuperare l'ID del carrello dell'utente
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            int carrelloId = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT CarrelloID FROM Carrello WHERE UtenteID = @UtenteID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UtenteID", utenteId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            carrelloId = Convert.ToInt32(reader["CarrelloID"]);
                        }
                    }
                }
            }

            return carrelloId;
        }

    }
}
