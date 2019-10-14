<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="UnAuthorizedAccess.aspx.cs" Inherits="ShiptalkWeb.UnAuthorizedAccess"
    Title="You are not authorized" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
            <h1>
                UnAuthorized Access</h1>
            <p>
                <asp:Label ID="lblMessage" runat="server" Text="You are not authorized to access the SHIP NPR resource. Should you require further assistance, please contact technical support."></asp:Label>
            </p>
        </div>
        <div class="clear">
        </div>
    </div>
</asp:Content>
