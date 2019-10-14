<%@ Page Language="C#" MasterPageFile="~/ShiptalkWebWide.Master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeBehind="PAMFEdit.aspx.cs" Inherits="NPRRebuild.ShiptalkWeb.PAMF.PAMFEdit" Title="Public and Media Activity Form" %>

<%@ Register Assembly="ShiptalkWebControls" Namespace="ShiptalkWebControls" TagPrefix="air" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls"
    TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet"
    TagPrefix="pp" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>Edit Public And Media Events</title>    
    
    <link href="../../css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../../scripts/jquery-ui-mask-1.7.2.js"></script>
    <script type="text/javascript" src="../../../scripts/jquery.charcounter.js"></script>

    <script type="text/javascript">
        
        $(document).ready(function() {

        $('.datePicker').datepicker({ showButtonPanel: true,
                onSelect: function() { },
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../../../css/images/calendar.gif',
                buttonImageOnly: true
            }).mask({ mask: '19/99/9999', allowPartials: true }).width(175);
            $('.phone').mask({ mask: '(999) 999-9999 x9999', allowPartials: true });
            $('.zip').mask({ mask: '99999', allowPartials: true });
            $('.3Digit').mask({ mask: '999', allowPartials: true });
            $('.4Digit').mask({ mask: '9999', allowPartials: true });
            $('.6Digit').mask({ mask: '999999', allowPartials: true });
            $('.7Digit').mask({ mask: '9999999', allowPartials: true });
            $('.alphanum').mask({ mask: '*************************', allowPartials: true });
            $('.onlynum').mask({ mask: '9999999999999999999999999', allowPartials: true });
        });
        
    </script>
    
    <script type="text/javascript">
        function addPresenter() {
            var stateCode = '<%= AgencyState.Code %>';
            var _url = "/user/admin/addpresentor/" + stateCode;

            window.showModalDialog(_url, window.self, "dialogHeight:500px;dialogWidth:898px;status:no;toolbar:no;menubar:no;location:no;scroll:no");
            var pageId = '<%=  Page.ClientID %>';
                __doPostBack(pageId, "returnValue");
        }

    
    </script>



