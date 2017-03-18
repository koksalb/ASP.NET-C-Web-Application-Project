using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
public partial class PlacesPage : System.Web.UI.Page
{

    public class restaurant
    {

        public int Id {get;set;}
        
        public String Place_Name{get;set;}

        public Boolean Affectedfromweather{get;set;}

        public DateTime Latest_Visit_Date{get;set;}

        public int Total_Visits_This_Month{get;set;}

        public Boolean CarorWalk{get;set;}

        public int Total_People_Voted{get;set;}

        public int Total_Votes{get;set;}

        public float Average_Vote{get;set;}

        public int Expected_Visits_This_Month{get;set;}

        public int Days_Since_Last_Visit { get; set; }

    }



    protected void Page_Load(object sender, EventArgs e)
    {


        //if (IsPostBack)
      //  {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantsConnectionString"].ConnectionString);
            
            conn.Open();

            string testquery = "select * from Restaurants" +
                
                                " where "+

                                "Days_Since_Last_Visit > 1"+

                                " and "+

                                "(Expected_Visits_This_Month-Total_Visits_This_Month) >= 0"
                
                
                ;

            SqlCommand com =new SqlCommand(testquery,conn);

            var list = new List<restaurant>();

            using(var command = com.ExecuteReader())
            {
                while(command.Read())
                {
                    /*list.Add(new restaurant {
                        Id = command.GetInt32(0),
                        Place_Name = command.GetString(1),
                        Affectedfromweather = command.GetBoolean(2),
                        Latest_Visit_Date = command.GetDateTime(3),
                        Total_Visits_This_Month = command.GetInt32(4),
                        CarorWalk = command.GetBoolean(5),
                        Total_People_Voted = command.GetInt32(6),
                        Total_Votes=command.GetInt32(7),
                        Average_Vote=command.GetInt32(8),
                        Days_Since_Last_Visit=command.GetInt32(9),
                        Expected_Visits_This_Month=command.GetInt32(10)
                    });
                     * 
                     * */

                    string foundplacename =  command.GetString(1);
                    Response.Write("I Found " + foundplacename + " for you !");
                }
            }


            conn.Close();

       // }


    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
        return;
    }

    protected void Export_Excel(object sender, EventArgs e)
    {
        /*
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        // Render grid view control.
        GridView1.RenderControl(htw);

        // Write the rendered content to a file.
        string renderedGridView = sw.ToString();
        System.IO.File.WriteAllText(@"C:\Path\On\Server\ExportedFile.xlsx", renderedGridView);

        */

        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Vithal" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);



        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

        Page.Controls.Add(form1);
        


        GridView1.GridLines = GridLines.Both;
        GridView1.HeaderStyle.Font.Bold = true;

        form1.Controls.Add(GridView1);

        form1.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();


    }
}