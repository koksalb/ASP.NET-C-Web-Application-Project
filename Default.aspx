<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--
    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>
    -->
    <!DOCTYPE html>

<html>

<body>
    
    <div class="row">
        <h2> Places Database</h2> 
             <div style="height: 100%; width: 100%">
    
        <asp:SqlDataSource ID="SqlDataSource_Restaurants" runat="server" ConnectionString="<%$ ConnectionStrings:RestaurantsConnectionString %>" SelectCommand="SELECT [Place_Name], [Latest_Visit_Date], [Average_Vote] FROM [Restaurants]"></asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataSourceID="SqlDataSource_Restaurants">
            <Columns>
                <asp:BoundField DataField="Place_Name" HeaderText="Place_Name" SortExpression="Place_Name" />
                <asp:BoundField DataField="Latest_Visit_Date" HeaderText="Latest_Visit_Date" SortExpression="Latest_Visit_Date" />
                <asp:BoundField DataField="Average_Vote" HeaderText="Average_Vote" SortExpression="Average_Vote" />
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


     <br />
                 <asp:Label ID="Labelweather" runat="server" Text=""></asp:Label>
                 <br />
                 <asp:Label ID="Labelcarorwalk" runat="server" Text=""></asp:Label>
                 <br />
                 <asp:Label ID="Labelexpected" runat="server" Text=""></asp:Label>
                 <br />
                 <asp:Label ID="Labelnoproblem" runat="server" Text=""></asp:Label>
                 <br />
                 <br />
                 
        
        &nbsp;&nbsp;&nbsp;&nbsp;
                 
        
        <asp:Button class="btn btn-warning" ID="ChooseButton" runat="server" Text="Choose a restaurant" OnClick="Choose_A_Restaurant" />


                 <br />
                 <br />
                 <asp:GridView ID="GridView2" runat="server" Visible="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
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
        </style>
    <div>
    
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:maillistconnectionstring %>" SelectCommand="SELECT * FROM [maillist]" ProviderName="<%$ ConnectionStrings:maillistconnectionstring.ProviderName %>"></asp:SqlDataSource>
    
    </div>
        <asp:GridView ID="maillistgrid" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="Email" DataSourceID="SqlDataSource1" GridLines="Vertical"  Width="100%">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
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
       
        <br />
       
        </div>
        

        <div class="col-md-6">
            <h2>Weather Information
        </h2>
           


 <div>
   
<table id="tblWeather" runat="server" border="0" cellpadding="0" cellspacing="0"
    visible="true">
    
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
    
    
    <br />
    
        </body>
    </html>
</asp:Content>
