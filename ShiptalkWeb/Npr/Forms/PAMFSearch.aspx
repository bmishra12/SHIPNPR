<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ShiptalkWebWide.Master"
    CodeBehind="PAMFSearch.aspx.cs" Inherits="NPRRebuild.ShiptalkWeb.PAMF.PAMFSearch" %>

<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls"
    TagPrefix="pp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>Public Media Event Search</title>
    <link href="../../css/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />


    <script type="text/javascript" src="../../scripts/jquery-ui-mask-1.7.2.js"></script>

    <script type="text/javascript" src="../../scripts/jquery.charcounter.js"></script>

    <script type="text/javascript">

        function initialize(isPageLoad) {

            $('.datePicker').datepicker({ showButtonPanel: true,
                onSelect: function() { },
                onClose: function(dateText, inst) { $(this).focus(); },
                changeMonth: true,
                changeYear: true,
                showOn: 'button',
                buttonImage: '../../css/images/calendar.gif',
                buttonImageOnly: true
            }).mask({ mask: '19/99/9999', allowPartials: true }).width(175);

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
                Find a Public Media Event</h1>
            <div>
                <div class="commands">
                    <a runat="server" id="aAddPam" href='<%# RouteController.PamAdd() %>'>Add a PAM</a>
                    <br />
                    <a runat="server" id="aAddSpecialField" href='<%# RouteController.SpeciaFieldsSearch("2") %>' visible="false">Add
                        a Special Field</a>
                    <br />
                    <asp:LinkButton runat="server" ID="linkButtonListRecentPams" OnClick="linkButtonListRecentPams_Click">List Recent PAMs</asp:LinkButton>
  <br />
                     <a runat="server" id="a1" href="~/Npr/Docs/Chapter_3_NPR_PAM_508.pdf"  target='_blank' > <b>NPR User Manual: PAM Form</b></a>
               
                </div>
                <br />
                <asp:Panel runat="server" ID="panelSearchCriteria">
                    <table width="100%">

                        <tr>
                            <td class="tdFormLabel">
                                Activity Start Date Range:
                            </td>
                            <td>
                                <asp:Label runat="server" ID="labelFromDateOfContact" AssociatedControlID="textBoxFromDateOfContact" Text="From:" ToolTip="From Contact Date." />
                                <br />
                                <asp:TextBox runat="server" ID="textBoxFromDateOfContact" CssClass="datePicker textfield3wm" Style="width: 100px;" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="labelToDateOfContact" AssociatedControlID="textBoxToDateOfContact" Text="To:" ToolTip="To Contact Date." />
                                <br />
                                <asp:TextBox runat="server" ID="textBoxToDateOfContact" CssClass="datePicker textfield3wm" Style="width: 100px;" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdFormLabel">
                                <asp:Label runat="server" ID="labelState" AssociatedControlID="dropDownListState">State:</asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList runat="server" ID="dropDownListState" DataTextField="Value" DataValueField="Key" AppendDataBoundItems="true" SelectedValue='<%# DefaultState.StateAbbr %>' Enabled='<%# Scope == Scope.CMS || Scope == Scope.CMSRegional %>' DataSource='<%# States %>' CssClass="dropdown1wm" AutoPostBack="True" OnSelectedIndexChanged="dropDownListState_SelectedIndexChanged">
                                    <asp:ListItem Text="-- Select a State --" Value="CM" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdFormLabel">
                                <asp:Label runat="server" ID="labelAgency" AssociatedControlID="dropDownListAgency">Agency:</asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList runat="server" ID="dropDownListAgency" DataTextField="Value" DataValueField="Key" Style="width: 475px;" CssClass="dropdown1wm" AutoPostBack="true" OnSelectedIndexChanged="dropDownListAgency_SelectedIndexChanged" Enabled='<%# Scope.IsLowerOrEqualTo(Scope.State) %>'>
                                        <asp:ListItem Text='-- All of my agencies --' Value="0" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdFormLabel">
                                <asp:Label runat="server" ID="labelCounselor" Text="Presenter:" ToolTip="Presenter" AssociatedControlID="dropDownListCounselor" />
                            </td>
                            <td colspan="2">
                                <asp:DropDownList runat="server" ID="dropDownlistCounselor" DataSource='<%# Presenters %>' DataTextField="Value" DataValueField="Key" Style="width: 550px;" CssClass="dropdown1wm" AppendDataBoundItems="true" OnDataBound="dropDownlistCounselor_DataBound" Enabled='<%# Scope.IsLowerOrEqualTo(Scope.State) %>' >
                                    <asp:ListItem Text="-- All presenters --" Value="0" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdFormLabel">
                                <asp:Label runat="server" ID="labelSubmitter" Text="Submitter:" ToolTip="Submitter" AssociatedControlID="dropDownlistSubmitter" />
                            </td>
                            <td colspan="2">
                                <asp:DropDownList runat="server" ID="dropDownlistSubmitter" DataSource='<%# Submitters %>' DataTextField="Value" DataValueField="Key" Style="width: 550px;" CssClass="dropdown1wm" AppendDataBoundItems="true" OnDataBound="dropDownlistSubmitter_DataBound" Enabled='<%# Scope.IsLowerOrEqualTo(Scope.State) %>'>
                                    <asp:ListItem Text="-- All submitters --" Value="0" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2">
                                <asp:Button runat="server" ID="buttonSearch" Text="Search" ToolTip="Search" OnClick="buttonSearch_Click" CssClass="formbutton1a" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                
                 <div id="dv5col">
                    <asp:Panel runat="server" ID="panelNoResults" Visible="false">
                        <p style="text-align: center; color: Red;">
                        <%
                            if (!IsPostBack)
                            {
                        %>
                            No recent Public Media Events were found that were submitted for you.
                        <%
                            }
                            else
                            {  %>
                            No Public Media Events were found for the search criteria you provided.
                        <%  } %>
                            
                        </p>
                    </asp:Panel>
                    <asp:ListView runat="server" ID="listViewPams" DataSourceID="dataSourcePams" ItemPlaceholderID="placeHolder" DataKeyNames="PamID">
                        <LayoutTemplate>
                            <table id="searchResults" class="dataTable">
                                <thead>
                                    <tr>
                                        <th scope="col" style="width: 270px;">
                                            Event Name
                                        </th>
                                        <th scope="col">
                                            Agency Name
                                        </th>
                                        <th scope="col">
                                            Start Date
                                        </th>
                                        <th scope="col">
                                            End Date
                                        </th>
                                        <th scope="col" style="width: 162px;">
                                            &nbsp;
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr runat="server" id="placeHolder">
                                    </tr>
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td valign="top">
                                    <%# Eval("EventName").EncodeHtml()%>
                                    </a>
                                    <br />
                                </td>
                                <td scope="row" valign="top">
                                    <%# Eval("AgencyName").EncodeHtml() %>
                                </td>
                                <td>
                                    <%# Eval("ActivityStartDate", "{0:MM/dd/yyyy}").EncodeHtml()%>
                                </td>
                                <td>
                                    <%# Eval("ActivityEndDate", "{0:MM/dd/yyyy}").EncodeHtml()%>
                                </td>
                                <td valign="top">
                                    <a id="A2" runat="server" href='<%# RouteController.PamView((int)Eval("PamID")) %>'
                                        title="View this Pam.">View</a> | <a id="A3" runat="server" href='<%# RouteController.PamEdit((int)Eval("PamID")) %>'
                                            title="Edit this Pam.">Edit</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                    <div style="text-align: center;">
                        <asp:DataPager ID="pager" runat="server" PageSize="25" PagedControlID="listViewPams">
                            <Fields>
                                <asp:NumericPagerField NextPageText="Next" PreviousPageText="Previous" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourcePams" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.SearchPublicMediaEventViewData"
        OnSelecting="dataSourcePublicMediaEvent_Selecting" />
</asp:Content>
