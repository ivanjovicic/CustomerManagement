<%@ Page
    Title="Customer Form"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="CustomerForm.aspx.cs"
    Inherits="CustomerManagement.WebApp.CustomerForm" %>


<asp:Content
    ID="HeadContent"
    ContentPlaceHolderID="HeadContent"
    runat="server">
  <link href="/Content/form.css" rel="stylesheet" />
</asp:Content>

<asp:Content
    ID="BodyContent"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <div class="customer-form">

        <div class="form-group">
            <label for="txtFirstName">First Name</label>
            <asp:TextBox
                ID="txtFirstName"
                runat="server"
                CssClass="form-control" />
            <asp:RequiredFieldValidator
                ControlToValidate="txtFirstName"
                ErrorMessage="First name is required"
                CssClass="field-validation-error"
                Display="Dynamic"
                runat="server" />
        </div>

        <div class="form-group">
            <label for="txtLastName">Last Name</label>
            <asp:TextBox
                ID="txtLastName"
                runat="server"
                CssClass="form-control" />
            <asp:RequiredFieldValidator
                ControlToValidate="txtLastName"
                ErrorMessage="Last name is required"
                CssClass="field-validation-error"
                Display="Dynamic"
                runat="server" />
        </div>

        <div class="form-group">
            <label for="txtEmail">Email</label>
            <asp:TextBox
                ID="txtEmail"
                runat="server"
                CssClass="form-control" />
            <asp:RegularExpressionValidator
                ControlToValidate="txtEmail"
                ValidationExpression="^\S+@\S+\.\S+$"
                ErrorMessage="Invalid email address"
                CssClass="field-validation-error"
                Display="Dynamic"
                runat="server" />
        </div>

    <div class="checkbox-row mt-3">
    <asp:CheckBox
        ID="chkActive"
        runat="server"
        Text="Active" />
</div>

        <div class="form-actions">
            <asp:Button
                Text="Save"
                CssClass="btn btn-primary"
                runat="server"
                OnClick="Save_Click" />

            <asp:Button
                Text="Cancel"
                CssClass="btn btn-secondary ms-2"
                runat="server"
                CausesValidation="false"
                OnClick="Cancel_Click" />
        </div>

    </div>

</asp:Content>
