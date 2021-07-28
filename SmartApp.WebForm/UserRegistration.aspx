<%@ Page Language="C#" Title="User registration" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs" Inherits="SmartApp.WebForm.UserRegistration" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top: 50px">
        <div class="col-md-3"></div>
        <div class="container h-100 col-md-6">
            <div class="row h-100 justify-content-center align-items-center">
                <div class="panel" style="margin-top: 30px">
                    <div class="panel-body jumbotron">
                        <table align="left" class="nav-center" style="width: 100%">
                            <tr>
                                <td>
                                    <div class="pb-4 sign-up-title">
                                        <h2>User registration</h2>
                                    </div>
                                    <div class="pb-4">
                                        <h6>Please sign up with your real name and email.
                            Once you entered it, yo will not be able to change it later.
                                        </h6>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="form-group">
                                        <label>
                                            Email
                                            <asp:RequiredFieldValidator ID="emailRequired" runat="server" ErrorMessage="Email is required" ControlToValidate="txtEmail" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </label>
                                        <asp:RegularExpressionValidator ID="emailValidator" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Invalid email format" SetFocusOnError="true" ForeColor="Red" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                                        &nbsp;<asp:TextBox ID="txtEmail" runat="server" class="form-control" Placeholder="Enter your email"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Phone number 
                                            <asp:RequiredFieldValidator ID="phoneRequired" runat="server" ErrorMessage="Phone number is required" ControlToValidate="txtPhoneNumber" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator></label>
                                        <asp:TextBox ID="txtPhoneNumber" runat="server" class="form-control input_control" Placeholder="Enter your phone number"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Password 
                                            <asp:RequiredFieldValidator ID="passwordRequired" runat="server" ErrorMessage="Password is required" ControlToValidate="txtPassword" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator></label>
                                        <asp:TextBox ID="txtPassword" runat="server" class="form-control input_control" TextMode="Password" Placeholder="Enter your password"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>
                                            Confrim Password 
                                            <asp:RequiredFieldValidator ID="confirmPasswordRequired" runat="server" ErrorMessage="Confirm password is required" ControlToValidate="txtConfirmPassword" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator></label>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" class="form-control input_control" TextMode="Password" Placeholder="Retype password"></asp:TextBox>
                                        <asp:CompareValidator ID="passwordCompare" runat="server" ErrorMessage="Password mis-matched" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ForeColor="Red" SetFocusOnError="True"></asp:CompareValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="BtnRegister" runat="server" Text="Create Account" class="btn btn-primary rounded btn-block" OnClick="BtnRegister_Click" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblStatusMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-3"></div>
    </div>
</asp:Content>
