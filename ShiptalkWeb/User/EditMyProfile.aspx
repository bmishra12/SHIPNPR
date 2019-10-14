<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="EditMyProfile.aspx.cs" Inherits="ShiptalkWeb.EditMyProfile" Title="User Information - Edit" %>

<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkLogic.BusinessLayer" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls"
    TagPrefix="pp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .InfoValueFont
        {
            font-size: 12px;
            font-family: Arial;
            font-weight: bold;
            word-wrap: break-word;
        }
        .Descriptors
        {
            padding-left: 0px;
            margin-left: 10px;
            list-style-type: circle;
            font-weight: bold;
            font-family: Arial;
            font-size: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col" runat="server" id="plhMessage" visible="false" enableviewstate="false">
            <h1>
                <asp:Label ID="lblTitleMessage" runat="server" Text="" EnableViewState="false"></asp:Label></h1>
            <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false" CssClass="required"></asp:Label>
        </div>
        <div class="dv3col" runat="server" id="MainPanel">
            <div style="text-align: left">
                <div class="commands">
                    <a href='<%# RouteController.UserView(UserIdOfProfileToEdit) %>' runat="server" id="viewProfileLink">
                        View complete profile</a><br />
                                             <a runat="server" id="A1" href='<%# RouteController.EditMyEmail() %>' visible="true">
                            Edit My Email</a>
                            <br />
                                             <a runat="server" id="A2" href='<%# RouteController.ChangePassword() %>' visible="true">
                            Change My Password<br /></a>
                    <asp:Panel ID="AdminLinksPanel" runat="server">
                        <a runat="server" href='<%# RouteController.EmailChangeVerificationsPending() %>' id="pendingEmailChangeVerifications" visible="false">
                              Pending Email Change Verifications</a>
                     </asp:Panel>
                </div>
                 

                <h1>
                    <asp:Label ID="TitleLbl" runat="server" Text="My Profile" EnableViewState="false"></asp:Label>
                </h1>
            </div>
            <span class="smaller">
                <asp:Literal ID="dv3colMessage" runat="server" Text="Edit your personal information such as name, phone number etc."
                    EnableViewState="false"></asp:Literal>
            </span>
            <br />
            <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceUserView" Width="100%"
                DefaultMode="Edit" DataKeyNames="UserId">
                <EditItemTemplate>
                    <asp:Panel ID="EditDataPanel" runat="server" DefaultButton="btnSubmit">
                        <div id="dv3colFormContent" runat="server" style="margin-top: 10px; width: 615px;
                            border-top: solid 2px #eee;">
                            <table>
                                <asp:PlaceHolder ID="plhMessage" runat="server" Visible="false" EnableViewState="false">
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false" CssClass="required"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <asp:PlaceHolder ID="plhRoleInformation" runat="server" Visible='<%# IsSingleProfileUser %>'>
                                    <tr>
                                        <td class="tdFormRequiredLabel">
                                        </td>
                                        <td class="tdFormLabel">
                                            <asp:Label ID="RoleLabel" runat="server" Text="Role" ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="RoleSelected" runat="server" Text='<%# UserProfileRoleTitle %>' CssClass="InfoValueFont"
                                                Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdFormRequiredLabel">
                                        </td>
                                        <td class="tdFormLabel">
                                            <asp:Label ID="Label1" runat="server" Text="Role description"></asp:Label>
                                        </td>
                                        <td>
                                            <div style="width: 280px;">
                                                <span style="font-weight: normal; font-family: Arial; font-size: 12px;">
                                                    <asp:Label ID="lblRoleDescription" runat="server" Text='<%# UserProfileRoleDescription %>'
                                                        Visible='<%# !((bool)Eval("IsShipDirector")) %>'></asp:Label></span>
                                                <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible='<%# (bool)Eval("IsShipDirector") %>'>
                                                    <div style="border: solid 0px #000; padding: 4px; background-color: #FFCC00; width: 232px">
                                                        <asp:Label ID="Label3" runat="server" Text="State SHIP Director" Font-Bold="true"></asp:Label>
                                                    </div>
                                                </asp:PlaceHolder>
                                            </div>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible='<%# !IsSingleProfileUser %>'>
                                    <tr>
                                        <td class="tdFormRequiredLabel">
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text='<%# ((Scope)Eval("Scope")).Description() + " Access"  %>'
                                                AssociatedControlID="MultiAccessLabel"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="MultiAccessLabel" runat="server" Text='<%# "Multiple " + ((Scope)Eval("Scope")).Description() + " access" %>'
                                                CssClass="InfoValueFont" Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="divStates" runat="server" Visible='<%# !((bool)Eval("IsUserCMSRegionalScope") || (bool)Eval("IsUserCMSScope")) %>'>
                                    <tr>
                                        <td class="tdFormRequiredLabel">
                                        </td>
                                        <td>
                                            <asp:Label ID="StatesLabel" runat="server" Text="State" ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="StateSelected" runat="server" Font-Bold="true" Text='<%# Eval("StateName").EncodeHtml().ToCamelCasing() %>'
                                                CssClass="InfoValueFont" Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td class="tdFormRequiredLabel">
                                        <asp:Label ID="Label4" runat="server" CssClass="required" Text="*"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="FirstNameLabel" runat="server" Text="First Name" AssociatedControlID="FirstName"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="FirstName" runat="server" Text='<%# Bind("FirstName") %>' CssClass="textfield3"
                                            ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ErrorMessage="<br/>First name is required"
                                            EnableClientScript="false" Display="Dynamic" SetFocusOnError="True" ControlToValidate="FirstName"
                                            ValidationGroup="UserProfile"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="FirstNameValidator" runat="server" ErrorMessage="<br />Only alphabets, apostrophe, hyphens allowed; maximum 50 characters."
                                            Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="FirstName"
                                            EnableClientScript="false" ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="MiddleNameLabel" runat="server" Text="Middle Name" AssociatedControlID="MiddleName"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MiddleName" runat="server" Text='<%# Bind("MiddleName") %>' CssClass="textfield3"
                                            ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="MiddleNameValidator" runat="server" ErrorMessage="<br />Only alphabets, apostrophe, hyphens allowed; maximum 50 characters."
                                            Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="MiddleName"
                                            EnableClientScript="false" ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" CssClass="required" Text="*"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LastNameLabel" runat="server" Text="Last Name" AssociatedControlID="LastName"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="LastName" runat="server" Text='<%# Bind("LastName") %>' CssClass="textfield3"
                                            ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ErrorMessage="&lt;br /&gt;Last name is required"
                                            Display="Dynamic" SetFocusOnError="True" ControlToValidate="LastName" ValidationGroup="UserProfile"
                                            EnableClientScript="false"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="LastNameValidator" runat="server" ErrorMessage="<br />Only alphabets, apostrophe, hyphens allowed; maximum 50 characters."
                                            Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="LastName"
                                            EnableClientScript="false" ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="NickNameLabel" runat="server" Text="Nick Name" AssociatedControlID="NickName"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="NickName" runat="server" Text='<%# Bind("NickName") %>' CssClass="textfield3"
                                            ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="NickNameValidator" runat="server" ErrorMessage="<br />Only alphabets, apostrophe, hyphens allowed; maximum 50 characters."
                                            Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="NickName"
                                            EnableClientScript="false" ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="SuffixLabel" runat="server" Text="Suffix" AssociatedControlID="Suffix"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Suffix" runat="server" Text='<%# Bind("Suffix") %>' CssClass="textfield3"
                                            ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="SuffixValidator" runat="server" ErrorMessage="<br />Only alphabets, numbers, hyphens allowed; maximum 20 characters."
                                            Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="Suffix"
                                            EnableClientScript="false" ValidationExpression="^[a-zA-Z\-.\s]{1,20}$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="HonorificsLabel" runat="server" Text="Honorifics" AssociatedControlID="Honorifics"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Honorifics" runat="server" Text='<%# Bind("Honorifics") %>' CssClass="textfield3"
                                            ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="HonorificsValidator" runat="server" ErrorMessage="<br />Only alphabets, numbers, hyphens allowed; maximum 20 characters."
                                            Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="Honorifics"
                                            EnableClientScript="false" ValidationExpression="^[a-zA-Z\-.\s]{1,20}$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" CssClass="required" Text="*"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="PrimaryEmailLabel" runat="server" Text="Primary Email" AssociatedControlID="Email"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="Email" runat="server" Text='<%# Eval("PrimaryEmail") %>' CssClass="textfield3wm"
                                            ValidationGroup="UserProfile" ReadOnly="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ErrorMessage="<br />The Primary email address is required"
                                            EnableClientScript="false" Display="Dynamic" SetFocusOnError="True" ControlToValidate="Email"
                                            ValidationGroup="UserProfile"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="EmailFmtValidate" runat="server" ErrorMessage="<br />Primary email is not in a valid format"
                                            ValidationExpression="^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$"
                                            SetFocusOnError="True" ControlToValidate="Email" Display="Dynamic" ValidationGroup="UserProfile"
                                            EnableClientScript="false"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="SecondaryEmailLabel" runat="server" Text="Secondary Email" AssociatedControlID="SecondaryEmail"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="SecondaryEmail" runat="server" Text='<%# Bind("SecondaryEmail") %>'
                                            CssClass="textfield3" ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="SecondaryEmailFmtValidate" runat="server" ErrorMessage="<br/ >Secondary email is not in a valid format"
                                            ValidationExpression="^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$"
                                            SetFocusOnError="True" ControlToValidate="SecondaryEmail" Display="Dynamic" ValidationGroup="UserProfile"
                                            EnableClientScript="false"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" CssClass="required" Text="*"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="PrimaryPhoneLabel" runat="server" Text="Primary Phone" AssociatedControlID="PrimaryPhone"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="PrimaryPhone" runat="server" Text='<%# Bind("PrimaryPhone") %>'
                                            CssClass="textfield3" ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PrimaryPhoneRequired" runat="server" ErrorMessage="<br />Primary phone is required"
                                            Display="Dynamic" SetFocusOnError="True" ControlToValidate="PrimaryPhone" ValidationGroup="UserProfile"
                                            EnableClientScript="false"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="PrimaryPhoneValidate" runat="server" ErrorMessage="<br/>Primary phone is not in a valid format"
                                            ValidationExpression="\(?\d{3}\)?[-\s.]?\d{3}[-.]\d{4}( x\d{0,5})?" SetFocusOnError="True"
                                            ControlToValidate="PrimaryPhone" Display="Dynamic" ValidationGroup="UserProfile"
                                            EnableClientScript="false"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="SecondaryPhoneLabel" runat="server" Text="Secondary Phone" AssociatedControlID="SecondaryPhone"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="SecondaryPhone" runat="server" Text='<%# Bind("SecondaryPhone") %>'
                                            CssClass="textfield3" ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="SecondaryPhoneFmtValidate" runat="server" ErrorMessage="<br />Secondary phone is not in a valid format"
                                            ValidationExpression="\(?\d{3}\)?[-\s.]?\d{3}[-.]\d{4}( x\d{0,5})?" SetFocusOnError="True"
                                            EnableClientScript="false" ControlToValidate="SecondaryPhone" Display="Dynamic"
                                            ValidationGroup="UserProfile"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="PlaceHolder3" runat="server" Visible='<%# (bool)Eval("IsStateLevel") && IsDataSubmitter %>'>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Zip code of Counseling Location
                                        </td>
                                        <td>
                                            <div style="width: 256px; background: #fff; color: #000; padding: 3px 0 8px 4px;">
                                                <asp:DropDownList ID="ddlZipcodes" runat="server" CssClass="dropdown2wm" DataSource='<%# Zipcodes %>'
                                                    AppendDataBoundItems="true" SelectedValue='<%# CurrentZipcode %>' ToolTip="Zip code of Counseling Location">
                                                    <asp:ListItem Text="--Select zip code--" Value="0" />
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            County of Counseling Location
                                        </td>
                                        <td>
                                            <div style="width: 256px; background: #fff; color: #000; padding: 3px 0 8px 4px;">
                                                <asp:DropDownList ID="ddlCounties" runat="server" CssClass="dropdown2wm" DataSource='<%# Counties %>'
                                                    DataValueField="Key" DataTextField="Value" AppendDataBoundItems="true" ToolTip="County of Counseling Location" SelectedValue='<%# CurrentCounty %>'>
                                                    <asp:ListItem Text="--Select county--" Value="0" />
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="plhDescriptors" runat="server" Visible='<%# (((bool)Eval("IsUserStateScope")) || ((bool)Eval("IsCMSLevel"))) && ((bool)Eval("HasDescriptors")) %>'>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Task Functions and Access Authorizations
                                        </td>
                                        <td>
                                            <div style="width: 256px; background: #fff; color: #000; padding: 3px 0 8px 4px;">
                                                <asp:BulletedList ID="Descriptors" runat="server" DataSource='<%# (IEnumerable<string>)Eval("GetAllDescriptorNamesForUser") %>'
                                                    CssClass="Descriptors" BulletStyle="Disc" ValidationGroup="UserProfile">
                                                </asp:BulletedList>
                                            </div>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                               <asp:PlaceHolder  ID="PlaceHolder4" runat="server" Visible='<%# IsQualifiedForUniqueID %>'>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            CMS SHIP Unique ID
                                        </td>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text='[Pending Approval]' Visible='<%# UniqueIDData.IsPendingApproval %>'></asp:Label>
                                            <asp:Label ID="Label7" runat="server" Text='<%# UniqueIDData.UniqueID %>' Visible='<%# UniqueIDData.IsApproved %>'></asp:Label>
                                            <asp:Button ID="btnReqUniqueId" runat="server" Text="Request Unique ID" CssClass="formbutton3" 
                                                OnCommand="RequestUniqueId_Click" Visible='<%# !UniqueIDData.IsPendingApproval && !UniqueIDData.IsApproved %>' />
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 125px;" align="left">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="formbutton1a" 
                                                        UseSubmitBehavior="true" CommandName="Update" ValidationGroup="UserProfile" />
                                                </td>
                                                <td style="width: 125px">
                                                </td>
                                            </tr>
                                        </table>
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
        OnSelecting="dataSourceUserView_Selecting" OnUpdated="dataSourceUserView_Updated"
        OnUpdating="dataSourceUserView_Updating" />
</asp:Content>
