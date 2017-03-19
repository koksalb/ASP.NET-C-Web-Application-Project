<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailPage.aspx.cs" Inherits="MailPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
</head>
<body>
    <form id="form1" runat="server" aria-orientation="horizontal">
    <div>
    
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:maillistconnectionstring %>" SelectCommand="SELECT * FROM [maillist]"></asp:SqlDataSource>
    
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
        <div class="auto-style1" style="height: 137px; width: 294px">
            <asp:Label ID="Label1" runat="server" CssClass="auto-style2" Text="Name(optional)" Width="150px"></asp:Label>
&nbsp;&nbsp;
            <input id="Txt_name" dir="ltr" type="text" runat="server"/><br />
            <asp:Label ID="Label2" runat="server" CssClass="auto-style2" Text="Surname(optional)" Width="150px"></asp:Label>
&nbsp;&nbsp;
            <input id="Txt_Surname" dir="ltr" type="text" runat="server"/><br />
            <asp:Label ID="Label3" runat="server" CssClass="auto-style2" Text="E-mail" Width="150px"></asp:Label>
&nbsp;&nbsp;
            <input id="Txt_mail" dir="ltr" type="email" runat="server"/><br />
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
       
    </form>
</body>
</html>
