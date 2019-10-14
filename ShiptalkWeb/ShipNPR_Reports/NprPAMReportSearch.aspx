<%@ Page Language="C#" MasterPageFile="~/ShiptalkWebWide.Master" AutoEventWireup="true"
    CodeBehind="NprPAMReportSearch.aspx.cs" Inherits="ShiptalkWeb.ShipNPR_Reports.NprPAMReportSearch" %>

<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects.UI" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls"
    TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>Shipnpr Reports</title>
	
	  <link href="../../css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />

   <%-- <script type="text/javascript" src="../../../scripts/jquery-ui-mask-1.7.2.js"></script>--%>
    
	<script type="text/javascript">

	    $(document).ready(function() {

	        $('.datePicker').datepicker({ showButtonPanel: true,
	            onSelect: function() { },
	            changeMonth: true,
	            changeYear: true,
	            showOn: 'button',
	            buttonImage: '../../../css/images/calendar.gif',
	            buttonImageOnly: true
	        })
	    });
	    
	    function selectParams() {

	        var reportType = document.getElementById("ctl00_body1_ReportType").value;

	        ctl00_body1_trState.style.display = 'none'
	        ctl00_body1_Agency.style.display = 'none';
	        ctl00_body1_Submit.style.display = 'none'
	        if (reportType == '1') {
	            ctl00_body1_DateOfContact.style.display = '';
	            ctl00_body1_trState.style.display = ''
	            ctl00_body1_Submit.style.display = ''
	        }
	        if (reportType == '2') {
	            ctl00_body1_DateOfContact.style.display = '';
	            ctl00_body1_trState.style.display = ''
	            ctl00_body1_Agency.style.display = '';
	            ctl00_body1_Submit.style.display = ''
	        }
	    }
    </script>
