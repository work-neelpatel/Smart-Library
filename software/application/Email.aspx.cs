using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

public partial class _Default : System.Web.UI.Page
{
    string name;
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\SSIP PROJECT\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
    protected void Page_Load(object sender, EventArgs e)
    {
        EmailAddress.Visible = false;
    }
    protected void Mail_Send(object sender, EventArgs e)
    {
        try
        {
            SmtpClient clint = new SmtpClient("smtp.gmail.com", 587);
            clint.EnableSsl = true;
            clint.Timeout = 10000;
            clint.DeliveryMethod = SmtpDeliveryMethod.Network;
            clint.UseDefaultCredentials = false;
            clint.Credentials = new NetworkCredential("smartlibrary80@gmail.com", "smartlibrary2019");
            MailMessage msg = new MailMessage();
            msg.To.Add(Email.Text);
            msg.From = new MailAddress("smartlibrary80@gmail.com");
            msg.Subject = "Message from L.J. Smart Library about " + Subject.Text;
            string Due_date = (DateTime.Now.AddDays(7)).ToString("MMMM dd ,yyyy");
            msg.Body = Message.InnerText;
            clint.Send(msg);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "success('Email send','Email');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Try this after some time','Books');", true);
        }
    }
    protected void Enroll_TextChanged(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT fname,lname,email from student where enrollno = '"+Enroll.Text+"'", con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            EmailAddress.Visible = true;
            Email.Text = dr["email"].ToString();
            name = dr["fname"].ToString() + " " + dr["lname"].ToString();
        }
        else
        {
            Enroll.Attributes["style"] = "border-color:red";
            Enroll.Text = "";
        }
        con.Close();
    }
}