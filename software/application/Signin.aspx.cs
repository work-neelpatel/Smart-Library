using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\SSIP PROJECT\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
    SqlCommand cmd;
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Username.Text != "")
        {
            Username_label.InnerText = "";
            Username.Attributes["style"] = "border-color:#e6e6e6";
            Username.Attributes["style"] = "color:black";
        }
        else
            Username_label.InnerText = "Username";

        if (Password.Text != "")
        {
            Password_label.InnerText = "";
            Password.Attributes["style"] = "border-color:#e6e6e6";
            Password.Attributes["style"] = "color:black";
        }
        else
            Password_label.InnerText = "Password";

        Session.RemoveAll();
    }

    protected void ForgotPassword_Click(object sender, EventArgs e)
    {
        if (Username.Text == "")
        {
            Username.Attributes["style"] = "border-color:red";
            Password.Attributes["style"] = "border-color:red";
            Password.Attributes["style"] = "color:black";
            //alert_resetpass.Visible = true;
            //alert.Visible = false;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "invailed_user();", true);
        }
        else
        {
            con.Open();
            cmd = new SqlCommand("select * from admin where username = '"+Username.Text+"' ",con);
            dr = cmd.ExecuteReader();
            if (dr.Read()){
                Session["Email"] = dr["email"].ToString();
                dr.Close();
                con.Close();
                try
                {
                    Random r = new Random();
                    int otp;
                    otp = r.Next(100000, 999999);
                    Context.Session["OTP"] = otp;
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.Timeout = 10000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("smartlibrary80@gmail.com", "smartlibrary2019");
                    MailMessage msg = new MailMessage();
                    msg.To.Add(Session["email"].ToString());
                    msg.From = new MailAddress("smartlibrary80@gmail.com");
                    msg.Subject = "Reset Password Request";
                    msg.IsBodyHtml = true;
                    //                    msg.Body = @"<table align='center' width='40%' border='1px solid grey' class='table' style='border-radius: 10px; font-family: Calibri' cellspacing='20px'><tr><td style='border:none;'><table width='100%' border='0px'><tr><th style='font-size: 25px'><span style='color: #4e73df'>LJ </span><span style='color: #5a5c69'>S</span><span style='color: #858796'>m</span><span style='color: #e74a3b'>a</span><span style='color: #36b9cc'>r</span><span style='color: #1cc88a'>t </span><span style='color: #4e73df'>Library</span><br><br></th></tr><tr><th style='font-size: 25px'>Reset password</th></tr><tr><td align='center'>" + email.Text + "</td></tr><tr><td align='center'></td></tr><tr><td><br><hr style='border: 1px solid grey;width: 90%'><br></td></tr><tr><td align='center'>We recieved a request to reset the password associated with <span style='font-weight: bold;'>neelp8501@gmail.com</span>.</td></tr><tr><td align='center'>Please enter the OTP <span style='font-weight: bold;'>" + n + "</span> in the Forgot password screen and reset your password to something more memorable.</td></tr></table></td></tr></table>";
                    msg.Body = "<!DOCTYPE html><html lang='en'><head>  <title>Email test</title>  <meta charset='utf-8'>  <meta name='viewport' content='width=device-width, initial-scale=1'>  <style type='text/css'>    #heading{      color: #000;             font-size: 25px;        padding:10px;        font-family: Calibri;    }    #title{      color:#000;    }    #md{      color: #36227a;          }    #content{      text-align: justify;      padding:20px;      padding-top: 0px;    }    body{      font-family: Calibri Light;      font-size: 20px;    }    table,hr{      border:1px solid grey;      border-radius: 5px;          }  </style></head>  <body>      <table align='center'>        <tr>            <th><div id='heading'><span id='md'>L</span>J S<span id='md'>MAR</span>T LIB<span id='md'>RARY</span><br><span id='title'><br>Reset Password</span><hr width='80%'></div></th>        </tr>          <tr>          <td id='content'>We recieved a request to reset the password associated with <span style='font-weight: bold;'>"+Session["Email"]+"</span>.</td>        </tr>       <tr>          <td id='content'>Here is OTP <span style='font-weight: bold;'>"+otp+"</span> for reset your password to something more memorable.</td>        </tr>      </table>  </body></html>";
                    client.Send(msg);
                    Response.Redirect("ResetPassword");
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please try again Later!');window.location ='SignIn';", true);
                }
            }
            else {
                dr.Close();
                con.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "invailed_user();", true);
            }
        }
    }


    protected void SignIn(object sender, EventArgs e)
    {
        if (Username.Text != "" && Password.Text != "")
        {
            con.Open();
            cmd = new SqlCommand("select * from admin where username = '" + Username.Text + "' and password = '"+Password.Text+"' ",con);
            dr = cmd.ExecuteReader();
            if (dr.Read()){
                Session["Admin"] = Username.Text;
                Response.Redirect("Books");
            }
            else {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "wrong_uop();", true);
                Username.Attributes["style"] = "color:red";
                Password.Attributes["style"] = "color:red";
                //alert.Visible = true;
                //alert_resetpass.Visible = false;
            }
        }
        else
        {
            if (Username.Text == "")
                Username.Attributes["style"] = "border-color:red";
            if (Password.Text == "")
                Password.Attributes["style"] = "border-color:red";
        }
  }
}