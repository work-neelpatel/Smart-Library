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
        if (!IsPostBack)
            FillData();
    }

    protected void Generate_Report_Click(object sender, EventArgs e)
    {

    }

    public void FillData() 
    {
        string Student = Request.QueryString["Student"];
        if (Student == "")
            Response.Redirect("Students");
        con.Open();
        cmd = new SqlCommand("SELECT s.RFID, s.FName, s.LName, s.Image, f.Name AS field, s.DOJ, s.Mobile, s.Email, (SELECT COUNT(SRFID) AS Expr1 FROM issue_return WHERE (SRFID = s.RFID)) AS Transactions,(SELECT SUM(Charge) AS Expr1 FROM issue_return AS ir WHERE (SRFID = s.RFID)) AS charges,(SELECT b.Name AS Expr1 FROM book AS b INNER JOIN BR_Connection AS br ON br.ISBN = b.ISBN INNER JOIN issue_return AS ir ON ir.BRFID = br.RFID WHERE (ir.SRFID = s.RFID) AND (ir.RTime IS NULL)) AS PendingBook, (SELECT br.ISBN AS Expr1 FROM BR_Connection AS br INNER JOIN issue_return AS ir ON ir.BRFID = br.RFID WHERE (ir.SRFID = s.RFID) AND (ir.RTime IS NULL)) AS PendingBookISBN FROM  student AS s INNER JOIN Fields AS f ON f.Id = s.Field where s.Enrollment_no='" + Student + "'", con);
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            RFID.Text = dr["RFID"].ToString();
            Enrollment_No.Text = Student;
            Fname.Text = dr["fname"].ToString();
            Lname.Text = dr["lname"].ToString();
            Mobile.Text = dr["mobile"].ToString();
            Email.Text = dr["email"].ToString();
            Field.Text = dr["field"].ToString();
            Total_Transactions.Text = dr["transactions"].ToString();
            Charges_Paid.Text = dr["charges"].ToString();
            if (dr["PendingBook"].ToString() != "")
            {
                Pending_Book.Text = dr["PendingBook"].ToString();
                Book_ISBN.Text = dr["PendingBookISBN"].ToString();
            }
            else
                Pending_Book.Text = Book_ISBN.Text = "-";
            Stu_Image.ImageUrl = dr["Image"].ToString();

            int TotalMonth = ((DateTime.Now.Year - Convert.ToDateTime(dr["DOJ"].ToString()).Year) * 12) + DateTime.Now.Month - Convert.ToDateTime(dr["DOJ"].ToString()).Month;
            if (TotalMonth >= 28)
                Semester.Text = 6.ToString();
            else if (TotalMonth >= 22)
                Semester.Text = 5.ToString();
            else if (TotalMonth >= 16)
                Semester.Text = 4.ToString();
            else if (TotalMonth >= 10)
                Semester.Text = 3.ToString();
            else if (TotalMonth >= 04)
                Semester.Text = 1.ToString();
            else
                Semester.Text = 1.ToString();
        }
        else
        {
            Alert_Found.Visible = true;
            Body.Visible = false;
        }
            dr.Close();
        con.Close();
        FillTableData();
    }

    public void FillTableData()
    {
        con.Open();
        cmd = new SqlCommand("SELECT b.Name AS Book, b.ISBN, ir.ITime, (SELECT Username FROM admin WHERE (Id = ir.IFrom)) AS Ifrom, (SELECT Username FROM admin AS admin_1 WHERE (Id = ir.RTo)) AS Rto, ir.RTime, ir.Charge, (SELECT Remark FROM charges WHERE (Id = ir.Charge_Remark)) AS Remark FROM book AS b INNER JOIN BR_Connection AS br ON br.ISBN = b.ISBN INNER JOIN issue_return AS ir ON ir.BRFID = br.RFID INNER JOIN student AS s ON s.RFID = ir.SRFID WHERE (s.Enrollment_No = '"+Enrollment_No.Text+"') ORDER BY ir.ITime DESC, ir.RTime DESC", con);
        dr = cmd.ExecuteReader();
        Table.Clear();
        int i = 0;
        for (; dr.Read(); i++)
        {
            Table.Append("<tr>");
            Table.Append("<td><abbr title=" + dr["ISBN"] + " class='text-decoration-none'><a href='Book-Profile?Book=" + dr["ISBN"] + "'  class=' text-decoration-none text-dark'>" + dr["Book"] + "</a></abbr></td>");
            Table.Append("<td>" + dr["ifrom"] + "</td>");
            Table.Append("<td>" + Convert.ToDateTime(dr["itime"]).ToString("MMM dd ,yyyy h:mm tt") + "</td>");
            if (!(dr["rto"] is DBNull))
            {
                Table.Append("<td>" + dr["rto"] + "</td>");
                Table.Append("<td>" + Convert.ToDateTime(dr["rtime"]).ToString("MMM dd ,yyyy h:mm tt") + "</td>");
                if (dr["Charge"].ToString() != "0")
                {
                    Table.Append("<td class='text-danger'>" + dr["charge"] + "</td>");
                    Table.Append("<td class='text-danger'>" + dr["remark"] + "</td>");
                }
                else
                    Table.Append("<td class='text-success'>0</td><td>-</td>");
            }
            else
            {
                Table.Append("<td class='text-danger'>Not Returned yet</td><td>-</td><td>-</td><td>-</td>");
            }
            Table.Append("</tr>");
        }
        TableData.Controls.Add(new Literal { Text = Table.ToString() });
        dr.Close();
        con.Close();
    }

    protected void Pending_Book_Click(object sender, EventArgs e)
    {
        if (Book_ISBN.Text != "-")
            Response.Redirect("Book-Profile.aspx?Book="+ Book_ISBN.Text +"");
    }
}