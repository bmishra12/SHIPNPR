<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CCFView.aspx.cs" Inherits="ShiptalkWeb.Npr.Forms.CCFView" MasterPageFile="~/ShiptalkWebWide.Master" %>

<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>View Client Contact</title>
    <link href="../../../css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
    

    <script type="text/javascript">

        function PrintThisPage() {
            var sOption = "toolbar=no,location=no,directories=yes,menubar=yes,";
            sOption += "scrollbars=yes,width=750,height=600,left=100,top=25";

            var sWinHTML = document.getElementById('contentforprint').innerHTML;

            var winprint = window.open("", "", sOption);
            winprint.document.open();
            winprint.document.write('<html><LINK href=../../../css/print.css rel=Stylesheet><body>');
            winprint.document.write('<h1>View Client Contact</h1>');
            winprint.document.write(sWinHTML);
            winprint.document.write('</body></html>');
            winprint.document.close();
            winprint.focus();
        }
 
 
    $(document).ready(function() {

        //Initialize the dialog to be used for delete confirmations.
        $("#confirmDelete").dialog({
            resizable: false,
            height: 240,
            autoOpen: false,
            modal: true,
            buttons: {
                "No": function() {
                    $(this).dialog('close');
                },
                "Yes": function() {
                    $(this).dialog('close');
                    __doPostBack($("[id$='_btnDelete']").attr("id").replace(/_/g, "$"), "");
                }
            }
        });

        $("[id$='_btnDelete']").click(function(e) {
            e.preventDefault();
            $('#confirmDelete').dialog('open');
        });

    });

    </script>

