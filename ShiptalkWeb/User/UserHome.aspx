<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true" CodeBehind="UserHome.aspx.cs" Inherits="ShiptalkWeb.UserHome" Title="Login home" %>
<%@ Import Namespace="ShiptalkWeb.Routing"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
            <h1>Users</h1>
                <a runat="server" href='<%# RouteController.UserAdd() %>' id="addUserLink">Add User</a>
                <br />
                <a runat="server" href='<%# RouteController.UserSearch() %>' id="findUserlink">Find Users</a>
                <br />
                <a runat="server" href='<%# RouteController.UserRegistrationsPending() %>' id="pendingRegistrations">Pending User Registrations</a>
        </div>
        <div class="clear"></div>
        <div style="display: none" >
        
        
        <div class="dv4colleft dvFindAShip" >
        <label for="ddlStates">
            <h2>Find a State SHIP</h2> </label>
            <p>Looking for a State SHIP?  Select your state below to find your local SHIP branch.</p>
            <asp:DropDownList ID="ddlStates" runat="server" CssClass="dropdown1wm" onchange="ddlChange(this)">
                <asp:ListItem Text="Select a State" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="Button2" runat="server" Text="GO >>" CssClass="formbutton3" />
        </div>
        </div>
</div>       
</asp:Content>
