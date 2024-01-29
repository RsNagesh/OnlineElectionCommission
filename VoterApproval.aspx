<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoterApproval.aspx.cs" Inherits="ElectionCommission.VoterApproval" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color:gainsboro; height:unset;"> 
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
            <asp:GridView ID="gdVoterList" PageSize="30" runat="server" CellPadding="2"
                                                    AutoGenerateColumns="False" Width="950px" ForeColor="#333333" GridLines="None"
                                                    Font-Names="arial,helvetica,sans-serif" Font-Size="11px" 
                                                    OnRowDataBound="gdVoterList_RowDataBound" 
                                                    onrowcommand="gdVoterList_RowCommand" HeaderStyle-Wrap ="false" AlternatingRowStyle-Wrap ="false" >
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
                                                        <asp:BoundField DataField="VoterId" HeaderText="Voter ID" />
                                                        <asp:BoundField DataField="Name" HeaderText="Name" />
                                                        <asp:BoundField DataField="Address" HeaderText="Address" />
                                                        <asp:BoundField DataField="State" HeaderText="State" />


                                                        <asp:TemplateField HeaderText="CHOOSE">
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                         <%--   <HeaderTemplate>
                                                                <asp:CheckBox ID="CheckAll" onclick="javascript: return select_deselectAll (this.checked, this.id);"
                                                                    runat="server"></asp:CheckBox>
                                                            </HeaderTemplate>--%>
                                                            <ItemTemplate>
                                                                &nbsp;
                                                                <asp:CheckBox ID="Chk" runat="server"></asp:CheckBox>&nbsp;
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                           <asp:TemplateField HeaderText="APPROVE/REJECT">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlAPPROVEREJECT" CssClass="page"  runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                                    Wrap="true" Width="100px">
                                                                    <asp:ListItem Value ="SELECT" Selected ="True">SELECT</asp:ListItem>
                                                                    <asp:ListItem Value ="APPROVE">APPROVE</asp:ListItem>                                                                    
                                                                </asp:DropDownList>
                                                            </ItemTemplate>

                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                         <%--<asp:TemplateField HeaderText="Document Proof">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkRequestDetail2" runat="server" 
                                                                CommandArgument='<%#DataBinder.Eval(Container.DataItem,"view")%>' 
                                                                CommandName="View" Text='<%#DataBinder.Eval(Container.DataItem,"VoterId")%>'>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                             <ItemStyle Wrap="False" />
                                                        </asp:TemplateField> --%>  
                                                        
                                                    </Columns>
                                                </asp:GridView>
            <br />
           </center>
            <div>
                <center> 
                    <asp:Label ID="LblMsg" runat="server" CssClass="page" Font-Size="Medium" ForeColor="Red"></asp:Label>
                    <br /><br />
                    <asp:Button ID="btnSubmit" CssClass="button small white" runat="server" Text="Submit"
               OnClick="btnSubmit_Click" BackColor="#000099" ForeColor="WhiteSmoke" CausesValidation="False" Visible="true" />
            </center>   
           </div>
        </div>
    </form>
</body>
</html>
