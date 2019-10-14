

<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="CustomError.aspx.cs" Inherits="ShiptalkWeb.CustomError" Title="SHIPNPR Error:" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>SHIPNPR Error
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
            <h1 style="color:Red">
                <asp:Literal  ID="colTitle" runat="server" Text="Error!"  EnableViewState="false"
                    Visible="True"></asp:Literal>
            </h1>
            <asp:Literal ID="dv3colSuccessMessage" runat="server" Visible="True" EnableViewState="false"
                Text="An unexpected error has occurred with the Ship Talk system. <br><br>Please close Internet Browser, open a new Internet Browser window and try your action again. <br><br> If you receive this error message again, please contact the SHIP NPR Help Desk at <br><br>1-800-253-7154, option 1 or SHIPNPRHelp@air.org for further assistance. "></asp:Literal>
            <br />
        </div>
    </div>
</asp:Content>

