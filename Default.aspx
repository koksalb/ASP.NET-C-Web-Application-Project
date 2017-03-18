<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--
    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>
    -->
    <div class="row">
        <div class="col-md-12">
            <h2>Places Database</h2> 
             <div style="height: 100%; width: 100%">
    
        <asp:SqlDataSource ID="SqlDataSource_Restaurants" runat="server" ConnectionString="<%$ ConnectionStrings:RestaurantsConnectionString %>" SelectCommand="SELECT * FROM [Restaurants]"></asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Id" DataSourceID="SqlDataSource_Restaurants">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="Place_Name" HeaderText="Place_Name" SortExpression="Place_Name" />
                <asp:CheckBoxField DataField="Affectedfromweather" HeaderText="Affectedfromweather" SortExpression="Affectedfromweather" />
                <asp:BoundField DataField="Latest_Visit_Date" HeaderText="Latest_Visit_Date" SortExpression="Latest_Visit_Date" />
                <asp:BoundField DataField="Total_Visits_This_Month" HeaderText="Total_Visits_This_Month" SortExpression="Total_Visits_This_Month" />
                <asp:CheckBoxField DataField="CarorWalk" HeaderText="CarorWalk" SortExpression="CarorWalk" />
                <asp:BoundField DataField="Total_People_Voted" HeaderText="Total_People_Voted" SortExpression="Total_People_Voted" />
                <asp:BoundField DataField="Total_Votes" HeaderText="Total_Votes" SortExpression="Total_Votes" />
                <asp:BoundField DataField="Average_Vote" HeaderText="Average_Vote" SortExpression="Average_Vote" />
                <asp:BoundField DataField="Expected_Visits_This_Month" HeaderText="Expected_Visits_This_Month" SortExpression="Expected_Visits_This_Month" />
                <asp:BoundField DataField="Days_Since_Last_Visit" HeaderText="Days_Since_Last_Visit" SortExpression="Days_Since_Last_Visit" />
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView>
    
    </div>
            
        </div>
        </div>
     <div class="row">
        <div class="col-md-6">
            <h2>Mail Page</h2>
            
            <style type="text/css">
        #form1 {
            width: 721px;
            height: 519px;
        }
        .auto-style1 {
            text-align: right;
            width: 50%;
        }
        .auto-style2 {
            font-weight: bold;
        }
        #Text1 {
            width: 198px;
        }
        #Text2 {
            width: 198px;
        }
        #Text3 {
            width: 198px;
        }
        #addmail {
            text-align: center;
        }
        .auto-style3 {
            width: 50%;
        }
    </style>
    <div>
    
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:maillistconnectionstring %>" SelectCommand="SELECT * FROM [maillist]" ProviderName="<%$ ConnectionStrings:maillistconnectionstring.ProviderName %>"></asp:SqlDataSource>
    
    </div>
        <asp:GridView ID="maillistgrid" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="Email" DataSourceID="SqlDataSource1" GridLines="Vertical" OnSelectedIndexChanged="maillistgrid_SelectedIndexChanged" Width="100%">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" ReadOnly="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
        <br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style3">
                     <br />
        <div class="auto-style1">
            <asp:Label ID="Label1" runat="server" CssClass="auto-style2" Text="Name(optional)" Width="150px"></asp:Label>
&nbsp;&nbsp;
            <input id="Txt_name" dir="ltr" type="text" runat="server"/><br />
            <asp:Label ID="Label2" runat="server" CssClass="auto-style2" Text="Surname(optional)" Width="150px"></asp:Label>
&nbsp;&nbsp;
            <input id="Txt_Surname" dir="ltr" type="text" runat="server"/><br />
            <asp:Label ID="Label3" runat="server" CssClass="auto-style2" Text="E-mail" Width="150px"></asp:Label>
&nbsp;&nbsp;
            <input id="Txt_mail" dir="ltr" type="email" runat="server"/><br />
            <br />
            <asp:Button ID="Button_Insert" runat="server" Text="Insert" OnClick="Button_Insert_Click" />
        </div>

                </td>
                &nbsp;&nbsp;
                <td class="auto-style1">
                    <br />

                    <asp:Label ID="Txt_mail_todelete" runat="server" CssClass="auto-style2" Text="E-mail to delete" Width="70%"></asp:Label>
                    &nbsp;
                    <asp:Button ID="Button_Delete" runat="server" Text="Delete" OnClick="Button_Delete_Click"  Width="20%" Enabled="False"/>
                    <br />
                </td>
            </tr>
            
            
        </table>
       
        <br />
        <asp:Button ID="Button_Send_Email" runat="server" Height="60px" Text="SEND EMAIL" Width="100%" BackColor="#009933" BorderColor="Yellow" BorderStyle="Groove" BorderWidth="5px" ForeColor="#003366" OnClick="Button_Send_Email_Click" style="font-weight: 700; font-size: xx-large" />
       
        </div>
        

        <div class="col-md-6">
            <h2>Weather Page</h2>
           


 <div>
   
<table id="tblWeather" runat="server" border="0" cellpadding="0" cellspacing="0"
    visible="true">
    <tr>
        <th colspan="2">
            Weather Information
        </th>
    </tr>
    <tr>
        <td rowspan="3">
            <asp:Image ID="imgWeatherIcon" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblCity_Country" runat="server" />
            <asp:Image ID="imgCountryFlag" runat="server" />
            <asp:Label ID="lblDescription" runat="server" />
            Humidity:
            <asp:Label ID="lblHumidity" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            Temperature:
            (Min:
            <asp:Label ID="lblTempMin" runat="server" />
            Max:
            <asp:Label ID="lblTempMax" runat="server" />
                Day:
            <asp:Label ID="lblTempDay" runat="server" />
                Night:
            <asp:Label ID="lblTempNight" runat="server" />)
        </td>
    </tr>
</table>


    </div>



        </div>
          </div>
</asp:Content>
