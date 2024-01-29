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
    public partial class VotingPage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCon"].ConnectionString);
        int rowIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string VoterState = Session["VoterState"].ToString();
                BindVoterList(VoterState);
                btnClear.Visible = false;
                btnSubmit.Visible = true;
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

                SqlDataAdapter ad2 = new SqlDataAdapter("prc_GetPartyNameStateWise '" + State + "'", con);

                DataSet ds2 = new DataSet();
                ad2.Fill(ds2, "tblPart");
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    DrdVoting.DataSource = ds2;
                    DrdVoting.DataBind();
                    //btnSubmit.Visible = true;
                    DrdVoting.Visible = true;
                    LblMsg.Visible = false;                    
                }
                else
                {

                    DrdVoting.Visible = false;
                    btnSubmit.Visible = false;
                    LblMsg.Text = "No Record Found";
                    LblMsg.Visible = true;
                    btnSubmit.Visible = false;

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


        protected void btnClear_Click(object sender, EventArgs e)
        {
            string VoterState = Session["VoterState"].ToString();
            BindVoterList(VoterState);
            btnSubmit.Visible = true;
            btnClear.Visible = false;
        }



        protected void RadioButtonSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkVote = (CheckBox)sender;

            // Access the GridView row to get other data
            GridViewRow row = (GridViewRow)checkVote.NamingContainer;
            rowIndex = row.RowIndex;
            if (rowIndex > 0)
            {
                LblMsg.Text = "Please choose one";
                LblMsg.ForeColor = System.Drawing.Color.Red;
                btnClear.Visible = true;
                btnSubmit.Visible = false;
                LblMsg.Visible = true;                
            }

            // Perform any additional logic here based on the selected row
            // For example, you can get data from the selected row using GridView1.Rows[rowIndex].Cells[columnIndex].Text
        }

        protected void gdVoterList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void BtnSubmit_click(object sender, EventArgs e)
        {
            int intCount = 0;
            int intRowCount = 0;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string PartyName = "";
                foreach (GridViewRow Itm in DrdVoting.Rows)
                {
                    int checkedCount = 0;
                    intRowCount = intRowCount + 1;
                    CheckBox Chk;
                    Chk = (CheckBox)Itm.FindControl("Chk");
                    if (Chk != null && Chk.Checked)
                    {
                        checkedCount++;
                    }
                    if (checkedCount == 1)
                    {
                        if (Chk.Checked == true)
                        {
                            int ChkCount = 1;
                            SqlCommand cmd = new SqlCommand("Prc_InsertVoterData", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@VoterId", Session["VoterId"].ToString());
                            PartyName = Itm.Cells[1].Text.Trim();
                            cmd.Parameters.AddWithValue("@PartyName", PartyName);
                            cmd.Parameters.AddWithValue("@State", Session["VoterState"].ToString());
                            SqlParameter ParamResult1 = new SqlParameter("@Result", SqlDbType.VarChar, 100);
                            ParamResult1.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(ParamResult1);
                            cmd.ExecuteNonQuery();

                            if (ParamResult1.Value.ToString().Trim() == "Success")
                            {
                                LblMsg.Visible = true;
                                LblMsg.Text = "Your Vote has Succesfully Submitted";
                                LblMsg.ForeColor = System.Drawing.Color.DarkBlue;
                                ChkCount = ChkCount + 1;
                                btnClear.Visible = false;    

                        }
                            else
                            {
                                LblMsg.Visible = true;
                                LblMsg.Text = ParamResult1.Value.ToString();
                                LblMsg.ForeColor = System.Drawing.Color.DarkBlue;
                            }
                        }
                    else
                    {
                        LblMsg.Text = "Please choose one";
                        LblMsg.ForeColor = System.Drawing.Color.Red;
                        btnClear.Visible = true;
                        btnSubmit.Visible = false;
                        LblMsg.Visible = true;
                    }
                }

            }
                con.Close();
            

        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Mainpage.aspx");
            Session["VoterId"] = "";
            Session["VoterState"] = "";
        }
    }
}