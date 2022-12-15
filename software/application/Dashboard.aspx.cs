using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\asp practice\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            FillData();
    }

    protected void Transaction_Time_Change(object sender, EventArgs e)
    {
    }

    public void FillData()
    {
        con.Open();
        cmd = new SqlCommand("select (select distinct count(RFID) from br_connection) as Books,(select distinct count(BRFID) from Books_on_read where rtime is null) as BooksOnRead, (select count(BRFID) from issue_return) as Transactions, (SELECT COUNT(RFID) AS Expr1 FROM br_connection WHERE  (YEAR(Add_Time) = YEAR({ fn CURDATE() }))) as BuyBooks,(SELECT COUNT(ISBN) AS Expr1 FROM Book_Requests) as BookRequets, (SELECT COUNT(Student_Enrollment_No) AS Expr1 FROM Book_Suggestions) as BookSuggestions", con);
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            Total_Books.InnerText = dr["Books"].ToString();
            Books_on_Read.InnerText = "(Live)  "+dr["BooksOnRead"].ToString();
            Books_buy_tYear.InnerText = "("+DateTime.Now.ToString("yyyy")+")  " + dr["BuyBooks"].ToString();
            Total_Transactions.InnerText = dr["Transactions"].ToString();
            Book_Requests.InnerText = dr["BookRequets"].ToString();
            Book_Suggestion.InnerText = dr["BookSuggestions"].ToString();
        }
        con.Close();
    }

    protected void Remove1_Click(object sender, EventArgs e)
    {
        div_BTM.Visible = false;
    }

    protected void Remove2_Click(object sender, EventArgs e)
    {
        div_BTW.Visible = false;
    }
}