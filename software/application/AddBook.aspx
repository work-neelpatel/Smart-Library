<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.master" AutoEventWireup="true" CodeFile="AddBook.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Add Book</title>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/jquery-ui.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".autosuggest1").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        url: "AddBook.aspx/AutoSuggestAuthor",
                        data: "{'author':'" + document.getElementById('Author1').value + "','author2':'" + document.getElementById('Author2').value + "','author3':'" + document.getElementById('Author3').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                }
            });
            $(".autosuggest2").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        url: "AddBook.aspx/AutoSuggestAuthor",
                        data: "{'author':'" + document.getElementById('Author2').value + "','author2':'" + document.getElementById('Author1').value + "','author3':'" + document.getElementById('Author3').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                }
            });
            $(".autosuggest3").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        url: "AddBook.aspx/AutoSuggestAuthor",
                        data: "{'author':'" + document.getElementById('Author3').value + "','author2':'" + document.getElementById('Author2').value + "','author3':'" + document.getElementById('Author1').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                }
            });
            $(".autosuggest4").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        url: "AddBook.aspx/AutoSuggestSubject",
                        data: "{'subject':'" + document.getElementById('Subject').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                }
            });
        });
 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="header" Runat="Server">
    <li class="active"><a href="Books"><i class="ti-book"></i> <span>Books</span></a></li>
    <li><a href="Students"><i class="fa fa-graduation-cap"></i> <span>Students</span></a></li>
    <li style="cursor:pointer"><a id="A1" runat="server" onclick="red_issue()" data-toggle="popover"  data-title="Alert" data-content="Scan Student ID and Book ID during page load." data-placement="bottom"><i class="fa fa-arrow-circle-left"></i><span>Issue Book</span></a></li>
    <li  style="cursor:pointer"><a id="A2" runat="server" onclick="red_return()" data-toggle="popover"  data-title="Alert" data-content="Scan Book ID during page load." data-placement="bottom"><i class="fa fa-arrow-circle-right"></i><span>Return Book</span></a></li>
    <li><a href="Transactions"><i class="fa fa-history"></i><span>Transactions</span></a></li>
    <li><a href="IncomingBooks"><i class="fa fa-sign-in"></i><span>Incoming Books</span></a></li>
        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
                    <div class="col-6">
                        <div class="card  mt-5">
                            <div class="card-body"> 
                                <form id="Form1" class="needs-validation" novalidate="" runat="server">
                                            <div class="form-row">
                                                <div class="col-md-4 mb-3" style="text-align: left">
                                                        <h4 class="header-title">Add Book <i class="fa fa-plus-square pl-1" style="color: #4336fb"></i></h4>
                                                </div>
                                            </div>
                                            <div class="form-row mb-3">
                                                <div class="col-md-6 mb-3"  style="padding-right: 50px">
                                                    <label for="validationCustomUsername">RFID</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="RFID" runat="server"  class="form-control" placeholder="RFID" required="" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 mb-3" style="padding-right: 50px">
                                                    <label for="validationCustomUsername">Name</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Name" runat="server"  class="form-control" placeholder="Name" required="" ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row mb-3">
                                                <div class="col-md-6 mb-3"  style="padding-right: 50px">
                                                    <label for="validationCustomUsername">ISBN</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="ISBN" runat="server"  type="number" class="form-control" placeholder="ISBN" required=""  onkeydown="limit(this,11);" onkeyup="limit(this,11);"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 mb-3" style="padding-right: 50px">
                                                    <label for="validationCustomUsername">Semester</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Semester" runat="server"  type="number" class="form-control" placeholder="Semester" required=""  onkeydown="limit(this,1);" onkeyup="limit(this,1);" max=6 min=1></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row mb-3">
                                                <div class="col-md-6 mb-3"  style="padding-right: 50px">
                                                    <label for="validationCustomUsername">Subject</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Subject" runat="server"  class="form-control autosuggest4" placeholder="Subject" required="" ></asp:TextBox>                                                    
                                                    </div>
                                                </div>
                                                <div class="col-md-6 mb-3" style="padding-right: 50px">
                                                    <label for="validationCustomUsername">Edition</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Edition" runat="server"  type="number" class="form-control" placeholder="Edition" required=""  onkeydown="limit(this,2);" onkeyup="limit(this,2);"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row mb-3">
                                                <div class="col-md-6 mb-3"  style="padding-right: 50px">
                                                    <label for="validationCustomUsername">Publisher</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Publisher" runat="server"  class="form-control" placeholder="Publisher" required="" ></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 mb-3" style="padding-right: 50px">
                                                    <label for="validationCustomUsername">Author 1</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Author1" runat="server"  class="form-control autosuggest1" placeholder="Author1" required="" ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row mb-3">
                                                <div class="col-md-6 mb-3"  style="padding-right: 50px">
                                                    <label for="validationCustomUsername">Author2</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Author2" runat="server"  class="form-control autosuggest2" placeholder="Author2" required="" ></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 mb-3" style="padding-right: 50px">
                                                    <label for="validationCustomUsername">Author 3</label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="Author3" runat="server"  class="form-control autosuggest3" placeholder="Author3" required="" ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="justify-content-center" style="text-align:center;">
                                                        <asp:Button ID="Button1" class="btn mt-1"  runat="server"  Text="Add Book" Font-Size="Medium" />
                                            </div>
                                </form>
                            </div>
                        </div>
                    </div>
</asp:Content>

