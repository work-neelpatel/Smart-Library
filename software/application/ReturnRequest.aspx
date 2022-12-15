<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.master" AutoEventWireup="true" CodeFile="ReturnRequest.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Request to Return</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" Runat="Server">
    <li><a href="Books"><i class="ti-book"></i> <span>Books</span></a></li>
    <li><a href="Students"><i class="fa fa-graduation-cap"></i> <span>Students</span></a></li>
    <li><a href="IssueBook" onclick="red_issue()" data-toggle="popover"  data-title="Alert" data-content="Scan Student ID and Book ID." data-placement="bottom"><i class="fa fa-arrow-circle-left"></i><span>Issue Book</span></a></li>
    <li><a href="ReturnBook" onclick="red_return()" data-toggle="popover"  data-title="Alert" data-content="Scan Book ID." data-placement="bottom"><i class="fa fa-arrow-circle-right"></i><span>Return Book</span></a></li>
    <li><a href="Transactions"><i class="fa fa-history"></i><span>Transactions</span></a></li>
    <li><a href="IncomingBooks"><i class="fa fa-sign-in"></i><span>Incoming Books</span></a></li>
        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
                    <div class="col-6">
                        <div class="card  mt-5">
                            <div class="card-body"> 
                            <form id="Form1" class="needs-validation text-capitalize" novalidate="" runat="server">
                                           <div class="form-row">
                                                <div class="col-md-12 mb-3" style="text-align: left">
                                                        <h4 class="header-title">Request for Return Book<i class="fa fa-share-square-o pl-1" style="color: #4336fb"></i></h4>
                                                </div>
                                            </div>

                                    <div class="p-3 mb-3" style="border-top:1px solid #4336fb">
                                        <h6 class="py-3" style="text-align:center">Book Details <i class="ti-book pl-1" style="color: #4336fb"></i></h6>
                                            <div class="form-row">
                                                <div class="col-md-6 mb-3">
                                                    <label for="validationCustom01">RFID</label>
                                                    <asp:Label ID="BRFID_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                                <div class="col-md-6 mb-3">
                                                    <label for="validationCustom02">ISBN</label>
                                                    <asp:Label ID="ISBN_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="col-md-6 mb-3">
                                                    <label for="validationCustom02">Name</label>
                                                    <asp:Label ID="Bname_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                                <div class="col-md-6 mb-3">
                                                    <label for="validationCustom02">Total Issued</label>
                                                    <asp:Label ID="Tissue_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="col-md-6 mb-3">
                                                    <label for="validationCustom02">Issue Time</label>
                                                    <asp:Label ID="IssueTimes_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                                <div class="col-md-6 mb-3">
                                                    <label for="validationCustom02">Days to Return</label>
                                                    <asp:Label ID="DaystR_lbl" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                    </div>

                                    <div class="p-3 mb-5" style="border-top:1px solid #4336fb;border-bottom:1px solid #4336fb;">
                                        <h6 class="py-3" style="text-align:center">Student Details <i class="fa fa-graduation-cap pl-1" style="color: #4336fb"></i></h6>
                                            <div class="form-row">
                                                <div class="col-md-6 mb-3">
                                                    <label for="validationCustom02">RFID</label>
                                                    <asp:Label ID="SRFID_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                                <div class="col-md-6 mb-3">
                                                    <label for="validationCustom02">Enroll.No</label>
                                                    <asp:Label ID="Enroll_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="col-md-6 mb-3">
                                                    <label for="validationCustom01">Name</label>
                                                    <asp:Label ID="Sname_lbl" runat="server" class="pl-3"></asp:Label>
                                                </div>
                                                <div class="col-md-6 mb-3">
                                                    <label for="validationCustom02">Email</label>
                                                    <asp:Label ID="Email_lbl" runat="server" class="pl-3 text-lowercase"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="col-md-6 mb-3">
                                                    <label for="validationCustom02">Issued Books</label>
                                                    <asp:Label ID="Ibooks_lbl" runat="server" class="pl-3 "></asp:Label>
                                                </div>
                                                <div class="col-md-6 mb-3">
                                                    <label for="validationCustom02">Total Charges</label>
                                                    <asp:Label ID="Tcharges_lbl" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                    </div>

                                    <div class="justify-content-center" style="text-align:center;">
                                            <p><h4 id="Req_Send_text" runat="server">Request already send </h4></p>
                                            <button id="SendRequest_btn" type="button" class="btn btn-Primary mt-1" runat="server" onserverclick="SendRequest" style="font-size:medium">Send Request <i class="fa fa-paper-plane pl-1"></i></button>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
</asp:Content>

