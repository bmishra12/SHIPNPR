<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="PendingUniqueIdRequests.aspx.cs" Inherits="ShiptalkWeb.PendingUniqueIdRequests"
    Title="Pending Unique Id Requests" %>

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
            <asp:Label ID="lblHeaderTitle" runat="server" Text="Pending Unique ID Requests"
                        EnableViewState="false"></asp:Label>
                </h1>
            <div>
                <div id="dv3colFormContent" class="section">
                    <asp:PlaceHolder ID="plhMessage" runat="server" Visible="false" EnableViewState="false">
                        <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false" CssClass="required"></asp:Label>
                    </asp:PlaceHolder>
                    <div id="NoSearchResultsMessage" runat="server" enableviewstate="false" visible="false">
                        <div style="margin-top: 20px; margin-bottom: 230px;">
                            <span class="required">At this time, there are no pending Unique ID generation requests. </span>
                        </div>
                    </div>     
                    
                    
                    

            <div class="commands">
                <asp:LinkButton ID="lbtnDownload" runat="server" Visible="false" EnableViewState="false" OnClick="DownloadUserList_Click">Download Pending User List</asp:LinkButton>
                <br />
                <asp:LinkButton ID="lbtnShowAll" runat="server" Visible="true" EnableViewState="false"  OnClick="ShowAllUserList_Click" Text="Show all CMS Unique ID List"></asp:LinkButton>
                <br />
                <asp:LinkButton ID="lbtnRevoke" runat="server" Visible="true" EnableViewState="false"  OnClick="ShowRevokedUserList_Click" Text="Show Revoked CMS Unique ID List"></asp:LinkButton>

            </div>
               <br />                           
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
                                            Generate
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
                                    <a id="A3" runat="server" href='<%# RouteController.UserPendingUniqueIdSelect((int)Eval("UserId")) %>'
                                        title="Select">Select</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                  <%--  <div style="text-align: center;">
                        <asp:DataPager ID="pager" runat="server" PageSize="5" PagedControlID="listViewPendingUsers">
                            <Fields>
                                <asp:NumericPagerField />
                            </Fields>
                        </asp:DataPager>
                    </div>--%>
                </div>
            </div>
        </div>
        <pp:ObjectContainerDataSource ID="dataSourcePendingUsers" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.UserSummaryViewData"
            OnSelecting="dataSourcePendingUsers_Selecting" />
    </div>
</asp:Content>
