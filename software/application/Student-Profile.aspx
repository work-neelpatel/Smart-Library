<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Student-Profile.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Student Profile</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopIconbar" Runat="Server">
                <a href="Books.aspx" class="text-secondary mr-2">
                        <abbr title="Books"><i class="fa fa-book"></i></abbr>
                </a>
                <a href="Students.aspx" class="text-light mr-2">
                        <abbr title="Students"><i class="fa fa-graduation-cap fa-lg"></i></abbr>
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
                    <div style="display:none">
                        <asp:Label ID="Book_ISBN" runat="server" ></asp:Label>
                    </div>
                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_Found" visible="false">
                            <div id="Div1" class="alert alert-danger" role="alert" runat="server">
                                Student not Found!
                            </div>
                        </div>
                    <div class="container-fluid" runat="server" id="Body">
                        <div class="form-inline mb-4">
                            <h1 class="mt-4 col-xl-12">Student Profile</h1>
                        </div>
                        <div class="raw">
                                <div class="card mb-4">
                                    <div class="card-header">
                                        <i class="fa fa-info mr-1"></i>
                                        Student Details
                                    </div>
                                    <div class="card-body form-inline">
                                        <div class="col-xl-4 text-center border-right ">
                                            <asp:Image ID="Stu_Image" runat="server"  height="200px" class="rounded"/>
                                        </div>
                                        <div class="col-xl-8 text-center text-capitalize">
                                            <div class="mb-2 pb-2 border-bottom">
                                                <div class="form-inline">
                                                    <asp:Label ID="Label1" runat="server" Text="RFID" class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Label2" runat="server" Text="Enrollment No" class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Label3" runat="server" Text="First Name" class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Label4" runat="server" Text="Last Name" class="col-md-3"></asp:Label>
                                                </div>
                                                <div class="form-inline text-secondary">
                                                    <asp:Label ID="RFID" runat="server"  class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Enrollment_No" runat="server"  class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Fname" runat="server"  class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Lname" runat="server"  class="col-md-3"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="mb-2 pb-2 border-bottom">
                                                <div class="form-inline">
                                                    <asp:Label ID="Label9" runat="server" Text="Field" class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Label10" runat="server" Text="Semester" class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Label11" runat="server" Text="Mobile" class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Label12" runat="server" Text="Email" class="col-md-3"></asp:Label>
                                                </div>
                                                <div class="form-inline text-secondary">
                                                    <asp:Label ID="Field" runat="server"  class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Semester" runat="server"  class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Mobile" runat="server"  class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Email" runat="server"  class="col-md-3"></asp:Label>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="form-inline">
                                                    <asp:Label ID="Label17" runat="server" Text="Total Transactions" class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Label18" runat="server" Text="Total Charges Paid" class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Label19" runat="server" Text="Pending Book" class="col-md-3"></asp:Label>
                                                </div>
                                                <div class="form-inline text-secondary">
                                                    <asp:Label ID="Total_Transactions" runat="server"  class="col-md-3"></asp:Label>
                                                    <asp:Label ID="Charges_Paid" runat="server"  class="col-md-3"></asp:Label>
                                                    <asp:LinkButton ID="Pending_Book" runat="server" class="col-md-3 text-decoration-none" 
                                                        onclick="Pending_Book_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </div>
                        <div class="raw">
                                <div class="card mb-4">
                                    <div class="card-header form-inline">
                                        <div class="col-xl-8 pl-0 col-sm-8 col-6">
                                            <i class="fa fa-history mr-1"></i>
                                            Student Transactions
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
                                                        <th>Book</th>
                                                        <th>Issue From</th>
                                                        <th>Issue Date</th>
                                                        <th>Return To</th>
                                                        <th>Return Time</th>
                                                        <th>Charges</th>
                                                        <th>Charges Remark</th>
                                                    </tr>
                                                </thead>
                                                <tfoot class="table-secondary">
                                                    <tr>
                                                        <th>Book</th>
                                                        <th>Issue From</th>
                                                        <th>Issue Date</th>
                                                        <th>Return To</th>
                                                        <th>Return Time</th>
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

