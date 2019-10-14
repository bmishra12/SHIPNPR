<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="UserSearch.aspx.cs" Inherits="ShiptalkWeb.UserSearch" Title="Search Users" %>

<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls"
    TagPrefix="pp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Search Users</title>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
            <div style="text-align: left">
                <h1>
                    Search Users</h1>
            </div>
            <span class="smaller">
                <asp:Literal ID="dv3colMessage" runat="server" Text="Use the search feature to find Users by their first name, last name or both. The results are filtered based on your role and search criteria."
                    EnableViewState="false"></asp:Literal>
            </span>
            <br />
            <br />
            <div>
                <asp:Panel ID="AdminLinksPanel" runat="server">
                    <div class="commands">
                        <a runat="server" id="A4" href='<%# RouteController.UserList() %>' visible='<%# ShowUserList %>'>
                            User List<br />
                        </a>
                      
                        <a runat="server" id="AddUserLink" href='<%# RouteController.UserAdd() %>' visible="false">
                            Add a New User</a>
                         <br />
                      <a runat="server" id="Inactivity180" href='<%# RouteController.Inactivity180() %>' visible="false">
                            180 Days Inactive Users</a>
                         <br />
                        <a runat="server" href='<%# RouteController.UserRegistrationsPending() %>' id="pendingRegistrations"
                            visible="false">Review, Approve, Deny Users</a>
                       <br />
                          <a runat="server" href='<%# RouteController.UserPendingUniqueIds() %>' id="pendingUniqueIds"
                            visible="false">Review, Approve, Deny, Revoke  CMS SHIP Unique ID Requests</a> 
                <br />            
                <asp:LinkButton ID="lbtnDownloadUniqueID" runat="server" Visible="false" EnableViewState="false" OnClick="DownloadApprovedUniquIdUserList_Click">Download Approved CMS SHIP Unique IDs</asp:LinkButton>
                <br />
                    
                    </div>
                </asp:Panel>
              <div class="commands">
                 <a runat="server"   id="a1" href="~/Npr/Docs/Chapter_1_NPR_Overview_508.pdf"  target='_blank' style = "text-align: right;" > <b>NPR User Manual: Overview of SHIP NPR </b></a>
                
               </div>
                <asp:Panel runat="server" ID="SearchPanel" DefaultButton="btnSearch">
                    <br />
                    <br />
                    <table class="formTable">
                        <tbody>
                            <div id="NoSearchResultsMessage" runat="server" enableviewstate="false" visible="false">
                                <tr>
                                    <td colspan="2">
                                        <div style="text-align: center; color: Red;">
                                            No results found for the following search criteria:
                                            <%=SearchDisplayTerm%>
                                            <br />
                                            <br />
                                        </div>
                                    </td>
                                </tr>
                            </div>
                            <tr>
                                <td class="tdFormLabel">
                                    <asp:Label runat="server" ID="label2" Text="Search Users:" ToolTip="Search by name, phone or email"
                                        AssociatedControlID="SearchText" />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="SearchText" Style="width: 400px;" CssClass="textfield3" />
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="SearchText"
                                        WatermarkText="Enter Full name or part of the name, email, phone to search" WatermarkCssClass="textfield3wm" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="margin: 0; padding: 0;">
                                    <div id="FilterByStates" runat="server">
                                        <table style="margin: 0; padding: 0;">
                                            <tr>
                                                <td class="tdFormLabel">
                                                    <asp:Label runat="server" ID="label3" Text="Find Users by State:" ToolTip="Find Users by State"
                                                        AssociatedControlID="ddlStates" />
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlStates" DataTextField="Value" DataValueField="Key"
                                                        AppendDataBoundItems="true" CssClass="dropdown1wm">
                                                        <asp:ListItem Text="-- All States --" Value="0" />
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnSearch" Text="Search" ToolTip="Search" OnClick="btnSearch_Click"
                                        CssClass="formbutton1a" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                <br />
                <div id="dv3colFormContent" class="section">
                    <asp:ListView runat="server" ID="listViewUsers" DataSourceID="dataSourceUsers" ItemPlaceholderID="placeHolder"
                        DataKeyNames="UserId">
                        <LayoutTemplate>
                            <table class="dataTable">
                                <thead>
                                    <tr>
                                        <th scope="col" style="width: 100px; text-align: left; padding-left: 4px">
                                            User Role
                                        </th>
                                        <th scope="col" style="text-align: left; padding-left: 4px" nowrap>
                                            First Name
                                        </th>
                                        <th scope="col" style="text-align: left; padding-left: 4px" nowrap>
                                            Last Name
                                        </th>
                                        <th scope="col" style="text-align: left; padding-left: 4px" nowrap>
                                            Primary Email
                                        </th>
                                        <th scope="col" style="text-align: left; padding-left: 4px" nowrap>
                                            Action
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
                                <td scope="row" style="vertical-align: top">
                                    <span class="smaller">
                                        <%# Eval("UserRoleText").EncodeHtml()%></span>
                                    <%--<asp:Label ID="Label1" runat="server" Visible='<%# !((bool)Eval("IsUserCMSScope")) && !((bool)Eval("IsUserCMSRegionalScope")) %>'>
                                            <span class="smaller"><span class="gray"><%# Eval("StateName") %></span></span>
                                    </asp:Label>--%>
                                    <asp:Label ID="RecordStateLabel" runat="server" Visible='<%# IsCMSLevelUser && (Eval("StateName") != "CMS") %>'>
                                            <br /><span class="smaller"><span class="gray"><%# Eval("StateName").EncodeHtml()%></span></span>
                                    </asp:Label>
                                    <asp:Label ID="RecordRegionLabel" runat="server" Visible='<%# (bool)Eval("IsUserCMSRegionalScope") || (bool)Eval("IsUserSubStateRegionalScope") || (bool)Eval("IsUserAgencyScope") %>'>
                                             <br /><span class="smaller"><span class="gray"><%# Eval("RegionName").EncodeHtml() %></span></span>
                                    </asp:Label>
                                </td>
                                <td style="vertical-align: top">
                                    <span class="smaller">
                                        <%# Eval("FirstName").EncodeHtml().ToCamelCasing()%>
                                    </span>
                                </td>
                                <td style="vertical-align: top">
                                    <span class="smaller">
                                        <%# Eval("LastName").EncodeHtml().ToCamelCasing()%></span>
                                </td>
                                <td style="vertical-align: top">
                                    <span class="smaller">
                                        <%# Eval("PrimaryEmail").EncodeHtml() %></span>
                                    <br />
                                    <asp:Image ID="Image1" runat="server" AlternateText="ActiveOrInactiveImage" ImageUrl='<%# this.ResolveUrl("~/images/" + (((bool)Eval("IsActive")) ? "activestatus.png" : "inactivestatus.png"))  %>' />
                                    <span class="smaller">
                                        <%# ((bool)Eval("IsActive") ? "Active" : "Inactive") %></span>
                                </td>
                                <td style="vertical-align: top">
                                    <span style="font-size: 14px"><a id="A2" runat="server" href='<%# RouteController.UserView((int)Eval("UserId")) %>'
                                        title='<%# "Select " + Eval("FirstName").EncodeHtml().ToCamelCasing() + " " + Eval("LastName").EncodeHtml().ToCamelCasing() %>'>
                                        Select</a> </span>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                    <div style="text-align: center;">
                        <asp:DataPager ID="pager" runat="server" PagedControlID="listViewUsers" Position="TopAndBottom">
                            <Fields>
                                <asp:NumericPagerField PreviousPageText="&lt; Prev" NextPageText="Next &gt;" ButtonCount="10"
                                    NextPreviousButtonCssClass="PrevNext" CurrentPageLabelCssClass="CurrentPage"
                                    NumericButtonCssClass="PageNumber" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </div>
            </div>
        </div>
        <pp:ObjectContainerDataSource ID="dataSourceUsers" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.UserSearchViewData"
            OnSelecting="dataSourceUsers_Selecting" />
    </div>
</asp:Content>
