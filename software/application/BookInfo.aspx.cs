using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

public partial class Default2 : System.Web.UI.Page
{
    string RFID, Upd;
    StringBuilder htmlTable = new StringBuilder();
    SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=E:\SSIP PROJECT\LJ Smart Library\app_data\Smartlibrary.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
    protected void Page_Load(object sender, EventArgs e)
    {
            RFID = Request.QueryString["Book"];
            if(RFID==null)
                Response.Redirect("Books");
            else
            {
                HideTextbox();
                HideButton();
                FillData();
            }

            Upd = Request.QueryString["Update"];
            if (Upd == "1")
            {
                con.Open();
                string sql = "select * from book where RFID = '"+RFID+"' and available = 1";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    HideLabel();
                    ShowButton();
                }
                else
                    notice.InnerText = "Notice : You can Update Book Details Only when Book is Available.";
            }
    }

    public void FillTblData()
    {
        con.Open();
        string sql="SELECT b.bname AS Book,s.RFID, s.fname AS StudentF ,s.lname AS StudentL, ir.itime AS itime, ir.ifrom AS ifrom, ir.rtime AS rtime, ir.rto AS rto, ir.charges AS Charges, remark AS remark FROM     issue_return AS ir INNER JOIN book b ON b.RFID = ir.BRFID INNER JOIN student s ON s.RFID = ir.SRFID where b.RFID='" + RFID + "' order by ir.itime desc";
        SqlCommand cmd = new SqlCommand(sql, con);
        SqlDataReader dr = cmd.ExecuteReader();
        int i = 0;
        for (; dr.Read(); i++)
        {
            data.Text = "";
            htmlTable.Append("<tr>");
            htmlTable.Append("<td><a href='StudentInfo?Student=" + dr["RFID"] + "'  style='color:#000'>" + dr["StudentF"] + " " + dr["StudentL"] + "</a></td>");
            htmlTable.Append("<td>" + dr["ifrom"] + "</td>");
            htmlTable.Append("<td>" + Convert.ToDateTime(dr["itime"]).ToString("MMMM dd ,yyyy HH:MM") + "</td>");
            if (!(dr["rto"] is DBNull))
            {
                htmlTable.Append("<td>" + dr["rto"] + "</td>");
                htmlTable.Append("<td>" + Convert.ToDateTime(dr["rtime"]).ToString("MMMM dd ,yyyy") + "</td>");
                if (dr["Charges"].ToString() != "0")
                {
                    htmlTable.Append("<td class='text-danger'>" + dr["Charges"] + "</td>");
                    htmlTable.Append("<td class='text-danger'>" + dr["remark"] + "</td>");
                }
                else
                    htmlTable.Append("<td class='text-success'>0</td><td>-</td>");
            }
            else
            {
                htmlTable.Append("<td class='text-danger'>Not Returned yet</td><td>-</td><td>-</td><td>-</td>");
            }
            htmlTable.Append("</tr>");
        }
        if (i <= 0)
        {
            data.Text = "none";
            htmlTable.Append("<tr><td colspan=8 align=center>No Transaction Done</td></tr>");
        }
        BookInfotbl.Controls.Add(new Literal { Text = htmlTable.ToString() });
        dr.Close();
        htmlTable.Clear();

        SqlCommand cmd2 = new SqlCommand(sql, con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        while (dr2.Read())
        {
            htmlTable.Append("<tr>");
            htmlTable.Append("<td>" + dr2["StudentF"] + " " + dr2["StudentL"] + "</td>");
            htmlTable.Append("<td>" + dr2["ifrom"] + "</td>");
            htmlTable.Append("<td>" + Convert.ToDateTime(dr2["itime"]).ToString("MMMM dd ,yyyy HH:MM") + "</td>");
            if (!(dr2["rto"] is DBNull))
            {
                htmlTable.Append("<td>" + dr2["rto"] + "</td>");
                htmlTable.Append("<td>" + Convert.ToDateTime(dr2["rtime"]).ToString("MMMM dd ,yyyy") + "</td>");
                if (dr2["Charges"].ToString() != "0")
                {
                    htmlTable.Append("<td class='text-danger'>" + dr2["Charges"] + "</td>");
                    htmlTable.Append("<td class='text-danger'>" + dr2["remark"] + "</td>");
                }
                else
                    htmlTable.Append("<td class='text-success'>0</td><td>-</td>");
            }
            else
            {
                htmlTable.Append("<td class='text-danger'>Not Returned yet</td><td>-</td><td>-</td><td>-</td>");
            }
            htmlTable.Append("</tr>");
        }
        BookInfotbl2.Controls.Add(new Literal { Text = htmlTable.ToString() });
        con.Close();
    }

    public void FillData()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT b.available, b.RFID, b.bname AS Name, s.semester AS Sem, s.sname AS Subject, b.edition AS Edition, p.pname AS Publisher, b.ISBN, ir.itime FROM     book AS b INNER JOIN                   subject AS s ON b.subject = s.id INNER JOIN                   publisher AS p ON b.publisher = p.id INNER JOIN                   issue_return AS ir ON b.RFID = '"+RFID+"'", con);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            RFID_txt.Text = RFID_lbl.Text = dr["RFID"].ToString();
            name_txt.Text = name_lbl.Text = dr["Name"].ToString();
            ISBN_txt.Text = ISBN_lbl.Text = dr["ISBN"].ToString();
            Subject_txt.Text = Subject_lbl.Text = dr["Subject"].ToString();
            Sem_txt.Text = Sem_lbl.Text = dr["Sem"].ToString();
            Edition_txt.Text = Edition_lbl.Text = dr["edition"].ToString();
            Publisher_txt.Text = Publisher_lbl.Text = dr["Publisher"].ToString();
            if (Convert.ToInt16(dr["available"]) == 1)
            {
                Status_lbl.ForeColor = System.Drawing.Color.Green;
                Status_lbl.Text = "Available Now";
                EstDate_lbl.Text = "-";
            }
            else
            {
                Update.Visible = Update_RFID.Visible = false;
                Status_lbl.ForeColor = System.Drawing.Color.Red;
                Status_lbl.Text = "Not Available";
                EstDate_lbl.ForeColor = System.Drawing.Color.Red;
                EstDate_lbl.Text = Convert.ToDateTime(dr["itime"]).AddDays(7).ToString("MMMM dd,yyyy");
            }
        }
        dr.Close();

        string[] author = new string[3];
        SqlCommand cmd2 = new SqlCommand("SELECT a.aname FROM     ba_connection AS ba INNER JOIN                   book AS b ON ba.RFID = b.RFID INNER JOIN                   author AS a ON ba.author = a.id WHERE  (ba.RFID = '" + RFID + "')", con);
        SqlDataReader dr2 = cmd2.ExecuteReader();
        for (int i = 0; dr2.Read(); i++)
        {
            author[i] = dr2["aname"].ToString();
        }
        dr2.Close();
        Author1_txt.Text = Author1_lbl.Text = author[0];
        Author2_txt.Text = Author2_lbl.Text = author[1];
        Author3_txt.Text = Author3_lbl.Text = author[2];

        SqlCommand cmd3 = new SqlCommand("SELECT COUNT(BRFID) AS TotalIssue, SUM(charges) AS TotalIssue FROM     issue_return where BRFID='" + RFID + "'GROUP BY BRFID", con);
        SqlDataReader dr3 = cmd3.ExecuteReader();
        while (dr3.Read())
        {
            IssueTimes_lbl.Text = dr3["TotalIssue"].ToString();
            Charges_lbl.Text = dr3["TotalIssue"].ToString();
        }

        if (Author2_lbl.Text == "")
            Author2_txt.Text = Author2_lbl.Text = Author3_txt.Text = Author3_lbl.Text = "-";
        else if (Author3_lbl.Text == "")
            Author3_txt.Text = Author3_lbl.Text = "-";

        if (IssueTimes_lbl.Text == "")
            IssueTimes_lbl.Text = "0";
        if (Charges_lbl.Text == "")
            Charges_lbl.Text = "0";
        con.Close();
        FillTblData();

    }

    public void HideLabel()
    {
        RFID_lbl.Visible = name_lbl.Visible = ISBN_lbl.Visible = Subject_lbl.Visible = Sem_lbl.Visible = Edition_lbl.Visible = Publisher_lbl.Visible = Author1_lbl.Visible = Author2_lbl.Visible = Author3_lbl.Visible = false;
        RFID_txt.Visible = name_txt.Visible = ISBN_txt.Visible = Subject_txt.Visible = Sem_txt.Visible = Edition_txt.Visible = Publisher_txt.Visible = Author1_txt.Visible = Author2_txt.Visible = Author3_txt.Visible = true;
    }

    public void HideTextbox()
    {
        RFID_txt.Visible = name_txt.Visible = ISBN_txt.Visible = Subject_txt.Visible = Sem_txt.Visible = Edition_txt.Visible = Publisher_txt.Visible = Author1_txt.Visible = Author2_txt.Visible = Author3_txt.Visible = false;
        RFID_lbl.Visible = name_lbl.Visible = ISBN_lbl.Visible = Subject_lbl.Visible = Sem_lbl.Visible = Edition_lbl.Visible = Publisher_lbl.Visible = Author1_lbl.Visible = Author2_lbl.Visible = Author3_lbl.Visible = true;
    }

    public void HideButton()
    {
        Update_Done.Visible = false;
        Update.Visible = Transaction_tbl.Visible = Update_RFID.Visible = true;
    }

    public void ShowButton()
    {
        Update_Done.Visible = true;
        Update.Visible = Transaction_tbl.Visible = Update_RFID.Visible = false;
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        HideLabel();
        ShowButton();
    }
    protected void UpdateDone_Click(object sender, EventArgs e)
    {
        HideButton();
        HideTextbox();
    }
    protected void UpdateRFID_Click(object sender, EventArgs e)
    {
    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        if (data.Text != "none")
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename="+ISBN_lbl.Text+"-" + name_lbl.Text + " Transactions.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.Output.Write(Request.Form[hfGridHtml.UniqueID]);
            Response.Flush();
            Response.End();
        }
    }

}