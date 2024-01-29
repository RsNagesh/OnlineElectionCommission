<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="ElectionCommission.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
    <style>
        .buttonContainer {
            text-align: center;
        }

        .imageButton {
            margin: 0 60px; /* Adjust the margin as needed */
        }
    </style>
<body>
    <form id="form1" runat="server">       
        <div style="background-color:blanchedalmond;">
            <br /><br /><br />

            <div style="height:30px; width:1000px; text-align:right ;">
                <asp:Button ID="btnlogout" runat="server" Text="LOG OUT" BackColor="#000000" ForeColor="Wheat" OnClick="Logout_Click" />
            </div>
            <div style="height:500px">
                     
<br /><br />
         <div class="buttonContainer">
            <asp:Button ID="ImageButton2" runat="server" BackColor="#006600" ForeColor="WhiteSmoke" Width="200px" Height="150px" Font-Bold="true" CssClass="imageButton" Text="Voter Approval"  OnClick="VoterApproval_Click" />
            <asp:Button ID="ImageButton3" runat="server" CssClass="imageButton" BackColor="#660066" ForeColor="Wheat" Font-Bold="true" Width="200px" Height="150px" Text="Party Registration" OnClick="PartReg_Click" />
            <asp:Button ID="ImageButton1" runat="server" CssClass="imageButton" BackColor="#000066" ForeColor="Wheat" Font-Bold="true" Width="200px" Height="150px  " Text="Voter List" OnClick="VoterList_Click" />
        </div>
                <br /><br /><br />

         <div class="buttonContainer">
            <asp:Button ID="Button1" runat="server" BackColor="#990000" ForeColor="WhiteSmoke" Width="200px" Height="150px" Font-Bold="true" CssClass="imageButton" Text="Party List"  OnClick="PartyList_Click" />
            <asp:Button ID="btnResult" runat="server" CssClass="imageButton" BackColor="#660033" ForeColor="Wheat" Font-Bold="true" Width="200px" Height="150px" Text="Election Result" OnClick="ElectionResult_Click" />            
        </div>
                    
            </div>

        </div>
    </form>
</body>
</html>