</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontentwide">
        <div class="dv5col">
            <h1>Edit Public And Media Events</h1>

            <asp:Panel runat="server" ID="panelForm" Visible="true">
                <asp:FormView runat="server" ID="formViewPAM" DefaultMode="Edit"  
                    DataKeyNames="PamID" DataSourceID="dataSourceEditPAM" OnPreRender="formViewPAM_OnPreRender">
                    <EditItemTemplate>
                         <p>(Items marked in <span class="required">*</span> indicate required fields.)</p>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="padding-left: 10px;
                            padding-right: 10px;">

                            <tr>
                                <td colspan="2">
                                    Agency Name:&nbsp; <%# AgencyName %>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2">
                                
                                    <asp:GridView ID="grdPamPresenters" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" GridLines="None" Width="100%" DataSource='<%# LISTPRESENTERS %>' OnPreRender="grdPamPresenters_OnPreRender">
                                        <RowStyle BackColor="#EFF3FB" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Presenter or Contributor Name">
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="dropDownListCounselor" DataTextField="Value"
                                                        DataValueField="Key" DataSource='<%# Bind("PAMShipUsers") %>' Width="460px"  AppendDataBoundItems="true">
                                                        <asp:ListItem Text="-- Select a Presenter or Contributor --" Value="0" />
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Affiliation">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAffiliation" runat="server" MaxLength="255" Width="200px" Text='<%# Bind("Affiliation") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Hours Spent on Activity Per Presenter-Contributor">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtHoursSpent" runat="server" MaxLength="7" Text='<%# Bind("HoursSpent") %>' Width="100px" CssClass="hoursSpent"></asp:TextBox>
                                                <cc1:TextBoxWatermarkExtender ID="txtHoursSpentExtender" runat="server" TargetControlID="txtHoursSpent"
                                                    WatermarkText="e.g., 9999.75" WatermarkCssClass="textfield3wm" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#2461BF" />
                                    </asp:GridView>
                                    
                                    <asp:Label id="lblgridValidation" ForeColor="Red" runat="server"></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td colspan="6">
                                                <asp:Panel ID="PnlPresenter" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="height: 24px" align="right">
                                                <asp:Button ID="btnAddNewRow" Text="Add New Row" runat="server" CausesValidation="false"
                                                    OnClick="btnAddRow_Click" />
                                            </td>
                                            <td colspan="6" style="height: 24px" align="right">
                                            <input type="button" value="Add New Presenter" onclick="addPresenter()" />

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
                                    <h2>
                                        Activity or Event</h2>
                                        <span class="required">*</span>&nbsp;<span class="tag">At least one Activity or Event
                                                    is required .</span>
                                         <br />

                                       <asp:Label id="lblSectionValidation" ForeColor="Red" runat="server"></asp:Label>

                                    <p>
                                        <strong>1. Interactive Presentation to Public. Face to Face In-Person.</strong></p>
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td valign="top" align="right" width="45%">
                                                Estimated Number of Attendees:
                                            </td>
                                            <td width="55%">
                                                <asp:TextBox ID="txtNoOfIPAttendees" runat="server" Text='<%# Bind("InteractiveEstAttendees") %>'
                                                    MaxLength="3"  CssClass="3Digit" ToolTip="Estimated Number of Attendees"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Persons Provided Enrollment Assistance:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIPEnrollmentAssistance" runat="server" Text='<%# Bind("InteractiveEstProvidedEnrollAssistance") %>'
                                                 MaxLength="3"  CssClass="3Digit"   ToolTip="Estimated Persons Provided Enrollment Assistance" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <strong>2. Booth or Exhibit. At Heath Fair, Senior Fair, or Special Event.</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Number of Direct Interactions with Attendees:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNoOfBoothAttendees" runat="server" Text='<%# Bind("BoothEstDirectContacts") %>'
                                                 MaxLength="4"  CssClass="4Digit"   ToolTip="Estimated Number of Attendees"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Persons Provided Enrollment Assistance:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBoothEnrollmentAssistance" runat="server" Text='<%# Bind("BoothEstEstProvidedEnrollAssistance") %>'
                                                MaxLength="4"  CssClass="4Digit"  ToolTip="Estimated Persons Provided Enrollment Assistance"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="2">
                                                <strong>3. Dedicated Enrollment Event Sponsored By SHIP or in Partnership.</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Est Number Persons Reached at Event Regardless of Enroll Assistance:
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtDedicatedEnrollmentEvent1" runat="server" Text='<%# Bind("DedicatedEstPersonsReached") %>'
                                                 MaxLength="3"  CssClass="3Digit" ToolTip="Est Number Persons Reached at Event Regardless of Enroll Assistance"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Number Persons Provided Any Enrollment Assistance:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDedicatedEnrollmentEvent2" runat="server" Text='<%# Bind("DedicatedEstAnyEnrollmentAssistance") %>'
                                                  MaxLength="3"  CssClass="3Digit" ToolTip=" Estimated Number Persons Provided Any Enrollment Assistance"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Number Provided Enrollment Assistance with Part D:
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtDedicatedEnrollmentEvent3" runat="server" Text='<%# Bind("DedicatedEstPartDEnrollmentAssistance") %>'
                                                  MaxLength="3"  CssClass="3Digit"   ToolTip="Estimated Number Provided Enrollment Assistance with Part D"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Number Provided Enrollment Assistance with LIS:
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtDedicatedEnrollmentEvent4" runat="server" Text='<%# Bind("DedicatedEstLISEnrollmentAssistance") %>'
                                                  MaxLength="3"  CssClass="3Digit"   ToolTip="Estimated Number Provided Enrollment Assistance with LIS"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Number Provided Enrollment Assistance with MSP:
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtDedicatedEnrollmentEvent5" runat="server" Text='<%# Bind("DedicatedEstMSPEnrollmentAssistance") %>'
                                                 MaxLength="3"  CssClass="3Digit"  ToolTip="Estimated Number Provided Enrollment Assistance with MSP"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Number Provided Enrollment Assist Other Medicare Program:
                                            </td>
                                            <td valign="top">
                                                <asp:TextBox ID="txtDedicatedEnrollmentEvent6" runat="server" Text='<%# Bind("DedicatedEstOtherEnrollmentAssistance") %>'
                                                 MaxLength="3"  CssClass="3Digit"  ToolTip="Estimated Number Provided Enrollment Assist Other Medicare Program"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="2">
                                                <strong>4. Radio Show. Live or Taped. Not a Public Service Announce or Ad.</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%" align="right">
                                                Estimated Number of Listeners Reached:
                                            </td>
                                            <td width="60%">
                                                <asp:TextBox ID="txtRadioShowEstNo" runat="server" Text='<%# Bind("RadioEstListenerReach") %>'
                                                  MaxLength="6"  CssClass="6Digit"  ToolTip="Estimated Number of Listeners Reached"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <strong>5. TV or Cable Show. Live or Taped. Not a Public Service Announce or Ad.</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                Estimated Number of Viewers Reached:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTVshowEstNo" runat="server" Text='<%# Bind("TVEstViewersReach") %>'
                                                  MaxLength="6"  CssClass="6Digit"   ToolTip="Estimated Number of Viewers Reached"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="2">
                                                <strong>6. Electronic Other Activity. PSAs, Electronic Ads, Crawls, Video Conf, Web
                                                    Conf, Web Chat</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="50%" style="line-height: 20px">
                                                Est Persons Viewing or Listening to PSA, Electronic Ad, Crawl
                                                <br />
                                                Across Entire Campaign, Video Conf,Web Conf, Web Chat:
                                            </td>
                                            <td width="50%">
                                                <asp:TextBox ID="txtEloctronicEstPersons" runat="server" Text='<%# Bind("ElectronicEstPersonsViewingOrListening") %>'
                                                 MaxLength="7"  CssClass="7Digit"  ToolTip="Est Persons Viewing or Listening to PSA"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <strong>7. Print Other Activity. Newspaper, Newsletter, Pamphlets, Fliers, Posters,
                                                    Targeted Mailings</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="line-height: 20px;">
                                                Est Persons Reading Article, Newsletter, Ad or Pieces of
                                                <br />
                                                Targeted Mail or Other Printed Across Entire Campaign:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtReadingEstPersons" runat="server" Text='<%# Bind("PrintEstPersonsReading") %>'
                                                MaxLength="7"  CssClass="7Digit"  ToolTip=" Est Persons Reading Article"></asp:TextBox>
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
                                <td valign="top" width="50%">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                               <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="label1"
                                                    AssociatedControlID="txtStartDate" Text="Start Date of Activity:"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Bind("ActivityStartDate", "{0:MM/dd/yyyy}") %>' ToolTip="Start Date of Activity" CssClass="datePicker" ></asp:TextBox>
                                                 <br />
                                                 <asp:CustomValidator runat="server" ID="proxyValidatorStartDateUpperBound" OnServerValidate="validatorCheckUpperBound_ServerValidate" ControlToValidate="txtStartDate" CssClass="required" ErrorMessage="Start Date of Activity should be less than or equal to today's date." />
                                                 <br />
                                                  <pp:PropertyProxyValidator ID="PropertyProxyValidatorActivityStartDate" runat="server" Display="Dynamic"
                                                    DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditPublicMediaEventViewData"
                                                    PropertyName="ActivityStartDate" OnValueConvert="PropertyProxyValidatorActivityStartDate_ValueConvert" ControlToValidate="txtStartDate" RulesetName="Data"
                                                    CssClass="required" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="label2"
                                                    AssociatedControlID="txtEndDate" Text="End Date of Activity:"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Bind("ActivityEndDate", "{0:MM/dd/yyyy}") %>' columns="12" ToolTip="End Date of Activity" CssClass="datePicker"></asp:TextBox>
                                                <br />
                                                 <asp:CustomValidator runat="server" ID="proxyValidatorEndDateUpperBound" OnServerValidate="validatorCheckEndDateUpperBound_ServerValidate" ControlToValidate="txtEndDate" CssClass="required" ErrorMessage="End Date of Activity should be less than or equal to today's date." />
                                                 <br />
                                                  <pp:PropertyProxyValidator ID="PropertyProxyValidatorActivityEndDate" runat="server" Display="Dynamic"
                                                    DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditPublicMediaEventViewData"
                                                    PropertyName="ActivityEndDate" ControlToValidate="txtEndDate" RulesetName="Data"
                                                    CssClass="required" />

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" width="50%">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="label3"
                                                    AssociatedControlID="txtEventName" Text="Event or Group Name:"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtEventName" runat="server" Text='<%# Bind("EventName") %>' MaxLength="100"  ToolTip="Event or Group Name"></asp:TextBox>
                                                <br />
                                                  <pp:PropertyProxyValidator ID="PropertyProxyValidatorEventName" runat="server" Display="Dynamic"
                                                    DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditPublicMediaEventViewData"
                                                    PropertyName="EventName" ControlToValidate="txtEventName" RulesetName="Data"
                                                    CssClass="required" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Contact First Name:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Bind("ContactFirstName") %>'
                                                 MaxLength="50"   ToolTip="Contact First Name"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Contact Last Name:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLastName" runat="server" Text='<%# Bind("ContactLastName") %>'
                                                 MaxLength="50"  ToolTip="Contact Last Name"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Contact Phone Number:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPhoneNo" runat="server" Text='<%# Bind("ContactPhone") %>' ToolTip="Contact Phone Number" CssClass="phone"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                            
                        <asp:UpdatePanel runat="server" ID="updatePanelPresenter" EnableViewState="true" RenderMode="Block" UpdateMode="Always">
                            <ContentTemplate>

                                <td valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">

                                        <tr id="new">
                                            <td>
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelState" AssociatedControlID="dropDownListEventState">State of Event:</asp:Label></td>
                                            
                                            <td>
                                                <asp:DropDownList runat="server" ID="dropDownListEventState" DataTextField="Value" DataValueField="Key"
                                                    
                                                    AutoPostBack="true" OnSelectedIndexChanged="dropDownListEventState_SelectedIndexChanged"
                                                    AppendDataBoundItems="true" 
                                                    DataSource='<%# EventStates %>' >
                                                    <asp:ListItem Text="-- Select State --" Value="" />
                                                </asp:DropDownList>
                                              <br />
                                                  <pp:PropertyProxyValidator ID="proxyValidatorEventStates" runat="server" Display="Dynamic"
                                                    DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditPublicMediaEventViewData"
                                                    PropertyName="EventState" ControlToValidate="dropDownListEventState" RulesetName="Data"
                                                    CssClass="required" OnValueConvert="proxyValidatorEventState_ValueConvert"/>                                            
                                             </td>
                                        </tr>

                                        <tr id="County">
                                            <td>
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="label9" AssociatedControlID="dropDownListEventCounty">County of Event:</asp:Label></td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="dropDownListEventCounty" DataTextField="Value" DataValueField="Key"
                                                    AutoPostBack="true" OnSelectedIndexChanged="dropDownListEventCounty_SelectedIndexChanged"
                                                      AppendDataBoundItems="False">
                                                    <asp:ListItem Text="-- Select County --" Value="" />
                                                </asp:DropDownList>
                                              <br />
                                                <pp:PropertyProxyValidator ID="proxyValidatorEventCounty" runat="server" Display="Dynamic"
                                                    DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditPublicMediaEventViewData"
                                                    PropertyName="EventCountycode" ControlToValidate="dropDownListEventCounty" RulesetName="Data"
                                                    CssClass="required"  />                                            
                                          </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelClientZIPCode"
                                                    AssociatedControlID="dropDownListEventZipCode" Text="Zip Code of Event:"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="dropDownListEventZipCode" DataTextField="Value" DataValueField="Key"
                                                    AppendDataBoundItems="false">
                                                    <asp:ListItem Text="-- Select Zip Code --" Value="" />
                                                </asp:DropDownList>
                                              <br />
                                                  <pp:PropertyProxyValidator ID="proxyValidatorEventZipCode" runat="server" Display="Dynamic"
                                                    DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditPublicMediaEventViewData"
                                                    PropertyName="EventZIPCode" ControlToValidate="dropDownListEventZipCode" RulesetName="Data"
                                                    CssClass="required" />                                            


                                            </td>
                                        </tr>
                                    </table>
                                </td>
                               </ContentTemplate>  
                            </asp:UpdatePanel>
                                
                                <td valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="label6"
                                                    AssociatedControlID="txtCity" Text="City of Event:"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtCity" runat="server" Text='<%# Bind("EventCity") %>' MaxLength="50" ToolTip="City of Event"></asp:TextBox>
                                                 <br />
                                                <pp:PropertyProxyValidator ID="PropertyProxyValidatorEventCity" runat="server" Display="Dynamic"
                                                    DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditPublicMediaEventViewData"
                                                    PropertyName="EventCity" ControlToValidate="txtCity" RulesetName="Data"
                                                    CssClass="required" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="label7"
                                                    AssociatedControlID="txtStreetAddress" Text="Street Address of Event:"></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtStreetAddress" runat="server" Text='<%# Bind("EventStreet") %>'
                                                   MaxLength="100" ToolTip="Street Address of Event"></asp:TextBox>
                                                 <br />
                                                <pp:PropertyProxyValidator ID="PropertyProxyValidatorEventStreet" runat="server" Display="Dynamic"
                                                    DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditPublicMediaEventViewData"
                                                    PropertyName="EventStreet" ControlToValidate="txtStreetAddress" RulesetName="Data"
                                                    CssClass="required" />
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
                                <td>
                                 <span class="required">*</span>&nbsp;<span class="tag">Topic Focus (Check All That Apply)</span>

                                </td>
                                <td>
                                 <span class="required">*</span>&nbsp;<span class="tag">Target Audience (Check All That Apply)</span>

                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <air:CheckBoxList ID="checkBoxListPamTopics" runat="server" DataTextField="Value"
                                        DataValueField="Key" DataSource='<%# PamFocus %>' 
                                        SelectedItems='<%# Bind("PAMSelectedTopics") %>'  
                                        OnDataBound="checkBoxListPamTopics_DataBound"/>
                                     <pp:PropertyProxyValidator ID="proxyValidatorPamTopics" runat="server" Display="Dynamic" 
                                      DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditPublicMediaEventViewData" 
                                      PropertyName="PAMSelectedTopicsCount" ControlToValidate="checkBoxListPamTopics" RulesetName="Data" CssClass="required" />
                                      
                                    <asp:Label runat="server" ID="labelOtherPamTopicSpecified" AssociatedControlID="textBoxOtherPamTopicSpecified" >Specify Other:</asp:Label><br />
                                    <asp:TextBox runat="server" ID="textBoxOtherPamTopicSpecified"  columns="50" MaxLength="250" Text='<%# Bind("OtherPamTopicSpecified") %>' />

                                 <br />
                                 <asp:CustomValidator runat="server" ID="CustomValidatorForTopicsCheckBox" OnServerValidate="validatorCheckIfOtherTopicsChecked_ServerValidate"
                                  ControlToValidate="checkBoxListPamTopics" CssClass="required" ErrorMessage="Please Enter text in  Specify Other." />
                                <br />
                                 <asp:CustomValidator runat="server" ID="CustomValidatorForTopicsText" OnServerValidate="validatorCheckIfOtherTopicsText_ServerValidate"
                                  ControlToValidate="textBoxOtherPamTopicSpecified" CssClass="required" ErrorMessage="Please check Other Topics Checkbox." />
                                            

                                </td>
                                <td valign="top">
                                     <air:CheckBoxList ID="checkBoxListPamAudience" runat="server" DataTextField="Value"
                                        DataValueField="Key" DataSource='<%# PamAudiences %>' 
                                        SelectedItems='<%# Bind("PAMSelectedAudiences") %>' 
                                        OnDataBound="checkBoxListPamAudience_DataBound"/>
                                      <pp:PropertyProxyValidator ID="PropertyProxyValidatorPamAudience" runat="server" Display="Dynamic" 
                                      DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditPublicMediaEventViewData" 
                                      PropertyName="PAMSelectedAudiencesCount" ControlToValidate="checkBoxListPamAudience" RulesetName="Data" CssClass="required" />

                                    <asp:Label runat="server" ID="labeltherPamAudienceSpecified" AssociatedControlID="textBoxOtherPamAudienceSpecified" >Specify Other:</asp:Label><br />
                                     <asp:TextBox runat="server" ID="textBoxOtherPamAudienceSpecified" columns="50" MaxLength="250" Text='<%# Bind("OtherPamAudienceSpecified") %>' />

                                 <br />
                                 <asp:CustomValidator runat="server" ID="CustomValidatorForAudienceCheckBox" OnServerValidate="validatorCheckIfOtherAudienceChecked_ServerValidate"
                                  ControlToValidate="checkBoxListPamAudience" CssClass="required" ErrorMessage="Please Enter text in  Specify Other." />
                                <br />
                                 <asp:CustomValidator runat="server" ID="CustomValidatorForAudienceText" OnServerValidate="validatorCheckIfOtherAudienceText_ServerValidate"
                                  ControlToValidate="textBoxOtherPamAudienceSpecified" CssClass="required" ErrorMessage="Please check Other Audiences Checkbox." />


                                </td>
                            </tr>

                            <tr>
                                <td colspan="2">

                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%;">
                            <tbody>
                                <tr>
                                    <td style="width: 50%;">
                                        <h4>
                                            Nationwide and CMS Special Use Fields</h4>
                                    </td>
                                    <td style="width: 50%;">
                                        <h4>
                                            State and Local Special Use Fields</h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:Label ID="ErrSpecialFieldListCMSmsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                        <air:SpecialFieldList runat="server" ID="specialFieldListCMS" DataKeyName="Id" DataTextField="Name"
                                            DataIsRequiredField="IsRequired" DataValidationTypeField="ValidationType"  DataRangeField="Range" DataTextFormat="{0}:"
                                            DataSource='<%# CMSSpecialFields %>' Items='<%# Bind("CMSSpecialUseFields") %>' ValueMaxLength="25" />
                                    </td>
                                    <td style="vertical-align: top;">
                                        <air:SpecialFieldList runat="server" ID="specialFieldListState" DataKeyName="Id"
                                            DataTextField="Name" DataIsRequiredField="IsRequired" DataValidationTypeField="ValidationType" DataRangeField="Range"
                                            DataTextFormat="{0}:" DataSource='<%# StateSpecialFields %>' Items='<%# Bind("StateSpecialUseFields") %>' ValueMaxLength="25" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                        <div class="commands">
                         <asp:panel runat="server" ID = "panelError" style="float: left; width: 500px; color: red;" Visible="false">
                            <asp:Label runat="server" ID="lblErrorOcoured" >Validation Error has occured. Please Scroll up and fix the error.</asp:Label>
                        </asp:panel>
                            <br />
                            <asp:Button runat="server" ID="buttonSubmit" Text="Submit" CssClass="formbutton3"
                                OnClick="buttonSubmit_Click" CausesValidation="false" />
                        </div>
                    </EditItemTemplate>
                </asp:FormView>
            </asp:Panel>
        </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceEditPAM" runat="server"
     DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.EditPublicMediaEventViewData"
     
     OnSelecting="dataSourceEditPAM_Selecting" 
    OnUpdated="dataSourceEditPAM_Updated" 
    OnUpdating="dataSourceEditPAM_Updating" />

</asp:Content>
