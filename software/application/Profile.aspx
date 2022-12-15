<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="Default2" ValidateRequest="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Admin Profile</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script>
        jQuery(function ($) {
            var $inputs = $('input[name=month],input[name=year],input[name=day]');
            $inputs.on('input', function () {
                // Set the required property of the other input to false if this input is not empty.
                $inputs.not(this).prop('required', !$(this).val().length);
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" Runat="Server">
    <li><a href="Books"><i class="ti-book"></i> <span>Books</span></a></li>
    <li><a href="Students"><i class="fa fa-graduation-cap"></i> <span>Students</span></a></li>
    <li><a href="IssueBook" onclick="red_issue()" data-toggle="popover"  data-title="Alert" data-content="Scan Student ID and Book ID during page load." data-placement="bottom"><i class="fa fa-arrow-circle-left"></i><span>Issue Book</span></a></li>
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
                                                        <h4 class="header-title">Admin Profile<i class="ti-user pl-1" style="color: #4336fb"></i></h4>
                                                </div>
                                            </div>
                                            <div class="p-3 mb-3 text-capitalize" style="border-top:1px solid #4336fb">
                                                <h6 class="py-3" style="text-align:center">Details <i class="fa fa-info-circle pl-1" style="color: #4336fb"></i></h6>

                                                    <div class="form-row mb-3 mt-3">
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label7" runat="server" Text="Name" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="Name_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-1" ></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 mb-3">
                                                            <asp:Label ID="Label1" runat="server" Text="Issued Books" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="Ibooks_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-4"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label2" runat="server" Text="Returned Books" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="Rbooks_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-4" ></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 mb-3"  style="padding-right: 50px">
                                                            <asp:Label ID="Label4" runat="server" Text="Total Charges" class="col-md-4"></asp:Label>
                                                            <asp:Label ID="Tcharges_lbl" runat="server" style="color:Gray;text-align:right" class="col-md-4" ></asp:Label>
                                                        </div>
                                                    </div>
                                             </div>
                                            <div class="p-3 mb-3" style="border-top:1px solid #4336fb" runat="server" id="Transaction_tbl">
                                                <h6 class="py-3" style="text-align:center">Transactions <i class="fa fa-history pl-1"  style="color: #4336fb"></i></h6>
                                                    <div class="form-row mb-3">
                                                        <div class="col-md-2 mb-3" style="padding-right: 50px">
                                                            <label for="validationCustomUsername">Day</label>
                                                            <asp:Label ID="Day_lbl" runat="server"></asp:Label>                      
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text" id="Span3"><i class="ti-calendar"></i></span>
                                                                </div>
                                                                <input type="number" class="form-control" name="day" placeholder="Day" aria-describedby="inputGroupPrepend" required  id="day" min="1" max="31" onkeydown="limit(this,2);" onkeyup="limit(this,2);">
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2 mb-3" style="padding-right: 50px">
                                                            <label for="validationCustomUsername">Month</label>
                                                            <asp:Label ID="Month_lbl" runat="server"></asp:Label>                      
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text" id="Span1"><i class="ti-calendar"></i></span>
                                                                </div>
                                                                <input type="number" class="form-control" name="month" placeholder="Month" aria-describedby="inputGroupPrepend" required  id="month" min="1" max="12" onkeydown="limit(this,2);" onkeyup="limit(this,2);">
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2 mb-3"  style="padding-right: 50px">
                                                            <label for="validationCustomUsername">Year</label>
                                                            <asp:Label ID="Year_lbl" runat="server"></asp:Label>                      
                                                            <div class="input-group">
                                                                <div class="input-group-prepend">
                                                                    <span class="input-group-text" id="Span2"><i class="ti-calendar"></i></span>
                                                                </div>
                                                                <input type="number" class="form-control" name="year" id="year" placeholder="Year" aria-describedby="inputGroupPrepend" required onkeydown="limit(this,4);" onkeyup="limit(this,4);">
                                                            </div>
                                                        </div>
<!--                                                        <div class="col-md-2 mb-3"  style="padding-right: 50px">
                                                            <label for="validationCustomUsername">Type</label>
                                                            <asp:Label ID="Student_lbl" runat="server"></asp:Label>                      
                                                            <div class="input-group">
                                                                <div class="dropdown">
                                                                <asp:DropDownList ID="Ttype" runat="server" class="form-control btn btn-secondary dropdown-toggle" data-toggle="dropdown" style="height:auto;text-transform: capitalize;" role="button" aria-haspopup="true" aria-expanded="false">                                        
                                                                        <asp:ListItem Value="both" class="dropdown-item">Both</asp:ListItem>
                                                                        <asp:ListItem Value="ifrom" class="dropdown-item">Issue From</asp:ListItem>
                                                                        <asp:ListItem Value="rto" class="dropdown-item">Return To</asp:ListItem>
                                                                </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>-->
                                                        <div class="col-md-6 mt-4" style="text-align:right;">
                                                            <asp:Button ID="Find" class="btn btn-outline-primary mt-1 col-md-2" runat="server" Text="Find" />
                                                        </div>
                                                      </div>
                                                <div class="data-tables datatable-primary">
                                                    <table id="dataTable" class="text-center table-hover table-responsive-lg text-capitalize"  data-order='[[ 3, "desc" ],[ 5, "desc" ]]'>
                                                        <thead>
                                                            <tr>
                                                                <th>Book</th>
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
                                                            <asp:PlaceHolder ID="AdminTransactiontbl" runat="server"></asp:PlaceHolder>
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
                                                         <asp:PlaceHolder ID="AdminTransactiontbl2" runat="server"></asp:PlaceHolder>
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

