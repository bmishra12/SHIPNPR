<%@ Page Title="" Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true" CodeBehind="AgencyEdit.aspx.cs" Inherits="ShiptalkWeb.Agency.AgencyEdit" %>

<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>Edit Agency</title>
    


    <script type="text/javascript" src="../../scripts/jquery-ui-mask-1.7.2.js"></script>

    <script type="text/javascript" src="../../scripts/jquery.charcounter.js"></script>

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.exp&amp;sensor=false"></script>

    <script type="text/javascript">

        var geocoder;
        var agencyAddress1;
        var agencyAddress2;
        var agencyCity;
        var agencyState;
        var agencyZip;
        var agencyLat;
        var agencyLng;

        $(document).ready(function () {
            //Initialize the Service Area select lists.
            //$("#serviceAreaSelection").selectLists();

            var srcListBox = $("#serviceAreaSelection .src").find()
            var src = $("#serviceAreaSelection .src");
            var dst = $("#serviceAreaSelection .dst");
            var add = $("#serviceAreaSelection .add");
            var remove = $("#serviceAreaSelection .remove");
            var inputValues = $("#serviceAreaSelection input[type='hidden']");           

            add.click(function () {
                src.find("option:selected").appendTo(dst);

                //keep the listvalues updated.
                updateValues();
            });

            remove.click(function () {
                dst.find("option:selected").appendTo(src);
                //keep the listvalues updated.
                updateValues();
            });

            function updateValues() {
                var values = new Array();

                dst.find("option").each(function () {
                    values.push($(this).val());
                })

                inputValues.val(values);
            }

            function upateDstValues() {
                var values = inputValues.val().split(',');

                for (i = 0; i <= values.length; i++)
                    src.find("option[value='" + values[i] + "']").appendTo(dst);
            }

            //rmove all the items in dst first.
            dst.find("option").remove();
            upateDstValues();
            //set the width.
            dst.width(src.width());
            src.width(dst.width());

            //Apply a masks for input controls.
            $('.phone').mask({ mask: '(999) 999-9999 x9999', allowPartials: true });
            $('.zip').mask({ mask: '99999-9999', allowPartials: true });
            //Apply a char restrictor to comments.
            $('.comment').charCounter(300,
            {
                container: "<div></div>",
                classname: "counter"
            });

            //Added by Lavanya : Getting GeoCodes
            agencyAddress1 = $("[id$='_textBoxPhysicalAddress1']");
            agencyAddress2 = $("[id$='_textBoxPhysicalAddress2']");
            agencyCity = $("[id$='_textBoxPhysicalCity']");
            agencyZip = $("[id$='textBoxPhysicalZip']");
            agencyState = $("[id$='_hdnState']");
            agencyLng = $("[id$='_AgencyLongitude']");
            agencyLat = $("[id$='_AgencyLatitude']");

            agencyAddress1.blur(geocodeAddress);
            agencyAddress2.blur(geocodeAddress);
            agencyCity.blur(geocodeAddress);
            agencyState.blur(geocodeAddress);
            agencyZip.blur(geocodeAddress);

        });

        function geocodeAddress() {            
            var Lat;
            var Lng;
            var geocoder = new google.maps.Geocoder();

            var address1 = $.trim(agencyAddress1.val());
            var address2 = $.trim(agencyAddress2.val());
            var city = $.trim(agencyCity.val());
            var state = $.trim(agencyState.val());
            var zip = $.trim(agencyZip.val());

           //alert(address1 + ' ' + address2 + ' ' + city + ' ' + state + ' ' + zip);

            setTimeout(function () {

                geocoder.geocode({
                    'address': address1 + ' ' + address2 + ' ' + city + ' ' + state + ' ' + zip
                },
                     function (results, status) {
                         if (status == google.maps.GeocoderStatus.OK) {
                             Lat = results[0].geometry.location.lat();
                             Lng = results[0].geometry.location.lng();

                             agencyLng.val(Lng);
                             agencyLat.val(Lat);

                         } else {
                             //alert("Geocode was not successful for the following reason: " + status);
                         }
                     });

            });

        }             

          
        
    </script>

