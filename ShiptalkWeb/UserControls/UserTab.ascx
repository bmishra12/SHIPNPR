<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserTab.ascx.cs" Inherits="ShiptalkWeb.UserTab" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
    <div id="AnonymousTabs" runat="server">
        <%--** DO NOT NEED REGISTER TAB WHEN REGISTER PAGE IS THE ONLY PAGE AVAILABLE **--%>
        <ul id="navbar" class="navbar">
            <li id="Li13" class="tabLinkActive">
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Default.aspx">Home</asp:HyperLink>
            </li>
            <li id="tabLink1" class="tabLinkActive">
                <asp:HyperLink ID="href3" runat="server" NavigateUrl="~/UserRegistration.aspx">REGISTER</asp:HyperLink>
            </li>
           <%-- <li id="Li7" class="tabLinkActive">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/DEFAULT.aspx">Find a SHIP</asp:HyperLink>
            </li>
            <li id="Li8" class="tabLinkActive">
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/DEFAULT.aspx">Find a Counselor</asp:HyperLink>
            </li>--%>
       <%-- <li id="Li12" class="tabLinkActive">
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/InfoLib/InfoLibItemsTopLevel.aspx">InfoLib</asp:HyperLink>
            </li>--%>
        </ul>
     </div>   
    <asp:Panel id="LoggedInTabs" runat="server" visible="false">
        <ul class="navbar">
            <%--                <li id="userTabLink1" class="tabLinkActive"><a>Welcome
                    <asp:LoginName ID="LoginName1" runat="Server"></asp:LoginName>
                    </a></li>--%>
            <li id="userTabLink4" class="tabLink" visible='true' runat="server"><a runat="server"
                href='<%# RouteController.AgencySearch() %>' id="A1">Agency</a></li>
            <li id="Li1" class="tabLink" visible='true' runat="server"><a runat="server" href='<%# RouteController.UserSearch() %>'
                id="A2">User</a></li>
            <li id="Li3" class="tabLink"><a runat="server" href='<%# RouteController.EditMyProfile() %>'
                id="A4">EditMyProfile</a></li>
        <li id="Li5" class="tabLink" visible='true' runat="server"><a runat="server" href='<%# RouteController.ResourceReportSearch() %>'
                id="A3">RR</a></li>
            <li id="Li4" class="tabLink" visible='true' runat="server"><a runat="server" href='<%# RouteController.CcfSearch() %>'
                id="A7">CC</a></li>
            <li id="Li10" class="tabLink"><a runat="server" href='<%# RouteController.PamSearch() %>'
                id="A9">PAM</a></li>
            <li id="Li2" class="tabLink" visible='true' runat="server"><a runat="server" href='<%# RouteController.UploadFile() %>'
                id="A6">Upload</a></li>
            <li id="Li6" class="tabLink" visible='true' runat="server"><a runat="server" href='<%# RouteController.ShipProfileView() %>'
                id="A5">SHIPProfile</a></li>            
            <li id="Li9" class="tabLink" visible='true' runat="server"><a runat="server" href='<%# RouteController.NprReports() %>'
                id="A8">NPRReports</a></li>
            <li id="Li7" class="tabLink" visible='true' runat="server"><a runat="server" href='<%# RouteController.UserManual() %>'
                id="A10">UserManual</a></li>
           <%-- <li id="Li11" class="tabLink" visible='true' runat="server"><a runat="server" href='<%# RouteController.ViewInfoLibItemsTopLevel() %>'
                id="A10">InfoLib</a></li>--%>
            <li id="usertabLink4" class="tabLink">
                <asp:LoginStatus ID="LoginStatus1" LogoutAction="Redirect" LogoutPageUrl="~/Default.aspx"
                    OnLoggedOut="LoginStatus1_LoggedOut" runat="server" />
            </li>
        </ul>
     </asp:Panel>   
