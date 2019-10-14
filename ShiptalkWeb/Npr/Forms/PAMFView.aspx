<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ShiptalkWebWide.Master" CodeBehind="PAMFView.aspx.cs" Inherits="NPRRebuild.ShiptalkWeb.PAMF.PAMFView" %>

<%@ Register Assembly="ShiptalkWebControls" Namespace="ShiptalkWebControls" TagPrefix="air" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects.UI" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>View Agency</title>
    


    <script type="text/javascript">


        function PrintThisPage() {
            var sOption = "toolbar=no,location=no,directories=yes,menubar=yes,";
            sOption += "scrollbars=yes,width=750,height=600,left=100,top=25";

            var sWinHTML = document.getElementById('contentforprint').innerHTML;

            var winprint = window.open("", "", sOption);
            winprint.document.open();
            winprint.document.write('<html><LINK href=../../../css/print.css rel=Stylesheet><body>');
            winprint.document.write('<h1>View Public Media Event</h1>');
            winprint.document.write(sWinHTML);
            winprint.document.write('</body></html>');
            winprint.document.close();
            winprint.focus();
        }

        $(document).ready(function() {

            //initialize alt row table styles.
            $(".dataTable tr:even").addClass("even");

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
            <a runat="server" href='<%# RouteController.PamAdd() %>'>Add a PAM</a>
            <br />
            <a  ID="addInSameAgency" runat="server" href='<%# RouteController.PamAddInAgency(Id.GetValueOrDefault(0))%>'>Add a PAM in the same Agency</a>
            <br />
            <br />
        </asp:Panel>
        
       <asp:Panel ID="panelForm" runat = "server">
        <div class="dv5col">
        
        
                 <asp:FormView runat="server" ID="formViewPAM" DataSourceID="dataSourceViewPAM" OnPreRender="formViewPAM_OnPreRender">
                 

                    <ItemTemplate>
                    
                    <div class="commands">
                        <a href="javascript:PrintThisPage();" >Printer Friendly Version</a> 
                        &nbsp;
                        <asp:Button runat="server" ID="buttonEditPam" Text="Edit" ToolTip="Edit this Pam." OnClick="buttonEditPam_Click" CssClass="formbutton1" />
                        &nbsp;
                        <asp:Button runat="server" ID="btnDelete" Text="Delete"  OnClick="buttonDeletePam_Click" CssClass="formbutton1" visible = '<%# isShowDelete %>'/>

                   </div>
                    <div id="contentforprint">
                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <span class="tag">Submitted By:</span>&nbsp;
                                                    <%# Eval("SubmitterName").EncodeHtml()%> &nbsp;
                                                    &nbsp;<span class="tag">On:</span>&nbsp;<%# Eval("CreatedDate", "{0:MM/dd/yyyy}").EncodeHtml()%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <hr>
                                                </td>
                                            </tr>
                            <tr>
                                <td colspan="2">
                                    Agency Name:&nbsp; <%# Eval("AgencyName").EncodeHtml()%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div>
                                    <br />
                                    <asp:Repeater runat="server" ID="repeaterPresenters" DataSource='<%# Eval("PamPresenters") %>'>
                                        <HeaderTemplate>
                                            <table class="dataTable">
                                                <thead>
                                                    <tr>
                                                        <th scope="col" nowrap>
                                                            Pam User
                                                        </th>
                                                        <th scope="col" nowrap>
                                                            Affiliation
                                                        </th>
                                                        <th scope="col" nowrap>
                                                            Hours 
                                                        </th>
                                                        <th>
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("PAMUserName").EncodeHtml()%>
                                                </td>
                                                <td>
                                                    <%# Eval("Affiliation").EncodeHtml()%>
                                                </td>
                                                <td>
                                                    <%# Eval("HoursSpent").EncodeHtml()%>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody> </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    </div>
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
                                    <p>
                                        <strong>1. Interactive Presentation to Public. Face to Face In-Person.</strong></p>
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td valign="top" align="right" width="45%">
                                                Estimated Number of Attendees:
                                            </td>
                                            <td width="55%">
                                                <%# Eval("InteractiveEstAttendees").EncodeHtml()%>
                                                    
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Persons Provided Enrollment Assistance:
                                            </td>
                                            <td>
                                                <%# Eval("InteractiveEstProvidedEnrollAssistance").EncodeHtml()%>
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
                                                <%# Eval("BoothEstDirectContacts").EncodeHtml()%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Persons Provided Enrollment Assistance:
                                            </td>
                                            <td>
                                                <%# Eval("BoothEstEstProvidedEnrollAssistance").EncodeHtml()%>
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
                                                <%# Eval("DedicatedEstPersonsReached").EncodeHtml()%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Number Persons Provided Any Enrollment Assistance:
                                            </td>
                                            <td>
                                                <%# Eval("DedicatedEstAnyEnrollmentAssistance").EncodeHtml()%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Number Provided Enrollment Assistance with Part D:
                                            </td>
                                            <td valign="top">
                                                <%# Eval("DedicatedEstPartDEnrollmentAssistance").EncodeHtml()%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Number Provided Enrollment Assistance with LIS:
                                            </td>
                                            <td valign="top">
                                                <%# Eval("DedicatedEstLISEnrollmentAssistance").EncodeHtml()%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Number Provided Enrollment Assistance with MSP:
                                            </td>
                                            <td valign="top">
                                                <%# Eval("DedicatedEstMSPEnrollmentAssistance").EncodeHtml()%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right">
                                                Estimated Number Provided Enrollment Assist Other Medicare Program:
                                            </td>
                                            <td valign="top">
                                                <%# Eval("DedicatedEstOtherEnrollmentAssistance").EncodeHtml()%>
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
                                                <%# Eval("RadioEstListenerReach").EncodeHtml()%>
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
                                                <%# Eval("TVEstViewersReach").EncodeHtml()%>
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
                                                <%# Eval("ElectronicEstPersonsViewingOrListening").EncodeHtml()%>
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
                                                <%# Eval("PrintEstPersonsReading").EncodeHtml()%>
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
                                                     Text="Start Date of Activity:"></asp:Label></td>
                                            <td>
                                                <%# Eval("ActivityStartDate", "{0:MM/dd/yyyy}").EncodeHtml()%>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="label2"
                                                     Text="End Date of Activity:"></asp:Label></td>
                                            <td>
                                                <%# Eval("ActivityEndDate", "{0:MM/dd/yyyy}").EncodeHtml()%> 

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td valign="top" width="50%">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="label3"
                                                    Text="Event or Group Name:"></asp:Label></td>
                                            <td>
                                                <%# Eval("EventName").EncodeHtml()%> 

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Contact First Name:
                                            </td>
                                            <td>
                                                <%# Eval("ContactFirstName").EncodeHtml()%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Contact Last Name:
                                            </td>
                                            <td>
                                                <%# Eval("ContactLastName").EncodeHtml()%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Contact Phone Number:
                                            </td>
                                            <td>
                                                <%# Eval("ContactPhone").EncodeHtml()%> 
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                            
                                <td valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">

                                       <tr id="new">
                                            <td>
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelState" >State of Event:</asp:Label>
                                            </td>
                                            <td>
                                              <%# Eval("EventState.StateName").EncodeHtml()%> 

                                            </td>
                                        </tr>

                                        <tr id="County">
                                            <td>
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="label9" >County Name of Event:</asp:Label>
                                            </td>
                                            <td>
                                                <%# Eval("EventCountyName").EncodeHtml()%> 

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelClientZIPCode"
                                                     Text="Zip Code of Event:"></asp:Label></td>
                                            <td>
                                                <%# Eval("EventZIPCode").EncodeHtml()%> 

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                
                                <td valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="label6"
                                                     Text="City of Event:"></asp:Label></td>
                                            <td>
                                                <%# Eval("EventCity").EncodeHtml()%> 

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="label7"
                                                     Text="Street Address of Event:"></asp:Label></td>
                                            <td>
                                                <%# Eval("EventStreet").EncodeHtml()%>

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
                                <td valign="top">
                                 <span class="required">*</span>&nbsp;<span class="tag">Topic Focus (Check All That Apply)</span>
                                    <air:CheckBoxList ID="checkBoxListPamTopics" runat="server" DataTextField="Value"
                                        DataValueField="Key" DataSource='<%# PamFocus %>'  Enabled="false" />
                                    <br />
                                    <asp:TextBox ID="txtOtherPamTopicSpecified" runat="server"  Enabled="false"
                                        Text='<%# Eval("OtherPamTopicSpecified").EncodeHtml().TrimEnd()%>'>
                                    </asp:TextBox>

                                </td>
                                <td valign="top">
                                 <span class="required">*</span>&nbsp;<span class="tag">Target Audience (Check All That Apply)</span>
                                     <air:CheckBoxList ID="checkBoxListPamAudience" runat="server" DataTextField="Value"
                                        DataValueField="Key" DataSource='<%# PamAudiences %>' Enabled="false"/>
                                    <br />
                                     <asp:TextBox ID="txtOtherPamAudienceSpecified" runat="server"  Enabled="false"
                                        Text='<%# Eval("OtherPamAudienceSpecified").EncodeHtml().TrimEnd()%>'>
                                    </asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>

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
                                            <asp:Repeater ID="repeaterCMSSpecialUseFields" runat="server" DataSource='<%# Eval("CMSSpecialUseFields") %>'>
                                                <ItemTemplate>
                                                    <span class="tag">
                                                        <%# Eval("Name").EncodeHtml()%>:&nbsp;&nbsp;</span><%# Eval("Value").EncodeHtml()%>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:Repeater ID="repeaterStateSpecialUseFields" runat="server" DataSource='<%# Eval("StateSpecialUseFields") %>'>
                                                <ItemTemplate>
                                                    <span class="tag">
                                                        <%# Eval("Name").EncodeHtml()%>:&nbsp;&nbsp;</span><%# Eval("Value").EncodeHtml()%>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>

                                <tr>
                                    <td colspan="2">
                                        <hr />
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
            <strong>Are you sure? The delete action will delete this Public Media Event record including all of this Public Media Event's associated data</strong>
            <br />NOTE: The record will be purged and cannot be recovered in future. 
        </p>
    </div>
    
    <pp:ObjectContainerDataSource ID="dataSourceViewPAM" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.ViewPublicMediaEventViewData" OnSelecting="dataSourceViewPAM_Selecting" />
</asp:Content>