</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontentwide">
        <asp:Panel ID="panelSuccess" runat="server" Visible='<%# DisplaySuccessMessage() %>'>
            <h1>
                <asp:Literal ID="literalSuccessHeader" runat="server"  />
            </h1>
            <p>
                <asp:Literal ID="literalSuccessMessage" runat="server" Text='<%# GetSuccessMessage() %>' />
            </p>
                        <a runat="server" ID="AddSimilarClientContact" href='<%# RouteController.CcfAddSimilarContact(Id.GetValueOrDefault(0)) %>'>Add another contact for THIS client</a>
                                    <br />
            <a runat="server" ID="addInSameAgency" href='<%# RouteController.CcfAddInAgency(Id.GetValueOrDefault(0)) %>'>Add a contact for a NEW client in the SAME agency</a>
              <br />
            <a runat="server" href='<%# RouteController.CcfAdd() %>'>Add a contact for a NEW client in a DIFFERENT agency</a>

          

            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="panelForm" runat = "server">
            <div class="dv5col">
                <asp:FormView runat="server" ID="formViewClientContact" DefaultMode="ReadOnly" DataSourceID="dataSourceViewClientContact">
                    <ItemTemplate>
                        <h1>
                            View Client Contact</h1>
                        <div class="commands">
                            <a href="javascript:PrintThisPage();" >Printer Friendly Version</a> 
                            &nbsp;
                            <asp:Button runat="server" ID="buttonEditClientContact" Text="Edit" ToolTip="Edit this Client Contact." OnClick="buttonEditClientContact_Click" CssClass="formbutton1" />
                            &nbsp;
                           <%-- <asp:Button runat="server" ID="buttonAddSimilarClientContact" Text="New Contact" ToolTip="Add a new Contact using this Client." OnClick="buttonAddSimilarClientContact_Click" CssClass="formbutton1" />
                             &nbsp;--%>
                            <asp:Button runat="server" ID="btnDelete" Text="Delete"  OnClick="buttonDeleteClientContact_Click" CssClass="formbutton1" visible = '<%# isShowDelete %>'/>

                        </div>
                        
                        <div id="contentforprint">
                        <table style="width: 100%;">
                            <tbody>
                                <tr>
                                    <td>
                                        <table class="formTable" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <span class="tag">Submitted By:</span>&nbsp;
                                                        <%# Eval("SubmitterFirstName").EncodeHtml()%> <%# Eval("SubmitterLastName").EncodeHtml()%>&nbsp;
                                                        &nbsp;<span class="tag">On:</span>&nbsp;<%# Eval("CreatedDate", "{0:MM/dd/yyyy}").EncodeHtml()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <hr>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <h4>
                                                            Client Identifier
                                                        </h4>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 334px;">
                                                        <span class="tag">Client Identifier Used by Your Agency or State:</span> &nbsp;
                                                        <%# Eval("StateSpecificClientId").EncodeHtml().TrimEnd() %>
                                                    </td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="tag">Client Identifier Auto-Assigned by NPR:</span> &nbsp;
                                                        <%# Eval("AutoAssignedClientId").EncodeHtml().TrimEnd() %>
                                                    </td>
                                               
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <hr>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="formTable" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2">
                                                        <h4>
                                                            Client Name and Contact Information
                                                        </h4>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="tag">Client First Name:</span> &nbsp;
                                                        <%# Eval("ClientFirstName").EncodeHtml().TrimEnd() %>
                                                    </td>
                                                    <td>
                                                        <span class="tag">Representative First Name:</span> &nbsp;
                                                        <%# Eval("RepresentativeFirstName").EncodeHtml().TrimEnd() %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="tag">Client Last Name:</span> &nbsp;
                                                        <%# Eval("ClientLastName").EncodeHtml().TrimEnd() %>
                                                    </td>
                                                    <td>
                                                        <span class="tag">Representative Last Name:</span> &nbsp;
                                                        <%# Eval("RepresentativeLastName").EncodeHtml().TrimEnd() %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="tag">Client Phone Number:</span> &nbsp;
                                                        <%# Eval("ClientPhoneNumber").EncodeHtml().TrimEnd() %>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <hr>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="formTable" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <h4>
                                                            Client ZIP Code and County Code
                                                        </h4>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 236px;">
                                                        <span class="tag">ZIP Code of Client Residence:</span> &nbsp;
                                                        <%# Eval("ClientZIPCode").EncodeHtml().TrimEnd()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="tag">County of Client Residence:</span> &nbsp;
                                                        <%# Eval("ClientCountyName").EncodeHtml().TrimEnd()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <hr>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="formTable" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2">
                                                        <h4>
                                                            Counselor and Agency
                                                        </h4>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 375px;">
                                                        <span class="tag">Counselor:</span> &nbsp;
                                                        <%# Eval("CounselorFirstName").EncodeHtml().TrimEnd()%>&nbsp;<%# Eval("CounselorLastName").EncodeHtml().TrimEnd()%>
                                                    </td>
                                                    <td>
                                                        <span class="tag">County of Counselor Location:</span> &nbsp;
                                                        <%# Eval("CounselorCountyName").EncodeHtml().TrimEnd() %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="tag">Agency:</span> &nbsp;
                                                        <%# Eval("AgencyName").EncodeHtml().TrimEnd() %>
                                                    </td>
                                                    <td>
                                                        <span class="tag">ZIP Code of Counselor Location:</span> &nbsp;
                                                        <%# Eval("CounselorZIPCode").EncodeHtml().TrimEnd() %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <span class="tag">Date Of Contact:</span> &nbsp;
                                                        <%# Eval("DateOfContact", "{0:MM/dd/yyyy}").EncodeHtml()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <hr>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="formTable" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 375px;">
                                                        <span class="tag">First vs Continuing Contact:</span>
                                                    </td>
                                                    <td>
                                                        <span class="tag">How Did Client Learn About SHIP:</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                       <%# ((ClientFirstVsContinuingContact)Enum.Parse(typeof(ClientFirstVsContinuingContact), (Eval("ClientFirstVsContinuingContact").EncodeHtml()))).Description()%>                                                       
                                                    </td>
                                                    <td>
                                                       <%# ((ClientLearnedAboutSHIP)Enum.Parse(typeof(ClientLearnedAboutSHIP), (Eval("ClientLearnedAboutSHIP").EncodeHtml()))).Description()%> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <hr>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="formTable" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <span class="tag">Method of Contact:</span>
                                                    </td>
                                                    <td>
                                                        <span class="tag">Client Age Group:</span>
                                                    </td>
                                                    <td>
                                                        <span class="tag">Client Gender:</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%# ((ClientMethodOfContact)Enum.Parse(typeof(ClientMethodOfContact), (Eval("ClientMethodOfContact").EncodeHtml()))).Description()%> 
                                                    </td>
                                                    <td>
                                                       <%# ((ClientAgeGroup)Enum.Parse(typeof(ClientAgeGroup), (Eval("ClientAgeGroup").EncodeHtml()))).Description()%>
                                                        
                                                    </td>
                                                    <td>
                                                        <%# ((ClientGender)Enum.Parse(typeof(ClientGender), (Eval("ClientGender").EncodeHtml()))).Description()%>    
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <hr>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="formTable" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 375px;">
                                                        <span class="tag">Client Race-Ethnicity:</span>
                                                    </td>
                                                    <td>
                                                        <span class="tag">Client Primary Language Other Than English:</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Repeater ID="repeaterClientRaceDescriptions" runat="server" DataSource='<%# Eval("ClientRaceDescriptions") %>'>
                                                            <ItemTemplate>
                                                                <%# Eval("Value").EncodeHtml()%>
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                    <td>
                                                     <%# ((ClientPrimaryLanguageOtherThanEnglish)Enum.Parse(typeof(ClientPrimaryLanguageOtherThanEnglish), (Eval("ClientPrimaryLanguageOtherThanEnglish").EncodeHtml()))).Description()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <hr>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="formTable" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 375px;">
                                                        <span class="tag">Client Monthly Income:</span>
                                                    </td>
                                                    <td>
                                                        <span class="tag">Client Assets:</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%# ((ClientMonthlyIncome)Enum.Parse(typeof(ClientMonthlyIncome), (Eval("ClientMonthlyIncome").EncodeHtml()))).Description()%>
                                                    </td>
                                                    <td>
                                                         <%# ((ClientAssets)Enum.Parse(typeof(ClientAssets), (Eval("ClientAssets").EncodeHtml()))).Description()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <hr>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="formTable" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 375px;">
                                                        <span class="tag">Receiving or Applying for Social Security Disability or Medicare Disability:</span>
                                                    </td>
                                                    <td>
                                                        <span class="tag">Dual Eligible with Mental Illness / Mental Disability :</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                         <%# ((ClientReceivingSSOrMedicareDisability)Enum.Parse(typeof(ClientReceivingSSOrMedicareDisability), (Eval("ClientReceivingSSOrMedicareDisability").EncodeHtml()))).Description()%>
                                                    </td>
                                                    <td>
                                                       <%# ((ClientDualEligble)Enum.Parse(typeof(ClientDualEligble), (Eval("ClientDualEligble").EncodeHtml()))).Description()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <hr>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="formTable" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2">
                                                        <h4>
                                                            PRESCRIPTION DRUG ASSISTANCE</h4>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span class="tag">Medicare Prescription Drug Coverage (Part D):</span>
                                                        <br />
                                                        <asp:Repeater ID="repeaterMedicarePrescriptionDrugCoverageTopics" runat="server" DataSource='<%# Eval("MedicarePrescriptionDrugCoverageTopics") %>'>
                                                            <ItemTemplate>
                                                                <%# Eval("Value").EncodeHtml()%>
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <br />
                                                        <span class="tag">Part D Low Income Subsidy (LIS/Extra Help):</span>
                                                        <br />
                                                        <asp:Repeater ID="repeaterPartDLowIncomeSubsidyTopics" runat="server" DataSource='<%# Eval("PartDLowIncomeSubsidyTopics") %>'>
                                                            <ItemTemplate>
                                                                <%# Eval("Value").EncodeHtml()%>
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <br />
                                                        <span class="tag">Other Prescription Assistance:</span>
                                                        <br />
                                                        <asp:Repeater ID="repeaterOtherPrescriptionAssistanceTopics" runat="server" DataSource='<%# Eval("OtherPrescriptionAssistanceTopics") %>'>
                                                            <ItemTemplate>
                                                                <%# Eval("Value").EncodeHtml()%>
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <br />
                                                        <span class="tag">Other:</span>&nbsp;<%# Eval("OtherPrescriptionAssitanceSpecified").EncodeHtml().TrimEnd()%>
                                                        <br />
                                                        <br />
                                                        <span class="tag">MEDICARE (Parts A & B):</span>
                                                        <br />
                                                        <asp:Repeater ID="repeaterMedicarePartsAandBTopics" runat="server" DataSource='<%# Eval("MedicarePartsAandBTopics") %>'>
                                                            <ItemTemplate>
                                                                <%# Eval("Value").EncodeHtml()%>
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                    <td>
                                                        <span class="tag">Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost):</span>
                                                        <br />
                                                        <asp:Repeater ID="repeaterMedicareAdvantageTopics" runat="server" DataSource='<%# Eval("MedicareAdvantageTopics") %>'>
                                                            <ItemTemplate>
                                                                <%# Eval("Value").EncodeHtml()%>
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <br />
                                                        <span class="tag">Medicare Supplement/Select:</span>
                                                        <br />
                                                        <asp:Repeater ID="repeaterMedicareSupplementTopics" runat="server" DataSource='<%# Eval("MedicareSupplementTopics") %>'>
                                                            <ItemTemplate>
                                                                <%# Eval("Value").EncodeHtml()%>
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <br />
                                                        <span class="tag">Medicaid:</span>
                                                        <br />
                                                        <asp:Repeater ID="repeaterMedicaidTopics" runat="server" DataSource='<%# Eval("MedicaidTopics") %>'>
                                                            <ItemTemplate>
                                                                <%# Eval("Value").EncodeHtml()%>
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <br />
                                                        <span class="tag">Other:</span>
                                                        <br />
                                                        <asp:Repeater ID="repeaterOtherDrugTopics" runat="server" DataSource='<%# Eval("OtherDrugTopics") %>'>
                                                            <ItemTemplate>
                                                                <%# Eval("Value").EncodeHtml()%>
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <br>
                                                        <span class="tag">Other:</span>&nbsp;<%# Eval("OtherDrugTopicsSpecified").EncodeHtml().TrimEnd()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <hr />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="formTable" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 325px;">
                                                        <span class="tag">Total Time Spent on This Contact Date:</span>
                                                    </td>
                                                    <td>
                                                        <span class="tag">Status:</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%# Eval("HoursSpent").EncodeHtml().TrimEnd()%>&nbsp;<span class="tag">Hours</span>&nbsp;&nbsp;<%# Eval("MinutesSpent").EncodeHtml().TrimEnd()%>&nbsp;<span class="tag">Minutes</span>
                                                    </td>
                                                    <td>
                                                        <%# ((ClientStatus)Enum.Parse(typeof(ClientStatus), (Eval("ClientStatus").EncodeHtml()))).Description()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <hr>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class="formTable" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <span class="tag">Comments</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <%# Eval("Comments").EncodeHtml().TrimEnd()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <hr>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 50%;">
                                                        <h4>
                                                            CMS Special Use Fields</h4>
                                                    </td>
                                                    <td style="width: 50%;">
                                                        <h4>
                                                            State and Local Special Use Fields</h4>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top;">
                                                        <asp:Repeater ID="repeaterCMSSpecialUseFields" runat="server" DataSource='<%# Eval("CMSSpecialUseFields") %>'>
                                                            <ItemTemplate>
                                                                <span class="tag">
                                                                    <%# Eval("Name").EncodeHtml()%>:&nbsp;</span><%# Eval("Value").EncodeHtml()%>
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                    <td style="vertical-align: top;">
                                                        <asp:Repeater ID="repeaterStateSpecialUseFields" runat="server" DataSource='<%# Eval("StateSpecialUseFields") %>'>
                                                            <ItemTemplate>
                                                                <span class="tag">
                                                                    <%# Eval("Name").EncodeHtml()%>:&nbsp;</span><%# Eval("Value").EncodeHtml()%>
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <hr>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </div>
                    </ItemTemplate>
                </asp:FormView>
            </div>
        </asp:Panel>
    </div>
        <div id="confirmDelete" title="Confirm Delete" style="display: none;">
        <p>
            <strong>Are you sure? The delete action will delete this Client Contact record including all of this Client Contact's associated data</strong>
            <br />NOTE: The account will be purged and cannot be recovered in future. 
        </p>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceViewClientContact" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.ViewClientContactViewData" OnSelecting="dataSourceViewClientContact_Selecting" />
</asp:Content>
