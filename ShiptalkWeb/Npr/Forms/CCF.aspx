<%@ Page Title="" Language="C#" MasterPageFile="~/ShiptalkWebWide.Master" AutoEventWireup="True" CodeBehind="CCF.aspx.cs" Inherits="ShiptalkWeb.Npr.Forms.CCF" EnableEventValidation="false" %>

<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Register Assembly="ShiptalkWebControls" Namespace="ShiptalkWebControls" TagPrefix="air" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <%-- <base href="<%= BaseUrl %>" />--%>
    <title>Client Contact</title>
    <link href="../../../css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />


    <script src="../../../scripts/json2.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../../scripts/jquery-ui-mask-1.7.2.js"></script>

    <script type="text/javascript" src="../../../scripts/jquery.charcounter.js"></script>

    <script type="text/javascript">

        var commentExists = false;

        function toggleClientAgeGroupConstraint() {
            if ($("[id$='_radioButtonListClientAgeGroup'] :input:radio:checked")[0] == null) return;
         
            if ($("input#[id$='_radioButtonListClientAgeGroup_0']").attr('checked')) {
                $("[id$='_radioButtonListClientReceivingSSOrMedicareDisability'] :input:radio").attr('disabled', '');
            }
            else {

                $("[id$='_radioButtonListClientReceivingSSOrMedicareDisability'] :input:radio").attr('disabled', 'disabled');
                $("input#[id$='_radioButtonListClientReceivingSSOrMedicareDisability_1']").attr('checked', true).attr('disabled', '');
            }
        }

        function toggleStateSpecificClientIdInput() {
            if ($("[id$='_radioButtonUseStateSpecificClientId']").attr('checked')) {
                $("[id$='_textBoxStateSpecificClientId']").attr('disabled', '');
            }
            else {
                $("[id$='_textBoxStateSpecificClientId']").attr('disabled', 'disabled');
            }
        }

        function initialize(isPageLoad) {
            $('.datePicker').datepicker({ showButtonPanel: true,
                onSelect: function() { },
                onClose: function(dateText, inst) { $(this).focus(); },
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../../../css/images/calendar.gif',
                buttonImageOnly: true
            }).mask({ mask: '19/99/9999', allowPartials: true }).width(175);
            $('.phone').mask({ mask: '(999) 999-9999 x9999', allowPartials: true });
            $('.zip').mask({ mask: '99999', allowPartials: true });
            $('.hours').mask({ mask: '99', allowPartials: true });
            $('.minutes').mask({ mask: '99', allowPartials: true });
            $('.alphanum').mask({ mask: '*************************', allowPartials: true });
            $('.onlynum').mask({ mask: '9999999999999999999999999', allowPartials: true });

            if (!commentExists) {
                //Apply a char restrictor to comments.
                $('.CCcomment').charCounter(2000,
                {
                    container: "<div></div>",
                    classname: "counter"
                });
                
                commentExists = true;
            }

            $("[id$='_radioButtonListClientAgeGroup'] :input:radio").click(function() { toggleClientAgeGroupConstraint(); });
            $("[id$='_radioButtonUseStateSpecificClientId']").click(function() { toggleStateSpecificClientIdInput(); });
            $("[id$='_radioButtonUseAutoClientId']").click(function() { toggleStateSpecificClientIdInput(); });

            toggleClientAgeGroupConstraint();
            toggleStateSpecificClientIdInput();
        }

        function endRequest(sender, args) {
            $(document).ready(function() { initialize(false); });
        }

        function pageLoad() {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
            $(document).ready(function() { initialize(true); });
        }
        
    </script>

