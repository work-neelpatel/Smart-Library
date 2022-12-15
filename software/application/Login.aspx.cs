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
        if(!IsPostBack)
            Response.Cookies.Remove("AdminInfo");
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select Id, Username from admin where Password = '" + Password.Text + "' collate sql_latin1_general_cp1_cs_as", con);
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            Alert_Invailed_Password.Visible = false;

            HttpCookie AdminInfo = new HttpCookie("AdminInfo");
            AdminInfo["Id"] = dr[0].ToString();
            AdminInfo["Username"] = dr[1].ToString();
            AdminInfo.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(AdminInfo);
 
            Response.Redirect("Dashboard.aspx");
        }
        else
        {
            Alert_Invailed_Password.Visible = true;
        }
        con.Close();
    }

    protected void Email_TextChange(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select Username from admin where Email = '" + Email.Text + "'", con);
        dr = cmd.ExecuteReader();
        if (!dr.Read())
        {
            Alert_Invailed_Email.Visible = true;
            Password.ReadOnly = true;
            Login.Visible = false;
            Email.Focus();
        }
        else 
        {
            Alert_Invailed_Email.Visible = false;
            Password.ReadOnly = false;
            Login.Visible = true;
            Password.Focus();
        }
        con.Close();
    }
}