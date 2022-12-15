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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["OTP"] == null && HttpContext.Current.Session["Email"] == null)
            Response.Redirect("SignIn");
        else
        {
            if (OTP.Text != "")
            {
                OTP_label.InnerText = "";
                OTP.Attributes["style"] = "border-color:#e6e6e6";
                OTP.Attributes["style"] = "color:black";
            }
            else
                OTP_label.InnerText = "OTP";

            if (New_pass.Text != "")
            {
                Newpass_label.InnerText = "";
                New_pass.Attributes["style"] = "border-color:#e6e6e6";
                New_pass.Attributes["style"] = "color:black";
            }
            else
                Newpass_label.InnerText = "New Password";

            if (Confirm_pass.Text != "")
            {
                Confirmpass_label.InnerText = "";
                Confirm_pass.Attributes["style"] = "border-color:#e6e6e6";
                Confirm_pass.Attributes["style"] = "color:black";
            }
            else
                Confirmpass_label.InnerText = "Confirm Password";

            email.InnerHtml = Session["Email"].ToString();
        }
//            alert_success.Visible = alert_OTP.Visible = alert_match.Visible = false;
    }

    protected void Reset_Password(object sender, EventArgs e)
    {
        if (OTP.Text != "" && New_pass.Text != "" && Confirm_pass.Text != "")
        {
            if (New_pass.Text != Confirm_pass.Text)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "pass_match();", true);
                /*alert_match.Visible = true;
                alert_OTP.Visible = alert_success.Visible = false;*/
                Confirm_pass.Attributes["style"] = "color:red";
                New_pass.Attributes["style"] = "color:red";
            }
            else
            {
                if (OTP.Text == Session["OTP"].ToString())
                {
                    con.Open();
                    cmd = new SqlCommand("update admin set password='"+New_pass.Text+"' where email='"+email.InnerHtml+"'",con);
                    cmd.ExecuteNonQuery();
                    con.Close();
/*                    try
                    {
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                        client.EnableSsl = true;
                        client.Timeout = 10000;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential("smartlibrary80@gmail.com", "smartlibrary2019");
                        MailMessage msg = new MailMessage();
                        msg.To.Add(Session["Email"].ToString());
                        msg.From = new MailAddress("smartlibrary80@gmail.com");
                        msg.Subject = "Security alert";
                        msg.IsBodyHtml = true;
                        //                    msg.Body = @"<table align='center' width='40%' border='1px solid grey' class='table' style='border-radius: 10px; font-family: Calibri' cellspacing='20px'><tr><td style='border:none;'><table width='100%' border='0px'><tr><th style='font-size: 25px'><span style='color: #4e73df'>LJ </span><span style='color: #5a5c69'>S</span><span style='color: #858796'>m</span><span style='color: #e74a3b'>a</span><span style='color: #36b9cc'>r</span><span style='color: #1cc88a'>t </span><span style='color: #4e73df'>Library</span><br><br></th></tr><tr><th style='font-size: 25px'>Your password was changed</th></tr><tr><td align='center'>"+Session["email"].ToString()+"</td></tr><tr><td align='center'></td></tr><tr><td><br><hr style='border: 1px solid grey;width: 90%'><br></td></tr><tr><td align='center'>The Password for your Account <span style='font-weight: bold;'>"+Session["email"].ToString()+"</span> was changed.</td></tr><tr><td align='center'>If you didn't change it, you shouuld recover your account.</td></tr></table></td></tr></table>";
                        msg.Body = "<!DOCTYPE html><html lang='en'><head>  <title>Email test</title>  <meta charset='utf-8'>  <meta name='viewport' content='width=device-width, initial-scale=1'>  <style type='text/css'>    #heading{      color: #000;             font-size: 25px;        padding:10px;        font-family: Calibri;    }    #title{      color:#000;    }    #md{      color: #36227a;          }    #content{      text-align: justify;      padding:20px;      padding-top: 0px;    }    body{      font-family: Calibri Light;      font-size: 20px;    }    table,hr{      border:1px solid grey;      border-radius: 5px;          }  </style></head>  <body>      <table align='center'>        <tr>            <th><div id='heading'><span id='md'>L</span>J S<span id='md'>MAR</span>T LIB<span id='md'>RARY</span><br><span id='title'><br>Your Password was changed</span><hr width='80%'></div></th>        </tr>          <tr>          <td id='content'>Your password for your account <span style='font-weight: bold;'>"+Session["Email"].ToString()+"</span> was changed.</td>        </tr>       <tr>          <td id='content'>If you did'nt change it, you should recover your account.</td>        </tr>      </table>  </body></html>";
/*                        client.Send(msg);
                    }
                    catch (Exception ex)
                    {
                    }
*/                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "success_changed();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "otp_match();", true);
                    /*                alert_success.Visible = true;
                                    alert_match.Visible = false;
                                    alert_OTP.Visible = false;*/
                    OTP.Attributes["style"] = "border-color:#e6e6e6";
                }
            }
        }
        else {
            if (OTP.Text == "")
                OTP.Attributes["style"] = "border-color:red";
            if (New_pass.Text == "")
                New_pass.Attributes["style"] = "border-color:red";
            if (Confirm_pass.Text == "")
                Confirm_pass.Attributes["style"] = "border-color:red";

//            alert_match.Visible = alert_OTP.Visible = alert_success.Visible = false;
        }
    }
}