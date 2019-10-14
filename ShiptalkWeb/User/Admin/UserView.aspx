<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="UserView.aspx.cs" Inherits="ShiptalkWeb.UserView" Title="User Information - View" %>

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
        td .c305
        {
            width: 305px;
            padding-left: 10px;
        }
        .Descriptors
        {
            padding-left: 4px;
            margin-left: 2px;
            list-style-type: circle;
            font-weight: bold;
            font-family: Arial;
            font-size: 12px;
        }
    </style>
    


    <script type="text/javascript">
        $(document).ready(function() {
            
            //Initialize the dialog to be used for delete confirmations.
            $("#confirmDelete").dialog({
                resizable: false,
                height: 240,
                autoOpen: false,
                modal: true,
                buttons: {
                    "No": function() {
                        $(this).dialog('close');
                    },
                    "Yes": function() {
                        $(this).dialog('close');
                        __doPostBack($("[id$='_btnDelete']").attr("id").replace(/_/g, "$"), "");
                    }
                }
            });

            $("[id$='_btnDelete']").click(function(e) {
                e.preventDefault();
                $('#confirmDelete').dialog('open');
            });

        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col" runat="server" id="MainPanel">
            <div style="text-align: left">
                <h1>
                    <asp:Label ID="lblTitle" runat="server" Text="User Information" EnableViewState="false"></asp:Label>
                </h1>
            </div>
            <span class="smaller">
                <asp:Literal ID="dv3colMessage" runat="server" Text="The User information contains personal and account profiles. If a User has access to multiple agencies or regions, each of the individual agency or region will be listed below."
                    EnableViewState="false"></asp:Literal>
            </span>
            <br />
            <br />
            <asp:HyperLink ID="BackToUserSearch" runat="server" NavigateUrl='<%# RouteController.UserSearch() %>' Visible="false">Back to User Search</asp:HyperLink>
            <br />
            <br />
            <asp:Panel ID="UserDataPanel" runat="server">
            <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceUserView" Width="100%">
                <ItemTemplate>
                    <asp:Panel ID="RegistrationDataPanel" runat="server" DefaultButton="btnEditRequest">
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
                                <%--<asp:PlaceHolder ID="plhRoleInformation" runat="server" Visible='<%# ((bool)Eval("IsCMSLevel")) || ((bool)Eval("IsUserStateScope")) && !((bool)Eval("IsShipDirector")) %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="RoleLabel" runat="server" Text="Role" AssociatedControlID="RoleSelected"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td class="c305">
                                            <asp:Label ID="RoleSelected" runat="server" Text='<%# Eval("RoleTitle") %>' CssClass="InfoValueFont"
                                                Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="Label1" runat="server" Text="Role description" AssociatedControlID="lblRoleDescription"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td class="c305">
                                            <div style="width: 280px;">
                                                <span style="font-weight: normal; font-family: Arial; font-size: 12px;">
                                                    <asp:Label ID="lblRoleDescription" runat="server" Text='<%# Eval("RoleDescription") %>'></asp:Label></span>
                                            </div>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>--%>
                                <asp:PlaceHolder ID="plhRoleInformation" runat="server" Visible='<%# ShowRoleInformation %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="RoleLabel" runat="server" Text="Role"></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td class="c305">
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
                                        <td class="c305">
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
                                    <td class="c305">
                                        <asp:Label ID="FirstName" runat="server" Text='<%# Eval("FirstName").EncodeHtml().ToCamelCasing() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                        &nbsp;
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="MiddleNameLabel" runat="server" Text="Middle Name" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td class="c305">
                                        <asp:Label ID="MiddleName" runat="server" Text='<%# Eval("MiddleName").EncodeHtml().ToCamelCasing() %>'
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
                                    <td class="c305">
                                        <asp:Label ID="LastName" runat="server" Text='<%# Eval("LastName").EncodeHtml().ToCamelCasing() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="Label2" runat="server" Text="Login Account Status" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td class="c305">
                                        <asp:Image ID="Image1" runat="server" AlternateText="ActiveOrInactiveImage" ImageUrl='<%# this.ResolveUrl("~/images/" + (((bool)Eval("IsActive")) ? "activestatus.png" : "inactivestatus.png"))  %>' />&nbsp;<asp:Label
                                            ID="AccountStatus" runat="server" Text='<%# (((bool)Eval("IsActive")) ? "Active" : "Inactive") %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="NickNameLabel" runat="server" Text="Nick Name" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td class="c305">
                                        <asp:Label ID="NickName" runat="server" Text='<%# Eval("NickName").EncodeHtml().ToCamelCasing() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="SuffixLabel" runat="server" Text="Suffix" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td class="c305">
                                        <asp:Label ID="Suffix" runat="server" Text='<%# Eval("Suffix").EncodeHtml().ToCamelCasing() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="HonorificsLabel" runat="server" Text="Honorifics" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td class="c305">
                                        <asp:Label ID="Honorifics" runat="server" Text='<%# Eval("Honorifics").EncodeHtml().ToCamelCasing() %>'
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
                                    <td class="c305">
                                        <asp:Label ID="Email" runat="server" Text='<%# Eval("PrimaryEmail").EncodeHtml() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="SecondaryEmailLabel" runat="server" Text="Secondary Email" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td class="c305">
                                        <asp:Label ID="SecondaryEmail" runat="server" Text='<%# Eval("SecondaryEmail").EncodeHtml() %>'
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
                                    <td class="c305">
                                        <asp:Label ID="PrimaryPhone" runat="server" Text='<%# Eval("PrimaryPhone").EncodeHtml() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="SecondaryPhoneLabel" runat="server" Text="Secondary Phone" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td class="c305">
                                        <asp:Label ID="SecondaryPhone" runat="server" Text='<%# Eval("SecondaryPhone").EncodeHtml() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="Label4" runat="server" Text="Zipcode of Counseling Location" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td class="c305">
                                        <asp:Label ID="lblCounselingZip" runat="server" Text='<%# Eval("CounselingLocation").EncodeHtml() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                        <asp:Label ID="Label6" runat="server" Text="County of Counseling Location" ></asp:Label>
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;
                                    </td>
                                    <td class="c305">
                                        <asp:Label ID="lblCounselingCounty" runat="server" Text='<%# Eval("CounselingCounty").EncodeHtml() %>'
                                            CssClass="InfoValueFont" Width="256px"></asp:Label>
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="plhApproverDesignate" runat="server" Visible='<%# (bool)Eval("IsUserStateScope") || (bool)Eval("IsUserCMSScope") %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="Label5" runat="server" Text="Can Approve User Registrations" ></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td class="c305">
                                            <asp:Label ID="CanApproveUser" runat="server" Text='<%# CanApproveUsersString %>'
                                                CssClass="InfoValueFont" Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="Label8" runat="server" Text='<%# "Is " + ((Scope)Eval("Scope")).Description() + " Administrator" %>' ></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td class="c305">
                                            <asp:Label ID="IsAdministrator" runat="server" Text='<%# (Convert.ToBoolean(Eval("IsAdmin").EncodeHtml())).ToString() %>'
                                                CssClass="InfoValueFont" Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="PlaceHolder4" runat="server" Visible='<%# (bool)Eval("IsUserStateScope") && !(bool)Eval("IsShipDirector") %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="Label9" runat="server" Text="Is Super Data Editor" ></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td class="c305">
                                            <asp:Label ID="lblIsSuperDataEditor" runat="server" Text='<%# (bool)Eval("IsStateSuperDataEditor") ? "Yes" : "No" %>'
                                                CssClass="InfoValueFont" Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="plhSupervisor" runat="server" Visible='<%# (bool)Eval("IsUserStateScope") && !(bool)Eval("IsShipDirector") %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                            <asp:Label ID="Label7" runat="server" Text="Supervisor name" ></asp:Label>
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;
                                        </td>
                                        <td class="c305">
                                            <asp:Label ID="SupervisorName" runat="server" Text='<%# Eval("SupervisorNameForStateUser").EncodeHtml() %>'
                                                CssClass="InfoValueFont" Width="286px"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="plhDescriptors" runat="server" Visible='<%# (((bool)Eval("IsUserStateScope")) || ((bool)Eval("IsCMSLevel"))) && ((bool)Eval("HasDescriptors")) %>'>
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
                                            <div style="width: 256px; background: #F2F2F2; color: #000; padding: 3px 0 8px 10px;
                                                border-top: solid 1px #E6E6E6;">
                                                <asp:BulletedList ID="Descriptors" runat="server" DataSource='<%# (IEnumerable<string>)Eval("GetAllDescriptorNamesForUser") %>'
                                                    CssClass="Descriptors" BulletStyle="Disc">
                                                </asp:BulletedList>
                                            </div>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <!-- Edit link will be hidden if the User cannot edit the record (Has only read access)-->
                                <asp:Panel ID="Panel1" runat="server" Visible='<%# IsEditAccessAllowed() %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td class="c305">
                                            <table style="width: 256px;">
                                                <tr>
                                                    <td style="width: 125px;" align="left">
                                                        <asp:Button ID="btnEditRequest" runat="server" Text="Edit"
                                                         CssClass="formbutton3"
                                                            Width="110px" OnCommand="EditUserCommand"
                                                             UseSubmitBehavior="true" CommandName="Edit"
                                                            CommandArgument="Edit" />
                                                    </td>
                                                    <td style="width: 125px">
                                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="formbutton3"
                                                            Width="110px" OnCommand="EditUserCommand" UseSubmitBehavior="true" CommandName="Edit"
                                                            CommandArgument="Delete" Visible='<%# IsDeleteAccessAllowed() %>' />
                                                    </td>

                                                    <td style="width: 125px">
                                                        <asp:Button ID="btnUnlock" runat="server" Text="Unlock" CssClass="formbutton3"
                                                            Width="110px" OnCommand="EditUserCommand" UseSubmitBehavior="true" CommandName="Edit"
                                                            CommandArgument="Unlock" Visible='<%# IsUnlockAccessAllowed() %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td style="width: 200px; text-align: right;">
                                    </td>
                                    <td style="width: 4px">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td class="c305">
                                    </td>
                                </tr>
                                <asp:Panel ID="Panel3" runat="server" Visible='<%# (bool)Eval("IsUserAgencyScope") || (bool)Eval("IsUserSubStateRegionalScope") %>'>
                                    <tr>
                                        <td style="width: 8px;">
                                        </td>
                                        <td style="width: 200px; text-align: right;">
                                        </td>
                                        <td style="width: 4px">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td class="c305" style="text-align: right">
                                            <a runat="server" href="<%# GetAddRoute() %>" visible='<%# IsAddAccessAllowed  %>'>Add
                                                User to another
                                                <%# GetScopeDisplayText %></a>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                        </div>
                    </asp:Panel>
                </ItemTemplate>
            </asp:FormView>
            <div id="RegionsContent" class="section">
                <asp:ListView runat="server" ID="listViewUserRegionalProfiles" DataSourceID="dataSourceRegionalProfiles"
                    ItemPlaceholderID="placeHolder" DataKeyNames="UserId" OnItemDataBound="listViewUserRegionalProfiles_ItemDataBound">
                    <LayoutTemplate>
                        <table class="dataTable">
                            <thead>
                                <tr>
                                    <th scope="col" style="width: 200px; text-align: left; padding-left: 4px">
                                        Agency/Region Name
                                    </th>
                                    <th scope="col" style="text-align: left; padding-left: 4px; width: 180px">
                                        Role
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
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td scope="row" style="vertical-align: top;">
                                <asp:PlaceHolder ID="Panel2" runat="server" Visible='<%# CanViewRegion((int)Eval("RegionId")) %>'>
                                    <div style="width: 200px; word-wrap: break-word">
                                        <span class="smaller"><a id="A2" runat="server" href='<%# GetViewRoute((int)Eval("RegionId")) %>'
                                            title="Region Name">
                                            <%# Eval("RegionName").EncodeHtml().ToCamelCasing() %></a> </span>
                                    </div>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible='<%# !CanViewRegion((int)Eval("RegionId")) %>'>
                                    <div style="width: 200px; word-wrap: break-word">
                                        <span class="smaller">
                                            <%# Eval("RegionName").EncodeHtml().ToCamelCasing() %></span>
                                    </div>
                                </asp:PlaceHolder>
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
                            <td valign="top" style="text-align: center">
                                <asp:Panel ID="RegionViewPanel" runat="server" Visible='<%# CanViewRegion((int)Eval("RegionId")) %>'>
                                    <div style="margin-bottom: 4px">
                                        <span class="smaller"><a id="A3" runat="server" href='<%# GetViewRoute((int)Eval("RegionId")) %>'
                                            title="View">View</a></span>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="RegionEditPanel" runat="server" Visible='<%# CanEditRegion((int)Eval("RegionId")) %>'>
                                    <span class="smaller"><a id="A1" runat="server" href='<%# GetEditRoute((int)Eval("RegionId")) %>'
                                        title="Edit">Edit</a></span>
                                </asp:Panel>
                                 <asp:PlaceHolder ID="PlaceHolder3" runat="server" Visible='<%# !CanViewRegion((int)Eval("RegionId")) %>'>
                                    <div style="">
                                        <span class="smaller">No access</span>
                                    </div>
                                </asp:PlaceHolder>
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
            </asp:Panel>
        </div>
    </div>
    <div id="confirmDelete" title="Confirm Delete" style="display: none;">
        <p>
            <strong>Are you sure? The delete action will delete the User account including all of the User's associated data</strong>
            <br />NOTE: The account will be purged and cannot be recovered in future. To inactivate user on a temporary basis, please use the Active/Inactive option in User Edit feature.
        </p>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceUserView" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.UserViewData"
        OnSelecting="dataSourceUserView_Selecting" />
    <pp:ObjectContainerDataSource ID="dataSourceRegionalProfiles" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UserRegionalAccessProfile"
        OnSelecting="dataSourceRegionalProfiles_Selecting" />
</asp:Content>
