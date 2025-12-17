<asp:TextBox ID="txtSearch" runat="server" />
<asp:DropDownList ID="ddlStatus" runat="server">
    <asp:ListItem Text="All" Value="" />
    <asp:ListItem Text="Active" Value="true" />
    <asp:ListItem Text="Inactive" Value="false" />
</asp:DropDownList>
<asp:Button Text="Search" runat="server" OnClick="Search_Click" />

<asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="ID" />
        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:CheckBoxField DataField="IsActive" HeaderText="Active" />

        <asp:HyperLinkField
            Text="Edit"
            DataNavigateUrlFields="Id"
            DataNavigateUrlFormatString="CustomerForm.aspx?id={0}" />

        <asp:ButtonField Text="Delete" CommandName="Delete" />
    </Columns>
</asp:GridView>