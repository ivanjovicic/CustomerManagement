<%@ Page
    Async="true"
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

        <asp:Label
            ID="lblInfo"
            runat="server"
            CssClass="text-warning"
            EnableViewState="false" />

        <div class="customers-toolbar">
            <asp:TextBox
                ID="txtSearch"
                runat="server"
                Placeholder="Search by name or email"
                oninput="onSearchChanged(this)"
                onkeydown="return event.key !== 'Enter';" />

            <asp:Button
                ID="btnSearch"
                runat="server"
                Text="Search"
                OnClick="Search_Click" />

            <asp:DropDownList
                ID="ddlStatus"
                runat="server"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                <asp:ListItem Text="All" Value="" />
                <asp:ListItem Text="Active" Value="true" />
                <asp:ListItem Text="Inactive" Value="false" />
            </asp:DropDownList>
        </div>

        <asp:UpdatePanel ID="upCustomers" runat="server">
            <ContentTemplate>

                <asp:Label
                    ID="lblLoadTime"
                    runat="server"
                    CssClass="load-time" />

                <div class="table-responsive">
                    <asp:GridView
                        ID="gvCustomers"
                        runat="server"
                        AutoGenerateColumns="false"
                        CssClass="table table-striped"
                        DataKeyNames="Id"
                        AllowPaging="true"
                        PageSize="10"
                        OnPageIndexChanging="gvCustomers_PageIndexChanging"
                        OnRowDeleting="gvCustomers_RowDeleting">

                        <PagerSettings
                            Mode="NumericFirstLast"
                            FirstPageText="⏮"
                            LastPageText="⏭" />

                        <PagerStyle
                            CssClass="pager"
                            HorizontalAlign="Center" />

                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" />
                            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:CheckBoxField DataField="IsActive" HeaderText="Active" />

                            <asp:HyperLinkField
                                Text="Edit"
                                DataNavigateUrlFields="Id"
                                DataNavigateUrlFormatString="CustomerForm.aspx?id={0}"
                                ControlStyle-CssClass="btn-edit" />

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton
                                        ID="btnDelete"
                                        runat="server"
                                        Text="Delete"
                                        CommandName="Delete"
                                        OnClientClick="return confirm('Are you sure you want to delete this customer?');"
                                        CssClass="btn btn-delete btn-sm" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button
            ID="btnAddCustomer"
            runat="server"
            Text="Add New Customer"
            CssClass="btn btn-primary"
            OnClick="AddCustomer_Click" />
    </main>

    <script type="text/javascript">
        function onSearchChanged(textbox) {
            if (textbox.value === "") {
                location.href = 'Customers.aspx';
            }
        }
    </script>
</asp:Content>