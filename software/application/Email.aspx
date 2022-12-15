<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.master" AutoEventWireup="true" CodeFile="Email.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Email</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" Runat="Server">
    <li><a href="Books"><i class="ti-book"></i> <span>Books</span></a></li>
    <li><a href="Students"><i class="fa fa-graduation-cap"></i> <span>Students</span></a></li>
    <li><a href="IssueBook" onclick="red_issue()" data-toggle="popover"  data-title="Alert" data-content="Scan Student ID and Book ID." data-placement="bottom"><i class="fa fa-arrow-circle-left"></i><span>Issue Book</span></a></li>
    <li><a href="ReturnBook" onclick="red_return()" data-toggle="popover"  data-title="Alert" data-content="Scan Book ID." data-placement="bottom"><i class="fa fa-arrow-circle-right"></i><span>Return Book</span></a></li>
    <li><a href="Transactions"><i class="fa fa-history"></i><span>Transactions</span></a></li>
    <li><a href="IncomingBooks"><i class="fa fa-sign-in"></i><span>Incoming Books</span></a></li>
    <li class="active"><a href="Email"><i class="fa fa-envelope"></i> <span>Email</span></a></li>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
                    <div class="col-lg-6">
                        <div class="card  mt-5">
                            <div class="card-body"> 
                                <form id="Form1" class="needs-validation" novalidate="" runat="server">
                                            <div class="form-row">
                                                <div class="col-md-12 mb-3" style="text-align: left">
                                                        <h4 class="header-title">Email <i class="fa fa-envelope pl-1" style="color: #4336fb"></i></h4>
                                                </div>
                                            </div>
<!--                                            <div class="form-row">
                                                    <div class="col-md-6 mb-3">
                                                    <button class="btn btn-secondary dropdown-toggle col-md-4" type="button" data-toggle="dropdown">
                                                        <span class="pr--3">Send To</span> 
                                                    </button>
                                                    <div class="dropdown-menu">
                                                        <a class="dropdown-item" href="#">All</a>
                                                        <a class="dropdown-item" href="#">Personal</a>
                                                    </div>
                                                </div>
                                            </div>-->
                                            <div class="form-row">
                                                <div class="col-md-12 mb-3">
                                                    <label for="validationCustomUsername">Enroll.No.</label>
                                                    <asp:TextBox ID="Enroll" runat="server" type="number" class="form-control" 
                                                        placeholder="Enroll.No" required="" onkeydown="limit(this,11);" 
                                                        onkeyup="limit(this,11);" AutoPostBack="true" 
                                                        ontextchanged="Enroll_TextChanged" ></asp:TextBox>
                                                    <div class="invalid-feedback">
                                                        Please provide a vailed Enroll.No.
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row" runat="server" id="EmailAddress">
                                                <div class="col-md-12 mb-3">
                                                    <label for="validationCustomUsername">Email Address</label>
                                                    <asp:TextBox ID="Email" runat="server" type="email" class="form-control" placeholder="Email Address" required="" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="col-md-12 mb-3">
                                                    <label for="validationCustom03">Subject</label>
                                                    <asp:TextBox ID="Subject" runat="server" class="form-control" placeholder="Subject" required=""></asp:TextBox>
                                                    <div class="invalid-feedback">
                                                        Please provide a vailed Subject.
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="col-md-12 mb-5">
                                                    <label for="validationCustom04">Message</label>
                                                    <textarea class="form-control" id="Message" placeholder="Message" required="" rows="5" runat="server"></textarea>
                                                    <div class="invalid-feedback">
                                                        Please provide a valid Message.
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="form-row justify-content-center" style="text-align:center;">
                                                <div class="col-md-12">
                                                    <asp:Button ID="Send" class="btn btn-primary mt-1"   runat="server" onclick="Mail_Send" Text="Send" Font-Size="Medium" />
                                                </div>
                                            </div>
                                            </form>
                            </div>
                        </div>
                    </div>
</asp:Content>

