<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Books-on-Read-History.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Books on Read</title>
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
                            <h1 class="my-4 col-xl-8 pl-0 col-sm-8">Books on Read Transactions</h1>
                            <div class="col-xl-4 pr-0 text-right col-sm-4">
                                <asp:LinkButton ID="Records_Live" runat="server" 
                                    class="text-dark mr-2 border-bottom border-secondary text-decoration-none" 
                                    onclick="Records_Live_Click">Live</asp:LinkButton>
                                <asp:LinkButton ID="Records_History" runat="server" 
                                    class="text-dark mr-2 text-decoration-none border-secondary" onclick="Records_History_Click">History</asp:LinkButton>
                            </div>
                        </div>
                        <div class="card mb-4">
                            <div class="card-header form-inline">
                                <div class="col-xl-8 pl-0 col-sm-8 col-6" >
                                    <i class="fa fa-building mr-1"></i>
                                    Books on Read Transactions (<b id="Table_Heading" runat="server">Live</b>)
                                </div>
                                <div class="col-xl-4 pr-0 col-sm-4 col-6">
                                    <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-primary float-right" onclick="Generate_Report_Click">Generate Report<i class="fa fa-file-excel-o ml-2" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </div>                            
                            <div class="card-body">
                                <div class="table-responsive">
                                    <table class="table table-bordered table-hover" id="dataTable" width="100%" cellspacing="0">
                                        <thead class="table-secondary">
                                            <tr>
                                                <th>Book</th>
                                                <th>Student</th>
                                                <th>Issue From</th>
                                                <th>Issue Date</th>
                                                <th>Return To</th>
                                                <th>Return Date</th>
                                            </tr>
                                        </thead>
                                        <tfoot class="table-secondary">
                                            <tr>
                                                <th>Book</th>
                                                <th>Student</th>
                                                <th>Issue From</th>
                                                <th>Issue Date</th>
                                                <th>Return To</th>
                                                <th>Return Date</th>
                                            </tr>
                                        </tfoot>
                                        <tbody>
                                            <asp:PlaceHolder ID="TableData" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
</asp:Content>
