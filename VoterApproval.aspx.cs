using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Collections;

namespace ElectionCommission
{
    public partial class VoterApproval : System.Web.UI.Page
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
                BindVoterList(ddlState.SelectedValue.ToString().Trim());
            }
            else
            {

            }
        }

        private void BindVoterList(string State)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {

                SqlDataAdapter ad2 = new SqlDataAdapter("prc_GetVoterApprovalList '" + State + "'", con);

                DataSet ds2 = new DataSet();
                ad2.Fill(ds2, "tblPart");
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    gdVoterList.DataSource = ds2;
                    gdVoterList.DataBind();
                    btnSubmit.Visible = true;
                    gdVoterList.Visible = true;
                    LblMsg.Visible = false;
                    btnSubmit.Visible = true;

                }
                else
                {

                    gdVoterList.Visible = false;
                    btnSubmit.Visible = false;
                    LblMsg.Text = "No Record Found";
                    LblMsg.Visible = true;
                    btnSubmit.Visible = false;

                }

            }
            catch (Exception ex)
            {
                
                LblMsg.Text=(ex.ToString());
                
            }

            finally
            {
                   
            }
            con.Close();
        }

        protected void gdVoterList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string Changenoteno = "";
            if (e.CommandName == "View" || e.CommandName == "View1")
            {

                Changenoteno = e.CommandArgument.ToString();
                if (Changenoteno != "")
                {

                    DownloadFile(Changenoteno);
                    //DownloadFile(Changenoteno,true );
                }
            }

            else if (e.CommandName == "Yes")
            {

                //string pageurl = "ViewBlogs.aspx?blogid=" + e.CommandArgument.ToString() + "&action=view";

                //string script = "<script language="'javascript'">viewblogs('" + pageurl + "');</script>";
                //ScriptManager.RegisterStartupScript(Page, typeof(string), "clientScript", script, 
                //Changenoteno = e.CommandArgument.ToString();
                Page.RegisterStartupScript("selectrefno", "<script> 	window.open('CTHistoryRpt.aspx?sn=" + e.CommandArgument.ToString() + "','_new','height=500,width=750,scrollbars=yes,resizable=yes,modal=yes');</script>");
            }


        }

        public void DownloadFile(string fileName)
        {

            bool filedosentexist = false;
            string strServerPath;
            ArrayList al = new ArrayList();
            al.Add(".jpg");
            //al.Add(".xls");
            //al.Add(".pdf");
            int i = 0;


            strServerPath = Server.MapPath("POPIMAGE\\" + fileName);
            strServerPath = strServerPath.Replace("\\HPACCESSORIES\\HPACCESSORIES", "\\HPACCESSORIES");




            while (i < al.Count)
            {
                System.IO.FileInfo file = new System.IO.FileInfo(strServerPath.ToString());
                //System.IO.FileInfo file = new System.IO.FileInfo(path);
                if (file.Exists)
                {

                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    i = al.Count;
                    filedosentexist = false;
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    filedosentexist = true;
                }

                i++;
            }

            if (filedosentexist)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "File Does not Exist", "<script>alert('File Does not Exist in System')</script>", false);
                return;

            }
        }

        protected void gdVoterList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int intCount = 0;
            int intRowCount = 0;
            string VoterID = "";
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            foreach (GridViewRow Itm in gdVoterList.Rows)
            {
                intRowCount = intRowCount + 1;
                CheckBox Chk1;
                Chk1 = (CheckBox)Itm.FindControl("Chk");
                if (Chk1.Checked == true)
                {
                    VoterID=Itm.Cells[1].Text.Trim();

                    SqlCommand sqlcmd = new SqlCommand("prc_UpdateApproval", con);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.Add("@VoterId", VoterID);
                    
                    sqlcmd.ExecuteNonQuery();

                    BindVoterList(ddlState.SelectedValue.ToString().Trim());
                }

            }

        }
    }
}