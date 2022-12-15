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
    string Book, Copy;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Book = Request.QueryString["Book"];
            Copy = Request.QueryString["Copy"];
            if (Book == "" && Book != null)
                Response.Redirect("Books");
            else
                FillData();

            if (Copy != null && Copy != "")
                FillBookTransactionsData(Copy);
        }
    }

    protected void Generate_Report_Click(object sender, EventArgs e)
    {

    }

    public void FillData()
    {
        con.Open();
        cmd = new SqlCommand("SELECT b.Name, b.Edition, p.Name AS Publisher, f.Name AS field, s.Semester, s.Name AS subject, (select count(BRFID) from book as b inner join br_connection as br on br.ISBN = b.ISBN inner join issue_return as ir on ir.BRFID = br.RFID) as Transactions, (select sum(charge) from book as b inner join br_connection as br on br.ISBN = b.ISBN inner join issue_return as ir on ir.BRFID = br.RFID) as Charges, (select count(RFID) from br_connection where ISBN = b.ISBN) as Copies, (select count(RFID) from br_connection where ISBN = b.ISBN and available = 0) as Issued_Copies  FROM book AS b INNER JOIN publisher AS p ON p.Id = b.Publisher INNER JOIN subject AS s ON s.Id = b.Subject INNER JOIN Fields AS f ON f.Id = s.Field WHERE (b.ISBN = '" + Book + "')", con);
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            ISBN_txt.Text = ISBN.Text = Book;
            Name_txt.Text = Name.Text = dr["Name"].ToString();
            Field_txt.Text = Field.Text = dr["field"].ToString();
            Semester_txt.Text = Semester.Text = dr["semester"].ToString();
            Subject_txt.Text = Subject.Text = dr["subject"].ToString();

            Edition_txt.Text = Edition.Text = dr["edition"].ToString();
            Publisher_txt.Text = Publisher.Text = dr["publisher"].ToString();
            Copies_txt.Text = Copies.Text = dr["Copies"].ToString();
            Issued_Copies_txt.Text = Issued_Copies.Text = dr["Issued_Copies"].ToString();


            Charges.Text = dr["Charges"].ToString() ;
            Transactions.Text = dr["Transactions"].ToString();
        }
        else
        {
            Alert_BookFound.Visible = true;
            Body.Visible = false;
        }
        dr.Close();
        con.Close();
        FillBookCopiesData();
    }

    public void FillBookCopiesData()
    {
        con.Open();
        cmd = new SqlCommand("SELECT RFID, Rack_No, Available, br.Add_Time, a.username, (select count(BRFID) from book as b inner join br_connection as br on br.ISBN = b.ISBN inner join issue_return as ir on ir.BRFID = br.RFID) as Transactions, (select sum(charge) from book as b inner join br_connection as br on br.ISBN = b.ISBN inner join issue_return as ir on ir.BRFID = br.RFID) as Charges FROM BR_Connection as br inner join admin as a on a.id= add_by where ISBN='" + ISBN.Text + "' order by br.Add_Time desc", con);
        dr = cmd.ExecuteReader();
        Table.Clear();
        int i = 0;
        for (; dr.Read(); i++)
        {
            Table.Append("<tr>");
            Table.Append("<td><i class='fa fa-pencil mr-2 pr-2 text-primary border-right border-secondary'></i><a href='Book-Profile?Book=" + ISBN.Text + "&Copy="+ dr["RFID"] +"'  class=' text-decoration-none text-dark'>" + dr["RFID"] + "</a></td>");
            Table.Append("<td>" + dr["Rack_No"] + "</td>");
            Table.Append("<td>" + dr["Username"] + "</td>");
            Table.Append("<td>" + Convert.ToDateTime(dr["Add_Time"]).ToString("MMM dd ,yyyy h:mm tt") + "</td>");
            Table.Append("<td><a href='Book-Profile?Book=" + ISBN_txt.Text + "&Copy=" + dr["RFID"] + "'  class=' text-decoration-none text-dark'>" + dr["Transactions"] + "</a></td>");
            Table.Append("<td>" + dr["Charges"] + "</td>");
            if (dr["Available"].ToString() == "0")
                Table.Append("<td class='text-danger'>Not Available</td>");
            else
                Table.Append("<td class='text-primary'>Available</td>");
            Table.Append("</tr>");
        }
        BookCopies_Data.Controls.Add(new Literal { Text = Table.ToString() });
        dr.Close();
        con.Close();

    }

    public void FillBookTransactionsData(string RFID)
    {
        BookTransactions_Div.Visible = true;
        con.Open();
        cmd = new SqlCommand("SELECT s.FName, s.LName, s.Enrollment_No, ir.ITime, ir.RTime, ir.Charge, (SELECT Username FROM admin WHERE (Id = ir.IFrom)) AS Ifrom, (SELECT Username FROM admin AS admin_1 WHERE (Id = ir.RTo)) as Rto, (SELECT Remark FROM charges WHERE (Id = ir.Charge_Remark)) AS Remark FROM BR_Connection AS br INNER JOIN issue_return AS ir ON br.RFID = ir.BRFID INNER JOIN student AS s ON s.RFID = ir.SRFID WHERE (br.RFID = '"+RFID+"') ORDER BY ir.ITime DESC, ir.RTime DESC", con);
        dr = cmd.ExecuteReader();
        Table.Clear();
        int i = 0;
        for (; dr.Read(); i++)
        {
            Table.Append("<tr>");
            Table.Append("<td><abbr title=" + dr["Enrollment_no"] + " class='text-decoration-none'><a href='Student-Profile?Student=" + dr["Enrollment_no"] + "'  class=' text-decoration-none text-dark'>" + dr["fname"] + " " + dr["lname"] + "</a></abbr></td>");
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
        BookTransaction_Data.Controls.Add(new Literal { Text = Table.ToString() });
        if (i == 0)
        {
            Table.Clear();
            BookTransactions_Div.Visible = false;
            Alert_CopyFound.Visible=true;
        }
        dr.Close();
        con.Close();
    }

    protected void Update_Details_btn_Click(object sender, EventArgs e)
    {
        Divlbl1.Visible = Divlbl2.Visible = false;
        Divtxt1.Visible = Divtxt2.Visible = true;
        Div_btn.Visible = false;
        Done1_btn.Visible = true;
    }

    protected void Update_RFID_btn_Click(object sender, EventArgs e)
    {
        Div_btn.Visible = false;
    }

    protected void Done1_btn_Click(object sender, EventArgs e)
    {
        Divlbl1.Visible = Divlbl2.Visible = true;
        Divtxt1.Visible = Divtxt2.Visible = false;
        Div_btn.Visible = true;
        Done1_btn.Visible = false;
    }

    protected void Done2_btn_Click(object sender, EventArgs e)
    {
        Div_btn.Visible = true;
    }
}