</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontentwide">
     <div class="dv5col">
        <table id="tblsearchForm" cellspacing="0" cellpadding="0" width="100%" align="center"
            border="1">
            <tr>
                <td class="pageText">
                    &nbsp;
                </td>
                <td  style="font-weight: bold; color: red"
                    colspan="2">
                    <asp:Label ID="errorMessage" CssClass="validationMessage" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="pageText" width="2%">
                    &nbsp;
                </td>
                <td  width="20%">
                    <asp:Label ID="lblPamReportType" runat="server" Text="PAM Report Type:" AssociatedControlID="ReportType"></asp:Label>       
                </td>
                <td class="pageText" width="80%">
                    <asp:DropDownList ID="ReportType" runat="server" ValidationGroup="NprReport"  
                         AutoPostBack="True" 
                        onselectedindexchanged="ReportType_SelectedIndexChanged" >
                        <asp:ListItem Text="<-------------------Select Report Type------------------->" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Activities-Events - By State - By Activity-Event Start Date" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Activities-Events -By Agency - By Activity-Event Start Date" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Activities-Events -By County of Event - By Activity-Event Start Date" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Activities-Events -By ZIP Code of Event - By Activity-Event Start Date" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Activities-Events -By Presenter-Contributor - By Activity-Event Start Date" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Activity-Event Records Entered - By Submitter - By Date Initially Submitted (Date Entered)" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Activities-Events -By Reporting Substate Region Based on Agency Groupings - By Activity-Event Start Date" Value="7"></asp:ListItem>
                        <asp:ListItem Text="Activities-Events -By Reporting Substate Region Based on Counties of Event - By Activity-Event Start Date" Value="8"></asp:ListItem>
                        <asp:ListItem Text="Activities-Events -National - By Activity-Event Start Date" Value="9"></asp:ListItem>
                    </asp:DropDownList>
                 <asp:RequiredFieldValidator runat="server" ID="ReportTypeRequired" Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true" EnableClientScript="true" ControlToValidate="ReportType" InitialValue="0" 
                 ErrorMessage="Report type is required" CssClass="validationMessage"><br />Please select a report type.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
                <td >
                <asp:CompareValidator ID="CompareValidator1"  runat="server" Display="Dynamic" ControlToValidate="dtStartContactDate" ErrorMessage="Enter a valid from date"
                    Operator="DataTypeCheck" Type="Date" CssClass="validationMessage" ValidationGroup="NprReport" >Enter Valid From Date<br /></asp:CompareValidator> 
                        
                    <asp:CompareValidator ID="CompareValidator2"  runat="server" Display="Dynamic" ControlToValidate="dtEndContactDate" ErrorMessage="Enter a valid to date"
                Operator="DataTypeCheck" Type="Date" CssClass="validationMessage" ValidationGroup="NprReport" >Enter Valid To Date<br /></asp:CompareValidator> 
                
                <asp:CompareValidator ID="CompareValidator3" Display="Dynamic"  runat="server" ControlToValidate="dtEndContactDate" ControlToCompare="dtStartContactDate" Type="Date" Operator="GreaterThanEqual"
                 ErrorMessage="To Date Should be greater than From Date."
                CssClass="validationMessage" ValidationGroup="NprReport" >To Date Should be greater than or equal From Date.<br /></asp:CompareValidator> 
                
                    <asp:RequiredFieldValidator ID="StartContactDateRequired" runat="server" ErrorMessage="From date is required"
                    EnableClientScript="true" Display="Dynamic" SetFocusOnError="True" InitialValue="" ControlToValidate="dtStartContactDate"
                    ValidationGroup="NprReport" CssClass="validationMessage" Enabled="true">From date is required<br /></asp:RequiredFieldValidator>
                    
                    <asp:RequiredFieldValidator ID="EndContactDateRequired" runat="server" ErrorMessage="To date is required"
                    EnableClientScript="true" Display="Dynamic" SetFocusOnError="True" InitialValue="" ControlToValidate="dtEndContactDate"
                    ValidationGroup="NprReport" CssClass="validationMessage" Enabled="true">To date is required<br /></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="DateOfContact" runat="server" valign="baseline">
                <td >
                    &nbsp;
                </td>
                <td >
                        Limit to Date Range
                        From
                    |
                        To (MM/DD/YYYY):
                </td>
                <td valign="top">
                    <asp:TextBox ID="dtStartContactDate" CssClass="datePicker" ValidationGroup="NprReport" runat="server" ToolTip="click here to open the calender"
                        Columns="8" MaxLength="10"></asp:TextBox>
                        
                    <asp:TextBox ID="dtEndContactDate" CssClass="datePicker" ValidationGroup="NprReport" runat="server" ToolTip="click here to open the calender"
                        Columns="8" MaxLength="10"></asp:TextBox><br>
                        
                </td>
            </tr>
            <tr id="trState" runat="server" valign="top">
                <td >
                    &nbsp;
                </td>
                <td >
                    <%--<label for="strStateFIPS">
                          State:</label>--%>
                    <asp:Label ID="lblState" runat="server" Text="State:" AssociatedControlID="ddlStates"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlStates" ValidationGroup="NprReport" DataTextField="Value" AutoPostBack="true" DataValueField="Key"
                        AppendDataBoundItems="true" Width="170px" 
                        onselectedindexchanged="ddlStates_SelectedIndexChanged">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true"  EnableClientScript="true" ControlToValidate="ddlStates" InitialValue="" 
                 ErrorMessage="State is required" CssClass="validationMessage"><br />Please select a state.</asp:RequiredFieldValidator>
                 
                     <asp:RequiredFieldValidator runat="server" ID="RequiredState" Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true"  EnableClientScript="true" ControlToValidate="ddlStates" InitialValue="0"
                 ErrorMessage="State is required" CssClass="validationMessage"><br />Please select a state.</asp:RequiredFieldValidator>
                </td>
            </tr>
              <tr id="trSubstateRegionsBasedOnAgencyGroupings" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                    <%--Reporting Substate Regions Based on Agency Groupings<label for="strSubstateRegionsBasedOnAgencyGroupings">:</label>--%>
                    <asp:Label ID="lblSubstateRegionsBasedOnAgencyGroupings" runat="server" Text="Reporting Substate Regions Based on Agency Groupings:" AssociatedControlID="ddlSubstateRegionsBasedOnAgencyGroupings"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlSubstateRegionsBasedOnAgencyGroupings" 
                        ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="170px"  >
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true"  EnableClientScript="true" ControlToValidate="ddlSubstateRegionsBasedOnAgencyGroupings" InitialValue="0" 
                 ErrorMessage="Substate Regions Based on Agency Groupings is required" CssClass="validationMessage"><br />Please select a Substate Regions Based on Agency Groupings.</asp:RequiredFieldValidator>
                </td>
            </tr>
               <tr id="trAgency" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                   <%-- <label for="strAgency">
                        Agency:</label>--%>
                        <asp:Label ID="lblAgency" runat="server" Text=" Agency:" AssociatedControlID="ddlAgency"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlAgency" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        Width="470px" OnSelectedIndexChanged = "ddlAgencyForSubstateAdmin_SelectedIndexChanged">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredAgency" Display="Dynamic" ValidationGroup="NprReport" 
                     SetFocusOnError="true" ControlToValidate="ddlAgency" EnableClientScript="true" InitialValue="0" 
                     ErrorMessage="Agency is required" CssClass="validationMessage"><br />Please select a agency.</asp:RequiredFieldValidator>
                </td>
            </tr>
            
            <tr id="trAgencySubmmiter" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                    <%--<label for="strAgency">
                        Agency:</label>--%>
                    <asp:Label ID="lblAgency2" runat="server" Text=" Agency:" AssociatedControlID="dropDownListAgencyForSubmitter"></asp:Label>
                </td>
                <td >
                                          
                    <asp:DropDownList runat="server" ID="dropDownListAgencyForSubmitter" 
                    ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="470px"  AutoPostBack="true"
                        OnSelectedIndexChanged="dropDownListAgencyForSubmitter_SelectedIndexChanged" >
                       <asp:ListItem Text='-- All of my agencies --' Value="0" />
                       
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trAgencyCounselor" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                   <%-- <label for="strAgency">
                        Agency:</label>--%>
                        <asp:Label ID="lblAgency3" runat="server" Text=" Agency:" AssociatedControlID="dropDownListAgencyForCounselor"></asp:Label>
                </td>
                <td >
                                          
                    <asp:DropDownList runat="server" ID="dropDownListAgencyForCounselor" 
                    ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="470px"  AutoPostBack="true"
                        OnSelectedIndexChanged="dropDownListAgencyForCounselor_SelectedIndexChanged" >
                       <asp:ListItem Text='-- All of my agencies --' Value="0" />
                       
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trCountyOfActivityEvent" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                   <%-- <label for="strCountyOfActivityEvent">
                        County of Activity-Event:</label>--%>
                        <asp:Label ID="lblCountyOfActivityEvent" runat="server" Text="County of Activity-Event:" AssociatedControlID="ddlCountyOfActivityEvent"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlCountyOfActivityEvent" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" 
                        Display="Dynamic" ValidationGroup="NprReport" 
                     SetFocusOnError="true" ControlToValidate="ddlCountyOfActivityEvent" 
                        EnableClientScript="true" InitialValue="0" 
                     ErrorMessage="County of activity-event is required" 
                        CssClass="validationMessage"><br />Please select a county of activity-event.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trZipCodeOfActivityEvent" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                   <%-- <label for="strZipCodeOfActivityEvent">
                        ZIP Code of Activity-Event:</label>--%>
                        <asp:Label ID="lblZipCodeOfActivityEvent" runat="server" Text="ZIP Code of Activity-Event:"  AssociatedControlID="ddlZipCodeOfActivityEvent"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlZipCodeOfActivityEvent" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="370px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" 
                        Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true" ControlToValidate="ddlZipCodeOfActivityEvent" InitialValue="0" 
                 ErrorMessage="Zip code of Activity-Event" CssClass="validationMessage"><br />Please select a zip code of activity-event.</asp:RequiredFieldValidator>
                </td>
            </tr>
          
            <tr id="trSubstateRegionBasedOnCountiesOfActivityEvent" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                    <%--<label for="strSubstateRegionBasedOnCountiesOfActivityEvent">
                        Reporting Substate Region Based on Counties of Activity-Event:</label>--%>
                        <asp:Label ID="lblSubstateRegionBasedOnCountiesOfActivityEvent" runat="server" Text="Reporting Substate Region Based on Counties of Activity-Event:" AssociatedControlID="ddlSubstateRegionBasedOnCountiesOfActivityEvent"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlSubstateRegionBasedOnCountiesOfActivityEvent" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="470px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" 
                        Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true" 
                        ControlToValidate="ddlSubstateRegionBasedOnCountiesOfActivityEvent" InitialValue="0" 
                 ErrorMessage="Substate Region Based on Counties of Activity-Events required" 
                        CssClass="validationMessage"><br />Please select a Substate Region Based on Counties of Activity-Event .</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trPresenterContributor" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                   <%-- Presenter-Contributor<label for="strPresenterContributor">:</label>--%>
                   <asp:Label ID="lblPresenterContributor" runat="server" Text=" Presenter-Contributor:" AssociatedControlID="ddlPresenterContributor"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlPresenterContributor" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="470px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" 
                        Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true"  EnableClientScript="true" 
                        ControlToValidate="ddlPresenterContributor" InitialValue="0" 
                 ErrorMessage="Presenter-Contributor required" CssClass="validationMessage"><br />Please select a Presenter-Contributor.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trSubmitter" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                  <%--  <label for="strSubmitter">
                        Submitter :</label>--%>
                    <asp:Label ID="lblSubmitter" runat="server" Text="Submitter :" AssociatedControlID="ddlSubmitter"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlSubmitter" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="470px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true"  EnableClientScript="true" ControlToValidate="ddlSubmitter" InitialValue="0" 
                 ErrorMessage="Submitter is required" CssClass="validationMessage"><br />Please select a Submitter.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="Submit" runat="server" valign="top">
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    <asp:Button ID="btnSubmit" runat="server" ValidationGroup="NprReport" CausesValidation="true" onclick="btnSubmit_Click" 
                        text="Submit" />
                </td>
            </tr>
        </table>
    </div>
    </div>
</asp:Content>
