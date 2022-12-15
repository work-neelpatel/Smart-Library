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
            FillTableData();
    }

    protected void Generate_Report_Click(object sender, EventArgs e)
    {

    }

    public void FillTableData()
    {
        con.Open();
        cmd = new SqlCommand("SELECT DATEDIFF(day, CONVERT(date, ir.ITime), { fn CURDATE() }) AS days, b.ISBN, b.Name AS book, s.Enrollment_No, s.FName, s.LName, ir.ITime, a.username FROM book as b inner join br_connection as br on br.ISBN = b.ISBN inner join issue_return AS ir ON br.RFID = ir.BRFID inner join admin as a on ir.ifrom = a.id INNER JOIN student AS s ON ir.SRFID = s.RFID WHERE (ir.RTime IS NULL) AND (DATEDIFF(day, CONVERT(date, ir.ITime), { fn CURDATE() }) > 7)", con);
        dr = cmd.ExecuteReader();
        Table.Clear();
        int i = 0;
        for (; dr.Read(); i++)
        {
            Table.Append("<tr>");
            Table.Append("<td><a href='Book-Profile?Book=" + dr["ISBN"] + "'  class=' text-decoration-none text-dark'>" + dr["Book"] + "</a></td>");
            Table.Append("<td><a href='Student-Profile?Student=" + dr["Enrollment_No"] + "' class='text-dark text-capitalize text-decoration-none'>" + (dr["fname"].ToString()) + " " + (dr["lname"].ToString()) + "</a></td>");
            Table.Append("<td>" + dr["username"] + "</td>");
            Table.Append("<td>" + Convert.ToDateTime(dr["itime"]).ToString("MMM dd ,yyyy h:mm tt") + "</td>");
            Table.Append("<td class='text-danger'>" + dr["days"] + "</td>");
            Table.Append("</tr>");
        }
        TableData.Controls.Add(new Literal { Text = Table.ToString() });
        dr.Close();
        con.Close();
    }
}