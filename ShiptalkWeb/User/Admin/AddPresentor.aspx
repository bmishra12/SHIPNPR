<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPresentor.aspx.cs" Inherits="ShiptalkWeb.AddPresentor"
    Title="Add a new Presenter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkLogic.BusinessLayer" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls"
    TagPrefix="pp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add a new Presenter</title>
    <link href="~/css/global.css" rel="stylesheet" type="text/css" id="style" runat="server" />
    <link href="~/css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" runat="server" />

    <base target="_self" />

    <script type="text/javascript">
    function CloseMe(addPresentorStatus)
    {
//        if(addPresentorStatus == 1) {
//            var window_opener = window.dialogArguments;

//            window_opener.SetAddPresentorReturnVal(addPresentorStatus);
//        }
        self.close();
        return false;
    }
    </script>

</head>
<body>
    <div id="container1">
        <div id="body">
            <form id="PresentorForm" runat="server">
            <asp:ScriptManager ID="scriptManager" runat="server">
                <Scripts>
                    <asp:ScriptReference Path="~/scripts/global.js" />
                </Scripts>
            </asp:ScriptManager>
            <div id="maincontent">
                <div class="dv3col" runat="server" id="plhMessage" visible="false" enableviewstate="false">
                    <h1>
                        <asp:Label ID="lblTitleMessage" runat="server" Text="" EnableViewState="false"></asp:Label></h1>
                    <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false" CssClass="required"></asp:Label>
                </div>
                <div class="dv3col" runat="server" id="MainPanel">
                    <div style="text-align: left">
                        <h1>
                            <asp:Label ID="TitleLbl" runat="server" Text="New Presenter" EnableViewState="False"></asp:Label>
                        </h1>
                    </div>
                    <span class="smaller">
                        <asp:Literal ID="dv3colMessage" runat="server" Text="Add a new presenter. Once added, the presenter will be available in Public and Media access form."
                            EnableViewState="False"></asp:Literal>
                    </span>
                    <br />
                    <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceUserView" Width="100%"
                        DefaultMode="Edit" DataKeyNames="UserId">
                        <EditItemTemplate>
                            <asp:Panel ID="InsertDataPanel" runat="server" DefaultButton="btnSubmit">
                                <div id="dv3colFormContent" runat="server" style="margin-top: 10px; width: 615px;
                                    border-top: solid 2px #eee;">
                                    <table>
                                        <tr>
                                            <td style="width: 8px;">
                                            </td>
                                            <td colspan="3" style="margin-bottom: 2px; margin-bottom: 0px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <asp:PlaceHolder ID="plhMessage" runat="server" Visible="false" EnableViewState="false">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false" CssClass="required"></asp:Label>
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <tr>
                                            <td style="width: 8px;">
                                                <asp:Label ID="Label2" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                                    margin: 0px"></asp:Label>
                                            </td>
                                            <td style="width: 150px; text-align: left;">
                                                <asp:Label ID="FirstNameLabel" runat="server" Text="First Name" AssociatedControlID="FirstName"></asp:Label>
                                            </td>
                                            <td style="width: 4px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ErrorMessage="First name is required<br/>"
                                                    EnableClientScript="false" Display="Dynamic" SetFocusOnError="True" ControlToValidate="FirstName"
                                                    ValidationGroup="UserProfile" CssClass="validationMessage"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="FirstNameValidator" runat="server" ErrorMessage="Only alphabets, apostrophe, hyphens allowed; maximum 50 characters.<br />"
                                                    Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="FirstName"
                                                    EnableClientScript="false" ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$" CssClass="validationMessage"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="FirstName" runat="server" Text='<%# Bind("FirstName") %>' CssClass="InfoValueFont"
                                                    Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px;">
                                                &nbsp;
                                            </td>
                                            <td style="width: 150px; text-align: left;">
                                                <asp:Label ID="MiddleNameLabel" runat="server" Text="Middle Name" AssociatedControlID="MiddleName"></asp:Label>
                                            </td>
                                            <td style="width: 4px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:RegularExpressionValidator ID="MiddleNameValidator" runat="server" ErrorMessage="Only alphabets, apostrophe, hyphens allowed; maximum 50 characters.<br />"
                                                    Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="MiddleName"
                                                    EnableClientScript="false" ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$" CssClass="validationMessage"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="MiddleName" runat="server" Text='<%# Bind("MiddleName") %>' CssClass="InfoValueFont"
                                                    Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px;">
                                                <asp:Label ID="Label6" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                                    margin: 0px"></asp:Label>
                                            </td>
                                            <td style="width: 150px; text-align: left;">
                                                <asp:Label ID="LastNameLabel" runat="server" Text="Last Name" AssociatedControlID="LastName"></asp:Label>
                                            </td>
                                            <td style="width: 4px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ErrorMessage="Last name is required<br />"
                                                    Display="Dynamic" SetFocusOnError="True" ControlToValidate="LastName" ValidationGroup="UserProfile"
                                                    EnableClientScript="false" CssClass="validationMessage"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="LastNameValidator" runat="server" ErrorMessage="Only alphabets, apostrophe, hyphens allowed; maximum 50 characters.<br />"
                                                    Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="LastName"
                                                    EnableClientScript="false" ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$" CssClass="validationMessage"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="LastName" runat="server" Text='<%# Bind("LastName") %>' CssClass="InfoValueFont"
                                                    Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px;">
                                            </td>
                                            <td style="width: 150px; text-align: left;">
                                                <asp:Label ID="NickNameLabel" runat="server" Text="Nick Name" AssociatedControlID="NickName"></asp:Label>
                                            </td>
                                            <td style="width: 4px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:RegularExpressionValidator ID="NickNameValidator" runat="server" ErrorMessage="Only alphabets, apostrophe, hyphens allowed; maximum 50 characters.<br />"
                                                    Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="NickName"
                                                    EnableClientScript="false" ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$" CssClass="validationMessage"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="NickName" runat="server" Text='<%# Bind("NickName") %>' CssClass="InfoValueFont"
                                                    Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px;">
                                                <asp:Label ID="Label3" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                                    margin: 0px"></asp:Label>
                                            </td>
                                            <td style="width: 150px; text-align: left;">
                                                <asp:Label ID="PrimaryEmailLabel" runat="server" Text="Primary Email" AssociatedControlID="Email"></asp:Label>
                                            </td>
                                            <td style="width: 4px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ErrorMessage="The Primary email address is required<br />"
                                                    EnableClientScript="false" Display="Dynamic" SetFocusOnError="True" ControlToValidate="Email"
                                                    ValidationGroup="UserProfile" CssClass="validationMessage"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="EmailFmtValidate" runat="server" ErrorMessage="Primary email is not in a valid format<br />"
                                                    ValidationExpression="^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$"
                                                    SetFocusOnError="True" ControlToValidate="Email" Display="Dynamic" ValidationGroup="UserProfile"
                                                    EnableClientScript="false" CssClass="validationMessage"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="Email" runat="server" Text='<%# Bind("PrimaryEmail") %>' CssClass="InfoValueFont"
                                                    Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="Email"
                                                    WatermarkText="Primary Email must be valid and unique" WatermarkCssClass="textfield3wm" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px;">
                                                <asp:Label ID="Label4" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                                    margin: 0px"></asp:Label>
                                            </td>
                                            <td style="width: 150px; text-align: left;">
                                                <asp:Label ID="PrimaryPhoneLabel" runat="server" Text="Primary Phone" AssociatedControlID="PrimaryPhone"></asp:Label>
                                            </td>
                                            <td style="width: 4px">
                                                &nbsp;
                                            </td>
                                            <td style="width: 305px">
                                                <asp:RequiredFieldValidator ID="PrimaryPhoneRequired" runat="server" ErrorMessage="Primary phone is required<br />"
                                                    Display="Dynamic" SetFocusOnError="True" ControlToValidate="PrimaryPhone" ValidationGroup="UserProfile"
                                                    EnableClientScript="false" CssClass="validationMessage"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="PrimaryPhoneValidate" runat="server" ErrorMessage="Primary phone is not in a valid format<br />"
                                                    ValidationExpression="\(?\d{3}\)?[-\s.]?\d{3}[-.]\d{4}( x\d{0,5})?" SetFocusOnError="True"
                                                    ControlToValidate="PrimaryPhone" Display="Dynamic" ValidationGroup="UserProfile"
                                                    EnableClientScript="false" CssClass="validationMessage"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="PrimaryPhone" runat="server" Text='<%# Bind("PrimaryPhone") %>'
                                                    CssClass="InfoValueFont" Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="PrimaryPhone"
                                                    WatermarkText="e.g., 202-555-1234 x1234" WatermarkCssClass="textfield3wm" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px;">
                                            </td>
                                            <td style="width: 150px; text-align: left;">
                                            </td>
                                            <td style="width: 4px">
                                                &nbsp;&nbsp;
                                            </td>
                                            <td style="width: 305px;">
                                                <table style="width: 256px;">
                                                    <tr>
                                                        <td style="width: 125px;" align="left">
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="formbutton3" Width="110px"
                                                                UseSubmitBehavior="true" CommandName="Update" ValidationGroup="UserProfile" />
                                                        </td>
                                                        <td style="width: 125px">
                                                            <input id="btnClose" type="button" value="Close" onclick='javascript:CloseMe(<%= AddPresentorStatus %>);' class="formbutton3" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px;">
                                            </td>
                                            <td colspan="3" style="margin-bottom: 2px; margin-bottom: 0px">
                                            &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </EditItemTemplate>
                    </asp:FormView>
                </div>
            </div>
            <pp:ObjectContainerDataSource ID="dataSourceUserView" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.UserViewData"
                OnSelecting="dataSourceUserView_Selecting" OnUpdating="dataSourceUserView_Updating"
                OnUpdated="dataSourceUserView_Updated" />
            </form>
        </div>
    </div>
</body>
</html>
