<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebEventMailFormatter.aspx.cs" Inherits="ShiptalkWeb.WebEventMailFormatter" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects"%>
<%@ Import Namespace="System.Web.Management"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form" runat="server">
    <div id="errordiv" runat=server>
      
        
             <strong>
                ** Exception Information **
            </strong>
            <br />
            ---------------
            <br />
            Exception Type: <%# RaisedEvent.ErrorException.GetType().ToString() %>
            <br />
            Exception Message: <%# RaisedEvent.ErrorException.Message %>
            <br />
            Exception Stack Trace: <%# RaisedEvent.ErrorException.StackTrace %>
            <br />
            Exception Source: <%# RaisedEvent.ErrorException.Source %>
            <br />
            
            <strong>
                ** User Information **
            </strong>
            <br />
            ---------------
           <br />
            
             <%# RequestUserInfo %>
             <br />
             <br />
            <strong>
                ** Application Information **
            </strong>
            <br />
            ---------------
            <br />
            Application Domain: <%# WebBaseEvent.ApplicationInformation.ApplicationDomain %>
            <br />
            Trust Level: <%# WebBaseEvent.ApplicationInformation.TrustLevel %>
            <br />
            Application Virtual Path: <%# WebBaseEvent.ApplicationInformation.ApplicationVirtualPath %>
            <br />
            Application Path: <%# WebBaseEvent.ApplicationInformation.ApplicationPath %>
            <br />
            Machine Name: <%# WebBaseEvent.ApplicationInformation.MachineName %>
            <br />
            <br />
             <strong>
                ** Events **
            </strong>
            <br />
            ---------------
            <br />
            Event Code: <%# RaisedEvent.EventCode %>
            <br />
            Event Message: <%# RaisedEvent.Message %>
            <br />
            Event Time: <%# RaisedEvent.EventTime %>
            <br />
            Event Time (UTC): <%# RaisedEvent.EventTimeUtc %>
            <br />
            Event Sequence: <%# RaisedEvent.EventSequence %>
            <br />
            <br />
             <strong>
                ** Process Information **
            </strong>
            <br />
            ---------------
            <br />
            Process Id: <%# RaisedEvent.ProcessInformation.ProcessID %>
            <br />
            Process Name: <%# RaisedEvent.ProcessInformation.ProcessName %>
            <br />
            Account Name: <%# RaisedEvent.ProcessInformation.AccountName %>
            <br />
            <br />
             <strong>
                ** Request Information **
            </strong>
            <br />
            ---------------
            <br />
            Request Url: <%# RequestUrl %>
            <br />
            User Agent: <%# RequestUserAgent %>
            <br />
            User Host Address: <%# RequestUserHostAddress %>
            <br />
            User: <%# RequestUser %>
            <br />
            User Login Status: <%# RequestIsLoggedIn %>
            <br />
           ---------------
            <br />
        
            <br />
    
        </div>
    </form>
</body>
</html>
