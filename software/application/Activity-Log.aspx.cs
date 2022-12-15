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

    public void FillData()
    {
        HttpCookie AdminInfo = Request.Cookies["AdminInfo"];
        if (AdminInfo != null)
        {
            con.Open();
            cmd = new SqlCommand("select (select count(IFrom) from Issue_return where ifrom = " + AdminInfo["Id"] + ") as IFrom, (select count(Rto) from Issue_return where rto = " + AdminInfo["Id"] + ") as RTo, (select sum(charge) from Issue_return where ifrom = " + AdminInfo["Id"] + " or rto= " + AdminInfo["Id"] + ") as Charges, (select distinct count(RFID) from br_connection where add_by = " + AdminInfo["Id"] + ") as AddBooks, (select count(add_by) from book_requests where add_by = " + AdminInfo["Id"] + ") as AddRequests, (select count(add_by) from book_suggestions where add_by = " + AdminInfo["Id"] + ") as AddSuggestions", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Total_Books_Issued.Text = dr["IFrom"].ToString();
                Total_Books_Returned.Text = dr["RTo"].ToString();
                Total_Charges_Taken.Text = dr["Charges"].ToString();
                Total_Books_Added.Text = dr["AddBooks"].ToString();
                Total_Book_Requests_Added.Text = dr["AddRequests"].ToString();
                Total_Book_Suggestions_Added.Text = dr["AddSuggestions"].ToString();
            }
            dr.Close();
            con.Close();
        }
        else
            Response.Redirect("Login.aspx");
    }

}