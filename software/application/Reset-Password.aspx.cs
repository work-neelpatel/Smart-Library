using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\asp practice\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void Reset_Password_Click(object sender, EventArgs e)
    {
        if (New_Password.Text == Confirm_Password.Text)
        {
            Alert_Match_Password.Visible = false;
            con.Open();
            cmd = new SqlCommand("update admin set Password = '" + Confirm_Password.Text + "' where Email = '" + Email.Text + "'", con);
            cmd.ExecuteReader();
            con.Close();
            Alert_Password_Changed.Visible = true;
            Reset_Password_btn.Visible = false;
        }
        else
        {
            Alert_Match_Password.Visible = true;
            Confirm_Password.Focus();
        }
    }

    protected void Send_OTP_btn_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select Username from admin where Email = '" + Email.Text + "'", con);
        dr = cmd.ExecuteReader();
        if (!dr.Read())
        {
            Alert_Invailed_Email.Visible = true;
        }
        else
        {
            try
            {
                //genrate OTP
                Random r = new Random();
                int otp_no;
                otp_no = r.Next(100000, 999999);
                Context.Session["OTP"] = otp_no;

                //send otp on mail
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("ljsmartlibrary@gmail.com", "LJSmartLibrary@20");
                MailMessage msg = new MailMessage();
                msg.To.Add(Email.Text);
                msg.From = new MailAddress("ljsmartlibrary@gmail.com", "LJSmartLibrary@20");
                msg.Subject = "Forgot Password!";
                msg.IsBodyHtml = true;
                msg.Body = "Dear " + dr["Username"] + ", there is a Reset Password Request from your account.<br>Check it, Your OTP is <b>" + otp_no + "</b><br><b>Note:This OTO is vailed for only 5mins after that it will not work.</b>";
                //msg.Body = "<!DOCTYPE html><html lang='en'><head>  <title>Email test</title>  <meta charset='utf-8'>  <meta name='viewport' content='width=device-width, initial-scale=1'>  <style type='text/css'>    #heading{      color: #000;             font-size: 25px;        padding:10px;        font-family: Calibri;    }    #title{      color:#000;    }    #md{      color: #36227a;          }    #content{      text-align: justify;      padding:20px;      padding-top: 0px;    }    body{      font-family: Calibri Light;      font-size: 20px;    }    table,hr{      border:1px solid grey;      border-radius: 5px;          }  </style></head>  <body>      <table align='center'>        <tr>            <th><div id='heading'><span id='md'>L</span>J S<span id='md'>MAR</span>T LIB<span id='md'>RARY</span><br><span id='title'><br>Reset Password</span><hr width='80%'></div></th>        </tr>          <tr>          <td id='content'>We recieved a request to reset the password associated with <span style='font-weight: bold;'>" + Session["Email"] + "</span>.</td>        </tr>       <tr>          <td id='content'>Here is OTP <span style='font-weight: bold;'>" + otp + "</span> for reset your password to something more memorable.</td>        </tr>      </table>  </body></html>";
                client.Send(msg);
                HttpCookie OTPDetails = new HttpCookie("OTP",otp_no.ToString());
                OTPDetails.Expires = DateTime.Now.AddMinutes(5);
                Response.Cookies.Add(OTPDetails);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please try again Later!');window.location ='Login.aspx';", true);
            }

            Alert_Expired_OTP.Visible = false;
            Alert_Invailed_Email.Visible = false;
            body1.Visible = false;
            body2.Visible = true;
            Email_lbl.InnerText = Email.Text;
            OTP.Focus();
        }
        con.Close();
    }

    protected void OTP_TextChange(object sender, EventArgs e)
    {
        HttpCookie OTPDetails = Request.Cookies["OTP"];
        if (OTPDetails != null)
        {
            if (OTP.Text != OTPDetails.Value)
            {
                Alert_Invailed_OTP.Visible = true;
                New_Password.ReadOnly = Confirm_Password.ReadOnly = true;
                Reset_Password_btn.Visible = false;
                OTP.Focus();
            }
            else
            {
                Alert_Invailed_OTP.Visible = false;
                New_Password.ReadOnly = Confirm_Password.ReadOnly = false;
                Reset_Password_btn.Visible = true;
                OTP.ReadOnly = true;
                OTPDetails.Expires = DateTime.Now;
                New_Password.Focus();
            }
        }
        else
        {
            OTP.Text = New_Password.Text = Confirm_Password.Text = "";
            body2.Visible = false;
            body1.Visible = true;
            Alert_Expired_OTP.Visible = true;
        }
    }

}