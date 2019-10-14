<%@ Page Title="" Language="C#" MasterPageFile="~/ShiptalkWebWide.Master" AutoEventWireup="true" CodeBehind="ResourceReportFView.aspx.cs" Inherits="ShiptalkWeb.Npr.Forms.ResourceReportFView" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" TagPrefix="pp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body1" runat="server">
<SCRIPT language="javascript">
      <!--
      function isNumberKey(evt)
      {
         var charCode = (evt.which) ? evt.which : event.keyCode
         if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
         return true;
      }
      //-->
   </SCRIPT>
   <asp:FormView runat="server" ID="formViewResourceReport" DefaultMode="ReadOnly" 
        DataSourceID="dataSourceViewResourceReport"  Width="100%" >
    <ItemTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="padding-left: 10px;
                padding-right: 10px;">
                <tr>
                    <td align="center" colspan="2">
                        <h1>
                            RESOURCE REPORT</h1>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Complete Only One RR Form for the Entire State. Do Not Submit Sponsoring-Agency-Level or Within-State-Regional Resource Reports.
                        All Person Counts Should Reflect Active Counselors, Coordinators, Other Staff as of the End of Each Grant Year &nbsp;(31 March).
                        
                    </td>
                </tr>
                <tr><td colspan="2">
                <div style="position:relative; float:right"><asp:Button ID="btnEdit" Text="Edit" Width="100px" Visible='<%# IsAdmin %>' CssClass="formbutton1" OnClick="btnEdit_Click" runat="server" /></div>
                <br />
                <br />
                <hr />
                 
                </td></tr>
                <tr>
                    <td valign="top" colspan="2">
                        <table width="100%">
                            <tr>
                                <td width="45%" colspan="2">
                                    <strong>12 Month Period for This Report</strong>
                                <td width="20%">
                                    <strong>State Code</strong>
                                </td>
                                <td width="35%">
                                    <strong>State Grantee Name</strong>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" width="20%">
                                    From:&nbsp;<asp:Label    ID="txtFromDate" runat="server" Text='<%#  Eval("RepYrFrom").EncodeHtml() %>' Width="100px"></asp:Label >
                                </td>
                                <td valign="top">
                                    To:&nbsp;<asp:Label    ID="txtToDate"  Text='<%#  Eval("RepYrTo").EncodeHtml() %>' runat="server" Width="100px"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateCode" runat="server" Width="50px" Text='<%# Eval("StateFIPSCode").EncodeHtml() %>'></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateGranteeName"   runat="server" Width="150px"  Text='<%#  Eval("StateGranteeName").EncodeHtml() %>' ></asp:Label >
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td valign="top" width="30%">
                                    <strong>Person Completing Report</strong>
                                </td>
                                <td valign="top" width="30%">
                                    <strong>Title</strong>
                                </td>
                                <td valign="top" width="40%">
                                    <strong>Telephone Number</strong>
                                    <br/>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label    ID="txtPersonCompletingReport" runat="server" Text='<%#  Eval("PersonCompletingReportName").EncodeHtml() %>' ></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label     ID="txtTitle" Text='<%#  Eval("Title").EncodeHtml() %>'   runat="server"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTelephone" runat="server" Text= '<%#  Eval("PersonCompletingReportTel").EncodeHtml() %>'></asp:Label >
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <strong>Section 1</strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <strong>Number of Active Counselors And Hours As of 31 March </strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td valign="top" width="40%">
                                </td>
                                <td valign="top" width="15%">
                                    State Office
                                </td>
                                <td valign="top" width="15%">
                                    All Other Local<br />
                                    and Field Sites
                                </td>
                                <td valign="top" width="30%">
                                    Total
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    A. Number of Volunteer Counselors
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateVolCoun" runat="server" Width="50px" Text='<%#  Eval("NoOfStateVolunteerCounselors").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherVolCoun" runat="server" Width="50px" Text='<%#  Eval("NoOfOtherVolunteerCounselors").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalVolCoun" runat="server" Width="50px" Text='<%#  Eval("TotalNumberOfVolCounselors").EncodeHtml() %>' onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    B. Number of SHIP-Paid Counselors
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateShipPdCoun" runat="server" Width="50px" Text='<%#  Eval("NoOfStateShipPaidCounselors").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherShipPdCoun" runat="server" Width="50px"  Text='<%#  Eval("NoOfOtherShipPaidCounselors").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalShipPdCoun" runat="server" Width="50px" Text='<%#  Eval("TotalNoOfShipPaidCounselors").EncodeHtml() %>'   onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    C. Number of In-Kind-Paid Counselors
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateInKindPdCoun" runat="server" Width="50px" Text='<%#  Eval("NoOfStateInKindPaidCounselors").EncodeHtml() %>'   onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherInKindPdCoun" runat="server" Width="50px" Text='<%#  Eval("NoOfOtherInKindPaidCounselors").EncodeHtml() %>' onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalInKindPdCoun" runat="server" Width="50px"  Text='<%#  Eval("TotalNoOfInKindPaidCounselors").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Total Number of Counselors - A+B+C
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateTotalCoun" runat="server" Width="50px"  Text='<%#  Eval("TotalNoOfCounselorsABCState").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherTotalCoun" runat="server" Width="50px"  Text='<%#  Eval("TotalNoOfCounselorsABCOther").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalTotalCoun" runat="server" Width="50px"  Text='<%#  Eval("TotalNoOfCounselorsABCStateAndOther").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    D. Volunteer Counselor Hours
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateVolCounHrs" runat="server" Width="50px" Text='<%#  Eval("NoOfStateVolunteerCounselorsHrs").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherVolCounHrs" runat="server" Width="50px"  Text='<%#  Eval("NoOfOtherVolunteerCounselorsHrs").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalVolCounHrs" runat="server" Width="50px"  Text='<%#  Eval("TotalNoOfCounselorHours").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    E. SHIP-Paid Counselor Hours
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateShipPdCounHrs" runat="server" Width="50px"  Text='<%#  Eval("NoOfStateShipPaidCounselorsHrs").EncodeHtml() %>'   onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherShipPdCounHrs" runat="server" Width="50px" Text='<%#  Eval("NoOfOtherShipPaidCounselorsHrs").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalShipPdCounHrs" runat="server" Width="50px"  Text='<%#  Eval("TotalShipPaidCounselorsHours").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    F. In-Kind-Paid Counselors Hours
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateInKindPdHrs" runat="server" Width="50px"  Text='<%#  Eval("NoOfStateInKindPaidCounselorsHrs").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherInKindPdHrs" runat="server" Width="50px"  Text='<%#  Eval("NoOfOtherInKindPaidCounselorsHrs").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalInKindPdHrs" runat="server" Width="50px"  Text='<%#  Eval("TotalInKindPdHrs").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Total Counselors Hours - D+E+F
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateTotalCounHrs" runat="server" Width="50px"  Text='<%#  Eval("TotalCounselorsHrsDEFState").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherTotalCounHrs" runat="server" Width="50px" Text='<%#  Eval("TotalCounselorsHrsDEFOther").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalTotalCounHrs" runat="server" Width="50px" Text='<%#  Eval("TotalCounselorsHrsDEFStateAndOther").EncodeHtml() %>' onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Section 2</strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Number of Local Coordinators / Sponsors and Hours As of 31 March</strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                                <td>
                                    Total
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    A. Number of Volunteer (Unpaid) Coordinators
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalUnPdCoord" runat="server" Width="50px" Text='<%#  Eval("NoOfUnpaidVolunteerCoordinators").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    B. Number of SHIP-Paid Coordinators
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalShipPdCoord" runat="server" Width="50px"  Text='<%#  Eval("NoOfSHIPPaidCoordinators").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    C. Number of In-Kind-Paid Coordinators
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalInKindPdCoord" runat="server" Width="50px"  Text='<%#  Eval("NoOfInKindPaidCoordinators").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Total Number of Coordinators - A+B+C
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalCoord" runat="server" Width="50px" Text='<%#  Eval("TotalNoOfCoordinators").EncodeHtml() %>'   onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    D. Volunteer (Unpaid) Coordinator Hours
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalUnPdCoordHrs" runat="server" Width="50px"  Text='<%#  Eval("NoOfUnpaidVolunteerCoordinatorsHrs").EncodeHtml() %>' onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    E. SHIP-Paid Coordinator Hours
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalShipPdCoordHrs" runat="server" Width="50px"  Text='<%#  Eval("NoOfSHIPPaidCoordinatorsHrs").EncodeHtml() %>'   onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    F. In-Kind-Paid Coordinator Hours
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalInKindPdCoordHrs" runat="server" Width="50px" Text='<%#  Eval("NoOfInKindPaidCoordinatorsHrs").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Total Coordinator Hours - D+E+F
                                </td>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalCoordHrs" runat="server" Width="50px"  Text='<%#  Eval("TotalCoordiantorsHrsDEF").EncodeHtml() %>'   onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Section 3</strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Number of Other Paid and Volunteer Staff And Hours As of 31 March </strong>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" width="40%">
                                </td>
                                <td valign="top" width="15%">
                                    State Office
                                </td>
                                <td valign="top" width="15%">
                                    All Other Local<br />
                                    and Field Sites
                                </td>
                                <td valign="top" width="30%">
                                    Total
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    A. Number of Volunteer Other Staff
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateVolOtherStaff" runat="server" Width="50px"  Text='<%#  Eval("NoOfStateVolunteerOtherStaff").EncodeHtml() %>'   onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherVolOtherStaff" runat="server" Width="50px" Text='<%#  Eval("NoOfOtherVolunteerOtherStaff").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalVolOtherStaff" runat="server" Width="50px"  Text='<%#  Eval("TotalNoOfOtherVolunteerOtherStaffStateAndOther").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    B. Number of SHIP-Paid Other Staff
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateShipPdOtherStaff" runat="server" Width="50px" Text='<%#  Eval("NoOfStateShipPaidOtherStaff").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherShipPdOtherStaff" runat="server" Width="50px" Text='<%#  Eval("NoOfOtherShipPaidOtherStaff").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalShipPdOtherStaff" runat="server" Width="50px"  Text='<%#  Eval("TotalNoOfShipPaidStateAndOtherStaff").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    C. Number of In-Kind-Paid Other Staff
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateInKindPdOtherStaff" runat="server" Width="50px"  Text='<%#  Eval("NoOfStateInKindPaidOtherStaff").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherInKindPdOtherStaff" runat="server" Width="50px" Text='<%#  Eval("NoOfOtherInKindPaidOtherStaff").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalInKindPdOtherStaff" runat="server" Width="50px"  Text='<%#  Eval("TotalNoOfOtherInKindPaidStateAndOtherStaff").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Total Number of Other Staff - A+B+C
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateTotalOtherStaff" runat="server" Width="50px" Text='<%#  Eval("TotalNoOfOtherInKindPaidStaffStateABC").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherTotalOtherStaff" runat="server" Width="50px"  Text='<%#  Eval("TotalNoOfOtherInKindPaidStaffOtherABC").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalTotalOtherStaff" runat="server" Width="50px"  Text='<%#  Eval("TotalNoOfOtherInKindPaidStaffStateAndOtherABC").EncodeHtml() %>' onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    D. Volunteer Other Staff Hours
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateVolOtherStaffHrs" runat="server" Width="50px" Text='<%#  Eval("NoOfStateVolunteerOtherStaffHrs").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherVolOtherStaffHrs" runat="server" Width="50px" Text='<%#  Eval("NoOfOtherVolunteerOtherStaffHrs").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalVolOtherStaffHrs" runat="server" Width="50px"  Text='<%#  Eval("TotalNoOfVolunteerOtherStaffHrsStateAndOther").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    E. SHIP-Paid Other Staff Hours
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateShipPdOtherStaffHrs" runat="server" Width="50px"  Text='<%#  Eval("NoOfStateShipPaidOtherStaffHrs").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherShipPdOtherStaffHrs" Text='<%#  Eval("NoOfOtherShipPaidOtherStaffHrs").EncodeHtml() %>'  runat="server" Width="50px"  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalShipPdOtherStaffHrs" runat="server" Width="50px" Text='<%#  Eval("TotalNoOfSHIPPaidStaffHrsStateAndOther").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    F. In-Kind-Paid Other Staff Hours
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateInKindPdOtherStaffHrs" runat="server" Width="50px" Text='<%#  Eval("NoOfStateInKindPaidOtherStaffHrs").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherInKindPdOtherStaffHrs" runat="server" Width="50px" Text='<%#  Eval("NoOfOtherInKindPaidOtherStaffHrs").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalInKindPdOtherStaffHrs" runat="server" Width="50px" Text='<%#  Eval("TotalNoOfOtherInKindPaidStateAndOtherHrs").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    Total Other Staff Hours - D+E+F
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtStateTotalOtherStaffHrs" runat="server" Width="50px"  Text='<%#  Eval("TotalNoOfOtherInKindPaidStateDEF").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtOtherTotalOtherStaffHrs" runat="server" Width="50px"  Text='<%#  Eval("TotalNoOfOtherInKindPaidOtherDEF").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtTotalTotalOtherStaffHrs" runat="server" Width="50px"  Text='<%#  Eval("TotalNoOfOtherInKindPaidOtherDEFStateAndOther").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Section 4</strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <strong>Counselor Trainings </strong>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                                <td>
                                    Total
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    A. Number of Initial Trainings for New SHIP Counselors
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtInitTrainings" runat="server" Width="50px" Text='<%#  Eval("NoOfInitialTrainings").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    B. Number of New SHIP Counselors Attending Initial Trainings
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtInitTrainingsAttend" runat="server" Width="50px" Text='<%#  Eval("NoOfInitialTrainingsAttend").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    C. Total Number of Counselor Hours in Initial Trainings
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtInitTrainingsTotalHrs" runat="server" Width="50px" Text='<%#  Eval("NoOfInitTrainingsTotalHrs").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    D. Number of Update Trainings for SHIP Counselors
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtUpdtTrainings" runat="server" Width="50px" Text='<%#  Eval("NoOfUpdateTrainings").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    E. Number of SHIP Counselors Attending Update Trainings
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtUpdtTrainingsAttend" runat="server" Width="50px"  Text='<%#  Eval("NoOfUpdateTrainingsAttend").EncodeHtml() %>' onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    F. Total Number of Counselor Hours in Update Trainings
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td valign="top">
                                    <asp:Label    ID="txtUpdtTrainingsTotalHrs" runat="server" Width="50px" Text='<%#  Eval("NoOfUpdateTrainingsTotalHrs").EncodeHtml() %>'   onkeypress="return isNumberKey(event)"></asp:Label >
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <strong>Section 5
                            <br />
                            <br />
                            Number of Total Active Counselors (SHIP-Paid, In-Kind-Paid and Volunteer Counselors)
                            with the Following Characteristics </strong>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" valign="top">
                        <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td valign="top" width="30%">
                                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td valign="top">
                                                <strong>Years of SHIP Service</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Less Than 1 Year
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtYrsServiceLessThan1" runat="server" Width="40px" Text='<%#  Eval("NoOfYrsServiceLessThan1").EncodeHtml() %>' onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                1 Year Up to 3 Years
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtYrsService1To3" runat="server" Width="40px" Text='<%#  Eval("NoOfYrsService1To3").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                3 Years Up to 5 Years
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtYrsService3To5" runat="server" Width="40px" Text='<%#  Eval("NoOfYrsService3To5").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                More Than 5 Years
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtYrsServiceOver5" runat="server" Width="40px" Text='<%#  Eval("NoOfYrsServiceOver5").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Not Collected
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtYrsServiceNotCol" runat="server" Width="40px" Text='<%#  Eval("NoOfYrsServiceNotCol").EncodeHtml() %>' onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Counselor Age</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Less Than 65 Years of Age
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtAgeLessThan65" runat="server" Width="40px" Text='<%#  Eval("NoOfAgeLessThan65").EncodeHtml() %>' onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                65 Years or Older
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtAgeOver65" runat="server" Width="40px" Text='<%#  Eval("NoOfAgeOver65").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Not Collected
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtAgeNotCol" runat="server" Width="40px" Text='<%#  Eval("NoOfAgeNotCollected").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Counselor Gender</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Female
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtGenderFemale" runat="server" Width="40px" Text='<%#  Eval("NoOfGenderFemale").EncodeHtml() %>'   onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Male
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtGenderMale" runat="server" Width="40px" Text='<%#  Eval("NoOfGenderMale").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Not Collected
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtGenderNotCol" runat="server" Width="40px" Text='<%#  Eval("NoOfGenderNotCollected").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" width="35%">
                                    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" style="padding-left: 15px;
                                        padding-right: 10px;">
                                        <tr>
                                            <td>
                                                <strong>Counselor Race - Ethnicity</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Hispanic, Latino, or Spanish Origin
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceHispanicLatSpa" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicityHispanic").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                White, Non-Hispanic
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceWhite" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicityWhite").EncodeHtml() %>'   onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Black, African American
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceAfAm" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicityAfricanAmerican").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                American Indian or Alaska Native
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceNative" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicityAmericanIndian").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Asian Indian
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceAsian" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicityAsianIndian").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Chinese
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceChinese" runat="server" Width="40px"  Text='<%#  Eval("NoOfEthnicityChinese").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Filipino
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceFilipino" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicityFilipino").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Japanese
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceJapanese" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicityJapanese").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Korean
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceKorean" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicityKorean").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Vietnamese
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceVietnamese" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicityVietnamese").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Native Hawaiian
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceHawaiian" runat="server" Width="40px"  Text='<%#  Eval("NoOfEthnicityNativeHawaiian").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Guamanian or Chamorro
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceGuamanian" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicityGuamanian").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Samoan
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtVietnameseSamoan" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicitySamoan").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Other Asian
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtVietnameseOtrAsian" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicityOtherAsian").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Other Pacific Islander
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRacePacIslander" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicityOthherPacificIslander").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Some Other Race - Ethnicity
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceOtherRace" runat="server" Width="40px"  Text='<%#  Eval("NoOfEthnicitySomeOtherRace").EncodeHtml() %>'   onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                More Than One Race - Ethnicity
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceMoreThanOne" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicityMoreThanOneRaceEthnicity").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Not Collected
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtRaceNotCol" runat="server" Width="40px" Text='<%#  Eval("NoOfEthnicityNotCollected").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" width="32%">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <strong>Counselor Disability</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Disabled
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtDisabledTrue" runat="server" Width="40px" Text='<%#  Eval("NoOfDisabled").EncodeHtml() %>' onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Not Disabled
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtDisabledFalse" runat="server" Width="40px" Text='<%#  Eval("NoOfNotDisabled").EncodeHtml() %>' onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Not Collected
                                            </td>
                                            <td valign="top">
                                                <asp:label   ID="txtCounselorDisabilityNotCollected" runat="server" Width="40px" Text='<%#  Eval("NoOfDisabledNotCollected").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <strong>Counselor Speaks Another Language</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Language Other than English
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtLangOther" runat="server" Width="40px" Text='<%#  Eval("NoOfSpeaksAnotherLanguageOtherThanEnglish").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                English Speaker Only
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtLangEnglish" runat="server" Width="40px" Text='<%#  Eval("NoOfEnglishSpeakerOnly").EncodeHtml() %>' onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                Not Collected
                                            </td>
                                            <td valign="top">
                                                <asp:Label    ID="txtLangNotCol" runat="server" Width="40px" Text='<%#  Eval("NoOfSpeaksAnotherLanguageNotCollected").EncodeHtml() %>'  onkeypress="return isNumberKey(event)"></asp:Label >
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="float:right">
                            <asp:Button ID="btnSave" Visible="false" CssClass="formbutton1" Width="105px" runat="server" 
                                Text="Save" onclick="btnSave_Click"/>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                    </td>
                </tr>
                
            </table>
        </ItemTemplate>
  </asp:FormView>

    <pp:ObjectContainerDataSource ID="dataSourceViewResourceReport" runat="server" 
        DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.ViewResourceReportViewData" 
        oninserted="dataSourceViewResourceReport_Inserted" 
        oninserting="dataSourceViewResourceReport_Inserting">
        
        </pp:ObjectContainerDataSource>
</asp:Content>
