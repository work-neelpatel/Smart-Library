<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Dashboard</title>
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
                        <h1 class="my-4">Dashboard</h1>

                        <div class="row">
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-primary text-white mb-4">
                                    <div class="card-body">
                                    <div class="form-inline">
                                        <div class="form-inline col-xl-8 p-0 col-sm-8 col-8">
                                        <span class="font-weight-bold h5">Books in Stock</span>
                                        <span class="row col-xl-12  h5 text-center" runat="server" id="Total_Books"></span>
                                        </div>
                                        <i class="fa fa-book fa-3x col-xl-4 text-right col-sm-4 col-4"></i>
                                    </div>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="Books.aspx">View Details</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-primary text-white mb-4">
                                    <div class="card-body">
                                    <div class="form-inline">
                                        <div class="form-inline col-xl-8 p-0 col-sm-8 col-8">
                                        <span class="font-weight-bold h5">Books on Read</span>
                                        <span class="row col-xl-12  h5 text-center" runat="server" id="Books_on_Read"></span>
                                        </div>
                                        <i class="fa fa-building fa-3x col-xl-4 text-right col-sm-4 col-4"></i>
                                    </div>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="Books-on-Read-History.aspx">View Details</a>
                                        <div class="small text-white"><i class="fa fa-angle-right col-sm-4 col-4"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-primary text-white mb-4">
                                    <div class="card-body">
                                    <div class="form-inline">
                                        <div class="form-inline col-xl-8 p-0 col-sm-8 col-8">
                                        <span class="font-weight-bold h5">Transactions</span>
                                        <span class="row col-xl-12 h5" runat="server" id="Total_Transactions"></span>
                                        </div>
                                        <i class="fa fa-history fa-3x col-xl-4 text-right col-sm-4 col-4"></i>
                                    </div>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="Transaction-History.aspx">View Details</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-primary text-white mb-4">
                                    <div class="card-body">
                                    <div class="form-inline">
                                        <div class="form-inline col-xl-8 p-0 col-sm-8 col-8">
                                        <span class="font-weight-bold h5">Bought Books</span>
                                        <span class="row col-xl-12  h5 text-center" runat="server" id="Books_buy_tYear"></span>
                                        </div>
                                        <i class="fa fa-inr fa-3x col-xl-4 text-right col-sm-4 col-4"></i>
                                    </div>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="Book-buy-History.aspx">View Details</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl-6 col-md-6">
                                <div class="card bg-warning text-white mb-4">
                                    <div class="card-body">
                                    <div class="form-inline">
                                        <div class="form-inline col-xl-8 p-0 col-sm-8 col-8">
                                        <span class="font-weight-bold h5">Book Requests</span>
                                        <span class="row col-xl-12 h5 " runat="server" id="Book_Requests"></span>
                                        </div>
                                        <i class="fa fa-paper-plane fa-3x col-xl-4 text-right col-sm-4 col-4"></i>
                                    </div>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="Book-Requests.aspx">View Details</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-6 col-md-6">
                                <div class="card bg-success text-white mb-4">
                                    <div class="card-body">
                                    <div class="form-inline">
                                        <div class="form-inline col-xl-8 p-0 col-sm-8 col-8">
                                        <span class="font-weight-bold h5">Book Suggestions</span>
                                        <span class="row col-xl-12  h5 text-center" runat="server" id="Book_Suggestion"></span>
                                        </div>
                                        <i class="fa fa-lightbulb-o fa-4x col-xl-4 text-right col-sm-4 col-4"></i>
                                    </div>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="Book-Suggestions.aspx">View Details</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                    </div>
                        <div class="row">
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-secondary text-white mb-4">
                                    <div class="card-body">
                                    <div class="form-inline">
                                        <div class="form-inline col-xl-8 p-0 col-sm-8 col-8">
                                        <span class="font-weight-bold h5">Fields</span>
                                        </div>
                                        <span class="text-right col-sm-4 col-4 h5" runat="server" id="Fields"></span>
                                    </div>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="Fields.aspx">View Details</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-secondary text-white mb-4">
                                    <div class="card-body">
                                    <div class="form-inline">
                                        <div class="form-inline col-xl-8 p-0 col-sm-8 col-8">
                                        <span class="font-weight-bold h5">Subjects</span>
                                        </div>
                                        <span class="text-right col-sm-4 col-4 h5" runat="server" id="Subjects"></span>
                                    </div>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="Subjects.aspx">View Details</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-secondary text-white mb-4">
                                    <div class="card-body">
                                    <div class="form-inline">
                                        <div class="form-inline col-xl-8 p-0 col-sm-8 col-8">
                                        <span class="font-weight-bold h5">Authors</span>
                                        </div>
                                        <span class="text-right col-sm-4 col-4 h5" runat="server" id="Authors"></span>
                                    </div>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="Authors.aspx">View Details</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-secondary text-white mb-4">
                                    <div class="card-body">
                                    <div class="form-inline">
                                        <div class="form-inline col-xl-8 p-0 col-sm-8 col-8">
                                        <span class="font-weight-bold h5">Publishers</span>
                                        </div>
                                        <span class="text-right col-sm-4 col-4 h5" runat="server" id="Publishers"></span>
                                    </div>
                                    </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="Publishers.aspx">View Details</a>
                                        <div class="small text-white"><i class="fa fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl-6">
                                <div class="card mb-4">
                                    <div class="card-header form-inline">
                                            <i class="fa fa-bar-chart mr-2"></i>
                                            Book Transactions
                                    </div>
                                    <div class="card-body"><canvas id="myBarChart" width="100%" height="40"></canvas></div>
                                </div>
                            </div>
                            <div class="col-xl-6">
                                <div class="card mb-4">
                                    <div class="card-header py-3">
                                        <i class="fa fa-area-chart mr-1"></i>
                                        Bought Books
                                    </div>
                                    <div class="card-body pb-4"><canvas id="myAreaChart" width="100%" height="40"></canvas></div>
                                </div>
                            </div>
                        </div>
                        <div class="row" runat="server" id="div_BTM">
                            <div class="col-xl-12">
                                <div class="card mb-4">
                                    <div class="card-header form-inline">
                                        <div class="col-xl-8 pl-0 col-sm-8 col-10">
                                            <i class="fa fa-bar-chart mr-1"></i>
                                            Book Transactions 2020
                                        </div>
                                        <div class="col-xl-4 pr-0 col-sm-4 col-2">
                                            <asp:LinkButton ID="Remove1" runat="server" class="float-right text-danger text-decoration-none" onclick="Remove1_Click">Remove</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="card-body"><canvas id="myBarChart" width="100%" height="40"></canvas></div>
                                </div>
                            </div>
                        </div>
                        <div class="row" runat="server" id="div_BTW">
                            <div class="col-xl-12">
                                <div class="card mb-4">
                                    <div class="card-header form-inline">
                                        <div class="col-xl-8 pl-0 col-sm-8 col-10">
                                            <i class="fa fa-bar-chart mr-1"></i>
                                            Book Transactions May 2020
                                        </div>
                                        <div class="col-xl-4 pr-0 col-sm-4 col-2">
                                            <asp:LinkButton ID="Remove2" runat="server" class="float-right text-danger text-decoration-none" onclick="Remove2_Click">Remove</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="card-body"><canvas id="myBarChart" width="100%" height="40"></canvas></div>
                                </div>
                            </div>
                        </div>
</asp:Content>

