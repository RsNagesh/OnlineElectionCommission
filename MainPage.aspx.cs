using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace ElectionCommission
{
    
    public partial class MainPage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCon"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSubmit_click(object sender, EventArgs e)
        {
            Response.Redirect("VoterCreation.aspx");
        }

        protected void BtnLogin_click(object sender, EventArgs e)
        {
            string UserId = "";
            string UserName = "";
            string VoterId = "";
            int Approval = 0;
            SqlDataReader sqlrdr;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            String pswd = txtPaswd.Text.ToString();
            string query = "CheckVoterLogin";   //stored procedure Name
            SqlCommand com = new SqlCommand(query, con);
            com.CommandType = CommandType.StoredProcedure;
            string pswd1 = Base64Encode(pswd);
            com.Parameters.AddWithValue("@VoterID", txtUser.Text.ToString().Trim());   //for username 
            com.Parameters.AddWithValue("@UserPwd", Base64Encode(pswd));  //for password
                                                                           //int usercount = (Int32)com.ExecuteScalar();// for taking single value
            sqlrdr = com.ExecuteReader();
            if (sqlrdr.HasRows)
            {
                if (sqlrdr.Read())
                {
                    Session["VoterId"] = sqlrdr[0].ToString();
                    VoterId = sqlrdr[0].ToString();
                    UserName = sqlrdr[1].ToString();
                    Approval= Convert.ToInt16(sqlrdr[2].ToString());
                    Session["VoterState"]= sqlrdr[3].ToString();
                } // end of while					
            } // end of if for sqlrdr.hasrows

            if ((txtUser.Text.ToString().Trim() == "Admin") && (txtPaswd.Text.ToString().Trim() == "Admin@123"))
            {   
                Response.Redirect("Admin.aspx");
            }
            else
            {
                if (VoterId == "" && UserName=="")
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Invalid Credentials";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
                else if (VoterId != "" && Approval==0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Your Registration still not Approved";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
                else if (VoterId != "" && Approval == 1)
                {
                    Response.Redirect("VotingPage.aspx");
                }
            }
            con.Close();

        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}