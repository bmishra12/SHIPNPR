﻿<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="PendingEmailChangeUserView.aspx.cs" Inherits="ShiptalkWeb.PendingEmailChangeUserView"
    Title="Pending Email Change Verification - User Information" %>

<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
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
            margin-left: 2px;
            list-style-type: circle;
            font-weight: bold;
            font-family: Arial;
            font-size: 12px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col" runat="server" id="MainPanel">
            <div style="text-align: left">
                <h1>
                    <asp:Label ID="lblTitle" runat="server" Text="Resend Email change verification email"  EnableViewState="false"></asp:Label>
                </h1>
            </div>
            <span class="smaller">
                <asp:Literal ID="dv3colMessage" runat="server" Text="During Email change process an email is sent to the User, requesting that the person's email be verified by following a link in the email. In cases where the User did not receive the email for various reasons or if the Email verification link is expired, use this Resend email notification feature to resend the email."
                    EnableViewState="true"></asp:Literal>
            </span>
            <br />
            <br />
            <div style="text-align: right">
                <a runat="server" href="~/user/admin/PendingEmailChange">Back to pending Email change verifications</a>
            </div>
            <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceUserView" Width="100%" >
                <ItemTemplate>
                    <asp:Panel ID="RegistrationDataPanel" runat="server" DefaultButton="btnReSendEmail">
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
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="RoleLabel" runat="server" Text="Role" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px; padding: 2px;">
                                        <asp:Label ID="RoleSelected" runat="server" Text='<%# Eval("RoleTitle").EncodeHtml() %>' CssClass="InfoValueFont"
                                            Width="286px"></asp:Label>
                                    </td>
                                </tr>  
                                <asp:PlaceHolder ID="plhCMSRegion" runat="server" Visible='<%# Eval("IsUserCMSRegionalScope") %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="CMSRegionLabel" runat="server" Text="CMS Region" ></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td class="c305">
                                            <asp:Label ID="CMSRegionSelected" runat="server" Text='<%# Eval("CMSRegionName").EncodeHtml().ToCamelCasing() %>'
                                                CssClass="InfoValueFont" Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
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
                                        <td class="c305">
                                            <asp:Label ID="StateSelected" runat="server" Text='<%# Eval("StateName").EncodeHtml() %>' CssClass="InfoValueFont"
                                                Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder> 
                                <asp:PlaceHolder ID="plhSubStateRegion" runat="server" Visible='<%# IsMultiSubStateAndDefaultSubStateExist %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="SubStateRegionLabel" runat="server" Text='<%# (((int)Eval("DefaultSubStateRegionIdOfUser") > 0) && (bool)Eval("IsMultiSubStateUser")) ? "Default Sub State" : "Sub State" %>'
                                                ></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td class="c305">
                                            <asp:Label ID="SubStateSelected" runat="server" Text='<%# (((int)Eval("DefaultSubStateRegionIdOfUser") > 0) && (bool)Eval("IsMultiSubStateUser")) ? Eval("DefaultSubStateName").EncodeHtml().ToCamelCasing() : Eval("SubStateName").EncodeHtml().ToCamelCasing() %>'
                                                CssClass="InfoValueFont" Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>  
                                 <asp:PlaceHolder ID="plhAgencies" runat="server" Visible='<%# IsMultiAgencyAndDefaultAgencyExist %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="AgencyLabel" runat="server" Text='<%# (((int)Eval("DefaultAgencyIdOfUser") > 0) && (bool)Eval("IsMultiAgencyUser")) ? "Default Agency" : "Agency" %>'
                                                ></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td class="c305">
                                            <asp:Label ID="AgencySelected" runat="server" Text='<%# (((int)Eval("DefaultAgencyIdOfUser") > 0) && (bool)Eval("IsMultiAgencyUser")) ? Eval("DefaultAgencyName").EncodeHtml().ToCamelCasing() : Eval("AgencyName").EncodeHtml().ToCamelCasing() %>'
                                                CssClass="InfoValueFont" Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>            
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="FirstNameLabel" runat="server" Text="First Name" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="FirstName" runat="server" Text='<%# Eval("FirstName").EncodeHtml().ToCamelCasing() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="LastNameLabel" runat="server" Text="Last Name" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="LastName" runat="server" Text='<%# Eval("LastName").EncodeHtml().ToCamelCasing() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>    
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="PrimaryEmailLabel" runat="server" Text="Primary Email" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="Email" runat="server" Text='<%# Eval("PrimaryEmail").EncodeHtml() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="PrimaryPhoneLabel" runat="server" Text="Primary Phone" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="PrimaryPhone" runat="server" Text='<%# Eval("PrimaryPhone").EncodeHtml() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                 <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                           <asp:Label ID="lblTempPrimaryEmail" runat="server" Text="Temp Primary Email" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="TempPrimaryEmail" runat="server" Text='<%# Eval("TempPrimaryEmail").EncodeHtml() %>' CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                 <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                           <asp:Label ID="lblChangeEmailRequestDate" runat="server" Text="Change Email Request Date" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 305px">
                                        <asp:Label ID="lblCERequestDate" runat="server" Text='<%# Eval("EmailChangeRequestDate").EncodeHtml() %>' CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                  <tr>                                  
                                    <td colspan="4">
                                           <br /><asp:Label ID="Label1" runat="server" Text="Note: 'Temp Primary Email' is the new email that user requested for email change." ForeColor="Gray" ></asp:Label>
                                    </td>                                   
                                   
                                </tr>
                                                                                           
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server" >
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td style="width: 305px">
                                            <div style="float: left; margin-left: 0px; padding: 4px">
                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="formbutton3" Width="105px"
                                                    OnCommand="PostBackUserCommand" UseSubmitBehavior="true"   CommandArgument="Edit" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnReSendEmail" runat="server" Text="Resend Email" CssClass="formbutton3" Width="105px"
                                                    OnCommand="PostBackUserCommand" UseSubmitBehavior="true"   CommandArgument="ResendEmail" />
                                            </div>
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
                                    <td style="width: 305px">
                                        <div style="float: left; margin-left: 0px; padding: 4px">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td style="width: 305px">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </ItemTemplate>
            </asp:FormView>
        </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceUserView" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.UserViewData"
        OnSelecting="dataSourceUserView_Selecting" />
</asp:Content>
