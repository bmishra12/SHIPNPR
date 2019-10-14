<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="UserEdit.aspx.cs" Inherits="ShiptalkWeb.UserEdit" Title="User Information - Edit" %>

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
                <h1>
                    <asp:Label ID="TitleLbl" runat="server" Text="Edit User Information" EnableViewState="false"></asp:Label>
                </h1>
            </div>
            <span class="smaller">
                <asp:Literal ID="dv3colMessage" runat="server" Text="Edit User profile enables changes to the personal profile of a User account as well as the access rights pertaining to Agencies, Sub State regions or other SHIP regions"
                    EnableViewState="false"></asp:Literal>
            </span>
            <br />
            <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceUserView" Width="100%"
                DefaultMode="Edit" DataKeyNames="UserId" OnPreRender="formView_PreRender">
                <EditItemTemplate>
                    <asp:Panel ID="EditDataPanel" runat="server" DefaultButton="btnSubmit">
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
                                <asp:PlaceHolder ID="plhRoleInformation" runat="server" Visible='<%# IsSingleProfileUser %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="RoleLabel" runat="server" Text="Role" ></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px; padding: 2px;">
                                            <asp:Label ID="RoleSelected" runat="server" Text='<%# UserProfileRoleTitle %>' CssClass="InfoValueFont"
                                                Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="Label1" runat="server" Text="Role description" ></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <div style="width: 280px;">
                                                <span style="font-weight: normal; font-family: Arial; font-size: 12px;">
                                                    <asp:Label ID="lblRoleDescription" runat="server" Text='<%# UserProfileRoleDescription %>'
                                                        Visible='<%# !((bool)Eval("IsShipDirector")) %>'></asp:Label></span>
                                                <asp:PlaceHolder ID="PlaceHolder3" runat="server" Visible='<%# (bool)Eval("IsShipDirector") %>'>
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
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="Label2" runat="server" Text='<%# ((Scope)Eval("Scope")).Description() + " Access"  %>'
                                                AssociatedControlID="MultiAccessLabel"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px; padding: 2px;">
                                            <asp:Label ID="MultiAccessLabel" runat="server" Text='<%# "Multiple " + ((Scope)Eval("Scope")).Description() + " access" %>'
                                                CssClass="InfoValueFont" Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <%--<asp:PlaceHolder ID="plhIsShipDirector" runat="server" Visible='<%# Eval("IsShipDirector") %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <div style="border: solid 0px #000; padding: 4px; background-color: #FFCC00; width: 232px">
                                                <asp:Label ID="ShipDirectorAccessRequested" runat="server" Text="State SHIP Director"
                                                    Font-Bold="true"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>--%>
                                <asp:PlaceHolder ID="divStates" runat="server" Visible='<%# !((bool)Eval("IsUserCMSRegionalScope") || (bool)Eval("IsUserCMSScope")) %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="StatesLabel" runat="server" Text="State" ></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <asp:Label ID="StateSelected" runat="server" Font-Bold="true" Text='<%# Eval("StateName").EncodeHtml().ToCamelCasing() %>'
                                                CssClass="InfoValueFont" Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="FirstNameLabel" runat="server" Text="First Name" AssociatedControlID="FirstName"></asp:Label>
                                        <asp:Label ID="Label6" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                            margin: 0px"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:TextBox ID="FirstName" runat="server" Text='<%# Bind("FirstName") %>' CssClass="InfoValueFont"
                                            Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ErrorMessage="<br/>First name is required"
                                            EnableClientScript="false" Display="Dynamic" SetFocusOnError="True" ControlToValidate="FirstName"
                                            ValidationGroup="UserProfile"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="FirstNameValidator" runat="server" ErrorMessage="<br />Only alphabets, apostrophe, hyphens allowed; maximum 50 characters."
                                            Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="FirstName"
                                            EnableClientScript="false" ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="MiddleNameLabel" runat="server" Text="Middle Name" AssociatedControlID="MiddleName"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:TextBox ID="MiddleName" runat="server" Text='<%# Bind("MiddleName") %>' CssClass="InfoValueFont"
                                            Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="MiddleNameValidator" runat="server" ErrorMessage="<br />Only alphabets, apostrophe, hyphens allowed; maximum 50 characters."
                                            Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="MiddleName"
                                            EnableClientScript="false" ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="LastNameLabel" runat="server" Text="Last Name" AssociatedControlID="LastName"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                            margin: 0px"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:TextBox ID="LastName" runat="server" Text='<%# Bind("LastName") %>' CssClass="InfoValueFont"
                                            Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ErrorMessage="&lt;br /&gt;Last name is required"
                                            Display="Dynamic" SetFocusOnError="True" ControlToValidate="LastName" ValidationGroup="UserProfile"
                                            EnableClientScript="false"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="LastNameValidator" runat="server" ErrorMessage="<br />Only alphabets, apostrophe, hyphens allowed; maximum 50 characters."
                                            Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="LastName"
                                            EnableClientScript="false" ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="NickNameLabel" runat="server" Text="Nick Name" AssociatedControlID="NickName"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:TextBox ID="NickName" runat="server" Text='<%# Bind("NickName") %>' CssClass="InfoValueFont"
                                            Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="NickNameValidator" runat="server" ErrorMessage="<br />Only alphabets, apostrophe, hyphens allowed; maximum 50 characters."
                                            Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="NickName"
                                            EnableClientScript="false" ValidationExpression="^[a-zA-Z'\-.\s]{1,50}$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="SuffixLabel" runat="server" Text="Suffix" AssociatedControlID="Suffix"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:TextBox ID="Suffix" runat="server" Text='<%# Bind("Suffix") %>' CssClass="InfoValueFont"
                                            Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="SuffixValidator" runat="server" ErrorMessage="<br />Only alphabets, numbers, hyphens allowed; maximum 20 characters."
                                            Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="Suffix"
                                            EnableClientScript="false" ValidationExpression="^[a-zA-Z\-.\s]{1,20}$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="HonorificsLabel" runat="server" Text="Honorifics" AssociatedControlID="Honorifics"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:TextBox ID="Honorifics" runat="server" Text='<%# Bind("Honorifics") %>' CssClass="InfoValueFont"
                                            Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="HonorificsValidator" runat="server" ErrorMessage="<br />Only alphabets, numbers, hyphens allowed; maximum 20 characters."
                                            Display="Dynamic" ValidationGroup="UserProfile" SetFocusOnError="True" ControlToValidate="Honorifics"
                                            EnableClientScript="false" ValidationExpression="^[a-zA-Z\-.\s]{1,20}$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="PrimaryEmailLabel" runat="server" Text="Primary Email" AssociatedControlID="Email"></asp:Label>
                                        <asp:Label ID="Label5" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                            margin: 0px"></asp:Label>
                                        <br />
                                        <%--<asp:Label ID="Label8" runat="server" Text="(Read-Only)" CssClass="smaller"></asp:Label>--%>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:TextBox ID="Email" runat="server" Text='<%# Eval("PrimaryEmail") %>' CssClass="InfoValueFont"
                                            Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
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
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="SecondaryEmailLabel" runat="server" Text="Secondary Email" AssociatedControlID="SecondaryEmail"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:TextBox ID="SecondaryEmail" runat="server" Text='<%# Bind("SecondaryEmail") %>'
                                            CssClass="InfoValueFont" Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="SecondaryEmailFmtValidate" runat="server" ErrorMessage="<br/ >Secondary email is not in a valid format"
                                            ValidationExpression="^[_a-zA-Z0-9-]+(\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.(([0-9]{1,3})|([a-zA-Z]{2,3})|(aero|coop|info|museum|name))$"
                                            SetFocusOnError="True" ControlToValidate="SecondaryEmail" Display="Dynamic" ValidationGroup="UserProfile"
                                            EnableClientScript="false"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="PrimaryPhoneLabel" runat="server" Text="Primary Phone" AssociatedControlID="PrimaryPhone"></asp:Label>
                                        <asp:Label ID="Label7" runat="server" CssClass="required" Text="*" Style="padding: 0px;
                                            margin: 0px"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:TextBox ID="PrimaryPhone" runat="server" Text='<%# Bind("PrimaryPhone") %>'
                                            CssClass="InfoValueFont" Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
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
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="SecondaryPhoneLabel" runat="server" Text="Secondary Phone" AssociatedControlID="SecondaryPhone"></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:TextBox ID="SecondaryPhone" runat="server" Text='<%# Bind("SecondaryPhone") %>'
                                            CssClass="InfoValueFont" Width="256px" ValidationGroup="UserProfile"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="SecondaryPhoneFmtValidate" runat="server" ErrorMessage="<br />Secondary phone is not in a valid format"
                                            ValidationExpression="\(?\d{3}\)?[-\s.]?\d{3}[-.]\d{4}( x\d{0,5})?" SetFocusOnError="True"
                                            EnableClientScript="false" ControlToValidate="SecondaryPhone" Display="Dynamic"
                                            ValidationGroup="UserProfile"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                 <asp:PlaceHolder ID="plhApproverDesignate" runat="server" Visible='<%# (bool)Eval("IsUserStateScope") || (bool)Eval("IsUserCMSScope") %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="Label9" runat="server" Text="Can Approve User Registrations" AssociatedControlID="cbIsApprover" ></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td class="c305">
                                            <asp:CheckBox ID="cbIsApprover" runat="server" Checked='<%# CanapproveUsers %>' Enabled='<%# EnableApproverCheckbox %>'
                                                ValidationGroup="UserProfile" />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="Label10" runat="server" Text='<%# "Is " + ((Scope)Eval("Scope")).Description() + " Administrator" %>' AssociatedControlID="cbIsAdmin" ></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td class="c305">
                                            <asp:CheckBox ID="cbIsAdmin" runat="server" Checked='<%# Bind("IsAdmin") %>' ValidationGroup="UserProfile" />
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="PlaceHolder4" runat="server" Visible='<%# (!(bool)Eval("IsShipDirector") && (bool)Eval("IsUserStateScope")) %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                           <asp:Label ID="lblIsSuperDataEditor" Text="Is Super Data Editor" runat="server" AssociatedControlID="CheckBox1"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td class="c305">
                                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsStateSuperDataEditor") %>' ValidationGroup="UserProfile" />
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="plhDescriptors" runat="server" Visible='<%# (!(bool)Eval("IsShipDirector") && (bool)Eval("IsUserStateScope")) %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            Task Functions and Access Authorizations
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <div style="width: 256px; background: #F2F2F2; color: #000; padding: 3px 0 8px 0px;
                                                border-top: solid 1px #E6E6E6;">
                                                <asp:CheckBoxList ID="Descriptors" runat="server" CssClass="Descriptors">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        Login Account Status
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <div style="width: 256px; background: #fff; color: #000; padding: 3px 0 8px 4px;">
                                            <asp:RadioButton ID="rbActive" runat="server" Checked='<%# (bool)Eval("IsActive") %>'
                                                ValidationGroup="UserProfile" Text="Active" GroupName="ActiveInactive" />
                                            <asp:RadioButton ID="rbInActive" runat="server" Checked='<%# !(bool)Eval("IsActive") %>'
                                                ValidationGroup="UserProfile" Text="Inactive" GroupName="ActiveInactive" />
                                        </div>
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="plhReviewers" runat="server" Visible='<%# (bool)Eval("IsUserStateScope") && !(bool)Eval("IsShipDirector") %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                           <asp:Label ID="lblSupervisor" Text="Supervisor" runat="server" AssociatedControlID="ddlReviewers"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <div style="width: 256px; background: #fff; color: #000; padding: 3px 0 8px 4px;">
                                                <asp:DropDownList ID="ddlReviewers" runat="server" CssClass="dropdown2wm" DataSource='<%# Supervisors %>'
                                                    DataTextField="Value" DataValueField="Key" SelectedValue='<%# CurrentSupervisor %>'
                                                    AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr style="visibility:hidden;">
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="Label8" runat="server" Text="Old Primary Email" AssociatedControlID="OldEmail"></asp:Label>   
                                            <br />                                     
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <asp:TextBox ID="OldEmail" runat="server" Text='<%# Eval("PrimaryEmail") %>' ></asp:TextBox>                                        
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td style="width: 305px;">
                                        <table style="width: 256px;">
                                            <tr>
                                                <td style="width: 125px;" align="left">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="formbutton3" Width="110px"
                                                        UseSubmitBehavior="true" CommandName="Update" 
                                                        OnClick ="btnSubmit_Click"
                                                        ValidationGroup="UserProfile" />
                                                </td>
                                                <td style="width: 125px">
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
                                <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible='<%# ((bool)Eval("IsUserAgencyScope")) || ((bool)Eval("IsUserSubStateRegionalScope")) %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td style="width: 305px; text-align: right">
                                            <a id="A1" runat="server" href="<%# GetAddRoute() %>">Add User to another
                                                <%# GetScopeDisplayText %></a>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                            </table>
                        </div>
                    </asp:Panel>
                </EditItemTemplate>
            </asp:FormView>
            <div id="RegionsContent" class="section">
                <asp:ListView runat="server" ID="listViewUserRegionalProfiles" DataSourceID="dataSourceRegionalProfiles"
                    ItemPlaceholderID="placeHolder" DataKeyNames="UserId">
                    <LayoutTemplate>
                        <table class="dataTable">
                            <thead>
                                <tr>
                                    <th scope="col" style="width: 200px; text-align: left; padding-left: 4px">
                                        Agency/Region Name
                                    </th>
                                    <th scope="col" style="text-align: left; padding-left: 4px; width: 180px">
                                        User Role and Status
                                    </th>
                                    <th scope="col" style="text-align: left; padding-left: 4px; width: 80px">
                                        Is Default Agency/Region
                                    </th>
                                    <th scope="col" style="text-align: left; padding-left: 4px; width: 55px">
                                        Select
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr runat="server" id="placeHolder">
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="2">
                                        <a id="A4" runat="server" href='<%# GetAddRoute() %>' title="Add User to a new sub state region">
                                            <%# "Add to another " + GetScopeDisplayText %></a>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td scope="row" style="vertical-align: top;">
                                <div style="width: 200px; word-wrap: break-word">
                                    <span class="smaller">
                                        <%# Eval("RegionName").EncodeHtml().ToCamelCasing() %>
                                    </span>
                                    <br />
                                </div>
                            </td>
                            <td style="vertical-align: top">
                                <div style="width: 180px; word-wrap: break-word">
                                    <span class="smaller">
                                        <%# GetUserScopeForRegionalUser((bool)Eval("IsAdmin")).ToCamelCasing() %>
                                    </span>
                                </div>
                                <div style="width: 180px; word-wrap: normal">
                                    <span class="smaller"><span class="gray">
                                        <img alt="ActiveOrInactiveImage" src='<%# this.ResolveUrl("~/images/" + (((bool)Eval("IsActive")) ? "activestatus.png" : "inactivestatus.png"))  %>'>&nbsp;<%# ((bool)Eval("IsActive"))? "Active" : "Inactive" %></img>
                                    </span></span>
                                </div>
                            </td>
                            <td style="vertical-align: top">
                                <div style="margin-left: 4px;">
                                    <span class="smaller">
                                        <%# ((bool)Eval("IsDefaultRegion")).ToString().ToCamelCasing() %>
                                    </span>
                                </div>
                            </td>
                            <td valign="top">
                                <span class="smaller"><a id="A3" runat="server" href='<%# GetEditRoute((int)Eval("RegionId")) %>'
                                    title="Edit">Edit</a></span>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                <div style="text-align: center;">
                    <asp:DataPager ID="pager" runat="server" PageSize="5" PagedControlID="listViewUserRegionalProfiles">
                        <Fields>
                            <asp:NumericPagerField />
                        </Fields>
                    </asp:DataPager>
                </div>
            </div>
        </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceUserView" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.UserViewData"
        OnSelecting="dataSourceUserView_Selecting" OnUpdated="dataSourceUserView_Updated" 
        OnUpdating="dataSourceUserView_Updating" />
    <pp:ObjectContainerDataSource ID="dataSourceRegionalProfiles" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UserRegionalAccessProfile"
        OnSelecting="dataSourceRegionalProfiles_Selecting" />
</asp:Content>
