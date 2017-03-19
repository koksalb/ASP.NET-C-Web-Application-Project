using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;

public partial class MailPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button_Insert_Click(object sender, EventArgs e)
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["maillistconnectionstring"].ConnectionString);
        conn.Open();

        string testquery = "INSERT INTO maillist (Name,Surname,Email) Values (" +      
                                          
           " '"+ Txt_name.Value+"' " + ", " +

            " '"+Txt_Surname.Value+"' " + ", " +

            " '"+Txt_mail.Value+"' " + ");"
               ;

        SqlCommand com = new SqlCommand(testquery, conn);
        var command = com.ExecuteReader();

        conn.Close();
        maillistgrid.DataBind();

    }
    protected void Button_Delete_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["maillistconnectionstring"].ConnectionString);
        conn.Open();

        string testquery = "DELETE FROM maillist WHERE Email=" +
             " '" + Txt_mail_todelete.Text + "' " + ";";
          SqlCommand com = new SqlCommand(testquery, conn);
        var command = com.ExecuteReader();
        conn.Close();
        maillistgrid.DataBind();
        maillistgrid.SelectedIndex = -1;
        Txt_mail_todelete.Text = "E-mail to delete";
        Button_Delete.Enabled = false;
    }




    protected void maillistgrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        Txt_mail_todelete.Text = maillistgrid.SelectedRow.Cells[3].Text;
        Button_Delete.Enabled = true;
    }
   
    
}