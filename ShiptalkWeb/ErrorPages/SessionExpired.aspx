<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="SessionExpired.aspx.cs" Inherits="ShiptalkWeb.SessionExpired"
    Title="Session Expired" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
            <h1>
                Session unavailable</h1>
            <p>
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </p>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
