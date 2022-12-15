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
    string n1, q1;
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
        if (IsPostBack)
        {
            if (Year.Text != "")
            {
                if (Year.Text.Length == 2)
                    Year.Text = "20" + Year.Text;
                q1 = "where (YEAR(br.Add_Time) = '" + Year.Text + "')";
                n1 = "_Year-" + Year.Text + "";
            }

            qry = "SELECT DISTINCT b.ISBN, b.Name AS Book, br.RFID, br.Add_Time, a.Username, f.Name AS Field, s.Semester, s.Name AS Subject FROM     book AS b inner join br_connection as br on b.ISBN = br.ISBN INNER JOIN subject AS s ON b.Subject = s.Id INNER JOIN Fields AS f ON s.Field = f.Id INNER JOIN admin AS a ON a.Id = br.Add_By " + q1 + " order by br.Add_Time";
            name = "Bought Books History " + n1 + "";

        }
        else
        {
            qry = "SELECT DISTINCT b.ISBN, b.Name AS Book, br.Add_Time, a.Username, f.Name AS Field, s.Semester, s.Name AS Subject FROM     book AS b inner join br_connection as br on b.ISBN = br.ISBN INNER JOIN subject AS s ON b.Subject = s.Id INNER JOIN Fields AS f ON s.Field = f.Id INNER JOIN admin AS a ON a.Id = br.Add_By order by br.Add_Time desc";
            name = "Transaction History";
        }
        cmd = new SqlCommand(qry, con);
        dr = cmd.ExecuteReader();
        Table.Clear();
        int i = 0;
        for (; dr.Read(); i++)
        {
            Table.Append("<tr>");
            Table.Append("<td><abbr title=" + dr["ISBN"] + " class='text-decoration-none'><a href='Book-Profile?Book=" + dr["ISBN"] + "'  class=' text-decoration-none text-dark'>" + dr["Book"] + "</a></abbr></td>");
            Table.Append("<td><a href='Book-Profile?Book=" + dr["ISBN"] + "&Copy=" + dr["RFID"] + "'  class=' text-decoration-none text-dark'>" + dr["RFID"] + "</a></td>");
            Table.Append("<td>" + dr["Field"] + "</td>");
            Table.Append("<td>" + dr["semester"] + "</td>");
            Table.Append("<td>" + dr["subject"] + "</td>");
            Table.Append("<td>" + Convert.ToDateTime(dr["Add_Time"]).ToString("MMMM dd,yyyy") + "</td>");
            Table.Append("<td>" + dr["username"] + "</td>");
            Table.Append("</tr>");
        }
        TableData.Controls.Add(new Literal { Text = Table.ToString() });
        dr.Close();
        con.Close();
    }
}