</asp:Content>
<asp:Content ID="contentBody" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
            <asp:FormView runat="server" ID="formViewAgency" DataSourceID="dataSourceEditAgency" DefaultMode="Edit" DataKeyNames="Id" Width="100%">
                <EditItemTemplate>
                    <h1>
                        Edit
                        <%# Eval("Name").EncodeHtml() %> (<%# Eval("Code").EncodeHtml().TrimEnd() %>)</h1>
                         <p>
                            (Items marked in <span class="required">*</span> indicate required fields.)</p>
                      <div id="dv3colFormContent" class="section">
                        <table class="formTable">
                            <tbody>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelName" AssociatedControlID="textBoxName">Agency Name:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxName" MaxLength="100" Text='<%# Bind("Name") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="Name" ControlToValidate="textBoxName" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelType" AssociatedControlID="dropDownListType">Agency Type:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dropDownListType" DataTextField="Value" DataValueField="Key" AppendDataBoundItems="true" DataSource='<%# AgencyTypes %>'       SelectedValue='<%# Bind("TypeValue") %>'>
                                            <asp:ListItem Text="-- Select Agency Type --" Value="0" />
                                        </asp:DropDownList>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorType" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="Type" ControlToValidate="dropDownListType" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorType_ValueConvert" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="labelIsActive" AssociatedControlID="checkBoxIsActive">Is Active:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="checkBoxIsActive" Checked='<%# Bind("IsActive") %>' Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="required">*</span>&nbsp;Service Areas:
                                    </td>
                                    <td>
                                        <div id="serviceAreaSelection">
                                            <table id="serviceAreas">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:Label runat="server" ID="labelCounties" AssociatedControlID="listBoxCounties" Text="Counties"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="labelServiceAreas" AssociatedControlID="listBoxServiceAreas" Text="Service Areas"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="2">
                                                            <%--Bind the datasource to the control on the fly.--%>
                                                            <asp:ListBox ID="listBoxCounties" runat="server" SelectionMode="Multiple" DataTextField="Value" DataValueField="Key" DataSource='<%# Counties %>' CssClass="src" Height="150px" Width="150px"></asp:ListBox>
                                                        </td>
                                                        <td style="text-align: center; vertical-align: bottom;">
                                                            <input id="buttonAddServiceArea" type="button" value=">>" class="add formbutton1" />
                                                            
                                                        </td>
                                                        <td rowspan="2">
                                                            <asp:ListBox ID="listBoxServiceAreas" runat="server" SelectionMode="Multiple" CssClass="dst" Height="150px" Width="150px" DataTextField="Value" DataValueField="Key" DataSource='<%# Counties %>'></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; vertical-align: top;">
                                                        <input id="buttonRemoveServiceArea" type="button" value="<<" class="remove formbutton1" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <asp:HiddenField runat="server" ID="hiddenServiceAreas" Value='<%# Bind("ServiceAreas") %>' />
                                        </div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                    </td>
                                    <td>
                                    <asp:CustomValidator runat="server" ID="validatorServiceAreas" ControlToValidate="dropDownListType" ErrorMessage="Service Areas are required." OnServerValidate="validatorServiceAreas_ServerValidate" Display="Dynamic" CssClass="required" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table class="formTable">
                            <thead>
                                <tr>
                                    <th colspan="2" style="text-align: left;">
                                        Physical Address
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelPhysicalAddress1" AssociatedControlID="textBoxPhysicalAddress1" Text="Address 1:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxPhysicalAddress1" MaxLength="100" Text='<%# Bind("PhysicalAddress1") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorPhysicalAddress1" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="PhysicalAddress1" ControlToValidate="textBoxPhysicalAddress1" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelPhysicalAddress2" AssociatedControlID="textBoxPhysicalAddress2" Text="Address 2:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxPhysicalAddress2" MaxLength="70" Text='<%# Bind("PhysicalAddress2") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorPhysicalAddress2" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="PhysicalAddress2" ControlToValidate="textBoxPhysicalAddress2" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelPhysicalCity" AssociatedControlID="textBoxPhysicalCity" Text="City:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxPhysicalCity" MaxLength="50" Text='<%# Bind("PhysicalCity") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorPhysicalCity" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="PhysicalCity" ControlToValidate="textBoxPhysicalCity" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelPhysicalZIP" AssociatedControlID="textBoxPhysicalZIP" Text="ZIP:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxPhysicalZip" MaxLength="10" Text='<%# Bind("PhysicalZip") %>' CssClass="zip" ></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorPhysicalZip" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="PhysicalZip" ControlToValidate="textBoxPhysicalZip" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelPhysicalCounty" AssociatedControlID="dropDownListPhysicalCounty" Text="County:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dropDownListPhysicalCounty" DataTextField="Value" DataValueField="Key" DataSource='<%# Counties %>' AppendDataBoundItems="true" SelectedValue='<%# Bind("PhysicalCountyFIPS") %>'>
                                            <asp:ListItem Text="-- Select County --" Value="" />
                                        </asp:DropDownList>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorPhysicalCounty" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="PhysicalCountyFIPS" ControlToValidate="dropDownListPhysicalCounty" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                             <%--   Added By Lavanya--%>
                                <tr style="visibility:hidden;" >
                                    <td colspan="2">                                        
                                        <asp:HiddenField ID="hdnState" runat="server" Value='<%# Bind("StateName") %>'  />
                                        <asp:HiddenField ID="AgencyLongitude" runat="server" Value='<%# Bind("Longitude") %>' />
                                        <asp:HiddenField ID="AgencyLatitude" runat="server" Value='<%# Bind("Latitude") %>' />
                                    </td>
                                </tr>
                               <%-- end--%>
                            </tbody>
                        </table>
                        <table class="formTable">
                            <thead>
                                <tr>
                                    <th colspan="2" style="text-align: left;">
                                        Mailing Address
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelMailingAddress1" AssociatedControlID="textBoxMailingAddress1" Text="Address 1:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxMailingAddress1" MaxLength="100" Text='<%# Bind("MailingAddress1") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorMailingAddress1" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="MailingAddress1" ControlToValidate="textBoxMailingAddress1" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelMailingAddress2" AssociatedControlID="textBoxMailingAddress2" Text="Address 2:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxMailingAddress2" MaxLength="70" Text='<%# Bind("MailingAddress2") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorMailingAddress2" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="MailingAddress2" ControlToValidate="textBoxMailingAddress2" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelMailingCity" AssociatedControlID="textBoxMailingCity" Text="City:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxMailingCity" MaxLength="50" Text='<%# Bind("MailingCity") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorMailingCity" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="MailingCity" ControlToValidate="textBoxMailingCity" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelMailingState" AssociatedControlID="dropDownListMailingState" Text="State:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dropDownListMailingState" DataTextField="Value" DataValueField="Key" DataSource='<%# States %>' AppendDataBoundItems="true" SelectedValue='<%# Bind("MailingStateValue") %>'>
                                            <asp:ListItem Text="-- Select State --" Value="" />
                                        </asp:DropDownList>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorMailingState" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="MailingStateValue" ControlToValidate="dropDownListMailingState" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorMailingState_ValueConvert" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelMailingZIP" AssociatedControlID="textBoxMailingZIP" Text="ZIP:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxMailingZip" MaxLength="10" Text='<%# Bind("MailingZip") %>' CssClass="zip"></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorMailingZip" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="MailingZip" ControlToValidate="textBoxMailingZip" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table class="formTable">
                            <thead>
                                <tr>
                                    <th colspan="2" style="text-align: left;">
                                        Contact Person
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelSponsorFirstName" AssociatedControlID="textBoxSponsorFirstName" Text="First Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxSponsorFirstName" MaxLength="50" Text='<%# Bind("SponsorFirstName") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorSponsorFirstName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="SponsorFirstName" ControlToValidate="textBoxSponsorFirstName" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelSponsorMiddleName" AssociatedControlID="textBoxSponsorMiddleName" Text="Middle Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxSponsorMiddleName" MaxLength="50" Text='<%# Bind("SponsorMiddleName") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorSponsorMiddleName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="SponsorMiddleName" ControlToValidate="textBoxSponsorMiddleName" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelSponsorLastName" AssociatedControlID="textBoxSponsorLastName" Text="Last Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxSponsorLastName" MaxLength="50" Text='<%# Bind("SponsorLastName") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorSponsorLastName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="SponsorLastName" ControlToValidate="textBoxSponsorLastName" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelSponsorTitle" AssociatedControlID="textBoxSponsorTitle" Text="Title:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxSponsorTitle" MaxLength="50" Text='<%# Bind("SponsorTitle") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorSponsorTitle" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="SponsorTitle" ControlToValidate="textBoxSponsorTitle" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table class="formTable">
                            <thead>
                                <tr>
                                    <th colspan="2" style="text-align: left;">
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td nowrap style="width: 125px;">
                                        <asp:Label runat="server" ID="labelHoursOfOperation" AssociatedControlID="textBoxHoursOfOperation" Text="Hours of Operation:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="textBoxHoursOfOperation" runat="server" MaxLength="150" Text='<%# Bind("HoursOfOperation") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorHoursOfOperation" runat="server" ControlToValidate="textBoxHoursOfOperation" CssClass="required" Display="Dynamic" DisplayMode="List" PropertyName="HoursOfOperation" RulesetName="Data" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <span class="required">*</span>&nbsp;<asp:Label ID="labelPrimaryPhone" runat="server" AssociatedControlID="textBoxPrimaryPhone" Text="Primary Phone:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="textBoxPrimaryPhone" runat="server" CssClass="phone" MaxLength="20" Text='<%# Bind("PrimaryPhone") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorPrimaryPhone" runat="server" ControlToValidate="textBoxPrimaryPhone" CssClass="required" Display="Dynamic" DisplayMode="List" PropertyName="PrimaryPhone" RulesetName="Data" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelSecondaryPhone" AssociatedControlID="textBoxSecondaryPhone" Text="Secondary Phone:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxSecondaryPhone" MaxLength="20" Text='<%# Bind("SecondaryPhone") %>' CssClass="phone"></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorSecondaryPhone" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="SecondaryPhone" ControlToValidate="textBoxSecondaryPhone" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelPrimaryEmail" AssociatedControlID="textBoxPrimaryEmail" Text="Primary Email:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxPrimaryEmail" MaxLength="100" Text='<%# Bind("PrimaryEmail") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorPrimaryEmail" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="PrimaryEmail" ControlToValidate="textBoxPrimaryEmail" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorEmptyStringToNull_ValueConvert"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelSecondaryEmail" AssociatedControlID="textBoxSecondaryEmail" Text="Secondary Email:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxSecondaryEmail" MaxLength="100" Text='<%# Bind("SecondaryEmail") %>'></asp:TextBox>
                                        <br />
                                       <pp:PropertyProxyValidator ID="proxyValidatorSecondaryEmail" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="SecondaryEmail" ControlToValidate="textBoxSecondaryEmail" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorEmptyStringToNull_ValueConvert"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelTollFreePhone" AssociatedControlID="textBoxTollFreePhone" Text="Toll Free Phone:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxTollFreePhone" MaxLength="20" Text='<%# Bind("TollFreePhone") %>' CssClass="phone"></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorTollFreePhone" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="TollFreePhone" ControlToValidate="textBoxTollFreePhone" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelTDD" AssociatedControlID="textBoxTDD" Text="TDD:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxTDD" MaxLength="20" Text='<%# Bind("TDD") %>' CssClass="phone"></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorTDD" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="TDD" ControlToValidate="textBoxTDD" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelTollFreeTDD" AssociatedControlID="textBoxTollFreeTDD" Text="Toll Free TDD:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxTollFreeTDD" MaxLength="20" Text='<%# Bind("TollFreeTDD") %>' CssClass="phone"></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorTollFreeTDD" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="TollFreeTDD" ControlToValidate="textBoxTollFreeTDD" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelFax" AssociatedControlID="textBoxFax" Text="Fax:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxFax" MaxLength="20" Text='<%# Bind("Fax") %>' CssClass="phone"></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorFax" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="Fax" ControlToValidate="textBoxFax" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelURL" AssociatedControlID="textBoxURL" Text="Website URL:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxURL" MaxLength="150" Text='<%# Bind("URL") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorURL" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="URL" ControlToValidate="textBoxURL" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorEmptyStringToNull_ValueConvert" />
                                    </td>
                                </tr>
                                <%--Added by Lavanya--%>
                                <tr>
                                    <td nowrap>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelAvailableLanguages" AssociatedControlID="textBoxAvailableLanguages" Text="Available Languages:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxAvailableLanguages" MaxLength="150" Text='<%# Bind("AvailableLanguages") %>'></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorAvailableLanguages" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="AvailableLanguages" ControlToValidate="textBoxAvailableLanguages" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                                <%--end--%>
                                <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelComments" AssociatedControlID="textBoxComments" Text="Comments:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="textBoxComments" Text='<%# Bind("Comments") %>' TextMode="MultiLine" Columns="20" Rows="2" CssClass="comment"></asp:TextBox>
                                        <br />
                                        <pp:PropertyProxyValidator ID="proxyValidatorComments" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" PropertyName="Comments" ControlToValidate="textBoxComments" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelHide" AssociatedControlID="chkHideAgency" Text="Hide Agency From Public:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkHideAgency" runat="server" Checked='<%# Bind("HideAgencyFromPublic") %>' Text="" />
                                        <br />
                                    </td>
                                </tr>
                                  <%--Added by Lavanya--%>
                                 <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelHideFromSearch" AssociatedControlID="chkHideAgencyFromSearch" Text="Hide Agency From Search:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkHideAgencyFromSearch" runat="server" Checked='<%# Bind("HideAgencyFromSearch") %>' />
                                        <br />
                                    </td>
                                </tr>
                                <%--end--%>
                                <tr>
                                    <td colspan="2">
                                        <div class="commands">
                                            <asp:Button runat="server" ID="buttonSubmit" Text="Submit" CssClass="formbutton3" OnClick="buttonSubmit_Click" CausesValidation="false" />
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </div>
                            
                </EditItemTemplate>
            </asp:FormView>
        </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceEditAgency" runat="server" 
    DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.EditAgencyViewData" 
    OnSelecting="dataSourceEditAgency_Selecting" 
    OnUpdated="dataSourceEditAgency_Updated" 
    OnUpdating="dataSourceEditAgency_Updating" />
</asp:Content>
