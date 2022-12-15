using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\SSIP PROJECT\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static List<string> AutoSuggestAuthor(string author, string author2, string author3)
    {
        List<string> result = new List<string>();

        using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\SSIP PROJECT\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True"))
        {
            using (SqlCommand cmd = new SqlCommand("select DISTINCT aname from author where aname LIKE '" + author + "%' and aname!='" + author2 + "' and aname!='" + author3 + "'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["aname"].ToString());
                }
                return result;
            }
        }
    }

    [WebMethod]
    public static List<string> AutoSuggestSubject(string subject)
    {
        List<string> result = new List<string>();

        using (SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\SSIP PROJECT\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True"))
        {
            using (SqlCommand cmd = new SqlCommand("select DISTINCT sname from subject where sname LIKE '" + subject + "%'", con))
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr["sname"].ToString());
                }
                return result;
            }
        }
    }

    protected void GetSemester(object sender, EventArgs e)
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select semester from subject where sname = '" + Subject.Text + "'", con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            Semester.ReadOnly = true;
            Semester.Text = dr[0].ToString();
        }
        else
        {
            Semester.ReadOnly = false;
            Semester.Text = "";
        }
        dr.Close();
        con.Close();
    }

}