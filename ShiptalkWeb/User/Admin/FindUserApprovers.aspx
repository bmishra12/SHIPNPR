<%@ Page Language="C#" MasterPageFile="~/ShiptalkWebWide.Master" AutoEventWireup="true" CodeBehind="FindUserApprovers.aspx.cs" Inherits="ShiptalkWeb.FindUserApprovers" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Existing Approvers and other Admin Users</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">

    AIR Admin sees all CMS Level Users
    
    CMS Designated User sees all CMS Level Users

    --Displays a table of all User Admin Users 
    --  A column indicates that the User is already an approver. Also, the column provides a checkbox to remove the status.
    --  A column provides a button to add 'Approver' status to the User.

</asp:Content>
