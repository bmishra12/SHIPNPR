<%@ Page Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true"
    CodeBehind="EmailConf.aspx.cs" Inherits="ShiptalkWeb.EmailConf" Title="SHIPtalk Registration : Email confirmation page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Email verification
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
            <h1>
                <asp:Literal ID="colTitle" runat="server" Text="Success!" EnableViewState="false"
                    Visible="false"></asp:Literal>
            </h1>
            <asp:Literal ID="dv3colSuccessMessage" runat="server" Visible="false" EnableViewState="false"
                Text="You have successfully verified your email address. Your registration request is now awaiting approval. You will receive an email within a few days notifying you about the status of your registration."></asp:Literal>
            <br />
        </div>
    </div>
</asp:Content>
