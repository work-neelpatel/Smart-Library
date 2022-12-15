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
    string Student,Book;
    StringBuilder htmlTable = new StringBuilder();
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\SSIP PROJECT\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
    protected void Page_Load(object sender, EventArgs e)
    {
        Student = Request.QueryString["Student"];
        if (Student == null)
            Response.Redirect("Students");
        else
            FillData();
    }
    public void FillTblData()
    {
        con.Open();
        string sql="SELECT distinct b.bname,b.RFID, ir.itime, ir.ifrom, ir.rtime, ir.rto, ir.charges, remark FROM     issue_return AS ir INNER JOIN                   book AS b ON b.RFID = ir.BRFID INNER JOIN                   student AS s ON ir.SRFID = '"+Student+"' order by ir.itime desc";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        int i = 0;
        for (; dr.Read(); i++)
        {
            data.Text = "";
            htmlTable.Append("<tr>");
            htmlTable.Append("<td><a href='BookInfo?Book=" + dr["RFID"] + "'  style='color:#000'>" + dr["bname"] + "</a></td>");
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
            htmlTable.Append("<tr><td colspan=8 align=center>No Transaction Done</td></tr>");
        }
        StudentInfotbl.Controls.Add(new Literal { Text = htmlTable.ToString() });
        dr.Close();
        htmlTable.Clear();

        sql = "SELECT distinct b.bname,b.RFID, ir.itime, ir.ifrom, ir.rtime, ir.rto, ir.charges, remark FROM     issue_return AS ir INNER JOIN                   book AS b ON b.RFID = ir.BRFID INNER JOIN                   student AS s ON ir.SRFID = '" + Student + "' order by ir.itime desc";
        SqlCommand cmd2 = new SqlCommand(sql, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        while (dr2.Read())
        {
            htmlTable.Append("<tr>");
            htmlTable.Append("<td>" + dr2["bname"] + "</td>");
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
        StudentInfotbl2.Controls.Add(new Literal { Text = htmlTable.ToString() });
        con.Close();
    }

    public void FillData()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT s.RFID, s.enrollno, s.fname, s.lname, s.mobile, s.email, s.address, s.zipcode, s.imgsrc, s.DOJ, COUNT(ir.SRFID) AS [Total books], SUM(ir.charges) AS [Total charges],                       (SELECT BRFID AS Expr1                        FROM      issue_return AS ir                        WHERE   (SRFID = s.RFID) AND (rtime IS NULL)) AS [Pending book RFID],                       (SELECT b.bname AS Expr1                        FROM      issue_return AS ir INNER JOIN                                          book AS b ON ir.BRFID = b.RFID                        WHERE   (ir.SRFID = s.RFID) AND (ir.rtime IS NULL)) AS [Pending book] FROM     student AS s INNER JOIN                    issue_return AS ir ON ir.SRFID = s.RFID OR s.RFID <> ir.SRFID where s.RFID='" + Student + "' GROUP BY s.RFID, s.RFID, s.enrollno, s.fname, s.lname, s.mobile, s.email, s.address, s.zipcode, s.imgsrc, s.DOJ, ir.BRFID", con);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            RFID_lbl.Text = dr["RFID"].ToString();
            Enroll_lbl.Text = dr["enrollno"].ToString();
            Fname_lbl.Text = dr["fname"].ToString();
            Lname_lbl.Text = dr["lname"].ToString();
            Sem_lbl.Text = dr["DOJ"].ToString();
            Mobile_lbl.Text = dr["mobile"].ToString();
            Email_lbl.Text = dr["email"].ToString();
            Zipcode_lbl.Text = dr["zipcode"].ToString();
            Address_lbl.Text = dr["address"].ToString();
            Charges_lbl.Text = dr["Total charges"].ToString();
            IssueBooks_lbl.Text = dr["Total books"].ToString();
            if (dr["Pending book"].ToString() == "")
            {
                PendingBook_lbl.ForeColor = System.Drawing.Color.Green;
                PendingBook_lbl.Text = "-";
            }
            else
            {
                PendingBook_lbl.ForeColor = System.Drawing.Color.Red;
                PendingBook_lbl.Text = dr["Pending book"].ToString();
            }
            Book = dr["Pending book RFID"].ToString();
            Stu_Image.ImageUrl = dr["imgsrc"].ToString();
        }
        dr.Close();
        con.Close();
        FillTblData();
    }
    protected void BookInfo(object sender, EventArgs e)
    {
        Response.Redirect("BookInfo?Book="+Book+"");
    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        if (data.Text != "none")
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + Enroll_lbl.Text + "-" + Fname_lbl.Text + "_"+Lname_lbl.Text+" Transactions.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.Output.Write(Request.Form[hfGridHtml.UniqueID]);
            Response.Flush();
            Response.End();
        }
    }
}