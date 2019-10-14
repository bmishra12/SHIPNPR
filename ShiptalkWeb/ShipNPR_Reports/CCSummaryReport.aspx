<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ShiptalkWebWide.Master" CodeBehind="CCSummaryReport.aspx.cs" Inherits="ShiptalkWeb.ShipNPR_Reports.CCSummaryReport" %>

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
    <title>NPR Client Contact Reports</title>

        <script type="text/javascript">
        
            function PrintThisPage() {
                var sOption = "toolbar=no,location=no,directories=yes,menubar=yes,";
                sOption += "scrollbars=yes,width=750,height=600,left=100,top=25";

                var sWinHeaderHTML = document.getElementById('contentHeaderforprint').innerHTML;

                var sWinHTML = document.getElementById('contentforprint').innerHTML;

                var winprint = window.open("", "", sOption);
                winprint.document.open();
                winprint.document.write('<html xmlns="http://www.w3.org/1999/xhtml">');
                winprint.document.write('<LINK href=../../../css/print.css rel=Stylesheet><body>');
                winprint.document.write(sWinHeaderHTML);
                winprint.document.write(sWinHTML);
                winprint.document.write('</body></html>');
                winprint.document.close();
                winprint.focus();
            }
            
         function PopUp()
            {
                window.open("preview", "Contactus", "menubar=no, width=800,height=600,toolbar=no,menubar=yes,scrollbars=yes");
            }
        </script>
