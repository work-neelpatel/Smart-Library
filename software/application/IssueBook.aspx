<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.master" AutoEventWireup="true" CodeFile="IssueBook.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Issue Book</title>
    <script>
    function ShowPopup(title, body) {
        $("#MyPopup").modal("show");
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" Runat="Server">
    <li><a href="Books"><i class="ti-book"></i> <span>Books</span></a></li>
    <li><a href="Students"><i class="fa fa-graduation-cap"></i> <span>Students</span></a></li>
    <li class="active"><a href="IssueBook" onclick="red_issue()" data-toggle="popover"  data-title="Alert" data-content="Scan Student ID and Book ID." data-placement="bottom"><i class="fa fa-arrow-circle-left"></i><span>Issue Book</span></a></li>
    <li><a href="ReturnBook" onclick="red_return()" data-toggle="popover"  data-title="Alert" data-content="Scan Book ID." data-placement="bottom"><i class="fa fa-arrow-circle-right"></i><span>Return Book</span></a></li>
    <li><a href="Transactions"><i class="fa fa-history"></i><span>Transactions</span></a></li>
    <li><a href="IncomingBooks"><i class="fa fa-sign-in"></i><span>Incoming Books</span></a></li>
    <li><a href="Email"><i class="fa fa-envelope"></i> <span>Email</span></a></li>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
                    <div class="col-12">
                        <div class="card  mt-5">
                            <div class="card-body"> 
                            <form id="Form1" class="needs-validation text-capitalize" novalidate="" runat="server">
                                           <div class="form-row">
                                                <div class="col-md-4 mb-3" style="text-align: left">
                                                        <h4 class="header-title">Issue Book <i class="fa fa-arrow-circle-left pl-1" style="color: #4336fb"></i></h4>
                                                </div>
                                                <div class="col-md-8 mb-3" style="text-align:right;">
                                                    <button type="button" class="btn btn-info mt-1" data-toggle="modal" data-target="#Help">Help <i class="fa fa-question-circle pl-1"></i></button>
                                                </div>
                                            </div>

                                    <div class="p-3 mb-3" style="border-top:1px solid #4336fb">
                                        <h6 class="py-3" style="text-align:center">Book Details <i class="ti-book pl-1" style="color: #4336fb"></i></h6>
                                            <div class="form-row">
                                               <div class="col-md-3 mb-3">
                                                    <label for="validationCustom01">ISBN</label>
                                                    <asp:Label ID="ISBN_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                                <div class="col-md-3 mb-3">
                                                    <label for="validationCustom02">Name</label>
                                                    <asp:Label ID="Bname_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                                <div class="col-md-3 mb-3">
                                                    <label for="validationCustom02">Subject</label>
                                                    <asp:Label ID="Subject_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                                <div class="col-md-3 mb-3">
                                                    <label for="validationCustom02">Semester</label>
                                                    <asp:Label ID="Bsem_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                                <div class="col-md-3 mb-3" style="display:none">
                                                    <label for="validationCustom01">RFID</label>
                                                    <asp:Label ID="BRFID_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                        </div>
                                    </div>

                                    <div class="p-3" style="border-top:1px solid #4336fb">
                                        <h6 class="py-3" style="text-align:center">Student Details <i class="fa fa-graduation-cap pl-1" style="color: #4336fb"></i></h6>
                                            <div class="form-row">
                                               <div class="col-md-3 mb-3">
                                                    <label for="validationCustom01">Enroll. No.</label>
                                                    <asp:Label ID="Enroll_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                                 <div class="col-md-3 mb-3">
                                                    <label for="validationCustom01">Name</label>
                                                    <asp:Label ID="Sname_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                                <div class="col-md-3 mb-3">
                                                    <label for="validationCustom02">Semester</label>
                                                    <asp:Label ID="Ssem_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                                <div class="col-md-3 mb-3">
                                                    <label for="validationCustom02">Email</label>
                                                    <asp:Label ID="Email_lbl" runat="server" class="pl-3 text-lowercase"></asp:Label>
                                                </div>
                                                <div class="col-md-3 mb-3" style="display:none">
                                                    <label for="validationCustom02">RFID</label>
                                                    <asp:Label ID="SRFID_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                        </div>

                                        <div class="form-row mb-3">
                                                <div class="col-md-12 mb-3 text-center" style="">
                                                        <asp:Image ID="Stu_Image" runat="server" class="col-md-12 p-3" style="width:200px;border:1px solid black"/>
                                                </div>
                                        </div>

                                        <div class="p-3 mb-5" style="border-top:1px solid #4336fb;border-bottom:1px solid #4336fb;">
                                            <h6 class="py-3" style="text-align:center">Other Details <i class="fa fa fa-info-circle pl-1" style="color: #4336fb"></i></h6>
                                                <div class="form-row">
                                                    <div class="col-md-3 mb-3">
                                                        <label for="validationCustom02">Issue From</label>
                                                        <asp:Label ID="IFrom_lbl" runat="server" class="pl-3"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3 mb-3">
                                                        <label for="validationCustom01">Issue Time</label>
                                                        <asp:Label ID="ITime_lbl" runat="server" class="pl-3"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3 mb-3">
                                                        <label for="validationCustom01">Est. Return Date</label>
                                                        <asp:Label ID="EReturnDate_lbl" runat="server" class="pl-3"></asp:Label>
                                                    </div>
                                            </div>
                                        </div>

                                    <div class="justify-content-center" style="text-align:center;">
                                            <button type="button" class="btn btn-Primary mt-1" runat="server" onserverclick="IssueBook" style="font-size:medium">Issue Book <i class="fa fa-arrow-circle-left pl-1"></i></button>
                                    </div>

                                <div class="modal fade bd-example-modal-sm" id="Help">
                                     <div class="modal-dialog " role="document">
                                       <div class="modal-content">
                                            <div class="modal-header alert-info">
                                                <h5 class="modal-title">Help <i class="fa fa-question-circle text-info"></i></h5>
                                                <button type="button" class="close" data-dismiss="modal"><span>&times;</span></button>
                                            </div>
                                            <div class="modal-body">
                                                <p>For Issue Book Scan Student and Book ID during page load.</p>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                                <button type="button" class="btn btn-info" onclick="window.location ='IssueBook'">Try again</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                </form>
                            </div>
                        </div>
                    </div>
</asp:Content>

