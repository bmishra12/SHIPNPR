<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PAMSummaryReportPreview.aspx.cs" Inherits="ShiptalkWeb.ShipNPR_Reports.PAMSummaryReportPreview" %>

<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects.UI" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls"
    TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="pp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>NPR Client Contact Reports Preview</title>
</head>
<body>    
    <form id="form1" runat="server">
    <div id="maincontentwide" width="100%">
    <table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td  width="100%">State Health Insurance Assistance Program - 
                        SHIP - National Performance Report - NPR
					</td>
				</tr>
				<tr>
					<td  align="left" width="100%"><asp:label id="strSubTitle" runat="server">Public and Media Events and Activities Summary Report</asp:label><br>
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
				<tr runat="server" id="trCountyOfActivityEvent" visible="false">
					<td  align="left" width="100%"><asp:label id="strCountyOfActivityEvent" runat="server" ></asp:label></td>
				</tr>
					<tr runat="server" id="trZipCodeOfActivityEvent" visible="false">
					<td  align="left" width="100%"><asp:label id="strZipCodeOfActivityEvent" runat="server" ></asp:label></td>
				</tr>
    			<tr runat="server" id="trContactsBySubStateRegionOnAgency" visible="false">
					<td  align="left" width="100%"><asp:label id="strContactsBySubStateRegionOnAgency" runat="server" ></asp:label></td>
				</tr>
					<tr runat="server" id="trContactsBySubStateRegionOnCountiesOfActivityEvent" visible="false">
					<td  align="left" width="100%"><asp:label id="strContactsBySubStateRegionOnCountiesOfActivityEvent" runat="server" ></asp:label></td>
				</tr>
				<tr runat="server" id="trPresenterContributor" visible="false">
					<td  align="left" width="100%"><asp:label id="strPresenterContributor" runat="server" ></asp:label></td>
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
			
		    <div class="dv5col">
		    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
            <asp:FormView runat="server" ID="formView" DataSourceID="dataSourceViewPAMSummary"  Width="100%">
                <ItemTemplate>
                   <div id="dv3colFormContent" >
                        <table id="tblsearchResults" class="TableBorder" cellspacing="1" border="1" cellpadding="1" width="100%" align="center" runat="server">
				            <tr valign="baseline">
					            <td width="80%" class="TableHeaderBorder1">
					                <strong>Events-Activities by Type, Persons Reached-Enrolled, Time Spent, Topics Discussed, Target Audiences</strong></td>
					            <td  width="10%" class="TableHeaderBorder1">&nbsp;
					            </td>
				            </tr>
				           <tr valign="baseline">
					            <td class="TDBorder">Total Events and Activities</td>
					            <td  width="10%" class="TDBorder"> <%# Eval("TotalEventsAndActivities").EncodeHtml()%> </td>
				            </tr>
				            <tr valign="baseline">
					            <td >Interactive Presentations to Public - Face to Face In-Person - Number of Events</td>
					            <td  width="10%"><%# Eval("InteractivePresentationstoPublicFacetoFaceInPersonNumberofEvents").EncodeHtml()%></td>
				            </tr>
				            <tr valign="baseline">
					            <td >Interactive Presentations to Public - Estimated Number of Attendees</td>
					            <td  width="10%"><%# Eval("InteractivePresentationstoPublicEstimatedNumberofAttendees").EncodeHtml()%></td>
				            </tr>
				            <tr vAlign="baseline">
					            <td class="TDBorder">Interactive Presentations to Public - Estimated Persons Provided Enrollment Assistance</td>
					            <td  width="10%" class="TDBorder"><%# Eval("InteractivePresentationstoPublicEstimatedPersonsProvidedEnrollmentAssistance").EncodeHtml()%></td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Booth or Exhibit At Heath Fair, Senior Fair, or Special Event - Number of Events</td>
					            <td  width="10%"><%# Eval("BoothorExhibitAtHeathFairSeniorFairorSpecialEventNumberofEvents").EncodeHtml()%></td>
					            
				            </tr>
				             <tr vAlign="baseline">
					            <td >Booth or Exhibit - Estimated Number of Direct Interactions with Attendees</td>
					            <td  width="10%"><%# Eval("BoothorExhibitEstimatedNumberofDirectInteractionswithAttendees").EncodeHtml()%></td>
					            
				            </tr>
				             <tr vAlign="baseline">
					            <td class="TDBorder">Booth or Exhibit - Estimated Persons Provided Enrollment Assistance</td>
					            <td  width="10%" class="TDBorder"><%# Eval("BoothorExhibitEstimatedPersonsProvidedEnrollmentAssistance").EncodeHtml()%></td>
					            
				            </tr>
				             <tr vAlign="baseline">
					            <td >Dedicated Enrollment Event Sponsored By SHIP or in Partnership - Number of Events</td>
					            <td  width="10%"><%# Eval("DedicatedEnrollmentEventSponsoredBySHIPOrInPartnershipNumberofEvents").EncodeHtml()%></td>
					            
				            </tr>
				              <tr vAlign="baseline">
					            <td >Dedicated Enrollment Event - Est Number Persons Reached at Event Regardless of Enroll Assistance</td>
					            <td  width="10%"><%# Eval("DedicatedEnrollmentEventEstNumberPersonsReachedatEventRegardlessofEnrollAssistance").EncodeHtml()%></td>
					            
				            </tr>
				             <tr vAlign="baseline">
					            <td >Dedicated Enrollment Event - Estimated Number Persons Provided Any Enrollment Assistance</td>
					            <td  width="10%"><%# Eval("DedicatedEnrollmentEventEstimatedNumberPersonsProvidedAnyEnrollmentAssistance").EncodeHtml()%></td>
					            
				            </tr>
				             <tr vAlign="baseline">
					            <td >Dedicated Enrollment Event - Estimated Number Provided Enrollment Assistance with Part D</td>
					            <td  width="10%"><%# Eval("DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithPartD").EncodeHtml()%></td>
					            
				            </tr>
				              <tr vAlign="baseline">
					            <td >Dedicated Enrollment Event - Estimated Number Provided Enrollment Assistance with LIS</td>
					            <td  width="10%"><%# Eval("DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithLIS").EncodeHtml()%></td>
					            
				            </tr>
				              <tr vAlign="baseline">
					            <td >Dedicated Enrollment Event - Estimated Number Provided Enrollment Assistance with MSP</td>
					            <td  width="10%"><%# Eval("DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistanceWithMSP").EncodeHtml()%></td>
					            
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">Dedicated Enrollment Event - Estimated Number Provided Enrollment Assist Other Medicare Program</td>
					            <td  width="10%" class="TDBorder"><%# Eval("DedicatedEnrollmentEventEstimatedNumberProvidedEnrollmentAssistOtherMedicareProgram").EncodeHtml()%></td>
					            
				            </tr>
				            <tr vAlign="baseline">
					            <td >Radio Show Live or Taped - Not a Public Service Announce or Ad - Number of Events</td>
					            <td  width="10%"><%# Eval("RadioShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents").EncodeHtml()%></td>
					            
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">Radio Show Live or Taped - Estimated Number of Listeners Reached</td>
					            <td  width="10%" class="TDBorder"><%# Eval("RadioShowLiveorTapedEstimatedNumberofListenersReached").EncodeHtml()%></td>
					            
				            </tr>
				              <tr vAlign="baseline">
					            <td >TV or Cable Show Live or Taped - Not a Public Service Announce or Ad - Number of Events</td>
					            <td  width="10%"><%# Eval("TVorCableShowLiveorTapedNotaPublicServiceAnnounceorAdNumberofEvents").EncodeHtml()%></td>
                                                            
				            </tr>
				             <tr vAlign="baseline">
					            <td class="TDBorder">TV or Cable Show Live or Taped - Estimated Number of Viewers Reached</td>
					            <td  width="10%" class="TDBorder"><%# Eval("TVorCableShowLiveorTapedEstimatedNumberofViewersReached").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Electronic Other Activity - PSAs, Electronic Ads, Crawls, Video Conf, Web Conf, Web Chat - Events</td>
					            <td  width="10%"><%# Eval("ElectronicOtherActivityPSAsElectronicAdsCrawlsVideoConfWebConfWebChatEvents").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">Electronic Other Activity - Est Persons Viewing or Listening to Electronic Other Activity Across Campaign</td>
					            <td  width="10%" class="TDBorder"><%# Eval("ElectronicOtherActivityEstPersonsViewingorListeningtoElectronicOtherActivityAcrossCampaign").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Print Other Activity - Newspaper, Newsletter, Pamphlets, Fliers, Posters, Targeted Mailings - Events</td>
					            <td  width="10%"><%# Eval("PrintOtherActivityNewspaperNewsletterPamphletsFliersPostersTargetedMailingsEvents").EncodeHtml()%></td>
				            </tr>
				            <tr vAlign="baseline">
					            <td class="TDBorder">Print Other Activity - Est Persons Reading or Receiving Printed Materials Across Entire Campaign</td>
					            <td  width="10%" class="TDBorder"><%# Eval("PrintOtherActivityEstPersonsReadingorReceivingPrintedMaterialsAcrossEntireCampaign").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to Interactive Presentations</td>
					            <td  width="10%"><%# Eval("NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoInteractivePresentations").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">Total Person-Hours of Effort Spent on Interactive Presentations to Public</td>
					            <td  width="10%" class="TDBorder"><%# Eval("TotalPersonHoursofEffortSpentonInteractivePresentationstoPublic").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to Booths and Exhibits</td>
					            <td  width="10%"><%# Eval("NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoBoothsandExhibits").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">Total Person-Hours of Effort Spent on Booths and Exhibits</td>
					            <td  width="10%" class="TDBorder"><%# Eval("TotalPersonHoursofEffortSpentonBoothsandExhibits").EncodeHtml()%></td>
				            </tr>
				            
				            <tr vAlign="baseline">
					            <td >Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to Enrollment Events</td>
					            <td  width="10%"><%# Eval("NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoEnrollmentEvents").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">Total Person-Hours of Effort Spent on Enrollment Events</td>
					            <td  width="10%" class="TDBorder"><%# Eval("TotalPersonHoursofEffortSpentonEnrollmentEvents").EncodeHtml()%></td>
				            </tr>
				            
				              <tr vAlign="baseline">
					            <td >Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to Radio Events</td>
					            <td  width="10%" ><%# Eval("NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoRadioEvents").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">Total Person-Hours of Effort Spent on Radio Events</td>
					            <td  width="10%" class="TDBorder"><%# Eval("TotalPersonHoursofEffortSpentonRadioEvents").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to Television Events</td>
					            <td  width="10%"><%# Eval("NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoTelevisionEvents").EncodeHtml()%></td>
				            </tr>
				            <tr vAlign="baseline">
					            <td class="TDBorder">Total Person-Hours of Effort Spent on Television Events</td>
					            <td  width="10%" class="TDBorder"><%# Eval("TotalPersonHoursofEffortSpentonTelevisionEvents").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to Electronic Other Activities</td>
					            <td  width="10%"><%# Eval("NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoElectronicOtherActivities").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td class="TDBorder">Total Person-Hours of Effort Spent on Electronic Other Activities</td>
					            <td  width="10%" class="TDBorder"><%# Eval("TotalPersonHoursofEffortSpentonElectronicOtherActivities").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to Print Other Activities</td>
					            <td  width="10%"><%# Eval("NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoPrintOtherActivities").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">Total Person-Hours of Effort Spent on Print Other Activities</td>
					            <td  width="10%" class="TDBorder"><%# Eval("TotalPersonHoursofEffortSpentonPrintOtherActivities").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Number of Unduplicated SHIP Staff and Affiliated Partners Contributing to All Events-Activities</td>
					            <td  width="10%"><%# Eval("NumberofUnduplicatedSHIPStaffandAffiliatedPartnersContributingtoAllEventsActivities").EncodeHtml()%></td>
				            </tr>
				            <tr vAlign="baseline">
					            <td class="TDBorder">Total Person-Hours of Effort Spent on All Events-Activities</td>
					            <td  width="10%" class="TDBorder"><%# Eval("TotalPersonHoursofEffortSpentonAllEventsActivities").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medicare Parts A and B [Topic Focus]</td>
					            <td  width="10%"><%# Eval("MedicarePartsAandBTopicFocus").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Plan Issues - Non-Renewal, Termination, Employer-COBRA</td>
					            <td  width="10%"><%# Eval("PlanIssuesNonRenewalTerminationEmployerCOBRA").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Long-Term Care</td>
					            <td  width="10%"><%# Eval("LongTermCare").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medigap - Medicare Supplements</td>
					            <td  width="10%"><%# Eval("MedigapMedicareSupplements").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medicare Fraud and Abuse</td>
					            <td  width="10%"><%# Eval("MedicareFraudandAbuse").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Medicare Prescription Drug Coverage - PDP / MA-PD</td>
					            <td  width="10%"><%# Eval("MedicarePrescriptionDrugCoveragePDPMAPD").EncodeHtml()%></td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Other Prescription Drug Coverage - Assistance</td>
					            <td  width="10%"><%# Eval("OtherPrescriptionDrugCoverageAssistance").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Medicare Advantage - Health Plans</td>
					            <td  width="10%"><%# Eval("MedicareAdvantageHealthPlans").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >QMB - SLMB - QI</td>
					            <td  width="10%"><%# Eval("QMBSLMBQI").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Other Medicaid</td>
					            <td  width="10%"><%# Eval("OtherMedicaid").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >General SHIP Program Information</td>
					            <td  width="10%"><%# Eval("GeneralSHIPProgramInformation").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Medicare Preventive Services</td>
					            <td  width="10%"><%# Eval("MedicarePreventiveServices").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Low-Income Assistance</td>
					            <td  width="10%"><%# Eval("LowIncomeAssistance").EncodeHtml()%></td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Dual Eligible with Mental Illness Mental Disability</td>
					            <td  width="10%"><%# Eval("DualEligiblewithMentalIllnessMentalDisability").EncodeHtml()%></td>
					        </tr>
				             <tr vAlign="baseline">
					            <td class="TDBorder">Volunteer Recruitment</td>
					            <td  width="10%" class="TDBorder"><%# Eval("VolunteerRecruitment").EncodeHtml()%></td>
				            </tr>
				            <tr valign="baseline">
					            <td >Partnership Recruitment</td>
					            <td  width="10%"><%# Eval("PartnershipRecruitment").EncodeHtml()%></td>
				            </tr>
				            <tr valign="baseline">
					            <td class="TDBorder">Other Topics</td>
					            <td  width="10%" class="TDBorder"><%# Eval("OtherTopics").EncodeHtml()%></td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Pre-Enrollees - Age 45-64 [Target Audience]</td>
					            <td  width="10%" ><%# Eval("MedicarePreEnrolleesAge4564TargetAudience").EncodeHtml()%></td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Medicare Beneficiaries</td>
					            <td  width="10%"><%# Eval("MedicareBeneficiaries").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Family Members Caregivers of Medicare Beneficiaries</td>
					            <td  width="10%"><%# Eval("FamilyMembersCaregiversofMedicareBeneficiaries").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Low-Income</td>
					            <td  width="10%" ><%# Eval("LowIncome").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Hispanic, Latino, or Spanish Origin</td>
					            <td  width="10%"><%# Eval("HispanicLatinoorSpanishOrigin").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >White, Non-Hispanic</td>
					            <td  width="10%"><%# Eval("WhiteNonHispanic").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Black or African-American</td>
					            <td  width="10%" ><%# Eval("BlackorAfricanAmerican").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >American Indian or Alaska Native</td>
					            <td  width="10%"><%# Eval("AmericanIndianorAlaskaNative").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Asian Indian</td>
					            <td  width="10%"><%# Eval("AsianIndian").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">Chinese</td>
					            <td  width="10%" class="TDBorder"><%# Eval("Chinese").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Filipino</td>
					            <td  width="10%"><%# Eval("Filipino").EncodeHtml()%></td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Japanese</td>
					            <td  width="10%"><%# Eval("Japanese").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">Korean</td>
					            <td  width="10%" class="TDBorder"><%# Eval("Korean").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Vietnamese</td>
					            <td  width="10%"><%# Eval("Vietnamese").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Native Hawaiian</td>
					            <td  width="10%"><%# Eval("NativeHawaiian").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Guamanian or Chamorro</td>
					            <td  width="10%"><%# Eval("GuamanianorChamorro").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Samoan</td>
					            <td  width="10%"><%# Eval("Samoan").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Other Asian</td>
					            <td  width="10%"><%# Eval("OtherAsian").EncodeHtml()%></td>
				            </tr>
				           <tr vAlign="baseline">
					            <td >Other Pacific Islander</td>
					            <td  width="10%"><%# Eval("OtherPacificIslander").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Some Other Race-Ethnicity</td>
					            <td  width="10%"><%# Eval("SomeOtherRaceEthnicity").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Disabled</td>
					            <td  width="10%"><%# Eval("Disabled").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Rural</td>
					            <td  width="10%"><%# Eval("Rural").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Employer-Related Groups</td>
					            <td  width="10%"><%# Eval("EmployerRelatedGroups").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Mental Health Professionals</td>
					            <td  width="10%"><%# Eval("MentalHealthProfessionals").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class=TDBorder>Social Work Professionals</td>
					            <td  width="10%" class=TDBorder><%# Eval("SocialWorkProfessionals").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Dual-Eligible Groups</td>
					            <td  width="10%"><%# Eval("DualEligibleGroups").EncodeHtml()%></td>
				            </tr>
				            <tr vAlign="baseline">
					            <td >Partnership Outreach</td>
					            <td  width="10%"><%# Eval("PartnershipOutreach").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Presentations to Groups in Languages Other Than English</td>
					            <td  width="10%"><%# Eval("PresentationstoGroupsinLanguagesOtherThanEnglish").EncodeHtml()%></td>
				            </tr>
				             <tr vAlign="baseline">
					            <td >Other Audiences</td>
					            <td  width="10%"><%# Eval("OtherAudiences").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td >Old Medicare Beneficiaries and/or Pre-Enrollees</td>
					            <td  width="10%"><%# Eval("OldMedicareBeneficiariesandorPreEnrollees").EncodeHtml()%></td>
					            
				            </tr>
				              <tr vAlign="baseline">
					            <td class=TDBorder>Old Asian</td>
					            <td  width="10%" class=TDBorder><%# Eval("OldAsian").EncodeHtml()%></td>
				            </tr>
				              <tr vAlign="baseline">
					            <td class="TDBorder">Old Native Hawaiian or Other Pacific Islander</td>
					            <td width="10%" class="TDBorder"><%# Eval("OldNativeHawaiianorOtherPacificIslander").EncodeHtml()%></td>
					            
				            </tr>
		                </table>
		            </div>
                </ItemTemplate>
            </asp:FormView>
            </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceViewPAMSummary" OnSelecting="dataSourceViewPAMSummary_Selecting" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.ViewPAMSummaryReportViewData" />
   </form>
</body>
</html>

        
