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
    string name, month, year, day, q1, q2, q3, q4, q5, q6, n1, n2, n3;
    StringBuilder htmlTable = new StringBuilder();
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\SSIP PROJECT\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
    protected void Page_Load(object sender, EventArgs e)
    {
        string qry = "SELECT request, b.RFID as BookRFID, b.bname AS Book,s.RFID as StudentRFID, s.fname AS StudentF ,s.lname AS StudentL, ir.itime AS itime, ir.ifrom AS ifrom, ir.rtime AS rtime, ir.rto AS rto, ir.charges AS Charges, remark AS remark FROM     issue_return AS ir INNER JOIN book b ON b.RFID = ir.BRFID INNER JOIN student s ON s.RFID = ir.SRFID ";
        name = "Incoming Books";

        year = string.Format("{0}", Request.Form["year"]);
        month = string.Format("{0}", Request.Form["month"]);
        day = string.Format("{0}", Request.Form["day"]);

        year += year.Length == 2 ? "20" : "";

        if (year == "")
            year = Year_lbl.Text;
        if (month == "")
            month = Month_lbl.Text;
        if (day == "")
            day = Day_lbl.Text;

        if (day!="" || month!="" || year!="")
        {
            if (day != "")
            {
                q1 = " (DAY(DATEADD(DD,7,ir.itime)) = '" + day + "')";
                q4 = "(DAY(DATEADD(DD,7,ir.itime)) = '" + day + "')";
                Day_lbl.Text = day;
                n1 = "Day-" + day + "";
            }

            if (month != "")
            {
                q2 = " and (MONTH(ir.itime) = '" + month + "')";
                q5 = " and (MONTH(ir.rtime) = '" + month + "')";
                Month_lbl.Text = month;
                n2 = "_Month-" + month + "";
            }

            if (year != "")
            {
                q3 = " and (YEAR(ir.itime) = '" + year + "')";
                q6 = " and (YEAR(ir.rtime) = '" + year + "')";
                Year_lbl.Text = year;
                n3 = "_Year-" + year + "";
            }

            qry += " where" + q1 + "" + q2 + "" + q3 + " or " + q4 + "" + q5 + "" + q6 + "";
            name = "Incoming Books " + n1 + "" + n2 + "" + n3 + "";
            qry = qry.Replace("where and", "where");
            qry = qry.Replace("where  ", "");
            name = name.Replace(" _", " ");
        }
        else
            qry += " where ir.rtime is null";


        con.Open();
        int i = 0;
        SqlCommand cmd = new SqlCommand(qry, con);
        SqlDataReader dr = cmd.ExecuteReader();
        TimeSpan t;
        for (; dr.Read(); i++)
        {
            data.Text = "";
            htmlTable.Append("<tr>");
            htmlTable.Append("<td><a href='BookInfo?Book=" + dr["BookRFID"] + "'  style='color:#000'>" + dr["Book"] + "</a></td>");
            htmlTable.Append("<td><a href='StudentInfo?Student=" + dr["StudentRFID"] + "'  style='color:#000'>" + dr["StudentF"] + " " + dr["StudentL"] + "</a></td>");
            if (dr["request"].ToString() != "")
            {
                t = DateTime.Now - Convert.ToDateTime(dr["request"]).AddDays(2);
                if (t.Days > 0)
                    htmlTable.Append("<td class='text-danger'>" + Convert.ToDateTime(dr["request"]).AddDays(2).ToString("MMMM dd ,yyyy") + "</td>");
                else
                    htmlTable.Append("<td class='text-primary'>" + Convert.ToDateTime(dr["request"]).AddDays(2).ToString("MMMM dd ,yyyy") + "</td>");
            }
            else
            {
                t = DateTime.Now - Convert.ToDateTime(dr["itime"]).AddDays(7);
                if (t.Days > 0)
                    htmlTable.Append("<td class='text-danger'>" + Convert.ToDateTime(dr["itime"]).AddDays(7).ToString("MMMM dd ,yyyy") + "</td>");
                else
                    htmlTable.Append("<td class='text-success'>" + Convert.ToDateTime(dr["itime"]).AddDays(7).ToString("MMMM dd ,yyyy") + "</td>");
            }
            htmlTable.Append("<td>" + Convert.ToDateTime(dr["itime"]).ToString("MMMM dd ,yyyy h:mm tt") + "</td>");
            htmlTable.Append("<td>" + dr["ifrom"] + "</td>");
            htmlTable.Append("</tr>");
        }
        if (i <= 0)
        {
            data.Text = "none";
            htmlTable.Append("<tr><td colspan=6 align=center style='text-transform:none'>All Books are Returned</td></tr>");
        }
        InBookstbl.Controls.Add(new Literal { Text = htmlTable.ToString() });
        dr.Close();

        htmlTable.Clear();
        qry = "SELECT request, b.RFID as BookRFID, b.bname AS Book,s.RFID as StudentRFID, s.fname AS StudentF ,s.lname AS StudentL, ir.itime AS itime, ir.ifrom AS ifrom, ir.rtime AS rtime, ir.rto AS rto, ir.charges AS Charges, remark AS remark FROM     issue_return AS ir INNER JOIN book b ON b.RFID = ir.BRFID INNER JOIN student s ON s.RFID = ir.SRFID order by ir.itime desc";
        SqlCommand cmd2 = new SqlCommand(qry, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        while (dr2.Read())
        {
            htmlTable.Append("<tr>");
            htmlTable.Append("<td>" + dr2["Book"] + "</td>");
            htmlTable.Append("<td>" + dr2["StudentF"] + " " + dr2["StudentL"] + "</td>");
            if (dr2["request"].ToString() != "")
            {
                t = DateTime.Now - Convert.ToDateTime(dr2["request"]).AddDays(2);
                if (t.Days > 0)
                    htmlTable.Append("<td class='text-danger'>" + Convert.ToDateTime(dr2["itime"]).AddDays(7).ToString("MMMM dd ,yyyy") + "</td>");
                else
                    htmlTable.Append("<td class='text-primary'>" + Convert.ToDateTime(dr2["itime"]).AddDays(7).ToString("MMMM dd ,yyyy") + "</td>");
            }
            else
            {
                t = DateTime.Now - Convert.ToDateTime(dr2["itime"]).AddDays(7);
                if (t.Days > 0)
                    htmlTable.Append("<td class='text-danger'>" + Convert.ToDateTime(dr2["itime"]).AddDays(7).ToString("MMMM dd ,yyyy") + "</td>");
                else
                    htmlTable.Append("<td class='text-success'>" + Convert.ToDateTime(dr2["itime"]).AddDays(7).ToString("MMMM dd ,yyyy") + "</td>");
            }
            htmlTable.Append("<td>" + Convert.ToDateTime(dr2["itime"]).ToString("MMMM dd ,yyyy h:mm tt") + "</td>");
            htmlTable.Append("<td>" + dr2["ifrom"] + "</td>");
            htmlTable.Append("</tr>");
        }
        InBookstbl2.Controls.Add(new Literal { Text = htmlTable.ToString() });
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

}