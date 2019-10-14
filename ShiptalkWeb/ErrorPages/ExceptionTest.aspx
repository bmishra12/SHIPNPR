<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExceptionTest.aspx.cs" Inherits="ShiptalkWeb.ExceptionTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        form { font-family: Arial; }
        #links
        {
        	margin-top: 10px;
        }
        #links a {
        	font-size: 12px;
        	margin-left: 34px;
        	padding-top: 10px;
        	}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>The following links will help simulate various exception handling scenarios</h3>
        The expected result is that the links will help reach various user friendly error handling pages.
    </div>
    <div id="links">
        <asp:LinkButton ID="lbtnUnhandled" runat="server" onclick="lbtnUnhandled_Click">Simulate Unhandled Exception</asp:LinkButton>
        <br />
        <br />
        <asp:LinkButton ID="lbtnSessionExpired" runat="server" 
            onclick="lbtnSessionExpired_Click">Simulate Session Expired Exception</asp:LinkButton>
        <br />
        <br />
        <asp:LinkButton ID="lbtnImageNotFound" runat="server" 
            onclick="lbtnImageNotFound_Click">Simulate File not found Exception (image)</asp:LinkButton>
        <br />
        <br />
        <asp:LinkButton ID="lbtnFileNotFound" runat="server" 
            onclick="lbtnFileNotFound_Click">Simulate File not found Exception (aspx)</asp:LinkButton>
    </div>
    </form>
</body>
</html>
