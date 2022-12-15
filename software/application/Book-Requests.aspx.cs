using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\asp practice\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;
    SqlDataReader dr;

    StringBuilder Table = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
            FillTableData();
    }

    protected void Generate_Report_Click(object sender, EventArgs e)
    {

    }

    protected void ISBN_TextChange(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select ISBN from book where ISBN = '"+ISBN.Text+"'",con);
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            dr.Close();
            cmd = new SqlCommand("select b.Name, f.name as field, s.semester from book as b inner join subject as s on s.id=b.subject inner join fields as f on f.id=s.field where b.ISBN = '" + ISBN.Text + "' and (select count(RFID) from br_connection where ISBN = b.ISBN) = (select count(RFID) from br_connection where ISBN = b.ISBN and available = 0)", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                cmd = new SqlCommand("select ISBN from book_requests where ISBN != '" + ISBN.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Book_Name.Text = dr["Name"].ToString();
                    Book_Field.Text = dr["Field"].ToString();
                    Book_Sem.Text = dr["semester"].ToString();
                    ISBN.ReadOnly = true;
                    Enrollment_No.ReadOnly = false;
                    Enrollment_No.Focus();
                    Alert_Fail2.Visible = Alert_Fail.Visible = Alert_Found.Visible = false;
                }
                else
                {
                    Alert_Fail2.Visible = true;
                    Alert_Fail.Visible = Alert_Found.Visible = false;
                    ISBN.Focus();                
                }
            }
            else
            {
                Alert_Fail.Visible = true;
                Alert_Fail2.Visible = Alert_Found.Visible = false;
                ISBN.Focus();
            }
            dr.Close();
        }
        else
        {
            Alert_Fail2.Visible = Alert_Fail.Visible = false;
            Alert_Found.Visible = true;
            ISBN.Focus();
        }
        dr.Close();
        con.Close();
    }

    protected void Enroll_TextChange(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select fname, lname from Student where Enrollment_No = '" + Enrollment_No.Text + "'", con);
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            Student_Name.Text = dr["fname"].ToString() + " " + dr["lname"].ToString();
            Enrollment_No.ReadOnly = true;
            Alert_Found2.Visible = false;
            AddRequest.Visible = true;
        }
        else
        {
            Alert_Found2.Visible = true; ;
            Enrollment_No.Focus();
        }
        dr.Close();
        con.Close();
    }

    protected void AddRequest_Click(object sender, EventArgs e)
    {
        HttpCookie AdminInfo = Request.Cookies["AdminInfo"];
        if (AdminInfo != null)
        {
            con.Open();
            cmd = new SqlCommand("insert into Book_requests (ISBN,Enrollment_No, Add_By, Add_Time) values('" + ISBN.Text + "', '" + Enrollment_No.Text + "', '" + AdminInfo["Id"].ToString() + "', '" + DateTime.Now + "')",con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        else
            Response.Redirect("Login.aspx");
    }

    public void FillTableData()
    {
        con.Open();
        cmd = new SqlCommand("SELECT b.Name AS Book, b.ISBN, s.FName, s.LName, s.Enrollment_No, r.Add_Time, a.Username, f.Name AS field, su.Semester FROM  Book_Requests AS r INNER JOIN book AS b ON b.ISBN = r.ISBN INNER JOIN student AS s ON s.Enrollment_No = r.Enrollment_No INNER JOIN admin AS a ON a.Id = r.Add_By INNER JOIN subject AS su ON su.Id = b.Subject INNER JOIN Fields AS f ON f.Id = su.Field order BY r.Add_Time DESC", con);
        dr = cmd.ExecuteReader();
        Table.Clear();
        int i = 0;
        for (i = 0; dr.Read(); i++)
        {
            Table.Append("<tr>");
            Table.Append("<td><abbr title=" + dr["ISBN"] + " class='text-decoration-none'><a href='Book-Profile?Book=" + dr["ISBN"] + "'  class=' text-decoration-none text-dark'>" + dr["Book"] + "</a></abbr></td>");
            Table.Append("<td><abbr title=" + dr["Enrollment_No"] + " class='text-decoration-none'><a href='Student-Profile?Student=" + dr["Enrollment_No"] + "' class='text-capitalize text-decoration-none text-dark'>" + (dr["fname"].ToString()) + " " + (dr["lname"].ToString()) + "</a></abbr></td>");
            Table.Append("<td>" + dr["field"] + "</td>");
            Table.Append("<td>" + dr["semester"] + "</td>");
            Table.Append("<td>" + dr["username"] + "</td>");
            Table.Append("<td>" + Convert.ToDateTime(dr["add_time"]).ToString("MMM dd ,yyyy h:mm tt") + "</td>");
            Table.Append("</tr>");
        }
        TableData.Controls.Add(new Literal { Text = Table.ToString() });
        dr.Close();
        con.Close();
    }
}