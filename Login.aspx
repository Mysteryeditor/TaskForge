<%@ Page Title="" Language="C#" MasterPageFile="~/landingpage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TaskManagement.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Include Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card mt-5">
                    <div class="card-header">
                        <asp:label runat="server">Login</asp:label>
                    </div>
                    <div class="card-body">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-3" placeholder="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." ValidationGroup="login" CssClass="text-danger"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Enter a valid email address." ValidationGroup="login" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="text-danger"></asp:RegularExpressionValidator>

                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control mb-3" placeholder="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required." ValidationGroup="login" CssClass="text-danger"></asp:RequiredFieldValidator>

                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-block" ValidationGroup="login" OnClick="btnLogin_Click" />

                        <div class="mt-3 text-center">
                            <asp:HyperLink ID="lnkForgotPassword" runat="server" NavigateUrl="~/ForgotPassword.aspx" Text="Forgot Password?" CssClass="text-primary"></asp:HyperLink>
                        
                        <div class="mt-2">
                            <label>New here? </label><asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="~/Register.aspx" Text="Register" CssClass="text-primary"></asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