</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontentwide">
        <div class="dv5col">
            <h1>
                Add Client Contact</h1>
            <asp:Panel runat="server" ID="panelStateAndAgencyFilter">
                <p>
                    Start by selecting a State, and then the Agency you would like to add Client Contact information in.
                </p>
                <asp:Label runat="server" ID="labelState" AssociatedControlID="dropDownListState">State:</asp:Label>&nbsp;
                <asp:DropDownList runat="server" ID="dropDownListState" DataTextField="Value" DataValueField="Key" AppendDataBoundItems="true" SelectedValue='<%# DefaultState.StateAbbr %>' Enabled='<%# IsAdmin && Scope == Scope.CMS %>' AutoPostBack="true" OnSelectedIndexChanged="dropDownListState_SelectedIndexChanged">
                    <asp:ListItem Text="-- Select a State --" Value="CM" />
                </asp:DropDownList>
                <br />
                <br />
                <asp:Panel runat="server" ID="panelAgencyFilter" Visible="false">
                    <asp:Label runat="server" ID="labelAgency" AssociatedControlID="dropDownListAgency">Agency:</asp:Label>&nbsp;
                    <asp:DropDownList runat="server" ID="dropDownListAgency" DataTextField="Value" DataValueField="Key" AutoPostBack="true" OnSelectedIndexChanged="dropDownListAgency_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label runat="server" ID="labelNoStateAgenciesFound" CssClass="tag" Text="No Agencies exist for the State selected." />
                </asp:Panel>
            </asp:Panel>
            <asp:Panel runat="server" ID="panelForm" Visible="false">
                        <asp:FormView runat="server" ID="formViewAddClientContact" DefaultMode="Insert" DataSourceID="dataSourceAddClientContact" OnDataBound="formViewAddclientContact_DataBound">
                            <InsertItemTemplate>
                                <asp:HiddenField runat="server" id="hiddenStateFIPSCode" Value='<%# AgencyState.Code %>' />
                                <p>
                                    (Items marked in <span class="required">*</span> indicate required fields.)</p>
                                <asp:ValidationSummary ID = "ValidationSummaryHeader" runat = "server"
                                HeaderText = "Fix the following error(s) before you click submit. ">
                                </asp:ValidationSummary>
                                
                                <asp:CustomValidator runat="server" ID="validatorCheckDuplicateLastName" OnServerValidate="validatorCheckDuplicates_ServerValidate" ControlToValidate="textBoxDateOfContact" CssClass="required" ErrorMessage="This client already has a contact record for this date." />

                                <table class="formTable" style="width: 100%;">
                                    <tbody>
                                        <tr>
                                            <td colspan="2">
                                                <h4>
                                                    <span class="required">*</span>&nbsp;Client Identifier
                                                </h4>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 334px;">
                                                <asp:Label runat="server" ID="labelStateSpecificClientId" AssociatedControlID="textBoxStateSpecificClientId" CssClass="tag">Client Identifier Used by Your Agency or State:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="textBoxStateSpecificClientId" Text='<%# Bind("StateSpecificClientId") %>' Style="width: 200px;" MaxLength="50" TabIndex="1" />
                                                <br />
                                                <pp:PropertyProxyValidator ID="proxyValidatorStateSpecificClientId" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="StateSpecificClientId" ControlToValidate="textBoxStateSpecificClientId" RulesetName="Data" CssClass="required" Enabled="false" />
                                            </td>
                                            <td>
                                                <asp:RadioButton runat="server" ID="radioButtonUseStateSpecificClientId" Checked='<%# Bind("UseStateSpecificClientId") %>' Text="Use Agency or State Identifier" GroupName="ClientId" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="tag">Client Identifier Auto-Assigned by NPR:</span>
                                            </td>
                                            <td>
                                                <asp:Literal runat="server" ID="literalAutoAssignedClientId" Text='<%# AutoAssignedClientId %>' />
                                            </td>
                                            <td>
                                                <asp:RadioButton runat="server" ID="radioButtonUseAutoClientId" Checked="True" Text="Use Auto-Assigned Identifier" GroupName="ClientId" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <hr />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="formTable4col" style="width: 100%;">
                                    <tbody>
                                        <tr>
                                            <td colspan="4">
                                                <h4>
                                                    Client Name and Contact Information
                                                </h4>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="labelClientFirstName" AssociatedControlID="textBoxClientFirstName" CssClass="tag">Client First Name:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="textBoxClientFirstName" Text='<%# Bind("ClientFirstName") %>' MaxLength="50" TabIndex="2" />
                                                <br />
                                                <pp:PropertyProxyValidator ID="proxyClientFirstName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="ClientFirstName" ControlToValidate="textBoxClientFirstName" RulesetName="Data" CssClass="required" />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="labelRepresentativeFirstName" AssociatedControlID="textBoxRepresentativeFirstName" CssClass="tag">Representative First Name:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="textBoxRepresentativeFirstName" Text='<%# Bind("RepresentativeFirstName") %>' MaxLength="50" TabIndex="5" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="labelClientLastName" AssociatedControlID="textBoxClientLastName" CssClass="tag">Client Last Name:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="textBoxClientLastName" Text='<%# Bind("ClientLastName") %>' MaxLength="50" TabIndex="3" />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="labelRepresentativeLastName" AssociatedControlID="textBoxRepresentativeLastName" CssClass="tag">Representative Last Name:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="textBoxRepresentativeLastName" Text='<%# Bind("RepresentativeLastName") %>' MaxLength="50" TabIndex="6" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="labelClientPhoneNumber" AssociatedControlID="textBoxClientPhoneNumber" CssClass="tag">Client Phone Number:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="textBoxClientPhoneNumber" Text='<%# Bind("ClientPhoneNumber") %>' CssClass="phone" MaxLength="20" TabIndex="4" />
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <hr />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:UpdatePanel runat="server" ID="updatePanelClientZIPAndCounty">
                                    <ContentTemplate>
                                        <table class="formTable4col" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td colspan="4">
                                                        <h4>
                                                            Client ZIP Code and County
                                                        </h4>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 236px;">
                                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelClientZIPCode" AssociatedControlID="textBoxClientZIPCode" CssClass="tag">ZIP Code of Client Residence:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <air:TextBox runat="server" ID="textBoxClientZIPCode" Text='<%# Bind("ClientZIPCode") %>' CssClass="zip" MaxLength="5"  TabIndex="7" AutoPostBack="true" OnBlur="textBoxClientZIPCode_Blur" />
                                                        <br />
                                                        <pp:PropertyProxyValidator ID="proxyValidatorClientZIPCode" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="ClientZIPCode" ControlToValidate="textBoxClientZIPCode" RulesetName="Data" CssClass="required" />
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" ID="labelClientCountyCode" AssociatedControlID="dropDownClientCountyCode" CssClass="tag">County of Client Residence:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="dropDownClientCountyCode" Width="200px" DataTextField="Value" DataValueField="Key" TabIndex="8" >
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <hr />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                
                                
                                <asp:UpdatePanel runat="server" ID="updatePanelCounselorLocation">
                                    <ContentTemplate>
                                <table class="formTable4col" style="width: 100%;">
                                    <tbody>
                                        <tr>
                                            <td colspan="4">
                                                <h4>
                                                    Counselor and Agency
                                                </h4>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%;">
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelCounselor" AssociatedControlID="dropDownListCounselor" CssClass="tag">Counselor:</asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList runat="server" ID="dropDownListCounselor" Width="600px" DataTextField="Value" DataValueField="Key" DataSource='<%# Counselors %>' AppendDataBoundItems="true" SelectedValue='<%# Bind("CounselorUserId") %>' TabIndex="9" OnDataBound="dropDownListCounselor_DataBound" AutoPostBack="true" OnSelectedIndexChanged="dropDownListCounselor_SelectedIndexChanged"> 
                                                    <asp:ListItem Text="-- Select a Counselor --" Value="0" />
                                                </asp:DropDownList>
                                                <br />
                                                <pp:PropertyProxyValidator ID="proxyValidatorCounselor" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="CounselorUserId" ControlToValidate="dropDownListCounselor" RulesetName="Data" CssClass="required" />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="required">*</span>&nbsp;<span class="tag">Agency:</span>
                                            </td>
                                            <td>
                                                <%# AgencyName %>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 20%;">
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelCounselorCountyCode" AssociatedControlID="dropDownListCounselorCounty" CssClass="tag">County of Counselor Location:</asp:Label>
                                            </td>
                                            <td style="width: 25%;">
                                                <asp:DropDownList runat="server" ID="dropDownListCounselorCounty" DataTextField="Value" DataValueField="Key" TabIndex="10"  AutoPostback="true" AppendDataBoundItems="false" OnSelectedIndexChanged="dropDownListCounselorCounty_SelectedIndexChanged"  OnDataBound="dropDownListCounselorCounty_DataBound">
                                                    <asp:ListItem Text="-- Select a County --" Value="" />
                                                </asp:DropDownList>
                                                <br />
                                                <pp:PropertyProxyValidator ID="proxyValidatorCounselorCounty" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="CounselorCountyCode" ControlToValidate="dropDownListCounselorCounty" RulesetName="Data" CssClass="required" />
                                            </td>                                        
                                            <td>
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelCounselorZIPCode" AssociatedControlID="dropDownListCounselorZipCode" CssClass="tag">ZIP Code of Counselor Location:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="dropDownListCounselorZipCode" DataTextField="Value" DataValueField="Key" AppendDataBoundItems="false" TabIndex="12">
                                                    <asp:ListItem Text="-- Select a ZIP Code --" Value="" />
                                                </asp:DropDownList>
                                                <br />
                                                <pp:PropertyProxyValidator ID="proxyValidatorCounselorZipCode" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="CounselorZIPCode" ControlToValidate="dropDownListCounselorZipCode" RulesetName="Data" CssClass="required" />
                                            </td>                                        </tr>
                                    </tbody>
                                </table>
                                   </ContentTemplate>
                                </asp:UpdatePanel>
                                
                                <table class="formTable4col" style="width: 100%;">
                                    <tbody>
                                        <tr>
                                            <td style="width: 141px;">
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelDateOfContact" AssociatedControlID="textBoxDateOfContact" CssClass="tag">Date Of Contact:</asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox runat="server" ID="textBoxDateOfContact" Text='<%# Bind("DateOfContact") %>' CssClass="datePicker" TabIndex="13" />
                                                <br />
                                                 <asp:CustomValidator runat="server" ID="proxyValidatorDateOfContactUpperBound" OnServerValidate="validatorCheckUpperBound_ServerValidate" ControlToValidate="textBoxDateOfContact" CssClass="required" ErrorMessage="Date of Contact should be less than or equal to today's date." />
                                                <br />
                                                <pp:PropertyProxyValidator ID="proxyValidatorDateOfContact" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="DateOfContact" ControlToValidate="textBoxDateOfContact" RulesetName="Data" CssClass="required" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <hr />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="formTable" style="width: 100%;">
                                    <tbody>
                                        <tr>
                                            <td style="width: 375px;">
                                                <span class="required">*</span>&nbsp;<span class="tag">First vs Continuing Contact:</span>
                                            </td>
                                            <td>
                                                <span class="required">*</span>&nbsp;<span class="tag">How Did Client Learn About SHIP:</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <air:RadioButtonList ID="radioButtonListClientFirstVsContinuingContact" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# ClientFirstVsContinuingContact %>' SelectedNullableEnumValue='<%# Bind("ClientFirstVsContinuingContact") %>' TabIndex="14" />
                                                <pp:PropertyProxyValidator ID="proxyValidatorClientFirstVsContinuingContact" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="ClientFirstVsContinuingContact" ControlToValidate="radioButtonListClientFirstVsContinuingContact" RulesetName="Data" CssClass="required" />
                                            </td>
                                            <td>
                                                <air:RadioButtonList ID="radioButtonListClientLearnedAboutSHIP" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# ClientLearnedAboutSHIP %>' SelectedNullableEnumValue='<%# Bind("ClientLearnedAboutSHIP") %>' TabIndex="15" />
                                                <pp:PropertyProxyValidator ID="proxyValidatorClientLearnedAboutSHIP" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="ClientLearnedAboutSHIP" ControlToValidate="radioButtonListClientLearnedAboutSHIP" RulesetName="Data" CssClass="required" />
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
                                            <td style="width: 375px;">
                                                <span class="required">*</span>&nbsp;<span class="tag">Method of Contact:</span>
                                            </td>
                                            <td>
                                                <span class="required">*</span>&nbsp;<span class="tag">Client Age Group:</span>
                                            </td>
                                            <td>
                                                <span class="required">*</span>&nbsp;<span class="tag">Client Gender:</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <air:RadioButtonList ID="radioButtonListClientMethodOfContact" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# ClientMethodOfContact %>' SelectedNullableEnumValue='<%# Bind("ClientMethodOfContact") %>' TabIndex="16" />
                                                <pp:PropertyProxyValidator ID="proxyValidatorClientMethodOfContact" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="ClientMethodOfContact" ControlToValidate="radioButtonListClientMethodOfContact" RulesetName="Data" CssClass="required" />
                                            </td>
                                            <td>
                                                <air:RadioButtonList ID="radioButtonListClientAgeGroup" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# ClientAgeGroup %>' SelectedNullableEnumValue='<%# Bind("ClientAgeGroup") %>' TabIndex="17" />
                                                <pp:PropertyProxyValidator ID="proxyValidatorClientAgeGroup" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="ClientAgeGroup" ControlToValidate="radioButtonListClientAgeGroup" RulesetName="Data" CssClass="required" />
                                            </td>
                                            <td>
                                                <air:RadioButtonList ID="radioButtonListClientGender" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# ClientGender %>' SelectedNullableEnumValue='<%# Bind("ClientGender") %>' TabIndex="18" />
                                                <pp:PropertyProxyValidator ID="proxyValidatorClientGender" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="ClientGender" ControlToValidate="radioButtonListClientGender" RulesetName="Data" CssClass="required" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <hr />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table class="formTable" style="width: 100%;">
                                    <tbody>
                                        <tr>
                                            <td style="width: 375px;">
                                                <span class="required">*</span>&nbsp;<span class="tag">Client Race-Ethnicity:</span>
                                            </td>
                                            <td>
                                                <span class="required">*</span>&nbsp;<span class="tag">Client Primary Language Other Than English:</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <air:CheckBoxList ID="checkBoxListClientRaceDescriptions" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# ClientRaceDescription %>' SelectedItems='<%# Bind("ClientRaceDescriptions") %>' TabIndex="19" />
                                                <pp:PropertyProxyValidator ID="proxyValidatorClientRaceDescription" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="ClientRaceDescriptionsCount" ControlToValidate="checkBoxListClientRaceDescriptions" RulesetName="Data" CssClass="required" />
                                                
                                                   <asp:CustomValidator runat="server" ID="CustomValidator1" OnServerValidate="validatorCheckIfNotCollectedChecked_ServerValidate"
                                      ControlToValidate="checkBoxListClientRaceDescriptions" CssClass="required" ErrorMessage="When a race is selected, cannot also check 'Not Collected'" />
                                   
                                            </td>
                                            <td>
                                                <air:RadioButtonList ID="radioButtonListClientPrimaryLanguageOtherThanEnglish" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# ClientPrimaryLanguageOtherThanEnglish %>' SelectedNullableEnumValue='<%# Bind("ClientPrimaryLanguageOtherThanEnglish") %>' TabIndex="20" />
                                                <pp:PropertyProxyValidator ID="proxyValidatorClientPrimaryLanguageOtherThanEnglish" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="ClientPrimaryLanguageOtherThanEnglish" ControlToValidate="radioButtonListClientPrimaryLanguageOtherThanEnglish" RulesetName="Data" CssClass="required" />
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
                                            <td style="width: 375px;">
                                                <span class="required">*</span>&nbsp;<span class="tag">Client Monthly Income:</span>
                                            </td>
                                            <td>
                                                <span class="required">*</span>&nbsp;<span class="tag">Client Assets:</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <air:RadioButtonList ID="radioButtonListClientMonthlyIncome" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# ClientMonthlyIncome %>' SelectedNullableEnumValue='<%# Bind("ClientMonthlyIncome") %>' TabIndex="21" />
                                                <pp:PropertyProxyValidator ID="proxyValidatorClientMonthlyIncome" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="ClientMonthlyIncome" ControlToValidate="radioButtonListClientMonthlyIncome" RulesetName="Data" CssClass="required" />
                                            </td>
                                            <td>
                                                <air:RadioButtonList ID="radioButtonListClientAssets" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# ClientAssets %>' SelectedNullableEnumValue='<%# Bind("ClientAssets") %>' TabIndex="22" />
                                                <pp:PropertyProxyValidator ID="proxyValidatorClientAssets" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="ClientAssets" ControlToValidate="radioButtonListClientAssets" RulesetName="Data" CssClass="required" />
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
                                            <td style="width: 375px;">
                                                <span class="required">*</span>&nbsp;<span class="tag">Receiving or Applying for Social Security Disability or Medicare Disability:</span>
                                            </td>
                                            <td>
                                                <span class="required">*</span>&nbsp;<span class="tag">Dual Eligible with Mental Illness / Mental Disability :</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <air:RadioButtonList ID="radioButtonListClientReceivingSSOrMedicareDisability" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# ClientReceivingSSOrMedicareDisability %>' SelectedNullableEnumValue='<%# Bind("ClientReceivingSSOrMedicareDisability") %>' TabIndex="23" />
                                                <pp:PropertyProxyValidator ID="proxyValidatorClientReceivingSSOrMedicareDisability" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="ClientReceivingSSOrMedicareDisability" ControlToValidate="radioButtonListClientReceivingSSOrMedicareDisability" RulesetName="Data" CssClass="required" />
                                            </td>
                                            <td>
                                                <air:RadioButtonList ID="radioButtonListClientDualEligble" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# ClientDualEligble %>' SelectedNullableEnumValue='<%# Bind("ClientDualEligble") %>' TabIndex="24" />
                                                <pp:PropertyProxyValidator ID="proxyValidatorClientDualEligble" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="ClientDualEligble" ControlToValidate="radioButtonListClientDualEligble" RulesetName="Data" CssClass="required" />
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
                                            <td colspan="2">
                                                <h4>
                                                    <span class="required">*</span>&nbsp; PRESCRIPTION DRUG ASSISTANCE</h4>
                                                <br />
                                                <pp:PropertyProxyValidator ID="proxyValidatorPrescriptionDrugAssistance" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="TopicCount" ControlToValidate="checkBoxListMedicarePrescriptionDrugCoverageTopics" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorPrescriptionDrugAssistance_ValueConvert" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="tag">Medicare Prescription Drug Coverage (Part D):</span>
                                                <br />
                                                <air:CheckBoxList ID="checkBoxListMedicarePrescriptionDrugCoverageTopics" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# MedicarePrescriptionDrugCoverage %>' SelectedItems='<%# Bind("MedicarePrescriptionDrugCoverageTopics") %>' TabIndex="25" />
                                                <br />
                                                <span class="tag">Part D Low Income Subsidy (LIS/Extra Help):</span>
                                                <br />
                                                <air:CheckBoxList ID="checkBoxListPartDLowIncomeSubsidyTopics" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# PartDLowIncomeSubsidy %>' SelectedItems='<%# Bind("PartDLowIncomeSubsidyTopics") %>' TabIndex="27" />
                                                <br />
                                                <span class="tag">Other Prescription Assistance:</span>
                                                <br />
                                                <air:CheckBoxList ID="checkBoxListOtherPrescriptionAssitanceTopics" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# OtherPrescriptionAssistance %>' SelectedItems='<%# Bind("OtherPrescriptionAssistanceTopics") %>' TabIndex="29" />
                                   
                     
                                                <asp:Label runat="server" ID="labelOtherPrescriptionAssitanceSpecified" AssociatedControlID="textBoxOtherPrescriptionAssitanceSpecified">Specify Other:</asp:Label><br />
                                                <asp:TextBox runat="server" ID="textBoxOtherPrescriptionAssitanceSpecified" Text='<%# Bind("OtherPrescriptionAssitanceSpecified") %>' MaxLength="250" TabIndex="30" />
                                      <br />
                                     <asp:CustomValidator runat="server" ID="CustomValidatorForPrescriptionCheckBox" OnServerValidate="validatorCheckIfOtherPrescriptionChecked_ServerValidate"
                                      ControlToValidate="checkBoxListOtherPrescriptionAssitanceTopics" CssClass="required" ErrorMessage="Please Enter text in  Specify Other." />
                                    <br />
                                     <asp:CustomValidator runat="server" ID="CustomValidatorForPrescriptionText" OnServerValidate="validatorCheckIfOtherPrescriptionText_ServerValidate"
                                      ControlToValidate="textBoxOtherPrescriptionAssitanceSpecified" CssClass="required" ErrorMessage="Please check Other Checkbox." />
                                                
                                               
                                                <br />
                                                <br />
                                                <span class="tag">MEDICARE (Parts A & B):</span>
                                                <br />
                                                <air:CheckBoxList ID="checkBoxListMedicarePartsAandBTopics" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# MedicarePartsAandB %>' SelectedItems='<%# Bind("MedicarePartsAandBTopics") %>' TabIndex="32" />
                                            </td>
                                            <td>
                                                <span class="tag">Medicare Advantage (HMO, POS, PPO, PFFS, SNP, MSA, Cost):</span>
                                                <br />
                                                <air:CheckBoxList ID="checkBoxListMedicareAdvantageTopics" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# MedicareAdvantage %>' SelectedItems='<%# Bind("MedicareAdvantageTopics") %>' TabIndex="26" />
                                                <br />
                                                <span class="tag">Medicare Supplement/Select:</span>
                                                <br />
                                                <air:CheckBoxList ID="checkBoxListMedicareSupplementTopics" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# MedicareSupplement %>' SelectedItems='<%# Bind("MedicareSupplementTopics") %>' TabIndex="28" />
                                                <br />
                                                <span class="tag">Medicaid:</span>
                                                <br />
                                                <air:CheckBoxList ID="checkBoxListMedicaidTopics" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# Medicaid %>' SelectedItems='<%# Bind("MedicaidTopics") %>' TabIndex="30" />
                                                <br />
                                                <span class="tag">Other:</span>
                                                <br />
                                                <air:CheckBoxList ID="checkBoxListOtherDrugTopics" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# OtherDrug %>' SelectedItems='<%# Bind("OtherDrugTopics") %>' TabIndex="32" />

                                      
                                                <asp:Label runat="server" ID="labelOtherDrugTopicsSpecified" AssociatedControlID="textBoxOtherDrugTopicsSpecified">Specify Other:</asp:Label>
                                                <br />
                                                <asp:TextBox runat="server" ID="textBoxOtherDrugTopicsSpecified" Text='<%# Bind("OtherDrugTopicsSpecified") %>' MaxLength="250" TabIndex="33" />
                                      
                                       <br />                                               
                                     <asp:CustomValidator runat="server" ID="CustomValidatorForDrugTopicsCheck" OnServerValidate="validatorCheckIfOtherDrugTopicsChecked_ServerValidate"
                                      ControlToValidate="checkBoxListOtherDrugTopics" CssClass="required" ErrorMessage="Please Enter text in  Specify Other." />
                                      <br />
                                     <asp:CustomValidator runat="server" ID="CustomValidatorForDrugTopicsText" OnServerValidate="validatorCheckIfOtherDrugTopicsText_ServerValidate"
                                      ControlToValidate="textBoxOtherDrugTopicsSpecified" CssClass="required" ErrorMessage="Please check Other Checkbox." />

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
                                            <td style="width: 375px;">
                                                <span class="required">*</span>&nbsp;<span class="tag">Total Time Spent on This Contact Date:</span>
                                            </td>
                                            <td>
                                                <span class="required">*</span>&nbsp;<span class="tag">Status:</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox runat="server" ID="textBoxHoursSpent" Text='<%# Bind("HoursSpent") %>' Width="50px" CssClass="hours" TabIndex="34" />
                                                <asp:Label runat="server" ID="labelHoursSpent" AssociatedControlID="textBoxHoursSpent" CssClass="tag">Hours</asp:Label>
                                                &nbsp;
                                                <asp:TextBox runat="server" ID="textBoxMinutesSpent" Text='<%# Bind("MinutesSpent") %>' Width="50px" CssClass="minutes" TabIndex="35" />
                                                <asp:Label runat="server" ID="labelMinutesSpent" AssociatedControlID="textBoxMinutesSpent" CssClass="tag">Minutes</asp:Label>
                                                <br />
                                                <asp:CustomValidator runat="server" ID="validatorTimeSpent" Text="Total Time Spent is required." OnServerValidate="validatorTimeSpent_ServerValidate" />
                                                <pp:PropertyProxyValidator ID="proxyValidatorHoursSpent" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="HoursSpent" ControlToValidate="textBoxHoursSpent" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorHoursAndMinutes_ValueConvert" />
                                                <br />
                                                <pp:PropertyProxyValidator ID="proxyValidatorMinutesSpent" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="MinutesSpent" ControlToValidate="textBoxMinutesSpent" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorHoursAndMinutes_ValueConvert" />
                                            </td>
                                            <td> 
                                                <air:RadioButtonList ID="radioButtonListClientStatus" runat="server" DataTextField="Value" DataValueField="Key" DataSource='<%# ClientStatus %>' SelectedEnumValue='<%# Bind("ClientStatus") %>' TabIndex="36" />
                                                <pp:PropertyProxyValidator ID="proxyValidatorClientStatus" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="ClientStatus" ControlToValidate="radioButtonListClientStatus" RulesetName="Data" CssClass="required" />
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
                                            <td>
                                                <asp:Label runat="server" ID="labelComments" AssociatedControlID="textBoxComments" CssClass="tag">Comments</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox runat="server" ID="textBoxComments" Text='<%# Bind("Comments") %>' TextMode="MultiLine" Columns="20" Rows="10" CssClass="CCcomment" TabIndex="37"></asp:TextBox>
                                                <br />
                                                <pp:PropertyProxyValidator ID="proxyValidatorComments" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" PropertyName="Comments" ControlToValidate="textBoxComments" RulesetName="Data" CssClass="required" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <hr />
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
                                                <asp:Label ID="ErrSpecialFieldListCMSmsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                <air:SpecialFieldList runat="server" ID="specialFieldListCMS" DataKeyName="Id" DataTextField="Name" DataIsRequiredField="IsRequired" DataValidationTypeField="ValidationType" DataRangeField="Range" DataTextFormat="{0}:" DataSource='<%# CMSSpecialFields %>' Items='<%# Bind("CMSSpecialUseFields") %>' ValueMaxLength="25" TabIndex="38" />
                                            </td>
                                            <td style="vertical-align: top;">
                                                <air:SpecialFieldList runat="server" ID="specialFieldListState" DataKeyName="Id" DataTextField="Name" DataIsRequiredField="IsRequired" DataValidationTypeField="ValidationType" DataRangeField="Range" DataTextFormat="{0}:" DataSource='<%# StateSpecialFields %>' Items='<%# Bind("StateSpecialUseFields") %>' ValueMaxLength="25" TabIndex="39" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <hr />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:ValidationSummary ID = "ValidationSummaryFooter" runat = "server"
                                HeaderText = "Fix the following error(s) before you click submit. ">
                                </asp:ValidationSummary>
                                <div class="commands">
                                    <br />
                                    <asp:Button runat="server" ID="buttonSubmit" Text="Submit" CssClass="formbutton3" OnClick="buttonSubmit_Click" CausesValidation="false" TabIndex="40" />
                                </div>
                            </InsertItemTemplate>
                        </asp:FormView>
            </asp:Panel>
        </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceAddClientContact" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.AddClientContactViewData" OnInserting="dataSourceAddClientContact_Inserting" OnInserted="dataSourceAddClientContact_Inserted" />
</asp:Content>
