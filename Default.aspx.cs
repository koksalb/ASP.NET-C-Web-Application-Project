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
using System.Web.Script.Serialization;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;


public partial class _Default : Page
{
    List<restaurant> list = new List<restaurant>();
    Boolean weather;//false if it is good, true if it is not good meaning there is a weather problem!
    deletionreasons reasons = new deletionreasons();
    restaurant chosen = new restaurant();

    public class deletionreasons
    {
        public List<String> weatherdeletes { get; set; }
        public List<String> carorwalkdeletes { get; set; }
        public List<String> yesterdaywentdeleted { get; set; }
        public List<String> expectedlimitreacheddeletes { get; set; }

        public List<String> noproblems { get; set; }
        public deletionreasons()
        {
            weatherdeletes = new List<String>();
            carorwalkdeletes = new List<String>();
            yesterdaywentdeleted = new List<String>();
            expectedlimitreacheddeletes = new List<String>();
            noproblems = new List<String>();
        }
    }
    public class restaurant
    {

        public int Id { get; set; }

        public String Place_Name { get; set; }

        public Boolean Affectedfromweather { get; set; }

        public DateTime Latest_Visit_Date { get; set; }

        public int Total_Visits_This_Month { get; set; }

        public Boolean CarorWalk { get; set; }

        public int Total_People_Voted { get; set; }

        public int Total_Votes { get; set; }

        public double Average_Vote { get; set; }

        public double Expected_Visits_This_Month { get; set; }

        public int Days_Since_Last_Visit { get; set; }

    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //set weather first
        string appId = "a8210c3d0e052d04991c174e73ec64b0";
        string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt=1&APPID={1}", "Istanbul", appId);
        using (WebClient client = new WebClient())
        {
            string json = client.DownloadString(url);

            WeatherInfo weatherInfo = (new JavaScriptSerializer()).Deserialize<WeatherInfo>(json);
            lblCity_Country.Text = weatherInfo.city.name + "," + weatherInfo.city.country;
            imgCountryFlag.ImageUrl = string.Format("http://openweathermap.org/images/flags/{0}.png", weatherInfo.city.country.ToLower());
            lblDescription.Text = weatherInfo.list[0].weather[0].description;
            imgWeatherIcon.ImageUrl = string.Format("http://openweathermap.org/img/w/{0}.png", weatherInfo.list[0].weather[0].icon);
            lblTempMin.Text = string.Format("{0}°С", Math.Round(weatherInfo.list[0].temp.min, 1));
            lblTempMax.Text = string.Format("{0}°С", Math.Round(weatherInfo.list[0].temp.max, 1));
            lblTempDay.Text = string.Format("{0}°С", Math.Round(weatherInfo.list[0].temp.day, 1));
            lblTempNight.Text = string.Format("{0}°С", Math.Round(weatherInfo.list[0].temp.night, 1));
            lblHumidity.Text = weatherInfo.list[0].humidity.ToString();
            tblWeather.Visible = true;

        }
       
        if (lblDescription.Text.ToString().Contains("rain") || lblDescription.Text.ToString().Contains("snow"))
        {
            weather = true;//weatherproblemexist         
        }
        else
        {
            weather = false;//goodweather           
        }



        ReadListFromDatabase(list);
        for (int i = 0; i < list.Count;i++ )
        {
            list.ElementAt(i).Days_Since_Last_Visit = (int)(DateTime.Now.Date - list.ElementAt(i).Latest_Visit_Date).TotalDays;
                     
        }

            UpdateDatabaseWithTheList(list);


    }


    protected void UpdateDatabaseWithTheList(List<restaurant> liste)
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantsConnectionString"].ConnectionString);
        conn.Open();
        string testquery = "delete from Restaurants where id=id";
        SqlCommand com = new SqlCommand(testquery, conn);
        var command = com.ExecuteReader();
        conn.Close();
        for (int i = 0; i < list.Count;i++ )
        {
            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["RestaurantsConnectionString"].ConnectionString);
            conn2.Open();
            string testquery2 = "insert into Restaurants (Id,Place_Name,Affectedfromweather,Latest_Visit_Date,Total_Visits_This_Month,CarorWalk,Total_People_Voted,Total_Votes,Average_Vote,Expected_Visits_This_Month,Days_Since_Last_Visit) VALUES ("

