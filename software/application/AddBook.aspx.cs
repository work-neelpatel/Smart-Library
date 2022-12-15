using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO.Ports;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Web.Services;

public partial class Default2 : System.Web.UI.Page
{
    static SerialPort port = new SerialPort("COM4", 9600);
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\SSIP PROJECT\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            check();
        }
    }
    public void check()
    {
            try
            {
                port.ReadTimeout = 5000;
                port.Open();
                string data = port.ReadLine();
                data = data.Trim();
                port.Close();
                con.Open();

                string sqlselect = "select * from book where RFID = '" + data + "'";
                SqlCommand cmd = new SqlCommand(sqlselect, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (!dr.Read())
                {
                    dr.Close();
                    RFID.Text = data;
                }
                else
                {
                    dr.Close();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Book Already Exists','Books');", true);
                }
            }
            catch (TimeoutException)
            {
                port.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Data Read Timeout','Books');", true);
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

}