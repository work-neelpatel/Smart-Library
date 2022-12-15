<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Resetpassword.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainhead" Runat="Server">
    <title>Reset Password</title>
    <script>
        function limit(element) {
            var max_chars = 6;

            if (element.value.length > max_chars) {
                element.value = element.value.substr(0, max_chars);
            }
        }
  
  		function pass_match(){
        swal({
          title: "Password doesn't match!",
          icon: "error",
          button: "Try again",
          dangerMode: true,
        });
		}
        
  		function otp_match(){
        swal({
          title: "Wrong OTP!",
          icon: "error",
          button: "Try again",
          dangerMode: true,
        });
		}

		function success_changed(){
            swal({
              title: "Password changed successfully.",
              icon: "success",
            }).then((value) => {
                window.location ='SignIn';        
            });
		}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainbody" Runat="Server">
                        <h5>Reset Password</h5>
                        <h7 style="color:#7e74ff;padding-top:30px">Check OTP on your email <strong id="email" runat="server"></strong></h7>
                    </div>
                    <div class="login-form-body">
                        <div class="form-gp">
                            <label for="exampleInputEmail1" id="OTP_label" runat="server">OTP</label>
                            <asp:TextBox ID="OTP" runat="server" type="number" onkeydown="limit(this);" onkeyup="limit(this);"></asp:TextBox>
                            <i class="ti-key"></i>
                            <div class="text-danger"></div>
                        </div>
                        <div class="form-gp">
                            <label for="exampleInputPassword1" id="Newpass_label" runat="server">New Password</label>
                            <asp:TextBox ID="New_pass" runat="server" type="password"></asp:TextBox>
                            <i class="ti-lock"></i>
                            <div class="text-danger"></div>
                        </div>
                        <div class="form-gp">
                            <label for="exampleInputPassword1" id="Confirmpass_label" runat="server">Confirm New Password</label>
                            <asp:TextBox ID="Confirm_pass" runat="server" type="password"></asp:TextBox>
                            <i class="ti-lock"></i>
                            <div class="text-danger"></div>
                        </div>
                        <div class="submit-btn-area">
                            <button id="form_submit" type="submit" runat="server" onserverclick="Reset_Password">Change Password <i class="ti-exchange-vertical"></i></button>
                        </div>
<!--                        <div class="row mb-4 rmber-area mt-4" id="alert" >
                            <div class="alert-items col-12 text-center">
                                <div class="alert alert-danger" role="alert" id="alert_OTP" runat="server" visible="false">
                                    <strong>Security alert!</strong> Wrong OTP.
                                </div>
                                <div class="alert alert-danger" role="alert" id="alert_match" runat="server" visible="false">
                                    <strong>Security alert!</strong> Password doesn't match.
                                </div>
                                <div class="alert alert-success" role="alert" id="alert_success" runat="server" visible="false">
                                            <strong>Password Changed!</strong> Go for <a href="Signin" class="alert-link">Sign In</a> .
                                        </div>
                            </div>
                        </div>-->
                    </div>
</asp:Content>

