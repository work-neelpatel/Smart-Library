<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Signin.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainhead" Runat="Server">
    <title>Sign In</title>
    <script>
        function invailed_user(){
        swal({
          title: "Invailed User!",
          icon: "error",
          button: "Try again",
          dangerMode: true,
        });
		}    

        function wrong_uop(){
        swal({
          title: "Wrong User or Password!",
          icon: "error",
          button: "Try again",
          dangerMode: true,
        });
		}    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainbody" Runat="Server">
                        <h5>Sign In</h5>
                    </div>
                    <div class="login-form-body">
                        <div class="form-gp">
                            <label for="exampleInputEmail1" id="Username_label" runat="server">Username</label>
                            <asp:TextBox ID="Username" runat="server" type="text" required></asp:TextBox>
                            <i class="ti-user"></i>
                            <div class="text-danger"></div>
                        </div>
                        <div class="form-gp">
                            <label for="exampleInputPassword1" id="Password_label" runat="server">Password</label>
                            <asp:TextBox ID="Password" runat="server" type="Password"></asp:TextBox>
                            <i class="ti-lock"></i>
                            <div class="text-danger"></div>
                        </div>
                        <div class="row mb-4 rmber-area">
                            <div class="col-12 text-right">
                                <asp:LinkButton ID="ForgotPassword" runat="server" 
                                    onclick="ForgotPassword_Click">Forgot Password?</asp:LinkButton>
                            </div>
                        </div>
                        <div class="submit-btn-area">
                            <button id="form_submit" type="submit" runat="server" onserverclick="SignIn">Sign In <i class="ti-unlock"></i></button>
                        </div>
<!--                        <div class="row mb-4 rmber-area mt-4">
                            <div class="alert-items col-12 text-center">
                                <div class="alert alert-danger" role="alert" id="alert"  runat="server" visible="false">
                                    <strong>Security alert!</strong> Wrong Email or Password.
                                </div>
                                <div class="alert alert-danger" role="alert"  id="alert_resetpass"  runat="server" visible="false">
                                    <strong>Security alert!</strong> You need to have Username to Reset password.
                                </div>
                            </div>
                        </div>-->
                     </div>
</asp:Content>

