<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Activity-Log.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Activity Log</title>
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
                <a href="Transaction-History.aspx" class="text-secondary">
                        <abbr title="History"><i class="fa fa-history"></i></abbr>
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
                            <h1 class="mt-4 col-xl-12">Admin Activity Log</h1>
                        </div>
                        <div class="row">
                            <div class="col-xl-12">
                                <div class="card mb-3">
                                    <div class="card-header form-inline">
                                        <div class="col-xl-8 pl-0">
                                            <i class="fa fa-list mr-1"></i>
                                            Admin Activity Details
                                        </div>
                                    </div>
                                    <div class="card-body text-center" runat="server">
                                         <div class="col-xl-12 mb-2">
                                                <div class="pt-2">
                                                    <div class="form-inline">
                                                        <asp:Label ID="Label4" runat="server" Text="Total Books Issued" class="col-xl-2 col-lg-2"></asp:Label>
                                                        <asp:Label ID="Label7" runat="server" Text="Total Books Returned" class="col-xl-2 col-lg-2"></asp:Label>
                                                        <asp:Label ID="Label2" runat="server" Text="Total Charges Taken" class="col-xl-2 col-lg-2"></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Text="Total Books Added" class="col-xl-2 col-lg-2"></asp:Label>
                                                        <asp:Label ID="Label3" runat="server" Text="Total Book Requests Added" class="col-xl-2 col-lg-2"></asp:Label>
                                                        <asp:Label ID="Label8" runat="server" Text="Total Book Suggestions Added" class="col-xl-2 col-lg-2"></asp:Label>
                                                    </div>
                                                    <div class="form-inline text-secondary mt-1">
                                                        <asp:Label ID="Total_Books_Issued" runat="server" class="col-xl-2 col-lg-2"></asp:Label> 
                                                        <asp:Label ID="Total_Books_Returned" runat="server" class="col-xl-2 col-lg-2"></asp:Label>
                                                        <asp:Label ID="Total_Charges_Taken" runat="server" class="col-xl-2 col-lg-2"></asp:Label>
                                                        <asp:Label ID="Total_Books_Added" runat="server" class="col-xl-2 col-lg-2"></asp:Label>
                                                        <asp:Label ID="Total_Book_Requests_Added" runat="server" class="col-xl-2 col-lg-2"></asp:Label>
                                                        <asp:Label ID="Total_Book_Suggestions_Added" runat="server" class="col-xl-2 col-lg-2"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>
                    </div>
</asp:Content>
