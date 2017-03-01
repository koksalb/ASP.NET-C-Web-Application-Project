<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlacesPage.aspx.cs" Inherits="PlacesPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 281px; width: 1087px">
    
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
    </form>
</body>
</html>
