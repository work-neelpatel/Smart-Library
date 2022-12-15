<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <!-- style css -->
    <link rel="stylesheet" href="assets/css/typography.css">
    <link rel="stylesheet" href="assets/css/default-css.css">
    <link rel="stylesheet" href="assets/css/styles.css">
    <link rel="stylesheet" href="assets/css/responsive.css">
    <!-- modernizr css -->
    <script src="assets/js/vendor/modernizr-2.8.3.min.js"></script>
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
                        url: "Default.aspx/AutoSuggestAuthor",
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
                        url: "Default.aspx/AutoSuggestAuthor",
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
                        url: "Default.aspx/AutoSuggestAuthor",
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
                        url: "Default.aspx/AutoSuggestSubject",
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="ui-widget">
            <label>Enter Author1: </label>
            <div class="form-row">
                <div>
                    <asp:TextBox ID="Author1" runat="server"  class="form-control autosuggest1"></asp:TextBox>
                </div>
            </div>
            <br />
            <label>Enter Author2: </label>
            <asp:TextBox runat="server" ID="Author2" class="autosuggest2"></asp:TextBox>
            <br />
            <label>Enter Author3: </label>
            <asp:TextBox runat="server" ID="Author3" class="autosuggest3"></asp:TextBox>
            <br />
            <label>Enter Subject: </label>
            <asp:TextBox runat="server" ID="Subject" class="autosuggest4"  AutoPostBack="true" OnTextChanged="GetSemester"></asp:TextBox>
            <br />
            <label>Semester: </label>
            <asp:TextBox runat="server" ID="Semester" ReadOnly="true"></asp:TextBox>
        </div>
    </form>
</body>
</html>
