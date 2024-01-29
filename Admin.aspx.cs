using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElectionCommission
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void VoterApproval_Click(object sender, EventArgs e)
        {
            Response.Redirect("VoterApproval.aspx");
        }

        protected void PartReg_Click(object sender, EventArgs e)
        {
            Response.Redirect("PartyRegistration.aspx");
        }

        protected void VoterList_Click(object sender, EventArgs e)
        {
            Response.Redirect("VoterList.aspx");
        }

        protected void ElectionResult_Click(object sender, EventArgs e)
        {
            Response.Redirect("ElectionResult.aspx");
        }

        protected void PartyList_Click(object sender, EventArgs e)
        {
            Response.Redirect("Partylist.aspx");
        }
        
        protected void Logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Mainpage.aspx");
            Session["VoterId"] = "";
            Session["VoterState"] = "";
        }
    }
}