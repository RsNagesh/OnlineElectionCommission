<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VotingPage.aspx.cs" Inherits="ElectionCommission.VotingPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color:gainsboro; height:unset;"> 
            <br />
            <div style="height:30px; width:1000px; text-align:right ;">
                <asp:Button ID="btnlogout" runat="server" Text="LOG OUT" BackColor="#000000" ForeColor="Wheat" OnClick="Logout_Click" />
            </div>
           <center><h3>Online Election Commission</h3></center> 
            <br /><br />
            <div>
            <center>
                <asp:Label ID="lblvot" runat="server" Text="Please Choose Your Vote" Font-Size="Larger"></asp:Label>
                <br /><br />
                <asp:GridView ID="DrdVoting" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdVoterList_RowDataBound"
                    Width="950px" ForeColor="#333333" GridLines="None">
                    <EditRowStyle BackColor="#999999" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333"  />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#2f81bb" Font-Size="12px" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                                                        <asp:TemplateField HeaderText="S.NO" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>  
                    <asp:BoundField DataField="Name" HeaderText="Party Name" SortExpression="ID" />
                    <%--<asp:BoundField DataField="Symbol" HeaderText="Symbol" SortExpression="Name" />--%>
                    <asp:ImageField DataImageUrlField="ImageData" DataImageUrlFormatString="data:image/jpeg;base64,{0}" HeaderText="Symbol" ControlStyle-Height="100" ControlStyle-Width="100" />
        <asp:TemplateField HeaderText="Choose Your Vote">
            <ItemTemplate>
                <%--<asp:RadioButton ID="RadioButtonSelect" runat="server" GroupName="SelectionGroup" AutoPostBack="true" OnCheckedChanged="RadioButtonSelect_CheckedChanged" />--%>
                <asp:CheckBox ID="Chk" runat="server" OnCheckedChanged="RadioButtonSelect_CheckedChanged"></asp:CheckBox>&nbsp;
            </ItemTemplate>
        </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div>
                <br /><br />
                <center> 
                    <asp:Label ID="LblMsg" runat="server" CssClass="page" Font-Size="Medium" ForeColor="Red"></asp:Label>
                    <br />
                    <asp:Button ID="btnClear" runat="server" Text="Clear"
               OnClick="btnClear_Click" BackColor="#000099" ForeColor="WhiteSmoke" CausesValidation="False" Visible="false" />
                    <br />
                    <asp:Button ID="btnSubmit" runat="server" Text="Sumbit" Height="25px" Width="120px" BackColor="#000099" ForeColor="#ffffcc" OnClick="BtnSubmit_click" />
            </center>   
           </div>
            </center>
                </div>
        </div>
    </form>
</body>
</html>
