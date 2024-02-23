using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace EcommerceBW4
{
    public partial class Profilo : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    int utenteId = Convert.ToInt32(Session["UserId"]);
                    CaricaOrdiniUtente(utenteId);
                }
                else
                {
                    // Reindirizza all'accesso se l'utente non è loggato
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void CaricaOrdiniUtente(int utenteId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetOrdiniUtente", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UtenteID", utenteId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Assegna i dati al Repeater
                RepeaterOrders.DataSource = dt;
                RepeaterOrders.DataBind();
            }
        }

        public DataTable GetDettagliOrdinePerProdotto(int ordineId)
        {
            DataTable dtDettagliOrdine = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["EcommerceBW4"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetDettagliOrdine", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrdineID", ordineId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtDettagliOrdine);
            }
            return dtDettagliOrdine;
        }

        // metdo per gestire il clicl sul pulsante "Dettagli" all'interno del Repeater
        protected void RepeaterOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ShowDetails")
            {
                int ordineId = Convert.ToInt32(e.CommandArgument);
                DataTable dtDettagliOrdine = GetDettagliOrdinePerProdotto(ordineId);


                Repeater repeaterOrderDetails = modalBody.FindControl("RepeaterOrderDetails") as Repeater;
                if (repeaterOrderDetails != null)
                {
                    repeaterOrderDetails.DataSource = dtDettagliOrdine;
                    repeaterOrderDetails.DataBind();
                }


                orderDetailsModal.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", "$('#orderDetailsModal').modal('show');", true);

            }
        }

        // Metodo per chiudere il modale con i dettagli dell'ordine
        protected void BtnCloseModal_Click(object sender, EventArgs e)
        {
            // Chiudi il modale
            orderDetailsModal.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closeModal", "$('#orderDetailsModal').modal('hide');", true);
        }

    }
}