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
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\asp practice\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;
    SqlDataReader dr;

    StringBuilder Table = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            FillTableData("SELECT b.Name AS Book, b.ISBN, s.FName, s.LName, s.Enrollment_No, ir.ITime, (SELECT Username FROM admin  WHERE (Id = ir.IFrom)) AS Ifrom, (SELECT Username FROM admin AS admin_1 WHERE (Id = ir.RTo)) AS Rto, ir.RTime FROM book AS b INNER JOIN BR_Connection AS br ON br.ISBN = b.ISBN INNER JOIN Books_on_Read AS ir ON br.RFID = ir.BRFID INNER JOIN student AS s ON s.RFID = ir.SRFID WHERE (ir.RTime IS NULL) ORDER BY ir.ITime DESC, ir.RTime DESC");
    }

    protected void Generate_Report_Click(object sender, EventArgs e)
    {

    }

    protected void Records_Live_Click(object sender, EventArgs e)
    {
        Records_Live.Attributes["class"] = "text-dark mr-2 text-decoration-none border-secondary border-bottom";
        Records_History.Attributes["class"] = "text-dark mr-2 text-decoration-none border-secondary border-0";
        Table_Heading.InnerText = "Live";
        FillTableData("SELECT b.Name AS Book, b.ISBN, s.FName, s.LName, s.Enrollment_No, ir.ITime, (SELECT Username FROM admin  WHERE (Id = ir.IFrom)) AS Ifrom, (SELECT Username FROM admin AS admin_1 WHERE (Id = ir.RTo)) AS Rto, ir.RTime FROM book AS b INNER JOIN BR_Connection AS br ON br.ISBN = b.ISBN INNER JOIN Books_on_Read AS ir ON br.RFID = ir.BRFID INNER JOIN student AS s ON s.RFID = ir.SRFID WHERE (ir.RTime IS NULL) ORDER BY ir.ITime DESC, ir.RTime DESC");
    }

    protected void Records_History_Click(object sender, EventArgs e)
    {
        Records_Live.Attributes["class"] = "text-dark mr-2 text-decoration-none border-secondary border-0";
        Records_History.Attributes["class"] = "text-dark mr-2 text-decoration-none border-secondary border-bottom";
        Table_Heading.InnerText = "History";
        FillTableData("SELECT b.Name AS Book, b.ISBN, s.FName, s.LName, s.Enrollment_No, ir.ITime, (SELECT Username FROM admin  WHERE (Id = ir.IFrom)) AS Ifrom, (SELECT Username FROM admin AS admin_1 WHERE (Id = ir.RTo)) AS Rto, ir.RTime FROM book AS b INNER JOIN BR_Connection AS br ON br.ISBN = b.ISBN INNER JOIN Books_on_Read AS ir ON br.RFID = ir.BRFID INNER JOIN student AS s ON s.RFID = ir.SRFID WHERE (ir.RTime IS NOT NULL) ORDER BY ir.ITime DESC, ir.RTime DESC");
    }

    public void FillTableData(string qry)
    {
        con.Open();
        cmd = new SqlCommand(qry, con);
        dr = cmd.ExecuteReader();
        Table.Clear();
        int i = 0;
        for (; dr.Read(); i++)
        {
            Table.Append("<tr>");
            Table.Append("<td><abbr title=" + dr["ISBN"] + " class='text-decoration-none'><a href='Book-Profile?Book=" + dr["ISBN"] + "'  class=' text-decoration-none text-dark'>" + dr["Book"] + "</a></abbr></td>");
            Table.Append("<td><abbr title=" + dr["Enrollment_No"] + " class='text-decoration-none'><a href='Student-Profile?Student=" + dr["Enrollment_No"] + "' class='text-dark text-capitalize text-decoration-none'>" + (dr["fname"].ToString()) + " " + (dr["lname"].ToString()) + "</a></abbr></td>");
            Table.Append("<td>" + dr["ifrom"] + "</td>");
            Table.Append("<td>" + Convert.ToDateTime(dr["itime"]).ToString("MMM dd ,yyyy h:mm tt") + "</td>");
            if (!(dr["rto"] is DBNull))
            {
                Table.Append("<td>" + dr["rto"] + "</td>");
                Table.Append("<td>" + Convert.ToDateTime(dr["rtime"]).ToString("MMM dd ,yyyy h:mm tt") + "</td>");
            }
            else
            {
                Table.Append("<td class='text-danger'>Not Returned yet</td><td>-</td>");
            }
            Table.Append("</tr>");
        }
        TableData.Controls.Add(new Literal { Text = Table.ToString() });
        dr.Close();
    }
}