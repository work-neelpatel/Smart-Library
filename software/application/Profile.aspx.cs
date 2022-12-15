using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

public partial class Default2 : System.Web.UI.Page
{
    string name, month, year, day, q1, q2, q3, q4, q5, q6, n1, n2, n3;
    StringBuilder htmlTable = new StringBuilder();
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\SSIP PROJECT\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["Admin"] == null)
            Response.Redirect("SignIn");

        Name_lbl.Text = Session["Admin"].ToString();
        FillData();
    }

    public void FillData()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT (SELECT COUNT(BRFID) AS Expr1 FROM issue_return WHERE (ifrom = '"+Session["Admin"]+"')) AS Ibooks,(SELECT COUNT(BRFID) AS Expr1 FROM issue_return AS issue_return_1 WHERE   (rto = '"+Session["Admin"]+"')) AS Rbooks, (SELECT SUM(charges) AS Expr1 FROM issue_return AS issue_return_2 WHERE   (rto = '" + Name_lbl.Text + "')) AS Tcharges ", con);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            Ibooks_lbl.Text=dr["Ibooks"].ToString();
            Rbooks_lbl.Text=dr["Rbooks"].ToString();
            Tcharges_lbl.Text=dr["Tcharges"].ToString();
        }
        dr.Close();
        con.Close();
        FillTblData();
    }
    
    public void FillTblData()
    {
        string qry = "SELECT b.bname, s.fname, s.lname,ir.BRFID, ir.SRFID, ir.ifrom, ir.itime, ir.rtime, ir.rto, ir.charges, ir.remark FROM issue_return AS ir INNER JOIN book AS b ON ir.BRFID = b.RFID INNER JOIN student AS s ON ir.SRFID = s.RFID";
        name = "Admin-"+Session["Admin"].ToString()+" Transactions";

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

        if (day != "" || month != "" || year != "")
        {
            if (day != "")
            {
                q1 = " (DAY(ir.itime) = '" + day + "')";
                q4 = "(DAY(ir.itime) = '" + day + "')";
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

            qry += " where" + q1 + "" + q2 + "" + q3 + " or " + q4 + "" + q5 + "" + q6 + " and ((ir.ifrom = '" + Session["Admin"] + "') OR (ir.rto = '" + Session["Admin"] + "'))   order by ir.itime desc";
            name = "Admin-" + Session["Admin"].ToString() + " Transactions " + n1 + "" + n2 + "" + n3 + "";
            qry = qry.Replace("where and", "where");
            qry = qry.Replace("or  and", "or ");
            qry = qry.Replace("where  ", "");
            name = name.Replace(" _", " ");
        }
        else
            qry += " where (ir.ifrom = '" + Session["Admin"] + "') OR (ir.rto = '" + Session["Admin"] + "')   order by ir.itime desc";

        con.Open();
        int i = 0;
        SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
        for (; dr.Read(); i++)
        {
            data.Text = "";
            htmlTable.Append("<tr>");
            htmlTable.Append("<td><a href='BookInfo?Book=" + dr["BRFID"] + "'  style='color:#000'>" + dr["bname"] + "</a></td>");
            htmlTable.Append("<td><a href='StudentInfo?Book=" + dr["SRFID"] + "'  style='color:#000'>" + dr["fname"] + " " + dr["lname"] + "</a></td>");
            htmlTable.Append("<td>" + dr["ifrom"] + "</td>");
            htmlTable.Append("<td>" + Convert.ToDateTime(dr["itime"]).ToString("MMMM dd ,yyyy HH:MM") + "</td>");
            if (!(dr["rto"] is DBNull))
            {
                htmlTable.Append("<td>" + dr["rto"] + "</td>");
                htmlTable.Append("<td>" + Convert.ToDateTime(dr["rtime"]).ToString("MMMM dd ,yyyy") + "</td>");
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
            htmlTable.Append("<tr><td colspan=8 align=center style='text-transform:none'>No Transaction Done</td></tr>");
        }
        AdminTransactiontbl.Controls.Add(new Literal { Text = htmlTable.ToString() });
        dr.Close();

        htmlTable.Clear();
        SqlCommand cmd2 = new SqlCommand(qry, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        while (dr2.Read())
        {
            htmlTable.Append("<tr>");
            htmlTable.Append("<td>" + dr2["bname"] + "</td>");
            htmlTable.Append("<td>" + dr2["fname"] + " " + dr2["lname"] + "</td>");
            htmlTable.Append("<td>" + dr2["ifrom"] + "</td>");
            htmlTable.Append("<td>" + Convert.ToDateTime(dr2["itime"]).ToString("MMMM dd ,yyyy HH:MM") + "</td>");
            if (!(dr2["rto"] is DBNull))
            {
                htmlTable.Append("<td>" + dr2["rto"] + "</td>");
                htmlTable.Append("<td>" + Convert.ToDateTime(dr2["rtime"]).ToString("MMMM dd ,yyyy") + "</td>");
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
        AdminTransactiontbl2.Controls.Add(new Literal { Text = htmlTable.ToString() });
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