</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontentwide" width="100%">
        <div class="commands">
            <asp:Panel ID="panelPrinterFriendly" runat="server">
                <a href="javascript:PrintThisPage();">Printer Friendly Version</a> &nbsp;
            </asp:Panel>
        </div>
                            
        <%--<a href="javascript:PopUp();"  visible="false" id="linkPrinterFriendly" style="color: Blue;" >Printer friendly version</a>
        --%> 
        <div id="contentHeaderforprint">  
            <table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">

				        <tr>
					        <td  width="100%">State Health Insurance Assistance Program - 
                                SHIP - National Performance Report - NPR
					        </td>
				        </tr>
				        <tr>
					        <td  align="left" width="100%"><asp:label id="strSubTitle" runat="server">Client Contact Summary Report</asp:label><br>
						        </td>
				        </tr>
				        <tr>
					        <td  align="left" width="100%">
					        <asp:label id="strDateOfContact" runat="server" Text=''></asp:label></td>
				        </tr>
				        <tr runat="server" id="trState" visible="false"> 
					        <td  align="left" width="100%"><asp:label id="strState" runat="server" ></asp:label></td>
				        </tr>
				        <tr runat="server" id="trAgencies" visible="false">
					        <td  align="left" width="100%"><asp:label id="strAgencies" runat="server" ></asp:label></td>
				        </tr>
				        <tr runat="server" id="trCountyOfCounselorLocation" visible="false">
					        <td  align="left" width="100%"><asp:label id="strCountyOfCounselorLocation" runat="server" ></asp:label></td>
				        </tr>
					        <tr runat="server" id="trZipCodeOfCounselorLocation" visible="false">
					        <td  align="left" width="100%"><asp:label id="strZipCodeOfCounselorLocation" runat="server" ></asp:label></td>
				        </tr>
				        <tr runat="server" id="trCountyOfClientResidence" visible="false">
					        <td  align="left" width="100%"><asp:label id="strCountyOfClientResidence" runat="server" ></asp:label></td>
				        </tr>
					        <tr runat="server" id="trZipCodeOfClientResidence" visible="false">
					        <td  align="left" width="100%"><asp:label id="strZipCodeOfClientResidence" runat="server" ></asp:label></td>
				        </tr>
    			        <tr runat="server" id="trContactsBySubStateRegionOnAgency" visible="false">
					        <td  align="left" width="100%"><asp:label id="strContactsBySubStateRegionOnAgency" runat="server" ></asp:label></td>
				        </tr>
				        <tr runat="server" id="trCounselor" visible="false">
					        <td  align="left" width="100%"><asp:label id="strCounselor" runat="server" ></asp:label></td>
				        </tr>
				        <tr runat="server" id="trSubmitter" visible="false">
					        <td  align="left" width="100%"><asp:label id="strSubmitter" runat="server" ></asp:label></td>
				        </tr>
				        <tr runat="server" id="trNational" visible="false">
					        <td  align="left" width="100%"><asp:label id="strNational" runat="server" ></asp:label></td>
				        </tr>
				        <tr>
					        <td  align="left" width="100%"><asp:label id="strRunDate" runat="server" ></asp:label></td>
				        </tr>
        			
				        <tr>
					        <td width="100%">&nbsp;
					        </td>
				        </tr>
			        </table>
		</div>   	
		    <div class="dv5col">
		    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
            <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceViewCCSummary"  Width="100%">
                <ItemTemplate>
                   <div id="dv3colFormContent" >
                      <div id="contentforprint"> 
                        <table id="tblsearchResults" class="TableBorder" cellspacing="1" border="1" cellpadding="1" width="100%" align="center" runat="server">
				            <tr valign="baseline">
					            <td width="80%" class="TableHeaderBorder1">
					                <strong>Contacts by Type, Client Demographics, Topics Discussed, Time Spent, Contact 
                                    Status</strong></td>
					            <td  width="10%" class="TableHeaderBorder1"><strong>Contacts</strong>
					            </td>
					            <td  width="10%" class="TableHeaderBorder1" ><strong>Distribution</strong>
					            </td>
				            </tr>
				            <tr valign="baseline">
					            <td class="TDBorder">Total Client Contacts</td>
					            <td  width="10%" class="TDBorder"> <%# Eval("TotalClientContacts").EncodeHtml()%> </td>
					            <td  width="10%" class="TDBorder">100.0%</td>
				            </tr>
				            <tr valign="baseline">
					            <td >First Contact for the Client&#39;s Issue</td>
					            <td  width="10%"><%# Eval("ClientFirstVsContinuingContact1").EncodeHtml() %></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientFirstVsContinuingContact1").EncodeHtml()),Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml())) %>%</td>
				            </tr>
				            <tr valign="baseline">
					            <td >Continuing Contacts for the Client&#39;s Issue</td>
					            <td  width="10%"><%# Eval("ClientFirstVsContinuingContact2").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientFirstVsContinuingContact2").EncodeHtml()),Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml())) %>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td class="TDBorder">First vs Continuing Missing, Blank, Not Collected, Invalid, 
                                    Miscoded, Out of Range</td>
					            <td  width="10%" class="TDBorder"><%# Eval("ClientFirstVsContinuingContact3").EncodeHtml()%></td>
					            <td width="10%" class="TDBorder"><%# GetDistribution(Convert.ToDecimal(Eval("ClientFirstVsContinuingContact3").EncodeHtml()),Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml())) %>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Client Learned About SHIP From Previous Contact with a SHIP</td>
					            <td  width="10%"><%# Eval("ClientLearnedAboutSHIP1").EncodeHtml()%></td>
					            <td width="10%" ><%# GetDistribution(Convert.ToDecimal(Eval("ClientLearnedAboutSHIP1").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Client Learned About SHIP From CMS / Medicare Website Brochures Mailings 1-800</td>
					            <td  width="10%"><%# Eval("ClientLearnedAboutSHIP2").EncodeHtml()%></td>
					            <td width="10%" ><%# GetDistribution(Convert.ToDecimal(Eval("ClientLearnedAboutSHIP2").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Client Learned About SHIP From Presentations or Fairs</td>
					            <td  width="10%"><%# Eval("ClientLearnedAboutSHIP3").EncodeHtml()%></td>
					            <td width="10%" ><%# GetDistribution(Convert.ToDecimal(Eval("ClientLearnedAboutSHIP3").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Client Learned About SHIP From State-Specific Mailings, Brochures, Posters</td>
					            <td  width="10%"><%# Eval("ClientLearnedAboutSHIP4").EncodeHtml()%></td>
					            <td width="10%" ><%# GetDistribution(Convert.ToDecimal(Eval("ClientLearnedAboutSHIP4").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Learned About SHIP From Another Agency - Social Security, Senior Org, Disability Org</td>
					            <td  width="10%"><%# Eval("ClientLearnedAboutSHIP5").EncodeHtml()%></td>
					            <td width="10%" ><%# GetDistribution(Convert.ToDecimal(Eval("ClientLearnedAboutSHIP5").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Client Learned About SHIP From Friend or Relative</td>
					            <td  width="10%"><%# Eval("ClientLearnedAboutSHIP6").EncodeHtml()%></td>
					            <td width="10%" ><%# GetDistribution(Convert.ToDecimal(Eval("ClientLearnedAboutSHIP6").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Client Learned About SHIP From Media - PSA Ad Newspaper Radio TV</td>
					            <td  width="10%"><%# Eval("ClientLearnedAboutSHIP7").EncodeHtml()%></td>
					            <td width="10%" ><%# GetDistribution(Convert.ToDecimal(Eval("ClientLearnedAboutSHIP7").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Learned About SHIP From State Website</td>
					            <td  width="10%"><%# Eval("ClientLearnedAboutSHIP8").EncodeHtml()%></td>
					            <td width="10%" ><%# GetDistribution(Convert.ToDecimal(Eval("ClientLearnedAboutSHIP8").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Learned About SHIP From Some Other Method</td>
					            <td  width="10%"><%# Eval("ClientLearnedAboutSHIP9").EncodeHtml()%></td>
					            <td width="10%" ><%# GetDistribution(Convert.ToDecimal(Eval("ClientLearnedAboutSHIP9").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">How Learned Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range</td>
					            <td  width="10%" class="TDBorder"><%# Eval("ClientLearnedAboutSHIP10").EncodeHtml()%></td>
					            <td width="10%" class="TDBorder"><%# GetDistribution(Convert.ToDecimal(Eval("ClientLearnedAboutSHIP10").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Phone Call Contact</td>
					            <td  width="10%"><%# Eval("ClientMethodOfContact1").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientMethodOfContact1").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Face to Face Contact at Counseling Location or Event Site</td>
					            <td  width="10%"><%# Eval("ClientMethodOfContact2").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientMethodOfContact2").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Face to Face Contact at Client's Home or Facility</td>
					            <td  width="10%"><%# Eval("ClientMethodOfContact3").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientMethodOfContact3").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Email Contact</td>
					            <td  width="10%"><%# Eval("ClientMethodOfContact4").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientMethodOfContact4").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Postal Mail or Fax Contact</td>
					            <td  width="10%"><%# Eval("ClientMethodOfContact5").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientMethodOfContact5").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Old Email-Fax-Postal</td>
					            <td  width="10%"><%# Eval("ClientMethodOfContact6").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientMethodOfContact6").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Old Unknown</td>
					            <td  width="10%"><%# Eval("ClientMethodOfContact7").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientMethodOfContact7").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td class="TDBorder">Method of Contact Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range</td>
					            <td  width="10%" class="TDBorder"><%# Eval("ClientMethodOfContact8").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientMethodOfContact8").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Client Age 64 or Younger</td>
					            <td  width="10%"><%# Eval("ClientAgeGroup1").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientAgeGroup1").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Age 65-74</td>
					            <td  width="10%"><%# Eval("ClientAgeGroup2").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientAgeGroup2").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Age 75-84</td>
					            <td  width="10%"><%# Eval("ClientAgeGroup3").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientAgeGroup3").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Age 85 or Older</td>
					            <td  width="10%"><%# Eval("ClientAgeGroup4").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientAgeGroup4").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">Client Age Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range</td>
					            <td  width="10%" class="TDBorder"><%# Eval("ClientAgeGroup5").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientAgeGroup5").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Female</td>
					            <td  width="10%" ><%# Eval("ClientGender1").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientGender1").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Male</td>
					            <td  width="10%"><%# Eval("ClientGender2").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientGender2").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td class=TDBorder>Client Gender Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range</td>
					            <td  width="10%" class=TDBorder><%# Eval("ClientGender3").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientGender3").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Any Mention of Hispanic, Latino, or Spanish Origin [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace1").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace1").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Client Any Mention of White, Non-Hispanic [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace2").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace2").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Any Mention of Black, African American [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace3").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace3").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Any Mention of American Indian or Alaska Native [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace4").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace4").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Any Mention of Asian Indian [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace5").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace5").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Client Any Mention of Chinese [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace6").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace6").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Any Mention of Filipino [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace7").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace7").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Any Mention of Japanese [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace8").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace8").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Client Any Mention of Korean [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace9").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace9").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Any Mention of Vietnamese [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace10").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace10").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Any Mention of Native Hawaiian [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace11").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace11").EncodeHtml()),Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml())) %>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Client Any Mention of Guamanian or Chamorro [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace12").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace12").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Client Any Mention of Samoan [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace13").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace13").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Client Any Mention of Other Asian [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace14").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace14").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Any Mention of Other Pacific Islander [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace15").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace15").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Any Mention of Some Other Race-Ethnicity [Can Select More Than One]</td>
					            <td  width="10%"><%# Eval("ClientContactRace16").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace16").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Old Asian Code [Single Choice]</td>
					            <td  width="10%"><%# Eval("ClientContactRace17").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace17").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Old Native Hawaiian or other Pacific Islander Code [Single Choice]</td>
					            <td  width="10%"><%# Eval("ClientContactRace18").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace18").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client Old Other Code [Single Choice]</td>
					            <td  width="10%"><%# Eval("ClientContactRace19").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace19").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Client Race-Ethnicity Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range</td>
					            <td  width="10%"><%# Eval("ClientContactRace20").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace20").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td class="TDBorder">Client Selected More Than One Race-Ethnicity Category [Can Select More Than One]</td>
					            <td  width="10%" class="TDBorder"><%# Eval("ClientContactRace21").EncodeHtml()%></td>
					            <td width="10%" class="TDBorder"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactRace21").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            
				            
				            
				            <!-- new -->
				            
				            <tr valign="baseline">
					            <td >Client's Primary  Language is Other Than English</td>
					            <td  width="10%"><%# Eval("ClientPrimaryLanguageOtherThanEnglish1").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientPrimaryLanguageOtherThanEnglish1").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr valign="baseline">
					            <td >English is Client's Primary Language</td>
					            <td  width="10%"><%# Eval("ClientPrimaryLanguageOtherThanEnglish2").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientPrimaryLanguageOtherThanEnglish2").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td class="TDBorder">Client 's Primary Language  Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range</td>
					            <td  width="10%" class="TDBorder"><%# Eval("ClientPrimaryLanguageOtherThanEnglish3").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientPrimaryLanguageOtherThanEnglish3").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Client's Income (or Client Plus Spouse's Income) is Below 150% of Federal Poverty Level</td>
					            <td  width="10%"><%# Eval("ClientMonthlyIncome1").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientMonthlyIncome1").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Client's Income (or Client Plus Spouse's Income) is At or Above 150% of FPL</td>
					            <td  width="10%"><%# Eval("ClientMonthlyIncome2").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientMonthlyIncome2").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td class="TDBorder">Client's Income Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range</td>
					            <td  width="10%" class="TDBorder"><%# Eval("ClientMonthlyIncome3").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientMonthlyIncome3").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Client's Assets are Below LIS Asset Limits</td>
					            <td  width="10%"><%# Eval("ClientAssests1").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientAssests1").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client's Assets are Above LIS Asset Limits</td>
					            <td  width="10%"><%# Eval("ClientAssests2").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientAssests2").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td class="TDBorder">Client's Assets Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range</td>
					            <td  width="10%" class="TDBorder"><%# Eval("ClientAssests3").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientAssests3").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            
				            
				             <tr vAlign="baseline">
					            <td >Client is Receiving or Applying for Social Security Disability or Medicare Disability</td>
					            <td  width="10%"><%# Eval("ClientReceivingSSOrMedicareDisability1").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientReceivingSSOrMedicareDisability1").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client is Neither Receiving Nor Applying for Social Security Disability or Medicare Disability</td>
					            <td  width="10%"><%# Eval("ClientReceivingSSOrMedicareDisability2").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientReceivingSSOrMedicareDisability2").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">Client's Disabled Prog Status Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range</td>
					            <td  width="10%" class="TDBorder"><%# Eval("ClientReceivingSSOrMedicareDisability3").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientReceivingSSOrMedicareDisability3").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Client is Dual Eligible Medicare-Medicaid with Mental Illness / Mental Disability [DMD]</td>
					            <td  width="10%"><%# Eval("ClientDualEligble1").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientDualEligble1").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Client is Not Dual Eligible Medicare-Medicaid with Mental Illness / Mental Disability [DMD]</td>
					            <td  width="10%"><%# Eval("ClientDualEligble2").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientDualEligble2").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">Client's DMD Status Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range</td>
					            <td  width="10%" class="TDBorder"><%# Eval("ClientDualEligble3").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientDualEligble3").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medicare Prescription Drug Coverage (Part D) - Eligibility/Screening [Topic]</td>
					            <td  width="10%"><%# Eval("ClientTopicID1").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID1").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Medicare Prescription Drug Coverage (Part D ) - Benefit Explanation</td>
					            <td  width="10%"><%# Eval("ClientTopicID2").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID2").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medicare Prescription Drug Coverage (Part D) - Plans Comparison</td>
					            <td  width="10%"><%# Eval("ClientTopicID3").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID3").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medicare Prescription Drug Coverage (Part D) - Plan Enrollment/Disenrollment</td>
					            <td  width="10%"><%# Eval("ClientTopicID4").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID4").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Medicare Prescription Drug Coverage (Part D) - Claims/Billing</td>
					            <td  width="10%"><%# Eval("ClientTopicID5").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID5").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				           <tr vAlign="baseline">
					            <td >Medicare Prescription Drug Coverage (Part D) - Appeals/Grievances</td>
					            <td  width="10%"><%# Eval("ClientTopicID6").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID6").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Medicare Prescription Drug Coverage (Part D) - Fraud and Abuse</td>
					            <td  width="10%"><%# Eval("ClientTopicID7").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID7").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medicare Prescription Drug Coverage (Part D) - Marketing/Sales Complaints or Issues</td>
					            <td  width="10%"><%# Eval("ClientTopicID8").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID8").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medicare Prescription Drug Coverage (Part D) - Quality of Care</td>
					            <td  width="10%"><%# Eval("ClientTopicID9").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID9").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medicare Prescription Drug Coverage (Part D) - Plan Non-Renewal</td>
					            <td  width="10%"><%# Eval("ClientTopicID10").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID10").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medicare Prescription Drug Coverage (Part D) - Old Plan Eligibility, Benefits Comparisons</td>
					            <td  width="10%"><%# Eval("ClientTopicID62").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID62").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class=TDBorder>Medicare Prescription Drug Coverage (Part D) - Old Appeals, Quality of Care, Complaints</td>
					            <td  width="10%" class=TDBorder><%# Eval("ClientTopicID63").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID63").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            
				              <tr vAlign="baseline">
					            <td >Part D Low Income Subsidy (LIS/Extra Help) - Eligibility/Screening</td>
					            <td  width="10%"><%# Eval("ClientTopicID11").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID11").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Part D Low Income Subsidy (LIS/Extra Help) - Benefit Explanation</td>
					            <td  width="10%"><%# Eval("ClientTopicID12").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID12").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Part D Low Income Subsidy (LIS/Extra Help) - Application Assistance</td>
					            <td  width="10%"><%# Eval("ClientTopicID13").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID13").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Part D Low Income Subsidy (LIS/Extra Help) - Claims/Billing</td>
					            <td  width="10%"><%# Eval("ClientTopicID14").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID14").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Part D Low Income Subsidy (LIS/Extra Help) - Appeals/Grievances</td>
					            <td  width="10%"><%# Eval("ClientTopicID15").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID15").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class=TDBorder>Part D Low Income Subsidy (LIS/Extra Help) - Old Low Income Assist - Eligibil, Benefit Comp</td>
					            <td  width="10%" class=TDBorder><%# Eval("ClientTopicID64").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID64").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Other Prescription Assistance - Union/Employer Plan</td>
					            <td  width="10%"><%# Eval("ClientTopicID16").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID16").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Other Prescription Assistance - Military Drug Benefits</td>
					            <td  width="10%"><%# Eval("ClientTopicID17").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID17").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Other Prescription Assistance - Manufacturer Programs</td>
					            <td  width="10%"><%# Eval("ClientTopicID18").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID18").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Other Prescription Assistance - State Pharmaceutical Assistance Programs</td>
					            <td  width="10%"><%# Eval("ClientTopicID19").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID19").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Other Prescription Assistance - Other</td>
					            <td  width="10%"><%# Eval("ClientTopicID20").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID20").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Other Prescription Assistance - Old Medicare-Approved Drug Discount Card</td>
					            <td  width="10%"><%# Eval("ClientTopicID65").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID65").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class=TDBorder>Other Prescription Assistance - Old Discount Plans</td>
					            <td  width="10%" class=TDBorder><%# Eval("ClientTopicID66").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID66").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Medicare (Parts A & B) - Eligibility</td>
					            <td  width="10%"><%# Eval("ClientTopicID21").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID21").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare (Parts A & B) - Benefit Explanation</td>
					            <td  width="10%"><%# Eval("ClientTopicID22").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID22").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Medicare (Parts A & B) - Claims/Billing</td>
					            <td  width="10%"><%# Eval("ClientTopicID23").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID23").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medicare (Parts A & B) - Appeals/Grievances</td>
					            <td  width="10%"><%# Eval("ClientTopicID24").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID24").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medicare (Parts A & B) - Fraud and Abuse</td>
					            <td  width="10%"><%# Eval("ClientTopicID25").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID25").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medicare (Parts A & B) - Quality of Care</td>
					            <td  width="10%"><%# Eval("ClientTopicID26").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID26").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medicare (Parts A & B) - Old Enrolment, Eligibility, Benefits</td>
					            <td  width="10%"><%# Eval("ClientTopicID60").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID60").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class=TDBorder>Medicare (Parts A & B) - Old Appeal-Quality of Care-Complaints</td>
					            <td  width="10%" class=TDBorder><%# Eval("ClientTopicID61").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID61").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Eligibility/Screening</td>
					            <td  width="10%"><%# Eval("ClientTopicID27").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID27").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Benefit Explanation</td>
					            <td  width="10%"><%# Eval("ClientTopicID28").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID28").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Plans Comparison</td>
					            <td  width="10%"><%# Eval("ClientTopicID29").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID29").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Plan Enrollment/Disenroll</td>
					            <td  width="10%"><%# Eval("ClientTopicID30").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID30").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Claims/Billing</td>
					            <td  width="10%"><%# Eval("ClientTopicID31").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID31").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Appeals/Grievances</td>
					            <td  width="10%"><%# Eval("ClientTopicID32").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID32").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Fraud and Abuse</td>
					            <td  width="10%"><%# Eval("ClientTopicID33").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID33").EncodeHtml()),Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml())) %>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Marketing/Sales Complaints </td>
					            <td  width="10%"><%# Eval("ClientTopicID34").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID34").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Quality of Care</td>
					            <td  width="10%"><%# Eval("ClientTopicID35").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID35").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost) - Plan Non-Renewal</td>
					            <td  width="10%"><%# Eval("ClientTopicID36").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID36").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Medicare Advantage - Old Enrollment, Disenrollment, Eligibility, Comparisons</td>
					            <td  width="10%"><%# Eval("ClientTopicID67").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID67").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td class=TDBorder>Medicare Advantage - Old Appeals - Quality of Care - Complaints</td>
					            <td  width="10%" class=TDBorder><%# Eval("ClientTopicID68").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID68").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Supplement/SELECT - Eligibility/Screening</td>
					            <td  width="10%"><%# Eval("ClientTopicID37").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID37").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Supplement/SELECT - Benefit Explanation</td>
					            <td  width="10%"><%# Eval("ClientTopicID38").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID38").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Supplement/SELECT - Plans Comparison</td>
					            <td  width="10%"><%# Eval("ClientTopicID39").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID39").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Supplement/SELECT - Claims/Billing</td>
					            <td  width="10%"><%# Eval("ClientTopicID40").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID40").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Supplement/SELECT - Appeals/Grievances</td>
					            <td  width="10%"><%# Eval("ClientTopicID41").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID41").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Supplement/SELECT - Fraud and Abuse</td>
					            <td  width="10%"><%# Eval("ClientTopicID42").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID42").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Medicare Supplement/SELECT - Marketing/Sales Complaints or Issues</td>
					            <td  width="10%"><%# Eval("ClientTopicID43").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID43").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Supplement/SELECT  - Quality of Care</td>
					            <td  width="10%"><%# Eval("ClientTopicID44").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID44").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Supplement/SELECT - Plan Non-Renewal</td>
					            <td  width="10%"><%# Eval("ClientTopicID45").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID45").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Supplement/SELECT - Old Enrollment, Eligibility, Comparisons</td>
					            <td  width="10%"><%# Eval("ClientTopicID70").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID70").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Supplement/SELECT - Old Change Coverage</td>
					            <td  width="10%"><%# Eval("ClientTopicID71").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID71").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td class=TDBorder>Medicare Supplement/SELECT - Old Claims or Appeals</td>
					            <td  width="10%" class=TDBorder><%# Eval("ClientTopicID72").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID72").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicaid - Medicare Savings Programs (MSP) Screening (QMB, SLMB, QI)</td>
					            <td  width="10%"><%# Eval("ClientTopicID46").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID46").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicaid - MSP Application Assistance</td>
					            <td  width="10%"><%# Eval("ClientTopicID47").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID47").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Medicaid - Medicaid (SSI, Nursing Home, MEPD, Elderly Waiver) Screening</td>
					            <td  width="10%"><%# Eval("ClientTopicID48").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID48").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicaid - Medicaid Application Assistance</td>
					            <td  width="10%"><%# Eval("ClientTopicID49").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID49").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicaid - Medicaid/QMB Claims</td>
					            <td  width="10%"><%# Eval("ClientTopicID50").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID50").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicaid - Fraud and Abuse</td>
					            <td  width="10%"><%# Eval("ClientTopicID51").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID51").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td class=TDBorder>Medicaid - Old Other Medicaid</td>
					            <td  width="10%" class=TDBorder><%# Eval("ClientTopicID69").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID69").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Other Topics - Long Term Care (LTC) Insurance</td>
					            <td  width="10%"><%# Eval("ClientTopicID52").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID52").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Other Topics - LTC Partnership</td>
					            <td  width="10%"><%# Eval("ClientTopicID53").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID53").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Other Topics - LTC Other</td>
					            <td  width="10%"><%# Eval("ClientTopicID54").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID54").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Other Topics - Military Health Benefits</td>
					            <td  width="10%"><%# Eval("ClientTopicID55").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID55").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Other Topics - Employer/Federal Employee Health Benefits (FEHB)</td>
					            <td  width="10%"><%# Eval("ClientTopicID56").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID56").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Other Topics - COBRA</td>
					            <td  width="10%"><%# Eval("ClientTopicID57").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID57").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Other Topics - Other Health Insurance</td>
					            <td  width="10%"><%# Eval("ClientTopicID58").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID58").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Other Topics - Other</td>
					            <td  width="10%"><%# Eval("ClientTopicID59").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID59").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Other Topics - Old Fraud and Abuse</td>
					            <td  width="10%"><%# Eval("ClientTopicID73").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID73").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td class=TDBorder>Other Topics - Old Customer Service Issues or Complaints</td>
					            <td  width="10%" class=TDBorder><%# Eval("ClientTopicID74").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientTopicID74").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Contacts in Which the Total Time Spent With or On Behalf of Client Was 1 - 9 Minutes</td>
					            <td  width="10%"><%# Eval("ClientContactHoursSpent1").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactHoursSpent1").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Contacts in Which the Total Time Spent With or On Behalf of Client Was 10 - 29 Minutes</td>
					            <td  width="10%"><%# Eval("ClientContactHoursSpent2").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactHoursSpent2").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Contacts in Which the Total Time Spent With or On Behalf of Client Was 30 - 59 Minutes</td>
					            <td  width="10%"><%# Eval("ClientContactHoursSpent3").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactHoursSpent3").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Contacts in Which the Total Time Spent With or On Behalf of Client Was 60 or More Minutes</td>
					            <td  width="10%"><%# Eval("ClientContactHoursSpent4").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactHoursSpent4").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Contacts in Which Time Spent Missing, Blank, Null, Not Collected</td>
					            <td  width="10%" ><%# Eval("ClientContactHoursSpent5").EncodeHtml()%></td>
					            <td width="10%" ><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactHoursSpent5").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				             <tr vAlign="baseline">
					            <td class="TDBorder">Total Time Spent With or On Behalf of Client Across All Contacts [Hours]</td>
					            <td  width="10%" class="TDBorder"><%# Eval("ClientContactHoursSpent6").EncodeHtml()%></td>
					            <td width="10%" class="TDBorder">&nbsp;</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Contacts Described as General Information and Referral</td>
					            <td  width="10%"><%# Eval("ClientContactCurrentStatus1").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactCurrentStatus1").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Contacts Described as Detailed Assistance - In Progress</td>
					            <td  width="10%"><%# Eval("ClientContactCurrentStatus2").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactCurrentStatus2").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Contacts Described as Detailed Assistance - Fully Completed</td>
					            <td  width="10%"><%# Eval("ClientContactCurrentStatus3").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactCurrentStatus3").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Contacts Described as Problem Solving / Problem Resolution - In Progress</td>
					            <td  width="10%"><%# Eval("ClientContactCurrentStatus4").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactCurrentStatus4").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            
				              <tr vAlign="baseline">
					            <td >Contacts Described as Problem Solving / Problem Resolution - Fully Completed</td>
					            <td  width="10%"><%# Eval("ClientContactCurrentStatus5").EncodeHtml()%></td>
					            <td width="10%"><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactCurrentStatus5").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
				            
				              <tr vAlign="baseline">
					            <td class=TDBorder>Contact Description-Status Missing, Blank, Not Collected, Invalid, Miscoded, Out of Range</td>
					            <td  width="10%" class=TDBorder><%# Eval("ClientContactCurrentStatus6").EncodeHtml()%></td>
					            <td width="10%" class=TDBorder><%# GetDistribution(Convert.ToDecimal(Eval("ClientContactCurrentStatus6").EncodeHtml()), Convert.ToDecimal(Eval("TotalClientContacts").EncodeHtml()))%>%</td>
				            </tr>
		                </table>
		              </div>
		            </div>
                </ItemTemplate>
            </asp:FormView>
            </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceViewCCSummary" OnSelecting="dataSourceViewCCSummary_Selecting" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.ViewCCSummaryReportViewData" />
</asp:Content>
        
