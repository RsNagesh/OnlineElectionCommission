<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ElectionResult.aspx.cs" Inherits="ElectionCommission.ElectionResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color:gainsboro; height:unset;">
            <div>
              <br /><br />
            <center><div>
                <a href="Admin.aspx"> HOME</a>
            </div></center>
            <br /><br />
            <div style="text-align:center; ">Please choose State &nbsp;&nbsp;&nbsp;
                		<asp:DropDownList id="ddlState" runat="server"  Width="250px" Height="23px" 
                            BorderStyle="Solid" BorderWidth="1px" AutoPostBack="True" 
                            onselectedindexchanged="ddlState_SelectedIndexChanged">
							
						</asp:DropDownList>
            </div>
        <br /><br /><center>

<asp:GridView ID="GrdVoting" runat="server" AutoGenerateColumns="False" Width="950px" ForeColor="#333333" GridLines="None">
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
                    <asp:BoundField DataField="PartyName" HeaderText="Party Name" SortExpression="ID" />                    
                    <asp:BoundField DataField="VoteCnt" HeaderText="Vote Count" SortExpression="ID" />
                    
                </Columns>
            </asp:GridView>

            </div>
            <br /><br />
            <div>
                <center> 
                    <asp:Label ID="LblMsg" runat="server" CssClass="page" Font-Size="Medium" ForeColor="Red"></asp:Label>
                    <br /><br />
            </center>   
           </div>
            <div style="height:300px;">

            </div>
        </div>
    </form>
</body>
</html>
