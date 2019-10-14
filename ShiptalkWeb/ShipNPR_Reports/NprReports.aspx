<%@ Page Language="C#" MasterPageFile="~/ShiptalkWebWide.Master" AutoEventWireup="true"
    CodeBehind="NprReports.aspx.cs" Inherits="ShiptalkWeb.ShipNPR_Reports.NprReports" %>

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
    <div id="maincontentwide" >
     <div class="dv5col">
        <table id="tblsearchForm" cellspacing="0" cellpadding="0" width="100%" align="center"
            border="1">
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="2">
                <div class="commands">
                    <asp:LinkButton ID="lnkPamReports" Visible="true" runat="server" onclick="lnkPamReports_Click">PAM Reports</asp:LinkButton>
                     <br />
                     <a id="aReportList" runat="server" href='<%# RouteController.ReportSubstateSearch() %>'>Create a Sub State Region for Reporting Purposes</a>
                     <br />
                      <a runat="server" id="a1" href="~/Npr/Docs/Chapter_5_NPR_Reports_508.pdf"  target='_blank' > <b>NPR User Manual: NPR Reports</b></a>
               
                    </div>
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td  style="font-weight: bold; color: red"
                    colspan="2">
                    <asp:Label ID="errorMessage" CssClass="validationMessage" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="2%">
                    &nbsp;
                </td>
                <td  width="20%">
                    CC Report Type:
                </td>
                <td width="80%">
                    <asp:DropDownList ID="ReportType" runat="server" ValidationGroup="NprReport"  ToolTip="CC Report Type"
                         AutoPostBack="True"  
                        onselectedindexchanged="ReportType_SelectedIndexChanged" >
                        <asp:ListItem Text="<-----------Select Report Type----------->" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Contacts - By State - By Date of Contact" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Contacts - By Agency - By Date of Contact" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Contacts - By County of Counselor Location - By Date of Contact" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Contacts - By ZIP Code of Counselor Location - By Date of Contact" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Contacts - By County of Client Residence - By Date of Contact" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Contacts - By ZIP Code of Client Residence - By Date of Contact" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Contacts - By Counselor - By Date of Contact" Value="7"></asp:ListItem>
                        <asp:ListItem Text="Contact Records Entered - By Submitter - By Date Initially Submitted (Date Entered)" Value="8"></asp:ListItem>
                        <asp:ListItem Text="Contacts - By Reporting Substate Region Based on Agency Groupings - By Date of Contact" Value="9"></asp:ListItem>
                        <asp:ListItem Text="Contacts - By Reporting Substate Region Based on Counties of Counselor Locations - By Date of Contact" Value="10"></asp:ListItem>
                        <asp:ListItem Text="Contacts - By Reporting Substate Region Based on ZIP Codes of Counselor Locations - By Date of Contact" Value="11"></asp:ListItem>
                        <asp:ListItem Text="Contacts - By Reporting Substate Region Based on Counties of Client Residence - By Date of Contact" Value="12"></asp:ListItem>
                        <asp:ListItem Text="Contacts - By Reporting Substate Region Based on ZIP Codes of Client Residence - By Date of Contact" Value="13"></asp:ListItem>
                        <asp:ListItem Text="Contacts - National  - By Date of Contact" Value="14"></asp:ListItem>
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
                   <%-- <label for="strStateFIPS">
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
              <tr id="SubstateRegionsBasedOnAgencyGroupings" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                  <%--  <label for="strSubstateRegionsBasedOnAgencyGroupings">
                        SubstateAgencyRegionID:</label>--%>
                    <asp:Label ID="lblSubstateAgencyRegionID" runat="server" Text="SubstateAgencyRegionID:" AssociatedControlID="ddlSubstateRegionsBasedOnAgencyGroupings"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlSubstateRegionsBasedOnAgencyGroupings" 
                        ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="400px" AutoPostBack="True" 
                        
                        Height="22px">
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
                        <asp:Label ID="lblAgency" runat="server" Text="Agency:" AssociatedControlID="ddlAgency"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlAgency" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                       Width="470px"  OnSelectedIndexChanged = "ddlAgencyForSubstateAdmin_SelectedIndexChanged">
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
                        <asp:Label ID="lblAgency1" runat="server" Text="Agency:" AssociatedControlID="dropDownListAgencyForSubmitter"></asp:Label>
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
                    <%--<label for="strAgency">
                        Agency:</label>--%>
                        <asp:Label ID="lblAgency2" runat="server" Text="Agency:" AssociatedControlID="dropDownListAgencyForCounselor"></asp:Label>
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
            <tr id="trCountyOfCounselorLocation" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                    <%--<label for="strCountyOfCounselorLocation">
                        County Of Counselor Location:</label>--%>
                    <asp:Label ID="lblCountyOfCounselorLocation" runat="server" Text="County Of Counselor Location:" AssociatedControlID="ddlCountyOfCounselorLocation"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlCountyOfCounselorLocation" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="NprReport" 
                     SetFocusOnError="true" ControlToValidate="ddlCountyOfCounselorLocation" EnableClientScript="true" InitialValue="0" 
                     ErrorMessage="County of counselor location is required" CssClass="validationMessage"><br />Please select a county of counselor location.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trZipCodeCounselorLocation" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                    <%--<label for="strZipCodeCounselorLocation">
                        ZIP Code of Counselor Location:</label>--%>
                    <asp:Label ID="lblZipCodeCounselorLocation" runat="server" Text="ZIP Code of Counselor Location:" AssociatedControlID="ddlZipCodeOfCounselorLocation"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlZipCodeOfCounselorLocation" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="170px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true" ControlToValidate="ddlZipCodeOfCounselorLocation" InitialValue="0" 
                 ErrorMessage="Zip code of counselor location is required" CssClass="validationMessage"><br />Please select a zip code of counselor location.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="CountyofClientResidence" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                    <%--<label for="strCountyofClientResidence">
                        County of Client Residence:</label>--%>
                    <asp:Label ID="lblCountyofClientResidence" runat="server" Text="County of Client Residence:" AssociatedControlID="ddlCountyofClientResidence"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlCountyofClientResidence" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="170px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true" ControlToValidate="ddlCountyofClientResidence" InitialValue="0" 
                 ErrorMessage="County of Client Residence is required" CssClass="validationMessage"><br />Please select a county of client residence.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="ZIPCodeofClientResidence" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                    <%--<label for="strZIPCodeofClientResidence">
                        Zip Code of Client Residence:</label>--%>
                    <asp:Label ID="lblZIPCodeofClientResidence" runat="server" Text="Zip Code of Client Residence:" AssociatedControlID="ddlZIPCodeofClientResidence"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlZIPCodeofClientResidence" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="170px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true" ControlToValidate="ddlZIPCodeofClientResidence" InitialValue="0" 
                 ErrorMessage="Zip code of Client Residence is required" CssClass="validationMessage"><br />Please select a zip code of client residence.</asp:RequiredFieldValidator>
                </td>
            </tr>
          
            <tr id="SubstateRegionBasedOnCountiesOfCounselorLocations" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                   <%-- <label for="strSubstateRegionBasedOnCountiesOfCounselorLocations">
                        Substate Region Based on Counties of Counselor Locations:</label>--%>
                    <asp:Label ID="lblSubstateRegionBasedOnCountiesOfCounselorLocations" runat="server" Text="Substate Region Based on Counties of Counselor Locations:" AssociatedControlID="ddlSubstateRegionBasedOnCountiesOfCounselorLocations"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlSubstateRegionBasedOnCountiesOfCounselorLocations" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="170px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true" ControlToValidate="ddlSubstateRegionBasedOnCountiesOfCounselorLocations" InitialValue="0" 
                 ErrorMessage="Substate Region Based on Counties of Counselor Locations is required" CssClass="validationMessage"><br />Please select a Substate Region Based on Counties of Counselor Locations .</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="SubstateRegionBasedOnZIPCodesOfCounselorLocations" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                    <%--<label for="strSubstateRegionBasedOnZIPCodesOfCounselorLocations">
                        Substate Region Based on ZIP Codes of Counselor Locations :</label>--%>
                    <asp:Label ID="lblSubstateRegionBasedOnZIPCodesOfCounselorLocations" runat="server" Text="Substate Region Based on ZIP Codes of Counselor Locations :" AssociatedControlID="ddlSubstateRegionBasedOnZIPCodesOfCounselorLocations"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlSubstateRegionBasedOnZIPCodesOfCounselorLocations" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="170px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true" ControlToValidate="ddlSubstateRegionBasedOnZIPCodesOfCounselorLocations" InitialValue="0" 
                 ErrorMessage="Substate Region Based on ZIP Codes of Counselor Locations is required" CssClass="validationMessage"><br />Please select a Substate Region Based on ZIP Codes of Counselor Locations .</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="SubstateRegionBasedonCountiesofClientResidence" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                   <%-- <label for="strSubstateRegionBasedonCountiesofClientResidence">
                        Substate Region Based on Counties of Client Residence :</label>--%>
                    <asp:Label ID="lblSubstateRegionBasedonCountiesofClientResidence" runat="server" Text="Substate Region Based on Counties of Client Residence :" AssociatedControlID="ddlSubstateRegionBasedonCountiesofClientResidence"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlSubstateRegionBasedonCountiesofClientResidence" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="170px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true" ControlToValidate="ddlSubstateRegionBasedonCountiesofClientResidence" InitialValue="0" 
                 ErrorMessage="Substate Region Based on Counties of Client Residence is required" CssClass="validationMessage"><br />Please select a Substate Region Based on Counties of Client Residence.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="SubstateRegionBasedOnZIPCodesofClientResidence" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                  <%--  <label for="strSubstateRegionBasedOnZIPCodesofClientResidence">
                        Substate Region Based on ZIP Codes of Client Residence :</label>--%>
                    <asp:Label ID="lblSubstateRegionBasedOnZIPCodesofClientResidence" runat="server" Text=" Substate Region Based on ZIP Codes of Client Residence :" AssociatedControlID="ddlSubstateRegionBasedOnZIPCodesofClientResidence"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlSubstateRegionBasedOnZIPCodesofClientResidence" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="170px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true" ControlToValidate="ddlSubstateRegionBasedOnZIPCodesofClientResidence" InitialValue="0" 
                 ErrorMessage="Substate Region Based on ZIP Codes of Client Residence is required" CssClass="validationMessage"><br />Please select a Substate Region Based on ZIP Codes of Client Residence.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="Counselor" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                   <%-- <label for="strCounselor">
                        Counselor :</label>--%>
                    <asp:Label ID="lblCounselor" runat="server" Text="Counselor :" AssociatedControlID="ddlCounselor"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlCounselor" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="470px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" Display="Dynamic" ValidationGroup="NprReport" 
                 SetFocusOnError="true"  EnableClientScript="true" ControlToValidate="ddlCounselor" InitialValue="0" 
                 ErrorMessage="Counselor is required" CssClass="validationMessage"><br />Please select a Counselor.</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="Submitter" runat="server" valign="top">
                <td>
                    &nbsp;
                </td>
                <td >
                   <%-- <label for="strSubmitter">
                        Submitter :</label>--%>
                    <asp:Label ID="lblSubmitter" runat="server" Text="Submitter :" AssociatedControlID="ddlSubmitter"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList runat="server" ID="ddlSubmitter" ValidationGroup="NprReport" DataTextField="Value" DataValueField="Key"
                        AppendDataBoundItems="true" Width="170px">
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
