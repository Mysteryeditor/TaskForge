<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TaskManagement.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <!-- Welcome message with username -->
        <div class="row justify-content-center">
            <div class="col-12 text-center">
                <h1><asp:Label ID="userLabel" runat="server"></asp:Label></h1>
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
                    <div class="card-body">
                        <!-- Add content for pending tasks here -->
                    </div>
                </div>
            </div>

            <!-- Completed tasks card -->
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        Completed Tasks
                    </div>
                    <div class="card-body">
                        <!-- Add content for completed tasks here -->
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
