﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartyRegistration.aspx.cs" Inherits="ElectionCommission.PartRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color:antiquewhite; height:unset;"> 
           <br />
        <div><center>
             <div>
                <a href="Admin.aspx"> HOME</a>
            </div>
            <div>
                <center> <h1> Candidate Registration Form </h1></center>
            </div>
            <br />
            <center>
                 <img alt="" style="position:fixed; width:420px; height:320px; top: 80px; left:20px"
                src="Images/Emblem.Png" alt="" />
            <div>
                <asp:Label ID="LblName" Text="Party Name : " Font-Size="Medium" runat="server" ></asp:Label> &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TxtName" runat="server" Width="200px"></asp:TextBox> 
            </div>
                <br />
            <div>   
                <asp:Label ID="lblVoter" Text="Voter ID : " Font-Size="Medium" runat="server" ></asp:Label> &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TxtVoterId" runat="server" Width="200px"></asp:TextBox> 
            </div>
                
            
                <br />
            <div>
                <asp:Label ID="LblAddr" Text="Address : " Font-Size="Medium" runat="server" ></asp:Label> &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TxtAddr" runat="server" Width="200px"></asp:TextBox> 
            </div>
                <br />
            <div>
                <asp:Label ID="LblState" Text="State : " Font-Size="Medium" runat="server" ></asp:Label> &nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" ViewStateMode="Enabled" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" TabIndex="4" Width="200px" Height="23px"></asp:DropDownList>
            </div>

                <br />
            <div>
<%--                <asp:Label ID="lblPrf" Text="Document Proof : " Font-Size="Medium" runat="server" ></asp:Label> &nbsp;&nbsp;&nbsp;
             <asp:FileUpload id="FileUploadControl" runat="server" />
                <INPUT class="textbox" id="proof1" style="WIDTH: 326px; HEIGHT: 23px" visible="false" type="file" size="35" runat="server">--%>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:label id="lblPrf" runat="server" Width="120px">Upload your Symbol :</asp:label>
				<%--<INPUT class="textbox" id="proof1" style="WIDTH: 326px; HEIGHT: 23px" type="file"
										size="35" runat="server">--%>
                <asp:FileUpload ID="fileUpload" runat="server" />
           
            </div>
                <br />
               
                <div>
                    <asp:Button ID="btnSubmit" runat="server" Text="Register" Height="25px" Width="120px" BackColor="#000099" ForeColor="#ffffcc" OnClick="BtnSubmit_click" />
                </div>
                <br /><br />
                <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
                <br />
                
                <div style="height:220px;"></div>
            </center>
        </div>
        </div>
    </form>
</body>
</html>
