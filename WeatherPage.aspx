<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WeatherPage.aspx.cs" Inherits="WeatherPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
 <div>
   
<table id="tblWeather" runat="server" border="0" cellpadding="0" cellspacing="0"
    visible="false">
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
    </form>
</body>
</html>
