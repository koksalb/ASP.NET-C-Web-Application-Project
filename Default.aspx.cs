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

public partial class _Default : Page
{
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
    }


    protected void Button_Insert_Click(object sender, EventArgs e)
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["maillistconnectionstring"].ConnectionString);
        conn.Open();

        string testquery = "INSERT INTO maillist (Name,Surname,Email) Values (" +

           " '" + Txt_name.Value + "' " + ", " +

            " '" + Txt_Surname.Value + "' " + ", " +

            " '" + Txt_mail.Value + "' " + ");"
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


        for (int i = 0; i < list.Count; i++)
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
    
}