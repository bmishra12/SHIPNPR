<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucSearch.ascx.cs" Inherits="ShiptalkWeb.ucSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Panel runat="server" ID="SearchPanel" DefaultButton="btnSearch">
<span style="float: right; display:none"><a href="" style="color: #fff">
Advanced Search
</a></span>
<br />
<span class="offscreen">
    <asp:Label ID="SearchLabel" runat="server" Text="Search" AssociatedControlID="txtSearch"></asp:Label>
</span>
<asp:TextBox ID="txtSearch" runat="server" CssClass="textfield2" ValidationGroup="SearchValidationGroup"></asp:TextBox>
<asp:Button ID="btnSearch" runat="server" Text="GO >>" CssClass="formbutton1"  ValidationGroup="SearchValidationGroup"
    OnClick="btnSearch_Click" />

<div id="loggedInSearch" runat="server" visible="false">
    <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" CssClass="checkbox2" ValidationGroup="SearchValidationGroup"/> 
    Search Current Documents
    <br />
    <asp:CheckBox ID="CheckBox2" runat="server" Checked="true" CssClass="checkbox2" ValidationGroup="SearchValidationGroup"/>
    Search Archives
</div>

<br />
<cc1:TextBoxWatermarkExtender ID="tbwe1" runat="server" 
    TargetControlID="txtSearch"
    WatermarkText="Search"
    WatermarkCssClass="textfield2wm" /> 
</asp:Panel>