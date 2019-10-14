<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneratePDF.aspx.cs" Inherits="ShiptalkWeb.SHIP.GeneratePDF" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceViewSHIPProfile" Width="100%" >
    </asp:FormView>

    <pp:ObjectContainerDataSource ID="dataSourceViewSHIPProfile" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.ViewSHIPProfileViewData" />
    </form>
</body>
</html>
