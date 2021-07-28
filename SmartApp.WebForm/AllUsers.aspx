<%@ Page Language="C#" Title="All users" AutoEventWireup="true" CodeBehind="AllUsers.aspx.cs" Inherits="SmartApp.WebForm.AllUsers" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <div style="margin-top: 50px">
            <asp:SqlDataSource ID="SqlDataSource_All_Users" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [Email], [UserName], [PhoneNumber] FROM [AspNetUsers] ORDER BY [UserName] DESC, [PhoneNumber]"></asp:SqlDataSource>
        </div>
        <div class="col-md-2"></div>
        <div class="col-md-8">
            <div class="panel" style="margin-top: 30px">
                <div class="panel-body jumbotron">
                    <table align="center" cellpadding="3" class="auto-style1">
                        <tr>
                            <td>
                                <h3>All application user | <asp:Label ID="lblUserInfo" runat="server" ForeColor="Green" Font-Size="10"></asp:Label> | <asp:LinkButton ID="LblLknbtnLogout" runat="server" Text="Logout" Visible="true" Font-Size="12" OnClick="LblLknbtnLogout_Click"></asp:LinkButton></h3>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridV_All_Users" runat="server" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" DataSourceID="SqlDataSource_All_Users" GridLines="Horizontal" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="UserName" HeaderText="Username" SortExpression="UserName">
                                            <HeaderStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone number" SortExpression="PhoneNumber">
                                            <HeaderStyle Width="200px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                    <SortedAscendingHeaderStyle BackColor="#487575" />
                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                    <SortedDescendingHeaderStyle BackColor="#275353" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-md-2"></div>
</asp:Content>