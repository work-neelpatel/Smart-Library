using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.IO.Ports;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    static SerialPort port = new SerialPort("COM4", 9600);
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\asp practice\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;
    SqlDataReader dr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            check();
    }

    public void check()
    {
        if (con.State != System.Data.ConnectionState.Open)
            con.Open();

        if (Book_RFID.Text != "" && Student_RFID.Text != "")
        {
            con.Close();
            FillData();
        }
        else
        {
            //add available ports         
            AvailablePorts.Items.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                AvailablePorts.Items.Add(s);
            }
            HttpCookie COMPort = Request.Cookies["COMPort"];
            if (COMPort != null && AvailablePorts.Items.Count != 0)
            {
                port.ReadTimeout = 5000;
                port.Open();
                try
                {
                    string data = port.ReadLine();
                    data = data.Trim();
                    port.Close();
                    cmd = new SqlCommand("select * from book where RFID = '" + data + "'", con);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        cmd = new SqlCommand("select * from br_connection where (select count(RFID) from br_connection where RFID = '" + data + "') > (select count(RFID) from br_connection where RFID = '" + data + "' and available = 0) ", con);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            dr.Close();
                            Book_RFID.Text = data;
                            check();
                        }
                        else
                        {
                            dr.Close();
                            Alert_BookReturn.Visible = true;
                            body.Visible = false; 
                            con.Close();
                        }
                    }
                    else
                    {
                        dr.Close();
                        cmd = new SqlCommand("select * from student where RFID = '" + data + "'", con);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            dr.Close();
                            cmd = new SqlCommand("select * from issue_return where SRFID = '" + data + "' and rtime is null", con);
                            dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                dr.Close();
                                Alert_StudentReturn.Visible = true;
                                body.Visible = false; 
                                con.Close();
                            }
                            else
                            {
                                dr.Close();
                                Student_RFID.Text = data;
                                check();
                            }
                        }
                        else
                        {
                            Alert_Match.Visible = true;
                            body.Visible = false;
                        }
                        dr.Close();
                    }
                }
                catch (TimeoutException)
                {
                    port.Close();
                    Alert_Timeout.Visible = true;
                    body.Visible = false;
                }
            }
            else
            {
                Alert_COM.Visible = true;
                body.Visible = false;
            }
        } 
    }

    public void FillData()
    {
        con.Open();
        cmd = new SqlCommand("SELECT b.Name AS Book, b.ISBN, s.Enrollment_No, s.DOJ, s.FName, s.LName, s.Image, s.Email, su.Name AS Subject, su.Semester,(SELECT Name FROM Fields WHERE   (Id = su.Field)) AS Book_Field, (SELECT Name FROM Fields AS Fields_1 WHERE (Id = s.Field)) AS Student_Field FROM book AS b INNER JOIN subject AS su ON su.Id = b.Subject INNER JOIN student AS s ON s.RFID = '" + Student_RFID.Text + "' AND b.RFID = '" + Book_RFID.Text + "' ", con);
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            Book_Name.InnerHtml = (dr["Book"].ToString());
            ISBN.Text = (dr["ISBN"].ToString());
            Subject.Text = (dr["subject"].ToString());
            Book_Semester.Text = (dr["semester"].ToString());
            Book_Field.Text = (dr["Book_Field"].ToString());

            Student_Name.InnerHtml = (dr["fname"].ToString() + " " + dr["lname"].ToString());
            Enrollment_No.Text = (dr["Enrollment_No"].ToString());
            Student_Field.Text = (dr["Student_Field"].ToString());
            if (File.Exists(Server.MapPath(dr["Image"].ToString())))
                Student_Image.ImageUrl = dr["Image"].ToString();
            else
                Student_Image.ImageUrl = "assets/img/Student-Profile.png";

            //find student semester from DOJ
            DateTime edt = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            DateTime sdt = Convert.ToDateTime(dr["DOJ"]);
            int numMonths = 0;
            while (sdt < edt)
            {
                sdt = sdt.AddMonths(1);
                numMonths++;
            }
            Student_Semester.Text = ((numMonths / 6) + 1).ToString();
            Student_Email.Text = dr["Email"].ToString();

            HttpCookie AdminInfo = Request.Cookies["AdminInfo"];
            if (AdminInfo != null)
            {
                Admin_Id.Text = AdminInfo["Id"].ToString();
                Issue_From.Text = AdminInfo["Username"].ToString();
            }
            else
                Response.Redirect("Login.aspx");

            Issue_Date.Text = DateTime.Now.ToString("MMMM dd ,yyyy");
            EstReturn_Date.Text = DateTime.Now.AddDays(7).ToString("MMMM dd ,yyyy");
        }
        dr.Close();
        con.Close();
    }

    protected void IssueBook_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("insert into Issue_Return (BRFID,SRFID,itime,ifrom) values('" + Book_RFID.Text + "','" + Student_RFID.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Admin_Id.Text + "')", con);
        cmd.ExecuteNonQuery();

        cmd = new SqlCommand("update br_connection set available = 0 where RFID = '" + Book_RFID.Text + "'", con);
        cmd.ExecuteNonQuery();

        IssueBook.Visible = false;
        Alert_Success.Visible = true;
        con.Close();

        try
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("ljsmartlibrary@gmail.com", "LJSmartLibrary@20");
            MailMessage msg = new MailMessage();
            msg.To.Add(Student_Email.Text);
            msg.From = new MailAddress("ljsmartlibrary@gmail.com", "LJSmartLibrary@20");
            msg.Subject = "Transaction on L.J. Smart Library";
            msg.IsBodyHtml = true;
            msg.Body = "Dear, " + Student_Name.InnerHtml + " Your Issued Book successfully." + "\nBook Name :" + Book_Name.InnerHtml + "\nIssue From : " + Issue_From.Text + "\nIssue Time : " + DateTime.Now.ToString("MMMM dd ,yyyy h:mm tt") + "\nDue date : " + DateTime.Now.AddDays(7).ToString("MMMM dd ,yyyy");
            client.Send(msg);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Due to some system error Mail can't send to Student!');", true);
        }

    }

    protected void COMPort_Change(object sender, EventArgs e)
    {
        Response.Cookies["COMPort"].Value = AvailablePorts.SelectedItem.ToString();
    }
}