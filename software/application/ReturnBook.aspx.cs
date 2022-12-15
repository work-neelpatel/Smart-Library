using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO.Ports;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

public partial class Default2 : System.Web.UI.Page
{
    static SerialPort port = new SerialPort("COM4", 9600);
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\SSIP PROJECT\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
    TimeSpan date_diff;
    string RBook, RStudent;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("RBook");
            check();
        }
    }

    public void check()
    {
        if (HttpContext.Current.Session["RBook"] != null)
        {
            assign();
        }
        else
        {
            try
            {
                port.ReadTimeout = 5000;
                port.Open();
                string data = port.ReadLine();
                data = data.Trim();
                port.Close();
                con.Open();

                string sqlselect = "select * from book where RFID = '" + data + "'";
                SqlCommand cmd = new SqlCommand(sqlselect, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    string sqlselect2 = "select * from book where RFID = '" + data + "' and available = 0 ";
                    SqlCommand cmd2 = new SqlCommand(sqlselect2, con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if ((dr2.Read()))
                    {
                        dr2.Close();
                        Session["RBook"] = data;
                        con.Close();
                        assign();
                    }
                    else
                    {
                        dr2.Close();
                        con.Close();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Book needs to Issue First','Books');", true);
                    }
                }
                else
                {
                    dr.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Invailed Book','Books');", true);
                }
            }
            catch (TimeoutException)
            {
                port.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Data Read Timeout','ReturnBook');", true);
            }
        }
    }

    void assign()
    {
        if (HttpContext.Current.Session["Admin"] != null)
        {
            Rto_lbl.Text = Session["Admin"].ToString();
            Rtime_lbl.Text = DateTime.Now.ToString("MMMM dd ,yyyy h:mm tt");
            if (HttpContext.Current.Session["RBook"] != null )
            {
                RFID_lbl.Text = Session["RBook"].ToString();
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('ID Problem','Books');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Admin Data Problem','Signin');", true);

        SqlCommand cmd = new SqlCommand("select * from charges", con);
        con.Open();
        cremark.DataSource = cmd.ExecuteReader();
        cremark.DataTextField = "remark";
        cremark.DataValueField = "charge";
        cremark.DataBind();
        con.Close();

        string sqlselect2 = "";
        con.Open();
        sqlselect2 = "SELECT student.DOJ , subject.semester , book.bname , book.ISBN, book.edition , student.email , p.pname , student.fname , student.lname , student.enrollno, student.mobile , subject.sname , ir.request ,  ir.ifrom , ir.itime  FROM     book INNER JOIN publisher  AS p ON book.publisher = p.id INNER JOIN issue_return AS ir ON ir.BRFID = book.RFID INNER JOIN subject ON subject.id = book.subject INNER JOIN  student ON student.RFID = ir.SRFID where ir.BRFID = '" + RFID_lbl.Text + "' and ir.rtime is null";
        SqlCommand cmd2 = new SqlCommand(sqlselect2, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        if (dr2.Read())
        {
            Bname_lbl.Text = (dr2["bname"].ToString());
            ISBN_lbl.Text = (dr2["ISBN"].ToString());
            Subject_lbl.Text = (dr2["sname"].ToString());
            Bsem_lbl.Text = (dr2["semester"].ToString());
            Sname_lbl.Text = (dr2["fname"].ToString() + " " + dr2["lname"].ToString());
            Enroll_lbl.Text = (dr2["enrollno"].ToString());
            Email_lbl.Text = (dr2["email"].ToString());
            Ssem_lbl.Text = ((FindMonths(dr2["DOJ"].ToString()) / 6) + 1).ToString();
            Ifrom_lbl.Text = (dr2["ifrom"].ToString());
            Itime_lbl.Text = Convert.ToDateTime(dr2["itime"]).ToString("MMMM dd ,yyyy h:mm tt");

            if (dr2["request"].ToString() != "")
            {
                date_diff = DateTime.Now - Convert.ToDateTime(dr2["request"]).AddDays(2);
                Est_rdate.ForeColor = System.Drawing.Color.Blue;
                Est_rdate.Text = Convert.ToDateTime(dr2["request"]).AddDays(2).ToString("MMMM dd ,yyyy");
            }
            else
            {
                date_diff = DateTime.Now - Convert.ToDateTime(dr2["itime"]).AddDays(7);
                Est_rdate.Text = (Convert.ToDateTime(dr2["itime"]).AddDays(7)).ToString("MMMM dd ,yyyy");
            }
            if (date_diff.Days > 0)
            {
                Ddays_lbl.ForeColor = System.Drawing.Color.Red;
                if (date_diff.TotalDays == (date_diff.Days + 0.00))
                {
                    cremark.SelectedIndex = 0;
                    Charges_txt.Text = (date_diff.Days * Convert.ToInt16(cremark.SelectedValue)).ToString();
                    Ddays_lbl.Text = date_diff.Days.ToString();
                }
                else
                {
                    cremark.SelectedIndex = 0;
                    Charges_txt.Text = ((date_diff.Days + 1) * Convert.ToInt16(cremark.SelectedValue)).ToString();
                    Ddays_lbl.Text = Convert.ToInt16(date_diff.Days + 1).ToString();
                }
            }
            else
            {
                Ddays_lbl.ForeColor = System.Drawing.Color.Green;
                cremark.SelectedIndex = 2;
                Charges_txt.Text = "0";
                Ddays_lbl.Text = date_diff.TotalDays == (Convert.ToInt16(date_diff.Days) + 0.00) ? (Math.Abs(date_diff.Days)).ToString() : (Math.Abs(Convert.ToInt16(date_diff.Days)) + 1).ToString();
            }
        }
        else
        {
            dr2.Close();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Data Problem','Books');", true);
        }
        dr2.Close();
        con.Close();
    }

    protected void ReturnBook(object sender, EventArgs e)
    {
        con.Open();
        string sql = "update issue_return set rtime = '" + Rtime_lbl.Text + "' , rto = '" + Session["Admin"].ToString() + "'  , charges = '" + Charges_txt.Text + "' , remark = '" + cremark.SelectedItem + "' where BRFID = '" + RFID_lbl.Text + "' and rtime IS NULL";
            SqlCommand cmd2 = new SqlCommand(sql, con);
            cmd2.ExecuteNonQuery();

            //update book available data
            SqlCommand cmd3 = new SqlCommand("update book set available = 1 where RFID = '" + RFID_lbl.Text + "' ", con);
            cmd3.ExecuteNonQuery();
            con.Close();
            Session.Remove("RBook");

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
                msg.Subject = "Book Transaction With L.J. Library";
                if (Charges_txt.Text == "0")
                    msg.Body = "Dear, " + Sname_lbl.Text + " Your book successfully Returned." + "\nBook Name :" + Bname_lbl.Text + "\nReturn To : " + Rto_lbl.Text + "\nReturn Time : " + DateTime.Now.ToString("MMMM dd ,yyyy h:mm tt") + "\nIssue Time : " + Rtime_lbl.Text + "";
                else
                    msg.Body = "Dear, " + Sname_lbl.Text + " Your book successfully Returned." + "\nBook Name :" + Bname_lbl.Text + "\nReturn To : " + Rto_lbl.Text + "\nReturn Time : " + DateTime.Now.ToString("MMMM dd ,yyyy h:mm tt") + "\nIssue Time : " + Rtime_lbl.Text + "\nCharges : RS." + Charges_txt.Text + "\ncharges Remark : " + cremark.SelectedValue + "\nDalayed Minutes : " + Ddays_lbl.Text;
                clint.Send(msg);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "success('Transacntion Successfull','Transactions');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex + "');window.location ='Transactions';", true);
            }
    }

    public int FindMonths(string DOJ)
    {
        DateTime edt = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
        DateTime sdt = Convert.ToDateTime(DOJ);
        int numMonths = 0;
        while (sdt < edt)
        {
            sdt = sdt.AddMonths(1);
            numMonths++;
        }
        return numMonths;
    }

    protected void Remark_Change(object sender, EventArgs e)
    {
        if (cremark.SelectedIndex == 0)
            assign();
        else if (cremark.SelectedIndex == 1)
            Charges_txt.Text = (Convert.ToInt16(Charges_txt.Text) + Convert.ToInt16(cremark.SelectedValue)).ToString();
        else
            Charges_txt.Text = "0";
    }
}