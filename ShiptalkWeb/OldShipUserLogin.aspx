<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="OldShipUserLogin.aspx.cs" Inherits="ShiptalkWeb.OldShipUserLogin"
    Title="Old www.SHIPtalk.org User Login page" %>

<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls"
    TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="pp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Old Shiptalk.org User Login</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv4colleft">
            <h2>
                Old www.SHIPtalk.org User Login</h2>
            <br />
            Enter www.SHIPtalk.org Username and Password.
            <asp:LoginView ID="LoginView1" runat="server">
                <AnonymousTemplate>
                    <span class="offscreen">
                        <asp:Label ID="lblUsername" runat="server" Text="Username" AssociatedControlID="txtUsername"></asp:Label>
                        <asp:Label ID="lblPassword" runat="server" Text="Password" AssociatedControlID="txtPassword"></asp:Label>
                    </span>
                    <div class="leftNavContent">
                        <asp:Panel ID="OldShipLoginPanel" runat="server" DefaultButton="btnLogin">
                            <p>
                                <asp:Label ID="LoginError" runat="server" Text="" BackColor="#ffcc00" EnableViewState="false"></asp:Label>
                                <br />
                                Enter Username:
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ErrorMessage="Username is required"
                                    ControlToValidate="txtUsername" Display="Dynamic" ValidationGroup="OldShipLoginForm"><br />Username is Required</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="UsernameValidate" runat="server" ControlToValidate="txtUsername"
                                    Display="Dynamic" ErrorMessage="User name must have between 6-30 characters."
                                    ValidationGroup="OldShipLoginForm" ValidationExpression="^.*(?=^.{6,30}$)((?=.*\\d)||(.*[\\W])).*$"><br />Username must be 6 to 30 characters in length.</asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="textfield1" ValidationGroup="LoginForm"></asp:TextBox>
                                <br />
                                Enter Password:
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="textfield1" TextMode="Password"
                                    ValidationGroup="OldShipLoginForm"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ErrorMessage="Password is required."
                                    Display="Dynamic" ControlToValidate="txtPassword" ValidationGroup="OldShipLoginForm"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="PasswordValidate" runat="server" ErrorMessage="Password must be 6-15 characters and contain atleast 1 numeric"
                                    ValidationExpression="^.*(?=^.{6,15}$)(?=.*[a-zA-Z])(?=.*[0-9])((?=.*\\d)||(.*[\\W])).*$"
                                    ControlToValidate="txtPassword" Display="Dynamic" ValidationGroup="OldShipLoginForm"></asp:RegularExpressionValidator>
                                <asp:Button ID="btnLogin" runat="server" Text="GO >>" CssClass="formbutton3" OnClick="btnLogin_Click"
                                    ValidationGroup="OldShipLoginForm" />
                            </p>
                        </asp:Panel>
                        <div class="clear">
                        </div>
                    </div>
                </AnonymousTemplate>
                <LoggedInTemplate>
                    <ul id="navbar">
                        <li>
                            <asp:LoginStatus ID="LoginStatus1" runat="server" />
                        </li>
                    </ul>
                </LoggedInTemplate>
            </asp:LoginView>
        </div>
    </div>
</asp:Content>
