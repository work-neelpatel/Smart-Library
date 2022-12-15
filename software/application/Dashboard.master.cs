using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

public partial class Dashboard : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["Admin"] == null)
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "alert('Admin Data Problem','Signin');", true);
        else
        {
            Admin.Text = Session["Admin"].ToString();
            string A = Session["Admin"].ToString();
            Session.Remove("Admin");
            Session["Admin"] = A;
        }
    }

}
