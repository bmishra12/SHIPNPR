<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="True"
    CodeBehind="PendingUserRegistrations.aspx.cs" Inherits="ShiptalkWeb.PendingUserRegistrations"
    Title="Pending User Registrations" %>

<%@ Import Namespace="ShiptalkWeb" %>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
            <h1>
                Pending User Registrations</h1>
            <span class="smaller">
                <asp:Literal ID="dv3colMessage" runat="server" Text="Displays the list of Users who have been registered but not yet approved. This feature allows toggle between unapproved users and users who have not yet verified their email address."
                    EnableViewState="false"></asp:Literal>
            </span>
            <div style="width: 100%; padding-right: 20px; text-align: right; padding-top: 10px;">
                <asp:Button ID="btnShowData" runat="server" OnCommand="btnShowData_Cmd" Text="Show pending email verifications"
                    CssClass="formbutton1" CommandName="SHOW_PENDING_EMAILS" />
            </div>
            <div>
                <div id="dv3colFormContent" class="section">
                    <asp:PlaceHolder ID="plhMessage" runat="server" Visible="false" EnableViewState="false">
                        <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false" CssClass="required"></asp:Label>
                    </asp:PlaceHolder>
                    <div style="margin-top: 20px; padding-bottom: 10px;">
                        <asp:Label ID="Label2" runat="server" EnableViewState="true" Visible='<%# ViewDataHasRows %>'
                            Text='<%# string.Concat("The following is the list of ", IncludeAllPendingEmailVerifications ? 
                                    " pending email verifications." : " pending registrations that are awaiting approval.") %>'></asp:Label>
                    </div>
                    <div style="text-align: center; margin-top: 20px; padding-bottom: 240px;" visible='<%# !ViewDataHasRows %>' runat="server" id="divNoResultsMesage">
                        <asp:Label ID="NoSearchResultsMessage" runat="server" EnableViewState="false" 
                            CssClass="required" Text='<%# IncludeAllPendingEmailVerifications ? 
                                    "At this time, there are no pending email verifications." : "At this time, there are no pending registrations that are awaiting approval." %>'></asp:Label>
                    </div>
                    <asp:ListView runat="server" ID="listViewPendingUsers" DataSourceID="dataSourcePendingUsers"
                        ItemPlaceholderID="placeHolder" DataKeyNames="UserId">
                        <LayoutTemplate>
                            <table class="dataTable">
                                <thead>
                                    <tr>
                                        <th scope="col" style="width: 200px; text-align: left; padding-left: 4px">
                                            Name
                                        </th>
                                        <th scope="col" style="text-align: left; padding-left: 4px; width: 180px">
                                            Primary Email
                                        </th>
                                        <th scope="col" style="text-align: left; padding-left: 4px; width: 80px">
                                            Requested Access
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
                                    <div style="width: 200px; word-wrap: break-word">
                                        <span class="smaller">
                                            <%# Eval("FirstName").EncodeHtml().ToCamelCasing() + " " + 
                                        Eval("MiddleName").EncodeHtml().ToCamelCasing() +
                                        ((Eval("MiddleName") == string.Empty) ? "" : " ") + 
                                        Eval("LastName").EncodeHtml().ToCamelCasing() %>
                                        </span>
                                    </div>
                                </td>
                                <td style="vertical-align: top">
                                    <div style="width: 180px; word-wrap: break-word">
                                        <span class="smaller">
                                            <%# Eval("PrimaryEmail").EncodeHtml() %>
                                        </span>
                                    </div>
                                </td>
                                <td style="vertical-align: top">
                                    <div style="margin-left: 4px;">
                                        <span class="smaller">
                                            <%# ((Scope)Eval("Scope")).Description() 
                                        + ((bool)Eval("IsAdmin") == true ? " Admin" : " User") %>
                                        </span>
                                    </div>
                                    <div style="margin-left: 4px;">
                                        <asp:Label ID="Label1" runat="server" Visible='<%# this.AccountInfo.Scope.CompareTo(Scope.State, ComparisonCriteria.IsHigher) && ((State)Eval("State")).StateName != "CMS" %>'>
                                            <span class="smaller"><span class="gray"><%# ((State)Eval("State")).StateName %></span></span>
                                        </asp:Label>
                                    </div>
                                </td>
                                <td valign="top">
                                    <a id="A3" runat="server" href='<%# RouteController.UserRegistrationsPendingSelect((int)Eval("UserId")) %>'
                                        title="Select">Select</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                    <div style="text-align: center;">
                        <asp:DataPager ID="pager" runat="server" PageSize="15" PagedControlID="listViewPendingUsers">
                            <Fields>
                                <asp:NumericPagerField />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </div>
            </div>
        </div>
        <pp:ObjectContainerDataSource ID="dataSourcePendingUsers" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.UserSummaryViewData"
            OnSelecting="dataSourcePendingUsers_Selecting" />
    </div>
</asp:Content>
