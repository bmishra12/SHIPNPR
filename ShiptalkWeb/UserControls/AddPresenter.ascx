<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddPresenter.ascx.cs" Inherits="ShiptalkWeb.UserControls.AddPresenter" %>
<table width="100%">
    <tr>
        <td valign="top" width="110px">
           &nbsp;
        </td>
        <td valign="top">
            <asp:TextBox ID="txtShipUser" runat="server"></asp:TextBox>
        </td>
        <td valign="top">
            <asp:TextBox ID="txtFName" runat="server"></asp:TextBox>
        </td>
        <td valign="top">
            <asp:TextBox ID="txtLName" runat="server"></asp:TextBox>
        </td>
        <td valign="top">
            <asp:TextBox ID="txtAffiliation" runat="server" Width="100px"></asp:TextBox>
        </td>
        <td valign="top" width="118px">
            <asp:TextBox ID="txtHoursSpent" runat="server" Width="50px"></asp:TextBox>
        </td>
    </tr>
</table>
