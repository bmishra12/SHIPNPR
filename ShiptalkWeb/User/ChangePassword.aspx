<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="ChangePassword.aspx.cs" Inherits="ShiptalkWeb.ChangePassword" Title="Change password" %>

<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls"
    TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="pp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Change password</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
     <asp:Panel ID="ResetResultPanel" runat="server" EnableViewState="false" Visible="false">
            <div class="dv3col">
                <h1>
                    <asp:Literal ID="ResetResultTitle" runat="server" Text="" EnableViewState="false"></asp:Literal>
                </h1>
                <asp:Literal ID="ResetResultDescription" runat="server" EnableViewState="false" Text=""></asp:Literal>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="ChangePasswordPanel" DefaultButton="btnChangePassword">
            <div class="dv4colleft">
                <h2>
                    Change my password</h2>
                <div style="margin-top: 8px; padding: 4px; background-color: #ffcc00;">
                    <span style="font-weight: bold;">Minimum security requirements</span>:
                    <br />
                    <span style="font-size: 12px;">Your password must be between 8 to 30 characters and must contain at least one upper case letter, at least one digit and at least one special character.</span>
                </div>
                <p>
                    Please enter your new password below:</p>
                <div>
                    <div style="float: left;">
                        <div style="width: 122px; float: left; border: solid 0px #000;">
                            New Password:</div>
                        <div style="width: 150px; float: right; border: solid 0px #000;">
                            <asp:TextBox ID="Password" runat="server" Width="142px" ValidationGroup="ChangePasswordGroup" ToolTip="New Password"
                                TextMode="Password"></asp:TextBox>
<%--                            <pp:PropertyProxyValidator ID="proxyValidatorPassword" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.UserAccountValidator" PropertyName="Password" ControlToValidate="Password" RulesetName="Data" CssClass="required" EnableClientScript="true" ValidationGroup="ChangePasswordGroup"/>
--%>                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ErrorMessage="Password is required."
                                Display="Dynamic" ControlToValidate="Password" ValidationGroup="ChangePasswordGroup"
                                EnableClientScript="False" Enabled="true"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="PasswordValidate" runat="server" ErrorMessage="&lt;br /&gt;Password entered does not meet minimum requirements."
ValidationExpression="^.*(?=^.{8,30}$)(?=.*\d)(?=.*[A-Z])(?=.*[\&quot;!#$%&'()*+,-./:;<=>?@[\]^_`{|}~]).*$" 
                                ControlToValidate="Password" Display="Dynamic" ValidationGroup="ChangePasswordGroup"
                                EnableClientScript="False" Enabled="true"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div style="float: left; margin-top: 9px">
                        <div style="width: 122px; float: left; border: solid 0px #000;">
                            Re-type Password:</div>
                        <div style="width: 150px; float: right; border: solid 0px #000;">
                            <asp:TextBox ID="ConfirmPassword" runat="server" Width="142px" ValidationGroup="ChangePasswordGroup" ToolTip="Re-type Password"
                                TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ErrorMessage="Please retype your password"
                                Display="Dynamic" ControlToValidate="ConfirmPassword" ValidationGroup="ChangePasswordGroup"
                                EnableClientScript="False" Enabled="true"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="ComparePassword" runat="server" ErrorMessage="&lt;br /&gt;Password and Re-type password must match"
                                ControlToValidate="ConfirmPassword" ControlToCompare="Password" Display="Dynamic"
                                ValidationGroup="ChangePasswordGroup" EnableClientScript="false"></asp:CompareValidator>
                        </div>
                    </div>
                    <div style="float: left; margin-top: 9px" runat="server" id="MessageArea" enableviewstate="false"
                        visible="false">
                        <div style="width: 272px; float: left; padding-right: 4px">
                            <asp:Label ID="Message" runat="server" Text="" EnableViewState="false" CssClass="required"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; margin-top: 9px">
                        <div style="width: 122px; float: left;">
                            &nbsp;
                        </div>
                        <div style="width: 150px; float: right; padding-right: 4px">
                            <asp:Button ID="btnChangePassword" runat="server" Text="Submit >>" CssClass="formbutton3"
                                OnClick="ChangePasswordCommand" ValidationGroup="ChangePasswordGroup" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
