<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Return-Book.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Return Book</title>
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
                <a href="Return-Book.aspx" class="text-light mr-2">
                        <abbr title="Return Book"><i class="fa fa-arrow-circle-right fa-lg"></i></abbr>
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
                        <asp:Label ID="Book_RFID" runat="server" ></asp:Label>
                        <asp:Label ID="Student_Email" runat="server"></asp:Label>
                        <asp:Label ID="Admin_Id" runat="server"></asp:Label>
                    </div>
                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_COM" visible="false">
                            <div id="Div2" class="alert alert-danger" role="alert" runat="server">
                                COM Port is not Responding, <a href="settings.aspx#COM-Port-Setting" class="text-dark">Check it</a>!
                            </div>
                        </div>
                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_Timeout" visible="false">
                            <div id="Div3" class="alert alert-info" role="alert" runat="server">
                                Data Read Time-out, <a href="Issue-Book.aspx" class="text-dark">Try Again</a>!
                            </div>
                        </div>
                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_BookIssue" visible="false">
                            <div id="Div4" class="alert alert-danger" role="alert" runat="server">
                                Book is not Issued yet!
                            </div>
                        </div>
                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_Match" visible="false">
                            <div id="Div6" class="alert alert-info" role="alert" runat="server">
                                This ID doesn't match with any Book!
                            </div>
                        </div>
                        <div id="Div1" class="dropdown col-xl-12" runat="server" visible="false">
                            <asp:DropDownList ID="AvailablePorts" runat="server" class="form-control btn btn-secondary dropdown-toggle" data-toggle="dropdown" style="height:auto;min-width:95px;text-transform: capitalize;" role="button" aria-haspopup="true" aria-expanded="s" AutoPostBack="true" OnSelectedIndexChanged="COMPort_Change">
                            </asp:DropDownList>
                        </div>

                    <div class="container-fluid" runat="server" id="body">
                        <div class="form-inline mb-4">
                            <h1 class="mt-4 col-xl-12">Return Book</h1>
                        </div>
                            <div class="row text-capitalize">
                            <div class="col-xl-6">
                                <div class="card mb-4">
                                    <div class="card-header">
                                        <i class="fa fa-graduation-cap mr-1"></i>
                                        Student Details
                                    </div>
                                    <div class="card-body">
                                        <div class="form-inline text-center">
                                            <div class="col-xl-4">
                                                <asp:Image ID="Student_Image" runat="server" class="rounded" Height="150px"/>
                                            </div>
                                            <div class="col-xl-8">
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
                                         <div class="col-xl-12 mb-2">
                                                <h4 id="Book_Name" runat="server" class="pt-4"></h4>
                                                <div class="pt-2 pb-4">
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
                        </div>
                        <div class="row align-items-center text-capitalize">
                            <div class="col-xl-6">
                                <div class="card mb-3">
                                    <div class="card-header">
                                        <i class="fa fa-info mr-1"></i>
                                        Other Details
                                    </div>
                                    <div class="card-body text-center p-4">
                                         <div class="col-xl-12 mb-3">
                                                <div class="pt-2 pb-4">
                                                    <div class="form-inline">
                                                        <asp:Label ID="Label9" runat="server" Text="Issue From" class="col-md-3"></asp:Label>
                                                        <asp:Label ID="Label25" runat="server" Text="Issue Date" class="col-md-3"></asp:Label>
                                                        <asp:Label ID="Label10" runat="server" Text="Return To" class="col-md-3"></asp:Label>
                                                        <asp:Label ID="Label11" runat="server" Text="Return Time" class="col-md-3"></asp:Label>
                                                    </div>
                                                    <div class="form-inline text-secondary">
                                                        <asp:Label ID="Issue_From" runat="server"  class="col-md-3"></asp:Label>
                                                        <asp:Label ID="Issue_Date" runat="server"  class="col-md-3"></asp:Label>
                                                        <asp:Label ID="Return_To" runat="server"  class="col-md-3"></asp:Label>
                                                        <asp:Label ID="Return_Date" runat="server"  class="col-md-3"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                            <div class="col-xl-6">
                                <div class="card mb-3">
                                    <div class="card-header">
                                        <i class="fa fa-inr mr-1"></i>
                                        Charges Details
                                    </div>
                                    <div class="card-body text-center">
                                         <div class="col-xl-12 mb-2">
                                                <div class="pt-2 pb-4">
                                                    <div class="form-inline">
                                                        <asp:Label ID="Label4" runat="server" Text="Late Days" class="col-md-4"></asp:Label>
                                                        <asp:Label ID="Label7" runat="server" Text="Charges" class="col-md-4"></asp:Label>
                                                        <asp:Label ID="Label8" runat="server" Text="Charges Remark" class="col-md-4"></asp:Label>
                                                    </div>
                                                    <div class="form-inline text-secondary">
                                                        <asp:Label ID="Late_Days" runat="server"  class="col-md-4"></asp:Label>
                                                        <asp:Label ID="Charges" runat="server"  class="col-md-4"></asp:Label>
                                                        <asp:DropDownList ID="Charges_Remark" runat="server" class="form-control btn btn-secondary dropdown-toggle col-sm-4 col-xl-4 col-4" data-toggle="dropdown" style="height:auto;width:200px;text-transform: capitalize;" role="button" aria-haspopup="true" aria-expanded="s"  OnTextChanged="Charges_Remark_Change" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-12 text-center my-4">
                            <asp:LinkButton ID="ReturnBook" runat="server" class="btn btn-success" onclick="ReturnBook_Click">Return Book <i class="fa fa-arrow-circle-right pl-1" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                        <div class="col-xl-12 justify-content-center row" id="Alert_Success" runat="server" visible="false">
                            <div class="alert alert-success" role="alert" >
                                Book Returned Successfully, <a href="Transaction-History.aspx" class="text-secondary">Check it</a>.
                            </div>
                        </div>
                    </div>
                        
</asp:Content>


