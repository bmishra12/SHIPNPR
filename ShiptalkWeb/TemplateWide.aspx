<%@ Page Language="C#" AutoEventWireup="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="/UserControls/ucNavLeft.ascx" TagName="NavLeft" TagPrefix="air" %>
<%@ Register Src="/UserControls/ucSearch.ascx" TagName="Search" TagPrefix="air" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link id="style" rel="stylesheet" type="text/css" href="css/global.css" />
    <script type="text/javascript" src="scripts/global.js"></script>
    <script type="text/javascript" src="scripts/jquery-1.3.2.js"></script>
</head>
<body>
<form id="form2" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div id="container">
        <div id="header">
            <div id="searchBox">
            
                <air:Search ID="Search1" runat="server" />
                
            </div>
        </div>
        <div id="navigation">
            <ul id="navbar">
                <li id="tabLink1" class="tabLinkActive"><a href="javascript:openTab(1);" id="href1">HOME</a></li>
	            <li id="tabLink2" class="tabLink"><a href="javascript:openTab(2);" id="href2">INFORMATION LIBRARY</a></li>
	            <li id="tabLink3" class="tabLink"><a href="javascript:openTab(3);" id="href3">REGISTER</a></li>
	        </ul>
        </div>
        <div id="body">
                <div id="maincontentwide">
                    <div class="dv5col">
                        <h1>Div class:  dv5col</h1>
                        <p>Use the dv5col class to create this box with the gradient.  This should be used as the main content area
                        for all sub pages.  Use a div class=clear between rows in this main content area.</p>
                    </div>
                    <div class="clear"></div>
                    
                    <div class="dv5col">
                        <h1>Div class:  dv5col</h1>
                        <p>
                        Grid Example.  I'm still working on the styles for this. Until then, just use the table layout as below.
                        When I add the styles to it, the tables will automatically get updated.
                        </p>
                        
                        <table>
                            <thead>
                                <th>Table Header Cell 1</th>
                                <th>Table Header Cell 2</th>
                            </thead>
                            <tr>
                                <td>Table content cell 1</td>
                                <td>Table content cell 2</td>
                            </tr>
                        </table>
                    </div>
                    <div class="clear"></div>
                    
                    <div class="dv5col">
                        <h1>Form Template</h1>
                        
                        <p>Template for a form.  The following is a template for a form.  This is some explanatory 
                        text for the form.  Please fill out the form below.</p>
                        
                        <table class="formTable">
                            <tr>
                                <td>Textfield here:</td>
                                <td><asp:TextBox ID="txt1" runat="server" CssClass="textfield1"></asp:TextBox>
                                    <cc1:TextBoxWatermarkExtender ID="tbwe1" runat="server"
                                            TargetControlID="txt1"
                                            WatermarkText="Name"
                                            WatermarkCssClass="textfield1wm" />
                                </td>
                            </tr>
                            <tr>
                                <td>Account you are requesting:</td>
                                <td>
                                    <asp:DropDownList ID="DropDownList4" runat="server" CssClass="dropdown1wm" onchange="ddlChange(this)">
                                        <asp:ListItem Text="Select.." Value=""></asp:ListItem>
                                        <asp:ListItem Text="One" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Two" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="formTableAltRow">
                                <td>Checkbox text:</td>
                                <td>
                                    <input type="checkbox" name="CheckBox1" value="testValue" class="checkbox1" checked>Check this box now!
                                    <br />
                                    <input type="checkbox" name="CheckBox2" value="testValue2" class="checkbox1">Check this box
                                    <br />
                                    <input type="checkbox" name="CheckBox2" value="testValue2" class="checkbox1">Check this box
                                </td>
                            </tr>
                            <tr>
                                <td>Textarea example:</td>
                                <td>
                                    <textarea NAME="textarea1" class="textarea1" rows="10" cols="60" wrap></textarea>
                                </td>
                            </tr>
                            <tr class="formTableAltRow">
                                <td>Radio example:</td>
                                <td>
                                    <input type="radio" class="checkbox1" name="group1" value="1"> Radio 1
                                    <br>
                                    <input type="radio" class="checkbox1" name="group1" value="2" checked> Radio 2
                                    <br>
                                    <input type="radio" class="checkbox1" name="group1" value="3"> Radio 3

                                </td>
                            </tr>
                        </table>
                        
                    </div>
                    
                </div>

         </div>
         
        <div id="footer">
	        <p id="TemplateWideaspx" >
		        <a href="/User/Disclaimer.aspx">Disclaimer</a> &nbsp;&nbsp;|&nbsp;&nbsp;
		        <a href="http://www.cms.gov">www.cms.gov</a> &nbsp;&nbsp;|&nbsp;&nbsp; <a href="http://www.medicare.gov">
			        www.medicare.gov</a>
		        &nbsp;&nbsp;&nbsp;&nbsp;©2004 State Health Insurance Assistance Program. All rights reserved.
	        </p>
        </div>
    </div>
    </form>
</body>
</html>
