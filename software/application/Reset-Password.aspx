<%@ Page Title="" Language="C#" MasterPageFile="~/Entry.master" AutoEventWireup="true" CodeFile="Reset-Password.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Reset Password</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_Expired_OTP" visible="false">
                            <div class="alert alert-danger" role="alert">
                                Please try again, OTP is expired now.
                            </div>
                        </div>
                        <span id="test" runat="server" class="text-light"></span>
                            <div class="col-lg-5">
                                <div class="card shadow-lg border-0 rounded-lg mt-5">
                                    <div class="card-header bg-secondary text-light"><h3 class="text-center font-weight-light my-4">Reset Password</h3></div>
                                    <div class="card-body">
                                        <div runat="server" id="body1">
                                            <div class="form-group">
                                                <label class="mb-1" for="inputEmailAddress">Email</label>
                                                <asp:TextBox class="form-control py-4" id="Email" type="Email" placeholder="Enter Email" runat="server" required ></asp:TextBox>
                                                <span class="text-danger" runat="server" id="Alert_Invailed_Email" visible="false">Invailed User</span>
                                            </div>
                                            <div class="form-group d-flex align-items-center justify-content-center mt-4 mb-0">
                                                <asp:Button ID="Send_OTP_btn" runat="server" Text="Check" 
                                                    class="btn btn-dark" onclick="Send_OTP_btn_Click"/>
                                            </div>
                                        </div>

                                        <div runat="server" id="body2" visible="false">
                                            <div class="small mb-3 text-muted">Check your email <b runat="server" id="Email_lbl"></b> for an OTP to reset your password.</div>
                                            <form>
                                                <div class="form-group">
                                                    <label class="mb-1" for="inputEmailAddress">OTP</label>
                                                    <asp:TextBox class="form-control py-4" id="OTP" type="number" placeholder="Enter OTP" runat="server" required  AutoPostBack="True" OnTextChanged="OTP_TextChange" onkeydown="limit(this,6);" onkeyup="limit(this,6);"></asp:TextBox>
                                                    <span class="text-danger" runat="server" id="Alert_Invailed_OTP" visible="false">Invailed OTP</span>
                                                </div>
                                                <div class="form-group">
                                                    <label class="mb-1" for="inputEmailAddress">New Password</label>
                                                    <asp:TextBox class="form-control py-4" id="New_Password" type="password" placeholder="Enter New Password" runat="server" required ></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label class="mb-1" for="inputEmailAddress">Confirm Password</label>
                                                    <asp:TextBox class="form-control py-4" id="Confirm_Password" type="password" placeholder="Confirm Password" runat="server" required ></asp:TextBox>
                                                    <span class="text-danger" runat="server" id="Alert_Match_Password" visible="false">Password does not match</span>
                                                </div>
                                                <div class="form-group d-flex align-items-center justify-content-center mt-4 mb-0">
                                                    <asp:Button ID="Reset_Password_btn" runat="server" Text="Reset Password" class="btn btn-dark" onclick="Reset_Password_Click"/>
                                                </div>
                                            </form>
                                        </div>
                                    </div>
                                    <div class="card-footer text-center">
                                        <div class="small"><a class="text-dark" href="Login.aspx" runat="server" id="Return_Login">Return to Login</a></div>
                                    </div>
                                </div>
                            </div>
                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_Password_Changed" visible="false">
                            <div class="alert alert-success" role="alert">
                                Your Password changed successfully.
                            </div>
                        </div>
</asp:Content>

