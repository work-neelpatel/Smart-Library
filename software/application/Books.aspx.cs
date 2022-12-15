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

    string name , qry;
    string n1, n2, n3, n4, n5,n6, q1, q2, q3, q4 ,q5, q6;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            FillData();
    }

    public void FillData()
    {
        con.Open();
        cmd = new SqlCommand("select Name, Id from Fields", con);
        Field.DataSource = cmd.ExecuteReader();
        Field.DataTextField = "Name";
        Field.DataValueField = "Id";
        Field.DataBind();
        Field.Items.Add("All");
        con.Close();

        Semester.SelectedValue = "All";
        BookState.SelectedValue = "All";

        FillSubject();
    }

    protected void Generate_Report_Click(object sender, EventArgs e)
    {

    }

    protected void Field_Change(object sender, EventArgs e)
    {
        FillSubject();
    }

    protected void Semester_Change(object sender, EventArgs e)
    {
        FillSubject();
    }

    protected void Search_Book_Click(object sender, EventArgs e)
    {
        FillTableData();
    }

    public void FillSubject()
    {
        if (Semester.SelectedValue == "None")
        {
            if (Field.SelectedValue == "All")
                qry = "where s.semester is null";
            else
                qry = "where s.semester is null and f.Name = '" + Field.SelectedItem + "' ";
        }
        else if (Semester.SelectedValue != "All")
        {
            if (Field.SelectedValue == "All")
                qry = "where s.semester=" + Semester.SelectedValue + "";
            else
                qry = "where s.semester=" + Semester.SelectedValue + " and f.Name = '" + Field.SelectedItem + "'";
        }
        else
        {
            if (Field.SelectedValue == "All")
                qry = "select Name , Id from subject where Id is null";
            else
                qry = "where f.Name = '" + Field.SelectedItem + "'";        
        }

        con.Open();
        cmd = new SqlCommand("select s.Name, s.Id from Subject as s inner join Fields as f on s.Field = f.Id " + qry, con);
        Subject.DataSource = cmd.ExecuteReader();
        Subject.DataTextField = "Name";
        Subject.DataValueField = "Id";
        Subject.DataBind();
        con.Close();
        if (Subject.Items.Count > 1)
            Subject.Items.Add("All");
        Subject.SelectedIndex = Subject.Items.Count - 1;

        FillTableData();
    }

    public void FillTableData()
    {
        //Create flexible query
        if (Field.SelectedValue != "All")
        {
            q1 = "s.Field=" + Field.SelectedValue + " ";
            n1 = "_" + Field.SelectedValue + "";
        }

        if (Semester.SelectedValue != "All")
        {
            if (Semester.SelectedValue == "None")
            {
                q2 = "and s.semester is null ";
                n2 = "Sem-" + Semester.SelectedValue + "";
            }
            else
            {
                q2 = "and s.semester=" + Semester.SelectedValue + " ";
                n2 = "Sem-" + Semester.SelectedValue + "";
            }
        }

        if (Subject.SelectedValue != "All")
        {
            q3 = "and s.name='" + Subject.SelectedItem + "' ";
            n3 = "_Sub-" + Subject.SelectedItem + "";
        }

        if (Publisher.Text != "")
        {
            q4 = "and p.name = '" + Publisher.Text + "' ";
            n4 = "_Pub-" + Publisher.Text + "";
        }

        if (Author.Text != "")
        {
            q5 = "and a.name='" + Author.Text + "'";
            n5 = "_Aut-" + Author.Text + "";
        }

        if (BookState.SelectedValue != "All")
        {
            if (BookState.SelectedValue == "Not Available")
            {
                q6 = "and (select count(RFID) from br_connection where ISBN = b.ISBN) = (select count(RFID) from br_connection where ISBN = b.ISBN and available = 0) ";
                n6 = "BookState-" + BookState.SelectedValue + "";
            }
            else
            {
                q6 = "and (select count(RFID) from br_connection where ISBN = b.ISBN) > (select count(RFID) from br_connection where ISBN = b.ISBN and available = 0) ";
                n6 = "BookState-" + BookState.SelectedValue + "";
            }        
        }

        qry = "SELECT DISTINCT b.ISBN, b.Name AS Book, br.Rack_No, f.Name AS Field, s.Semester, s.Name AS Subject, b.Edition, (SELECT COUNT(BRFID) AS Expr1 FROM issue_return as ir inner join br_connection as br on br.ISBN = b.ISBN WHERE   (BRFID = br.RFID)) AS Transactions, (select count(RFID) from br_connection where ISBN = b.ISBN) as Copies, (select count(RFID) from br_connection where ISBN = b.ISBN and available = 0) as Issued_Copies FROM book AS b  INNER JOIN BR_Connection AS br ON br.ISBN = b.ISBN INNER JOIN subject AS s ON b.Subject = s.Id INNER JOIN publisher AS p ON b.Publisher = p.Id INNER JOIN Fields AS f ON s.Field = f.Id INNER JOIN ba_connection AS ba ON ba.BRFID = br.RFID INNER JOIN author AS a ON a.Id = ba.Author where " + q1 + "" + q2 + "" + q3 + "" + q4 + "" + q5 + "" + q6 + " ";
        name = "Books " + n3 + "" + n2 + "" + n1 + "" + n4 + "" + n5 + "" + n6 + "";
        qry = qry.Replace("where and", "where ");
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
            Table.Append("<td>" + dr["ISBN"] + "</td>");
            Table.Append("<td><a href='Book-Profile?Book=" + dr["ISBN"] + "'  class=' text-decoration-none text-dark'>" + dr["Book"] + "</a></td>");
            Table.Append("<td>" + dr["Field"] + "</td>");
            Table.Append("<td>" + dr["Semester"] + "</td>");
            Table.Append("<td>" + dr["Subject"] + "</td>");
            Table.Append("<td>" + dr["Edition"] + "</td>");
            Table.Append("<td>" + dr["Rack_No"] + "</td>");
            if (Convert.ToInt16(dr["Issued_Copies"]) == Convert.ToInt16(dr["Copies"]))
                Table.Append("<td class='text-danger'>" + (Convert.ToInt16(dr["Copies"]) - Convert.ToInt16(dr["Issued_Copies"])) + " <span class='text-secondary'>of</span> <span class='text-primary'>" + dr["Copies"] + "</span></td>");
            else
                Table.Append("<td>" + (Convert.ToInt16(dr["Copies"]) - Convert.ToInt16(dr["Issued_Copies"])) + " <span class='text-secondary'>of</span> <span class='text-primary'>" + dr["Copies"] + "</span></td>");
            Table.Append("<td>" + dr["Transactions"] + "</td>");
            Table.Append("</tr>");
        }
        TableData.Controls.Add(new Literal { Text = Table.ToString() });
        dr.Close();
        con.Close();
    }
}