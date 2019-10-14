<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Encryptor.aspx.cs" Inherits="ShiptalkWeb.Encryptor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
    <div>
        &nbsp;<br />
        <br />
        <br />
    
        <br />
        <asp:Panel ID="Panel1" runat="server" BackColor="Cornsilk" Width="330px"  HorizontalAlign="Center">
            <asp:TextBox ID="txtDecrypt" runat="server" Width="186px"></asp:TextBox>
            <br />
            <asp:Label ID="lblDecrypt" runat="server" Font-Size="Smaller" Text="Label" Width="320px"></asp:Label><br />
            <asp:Button ID="btndecrypt" runat="server" Text="Decrypt this password" OnClick="btndecrypt_Click" />
        </asp:Panel>
        
    </div>
    </form>
</body>
</html>
