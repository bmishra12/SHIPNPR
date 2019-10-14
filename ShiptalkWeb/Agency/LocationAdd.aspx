<%@ Page Title="" Language="C#" MasterPageFile="~/ShiptalkWeb.Master" AutoEventWireup="true" CodeBehind="LocationAdd.aspx.cs" Inherits="ShiptalkWeb.Agency.LocationAdd" %>

<%@ Import Namespace="ShiptalkWeb.Routing" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects.UI" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Import Namespace="ShiptalkWeb" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>Add Location</title>



    <script type="text/javascript" src="../../../scripts/jquery-ui-mask-1.7.2.js"></script>

    <script type="text/javascript" src="../../../scripts/jquery.charcounter.js"></script>

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
        
        $(document).ready(function() {
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
                agencyState = $("[id$='_dropDownListState']");
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

                // alert(address1 + ' ' + address2 + ' ' + city + ' ' + state + ' ' + zip);

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
            <h1>
                Add a new Location</h1>
            <div>
                <asp:FormView runat="server" ID="formViewAgencyLocation" DataSourceID="dataSourceAgencyLocation" DefaultMode="Insert" Width="100%">
                    <InsertItemTemplate>
                        <p>
                            (Items marked in <span class="required">*</span> indicate required fields.)</p>
                        <div id="dv3colFormContent" style="margin-top: 10px; width: 615px; border-top: solid 2px #eee;">
                            <table class="formTable">
                                <tbody>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            State:
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="dropDownListState" DataTextField="Value" DataValueField="Key" DataSource='<%# States %>' SelectedValue='<%# (DefaultState.StateAbbr == "CM") ? string.Empty : DefaultState.StateAbbr %>' AutoPostBack="true" OnSelectedIndexChanged="dropDownListState_SelectedIndexChanged" Enabled='<%# (Scope == Scope.CMS && IsAdmin) %>' AppendDataBoundItems="true">
                                            <asp:ListItem Text="-- Select State --" Value="" />
                                            </asp:DropDownList>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelLocationName" AssociatedControlID="textBoxLocationName">Location Name:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxLocationName" MaxLength="100" Text='<%# Bind("LocationName") %>'></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorLocationName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="LocationName" ControlToValidate="textBoxLocationName" RulesetName="Data" CssClass="required" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <h4>
                            Physical Address
                            </h4>
                            <table class="formTable">
                                
                                <tbody>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelPhysicalAddress1" AssociatedControlID="textBoxPhysicalAddress1" Text="Address 1:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxPhysicalAddress1" MaxLength="100" Text='<%# Bind("PhysicalAddress1") %>'></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorPhysicalAddress1" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="PhysicalAddress1" ControlToValidate="textBoxPhysicalAddress1" RulesetName="Data" CssClass="required" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <asp:Label runat="server" ID="labelPhysicalAddress2" AssociatedControlID="textBoxPhysicalAddress2" Text="Address 2:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxPhysicalAddress2" MaxLength="70" Text='<%# Bind("PhysicalAddress2") %>'></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorPhysicalAddress2" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="PhysicalAddress2" ControlToValidate="textBoxPhysicalAddress2" RulesetName="Data" CssClass="required" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelPhysicalCity" AssociatedControlID="textBoxPhysicalCity" Text="City:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxPhysicalCity" MaxLength="50" Text='<%# Bind("PhysicalCity") %>'></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorPhysicalCity" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="PhysicalCity" ControlToValidate="textBoxPhysicalCity" RulesetName="Data" CssClass="required" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelPhysicalZip" AssociatedControlID="textBoxPhysicalZip" Text="ZIP:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxPhysicalZip" MaxLength="10" Text='<%# Bind("PhysicalZip") %>' CssClass="zip"></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorPhysicalZip" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="PhysicalZip" ControlToValidate="textBoxPhysicalZip" RulesetName="Data" CssClass="required" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelPhysicalCounty" AssociatedControlID="dropDownListPhysicalCounty" Text="County:"></asp:Label>
                                        </td>
                                        <td>
                                            <%--Bind the datasource to the control on the fly.--%>
                                            <asp:DropDownList runat="server" ID="dropDownListPhysicalCounty" DataTextField="Value" DataValueField="Key" DataSource='<%# Counties %>' AppendDataBoundItems="true" SelectedValue='<%# Bind("PhysicalCounty") %>'>
                                                <asp:ListItem Text="-- Select County --" Value="" />
                                            </asp:DropDownList>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorPhysicalCounty" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="PhysicalCounty" ControlToValidate="dropDownListPhysicalCounty" RulesetName="Data" CssClass="required" />
                                        </td>
                                    </tr>
                              <%--Added By Lavanya--%>
                                <tr style="visibility:hidden;" >
                                    <td colspan="2"> 
                                        <asp:HiddenField ID="AgencyLongitude" runat="server" Value='<%# Bind("Longitude") %>' />
                                        <asp:HiddenField ID="AgencyLatitude" runat="server" Value='<%# Bind("Latitude") %>' />
                                    </td>
                                </tr>
                               <%-- end--%>
                                </tbody>
                            </table>
                             <h4>
                            Mailing Address
                            </h4>
                            <table class="formTable">
                               
                                <tbody>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelMailingAddress1" AssociatedControlID="textBoxMailingAddress1" Text="Address 1:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxMailingAddress1" MaxLength="100" Text='<%# Bind("MailingAddress1") %>'></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorMailingAddress1" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="MailingAddress1" ControlToValidate="textBoxMailingAddress1" RulesetName="Data" CssClass="required" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <asp:Label runat="server" ID="labelMailingAddress2" AssociatedControlID="textBoxMailingAddress2" Text="Address 2:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxMailingAddress2" MaxLength="70" Text='<%# Bind("MailingAddress2") %>'></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorMailingAddress2" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="MailingAddress2" ControlToValidate="textBoxMailingAddress2" RulesetName="Data" CssClass="required" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelMailingCity" AssociatedControlID="textBoxMailingCity" Text="City:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxMailingCity" MaxLength="50" Text='<%# Bind("MailingCity") %>'></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorMailingCity" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="MailingCity" ControlToValidate="textBoxMailingCity" RulesetName="Data" CssClass="required" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelMailingState" AssociatedControlID="dropDownListMailingState" Text="State:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="dropDownListMailingState" DataTextField="Value" DataValueField="Key" DataSource='<%# States %>' AppendDataBoundItems="true" SelectedValue='<%# Bind("MailingState") %>'>
                                                <asp:ListItem Text="-- Select State --" Value="" />
                                            </asp:DropDownList>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorMailingState" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="MailingState" ControlToValidate="dropDownListMailingState" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorMailingState_ValueConvert" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelMailingZip" AssociatedControlID="textBoxMailingZip" Text="Zip:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxMailingZip" MaxLength="10" Text='<%# Bind("MailingZip") %>' CssClass="zip"></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorMailingZip" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="MailingZip" ControlToValidate="textBoxMailingZip" RulesetName="Data" CssClass="required" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                             <h4>
                            Contact Person
                            </h4>
                            <table class="formTable">
                                
                                <tbody>
                                    <tr>
                                        <td nowrap style="width: 125px;">
                                            <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelContactFirstName" AssociatedControlID="textBoxContactFirstName" Text="First Name:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxContactFirstName" MaxLength="50" Text='<%# Bind("ContactFirstName") %>'></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorContactFirstName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="ContactFirstName" ControlToValidate="textBoxContactFirstName" RulesetName="Data" CssClass="required" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <asp:Label runat="server" ID="labelContactMiddleName" AssociatedControlID="textBoxContactMiddleName" Text="Middle Name:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxContactMiddleName" MaxLength="50" Text='<%# Bind("ContactMiddleName") %>'></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorContactMiddleName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="ContactMiddleName" ControlToValidate="textBoxContactMiddleName" RulesetName="Data" CssClass="required" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelContactLastName" AssociatedControlID="textBoxContactLastName" Text="Last Name:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxContactLastName" MaxLength="50" Text='<%# Bind("ContactLastName") %>'></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorContactLastName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="ContactLastName" ControlToValidate="textBoxContactLastName" RulesetName="Data" CssClass="required" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelContactTitle" AssociatedControlID="textBoxContactTitle" Text="Title:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxContactTitle" MaxLength="50" Text='<%# Bind("ContactTitle") %>'></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorContactTitle" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="ContactTitle" ControlToValidate="textBoxContactTitle" RulesetName="Data" CssClass="required" />
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
                                            <pp:PropertyProxyValidator ID="proxyValidatorHoursOfOperation" runat="server" ControlToValidate="textBoxHoursOfOperation" CssClass="required" Display="Dynamic" DisplayMode="List" PropertyName="HoursOfOperation" RulesetName="Data" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <span class="required">*</span>&nbsp;<asp:Label ID="labelPrimaryPhone" runat="server" AssociatedControlID="textBoxPrimaryPhone" Text="Primary Phone:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="textBoxPrimaryPhone" runat="server" CssClass="phone" MaxLength="20" Text='<%# Bind("PrimaryPhone") %>'></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorPrimaryPhone" runat="server" ControlToValidate="textBoxPrimaryPhone" CssClass="required" Display="Dynamic" DisplayMode="List" PropertyName="PrimaryPhone" RulesetName="Data" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <asp:Label runat="server" ID="labelSecondaryPhone" AssociatedControlID="textBoxSecondaryPhone" Text="Secondary Phone:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxSecondaryPhone" MaxLength="20" Text='<%# Bind("SecondaryPhone") %>' CssClass="phone"></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorSecondaryPhone" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="SecondaryPhone" ControlToValidate="textBoxSecondaryPhone" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelPrimaryEmail" AssociatedControlID="textBoxPrimaryEmail" Text="Primary Email:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxPrimaryEmail" MaxLength="100" Text='<%# Bind("PrimaryEmail") %>'></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorPrimaryEmail" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="PrimaryEmail" ControlToValidate="textBoxPrimaryEmail" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorEmptyStringToNull_ValueConvert" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <asp:Label runat="server" ID="labelSecondaryEmail" AssociatedControlID="textBoxSecondaryEmail" Text="Secondary Email:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxSecondaryEmail" MaxLength="100" Text='<%# Bind("SecondaryEmail") %>'></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorSecondaryEmail" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="SecondaryEmail" ControlToValidate="textBoxSecondaryEmail" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorEmptyStringToNull_ValueConvert" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <asp:Label runat="server" ID="labelTollFreePhone" AssociatedControlID="textBoxTollFreePhone" Text="Toll Free Phone:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxTollFreePhone" MaxLength="20" Text='<%# Bind("TollFreePhone") %>' CssClass="phone"></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorTollFreePhone" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="TollFreePhone" ControlToValidate="textBoxTollFreePhone" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <asp:Label runat="server" ID="labelTDD" AssociatedControlID="textBoxTDD" Text="TDD:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxTDD" MaxLength="20" Text='<%# Bind("TDD") %>' CssClass="phone"></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorTDD" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="TDD" ControlToValidate="textBoxTDD" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <asp:Label runat="server" ID="labelTollFreeTDD" AssociatedControlID="textBoxTollFreeTDD" Text="Toll Free TDD:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxTollFreeTDD" MaxLength="20" Text='<%# Bind("TollFreeTDD") %>' CssClass="phone"></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorTollFreeTDD" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="TollFreeTDD" ControlToValidate="textBoxTollFreeTDD" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <asp:Label runat="server" ID="labelFax" AssociatedControlID="textBoxFax" Text="Fax:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxFax" MaxLength="20" Text='<%# Bind("Fax") %>' CssClass="phone"></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorFax" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="Fax" ControlToValidate="textBoxFax" RulesetName="Data" CssClass="required" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <asp:Label runat="server" ID="labelComments" AssociatedControlID="textBoxComments" Text="Comments:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="textBoxComments" Text='<%# Bind("Comments") %>' TextMode="MultiLine" Columns="20" Rows="2" CssClass="comment"></asp:TextBox>
                                            <br />
                                            <pp:PropertyProxyValidator ID="proxyValidatorComments" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="Comments" ControlToValidate="textBoxComments" RulesetName="Data" CssClass="required" />
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
                                        <pp:PropertyProxyValidator ID="proxyValidatorAvailableLanguages" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" PropertyName="AvailableLanguages" ControlToValidate="textBoxAvailableLanguages" RulesetName="Data" CssClass="required" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelHideFromSearch" AssociatedControlID="chkHideAgencyFromSearch" Text="Hide Agency From Search:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkHideAgencyFromSearch" runat="server" Checked='<%# Bind("HideAgencyFromSearch") %>' Text="" />
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
                    </InsertItemTemplate>
                </asp:FormView>
            </div>
        </div>
        <pp:ObjectContainerDataSource ID="dataSourceAgencyLocation" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.AddAgencyLocationViewData" OnInserting="dataSourceAgencyLocation_Inserting" OnInserted="dataSourceAgencyLocation_Inserted" />
</asp:Content>
