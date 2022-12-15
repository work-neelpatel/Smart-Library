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
            FillData();
    }

    protected void Generate_Report_Click(object sender, EventArgs e)
    {

    }

    public void FillData()
    {
        con.Open();
        cmd = new SqlCommand("select Name, Id from Fields", con);
        Field.DataSource = cmd.ExecuteReader();
        Field.DataTextField = "Name";
        Field.DataValueField = "Id";
        Field.DataBind();
        con.Close();

        FillSubject();
    }

    protected void Field_Change(object sender, EventArgs e)
    {
        FillSubject();
    }

    protected void Semester_Change(object sender, EventArgs e)
    {
        FillSubject();
    }

    public void FillSubject()
    {
        if (Semester.SelectedValue != "None")
        {
            Div_Subject_drpdwn.Visible = true;
            Div_Subject_txt.Visible = false;
            con.Open();
            cmd = new SqlCommand("select Name, Id from Subject where field = '" + Field.SelectedValue + "' and semester='" + Semester.SelectedValue + "'", con);
            Subject.DataSource = cmd.ExecuteReader();
            Subject.DataTextField = "Name";
            Subject.DataValueField = "Id";
            Subject.DataBind();
            con.Close();
            if (Subject.Items.Count == 0)
                Book_Name.ReadOnly = true;
            else
                Book_Name.ReadOnly = false;
        }
        else
        {
            Div_Subject_drpdwn.Visible = false;
            Div_Subject_txt.Visible = true;
        }
    }

    protected void Subject_TextChanged(object sender, EventArgs e)
    {
        if (Subject.Text != "")
        {
            Book_Name.Focus();
            con.Open();
            cmd = new SqlCommand("select id, name from subject where name like '" + Subject_txt.Text + "%'", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (Subject_txt.Text == dr["name"].ToString())
                {
                    Subject_Id.InnerText = dr["Id"].ToString();
//                    Alert_New_Subject.Text = Alert_Suggest_Subject.Text = "";
                }
                else
                {
                    Subject_Id.InnerText = "";
                    Alert_Suggest_Subject.Text = "Do you mean " + dr["name"].ToString() + "";
//                    Alert_New_Subject.Text = "That's a New Subject?";
                }
            }
            dr.Close();
            con.Close();
        }
        else
            Subject_Id.InnerText = Alert_Suggest_Subject.Text = "";
    }

    protected void Subject_Suuggest(object sender, EventArgs e)
    {
        Subject_txt.Text = Alert_Suggest_Subject.Text.Replace("Do you mean ", "");
        Subject_TextChanged(null, null);
    }

    protected void Create_Subject(object sender, EventArgs e)
    {
    }

}