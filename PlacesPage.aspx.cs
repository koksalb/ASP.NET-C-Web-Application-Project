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
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

public partial class PlacesPage : System.Web.UI.Page
{
    List<restaurant> list = new List<restaurant>();
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

        public double Average_Vote{get;set;}

        public double Expected_Visits_This_Month{get;set;}

        public int Days_Since_Last_Visit { get; set; }

    }



    protected void Page_Load(object sender, EventArgs e)
    {

        ReadListFromDatabase(list);

     


    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
        return;
    }

    protected void Export_Excel(object sender, EventArgs e)
    {


        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "RestausantsExcel_" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();

        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);



        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        HtmlForm form1 = new HtmlForm();
        Page.Controls.Add(form1);



        GridView1.GridLines = GridLines.Both;
        GridView1.HeaderStyle.Font.Bold = true;
        GridView1.AllowPaging = false;
        GridView1.AllowSorting = false;
        form1.Controls.Add(GridView1);

        form1.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
        GridView1.AllowPaging = true;
        GridView1.AllowSorting = true;

    }

    protected void ReadListFromDatabase(List<restaurant> list)
    {
        list.Clear();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantsConnectionString"].ConnectionString);
        conn.Open();
        string testquery = "select * from Restaurants";

        SqlCommand com = new SqlCommand(testquery, conn);
        using (var command = com.ExecuteReader())
        {
            while (command.Read())
            {
                restaurant temp = new restaurant();
                temp.Id = command.GetInt32(0);
                temp.Place_Name = command.GetString(1);
                temp.Affectedfromweather = command.GetBoolean(2);
                temp.Latest_Visit_Date = command.GetDateTime(3);
                temp.Total_Visits_This_Month = command.GetInt32(4);
                temp.CarorWalk = command.GetBoolean(5);
                temp.Total_People_Voted = command.GetInt32(6);
                temp.Total_Votes = command.GetInt32(7);
                temp.Average_Vote = command.GetDouble(8);
                temp.Expected_Visits_This_Month = command.GetDouble(9);
                temp.Days_Since_Last_Visit = command.GetInt32(10);
                list.Add(temp);


            }
        }
        conn.Close();
    }

    protected void UpdateDatabaseWithTheList(List<restaurant> liste)
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantsConnectionString"].ConnectionString);
        conn.Open();
        string testquery = "delete from Restaurants where id=id";
        SqlCommand com = new SqlCommand(testquery, conn);
        var command = com.ExecuteReader();
        conn.Close();
        for (int i = 0; i < list.Count; i++)
        {
            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantsConnectionString"].ConnectionString);
            conn2.Open();
            string testquery2 = "insert into Restaurants (Id,Place_Name,Affectedfromweather,Latest_Visit_Date,Total_Visits_This_Month,CarorWalk,Total_People_Voted,Total_Votes,Average_Vote,Expected_Visits_This_Month,Days_Since_Last_Visit) VALUES ("

                +
            list.ElementAt(i).Id + "," +
             "'" + Regex.Replace(list.ElementAt(i).Place_Name, @"\s", "") + "'" + ",";
            if (list.ElementAt(i).Affectedfromweather)
            {
                testquery2 = testquery2 + "1" + ",";
            }
            else
            {
                testquery2 = testquery2 + "0" + ",";
            }

            string format = "yyyy-MM-dd HH:mm:ss";
            testquery2 +=
              "'" + list.ElementAt(i).Latest_Visit_Date.ToString(format) + "'" + "," +
              list.ElementAt(i).Total_Visits_This_Month + ",";

            if (list.ElementAt(i).CarorWalk)
            {
                testquery2 = testquery2 + "1" + ",";
            }
            else
            {
                testquery2 = testquery2 + "0" + ",";
            }

            double averagevote = (double)list.ElementAt(i).Total_Votes / list.ElementAt(i).Total_People_Voted;
            testquery2 +=
                   list.ElementAt(i).Total_People_Voted + "," +
                    list.ElementAt(i).Total_Votes + "," +
                    Regex.Replace(averagevote.ToString(), @",", ".") + "," +
                    Regex.Replace(list.ElementAt(i).Expected_Visits_This_Month.ToString(), @",", ".") + "," +
                       list.ElementAt(i).Days_Since_Last_Visit + ");"

                 ;
            SqlCommand com2 = new SqlCommand(testquery2, conn2);
            var command2 = com2.ExecuteReader();
            conn2.Close();

        }
        GridView1.DataBind();



    }

    

    protected void Import_Excel(object sender, EventArgs e)
    {

    }
    protected void New_Month_Click(object sender, EventArgs e)
    {
        var now = DateTime.Now;
        var first = new DateTime(now.Year, now.Month, 1);
        var start = first.AddMonths(-1);
        var last = first.AddMonths(1);


        double howmanydays = (last - first).TotalDays;

        double totalvote = 0;
        for (int i = 0; i < list.Count; i++)
        {
            list.ElementAt(i).Total_Visits_This_Month = 0;
            list.ElementAt(i).Latest_Visit_Date = start;
            list.ElementAt(i).Days_Since_Last_Visit = (int)(now - start).TotalDays;
            totalvote += list.ElementAt(i).Total_Votes;
        }
        for (int i = 0; i < list.Count; i++)
        {
            list.ElementAt(i).Average_Vote = list.ElementAt(i).Total_Votes / totalvote;
            list.ElementAt(i).Expected_Visits_This_Month = howmanydays * list.ElementAt(i).Average_Vote;

        }
        UpdateDatabaseWithTheList(list);
    }
}