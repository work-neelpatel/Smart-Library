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
    string Book, Student;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("Book");
            Session.Remove("Student");
            check();
        }
    }
    
    public void check()
    {
        if (HttpContext.Current.Session["Book"] != null && HttpContext.Current.Session["Student"] != null)
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
                        con.Close();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Book needs to Return First','Transactions');", true);
                    }
                    else
                    {
                        dr2.Close();
                        Session["Book"] = data;
                        con.Close();
                        check();
                    }
                }
                else
                {
                    dr.Close();
                    string sqlselect2 = "select * from student where RFID = '" + data + "'";
                    SqlCommand cmd2 = new SqlCommand(sqlselect2, con);
                    SqlDataReader dr3 = cmd2.ExecuteReader();
                    if (dr3.Read())
                    {
                        dr3.Close();
                        string sqlselect3 = "select * from issue_return where SRFID = '" + data + "' and rtime is null";
                        SqlCommand cmd3 = new SqlCommand(sqlselect3, con);
                        SqlDataReader dr4 = cmd3.ExecuteReader();
                        if (dr4.Read())
                        {
                            dr4.Close();
                            Session.Remove("Book");
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('One Student can issue one Book at time','Books');", true);
                        }
                        else
                        {
                            dr4.Close();
                            Session["Student"] = data;
                            con.Close();
                            check();
                        }
                    }
                    else 
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('ID does not match','Books');", true);
                    }
                    dr3.Close();
                }
            }
            catch (TimeoutException)
            {
                port.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Data Read Timeout','IssueBook');", true);
            }
        }
    }

    void assign()
    {
        if (HttpContext.Current.Session["Admin"] != null)
        {
            IFrom_lbl.Text = Session["Admin"].ToString();
            ITime_lbl.Text = DateTime.Now.ToString("MMMM dd ,yyyy h:mm tt");
            EReturnDate_lbl.Text = DateTime.Now.AddDays(7).ToString("MMMM dd ,yyyy");
            if (HttpContext.Current.Session["Book"] != null && HttpContext.Current.Session["Student"] != null)
            {
                BRFID_lbl.Text = Session["Book"].ToString();
                SRFID_lbl.Text = Session["Student"].ToString();
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('ID Problem','Books');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Admin Data Problem','Signin');", true);

        con.Open();
        string sql = "SELECT b.bname,s2.semester, b.edition, b.ISBN, s.enrollno,s.DOJ, s.fname, s.lname, s.mobile, s.email, s.imgsrc, s2.sname, p.pname FROM     book AS b INNER JOIN subject AS s2 ON s2.id = b.subject INNER JOIN publisher AS p ON p.id = b.publisher INNER JOIN student AS s ON s.RFID = '" + SRFID_lbl.Text + "' and  (b.RFID = '" + BRFID_lbl.Text + "')";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            Bname_lbl.Text = (dr["bname"].ToString());
            ISBN_lbl.Text = (dr["ISBN"].ToString());
            Subject_lbl.Text = (dr["sname"].ToString());
            Bsem_lbl.Text = (dr["semester"].ToString());
            Sname_lbl.Text = (dr["fname"].ToString() + " " + dr["lname"].ToString());
            Email_lbl.Text = (dr["email"].ToString());
            Ssem_lbl.Text = ((FindMonths(dr["DOJ"].ToString()) / 6) + 1).ToString();
            Enroll_lbl.Text = (dr["enrollno"].ToString());
            Stu_Image.ImageUrl = dr["imgsrc"].ToString();
        }
        dr.Close();
        con.Close();
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

    protected void IssueBook(object sender, EventArgs e)
    {
        con.Open();
            SqlCommand cmd = new SqlCommand("insert into issue_return (BRFID,SRFID,itime,ifrom) values('" + BRFID_lbl.Text + "','" + SRFID_lbl.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + IFrom_lbl.Text + "')", con);
            cmd.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand("update book set available = 0 where RFID = '" + BRFID_lbl.Text + "'", con);
            cmd2.ExecuteNonQuery();

            con.Close();
            Session.Remove("Book");
            Session.Remove("Student");
            try
            {
                SmtpClient clint = new SmtpClient("smtp.gmail.com", 587);
                clint.EnableSsl = true;
                clint.Timeout = 10000;
                clint.DeliveryMethod = SmtpDeliveryMethod.Network;
                clint.UseDefaultCredentials = false;
                clint.Credentials = new NetworkCredential("smartlibrary80@gmail.com", "SmartLibrary@80");
                MailMessage msg = new MailMessage();
                string note = "\nNOTE : If Book due date has passed away you have to pay extra 5RS/day.";
                msg.To.Add(Email_lbl.Text);
                msg.From = new MailAddress("smartlibrary80@gmail.com");
                msg.Subject = "Book Transaction With L.J. Smart Library";
                string Due_date = (DateTime.Now.AddDays(7)).ToString("MMMM dd ,yyyy");
                msg.Body = "Dear, " + Sname_lbl.Text + " Your book successfully Issued." + "\nBook Name :" + Bname_lbl.Text + "\nIssue From : " + IFrom_lbl.Text + "\nIssue Time : " + ITime_lbl.Text + "\nDue date : " + Due_date + note;
                clint.Send(msg);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "success('Transacntion Successfull','Transactions');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex + "');window.location ='Transactions';", true);
            }
    }

}