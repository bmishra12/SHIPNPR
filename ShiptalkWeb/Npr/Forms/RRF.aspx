<%@ Page Language="C#" MasterPageFile="~/ShiptalkWebWide.Master" AutoEventWireup="true"
    CodeBehind="RRF.aspx.cs" Inherits="NPRRebuild.ShiptalkWeb.RRF.RRF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body1" runat="server">
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
                All Person Counts Should Reflect Active Counselors, Coordinators, Other Staff as of the End of Each Grant Year(31 March).
                The Unique Count of Counselors Attending Any Update Training During the Grant Year Cannot Exceed the Grand total Number of Counselors.
            </td>
        </tr>
        <tr><td colspan="2"><hr /></td></tr>
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
                            From:&nbsp;<asp:TextBox ID="txtFromDate" runat="server" Width="100px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            To:&nbsp;<asp:TextBox ID="txtToDate" runat="server" Width="100px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtStateCode" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtStateName" runat="server" Width="100px"></asp:TextBox>
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
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:TextBox ID="txtPersonCompletingReport" runat="server"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="txtTelephone" runat="server"></asp:TextBox>
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
                            <asp:TextBox ID="StateVolCoun" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherVolCoun" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalVolCoun" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            B. Number of SHIP-Paid Counselors
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="StateShipPdCoun" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherShipPdCoun" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalShipPdCoun" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            C. Number of In-Kind-Paid Counselors
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="StateInKindPdCoun" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherInKindPdCoun" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalInKindPdCoun" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Total Number of Counselors - A+B+C
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="StateTotalCoun" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherTotalCoun" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalTotalCoun" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            D. Volunteer Counselor Hours
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="StateVolCounHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherVolCounHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalVolCounHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            E. SHIP-Paid Counselor Hours
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="StateShipPdCounHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherShipPdCounHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalShipPdCounHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            F. In-Kind-Paid Counselors Hours
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="StateInKindPdHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherInKindPdHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalInKindPdHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Total Counselors Hours - D+E+F
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="StateTotalCounHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherTotalCounHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalTotalCounHrs" runat="server" Width="50px"></asp:TextBox>
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
                            <asp:TextBox ID="TotalUnPdCoord" runat="server" Width="50px"></asp:TextBox>
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
                            <asp:TextBox ID="TotalShipPdCoord" runat="server" Width="50px"></asp:TextBox>
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
                            <asp:TextBox ID="TotalInKindPdCoord" runat="server" Width="50px"></asp:TextBox>
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
                            <asp:TextBox ID="TotalCoord" runat="server" Width="50px"></asp:TextBox>
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
                            <asp:TextBox ID="TotalUnPdCoordHrs" runat="server" Width="50px"></asp:TextBox>
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
                            <asp:TextBox ID="TotalShipPdCoordHrs" runat="server" Width="50px"></asp:TextBox>
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
                            <asp:TextBox ID="TotalInKindPdCoordHrs" runat="server" Width="50px"></asp:TextBox>
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
                            <asp:TextBox ID="TotalCoordHrs" runat="server" Width="50px"></asp:TextBox>
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
                            <asp:TextBox ID="StateVolOtherStaff" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherVolOtherStaff" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalVolOtherStaff" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            B. Number of SHIP-Paid Other Staff
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="StateShipPdOtherStaff" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherShipPdOtherStaff" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalShipPdOtherStaff" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            C. Number of In-Kind-Paid Other Staff
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="StateInKindPdOtherStaff" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherInKindPdOtherStaff" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalInKindPdOtherStaff" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Total Number of Other Staff - A+B+C
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="StateTotalOtherStaff" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherTotalOtherStaff" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalTotalOtherStaff" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            D. Volunteer Other Staff Hours
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="StateVolOtherStaffHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherVolOtherStaffHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalVolOtherStaffHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            E. SHIP-Paid Other Staff Hours
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="StateShipPdOtherStaffHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherShipPdOtherStaffHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalShipPdOtherStaffHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            F. In-Kind-Paid Other Staff Hours
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="StateInKindPdOtherStaffHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherInKindPdOtherStaffHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalInKindPdOtherStaffHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            Total Other Staff Hours - D+E+F
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="StateTotalOtherStaffHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="OtherTotalOtherStaffHrs" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="TotalTotalOtherStaffHrs" runat="server" Width="50px"></asp:TextBox>
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
                            <asp:TextBox ID="InitTrainings" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="2">
                            B. Number of New SHIP Counselors Attinding Initial Trainings
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="InitTrainingsAttend" runat="server" Width="50px"></asp:TextBox>
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
                            <asp:TextBox ID="InitTrainingsTotalHrs" runat="server" Width="50px"></asp:TextBox>
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
                            <asp:TextBox ID="UpdtTrainings" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="2">
                            E. Number of SHIP Counselors Atteniding Update Trainings
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td valign="top">
                            <asp:TextBox ID="UpdtTrainingsAttend" runat="server" Width="50px"></asp:TextBox>
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
                            <asp:TextBox ID="UpdtTrainingsTotalHrs" runat="server" Width="50px"></asp:TextBox>
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
                                        <asp:TextBox ID="YrsServiceLessThan1" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        1 Year Up to 3 Years
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="YrsService1To3" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        3 Years Up to 5 Years
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="YrsService3To5" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        More Tahn 5 Years
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="YrsServiceOver5" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Not Collected
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="YrsServiceNotCol" runat="server" Width="40px"></asp:TextBox>
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
                                        <asp:TextBox ID="AgeLessThan65" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        65 Years or Older
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="AgeOver65" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Not Collected
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="AgeNotCol" runat="server" Width="40px"></asp:TextBox>
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
                                        <asp:TextBox ID="GenderFemale" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Male
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="GenderMale" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Not Collected
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="GenderNotCol" runat="server" Width="40px"></asp:TextBox>
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
                                        <asp:TextBox ID="RaceHispanicLatSpa" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        White, Non-Hispanic
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RaceWhite" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Black, African American
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RaceAfAm" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        American Indian or Alaska Native
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RaceNative" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Asian Indian
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RaceAsian" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Chinese
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RaceChinese" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Filipino
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RaceFilipino" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Japanese
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RaceJapanese" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Korean
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RaceKorean" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Vietnamese
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RaceVietnamese" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Native Hawaiian
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RaceHawaiian" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Guamanian or Chamorro
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RaceGuamanian" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Samoan
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="VietnameseSamoan" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Other Asian
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="VietnameseOtrAsian" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Other Pacific Islander
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RacePacIslander" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Some Other Race - Ethnicity
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RaceOtherRace" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        More Than One Race - Ethnicity
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RaceMoreThanOne" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Not Collected
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="RaceNotCol" runat="server" Width="40px"></asp:TextBox>
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
                                        <asp:TextBox ID="DisabledTrue" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Not Disabled
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="DisabledFalse" runat="server" Width="40px"></asp:TextBox>
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
                                        <asp:TextBox ID="LangOther" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        English Speaker Only
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="LangEnglish" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Not Collected
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="LangNotCol" runat="server" Width="40px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
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
                <strong>PRA Disclosure Statement </strong>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                According to the Paperwork Reduction Act of 1995, no persons are required to respond
                to a collection of information unless it displays a valid OMB control number. The
                valid OMB control number for this information collection is <b>0938-0850</b>. The time
                required to complete this information collection is estimated to average <b>2 hours</b>
                per response, including the time to review instructions, search existing data resources,
                gather the data needed, and complete and review the information collection. If you
                have comments concerning the accuracy of the time estimate(s) or suggestions for
                improving this form, please write to: CMS, 7500 Security Boulevard, Attn: PRA Reports
                Clearance Officer, Mail Stop C4-26-05, Baltimore, Maryland 21244-1850.
            </td>
        </tr>
    </table>
</asp:Content>
