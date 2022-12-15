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

public partial class Default2 : System.Web.UI.Page
{
    string Book;
    StringBuilder htmlTable = new StringBuilder();
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\SSIP PROJECT\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
    protected void Page_Load(object sender, EventArgs e)
    {
        Req_Send_text.Visible = false;
        Book = Request.QueryString["Book"];
        if (Book == null)
            Response.Redirect("Books");
        else
            FillData();
    }

    public void FillData()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT b.ISBN, b.RFID as bRFID, b.bname, s.RFID as sRFID, s.enrollno, s.fname, s.lname, s.email, ir.itime,ir.request,                       (SELECT COUNT(*) AS Expr1                        FROM      issue_return                        WHERE   (BRFID = b.RFID)) AS bcount,                       (SELECT COUNT(*) AS Expr1                        FROM      issue_return AS Expr4                        WHERE   (SRFID = s.RFID)) AS scount,                       (SELECT SUM(charges) AS Expr2                        FROM      issue_return AS Expr4                        WHERE   (SRFID = s.RFID)) AS ssum FROM     issue_return AS ir INNER JOIN                   book AS b ON b.RFID = '"+Book+"' AND ir.rtime IS NULL INNER JOIN                   student AS s ON ir.SRFID = s.RFID  ", con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            BRFID_lbl.Text = dr["bRFID"].ToString();
            ISBN_lbl.Text = dr["ISBN"].ToString();
            Bname_lbl.Text = dr["bname"].ToString();
            Tissue_lbl.Text = Convert.ToInt16(dr["bcount"]) > 1 ? dr["bcount"].ToString() + " times" : dr["bcount"].ToString() + " time";
            IssueTimes_lbl.Text = Convert.ToDateTime(dr["itime"]).ToString("MMMM dd,yyyy HH:MM");
            TimeSpan t = DateTime.Now - Convert.ToDateTime(dr["itime"]).AddDays(7);
            if (t.Days > 0)
            {
                DaystR_lbl.Attributes["class"] = "pl-3 text-danger";
                DaystR_lbl.Text = t.Days.ToString();
            }
            else
            {
                DaystR_lbl.Attributes["class"] = "pl-3 text-success";
                DaystR_lbl.Text = Math.Abs(t.Days).ToString();
            }
            SRFID_lbl.Text = dr["sRFID"].ToString();
            Enroll_lbl.Text = dr["enrollno"].ToString();
            Sname_lbl.Text = dr["fname"].ToString() + " " + dr["lname"].ToString();
            Email_lbl.Text = dr["email"].ToString();
            Ibooks_lbl.Text = dr["scount"].ToString();
            if (dr["ssum"].ToString() == "0")
                Tcharges_lbl.Attributes["class"] = "pl-3 text-success";
            else
                Tcharges_lbl.Attributes["class"] = "pl-3 text-danger";
            Tcharges_lbl.Text = "RS. "+dr["ssum"].ToString();
            if (dr["request"].ToString() != "")
            {
                SendRequest_btn.Visible = false;
                Req_Send_text.Visible = true;
                TimeSpan t2 = DateTime.Now - Convert.ToDateTime(dr["request"]);
                Req_Send_text.Attributes["class"] = Convert.ToUInt16(t2.Days) > 2 ? "header-title text-danger" : "header-title text-success";
                Req_Send_text.InnerText += Convert.ToInt16(t2.Days) > 0 ? (Convert.ToInt16(t2.Days) > 1 ? " before " + t2.Days + " days." : " tomorrow.") : "today.";
            }
        }
        dr.Close();
        con.Close();
    }

    protected void SendRequest(object sender, EventArgs e)
    {
        try
        {
            SmtpClient clint = new SmtpClient("smtp.gmail.com", 587);
            clint.EnableSsl = true;
            clint.Timeout = 10000;
            clint.DeliveryMethod = SmtpDeliveryMethod.Network;
            clint.UseDefaultCredentials = false;
            clint.Credentials = new NetworkCredential("smartlibrary80@gmail.com", "SmartLibrary@80");
            MailMessage msg = new MailMessage();
            msg.To.Add(Email_lbl.Text);
            msg.From = new MailAddress("smartlibrary80@gmail.com");
            msg.Subject = "Request to Return Book from LJ Smart Library";
            string Due_date = (DateTime.Now.AddDays(7)).ToString("MMMM dd ,yyyy");
            msg.IsBodyHtml = true;
            msg.Body = "Sorry to say, but we need  back your last issued book "+Bname_lbl.Text+" for some reason.<br> Please Return book in library and take it after some time.";
            msg.Body += "<br><b>Note: If you don't return book in 2 days we need to take some strict steps on you. and Also take charges.</b>";
            clint.Send(msg);

            con.Open();
            SqlCommand cmd = new SqlCommand("update issue_return set request='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where BRFID='" + BRFID_lbl.Text + "' and rtime is null", con);
            SqlDataReader dr = cmd.ExecuteReader();
            con.Close();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "success('Request send','Books   ');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Try this after some time','Books');", true);
        }
    }
}