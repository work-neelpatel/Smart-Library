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

    protected void ChargesRemark_Change(object sender, EventArgs e)
    {

    }

    public void check()
    {
        con.Open();
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
                        cmd = new SqlCommand("select * from issue_return where BRFID = '" + data + "' and rtime is null", con);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            dr.Close();
                            Book_RFID.Text = data;
                            con.Close();
                            FillData();
                        }
                        else
                        {
                            dr.Close();
                            Alert_BookIssue.Visible = true;
                            body.Visible = false;
                            con.Close();
                        }
                    }
                    else
                    {
                        dr.Close();
                        Alert_Match.Visible = true;
                        body.Visible = false;
                        con.Close();
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

    public void FillData()
    {
        con.Open();
        cmd = new SqlCommand("select charge, remark from charges", con);
        Charges_Remark.DataSource = cmd.ExecuteReader();
        Charges_Remark.DataTextField = "remark";
        Charges_Remark.DataValueField = "charge";
        Charges_Remark.DataBind();
        Charges_Remark.Items.Add("None");
        con.Close();

        con.Open();
        cmd = new SqlCommand("SELECT b.Name AS Book, b.ISBN, s.Enrollment_No, s.DOJ, s.FName, s.LName, s.Image, s.Email, su.Name AS Subject, su.Semester,ir.retime, ir.IFrom, ir.ITime, (SELECT Name FROM Fields WHERE   (Id = su.Field)) AS Book_Field, (SELECT Name FROM      Fields AS Fields_1 WHERE   (Id = s.Field)) AS Student_Field FROM     book AS b INNER JOIN subject AS su ON su.Id = b.Subject INNER JOIN issue_return AS ir ON ir.BRFID = b.RFID INNER JOIN student AS s ON s.RFID = ir.SRFID WHERE  (b.RFID = '"+Book_RFID.Text+"') ORDER BY ir.ITime DESC, ir.RTime DESC", con);
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
                Return_To.Text = AdminInfo["Username"].ToString();
            }
            else
                Response.Redirect("Login.aspx");

            Return_Date.Text = DateTime.Now.ToString("MMM dd ,yyyy");
            Issue_Date.Text = Convert.ToDateTime(dr["itime"]).ToString("MMM dd,yyyy");
            Issue_From.Text = dr["ifrom"].ToString();

            TimeSpan DateDiff;
            if (dr["retime"].ToString() != "")
                DateDiff = DateTime.Now - Convert.ToDateTime(dr["retime"]).AddDays(2);
            else
                DateDiff = DateTime.Now - Convert.ToDateTime(dr["itime"]).AddDays(7);

            if (DateDiff.Days > 0)
            {
                Late_Days.ForeColor = System.Drawing.Color.Red;
                if (DateDiff.TotalDays == (DateDiff.Days + 0.00))
                {
                    Charges_Remark.SelectedIndex = 0;
                    Charges.Text = (DateDiff.Days * Convert.ToInt16(Charges_Remark.SelectedValue)).ToString();
                    Late_Days.Text = DateDiff.Days.ToString();
                }
                else
                {
                    Charges_Remark.SelectedIndex = 0;
                    Charges.Text = ((DateDiff.Days + 1) * Convert.ToInt16(Charges_Remark.SelectedValue)).ToString();
                    Late_Days.Text = Convert.ToInt16(DateDiff.Days + 1).ToString();
                }
            }
            else
            {
                Late_Days.ForeColor = System.Drawing.Color.Green;
                Charges_Remark.SelectedIndex = 2;
                Charges.Text = "0";
                Late_Days.Text = "-";
//                Late_Days.Text = DateDiff.TotalDays == (Convert.ToInt16(DateDiff.Days) + 0.00) ? (Math.Abs(DateDiff.Days)).ToString() : (Math.Abs(Convert.ToInt16(DateDiff.Days)) + 1).ToString();
            }
        }
        dr.Close();
        con.Close();
    }

    protected void ReturnBook_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("update issue_return set rtime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' , rto = '" + Admin_Id.Text + "'  , charge = " + Charges.Text + " , charge_remark = " + Charges_Remark.SelectedIndex + " where BRFID = '" + Book_RFID.Text + "' and rtime IS NULL", con);
        cmd.ExecuteNonQuery();

        cmd = new SqlCommand("update br_connection set available = 1 where RFID = '" + Book_RFID.Text + "'", con);
        cmd.ExecuteNonQuery();

        ReturnBook.Visible = false;
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
            msg.Body = "Dear, " + Student_Name.InnerHtml + " Your Returned Book successfully." + "\nBook Name :" + Book_Name.InnerText + "\nReturn To : " + Return_To.Text + "\nReturn Time : " + DateTime.Now.ToString("MMMM dd ,yyyy h:mm tt") + "\nIssue Time : " + Issue_Date.Text + "\nCharges : RS." + Charges.Text + "\ncharges Remark : " + Charges_Remark.SelectedValue + "\nLate Days : " + Late_Days.Text;
            client.Send(msg);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Due to some system error Mail can't send to Student!');", true);
        }

    }

    protected void Charges_Remark_Change(object sender, EventArgs e)
    {
        if (Charges_Remark.SelectedIndex == 0)
            FillData();
        else if (Charges_Remark.SelectedIndex == 1)
            Charges.Text = (Convert.ToInt16(Charges.Text) + Convert.ToInt16(Charges_Remark.SelectedValue)).ToString();
        else
            Charges.Text = "0";
    }

    protected void COMPort_Change(object sender, EventArgs e)
    {
        Response.Cookies["COMPort"].Value = AvailablePorts.SelectedItem.ToString();
    }
}