                +
            list.ElementAt(i).Id + "," +
             "'" + Regex.Replace(list.ElementAt(i).Place_Name, @"\s", "") + "'" + ",";
            if (list.ElementAt(i).Affectedfromweather)
            { 
            testquery2 = testquery2 + "1"+",";
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

    
   




    
    protected void send_email()
    {
        
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


        for (int i = 0; i < list.Count; i++)
        {
            mail.To.Add(list.ElementAt(i));

        }


        mail.CC.Add("integramhd@gmail.com");
        mail.Subject = "Your Lunch Program";
        mail.IsBodyHtml = false;

        String emailbody = "";
        emailbody += "Today you are going to: " + chosen.Place_Name+"\n\n\n";

        emailbody += Labelweather.Text + "\n" + Labelcarorwalk.Text + "\n" + Labelexpected.Text + "\n" + Labelnoproblem.Text + "\n";

        emailbody += "Thank you for using our program!\n Bon appetit! :)";



        mail.Body = emailbody;
        sc.Send(mail);

        

    }

    public class WeatherInfo
    {
        public City city { get; set; }
        public List<List> list { get; set; }
    }

    public class City
    {
        public string name { get; set; }
        public string country { get; set; }
    }

    public class Temp
    {
        public double day { get; set; }
        public double min { get; set; }
        public double max { get; set; }
        public double night { get; set; }
    }

    public class Weather
    {
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class List
    {
        public Temp temp { get; set; }
        public int humidity { get; set; }
        public List<Weather> weather { get; set; }
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


    
    

    protected void Initialise_New_Month(object sender, EventArgs e)
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
            list.ElementAt(i).Expected_Visits_This_Month = howmanydays*list.ElementAt(i).Total_Votes / totalvote;

        }
        UpdateDatabaseWithTheList(list);
    }

    protected void Choose_A_Restaurant(object sender, EventArgs e)
    {
        bool didyoupickarestauranttoday = false;
        for (int i = 0; i < list.Count; i++)
        {
            if(list.ElementAt(i).Days_Since_Last_Visit==0)
            {
                didyoupickarestauranttoday = true;
            }
        }
        if (didyoupickarestauranttoday==false)
        {
        double daynumber = 0;
        for (int i = 0; i < list.Count; i++)
        {
            daynumber += list.ElementAt(i).Total_Visits_This_Month;
        }
        

        double howmany = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1) -  new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).TotalDays;
        if (daynumber == howmany)
        {
            Response.Clear();
            Response.Write("END OF THE MONTH");
            return;
        }

        chosen = new restaurant();


        restaurant yesterday = new restaurant();
        restaurant daybeforeyesterday = new restaurant();
        yesterday.CarorWalk = true;
        daybeforeyesterday.CarorWalk = true;

        for (int i = 0; i < list.Count; i++)
        {
            if (list.ElementAt(i).Days_Since_Last_Visit == 1)
            {
                yesterday = list.ElementAt(i);
                reasons.yesterdaywentdeleted.Add(Regex.Replace(list.ElementAt(i).Place_Name, @"\s", "").ToString());
                list.RemoveAt(i);
                break;
            }
        }
        
        for (int i = 0; i < list.Count; i++)
        {
            if (list.ElementAt(i).Days_Since_Last_Visit == 2)
            {
                daybeforeyesterday = list.ElementAt(i);
                break;
            }
        }

        //if there is a weather problem. remove every restaurant that gets effected from the weather
        if (weather == true)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list.ElementAt(i).Affectedfromweather)
                {
                    reasons.weatherdeletes.Add(Regex.Replace(list.ElementAt(i).Place_Name, @"\s", "").ToString());
                    list.RemoveAt(i);
                    i--;
                }
            }
        }

        //If we used car yesterday or the day before. We cant choose a restaurant with carorwalt=false today. so we remove them
        if (yesterday.CarorWalk==false || daybeforeyesterday.CarorWalk==false)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list.ElementAt(i).CarorWalk == false)
                    {
                        reasons.carorwalkdeletes.Add(Regex.Replace(list.ElementAt(i).Place_Name, @"\s", "").ToString());
                        list.RemoveAt(i);
                        i--;
                    }
            }
        }

        //we have to remove restaurants that reached their expected visit limit for this month. (Expectedvisits - total visits < 1 is not okay!)
        for (int i = 0; i < list.Count; i++)
        {
            if ((list.ElementAt(i).Expected_Visits_This_Month - (double)list.ElementAt(i).Total_Visits_This_Month) < 1)
            {
                reasons.expectedlimitreacheddeletes.Add(Regex.Replace(list.ElementAt(i).Place_Name, @"\s", "").ToString());
                list.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i < list.Count; i++)
        {
            reasons.noproblems.Add(Regex.Replace(list.ElementAt(i).Place_Name, @"\s", "").ToString());
        }

        //if there is NO restaurant to go! All of them have problems somehow we still have to choose one by ignoring our limitations

        //ONE: NO WEATHER LIMITATION!
        if(list.Count==0)
        {
            reasons = new deletionreasons();
            reasons.weatherdeletes.Add("There was no restaurant to go so I removed the weather limitation.");
            
            ReadListFromDatabase(list);
            if (yesterday.CarorWalk == false || daybeforeyesterday.CarorWalk == false)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list.ElementAt(i).CarorWalk == false)
                    {
                        reasons.carorwalkdeletes.Add(Regex.Replace(list.ElementAt(i).Place_Name, @"\s", "").ToString());
                        list.RemoveAt(i);
                        i--;
                    }
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                if ((list.ElementAt(i).Expected_Visits_This_Month - (double)list.ElementAt(i).Total_Visits_This_Month) < 1)
                {
                    reasons.expectedlimitreacheddeletes.Add(Regex.Replace(list.ElementAt(i).Place_Name, @"\s", "").ToString());
                    list.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                reasons.noproblems.Add(Regex.Replace(list.ElementAt(i).Place_Name, @"\s", "").ToString());
            }
            
        }

        //TWO: NO WEATHER LIMITATION & NO CAR LIMITATION!
        if (list.Count == 0)
        {
            reasons = new deletionreasons();
            reasons.weatherdeletes.Add("There was no restaurant to go so I removed the weather limitation.");
            reasons.carorwalkdeletes.Add("There was no restaurant to go so I removed the car or walk limitation.");
            ReadListFromDatabase(list);        
            for (int i = 0; i < list.Count; i++)
            {
                if ((list.ElementAt(i).Expected_Visits_This_Month - (double)list.ElementAt(i).Total_Visits_This_Month) < 1)
                {
                    reasons.expectedlimitreacheddeletes.Add(Regex.Replace(list.ElementAt(i).Place_Name, @"\s", "").ToString());
                    list.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                reasons.noproblems.Add(Regex.Replace(list.ElementAt(i).Place_Name, @"\s", "").ToString());
            }

        }





        list = list.OrderBy(o => (o.Expected_Visits_This_Month - o.Total_Visits_This_Month)).ToList(); 
        double maximum = 0;
        double sofar = 0;
        for (int i = 0; i < list.Count; i++)
        {
            maximum += (list.ElementAt(i).Expected_Visits_This_Month - list.ElementAt(i).Total_Visits_This_Month);
        }
        Random random = new Random();
        System.Double test = random.NextDouble() * (maximum);
        
        for (int i = 0; i < list.Count; i++)
        {
            if ((sofar + list.ElementAt(i).Expected_Visits_This_Month - list.ElementAt(i).Total_Visits_This_Month) < test)
            {
                sofar += list.ElementAt(i).Expected_Visits_This_Month - list.ElementAt(i).Total_Visits_This_Month;
            }
            else 
            {
                chosen = list.ElementAt(i);
                break;
            }
        }
        list = list.OrderBy(o => o.Id).ToList();

        //THREE: NO WEATHER LIMITATION & NO CAR LIMITATION & NO EXPECTED DAY LIMITATION!
        if (list.Count == 0)
        {
            reasons = new deletionreasons();
            reasons.weatherdeletes.Add("There was no restaurant to go so I removed the weather limitation.");
            reasons.carorwalkdeletes.Add("There was no restaurant to go so I removed the car or walk limitation.");
            reasons.expectedlimitreacheddeletes.Add("There was no restaurant to go so I removed the expected day limitation.");

            ReadListFromDatabase(list);

            for (int i = 0; i < list.Count; i++)
            {
                if(list.ElementAt(i).Total_Visits_This_Month>=list.ElementAt(i).Expected_Visits_This_Month)
                {
                    list.RemoveAt(i);
                }
            }
            list = list.OrderBy(o => (o.Expected_Visits_This_Month-o.Total_Visits_This_Month)).ToList();
            maximum = 0;
            sofar = 0;
            for (int i = 0; i < list.Count; i++)
            {
                maximum += (list.ElementAt(i).Expected_Visits_This_Month - list.ElementAt(i).Total_Visits_This_Month);
            }
            random = new Random();
            test = random.NextDouble() * (maximum);

            for (int i = 0; i < list.Count; i++)
            {
                if ((sofar + list.ElementAt(i).Expected_Visits_This_Month - list.ElementAt(i).Total_Visits_This_Month) < test)
                {
                    sofar += list.ElementAt(i).Expected_Visits_This_Month - list.ElementAt(i).Total_Visits_This_Month;
                }
                else
                {
                    chosen = list.ElementAt(i);
                    break;
                }
            }
            
            list = list.OrderBy(o => o.Id).ToList();
        }
        

        /*
        GridView2.Visible = true;
        GridView2.DataSource = list;
        GridView2.DataBind();
       for (int i = 0; i < list.Count; i++)
        {
           if(list.ElementAt(i).Id == chosen.Id)
           {
               GridView2.SelectedIndex = i;
               break;
           }        
       }
        */

       ReadListFromDatabase(list);
       for (int i = 0; i < list.Count; i++)
       {
           if(list.ElementAt(i).Id == chosen.Id)
           {
               list.ElementAt(i).Latest_Visit_Date = DateTime.Now.Date;
               list.ElementAt(i).Total_Visits_This_Month++;
               list.ElementAt(i).Days_Since_Last_Visit = 0;
               
           }
           else 
           {
               list.ElementAt(i).Days_Since_Last_Visit = (int)(DateTime.Now.Date - list.ElementAt(i).Latest_Visit_Date).TotalDays;
           }
       }

        string temp = "Deleted because of weather conditions: ";
        for(int i=0;i<reasons.weatherdeletes.Count;i++)
        {
            if(i!=reasons.weatherdeletes.Count-1)
            {
            temp += reasons.weatherdeletes.ElementAt(i)+", ";
            }
            else
            {
            temp += reasons.weatherdeletes.ElementAt(i);
            }
        }
        Labelweather.Text = temp;

        temp = "Deleted because of carorwalk conditions: ";
        for(int i=0;i<reasons.carorwalkdeletes.Count;i++)
        {
            if(i!=reasons.carorwalkdeletes.Count-1)
            {
            temp += reasons.carorwalkdeletes.ElementAt(i)+", ";
            }
            else
            {
            temp += reasons.carorwalkdeletes.ElementAt(i);
            }
        }
        Labelcarorwalk.Text = temp;

        temp = "Deleted because of expected date limitations: ";
        for(int i=0;i<reasons.expectedlimitreacheddeletes.Count;i++)
        {
            if(i!=reasons.expectedlimitreacheddeletes.Count-1)
            {
            temp += reasons.expectedlimitreacheddeletes.ElementAt(i)+", ";
            }
            else
            {
            temp += reasons.expectedlimitreacheddeletes.ElementAt(i);
            }
        }
        Labelexpected.Text = temp;

        temp = "You can go to one of these with no problem: ";
        for(int i=0;i<reasons.noproblems.Count;i++)
        {
            if(i!=reasons.noproblems.Count-1)
            {
            temp += reasons.noproblems.ElementAt(i)+", ";
            }
            else
            {
            temp += reasons.noproblems.ElementAt(i);
            }
        }
        Labelnoproblem.Text = temp;


       UpdateDatabaseWithTheList(list);

       send_email();

}
        else
        {
            Response.Write("You have picked a restaurant today! Come back tomorrow!");
        }
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
}