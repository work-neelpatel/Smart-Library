using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.SqlClient;

public partial class Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie AdminInfo = Request.Cookies["AdminInfo"];
        if (AdminInfo != null)
        {
            AdminName.Text = AdminInfo["Username"].ToString();
        }
        else
            Response.Redirect("Login.aspx");
    }
}
