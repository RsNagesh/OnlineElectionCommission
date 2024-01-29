using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ElectionCommission
{
    public partial class ElectionResult : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCon"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindState();
            }
        }

        private void BindState()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter ad = new SqlDataAdapter("prc_getState", con);
            ad.SelectCommand.CommandTimeout = 10000;
            DataSet ds = new DataSet();
            ad.Fill(ds, "tblWono");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlState.DataTextField = ds.Tables[0].Columns[0].ToString();
                ddlState.DataValueField = ds.Tables[0].Columns[1].ToString();
                ddlState.DataSource = ds;
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("Please Select", String.Empty));
                ddlState.SelectedIndex = 1;
                //lnkCalldetails.Text = "Add Details";
            }

            con.Close();
            //obj = null;     
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue != String.Empty)
            {
                BindVoteCount(ddlState.SelectedValue.ToString().Trim());
            }
            else
            {

            }
        }

        private void BindVoteCount(string State)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {

                SqlDataAdapter ad2 = new SqlDataAdapter("prc_GetVoteCount '" + State + "'", con);

                DataSet ds2 = new DataSet();
                ad2.Fill(ds2, "tblPart");
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    GrdVoting.DataSource = ds2;
                    GrdVoting.DataBind();
                    GrdVoting.Visible = true;
                    LblMsg.Visible = false;
                    GrdVoting.Visible = true;

                }
                else
                {

                    GrdVoting.Visible = false;                   
                    LblMsg.Text = "No Record Found";
                    LblMsg.Visible = true;

                }

            }
            catch (Exception ex)
            {

                LblMsg.Text = (ex.ToString());

            }

            finally
            {

            }
            con.Close();
        }
    }
}