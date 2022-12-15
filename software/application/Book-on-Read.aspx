<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Book-on-Read.aspx.cs" Inherits="_Default" %>

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
                <a href="Book-on-Read.aspx" class="text-light mr-2">
                        <abbr title="Books on Read"><i class="fa fa-building fa-lg"></i></abbr>
                </a>
                <a href="Book-Requests.aspx" class="text-secondary mr-2">
                        <abbr title="Book Requests"><i class="fa fa-paper-plane"></i></abbr>
                </a>
                <a href="Book-Suggestions.aspx" class="text-secondary">
                        <abbr title="Book Suggestions"><i class="fa fa-lightbulb-o fa-lg"></i></abbr>
                </a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
                    <div style="display:none">
                        <asp:Label ID="Book_RFID" runat="server" ></asp:Label>
                        <asp:Label ID="Student_RFID" runat="server"></asp:Label>
                        <asp:Label ID="Admin_Id" runat="server"></asp:Label>
                    </div>

                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_COM" visible="false">
                            <div id="Div1" class="alert alert-danger" role="alert" runat="server">
                                COM Port is not Responding, Please check it from <a href="settings.aspx#COM-Port-Setting" class="text-dark">here</a>!
                            </div>
                        </div>
                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_Timeout" visible="false">
                            <div id="Div2" class="alert alert-info" role="alert" runat="server">
                                Data Read Time-out, <a href="Book-on-Read.aspx" class="text-dark">Try Again</a>!
                            </div>
                        </div>
                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_BookReturn" visible="false">
                            <div id="Div3" class="alert alert-danger" role="alert" runat="server">
                                Book is already Issued, you can't again Issued Before it's Return!
                            </div>
                        </div>
                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_BookIssue" visible="false">
                            <div id="Div5" class="alert alert-danger" role="alert" runat="server">
                                Book is not Issued yet!
                            </div>
                        </div>
                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_Match" visible="false">
                            <div id="Div4" class="alert alert-info" role="alert" runat="server">
                                This ID doesn't match with any Book or Student!
                            </div>
                        </div>
                    <div class="container-fluid justify-content-center">
                        <div class="form-inline mb-4">
                            <h1 class="my-4 col-xl-8 pl-0 col-sm-8">Books on Read</h1>
                            <div class="col-xl-4 pr-0 text-right col-sm-4">
                                <a href="Books-on-Read-History.aspx" class="btn btn-dark">Transactions <i class="fa fa-history ml-2"></i></a>
                            </div>
                        </div>
                        <div runat="server" id="body" runat="server">
                            <h4 class="text-center">Here You have Two options</h4>
                            <h6 class="text-center text-secondary">For Book Transaction within Library</h6>
                            <div class="container border text-center p-4 col-12 col-lg-6 col-xl-4 border-secondary rounded">
                                    <div class="col-xl-12 mb-4">
                                        <asp:LinkButton ID="Issue_Book" runat="server" class="btn btn-primary" 
                                            onclick="Issue_Book_Click">Issue Book<i class="fa fa-arrow-circle-o-left ml-2" aria-hidden="true"></i></asp:LinkButton>
                                    </div>
                                    <div class="col-xl-12 col-sm-12 col-12">
                                        <asp:LinkButton ID="Return_Book" runat="server" class="btn btn-primary" 
                                            onclick="Return_Book_Click">Return Book<i class="fa fa-arrow-circle-o-left ml-2" aria-hidden="true"></i></asp:LinkButton>
                                    </div>                            
                            </div>
                        </div>
                        <div class="row text-capitalize" id="body2" runat="server" visible="false">
                            <h3 class="col-xl-12 text-center mb-4" id="Transaction_Type" runat="server"></h3>
                            <div class="col-xl-6">
                                <div class="card mb-4">
                                    <div class="card-header">
                                        <i class="fa fa-graduation-cap mr-1"></i>
                                        Student Details
                                    </div>
                                    <div class="card-body">
                                        <div class="form-inline text-center">
                                            <div class="col-xl-12">
                                                <h4 id="Student_Name" runat="server"></h4>
                                                <div class="pt-2">
                                                    <div class="form-inline">
                                                        <asp:Label ID="Label13" runat="server" Text="Enroll No" class="col-md-4"></asp:Label>
                                                        <asp:Label ID="Label14" runat="server" Text="Field" class="col-md-4"></asp:Label>
                                                        <asp:Label ID="Label15" runat="server" Text="Semester" class="col-md-4"></asp:Label>
                                                    </div>
                                                    <div class="form-inline text-secondary">
                                                        <asp:Label ID="Enrollment_No" runat="server"  class="col-md-4"></asp:Label>
                                                        <asp:Label ID="Student_Field" runat="server"  class="col-md-4"></asp:Label>
                                                        <asp:Label ID="Student_Semester" runat="server"  class="col-md-4"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                            </div>
                            <div class="col-xl-6">
                                <div class="card mb-4">
                                    <div class="card-header">
                                        <i class="fa fa-book mr-1"></i>
                                        Book Details
                                    </div>
                                    <div class="card-body text-center">
                                         <div class="col-xl-12">
                                                <h4 id="Book_Name" runat="server"></h4>
                                                <div class="pt-2">
                                                    <div class="form-inline">
                                                        <asp:Label ID="Label5" runat="server" Text="ISBN" class="col-md-3"></asp:Label>
                                                        <asp:Label ID="Label6" runat="server" Text="Field" class="col-md-3"></asp:Label>
                                                        <asp:Label ID="Label3" runat="server" Text="Semester" class="col-md-3"></asp:Label>
                                                        <asp:Label ID="Label16" runat="server" Text="Subject" class="col-md-3"></asp:Label>
                                                    </div>
                                                    <div class="form-inline text-secondary">
                                                        <asp:Label ID="ISBN" runat="server"  class="col-md-3"></asp:Label>
                                                        <asp:Label ID="Book_Field" runat="server"  class="col-md-3"></asp:Label>
                                                        <asp:Label ID="Book_Semester" runat="server"  class="col-md-3"></asp:Label>
                                                        <asp:Label ID="Subject" runat="server"  class="col-md-3"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        <div class="col-xl-12 text-center my-4">
                            <asp:LinkButton ID="IssueBook" runat="server" class="btn btn-success" 
                                Visible="false" onclick="IssueBook_Click" >Issue Book <i class="fa fa-arrow-circle-o-left pl-1" aria-hidden="true"></i></asp:LinkButton>
                            <asp:LinkButton ID="ReturnBook" runat="server" class="btn btn-success" 
                                Visible="false" onclick="ReturnBook_Click" >Return Book <i class="fa fa-arrow-circle-o-right pl-1" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                        <div class="col-xl-12 justify-content-center row" id="Alert_Success" runat="server" visible="false">
                            <div class="alert alert-success" role="alert" >
                                Transaction Successfull, <a href="Books-on-Read-History.aspx" class="text-secondary">Check it</a>.
                            </div>
                        </div>
                        </div>
                    </div>
</asp:Content>
