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
    }

    protected void Create_Account_Click(object sender, EventArgs e)
    {
        if (Password.Text == Confirm_Password.Text)
        {
            Alert_Match_Password.Visible = false;
            con.Open();
            cmd = new SqlCommand("select Id from admin where Email = '" + Admin_Email.Text + "'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
                cmd = new SqlCommand("insert into admin (Email, Password, Username, Add_Through, Add_Time) values('" + Email.Text + "', '" + Confirm_Password.Text + "', '" + Username.Text + "', " + dr[0] + ", '" + DateTime.Now + "')", con);

            dr.Close();
            cmd.ExecuteNonQuery();
            con.Close();
            Alert_Account_Created.Visible = true;
            Create_Account.Visible = false;
        }
        else
        {
            Alert_Match_Password.Visible = true;
            Confirm_Password.Focus();
        }
    }

    protected void Admin_Email_TextChange(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select Username from admin where Email = '" + Admin_Email.Text + "'", con);
        dr = cmd.ExecuteReader();
        if (!dr.Read())
        {
            Alert_Invailed_Email.Visible = true;
            Admin_Email.Focus();
        }
        else
        {
            Alert_Invailed_Email.Visible = false;
            Admin_Password.ReadOnly = false;
            Admin_Email.ReadOnly = true;
            Admin_Password.Focus();
        }

        con.Close();
    }

    protected void Admin_Password_TextChange(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select Username from admin where Password = '" + Admin_Password.Text + "' collate sql_latin1_general_cp1_cs_as", con);
        dr = cmd.ExecuteReader();
        if (!dr.Read())
        {
            Alert_Invailed_Password.Visible = true;
            Admin_Password.Focus();
        }
        else
        {
            Alert_Invailed_Password.Visible = false;
            Admin_Password.ReadOnly = true;
            Email.ReadOnly = false;
            Email.Focus();
        }
        con.Close();
    }

    protected void Email_TextChange(object sender, EventArgs e)
    {
        con.Open();
        cmd = new SqlCommand("select Username from admin where Email = '" + Email.Text + "' collate sql_latin1_general_cp1_cs_as", con);
        dr = cmd.ExecuteReader();
        if (!dr.Read())
        {
            Alert_Email_Exist.Visible = false;
            Username.ReadOnly = Password.ReadOnly = Confirm_Password.ReadOnly = false;
            Create_Account.Visible = true;
            Username.Focus();
        }
        else
        {
            Alert_Email_Exist.Visible = true;
            Username.Text = Password.Text = Confirm_Password.Text = "";
            Username.ReadOnly = Password.ReadOnly = Confirm_Password.ReadOnly = true;
            Create_Account.Visible = false;
            Email.Focus();
        }
    }
}