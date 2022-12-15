<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Book-Profile.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Book Profile</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="TopIconbar" Runat="Server">
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
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_BookFound" visible="false">
                            <div id="Div2" class="alert alert-danger" role="alert" runat="server">
                                Book not Found!
                            </div>
                        </div>
                    <div class="container-fluid text-capitalize" id="Body" runat="server">
                        <div class="form-inline mb-4">
                            <h1 class="mt-4 col-xl-12">Book Profile</h1>
                        </div>
                        <div class="raw ">
                                <div class="card mb-4">
                                    <div class="card-header">
                                        <i class="fa fa-info mr-1"></i>
                                        Book Details
                                    </div>
                                    <div class="card-body">
                                        <div class="col-xl-12 text-center">
                                            <div class="mb-3 pb-3 border-bottom">
                                                <div class="form-inline">
                                                    <asp:Label ID="Label2" runat="server" Text="ISBN" class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Label3" runat="server" Text="Name" class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Label4" runat="server" Text="Field" class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Label17" runat="server" Text="Semester" class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Label18" runat="server" Text="Subject" class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Label9" runat="server" Text="Edition" class="col-md-2"></asp:Label>
                                                </div>
                                                <div class="form-inline text-secondary" runat="server" id="Divlbl1">
                                                    <asp:Label ID="ISBN" runat="server"  class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Name" runat="server"  class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Field" runat="server"  class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Semester" runat="server"  class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Subject" runat="server"  class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Edition" runat="server"  class="col-md-2"></asp:Label>
                                                </div>
                                                <div class="form-inline text-secondary" runat="server" id="Divtxt1" visible="false">
                                                    <div class="col-md-2"><asp:TextBox ID="ISBN_txt" runat="server" placeholder="ISBN"></asp:TextBox></div>
                                                    <div class="col-md-2"><asp:TextBox ID="Name_txt" runat="server" placeholder="Name"></asp:TextBox></div>
                                                    <div class="col-md-2"><asp:TextBox ID="Field_txt" runat="server" placeholder="Field"></asp:TextBox></div>
                                                    <div class="col-md-2"><asp:TextBox ID="Semester_txt" runat="server" placeholder="Semester"></asp:TextBox></div>
                                                    <div class="col-md-2"><asp:TextBox ID="Subject_txt" runat="server" placeholder="Subject" ReadOnly="true"></asp:TextBox></div>
                                                    <div class="col-md-2"><asp:TextBox ID="Edition_txt" runat="server" placeholder="Edition"></asp:TextBox></div>
                                                </div>
                                            </div>
                                            <div class="mb-3">
                                                <div class="form-inline">
                                                    <asp:Label ID="Label10" runat="server" Text="Author 1" class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Label11" runat="server" Text="Author 2" class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Label12" runat="server" Text="Author 3" class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Label21" runat="server" Text="Publisher" class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Label7" runat="server" Text="Total Copies" class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Label6" runat="server" Text="Issued Copies" class="col-md-2"></asp:Label>
                                                </div>
                                                <div class="form-inline text-secondary" runat="server" id="Divlbl2">
                                                    <asp:Label ID="Author1" runat="server"  class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Author2" runat="server"  class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Author3" runat="server"  class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Publisher" runat="server"  class="col-md-2"></asp:Label>
                                                    <asp:Label ID="Copies" runat="server"  class="col-md-2 text-primary"></asp:Label>
                                                    <asp:Label ID="Issued_Copies" runat="server"  class="col-md-2 text-danger"></asp:Label>
                                                </div>
                                                <div class="form-inline text-secondary" runat="server" id="Divtxt2" visible="false">
                                                    <div class="col-md-2"><asp:TextBox ID="Author1_txt" runat="server" placeholder="Author 1"></asp:TextBox></div>
                                                    <div class="col-md-2"><asp:TextBox ID="Author2_txt" runat="server" placeholder="Author 2"></asp:TextBox></div>
                                                    <div class="col-md-2"><asp:TextBox ID="Author3_txt" runat="server" placeholder="Author 3"></asp:TextBox></div>
                                                    <div class="col-md-2"><asp:TextBox ID="Publisher_txt" runat="server" placeholder="Publisher"></asp:TextBox></div>
                                                    <div class="col-md-2"><asp:TextBox ID="Copies_txt" runat="server" ReadOnly="true"></asp:TextBox></div>
                                                    <div class="col-md-2"><asp:TextBox ID="Issued_Copies_txt" runat="server" ReadOnly="true"></asp:TextBox></div>
                                                </div>
                                            </div>
                                            <div class="mb-2 pt-2">
                                                <div class="form-inline" runat="server" id="Div_btn">
                                                    <div class="col-12 text-center">
                                                        <asp:LinkButton ID="Update_Details_btn" runat="server" class="btn btn-outline-success" onclick="Update_Details_btn_Click" >Update Details<i class="fa fa-pencil-square-o ml-2" aria-hidden="true"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-center">
                                                    <asp:LinkButton Visible="false" ID="Done1_btn" runat="server" class="btn btn-success" onclick="Done1_btn_Click" >Update Details<i class="fa fa-check-square-o ml-2" aria-hidden="true"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </div>
                        <div class="raw">
                                <div class="card mb-4">
                                    <div class="card-header form-inline">
                                        <div class="col-xl-8 pl-0 col-sm-6 col-6">
                                            <i class="fa fa-files-o mr-1"></i>
                                            Book Copies
                                        </div>
                                        <div class="col-xl-4 pr-0 col-sm-6 col-6 form-inline text-center">
                                            <div class="col-6 border-right">Charges: <asp:Label ID="Charges" runat="server" class="text-secondary"></asp:Label> INR</div>
                                            <div class="col-6 pr-0">Transactions: <asp:Label ID="Transactions" runat="server" class="text-secondary"></asp:Label> Times</div>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="table-responsive">
                                            <table class="table table-bordered table-hover" id="Table1" width="100%" cellspacing="0">
                                                <thead class="table-secondary">
                                                    <tr>
                                                        <th>RFID</th>
                                                        <th>Rack No</th>
                                                        <th>Add By</th>
                                                        <th>Add Time</th>
                                                        <th>Transactions</th>
                                                        <th>Charges</th>
                                                        <th>State</th>
                                                    </tr>
                                                </thead>
                                                <tfoot class="table-secondary">
                                                    <tr>
                                                        <th>RFID</th>
                                                        <th>Rack No</th>
                                                        <th>Add By</th>
                                                        <th>Add Time</th>
                                                        <th>Transactions</th>
                                                        <th>Charges</th>
                                                        <th>State</th>
                                                    </tr>
                                                </tfoot>
                                                <tbody>
                                                    <asp:PlaceHolder ID="BookCopies_Data" runat="server"></asp:PlaceHolder>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                    </div>
                        <div class="raw" id="BookTransactions_Div" runat="server" visible="false">
                                <div class="card mb-4" id="BookTransactions">
                                    <div class="card-header form-inline">
                                        <div class="col-xl-8 pl-0 col-sm-8 col-6">
                                            <i class="fa fa-history mr-1"></i>
                                            Book Transactions
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
                                                        <th>Student</th>
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
                                                        <th>Student</th>
                                                        <th>Issue From</th>
                                                        <th>Issue Date</th>
                                                        <th>Return To</th>
                                                        <th>Return Time</th>
                                                        <th>Charges</th>
                                                        <th>Charges Remark</th>
                                                    </tr>
                                                </tfoot>
                                                <tbody>
                                                    <asp:PlaceHolder ID="BookTransaction_Data" runat="server"></asp:PlaceHolder>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                    </div>
                </div>
                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_CopyFound" visible="false">
                            <div id="Div3" class="alert alert-secondary" role="alert" runat="server">
                                No Transactions!
                            </div>
                        </div>
</asp:Content>