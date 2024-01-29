<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="ElectionCommission.MainPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="background-color:lightsteelblue; height:auto;" >
        <div>
            <br /><br />
            <center><h2>Online Election Commission</center></h2>
   
            <div><center>
                         <img alt="" style="position: inherit; width: 350px; height: 290px; top: 190px; left: 10px"
                src="Images/Voter.png" alt="" />
                <br /><br />
                <div style="width:400px">
                <asp:Label ID="lblUserName" Text="Admin/User :" Font-Size="Larger" Font-Bold="true" runat="server" ></asp:Label> 
                <asp:TextBox ID="txtUser" runat="server" Width="150px" placeholder="Admin / Voter ID"></asp:TextBox> </div>
                <br />
                 <asp:Label ID="lblPwd" Text="Password : " Font-Size="Larger" Font-Bold="true" runat="server" ></asp:Label> &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtPaswd" runat="server" placeholder="Password" Width="150px"></asp:TextBox> 
                
                <br /><br />
                <asp:Button ID="btnLogin" runat="server" BackColor="#339933" ForeColor="White" Text="Login" Width="150px" Height="25px" OnClick="BtnLogin_click" />&nbsp;&nbsp;
                <asp:Button ID="btnVoterCre" runat="server" BackColor="#3333cc" ForeColor="White" Text="Voter Creation" Width="150px" Height="25px" OnClick="BtnSubmit_click" />
                <br /><br />
                <asp:Label ID="lblmsg" runat="server" Visible="false" Font-Size="Medium" Font-Bold="true"></asp:Label>
                </center>
                <div style="height:60px;"></div>
            </div>
        </div>
    </form>
</body>
</html>
