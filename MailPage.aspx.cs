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
    protected void Button_Send_Email_Click(object sender, EventArgs e)
    {
        /*
       var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("integramhd@gmail.com", "b4momkktm"),
                EnableSsl = true
            };
       client.Send("koksalb@itu.edu.tr", "koksalb@itu.edu.tr", "Your Lunch Program", "Test string for the email is set\nhere!");
       */
        SmtpClient sc = new SmtpClient();
        sc.Port = 587;
        sc.Host = "smtp.gmail.com";
        sc.EnableSsl = true;
        sc.Credentials = new NetworkCredential("integramhd@gmail.com", "b4momkktm");//TODO: edit the password before using
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("integramhd@gmail.com", "Lunch Planner 9000+");

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["maillistconnectionstring"].ConnectionString);
        conn.Open();
        string testquery = "SELECT EMAIL FROM maillist";
        SqlCommand com = new SqlCommand(testquery, conn);


        var list = new List<String>();

        using (var command = com.ExecuteReader())
        {
            while (command.Read())
            {
                list.Add(command.GetString(0));
            }
        }

        conn.Close();


        for (int i = 0; i < list.Count;i++ )
        {
            mail.To.Add(list.ElementAt(i));

        }

           
        mail.CC.Add("integramhd@gmail.com");
        mail.Subject = "Your Lunch Program";
        mail.IsBodyHtml = false;
        mail.Body = "test string to try the code";
        sc.Send(mail);

            Response.Write("SENT");
            
    }
    
}