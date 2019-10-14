<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucInfoLibParentNavigation.ascx.cs"
    Inherits="ShiptalkWeb.ucInfoLibParentNavigation" %>
    <%@ Import Namespace="ShiptalkWeb.Routing" %>
    <asp:HyperLink ID="hlTopLevelItems" runat="server" Visible="true" NavigateUrl='<%# RouteController.ViewInfoLibItemsTopLevel() %>'>View All Topics</asp:HyperLink>&nbsp;&nbsp;
    <asp:HyperLink ID="hlTopLevelParentLink" runat="server" Visible='<%# !string.IsNullOrEmpty(topLevelParentLink) %>' NavigateUrl='<%# topLevelParentLink %>'>Top level parent</asp:HyperLink>&nbsp;&nbsp;
    <asp:HyperLink ID="hlViewParentLink" runat="server" Visible='<%# !string.IsNullOrEmpty(parentLink) %>' NavigateUrl='<%# parentLink %>'>Parent item</asp:HyperLink>

