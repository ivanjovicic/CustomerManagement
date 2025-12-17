<asp:TextBox ID="txtFirstName" runat="server" />
<asp:RequiredFieldValidator ControlToValidate="txtFirstName" runat="server" />

<asp:TextBox ID="txtLastName" runat="server" />
<asp:RequiredFieldValidator ControlToValidate="txtLastName" runat="server" />

<asp:TextBox ID="txtEmail" runat="server" />
<asp:RegularExpressionValidator
    ControlToValidate="txtEmail"
    ValidationExpression="^\S+@\S+\.\S+$"
    runat="server" />

<asp:CheckBox ID="chkActive" runat="server" Text="Active" />

<asp:Button Text="Save" runat="server" OnClick="Save_Click" />