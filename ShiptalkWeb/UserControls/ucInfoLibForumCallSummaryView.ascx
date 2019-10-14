<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucInfoLibForumCallSummaryView.ascx.cs" Inherits="ShiptalkWeb.ucInfoLibForumCallSummaryView" %>
<%@ Import Namespace="ShiptalkCommon" %>

<div id="divforumCallSummaryView" runat="server" visible="false" class="leftNavBox1">
    <div class="leftNavHeader">
        SHIP Forum Call!
    </div>
    <div class="leftNavContent">
        <p>
            <asp:Literal ID="litSummaryViewContent" runat="server"></asp:Literal>
            <asp:HyperLink ID="hlMoreLink" runat="server" Visible="false">[more..]</asp:HyperLink>
        </p>
    </div>
</div>
