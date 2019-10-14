<%@ Page Language="C#" MasterPageFile="~/ShiptalkWebWide.Master" AutoEventWireup="true"
    CodeBehind="UserList.aspx.cs" Inherits="ShiptalkWeb.UserList" Title="List of available Users" %>

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
    <div id="maincontentwide">
        <div class="dv5col">
            <div style="text-align: left">
                <h1>
                    View / Download User List</h1>
            </div>
            <span class="smaller">
                <asp:Literal ID="dv3colMessage" runat="server" Text="View user information that include UserID of the User. View or download the list of users at state, agency and sub state regional levels."
                    EnableViewState="false"></asp:Literal>
            </span>
            <br />
            <br />
            <div class="commands">
                <asp:LinkButton ID="lbtnDownload" runat="server" Visible="false" EnableViewState="false" OnClick="DownloadUserList_Click">Download User List</asp:LinkButton>
            </div>
            <div>
                <asp:Panel ID="AdminLinksPanel" runat="server">
                    <div class="commands">
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="FilterByStatePanel" DefaultButton="btnFilterByState">
                    <table style="width: 615px;">
                        <tbody>
                            <tr>
                                <td style="width: 375px;">
                                    <div id="FilterByStates" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 100%">
                                                    <div>
                                                        <strong>
                                                            <asp:Label runat="server" ID="label3" Text="Filter Users by State:" ToolTip="Find Users by State"
                                                                AssociatedControlID="ddlStates" /></strong>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%">
                                                    <div>
                                                        <asp:DropDownList runat="server" ID="ddlStates" DataTextField="Value" DataValueField="Key"
                                                            AppendDataBoundItems="true" Width="170px">
                                                            <asp:ListItem Text="-- Select a state --" Value="0" />
                                                        </asp:DropDownList>
                                                        <span class="smaller"><span class="gray">&nbsp;</span></span>
                                                        <asp:Button runat="server" ID="btnFilterByState" Text="Search" ToolTip="Search" OnClick="btnFilterByState_Click"
                                                            CssClass="formbutton1" Width="97px" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td style="width: 240px;">
                                    <div style="float: left; margin-left: 10px">
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
                <br />
                <div id="dv3colFormContent">
                    <asp:ListView runat="server" ID="listViewUsers" DataSourceID="dataSourceUsers" ItemPlaceholderID="placeHolder"
                        DataKeyNames="UserId" OnPreRender="listViewUsers_PreRender">
                        <LayoutTemplate>
                            <table class="dataTable">
                                <thead>
                                    <tr>
                                        <th scope="col" style="text-align: left; padding-left: 4px" nowrap>
                                            Role
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
                                        <th scope="col" style="text-align: right; padding-right: 4px" nowrap>
                                            CMS SHIP Unique ID
                                        </th>
                                        <th scope="col" style="text-align: right; padding-right: 4px" nowrap>
                                            NPR User ID
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
                                        <%# Eval("UserRoleText").EncodeHtml()%>
                                    </span>
                                    <asp:Label ID="RecordStateLabel" runat="server" Visible='<%# IsCMSLevelUser && (Eval("StateName") != "CMS") %>'>
                                            <br /><span class="smaller"><span class="gray"><%# Eval("StateName").EncodeHtml()%></span></span>
                                    </asp:Label>
                                    <asp:Label ID="RecordRegionLabel" runat="server">
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
                                        <%# Eval("PrimaryEmail").EncodeHtml() %>
                                    </span>
                                </td>
                                <td style="vertical-align: top">
                                    <span class="smaller">
                                        <%# Eval("MedicareUniqueId").EncodeHtml()%>
                                    </span>
                                </td>
                                <td style="vertical-align: top">
                                    <span style="font-size: 14px"><a id="A2" runat="server" href='<%# RouteController.UserView((int)Eval("UserId")) %>'
                                        title='UserID of <%# Select Eval("FirstName").EncodeHtml().ToCamelCasing() %>'>
                                        <%# (Convert.ToInt32(Eval("UserId").EncodeHtml())).ToString()%></a> </span>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                    <div style="text-align: center;">
                        <asp:DataPager ID="pager" runat="server" PagedControlID="listViewUsers">
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
