<%@ Page Title="" Language="C#" MasterPageFile="~/Entry.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Create Account</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
                            <div class="col-lg-7">
                                <div class="card shadow-lg border-0 rounded-lg mt-5">
                                    <div class="card-header bg-secondary"><h3 class="text-center font-weight-light my-4 text-light">Create Account</h3></div>
                                    <div class="card-body">
                                        <div class="small mb-3 text-muted">Here you need a help of Library system Admin to create new Admin account</div>
                                        <form>
                                            <div class="form-row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="small mb-1" for="inputFirstName">Admin Email</label>
                                                        <asp:TextBox class="form-control py-4" id="Admin_Email" type="email" placeholder="Enter Admin Email" required runat="server" AutoPostBack="True" OnTextChanged="Admin_Email_TextChange"></asp:TextBox>
                                                        <span class="text-danger col-xl-12 pl-0" runat="server" id="Alert_Invailed_Email" visible="false">Invailed User</span>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="small mb-1" for="inputLastName">Admin Password</label>
                                                        <asp:TextBox class="form-control py-4" id="Admin_Password" type="password" placeholder="Enter Admin Password" required runat="server" AutoPostBack="True" OnTextChanged="Admin_Password_TextChange" ReadOnly="true"></asp:TextBox>
                                                        <span class="text-danger col-xl-12 pl-0" runat="server" id="Alert_Invailed_Password" visible="false">Invailed Password</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="small mb-1" for="inputFirstName">Email</label>
                                                        <asp:TextBox class="form-control py-4" id="Email" type="email" placeholder="Enter Email" required runat="server" ReadOnly="true" AutoPostBack="True" OnTextChanged="Email_TextChange"></asp:TextBox>
                                                        <span class="text-danger col-xl-12 pl-0" runat="server" id="Alert_Email_Exist" visible="false">Email already Exists</span>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="small mb-1" for="inputLastName">Username</label>
                                                        <asp:TextBox class="form-control py-4" id="Username" type="text" placeholder="Enter Username" required runat="server" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="small mb-1" for="inputPassword">Password</label>
                                                        <asp:TextBox class="form-control py-4" id="Password" type="password" placeholder="Enter Password" required runat="server" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label class="small mb-1" for="inputConfirmPassword">Confirm Password</label>
                                                        <asp:TextBox class="form-control py-4" id="Confirm_Password" type="password" placeholder="Confirm Password" required runat="server" ReadOnly="true"></asp:TextBox>
                                                        <span class="text-danger" runat="server" id="Alert_Match_Password" visible="false">Password does not match</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group mt-2 mb-0 text-center">
                                                <asp:Button ID="Create_Account" runat="server" Text="Create Account" class="btn btn-dark" onclick="Create_Account_Click" Visible="false"/>
                                            </div>
                                        </form>
                                    </div>
                                    <div class="card-footer text-center">
                                        <div class="small"><a href="Login.aspx" class="text-dark">Have an account? Go to Login</a></div>
                                    </div>
                                </div>
                            </div>
                        <div class="col-xl-12 justify-content-center row pt-2" runat="server" id="Alert_Account_Created" visible="false">
                            <div class="alert alert-success" role="alert">
                                Your Account created successfully.
                            </div>
                        </div>
</asp:Content>

