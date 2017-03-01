using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class PlacesPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        //if (IsPostBack)
      //  {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantsConnectionString"].ConnectionString);
            
            conn.Open();

            string testquery = "select count(*) from Restaurants where Days_Since_Last_Visit > 3";

            SqlCommand com =new SqlCommand(testquery,conn);
            int result = Convert.ToInt32(com.ExecuteScalar().ToString());

            if (result > 0)
            {
                Response.Write("I Found some restaurants!");
            }
            else
            {
                Response.Write("I have nothing :(");
            }

            conn.Close();

       // }


    }
}