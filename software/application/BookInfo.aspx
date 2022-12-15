<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.master" AutoEventWireup="true" CodeFile="BookInfo.aspx.cs" Inherits="Default2" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Book Profile</title>
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
                                                <div class="col-md-12 mb-3" style="text-align: left">
                                                        <h4 class="header-title">Book Profile<i class="ti-book pl-1" style="color: #4336fb"></i></h4>
                                                        <h6 class="text-danger" runat="server" id="notice"></h6>
                                                </div>
                                            </div>
                                            <div class="p-3 mb-3 text-capitalize" style="border-top:1px solid #4336fb">
                                                <h6 class="py-3" style="text-align:center">Details <i class="fa fa-info-circle pl-1" style="color: #4336fb"></i></h6>

                                                    <div class="form-row mb-3 mt-3">
                                                        <div class="col-md-6 mb-3 text-right">
                                                            <asp:Label ID="Label12" runat="server" Text="Current Status" class="col-md-6" style="font-size:medium" ></asp:Label>
                                                            <asp:Label ID="Status_lbl" runat="server" style="text-align:right;font-size:medium" class="col-md-6"></asp:Label>
                                                        </div>
                                                        <div class="col-md-6 mb-3 text-left" id="EstDate" runat="server">
                                                            <asp:Label ID="Label14" runat="server" Text="Est. return Date" class="col-md-6" style="font-size:medium" ></asp:Label>
                                                            <asp:Label ID="EstDate_lbl" runat="server" style="text-align:right;font-size:medium" class="col-md-6"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="form-row mb-3 mt-3">
                                                        <div class="col-md-3 mb-3" style="padding-right: 50px">
                                                            <asp:Label ID="Label1" runat="server" Text="RFID" class="col-md-6"></asp:Label>
                                                            <asp:Label ID="RFID_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-6"></asp:Label>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="RFID_txt" runat="server" class="form-control" placeholder="RFID" aria-describedby="inputGroupPrepend" required ReadOnly="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label7" runat="server" Text="Name" class="col-md-6"></asp:Label>
                                                            <asp:Label ID="name_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-6" ></asp:Label>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="name_txt" runat="server" class="form-control" placeholder="Name" aria-describedby="inputGroupPrepend" required></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label2" runat="server" Text="ISBN" class="col-md-6"></asp:Label>
                                                            <asp:Label ID="ISBN_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-6" ></asp:Label>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="ISBN_txt" runat="server" class="form-control" placeholder="ISBN" aria-describedby="inputGroupPrepend" required type="number" onkeydown="limit(this,11);" onkeyup="limit(this,11);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label4" runat="server" Text="Subject" class="col-md-6"></asp:Label>
                                                            <asp:Label ID="Subject_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-6" ></asp:Label>
                                                            <div class="input-group">
                                                            <asp:TextBox ID="Subject_txt" runat="server" class="form-control" placeholder="Subject" aria-describedby="inputGroupPrepend" required></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-row mb-3">
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label5" runat="server" Text="Publisher" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="Publisher_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-6" ></asp:Label>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="Publisher_txt" runat="server" class="form-control" placeholder="Publisher" aria-describedby="inputGroupPrepend" required></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label9" runat="server" Text="Author" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="Author1_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-6" ></asp:Label>
                                                            <div class="input-group">
                                                            <asp:TextBox ID="Author1_txt" runat="server" class="form-control" placeholder="Author 1" aria-describedby="inputGroupPrepend" required></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label11" runat="server" Text="Author" class="col-md-6"></asp:Label>
                                                            <asp:Label ID="Author2_lbl" runat="server" style="color:Gray;text-align:right;text-align:right" class="col-md-6"></asp:Label>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="Author2_txt" runat="server" class="form-control" placeholder="Author 2" aria-describedby="inputGroupPrepend" required></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label13" runat="server" Text="Author" class="col-md-6"></asp:Label>
                                                            <asp:Label ID="Author3_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-6" ></asp:Label>
                                                            <div class="input-group">
                                                            <asp:TextBox ID="Author3_txt" runat="server" class="form-control" placeholder="Author 3" aria-describedby="inputGroupPrepend" required></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-row mb-3">
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label3" runat="server" Text="Semester" class="col-md-6"></asp:Label>
                                                            <asp:Label ID="Sem_lbl" runat="server" style="color:Gray;text-align:right;" class="col-md-6"></asp:Label>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="Sem_txt" runat="server" class="form-control" placeholder="Semester" aria-describedby="inputGroupPrepend" required type="number" onkeydown="limit(this,1);" onkeyup="limit(this,1);" min=1 MaxLength=6></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label6" runat="server" Text="Edition" class="col-md-6"></asp:Label>
                                                            <asp:Label ID="Edition_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-6" ></asp:Label>
                                                            <div class="input-group">
                                                            <asp:TextBox ID="Edition_txt" runat="server" class="form-control" placeholder="Edition" aria-describedby="inputGroupPrepend" required type="number" onkeydown="limit(this,2);" onkeyup="limit(this,2);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label8" runat="server" Text="Total Charges" class="col-md-6"></asp:Label>
                                                            <asp:Label ID="Charges_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-6" ></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label10" runat="server" Text="Issued Times" class="col-md-6"></asp:Label>
                                                            <asp:Label ID="IssueTimes_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-6" ></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="form-row">
                                                        <div class="col-md-3 mb-3" runat="server" ID="Update">
                                                            <asp:Button ID="Button1" class="btn btn-primary"  name="Update" runat="server"  Text="Update" 
                                                                 onclick="Update_Click" />
                                                        </div>
                                                        <div class="col-md-3 mb-3" runat="server" ID="Update_Done">
                                                            <asp:Button ID="Button2" class="btn"  runat="server"  Text="Done" 
                                                                  name="UpdateDone" onclick="UpdateDone_Click"/>
                                                        </div>
                                                        <div class="col-md-3 mb-3" runat="server" ID="Update_RFID">
                                                            <asp:Button ID="Button3" class="btn btn-success"  runat="server"  
                                                                Text="Update RFID" name="UpdateRFID" 
                                                                onclick="UpdateRFID_Click"/>
                                                        </div>
                                                    </div>
                                             </div>
                                             <div runat="server" id="Transaction_tbl"> 
                                            <div class="p-3 mb-3" style="border-top:1px solid #4336fb">
                                                <h6 class="py-3" style="text-align:center">Transactions <i class="fa fa-history pl-1" style="color: #4336fb"></i></h6>
                                                <div class="data-tables datatable-primary">
                                                    <table id="dataTable" class="text-center table-hover table-responsive-lg text-capitalize"  data-order='[[ 2, "desc" ],[ 4, "desc" ]]'>
                                                        <thead>
                                                            <tr>
                                                                <th>Student</th>
                                                                <th>Issue From</th>
                                                                <th>Issue Time</th>
                                                                <th>Return To</th>
                                                                <th>Return Time</th>
                                                                <th>Charges</th>
                                                                <th>Charges Remark</th>                            
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:PlaceHolder ID="BookInfotbl" runat="server"></asp:PlaceHolder>
                                                        </tbody>                                        
                                                    </table>
                                                </div>
                                            </div>

                                            <asp:Label ID="data" runat="server" Text="Label" style="display:none"></asp:Label>
                                            <div id="Grid" style="display:none">
                                                    <table class="table">
                                                      <thead class="thead-dark">
                                                        <tr>
                                                                <th>Student</th>
                                                                <th>Issue From</th>
                                                                <th>Issue Time</th>
                                                                <th>Return To</th>
                                                                <th>Return Time</th>
                                                                <th>Charges</th>
                                                                <th>Charges Remark</th>                            
                                                        </tr>
                                                      </thead>
                                                      <tbody>
                                                         <asp:PlaceHolder ID="BookInfotbl2" runat="server"></asp:PlaceHolder>
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

