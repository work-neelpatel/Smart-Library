<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.master" AutoEventWireup="true" CodeFile="StudentInfo.aspx.cs" Inherits="Default2" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Student Profile</title>
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
                    <div class="col-12">
                        <div class="card  mt-5">
                            <div class="card-body"> 
                                <form id="Form1" class="needs-validation" novalidate="" runat="server">
                                            <div class="form-row">
                                                <div class="col-md-4 mb-3" style="text-align: left">
                                                        <h4 class="header-title">Student Profile<i class="fa fa-graduation-cap pl-1" style="color: #4336fb"></i></h4>
                                                </div>
                                            </div>
                                            <div class="p-3 mb-3 text-capitalize" style="border-top:1px solid #4336fb">
                                                <h6 class="py-3" style="text-align:center">Details <i class="fa fa-info-circle pl-1" style="color: #4336fb"></i></h6>

                                                    <div class="form-row mb-3 mt-3">
                                                        <div class="col-md-12 mb-3 text-center" style="">
                                                            <asp:Image ID="Stu_Image" runat="server" class="col-md-12 p-3" style="width:200px;border:1px solid black"/>
                                                        </div>
                                                    </div>

                                                    <div class="form-row mb-3 mt-3">
                                                        <div class="col-md-3 mb-3">
                                                            <asp:Label ID="Label1" runat="server" Text="RFID" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="RFID_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-4"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label7" runat="server" Text="Enroll.No." class="col-md-4"></asp:Label>
                                                            <asp:Label ID="Enroll_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-1" ></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label2" runat="server" Text="Fname" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="Fname_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-4" ></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label4" runat="server" Text="Lname" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="Lname_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-4" ></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-row mb-3">
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label3" runat="server" Text="Semester" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="Sem_lbl" runat="server" style="color:Gray;text-align:right;" class="col-md-1"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label11" runat="server" Text="Mobile" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="Mobile_lbl" runat="server" style="color:Gray;text-align:right;text-align:right" class="col-md-1"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label13" runat="server" Text="Email" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="Email_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-4" ></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label9" runat="server" Text="Zipcode" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="Zipcode_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-4" ></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-row mb-3">
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label5" runat="server" Text="Address" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="Address_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-8"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label8" runat="server" Text="Total Charges" class="col-md-6"></asp:Label>
                                                            <asp:Label ID="Charges_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-6" ></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label10" runat="server" Text="Issued Books" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="IssueBooks_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-4" ></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label6" runat="server" Text="Pending Book" class="col-md-4"></asp:Label>
                                                            <asp:LinkButton ID="PendingBook_lbl" runat="server" style="text-align:right" class="col-md-4" OnClick="BookInfo"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                             </div>
                                            <div class="p-3 mb-3" style="border-top:1px solid #4336fb" runat="server" id="Transaction_tbl">
                                                <h6 class="py-3" style="text-align:center">Transactions <i class="fa fa-history pl-1"  style="color: #4336fb"></i></h6>
                                                <div class="data-tables datatable-primary">
                                                    <table id="dataTable" class="text-center table-hover table-responsive-lg text-capitalize"  data-order='[[ 2, "desc" ],[ 4, "desc" ]]'>
                                                        <thead>
                                                            <tr>
                                                                <th>Book</th>
                                                                <th>Issue From</th>
                                                                <th>Issue Time</th>
                                                                <th>Return To</th>
                                                                <th>Return Time</th>
                                                                <th>Charges</th>
                                                                <th>Charges Remark</th>                            
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:PlaceHolder ID="StudentInfotbl" runat="server"></asp:PlaceHolder>
                                                        </tbody>                                        
                                                    </table>
                                                </div>
                                            </div>

                                            <asp:Label ID="data" runat="server" Text="Label" style="display:none"></asp:Label>
                                            <div id="Grid" style="display:none">
                                                    <table class="table">
                                                      <thead class="thead-dark">
                                                        <tr>
                                                                <th>Book</th>
                                                                <th>Issue From</th>
                                                                <th>Issue Time</th>
                                                                <th>Return To</th>
                                                                <th>Return Time</th>
                                                                <th>Charges</th>
                                                                <th>Charges Remark</th>                            
                                                        </tr>
                                                      </thead>
                                                      <tbody>
                                                         <asp:PlaceHolder ID="StudentInfotbl2" runat="server"></asp:PlaceHolder>
                                                      </tbody>                                          
                                                    </table>
                                                </div>
                                                <br />
                                                <asp:HiddenField ID="hfGridHtml" runat="server" />

                                                <div class="form-group">
                                                      <div class="form-inline btn" style="padding-left:12px">
                                                          <asp:LinkButton ID="btnExport" runat="server" OnClick="ExportToExcel" class="btn btn-outline-primary mt-1">Genrate Report <i class="fa fa-file-excel-o pl-1"></i></asp:LinkButton>
                                                      </div>
                                                </div>
                                                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                                                <script type="text/javascript">
                                                    $(function () {
                                                        $("[id*=btnExport]").click(function () {
                                                            $("[id*=hfGridHtml]").val($("#Grid").html());
                                                        });
                                                    });
                                                </script>

                                </form>
                            </div>
                        </div>
                    </div>    
</asp:Content>

