<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Settings</title>
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
                <a href="Book-Suggestions.aspx" class="text-secondary">
                        <abbr title="Book Suggestions"><i class="fa fa-lightbulb-o fa-lg"></i></abbr>
                </a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" Runat="Server">
                    <asp:Label ID="Admin_Id" runat="server" Visible="false"></asp:Label>
                    <div class="container-fluid">
                        <div class="form-inline mb-4">
                            <h1 class="mt-4 col-xl-12">Settings</h1>
                        </div>
                        <div class="row pb-2 border-bottom" id="COM-Port-Setting">
                            <div class="col-xl-12">
                                <div class="card mb-3">
                                <div class="card-header form-inline">
                                    <div class="col-xl-12 pl-0">
                                        <i class="fa fa-plug mr-1"></i>
                                        COM Port 
                                    </div>
                               </div>
                            <div class="card-body">
                            <div class="small mb-2 text-muted pb-2 border-bottom">Here COM(Communication) Port is used to pass the RFID data from RFID Device to System, So select a vailed COM Port is very neccesary.</div>
                                <div class="mb-2 small col-xl-12 col-lg-12 col-12 pl-0 pb-2 border-bottom">
                                    <div class="text-dark mb-2">Here are the steps to find out a proper COM Port:</div>
                                    <div class="text-muted mb-1">1. Press<i class="fa fa-windows mx-2"></i>key and goto <span class="font-weight-bold">Device Manager</span>.</div>
                                    <div class="text-muted">2. Here you will find RFID Device's COM Port under <span class="font-weight-bold">Ports</span> section (if you connected the RFID Device with System).</div>
                                </div>
                            <div class="form-inline col-xl-12 pl-0 mb-2 text-left">
                                <div class="input-group input-group-joined col-xl-10 col-sm-10 col-12 pl-0">
                                    <asp:Label ID="Label2" runat="server" class="col-8 col-sm-4 col-xl-2 ">Selected COM Port</asp:Label>
                                    <asp:Label ID="Curr_COM_Port" runat="server" class="text-secondary col-4 col-sm-4 col-xl-8"></asp:Label>
                                </div>
                                <div class="col-xl-2 col-sm-2 col-12 pr-0">
                                    <asp:LinkButton ID="Change_Port" runat="server" 
                                        class="btn btn-success float-right mt-1" onclick="Change_Port_Click" >Change Port<i class="fa fa-pencil-square ml-2" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </div>
                            <div class="form-inline col-xl-12 pl-0 mb-2 text-left" runat="server" id="Div_Change_Port" visible="false">
                                <div class="input-group input-group-joined col-xl-10 col-sm-10 col-12 pl-0">
                                    <asp:Label ID="Label3" runat="server" class="col-8 col-sm-4 col-xl-2 ">Select COM Port</asp:Label>
                                    <div class="dropdown col-4 col-sm-4 col-xl-2 pl-0">
                                        <asp:DropDownList ID="AvailablePorts" runat="server" class="form-control btn btn-secondary dropdown-toggle" data-toggle="dropdown" style="height:auto;min-width:95px;text-transform: capitalize;" role="button" aria-haspopup="true" aria-expanded="s">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-xl-2 col-sm-2 col-12 pr-0">
                                    <asp:LinkButton ID="Set_Port" runat="server" 
                                        class="btn btn-primary float-right mt-1" onclick="Set_Port_Click" >Set Port<i class="fa fa-check ml-2" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                     </div>
                        </div>
                        </div>
                        <div class="row my-4" id="Edit_Profile">
                            <div class="col-xl-12">
                                <div class="card mb-3">
                                <div class="card-header form-inline">
                                    <div class="col-xl-12 pl-0">
                                        <i class="fa fa-pencil mr-1"></i>
                                        Edit Profile 
                                    </div>
                                </div>
                            <div class="card-body">
                            
                            <div class="form-inline col-xl-12 pl-0 mb-2">
                                <div class="input-group input-group-joined col-xl-8 col-sm-8 col-12 px-0">
                                    <asp:Label ID="Label4" runat="server" Text="Email" class="col-4 col-sm-3 col-xl-2 pl-0"></asp:Label>
                                    <asp:Label ID="Email" runat="server" class="col-8 col-sm-8 col-xl-8 text-secondary px-0"></asp:Label>
                                </div>
                                <div class="col-xl-4 col-sm-4 col-12 pr-0">
                                    <asp:LinkButton ID="Change_Email" runat="server" 
                                        class="btn btn-success float-right mt-1" onclick="Change_Email_Click" >Change<i class="fa fa-pencil-square ml-2" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </div>

                            <div  runat="server" id="Div_Change_Email" visible="false" class="p-2 border rounded">
                                <div class="form-inline col-xl-12 pl-0 mb-2">
                                        <asp:Label ID="Label6" runat="server" Text="Password" class="col-6 col-sm-6 col-xl-6 pl-0 small"></asp:Label>
                                        <asp:Label ID="Label7" runat="server" Text="New Email" class="col-6 col-sm-6 col-xl-6 px-0 small"></asp:Label>
                                </div>
                                <div class="form-inline col-xl-12 pl-0">
                                    <div class="form-group col-6 col-sm-6 col-xl-6 pl-0">
                                        <asp:TextBox class="form-control py-4 col-xl-12" id="Check_Password" type="password" placeholder="Enter Password"  runat="server"  onkeydown="limit(this,20);" onkeyup="limit(this,20);"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-6 col-sm-6 col-xl-6 pl-0">
                                        <asp:TextBox class="form-control py-4 col-xl-12" id="Check_New_Email" type="email" placeholder="Enter New Email"  runat="server"  onkeydown="limit(this,50);" onkeyup="limit(this,50);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-inline col-xl-12 pl-0 mt-1 text-danger">
                                        <asp:Label ID="Alert_SE_Password" runat="server"  class="col-6 col-sm-6 col-xl-6 pl-0 small"></asp:Label>
                                        <asp:Label ID="Alert_SE_Email" runat="server" class="col-6 col-sm-6 col-xl-6 pl-0 small"></asp:Label>
                                </div>
                                <div class="col-xl-12 pr-0 text-right">
                                        <asp:LinkButton ID="Set_Email" runat="server" class="btn btn-primary my-2" onclick="Set_Email_Click" >Change Email<i class="fa fa-check ml-2" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </div>


                            <div class="form-inline col-xl-12 pl-0 my-2">
                                <div class="input-group input-group-joined col-xl-8 col-sm-8 col-12 pl-0">
                                    <asp:Label ID="Label5" runat="server" Text="Username" class="col-4 col-sm-3 col-xl-2 px-0"></asp:Label>
                                    <asp:Label ID="Username" runat="server" class="col-8 col-sm-8 col-xl-8 text-secondary px-0 text-capitalize"></asp:Label>
                                </div>
                                <div class="col-xl-4 col-sm-4 col-12 pr-0">
                                    <asp:LinkButton ID="Change_Username" runat="server" 
                                        class="btn btn-success float-right mt-1" onclick="Change_Username_Click" >Change<i class="fa fa-pencil-square ml-2" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </div>

                            <div  runat="server" id="Div_Change_Username" visible="false" class="p-2 border rounded">
                                <div class="form-inline col-xl-12 pl-0 mb-2">
                                        <asp:Label ID="Label10" runat="server" Text="Password" class="col-6 col-sm-6 col-xl-6 pl-0 small"></asp:Label>
                                        <asp:Label ID="Label11" runat="server" Text="New Username" class="col-6 col-sm-6 col-xl-6 px-0 small"></asp:Label>
                                </div>
                                <div class="form-inline col-xl-12 pl-0">
                                    <div class="form-group col-6 col-sm-6 col-xl-6 pl-0">
                                        <asp:TextBox class="form-control py-4 col-xl-12" id="Check_Password2" type="password" placeholder="Enter Password" runat="server" onkeydown="limit(this,20);" onkeyup="limit(this,20);"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-6 col-sm-6 col-xl-6 pl-0">
                                        <asp:TextBox class="form-control py-4 col-xl-12" id="Check_New_Username" type="text" placeholder="Enter New Username" runat="server" onkeydown="limit(this,20);" onkeyup="limit(this,20);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-inline col-xl-12 pl-0 mt-1 text-danger">
                                        <asp:Label ID="Alert_SU_Password" runat="server" class="col-6 col-sm-6 col-xl-6 pl-0 small"></asp:Label>
                                        <asp:Label ID="Alert_SU_Username" runat="server" class="col-6 col-sm-6 col-xl-6 pl-0 small"></asp:Label>
                                </div>
                                <div class="col-xl-12 pr-0 text-right">
                                        <asp:LinkButton ID="Set_Username" runat="server" class="btn btn-primary my-2" onclick="Set_Username_Click" >Change Username<i class="fa fa-check ml-2" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </div>


                            <div class="form-inline col-xl-12 pl-0 my-2">
                                <div class="input-group input-group-joined col-xl-8 col-sm-8 col-12 pl-0">
                                    <asp:Label ID="Label1" runat="server" Text="Password" class="col-4 col-sm-3 col-xl-2 px-0"></asp:Label>
                                    <asp:Label ID="Password" runat="server" class="col-8 col-sm-8 col-xl-8 text-secondary px-0"></asp:Label>
                                </div>
                                <div class="col-xl-4 col-sm-4 col-12 pr-0">
                                    <asp:LinkButton ID="Change_Password" runat="server" 
                                        class="btn btn-success float-right mt-1" onclick="Change_Password_Click" >Change<i class="fa fa-pencil-square ml-2" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </div>

                            <div  runat="server" id="Div_Change_Password" visible="false" class="p-2 border rounded">
                                <div class="form-inline col-xl-12 pl-0 mb-2">
                                        <asp:Label ID="Label13" runat="server" Text="Old Password" class="col-4 col-sm-4 col-xl-4 pl-0 small"></asp:Label>
                                        <asp:Label ID="Label14" runat="server" Text="New Password" class="col-4 col-sm-4 col-xl-4 px-0 small"></asp:Label>
                                        <asp:Label ID="Label16" runat="server" Text="Confirm Password" class="col-4 col-sm-4 col-xl-4 px-0 small"></asp:Label>
                                </div>
                                <div class="form-inline col-xl-12 pl-0">
                                    <div class="form-group col-4 col-sm-4 col-xl-4 pl-0">
                                        <asp:TextBox class="form-control py-4 col-xl-12" id="Old_Password" type="password" placeholder="Enter Old  Password"  runat="server" onkeydown="limit(this,20);" onkeyup="limit(this,20);"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-4 col-sm-4 col-xl-4 pl-0">
                                        <asp:TextBox class="form-control py-4 col-xl-12" id="New_Password" type="password" placeholder="Enter New Password"  runat="server" onkeydown="limit(this,20);" onkeyup="limit(this,20);"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-4 col-sm-4 col-xl-4 pl-0">
                                        <asp:TextBox class="form-control py-4 col-xl-12" id="Confirm_Password" type="password" placeholder="Confirm Password"  runat="server" onkeydown="limit(this,20);" onkeyup="limit(this,20);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-inline col-xl-12 pl-0 mt-1 text-danger">
                                        <asp:Label ID="Alert_SP_OPassword" runat="server" class="col-4 col-sm-4 col-xl-4 pl-0 small"></asp:Label>
                                        <asp:Label ID="Alert_SP_NPassword" runat="server" class="col-4 col-sm-4 col-xl-4 pl-0 small"></asp:Label>
                                        <asp:Label ID="Alert_SP_CPassword" runat="server" class="col-4 col-sm-4 col-xl-4 pl-0 small"></asp:Label>
                                </div>
                                <div class="col-xl-12 pr-0 text-right">
                                        <asp:LinkButton ID="Set_Password" runat="server" class="btn btn-primary my-2" onclick="Set_Password_Click" >Change Password<i class="fa fa-check ml-2" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </div>

                            </div>


                        </div>
                        </div>
                        </div>

                    </div>
</asp:Content>

