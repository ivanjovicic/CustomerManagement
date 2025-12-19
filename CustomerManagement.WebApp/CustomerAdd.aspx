<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="CustomerAdd.aspx.cs"
    Inherits="CustomerManagement.WebApp.CustomerAdd"
    MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add New Customer</h2>

    <asp:ValidationSummary
        ID="ValidationSummary1"
        runat="server"
        CssClass="text-danger" />

    <div class="form-group">
        <label>First Name</label>
        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator
            ControlToValidate="txtFirstName"
            ErrorMessage="First Name is required"
            runat="server"
            CssClass="text-danger" />
    </div>

    <div class="form-group">
        <label>Last Name</label>
        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator
            ControlToValidate="txtLastName"
            ErrorMessage="Last Name is required"
            runat="server"
            CssClass="text-danger" />
    </div>

    <div class="form-group">
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

    <div class="form-group">
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
