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
    }

    protected void Generate_Report_Click(object sender, EventArgs e)
    {

    }

    protected void Return_Book_Click(object sender, EventArgs e)
    {
        con.Open();
        port.ReadTimeout = 5000;
        try
        {
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
                    cmd = new SqlCommand("select * from Books_on_Read where BRFID = '" + data + "' and rtime is null", con);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        Book_RFID.Text = data;
                        Student_RFID.Text = "";
                        con.Close();
                        body.Visible = false;
                        body2.Visible = true;
                        Transaction_Type.InnerText = "Return Book";
                        ReturnBook.Visible = true;
                        FillData();
                    }
                    else
                    {
                        dr.Close();
                        Alert_BookIssue.Visible = true;
                        body2.Visible = body.Visible = false;
                        con.Close();
                    }
                }
                else
                {
                    dr.Close();
                    Alert_Match.Visible = true;
                    body2.Visible = body.Visible = false;
                    con.Close();
                }
            }
            catch (TimeoutException)
            {
                port.Close();
                Alert_Timeout.Visible = true;
                body2.Visible = body.Visible = false;
            }
        }
        catch (TimeoutException)
        {
            port.Close();
            Alert_COM.Visible = true;
            body2.Visible = body.Visible = false;
        }
    }

    protected void Issue_Book_Click(object sender, EventArgs e)
    {
        if (con.State != System.Data.ConnectionState.Open)
            con.Open();

        if (Book_RFID.Text != "" && Student_RFID.Text != "")
        {
            con.Close();
            body.Visible = false;
            body2.Visible = true;
            Transaction_Type.InnerText = "Issue Book";
            IssueBook.Visible = true;
            FillData();
        }
        else
        {
            port.ReadTimeout = 5000;
            try
            {
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
                        cmd = new SqlCommand("select * from book where RFID = '" + data + "' and Copies > Issued_Copies", con);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            dr.Close();
                            Book_RFID.Text = data;
                            Issue_Book_Click(null,null);
                        }
                        else
                        {
                            dr.Close();
                            Alert_BookReturn.Visible = true;
                            body2.Visible = body.Visible = false;
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
                            Student_RFID.Text = data;
                            Issue_Book_Click(null, null);
                        }
                        else
                        {
                            Alert_Match.Visible = true;
                            body2.Visible = body.Visible = false;
                        }
                        dr.Close();
                    }
                }
                catch (TimeoutException)
                {
                    port.Close();
                    Alert_Timeout.Visible = true;
                    body2.Visible = body.Visible = false;
                }
            }
            catch (TimeoutException)
            {
                port.Close();
                Alert_COM.Visible = true;
                body2.Visible = body.Visible = false;
            }
        }
    }

    protected void IssueBook_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("insert into Books_on_Read (BRFID,SRFID,itime,ifrom) values('" + Book_RFID.Text + "','" + Student_RFID.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + Admin_Id.Text + "')", con);
        cmd.ExecuteNonQuery();

        cmd = new SqlCommand("update book set Issued_Copies += 1 where RFID = '" + Book_RFID.Text + "'", con);
        cmd.ExecuteNonQuery();

        IssueBook.Visible = false;
        Alert_Success.Visible = true;
        con.Close();
    }

    protected void ReturnBook_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("update Books_on_Read set rtime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' , rto = '" + Admin_Id.Text + "'  where BRFID = '" + Book_RFID.Text + "' and rtime IS NULL", con);
        cmd.ExecuteNonQuery();

        cmd = new SqlCommand("update book set Issued_Copies -= 1 where RFID = '" + Book_RFID.Text + "'", con);
        cmd.ExecuteNonQuery();

        ReturnBook.Visible = false;
        Alert_Success.Visible = true;
        con.Close();
    }

    public void FillData()
    {
        con.Open();
        if (Student_RFID.Text != "") 
            cmd = new SqlCommand("SELECT b.Name AS Book, b.ISBN, s.Enrollment_No, s.DOJ, s.FName, s.LName, su.Name AS Subject, su.Semester,(SELECT Name FROM Fields WHERE   (Id = su.Field)) AS Book_Field, (SELECT Name FROM Fields AS Fields_1 WHERE (Id = s.Field)) AS Student_Field FROM book AS b INNER JOIN subject AS su ON su.Id = b.Subject INNER JOIN student AS s ON s.RFID = '" + Student_RFID.Text + "' where b.RFID = '" + Book_RFID.Text + "' ", con);
        else
            cmd = new SqlCommand("SELECT b.Name AS Book, b.ISBN, s.Enrollment_No, s.DOJ, s.FName, s.LName, su.Name AS Subject, su.Semester,(SELECT Name FROM Fields WHERE   (Id = su.Field)) AS Book_Field, (SELECT Name FROM Fields AS Fields_1 WHERE (Id = s.Field)) AS Student_Field FROM book AS b INNER JOIN subject AS su ON su.Id = b.Subject INNER JOIN Books_on_Read as br on br.BRFID = b.RFID INNER JOIN student AS s ON s.RFID = br.SRFID where br.BRFID = '" + Book_RFID.Text + "' ", con);

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

            HttpCookie AdminInfo = Request.Cookies["AdminInfo"];
            if (AdminInfo != null)
            {
                Admin_Id.Text = AdminInfo["Id"].ToString();
            }
        }
        dr.Close();
        con.Close();
    }
}