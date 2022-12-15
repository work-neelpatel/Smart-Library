<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Books.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Books</title>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        jQuery(function ($) {
            $("[id$=Author]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/GetAuthors") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                minLength: 1
            });

            $("[id$=Publisher]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/GetPublishers") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                minLength: 1
            });
        }); 
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="TopIconbar" Runat="Server">
                <a href="Books.aspx" class="text-light mr-2">
                        <abbr title="Books"><i class="fa fa-book fa-lg"></i></abbr>
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
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
                    <div class="container-fluid">
                        <div class="form-inline">
                            <h1 class="my-4 col-xl-6 pl-0 col-sm-6 col-6">Books</h1>
                            <div class="col-xl-6 pr-0 col-sm-6 col-6">
                                <a class="btn btn-dark float-right mt-1" href="Add-Book.aspx">Add Book<i class="fa fa-plus-square ml-2" aria-hidden="true"></i></a>
                            </div>
                        </div>
                        <div class="form-inline mb-2 border-bottom pb-2">
                            <div class="form-inline pl-0 col-xl-8 col-sm-6 col-12">
                                <div class="dropdown pl-0 col-xl-2 col-sm-4 col-12 px-0">
                                    <asp:DropDownList ID="Field" runat="server" class="form-control btn btn-secondary dropdown-toggle" data-toggle="dropdown" style="height:auto;text-transform: capitalize;" role="button" aria-haspopup="true" aria-expanded="s" OnTextChanged="Field_Change" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="dropdown col-xl-4 col-sm-6 col-12 px-0">
                                    <asp:DropDownList ID="Semester" runat="server" class="form-control btn btn-secondary dropdown-toggle" data-toggle="dropdown" style="height:auto;text-transform: capitalize;" role="button" aria-haspopup="true" aria-expanded="s" OnTextChanged="Semester_Change" AutoPostBack="true">
                                        <asp:ListItem Value="1">Semester 1</asp:ListItem>
                                        <asp:ListItem Value="2">Semester 2</asp:ListItem>
                                        <asp:ListItem Value="3">Semester 3</asp:ListItem>
                                        <asp:ListItem Value="4">Semester 4</asp:ListItem>
                                        <asp:ListItem Value="5">Semester 5</asp:ListItem>
                                        <asp:ListItem Value="6">Semester 6</asp:ListItem>
                                        <asp:ListItem Value="All">All</asp:ListItem>
                                        <asp:ListItem Value="None">None</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-inline mb-4 pb-2 border-bottom border-secondary">
                            <div class="col-xl-10 form-inline pl-0 col-sm-10 col-12">
                                <div class="dropdown col-sm-3  pl-0 col-12">
                                    <asp:DropDownList ID="Subject" runat="server" class="form-control btn btn-secondary dropdown-toggle col-sm-12 col-xl-12 col-12" data-toggle="dropdown" style="height:auto;width:200px;text-transform: capitalize;" role="button" aria-haspopup="true" aria-expanded="s">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-3 col-12 pl-0">
                                    <asp:TextBox ID="Author" runat="server" class="form-control col-sm-12 col-xl-12 col-12" type="text" placeholder="Author"></asp:TextBox>
                                </div>
                                <div class="col-sm-3 col-12 pl-0">
                                    <asp:TextBox ID="Publisher" runat="server" class="form-control col-sm-12 col-xl-12 col-12" type="text" placeholder="Publisher"></asp:TextBox>
                                </div>
                                <div class="dropdown col-sm-3 col-12 pl-0">
                                    <asp:DropDownList ID="BookState" runat="server" class="form-control btn btn-secondary dropdown-toggle col-sm-12 col-xl-12 col-12" data-toggle="dropdown" style="height:auto;text-transform: capitalize;" role="button" aria-haspopup="true" aria-expanded="s">
                                        <asp:ListItem Value="Available">Available Only</asp:ListItem>
                                        <asp:ListItem Value="Not Available">Not Available Only</asp:ListItem>
                                        <asp:ListItem Value="All">All</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xl-2 px-0 col-sm-2 col-12 mt-1">
                                <asp:LinkButton ID="Search_Book" runat="server" class="btn btn-outline-primary float-right text-decoration-none" 
                                    onclick="Search_Book_Click">Search<i class="ml-2 fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="card mb-4"  runat="server" id="Table1">
                            <div class="card-header form-inline">
                                <div class="col-xl-8 pl-0 col-sm-8 col-6">
                                    <i class="fa fa-book mr-1"></i>
                                    Book Data
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
                                                <th>ISBN</th>
                                                <th>Name</th>
                                                <th>Field</th>
                                                <th>Semester</th>
                                                <th>Subject</th>
                                                <th>Edition</th>
                                                <th>Rack No</th>
                                                <th>Remain Copies</th>
                                                <th>Transactions</th>
                                            </tr>
                                        </thead>
                                        <tfoot class="table-secondary">
                                            <tr>
                                                <th>ISBN</th>
                                                <th>Name</th>
                                                <th>Field</th>
                                                <th>Semester</th>
                                                <th>Subject</th>
                                                <th>Edition</th>
                                                <th>Rack No</th>
                                                <th>Remain Copies</th>
                                                <th>Transactions</th>
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

