using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO.Ports;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\asp practice\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;User Instance=True");
    SqlCommand cmd;
    SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            FillProfileData();
    
        FillPortData();
    }

    protected void Change_Port_Click(object sender, EventArgs e)
    {
        Div_Change_Port.Visible = true;
        Change_Port.Visible = false;
    }
    protected void Set_Port_Click(object sender, EventArgs e)
    {
        if (AvailablePorts.SelectedValue == "")
        {
            Change_Port.Visible = false;
        }
        else
        {
            if (Curr_COM_Port.Text != AvailablePorts.SelectedItem.ToString())
            {
                Response.Cookies["COMPort"].Value = AvailablePorts.SelectedItem.ToString();
                Response.Cookies["COMPort"].Expires = DateTime.MaxValue;
                FillPortData();
            }
            Change_Port.Visible = true;
        }
        Div_Change_Port.Visible = false;
    }

    protected void Change_Email_Click(object sender, EventArgs e)
    {
        Div_Change_Email.Visible = true;
        Change_Email.Visible = false;
    }
    protected void Set_Email_Click(object sender, EventArgs e)
    {
            Alert_SE_Email.Text = Alert_SE_Password.Text = "";
            con.Open();
            cmd = new SqlCommand("select id from admin where password='" + Check_Password2.Text + "' and id=" + Admin_Id.Text + "", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                try
                {
                    var EmailChecked = new System.Net.Mail.MailAddress(Check_New_Email.Text);
                    cmd = new SqlCommand("select id from admin where email = '" + Check_New_Email.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        cmd = new SqlCommand("update admin set Email = '" + Check_New_Email.Text + "' where id=" + Admin_Id.Text + "", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        FillProfileData();
                        Div_Change_Email.Visible = false;
                        Change_Email.Visible = true;
                        Check_New_Email.Text = Check_Password.Text = "";
                    }
                    else
                    {
                        if (Check_New_Email.Text != "")
                            Alert_SE_Email.Text = "Email Already Exists";
                        else
                            Alert_SE_Email.Text = "Invailed Email";
                    }
                }
                catch
                {
                    Alert_SE_Email.Text = "Invailed Email";
                }
            }
            else
                Alert_SE_Password.Text = "Invailed Password";
            dr.Close();
            con.Close();
    }

    protected void Change_Username_Click(object sender, EventArgs e)
    {
        Div_Change_Username.Visible = true;
        Change_Username.Visible = false;
    }

    protected void Set_Username_Click(object sender, EventArgs e)
    {
        Alert_SU_Password.Text = "";
        con.Open();
        cmd = new SqlCommand("select id from admin where password='" + Check_Password2.Text + "' and id="+Admin_Id.Text+"", con);
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            dr.Close();
            if (Check_New_Username.Text != "")
            {
                cmd = new SqlCommand("update admin set Username = '" + Check_New_Username.Text + "' where id=" + Admin_Id.Text + "", con);
                cmd.ExecuteNonQuery();
                con.Close();
                FillProfileData();
                Div_Change_Username.Visible = false;
                Change_Username.Visible = true;
                Check_New_Username.Text = Check_Password2.Text = "";
            }
            else
                Alert_SU_Username.Text = "Invailed Username";
        }
        else
            Alert_SU_Password.Text = "Invailed Password";
        dr.Close();
        con.Close();
    }

    protected void Change_Password_Click(object sender, EventArgs e)
    {
        Div_Change_Password.Visible = true;
        Change_Password.Visible = false;
    }

    protected void Set_Password_Click(object sender, EventArgs e)
    {
        Alert_SP_OPassword.Text = Alert_SP_NPassword.Text = Alert_SP_CPassword.Text = "";
        con.Open();
        cmd = new SqlCommand("select id from admin where id=" + Admin_Id.Text + " and password='" + Old_Password.Text + "' collate sql_latin1_general_cp1_cs_as", con);
        dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            dr.Close();
            if (New_Password.Text != "")
            {
                if (Confirm_Password.Text != "")
                {
                    if (New_Password.Text == Confirm_Password.Text)
                    {
                        cmd = new SqlCommand("update admin set Password = '" + Confirm_Password.Text + "' where id=" + Admin_Id.Text + "", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        FillProfileData();
                        Div_Change_Password.Visible = false;
                        Change_Password.Visible = true;
                        Old_Password.Text = New_Password.Text = Confirm_Password.Text = "";
                    }
                    else
                        Alert_SP_CPassword.Text = "Password doesn't match";
                }
                else
                    Alert_SP_CPassword.Text = "Invailed Confirm Password";
            }
            else
                Alert_SP_NPassword.Text = "Invailed New Password";
        }
        else
            Alert_SP_OPassword.Text = "Invailed Password";
        dr.Close();
        con.Close();
    }

    public void FillPortData() 
    {
        HttpCookie COMPort = Request.Cookies["COMPort"];
        if (COMPort != null)
            Curr_COM_Port.Text = Request.Cookies["COMPort"].Value;
        else 
            Curr_COM_Port.Text = "Not Selected";

        AvailablePorts.Items.Clear();
        foreach (string s in SerialPort.GetPortNames())
        {
            AvailablePorts.Items.Add(s);
        }
        if (AvailablePorts.Items.Count == 0)
        {
            Curr_COM_Port.Text += " (Device Not Connected)";
            Change_Port.Visible = false;
        }
        else
            Set_Port.Visible = true;
    }

    public void FillProfileData()
    {
        HttpCookie AdminInfo = Request.Cookies["AdminInfo"];
        if (AdminInfo != null)
        {
            Admin_Id.Text = AdminInfo["Id"].ToString();
            con.Open();
            cmd = new SqlCommand("select Email,Username, Password from admin where id="+Admin_Id.Text+"", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Email.Text = dr["Email"].ToString();
                AdminInfo["Username"] = Username.Text = dr["Username"].ToString();
                Password.Text = "";
                string pass = dr["Password"].ToString();
                for (int i = 0; i < pass.Length; i++)
                    Password.Text += "*";
            }
            dr.Close();
            con.Close();
        }
        else
            Response.Redirect("Login.aspx");
    }
}