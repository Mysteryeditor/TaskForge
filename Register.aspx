<%@ Page Title="" Language="C#" MasterPageFile="~/landingpage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TaskManagement.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .required::after {
            content: "*";
            color: red;
        }
        .validation-message {
            margin-top: -10px;
            margin-bottom: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2>Register</h2>

        <div class="row">
            <div class="col-lg-8">
                  <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control mb-3" placeholder="First Name" />
  <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name is required." ValidationGroup="register" CssClass="text-danger validation-message" />

  <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control mb-3" placeholder="Last Name" />
  <!-- Last Name is optional -->

  <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-3" placeholder="Email" />
  <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required." ValidationGroup="register" CssClass="text-danger validation-message" />
  <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Enter a valid email ending with 'gmail.com'." ValidationGroup="register" ValidationExpression="^[a-zA-Z0-9._%+-]+@gmail\.com$" CssClass="text-danger validation-message" />

  <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control mb-3" placeholder="Password" />
  <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required." ValidationGroup="register" CssClass="text-danger validation-message" />
  <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password must match the pattern." ValidationGroup="register" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$" CssClass="text-danger validation-message" />

  <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control mb-3" placeholder="Confirm Password" />
  <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm Password is required." ValidationGroup="register" CssClass="text-danger validation-message" />
  <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" ErrorMessage="Passwords do not match." ValidationGroup="register" CssClass="text-danger validation-message" />

  <asp:TextBox ID="txtAge"  runat="server" CssClass="form-control mb-3" placeholder="Age" />
  <asp:RequiredFieldValidator ID="rfvAge" runat="server" ControlToValidate="txtAge" ErrorMessage="Age is required." ValidationGroup="register" CssClass="text-danger validation-message" />
  <%--<asp:RangeValidator ID="rvAge" runat="server" ControlToValidate="txtAge" ErrorMessage="Age must be greater than 12." MaximumValue="120" InitialValue="0" Type="Integer" ValidationGroup="register" CssClass="text-danger validation-message" />--%>

  <asp:TextBox ID="txtSecurityQuestion" runat="server" CssClass="form-control mb-3" placeholder="Security Question" />
  <asp:RequiredFieldValidator ID="rfvSecurityQuestion" runat="server" ControlToValidate="txtSecurityQuestion" ErrorMessage="Security Question is required." ValidationGroup="register" CssClass="text-danger validation-message" />
  <asp:RegularExpressionValidator ID="revSecurityQuestion" runat="server" ControlToValidate="txtSecurityQuestion" ErrorMessage="Security Question must be at least 10 characters long." ValidationGroup="register" ValidationExpression="^.{10,}$" CssClass="text-danger validation-message" />

  <asp:TextBox ID="txtSecurityAnswer" runat="server" CssClass="form-control mb-3" placeholder="Security Answer" />
  <asp:RequiredFieldValidator ID="rfvSecurityAnswer" runat="server" ControlToValidate="txtSecurityAnswer" ErrorMessage="Security Answer is required." ValidationGroup="register" CssClass="text-danger validation-message" />

                <div class="d-flex">
<asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary" ValidationGroup="register" OnClick="btnRegister_Click" />
                                        <p class="mx-5">Already a user? <a href="login.aspx">Login here</a>.</p>

                </div>
            </div>
            <div class="col-lg-4">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="register" CssClass="text-danger" />
            </div>
        </div>
    </div>
</asp:Content>


