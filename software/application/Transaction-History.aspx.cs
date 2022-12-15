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

    string name, qry;
    string n1, n2, n3, n4, n5, n6, q1, q2, q3, q4, q5, q6, q7, q8;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            FillTableData();
    }

    protected void Generate_Report_Click(object sender, EventArgs e)
    {

    }

    protected void Search_Record_Click(object sender, EventArgs e)
    {
        FillTableData();
    }

    public void FillTableData()
    {
        con.Open();
        cmd = new SqlCommand("SELECT TOP (3) b.ISBN, b.Name AS Book, s.Name AS subject, s.Semester, f.Name AS field, (select count(BRFID) from book as b inner join br_connection as br on br.ISBN = b.ISBN inner join issue_return as ir on ir.BRFID = br.RFID) as Transactions FROM book AS b INNER JOIN subject AS s ON s.Id = b.Subject INNER JOIN Fields AS f ON f.Id = s.Field where (select count(BRFID) from book as b inner join br_connection as br on br.ISBN = b.ISBN inner join issue_return as ir on ir.BRFID = br.RFID) > 0", con);
        dr = cmd.ExecuteReader();
        Table.Clear();
        int i = 0;
        for (; dr.Read(); i++)
        {
            Table.Append("<tr>");
            Table.Append("<td>" + dr["ISBN"] + "</td>");
            Table.Append("<td><a href='Book-Profile?Book=" + dr["ISBN"] + "'  class=' text-decoration-none text-dark'>" + dr["Book"] + "</a></td>");
            Table.Append("<td>" + dr["field"] + "</td>");
            Table.Append("<td>" + dr["semester"] + "</td>");
            Table.Append("<td>" + dr["subject"] + "</td>");
            if (dr["transactions"].ToString()!="")
                Table.Append("<td>" + dr["transactions"] + "</td>");
            else
                Table.Append("<td>0</td>");                
            Table.Append("</tr>");
        }
        dr.Close();
        while (i < 3)
        {
            Table.Append("<tr><td>-</td><td>-</td><td>-</td><td>-</td><td>-</td><td>-</td></tr>");
            i++;
        }
        TableTopData.Controls.Add(new Literal { Text = Table.ToString() });


        if (IsPostBack)
        {
            //Create flexible query
            if (Day.Text != "")
            {
                q1 = " (DAY(ir.itime) = '" + Day.Text + "')";
                q6 = "(DAY(ir.rtime) = '" + Day.Text + "')";
                n1 = "Day-" + Day.Text + "";
            }

            if (Month.Text != "")
            {
                q2 = " and (MONTH(ir.itime) = '" + Month.Text + "')";
                q7 = " and (MONTH(ir.rtime) = '" + Month.Text + "')";
                n2 = "_Month-" + Month.Text + "";
            }

            if (Year.Text != "")
            {
                if (Year.Text.Length == 2)
                    Year.Text = "20" + Year.Text;
                q3 = " and (YEAR(ir.itime) = '" + Year.Text + "')";
                q8 = " and (YEAR(ir.rtime) = '" + Year.Text + "')";
                n3 = "_Year-" + Year.Text + "";
            }

            if (Student.Text != "")
            {
                q4 = " and s.enrollment_No = '" + Student.Text + "' ";
                n4 = "_Stu-" + Student.Text + "";
            }

            if (Book.Text != "")
            {
                q5 = " and b.ISBN = '" + Book.Text + "'";
                n5 = "_Book-" + Book.Text + "";
            }


            qry = "SELECT b.Name AS Book, b.ISBN, s.FName, s.LName, s.Enrollment_No, ir.ITime,ir.retime, (select Username from admin where id=ir.ifrom) as Ifrom, (select Username from admin where id=ir.rto) as Rto, ir.RTime, ir.Charge, (select Remark from charges where id=ir.charge_remark) as Remark FROM book as b inner join br_connection as br on br.ISBN = b.ISBN inner join issue_return AS ir ON br.RFID = ir.BRFID INNER JOIN student AS s ON s.RFID = ir.SRFID where" + q1 + "" + q2 + "" + q3 + "" + q4 + "" + q5 + " or " + q6 + "" + q7 + "" + q8 + " order by ir.itime desc,ir.rtime desc";
            name = "Transaction History " + n3 + "" + n2 + "" + n1 + "" + n4 + "" + n5 + "";
            qry = qry.Replace("where and", "where");
            qry = qry.Replace("or  and ", "or ");
            qry = qry.Replace("or  ", " ");
            qry = qry.Replace("where  ", "");
            name = name.Replace(" _", " ");

        }
        else
        {
            qry = "SELECT b.Name AS Book, b.ISBN, s.FName, s.LName, s.Enrollment_No, ir.ITime,ir.retime, (select Username from admin where id=ir.ifrom) as Ifrom, (select Username from admin where id=ir.rto) as Rto, ir.RTime, ir.Charge, (select Remark from charges where id=ir.charge_remark) as Remark FROM book as b inner join br_connection as br on br.ISBN = b.ISBN inner join issue_return AS ir ON br.RFID = ir.BRFID INNER JOIN student AS s ON s.RFID = ir.SRFID order by ir.itime desc, ir.rtime desc";
            name = "Transaction History";
        }
        cmd = new SqlCommand(qry, con);
        dr = cmd.ExecuteReader();
        Table.Clear();
        for (i = 0; dr.Read(); i++)
        {
            Table.Append("<tr>");
            Table.Append("<td><abbr title=" + dr["ISBN"] + " class='text-decoration-none'><a href='Book-Profile?Book=" + dr["ISBN"] + "'  class=' text-decoration-none text-dark'>" + dr["Book"] + "</a></abbr></td>");
            if (dr["retime"].ToString() != "")
                Table.Append("<td><abbr title=" + dr["Enrollment_No"] + " class='text-decoration-none'><a href='Student-Profile?Student=" + dr["Enrollment_No"] + "' class='text-capitalize text-decoration-none'>" + (dr["fname"].ToString()) + " " + (dr["lname"].ToString()) + "</a></abbr></td>");
            else
                Table.Append("<td><abbr title=" + dr["Enrollment_No"] + " class='text-decoration-none'><a href='Student-Profile?Student=" + dr["Enrollment_No"] + "' class='text-dark text-capitalize text-decoration-none'>" + (dr["fname"].ToString()) + " " + (dr["lname"].ToString()) + "</a></abbr></td>");
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
}