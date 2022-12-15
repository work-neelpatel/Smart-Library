using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;

/// <summary>
/// Summary description for Service
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService {

    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetAuthors(string author, string author2, string author3)
    {
        List<string> Authors = new List<string>();
        using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\asp practice\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;User Instance=True"))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select DISTINCT Name, Id from author where Name LIKE '" + author + "%' and Name !='" + author2 + "' and Name !='" + author3 + "' ";
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Authors.Add(string.Format("{0}-{1}", sdr["Name"], sdr["Id"]));
                    }
                }
                conn.Close();
            }
            return Authors.ToArray();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetSubjects(string subject, string field)
    {
        List<string> subjects = new List<string>();
        using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\asp practice\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;User Instance=True"))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select name,id from subject where name like '" + subject + "%' and field=" + field + " and semester is null";
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        subjects.Add(string.Format("{0}-{1}", sdr["Name"], sdr["Id"]));
                    }
                }
                conn.Close();
            }
            return subjects.ToArray();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string[] GetPublishers(string prefix)
    {
        List<string> Publishers = new List<string>();
        using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\asp practice\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;User Instance=True"))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Name, Id from Publisher where Name like '" + prefix + "%'";
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Publishers.Add(string.Format("{0}-{1}", sdr["Name"], sdr["Id"]));
                    }
                }
                conn.Close();
            }
            return Publishers.ToArray();
        }
    }

}
