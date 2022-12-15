<%@ Page Title="" Language="C#" MasterPageFile="~/Entry.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Login</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
                            <div class="col-lg-5">
                                <div class="card shadow-lg border-0 rounded-lg mt-5">
                                    <div class="card-header bg-secondary text-light text-center">
                                        <img src="assets\img\ljcca-logo.png" height="100px">
                                        <h3 class="my-2">LJ Smart Library</h3>
                                    </div>
                                    <div class="card-body">
                                        <form>
                                            <div class="form-group">
                                                <label class="mb-1" for="inputEmailAddress">Email</label>
                                                <asp:TextBox class="form-control py-4" id="Email" type="email" placeholder="Enter Email" runat="server"  required  AutoPostBack="True" OnTextChanged="Email_TextChange"></asp:TextBox>
                                                <span class="text-danger col-xl-12 pl-0" runat="server" id="Alert_Invailed_Email" visible="false">Invailed User</span>
                                            </div>
                                            <div class="form-group">
                                                <label class="mb-1" for="inputPassword">Password</label>
                                                <asp:TextBox class="form-control py-4" id="Password" type="password" placeholder="Enter Password" runat="server" required ReadOnly="true"></asp:TextBox>
                                                <span class="text-danger" runat="server" id="Alert_Invailed_Password"  visible="false">Invailed Password</span>
                                            </div>
                                                <div class="form-group d-flex align-items-center justify-content-between mt-4 mb-0">
                                                <a class="small text-dark" href="Reset-Password.aspx">Forgot Password?</a>
                                                <asp:Button ID="Login" runat="server" Text="Login" onclick="Login_Click" class="btn btn-dark"/>
                                            </div>
                                        </form>
                                    </div>
                                    <div class="card-footer text-center">
                                        <div class="small"><a href="Register.aspx" class="text-dark">Need an account? Sign up!</a></div>
                                    </div>
                                </div>
                            </div>

</asp:Content>

