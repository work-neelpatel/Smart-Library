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
    string name, qry, student, book, year, month, day, q1, q2, q3, q4, q5, q6, q7, q8, n1, n2, n3, n4, n5;
    StringBuilder htmlTable = new StringBuilder();
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\SSIP PROJECT\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
    protected void Page_Load(object sender, EventArgs e)
    {
        qry = "SELECT b.RFID AS BookRFID, b.bname, b.ISBN, s.RFID AS StudentRFID, s.fname, s.lname, s.enrollno, ir.itime, ir.ifrom, ir.rtime, ir.rto, ir.charges AS Charges, ir.remark FROM     issue_return AS ir INNER JOIN                   book AS b ON b.RFID = ir.BRFID INNER JOIN                   student AS s ON s.RFID = ir.SRFID ";
        name = "Transactions";

        student = string.Format("{0}", Request.Form["student"]);
        book = string.Format("{0}", Request.Form["book"]);
        year = string.Format("{0}", Request.Form["year"]);
        month = string.Format("{0}", Request.Form["month"]);
        day = string.Format("{0}", Request.Form["day"]);

        year += year.Length == 2 ? "20" : "";

        if (student == "")
            student = Student_lbl.Text;
        if (book == "")
            book = Book_lbl.Text;
        if (year == "")
            year = Year_lbl.Text;
        if (month == "")
            month = Month_lbl.Text;
        if (day == "")
            day = Day_lbl.Text;

        if (student != "" || book != "" || year != "" || month != "" || day != "")
            find();

        qry += " order by ir.itime desc";
        con.Open();
        int i=0;
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataReader dr = cmd.ExecuteReader();
        for (; dr.Read(); i++)
        {
            data.Text = "";
            htmlTable.Append("<tr>");
            htmlTable.Append("<td><a href='BookInfo?Book=" + dr["BookRFID"] + "'  style='color:#000'>" + dr["bname"] + "</a></td>");
            htmlTable.Append("<td><a href='StudentInfo?Student=" + dr["StudentRFID"] + "'  style='color:#000'>" + dr["fname"] + " " + dr["lname"] + "</a></td>");
            htmlTable.Append("<td>" + dr["ifrom"] + "</td>");
            htmlTable.Append("<td>" + Convert.ToDateTime(dr["itime"]).ToString("MMMM dd ,yyyy h:mm tt") + "</td>");
            if (!(dr["rto"] is DBNull))
            {
                htmlTable.Append("<td>" + dr["rto"] + "</td>");
                htmlTable.Append("<td>" + Convert.ToDateTime(dr["rtime"]).ToString("MMMM dd ,yyyy h:mm tt") + "</td>");
                if (dr["Charges"].ToString() != "0")
                {
                    htmlTable.Append("<td class='text-danger'>" + dr["Charges"] + "</td>");
                    htmlTable.Append("<td class='text-danger'>" + dr["remark"] + "</td>");
                }
                else
                    htmlTable.Append("<td class='text-success'>0</td><td>-</td>");
            }
            else
            {
                htmlTable.Append("<td class='text-danger'>Not Returned yet</td><td>-</td><td>-</td><td>-</td>");
            }
            htmlTable.Append("</tr>");
        }
        if (i <= 0)
        {
            data.Text = "none";
            htmlTable.Append("<tr><td colspan=8 align=center>No Transaction Done</td></tr>");
        }
        Transactiontbl.Controls.Add(new Literal { Text = htmlTable.ToString() });
        dr.Close();

        htmlTable.Clear();
        SqlCommand cmd2 = new SqlCommand(qry, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        while (dr2.Read())
        {
            data.Text = "";
            htmlTable.Append("<tr>");
            htmlTable.Append("<td>" + dr2["bname"] + "</td>");
            htmlTable.Append("<td>" + dr2["fname"] + " " + dr2["lname"] + "</td>");
            htmlTable.Append("<td>" + dr2["ifrom"] + "</td>");
            htmlTable.Append("<td>" + Convert.ToDateTime(dr2["itime"]).ToString("MMMM dd ,yyyy h:mm tt") + "</td>");
            if (!(dr2["rto"] is DBNull))
            {
                htmlTable.Append("<td>" + dr2["rto"] + "</td>");
                htmlTable.Append("<td>" + Convert.ToDateTime(dr2["rtime"]).ToString("MMMM dd ,yyyy h:mm tt") + "</td>");
                if (dr2["Charges"].ToString() != "0")
                {
                    htmlTable.Append("<td class='text-danger'>" + dr2["Charges"] + "</td>");
                    htmlTable.Append("<td class='text-danger'>" + dr2["remark"] + "</td>");
                }
                else
                    htmlTable.Append("<td class='text-success'>0</td><td>-</td>");
            }
            else
            {
                htmlTable.Append("<td class='text-danger'>Not Returned yet</td><td>-</td><td>-</td><td>-</td>");
            }
            htmlTable.Append("</tr>");
        }
        Transactiontbl2.Controls.Add(new Literal { Text = htmlTable.ToString() });
        con.Close();
    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        if (data.Text != "none")
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + name + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.Output.Write(Request.Form[hfGridHtml.UniqueID]);
            Response.Flush();
            Response.End();
        }
    }

    public void find()
    {        
        if (day != "")
        {
            q1 = " (DAY(ir.itime) = '" + day + "')";
            q6 = "(DAY(ir.rtime) = '" + day + "')";
            Day_lbl.Text = day;
            n1 = "Day-" + day + "";
        }

        if (month != "")
        {
            q2 = " and (MONTH(ir.itime) = '" + month + "')";
            q7 = " and (MONTH(ir.rtime) = '" + month + "')";
            Month_lbl.Text = month;
            n2 = "_Month-" + month + "";
        }

        if (year != "")
        {
            q3 = " and (YEAR(ir.itime) = '" + year + "')";
            q8 = " and (YEAR(ir.rtime) = '" + year + "')";
            Year_lbl.Text = year;
            n3 = "_Year-" + year + "";
        }

        if (student != "")
        {
            q4 = " and s.enrollno = '"+student+"' ";
            Student_lbl.Text = student;
            n4 = "_Stu-" + student + "";
        }

        if (book != "")
        {
            q5 = " and b.ISBN = '"+book+"'";
            Book_lbl.Text = book;
            n5 = "_Book-" + book + "";
        }

        qry += " where" + q1 + "" + q2 + "" + q3 + "" + q4 + "" + q5 + " or " + q6 + "" + q7 + "" + q8 + "";
        name = "Transactions " + n3 + "" + n2 + "" + n1 + "" + n4 + "" + n5 + "";
        qry = qry.Replace("where and", "where");
        qry = qry.Replace("or  and", "or ");
        qry = qry.Replace("where  ", "");
        name = name.Replace(" _", " ");
    }


}