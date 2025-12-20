<%@ Page Language="C#" AutoEventWireup="true"
    Async="true"
    CodeBehind="CustomerAdd.aspx.cs"
    Inherits="CustomerManagement.WebApp.CustomerAdd"
    MasterPageFile="~/Site.Master" %>

<asp:Content
    ID="HeadContent"
    ContentPlaceHolderID="HeadContent"
    runat="server">
  <link href="/Content/form.css" rel="stylesheet" />
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add New Customer</h2>

    <asp:ValidationSummary
        ID="ValidationSummary1"
        runat="server"
        CssClass="text-danger" />

    <div class="customer-form">
        <label>First Name</label>
        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator
            ControlToValidate="txtFirstName"
            ErrorMessage="First Name is required"
            runat="server"
            CssClass="text-danger" />

        <asp:CustomValidator
            ID="cvFirstName"
            runat="server"
            ControlToValidate="txtFirstName"
            ErrorMessage="First letter must be uppercase"
            CssClass="text-danger"
            OnServerValidate="ValidateFirstLetterUppercase"
            Display="None"/>
    </div>

    <div class="customer-form">
        <label>Last Name</label>
        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator
            ControlToValidate="txtLastName"
            ErrorMessage="Last Name is required"
            runat="server"
            CssClass="text-danger" />
        <asp:CustomValidator
            ID="cvLastName"
            runat="server"
            ControlToValidate="txtLastName"
            ErrorMessage="First letter must be uppercase"
            CssClass="text-danger"
            OnServerValidate="ValidateFirstLetterUppercase" 
            Display="None"/>
    </div>

    <div class="customer-form">
        <label>Email</label>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator
            ControlToValidate="txtEmail"
            ErrorMessage="Email is required"
            runat="server"
            CssClass="text-danger" />
        <asp:RegularExpressionValidator
            ControlToValidate="txtEmail"
            ErrorMessage="Invalid email format"
            runat="server"
            CssClass="text-danger"
            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" />
    </div>

    <div class="customer-form">
        <label>Status</label>
        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
            <asp:ListItem Text="Active" Value="true" />
            <asp:ListItem Text="Inactive" Value="false" />
        </asp:DropDownList>
    </div>

    <br />

    <asp:Button
        ID="btnSave"
        runat="server"
        Text="Save"
        CssClass="btn btn-primary"
        OnClick="btnSave_Click" />

    <asp:Button
        ID="btnCancel"
        runat="server"
        Text="Cancel"
        CssClass="btn btn-secondary"
        CausesValidation="false"
        OnClick="btnCancel_Click" />

</asp:Content>
