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

    string name, qry, n1, n2, q1, q2;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("done");
        if (!IsPostBack)
        {
            Semester.SelectedValue = "All";
            FillTableData();
        }
    }

    protected void Generate_Report_Click(object sender, EventArgs e)
    {

    }

    public void FillTableData()
    {
        if (Field.SelectedValue != "All")
        {
            q1 = " and (f.name='" + Field.SelectedValue + "') ";
            n1 = "_" + Field.SelectedValue + "";
        }
        if (Semester.SelectedValue != "All")
        {
            q2 = FindYear();
            n2 = "-Sem:" + Semester.SelectedValue+ "";
        }

        qry = "SELECT s.Enrollment_No, s.FName, s.LName, s.DOJ, f.Name AS Field, (SELECT COUNT(SRFID) AS Expr1  FROM  issue_return WHERE (SRFID = s.RFID)) AS Transactions, (SELECT COUNT(Charge) AS Expr1 FROM  issue_return AS issue_return_1 WHERE (SRFID = s.RFID)) AS charges, (SELECT TOP (1) b.Name FROM  book AS b INNER JOIN BR_Connection AS br ON b.ISBN = br.ISBN INNER JOIN issue_return AS ir ON br.RFID = ir.BRFID WHERE (ir.SRFID = s.RFID) ORDER BY ir.RTime DESC) AS LastBook, (SELECT TOP (1) br.ISBN FROM  BR_Connection AS br INNER JOIN issue_return AS ir ON br.RFID = ir.BRFID WHERE (ir.SRFID = s.RFID) ORDER BY ir.RTime DESC) AS LBookISBN, (SELECT TOP (1) RTime FROM  issue_return AS issue_return_3 WHERE (SRFID = s.RFID) ORDER BY RTime DESC) AS LBookRTime, (SELECT TOP (1) ITime FROM  issue_return AS issue_return_2 WHERE (SRFID = s.RFID) ORDER BY ITime DESC) AS BookITime FROM  student AS s INNER JOIN Fields AS f ON s.Field = f.Id where " + q2 + "" + q1 + " ";
        name = "Students " + n1 + "" + n2 + "";
        qry = qry.Replace("where and", "where");
        qry = qry.Replace("where  ", "");
        name = name.Replace(" _", " ");

        con.Open();
        cmd = new SqlCommand(qry, con);
        dr = cmd.ExecuteReader();
        Table.Clear();
        int i = 0;
        for (; dr.Read(); i++)
        {
            Table.Append("<tr>");
            Table.Append("<td>" + dr["Enrollment_No"] + "</td>");
            Table.Append("<td><a href='Student-Profile?Student=" + dr["Enrollment_No"] + "' class='text-dark text-capitalize text-decoration-none'>" + (dr["fname"].ToString()) + " " + (dr["lname"].ToString()) + "</a></td>");
            Table.Append("<td>" + dr["Field"] + "</td>");
            Table.Append("<td>" + FindSemester(dr["DOJ"].ToString()) + "</td>");
            Table.Append("<td>" + dr["Transactions"] + "</td>");
            Table.Append("<td>" + dr["Charges"] + "</td>");
            if (dr["LastBook"].ToString() == "")
                Table.Append("<td>-</td><td>-</td>");
            else
            {
                //last transactin time
                if (dr["LBookRTime"].ToString() == "")
                    Table.Append("<td><b class='pr-2 text-info'>I</b>" + Convert.ToDateTime(dr["LBookITime"]).ToString("MMMM dd ,yyyy h:mm tt") + "</td>");
                else
                    Table.Append("<td><b class='pr-2 text-info'>R</b>" + Convert.ToDateTime(dr["LBookRTime"]).ToString("MMMM dd ,yyyy h:mm tt") + "</td>");

                //last trasaction book
                if (dr["LBookRTime"].ToString() == "")
                    Table.Append("<td><a href='Book-Profile?Book=" + dr["LBookISBN"] + "' class='text-decoration-none text-danger'>" + dr["LastBook"] + "</a></td>");
                else
                    Table.Append("<td><a href='Book-Profile?Book=" + dr["LBookISBN"] + "' class='text-decoration-none'>" + dr["LastBook"] + "</a></td>");
            }
            Table.Append("</tr>");
        }
        TableData.Controls.Add(new Literal { Text = Table.ToString() });
        dr.Close();
        con.Close();
    }

    public int FindSemester(string DOJ)
    {
        int TotalMonth = ((DateTime.Now.Year - Convert.ToDateTime(DOJ).Year) * 12) + DateTime.Now.Month - Convert.ToDateTime(DOJ).Month;
        if (TotalMonth >= 28)
            return 6;
        else if (TotalMonth >= 22)
            return 5;
        else if (TotalMonth >= 16)
            return 4;
        else if (TotalMonth >= 10)
            return 3;
        else if (TotalMonth >= 04)
            return 2;
        else
            return 1;
        /*
            SEM-6	18-6 19-6 20-6 21-4	= 34 OR 32
            SEM-5	18-6 19-6 20-6 20-12	= 30 OR 28
            SEM-4	19-6 20-6 21-6		= 24 OR 22
            SEM-3	19-6 20-6 20-12		= 18 OR 16
            SEM-2	20-6 21-6		= 12 OR 10
            SEM-1	20-6 20-12		= 6  OR 4

            SEM-6	18-6 19-6 20-6 20-12	= 30 OR 28
            SEM-5	18-6 19-6 20-6 	     	= 24 OR 22
            SEM-4	19-6 20-6 20-12		= 18 OR 16
            SEM-3	19-6 20-6		= 12 OR 10
            SEM-2	20-6 20-12		= 6  OR 4         
        */
    }

    public string FindYear()
    {
        int TotalMonth1 = 0, TotalMonth2 = 0, Month = 0, Month2 = 0, SearchSem = Convert.ToInt16(Semester.SelectedValue);

        if (SearchSem == 6)
        {
            TotalMonth1 = -28;
            TotalMonth2 = -32;
        }
        else if (SearchSem == 5)
        {
            TotalMonth1 = -22;
            TotalMonth2 = -28;
        }
        else if (SearchSem == 4)
        {
            TotalMonth1 = -16;
            TotalMonth1 = -22;
        }
        else if (SearchSem == 3)
        {
            TotalMonth1 = -10;
            TotalMonth2 = -16;
        }
        else if (SearchSem == 2)
        {
            TotalMonth1 = -4;
            TotalMonth2 = -10;
        }
        else
        {
            TotalMonth1 = 0;
            TotalMonth2 = -4;
        }

        if (SearchSem % 2 == 0)
        {
            Month = 12;
            Month2 = 1;
        }
        else
        {
            Month = 6;
            Month2 = 7;
        }
        int Year = DateTime.Now.AddMonths(TotalMonth1).Year;
        int Year2 = DateTime.Now.AddMonths(TotalMonth2).Year;


        return "((YEAR(s.DOJ) = " + Year + ") and (Month(s.DOJ) = " + Month + ")) or ((YEAR(s.DOJ) = " + Year2 + ") and (Month(s.DOJ) = " + Month2 + ")) ";
    }

    protected void Search_Student_Click(object sender, EventArgs e)
    {
        FillTableData();
    }
}