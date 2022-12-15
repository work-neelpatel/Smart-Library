<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Book-Suggestions.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Book Suggestions</title>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        jQuery(function ($) {
            $("[id$=Subject_txt]").autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: '<%=ResolveUrl("~/Service.asmx/GetSubjects") %>',
                                    data: "{ 'subject': '" + request.term + "', 'field':'" + $("[id$=Field]").val() + "'}",
                                    dataType: "json",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    success: function (data) {
                                        response($.map(data.d, function (item) {
                                            return {
                                                label: item.split('-')[0],
                                                val: item.split('-')[1]
                                            }
                                        }))
                                    },
                                    error: function (response) {
                                        alert(response.responseText);
                                    },
                                    failure: function (response) {
                                        alert(response.responseText);
                                    }
                                });
                            },
                            select: function (e, i) {
                               // $("[id$=Subject_Id]").text(i.item.val);
                            },
                            minLength: 1
                        });
        });
    </script>
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
                <a href="Book-on-Read.aspx" class="text-secondary mr-2">
                        <abbr title="Books on Read"><i class="fa fa-building"></i></abbr>
                </a>
                <a href="Book-Requests.aspx" class="text-secondary mr-2">
                        <abbr title="Book Requests"><i class="fa fa-paper-plane"></i></abbr>
                </a>
                <a href="Book-Suggestions.aspx" class="text-light">
                        <abbr title="Book Suggestions"><i class="fa fa-lightbulb-o fa-lg"></i></abbr>
                </a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
                    <div id="Div1" runat="server" visible="true">    
                        <label runat="server" ID="Subject_Id"></label>
                    </div>
                    <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_Found2" visible="false">
                        <div id="Div4" class="alert alert-danger" role="alert" runat="server">
                            Student not Found!
                        </div>
                    </div>
                    <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_Fail2" visible="false">
                        <div id="Div5" class="alert alert-danger" role="alert" runat="server">
                            Other Student already Requested for this Book!
                        </div>
                    </div>
                    <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_Found" visible="false">
                        <div id="Div2" class="alert alert-danger" role="alert" runat="server">
                            Book not Found!
                        </div>
                    </div>
                    <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_Fail" visible="false">
                        <div id="Div3" class="alert alert-info" role="alert" runat="server">
                            This Book is currently in the Library.
                        </div>
                    </div>
                    <div class="container-fluid">
                        <div class="form-inline mb-4">
                            <h1 class="mt-4 col-xl-12">Book Suggestions</h1>
                        </div>
                        <div class="row pb-2 border-bottom">
                            <div class="col-xl-12">
                                <div class="card card-collapsable mb-4">
                                    <a class="text-left text-decoration-none text-dark" data-target="#AddBookRequest" data-toggle="collapse" role="button" aria-expanded="true" aria-controls="AddBookRequest">
                                        <div class="card-header form-inline pb-0">
                                            <p class="col-xl-8 pl-0 col-sm-8 col-8"><i class="fa fa-plus mr-2"></i>Add Book Suggestion</p>
                                            <p class="col-xl-4 pr-0 text-right col-sm-4 col-4"><i class="fa fa-chevron-down"></i></p>
                                        </div>
                                    </a>
                                    <div class="collapse show" id="AddBookRequest">
                                            <div class="card-body text-center">
                                                 <div class="col-xl-12 mb-2">
                                                        <div class="pt-2">
                                                            <div class="form-inline">
                                                                <asp:Label ID="Label4" runat="server" Text="Book Field" class="col-md-2 col-sm-2"></asp:Label>
                                                                <asp:Label ID="Label7" runat="server" Text="Book Semester" class="col-md-2 col-sm-2"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Text="Book Subject" class="col-md-2 col-sm-2"></asp:Label>
                                                                <asp:Label ID="Label3" runat="server" Text="Book Name" class="col-md-2 col-sm-2"></asp:Label>
                                                                <asp:Label ID="Label8" runat="server" Text="Enroll No" class="col-md-2 col-sm-2"></asp:Label>
                                                                <asp:Label ID="Label1" runat="server" Text="Student" class="col-md-2 col-sm-2"></asp:Label>
                                                            </div>
                                                            <div class="form-inline text-secondary mt-1 text-capitalize">
                                                                <div class="input-group input-group-joined col-xl-2 col-sm-2">
                                                                    <asp:DropDownList ID="Field" runat="server" class="form-control btn btn-secondary dropdown-toggle" data-toggle="dropdown" style="height:auto;text-transform: capitalize;" role="button" aria-haspopup="true" aria-expanded="s" AutoPostBack="true" OnTextChanged="Field_Change">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="input-group input-group-joined col-xl-2 col-sm-2">
                                                                    <asp:DropDownList ID="Semester" runat="server" class="form-control btn btn-secondary dropdown-toggle" data-toggle="dropdown" style="height:auto;text-transform: capitalize;" role="button" aria-haspopup="true" aria-expanded="s" AutoPostBack="true" OnTextChanged="Semester_Change">
                                                                        <asp:ListItem Value="1">Semester 1</asp:ListItem>
                                                                        <asp:ListItem Value="2">Semester 2</asp:ListItem>
                                                                        <asp:ListItem Value="3">Semester 3</asp:ListItem>
                                                                        <asp:ListItem Value="4">Semester 4</asp:ListItem>
                                                                        <asp:ListItem Value="5">Semester 5</asp:ListItem>
                                                                        <asp:ListItem Value="6">Semester 6</asp:ListItem>
                                                                        <asp:ListItem Value="None">None</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="input-group input-group-joined col-xl-2 col-sm-2"  id="Div_Subject_drpdwn" runat="server" >
                                                                    <asp:DropDownList ID="Subject" runat="server" class="form-control btn btn-secondary dropdown-toggle" data-toggle="dropdown" style="height:auto;text-transform: capitalize;" role="button" aria-haspopup="true" aria-expanded="s">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="input-group input-group-joined col-xl-2 col-sm-2" id="Div_Subject_txt" runat="server" visible="false">
                                                                    <asp:TextBox ID="Subject_txt" runat="server" class="form-control" type="text" placeholder="Subject" AutoPostBack="true" OnTextChanged="Subject_TextChanged"></asp:TextBox>
                                                                </div>
                                                                <div class="input-group input-group-joined col-xl-2 col-sm-2">
                                                                    <asp:TextBox ID="Book_Name" runat="server" class="form-control" type="text" placeholder="Book" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                                <div class="input-group input-group-joined col-xl-2 col-sm-2">
                                                                    <asp:TextBox ID="Enrollment_No" runat="server" class="form-control" type="text" placeholder="Enroll No" ReadOnly="true" AutoPostBack="true"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="Student_Name" runat="server" Text="Student" class="col-md-2 col-sm-2"></asp:Label>
                                                            </div>
                                                        <div class="col-xl-12 pl-0 mt-1 ">
                                                                <label class="col-6 col-sm-6 col-xl-6 pl-0 small"></label>
                                                                <asp:LinkButton ID="Alert_Suggest_Subject" runat="server" class="col-3 col-sm-3 col-xl-3 pl-0 small" OnClick="Subject_Suuggest">zsdfvbsd</asp:LinkButton>
                                                        </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-12 text-center col-sm-12 col-12">
                                                        <asp:LinkButton ID="AddSuggestion" runat="server" class="btn btn-success mt-1" Visible="false" >Add Suggestion <i class="fa fa-plus pl-1" aria-hidden="true"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                   
                        
                                                
                        <div class="card my-4">
                            <div class="card-header form-inline">
                                <div class="col-xl-8 pl-0 col-sm-8 col-6">
                                    <i class="fa fa-lightbulb-o fa-lg mr-1"></i>
                                    Book Suggestion Data
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
                                                <th>Book Field</th>
                                                <th>Book Semester</th>
                                                <th>Book Subject</th>
                                                <th>Student</th>
                                                <th>Add From</th>
                                                <th>Suggestion Date</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tfoot class="table-secondary">
                                            <tr>
                                                <th>Book</th>
                                                <th>Book Field</th>
                                                <th>Book Semester</th>
                                                <th>Book Subject</th>
                                                <th>Student</th>
                                                <th>Add From</th>
                                                <th>Suggestion Date</th>
                                                <th>Action</th>
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
</asp:Content>
