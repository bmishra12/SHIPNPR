<%@ Page Language="C#" MasterPageFile="~/ShiptalkWebWideNoTab.Master"  AutoEventWireup="true" CodeBehind="SHIPprofile.aspx.cs" Inherits="ShiptalkWeb.SHIPprofile" Title="Shiptalk" %>

<%--<asp:Content id="Content1" contentplaceholderid="head" runat="server" />--%>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="content-type" content="text/html;charset=ISO-8859-1" />
                <meta name="description"
                  content="The State Health Insurance Assistance Program, or SHIP, is a national program that offers one-on-one counseling and assistance to people with Medicare and their families" />
</asp:Content> 



<asp:Content id="Content2" contentplaceholderid="body1" runat="server">

    <div id="maincontent">
        <div class="dv3col">
            <h1>What is SHIPtalk?</h1>
            <p>The State Health Insurance Assistance Program, or SHIP, is a 
            national program that offers one-on-one counseling and assistance 
            to people with Medicare and their families.</p>
        </div>
        <div class="clear"></div>
        
        
        <div class="dv4colleft dvFindAShip">
            <h2>Find a State SHIP</h2>
            <p>Looking for a State SHIP?  Select your state below to find your local SHIP branch.</p>
            <asp:DropDownList runat="server" ID="ddlStates1" DataTextField="Value" DataValueField="Key"
                                                            AppendDataBoundItems="true" Width="170px" ToolTip="Select State" 
                                onselectedindexchanged="ddlStates1_SelectedIndexChanged">
                                                            <asp:ListItem Text="Select a State" Value="0" />
                                                        </asp:DropDownList>
            <asp:Button ID="Button2" runat="server" Text="GO >>" CssClass="formbutton3" />
        </div>
        
       
        <div class="clear"></div>
        <div>
         <asp:LinkButton ID="lnkBtn" Text="Back to SHIPtalk" ToolTip="Back to SHIPtalk" runat="server" PostBackUrl="http://nprpreprocessor.shiptalk.org/" Font-Bold="true"></asp:LinkButton><br />
           <asp:ImageButton ID="ImageButton1" ToolTip="Back to SHIPtalk" ImageUrl="~/images/ShpTlkbtn.jpg" PostBackUrl="http://nprpreprocessor.shiptalk.org/" runat="server"  />
        </div>
      
    </div>
    
</asp:Content>
