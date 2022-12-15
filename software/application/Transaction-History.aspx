<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Transaction-History.aspx.cs" Inherits="_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>History</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TopIconbar" Runat="Server">
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
                <a href="Transaction-History.aspx" class="text-light mr-2">
                        <abbr title="History"><i class="fa fa-history fa-lg"></i></abbr>
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
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
                    <div class="container-fluid">
                        <div class="form-inline mb-4 ">
                            <h1 class="mt-4 ">Transaction History</h1>
                        </div>
                        <div class="form-inline mb-4 pb-2 border-bottom border-secondary">
                            <div class="form-inline col-xl-10 pl-0 col-sm-10">
                                <div class="input-group input-group-joined col-xl-2 pl-0 col-sm-2">
                                    <asp:TextBox ID="Day" runat="server" class="form-control border-right-0" type="number" placeholder="Day" ></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">
                                            <i class="fa fa-calendar pr-1"></i>
                                        </span>
                                    </div>
                                </div>
                                <div class="input-group input-group-joined col-xl-2 pl-0 col-sm-2">
                                    <asp:TextBox ID="Month" runat="server" class="form-control border-right-0" type="number" placeholder="Month" ></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">
                                            <i class="fa fa-calendar pr-1"></i>
                                        </span>
                                    </div>
                                </div>
                                <div class="input-group input-group-joined col-xl-2 pl-0 col-sm-2">
                                    <asp:TextBox ID="Year" runat="server" class="form-control border-right-0" type="number" placeholder="Year" ></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">
                                            <i class="fa fa-calendar pr-1"></i>
                                        </span>
                                    </div>
                                </div>
                                <div class="input-group input-group-joined col-xl-3 pl-0 col-sm-3">
                                    <asp:TextBox ID="Student" runat="server" class="form-control border-right-0" type="text" placeholder="Student" ></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">
                                            <i class="fa fa-graduation-cap pr-1"></i>
                                        </span>
                                    </div>
                                </div>
                                <div class="input-group input-group-joined col-xl-3 pl-0 col-sm-3">
                                        <asp:TextBox ID="Book" runat="server" class="form-control border-right-0" type="text" placeholder="Book" ></asp:TextBox>
                                    <div class="input-group-append">
                                        <span class="input-group-text">
                                            <i class="fa fa-book pr-1"></i>
                                        </span>
                                    </div>
                                </div>

                            </div>
                            <div class="col-xl-2 pr-0 col-sm-2">
                                <asp:LinkButton ID="Search_Record" runat="server" class="btn btn-outline-primary float-right mt-1 text-decoration-none"  onclick="Search_Record_Click">Search<i class="ml-2 fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="card mb-4">
                            <div class="card-header form-inline">
                                <div class="col-xl-8 pl-0 col-sm-8 col-6">
                                    <i class="fa fa-history mr-1"></i>
                                    Transactions History
                                </div>
                                <div class="col-xl-4 pr-0 col-sm-4 col-6">
                                    <asp:LinkButton ID="Generate_Report" runat="server" class="btn btn-primary float-right" onclick="Generate_Report_Click">Generate Report<i class="fa fa-file-excel-o ml-2" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </div>                            
                            <div class="card-body">
                            <div class="container">
                                <div class="table-responsive row justify-content-center">
                                    <h4 class="border-bottom text-center pb-1 text-primary">Top 3 Transacted Books</h4>
                                    <table class="table table-bordered table-hover" id="Table1" width="100%" cellspacing="0">
                                        <thead class="table-secondary">
                                            <tr>
                                                <th>ISBN</th>
                                                <th>Book</th>
                                                <th>Field</th>
                                                <th>Semester</th>
                                                <th>Subject</th>
                                                <th>Total Transactions</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:PlaceHolder ID="TableTopData" runat="server"></asp:PlaceHolder>
                                        </tbody>
                                    </table>
                                    <p class="border-bottom text-center pb-1 text-primary"></p>
                                </div>
                                </div>
                                <div class="mt-4">
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
                                                <th>Charges</th>
                                                <th>Charges Remark</th>
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
                                                <th>Charges</th>
                                                <th>Charges Remark</th>
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
                    </div>
</asp:Content>
