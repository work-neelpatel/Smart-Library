<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Publishers.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Publishers</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopIconbar" Runat="Server">
                <a href="Books.aspx" class="text-secondary mr-2">
                        <abbr title="Books"><i class="fa fa-book"></i></abbr>
                </a>
                <a href="Students.aspx" class="text-secondary mr-2">
                        <abbr title="Students"><i class="fa fa-graduation-cap"></i></abbr>
                </a>
                <a href="Issue-Book.aspx" class="text-secondary mr-2">
                        <abbr title="Issue Book"><i class="fa fa-arrow-circle-left"></i></abbr>
                </a>
                <a href="Return-Book.aspx" class="text-secondary mr-2">
                        <abbr title="Return Book"><i class="fa fa-arrow-circle-right"></i></abbr>
                </a>
                <a href="Pending-Books.aspx" class="text-secondary mr-2">
                        <abbr title="Pending Books"><i class="fa fa-sign-in"></i></abbr>
                </a>
                <a href="Transaction-History.aspx" class="text-secondary mr-2">
                        <abbr title="History"><i class="fa fa-history"></i></abbr>
                </a>
                <a href="Book-on-Read.aspx" class="text-secondary mr-2">
                        <abbr title="Books on Read"><i class="fa fa-building"></i></abbr>
                </a>
                <a href="Book-Requests.aspx" class="text-secondary mr-2">
                        <abbr title="Book Requests"><i class="fa fa-paper-plane"></i></abbr>
                </a>
                <a href="Book-Suggestions.aspx" class="text-secondary">
                        <abbr title="Book Suggestions"><i class="fa fa-lightbulb-o fa-lg"></i></abbr>
                </a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
                    <div class="container-fluid">
                        <div class="form-inline mb-4">
                            <h1 class="mt-4 col-xl-12 pl-0">Publishers</h1>
                        </div>
                        <div class="form-inline mb-4 pb-2 border-bottom border-secondary">
                            <div class="form-inline pl-0 col-xl-4 col-sm-4 col-8">
                                <div class="dropdown pl-0 col-xl-4 col-sm-6 col-12">
                                    <asp:DropDownList ID="Field" runat="server" class="form-control btn btn-secondary dropdown-toggle" data-toggle="dropdown" style="height:auto;text-transform: capitalize;" role="button" aria-haspopup="true" aria-expanded="s">
                                        <asp:ListItem Value="B.C.A.">B.C.A.</asp:ListItem>
                                        <asp:ListItem Value="B.Com.">B.Com.</asp:ListItem>
                                        <asp:ListItem Value="B.B.A.">B.B.A.</asp:ListItem>
                                        <asp:ListItem Value="All"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="dropdown col-xl-6 col-sm-6 col-12 pl-0">
                                    <asp:DropDownList ID="Semester" runat="server" class="form-control btn btn-secondary dropdown-toggle" data-toggle="dropdown" style="height:auto;text-transform: capitalize;" role="button" aria-haspopup="true" aria-expanded="s">
                                        <asp:ListItem Value="1">Semester 1</asp:ListItem>
                                        <asp:ListItem Value="2">Semester 2</asp:ListItem>
                                        <asp:ListItem Value="3">Semester 3</asp:ListItem>
                                        <asp:ListItem Value="4">Semester 4</asp:ListItem>
                                        <asp:ListItem Value="5">Semester 5</asp:ListItem>
                                        <asp:ListItem Value="6">Semester 6</asp:ListItem>
                                        <asp:ListItem Value="All"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xl-8 px-0 col-sm-8 col-4">
                                <asp:LinkButton ID="Search" runat="server" class="btn btn-outline-primary float-right text-decoration-none" onclick="Search_Click" >Search<i class="ml-2 fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="card mb-4">
                            <div class="card-header form-inline">
                                <div class="col-xl-8 pl-0 col-sm-8 col-6">
                                    <i class="fa fa-graduation-cap mr-1"></i>
                                    Student Data
                                </div>
                                <div class="col-xl-4 pr-0 col-sm-4 col-6">
                                    <asp:LinkButton ID="Generate_Report" runat="server" class="btn btn-primary float-right" onclick="Generate_Report_Click">Generate Report<i class="fa fa-file-excel-o ml-2" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </div>                            
                            <div class="card-body">
                                <div class="table-responsive">
                                    <table class="table table-bordered table-hover" id="dataTable" width="100%" cellspacing="0">
                                        <thead class="table-secondary">
                                            <tr>
                                                <th>Enrollment No</th>
                                                <th>Name</th>
                                                <th>Field</th>
                                                <th>Semester</th>
                                                <th>Transactions</th>
                                                <th>Charges</th>
                                                <th>Last Transaction Time</th>
                                                <th>Last Book</th>
                                            </tr>
                                        </thead>
                                        <tfoot class="table-secondary">
                                            <tr>
                                                <th>Enrollment No</th>
                                                <th>Name</th>
                                                <th>Field</th>
                                                <th>Semester</th>
                                                <th>Transactions</th>
                                                <th>Charges</th>
                                                <th>Last Transaction Time</th>
                                                <th>Last Book</th>
                                            </tr>
                                        </tfoot>
                                        <tbody>
                                            <asp:PlaceHolder ID="TableData" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
</asp:Content>
