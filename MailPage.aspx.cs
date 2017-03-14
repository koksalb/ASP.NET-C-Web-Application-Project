using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

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
}