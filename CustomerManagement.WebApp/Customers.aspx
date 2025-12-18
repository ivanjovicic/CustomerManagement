<%@ Page
    Title="Home Page"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Customers.aspx.cs"
    Inherits="CustomerManagement.WebApp.Customers" %>

<asp:Content
    ID="HeadContent"
    ContentPlaceHolderID="HeadContent"
    runat="server">

    <link href="/Content/customers.css" rel="stylesheet" />

</asp:Content>

<asp:Content
    ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <main>

        <h2>Customers</h2>

        <div style="margin-bottom: 15px;">

            <asp:TextBox
                ID="txtSearch"
                runat="server"
                Placeholder="Search by name or email" />

            <asp:DropDownList
                ID="ddlStatus"
                runat="server">

                <asp:ListItem Text="All" Value="" />
                <asp:ListItem Text="Active" Value="true" />
                <asp:ListItem Text="Inactive" Value="false" />

            </asp:DropDownList>

            <asp:Button
                ID="btnSearch"
                runat="server"
                Text="Search"
                OnClick="Search_Click" />

        </div>

        <asp:GridView
            ID="gvCustomers"
            runat="server"
            AutoGenerateColumns="false"
            CssClass="table table-striped"
            OnRowDeleting="gvCustomers_RowDeleting"
            DataKeyNames="Id">

            <Columns>

                <asp:BoundField
                    DataField="Id"
                    HeaderText="ID" />

                <asp:BoundField
                    DataField="FirstName"
                    HeaderText="First Name" />

                <asp:BoundField
                    DataField="LastName"
                    HeaderText="Last Name" />

                <asp:BoundField
                    DataField="Email"
                    HeaderText="Email" />

                <asp:CheckBoxField
                    DataField="IsActive"
                    HeaderText="Active" />

                <asp:HyperLinkField
                    Text="Edit"
                    DataNavigateUrlFields="Id"
                    DataNavigateUrlFormatString="CustomerForm.aspx?id={0}" />

                <asp:ButtonField 
                    Text="Delete" 
                    CommandName="Delete" 
                    ButtonType="Button" />

            </Columns>

        </asp:GridView>

    </main>

</asp:Content>
