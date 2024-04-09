<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TaskManagement.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
            <!-- Welcome message with username -->
    <div class="row justify-content-center">
        <div class="col-12 text-center">
            <h1>
                <asp:Label ID="userLabel" runat="server"></asp:Label>
            </h1>
        </div>
    </div>
        <div class="row mt-5">
            <div class="col-md-12">
                <div id="accordion">
                    <div class="card">
                        <div class="card-header" id="headingOne">
                            <h5 class="mb-0">
                                <button class="btn" data-toggle="collapse" onclick="collapseAccordion()">
                                    Create New Task
                                </button>
                            </h5>
                        </div>

                        <div>
                            <div class="card-body d-flex align-items-center justify-content-evenly">
                                <div class="col-3">
                                    <asp:TextBox ID="txtTaskName" runat="server" CssClass="form-control" placeholder="Task Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTaskName" runat="server" ControlToValidate="txtTaskName" ErrorMessage="Task name is required." Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-3">
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Description"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="Description is required." Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-3">
                                    <asp:TextBox ID="txtDeadline" runat="server" CssClass="form-control" placeholder="Deadline (YYYY-MM-DD)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDeadline" runat="server" ControlToValidate="txtDeadline" ErrorMessage="Deadline is required." Display="Dynamic" CssClass="text-danger"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revDeadline" runat="server" ControlToValidate="txtDeadline" ErrorMessage="Invalid deadline format. Please use YYYY-MM-DD." ValidationExpression="^\d{4}-\d{2}-\d{2}$" Display="Dynamic" CssClass="text-danger"></asp:RegularExpressionValidator>
                                </div>
                            <asp:Button ID="btnCreateTask" runat="server" Text="Create Task" CssClass="btn btn-primary" OnClick="btnCreateTask_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <!-- Task cards -->
    <div class="row mt-5">
        <!-- Pending tasks card -->
        <div class="col-md-6">
            <div class="card">
                <div class="card-header">
                    Pending Tasks
                </div>
                <div class="card-body" id="pendingTasks" runat="server">
                </div>
            </div>
        </div>

        <!-- Completed tasks card -->
        <div class="col-md-6">
            <div class="card">
                <div class="card-header">
                    Completed Tasks
                </div>
                <div class="card-body" id="completedTasks" runat="server">
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
