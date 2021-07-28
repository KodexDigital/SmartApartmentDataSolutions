<%@ Page Language="C#" Title="Login" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SmartApp.WebForm.Login" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <div style="margin-top: 50px">
          <div class="col-md-3"></div>
        <div class="col-md-6">
                 <div class="panel" style="margin-top:30px">
                   <div class="panel-body jumbotron">
                         <table align="left" class="nav-center" style="width: 100%">
                        <tr>
                            <td>
                                <div class="pb-4 sign-up-title">
                                    <h2>Log in</h2>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <label>Email <asp:RequiredFieldValidator ID="emailRequired" runat="server" ErrorMessage="Email is required" ControlToValidate="txtEmail" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </label>
                                    &nbsp;<asp:TextBox ID="txtEmail" runat="server" class="form-control input_control" Placeholder="Enter your email"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="emailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid email format" SetFocusOnError="true" ForeColor="Red" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group">
                                    <label>Password  <asp:RequiredFieldValidator ID="passwordRequired" runat="server" ErrorMessage="Password is required" ControlToValidate="txtPassword" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator></label>
                                    <asp:TextBox ID="txtPassword" runat="server" class="form-control input_control" TextMode="Password" Placeholder="Enter your password"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="BtnLogin" runat="server" Text="Login" class="btn btn-primary rounded btn-block" OnClick="BtnLogin_Click" />
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblStatusText" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                    </div>
                </div>
        </div>
        <div class="col-md-3"></div>
     </div>
</asp:Content>