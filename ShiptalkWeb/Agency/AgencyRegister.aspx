<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ShiptalkWeb.Master" CodeBehind="AgencyRegister.aspx.cs" Inherits="ShiptalkWeb.Agency.AgencyRegister" %>

<%@ Import Namespace="ShiptalkWeb" %>
<%@ Import Namespace="ShiptalkLogic.BusinessObjects" %>
<%@ Register Assembly="Microsoft.Practices.Web.UI.WebControls" Namespace="Microsoft.Practices.Web.UI.WebControls" TagPrefix="pp" %>
<%@ Register Assembly="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" Namespace="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.AspNet" TagPrefix="pp" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <title>Register a new Agency</title>

       

    <script src="../../../scripts/json2.js" type="text/javascript"></script>

        <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?v=3.exp&amp;sensor=false"></script>

    <script type="text/javascript" src="../../../scripts/jquery-ui-mask-1.7.2.js"></script>

    <script type="text/javascript" src="../../../scripts/jquery.charcounter.js"></script>  
    

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



            $("[id$='_CheckBox1']").click(function () {
                copyPhysicalAddToMailingAdd();
            }
          );



            function copyPhysicalAddToMailingAdd() {
                var add1 = $("[id$='_textBoxPhysicalAddress1']").val();
                var add2 = $("[id$='_textBoxPhysicalAddress2']").val();
                var city = $("[id$='_textBoxPhysicalCity']").val();
                var zip = $("[id$='_textBoxPhysicalZIP']").val();
                var state = $("[id$='_dropDownListState']").val();


                if ($("[id$='_CheckBox1']").attr("checked") == true) {
                    $("[id$='_textBoxMailingAddress1']").val(add1)
                    $("[id$='_textBoxMailingAddress2']").val(add2);
                    $("[id$='_textBoxMailingCity']").val(city);
                    $("[id$='_textBoxMailingZIP']").val(zip);
                    $("[id$='_dropDownListMailingState']").val(state);
                }

                else {
                    $("[id$='_textBoxMailingAddress1']").val("")
                    $("[id$='_textBoxMailingAddress2']").val("");
                    $("[id$='_textBoxMailingCity']").val("");
                    $("[id$='_textBoxMailingZIP']").val("");
                    $("[id$='_dropDownListMailingState']").val("");
                }




            }




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
                            // alert("Geocode was not successful for the following reason: " + status);
                         }
                     });

            });

        }             

       

        
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">
    <div id="maincontent">
        <div class="dv3col">
            <h1>
                Register a new Agency</h1>
            <div>
                
                <asp:FormView runat="server" ID="formViewAgency" DataSourceID="dataSourceAgency" DefaultMode="Insert" Width="100%">
                    <InsertItemTemplate>
                        <p>
                            (Items marked in <span class="required">*</span> indicate required fields.)</p>
                            <div id="dv3colFormContent" class="section">
                        <table class="formTable">
                            <tbody>
                                <tr>
                                    <td>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelState" AssociatedControlID="dropDownListState">State:</asp:Label></td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dropDownListState" CssClass="dropdown1wm" DataTextField="Value" DataValueField="Key" DataSource='<%# States %>' SelectedValue='<%# (DefaultState.StateAbbr == "CM") ? string.Empty : DefaultState.StateAbbr %>' AutoPostBack="true" OnSelectedIndexChanged="dropDownListState_SelectedIndexChanged" Enabled='<%# (Scope == Scope.CMS && IsAdmin) %>' AppendDataBoundItems="true">
                                         <asp:ListItem Text="-- Select State --" Value="" />
                                        </asp:DropDownList>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelName" AssociatedControlID="textBoxName">Agency Name:</asp:Label></td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="Name" ControlToValidate="textBoxName" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxName" MaxLength="100" Text='<%# Bind("Name") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelType" AssociatedControlID="dropDownListType">Agency Type:</asp:Label></td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorType" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="Type" ControlToValidate="dropDownListType" RulesetName="Data" CssClass="validationMessage" OnValueConvert="proxyValidatorType_ValueConvert" />
                                        <asp:DropDownList runat="server" ID="dropDownListType" DataTextField="Value" CssClass="dropdown1wm" DataValueField="Key" AppendDataBoundItems="true" DataSource='<%# AgencyTypes %>' SelectedValue='<%# Bind("Type") %>'>
                                            <asp:ListItem Text="-- Select Agency Type --" Value="0" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="required">*</span>&nbsp;Service Areas:
                                    </td>
                                    <td>
                                        <div id="serviceAreaSelection">
                                            <asp:CustomValidator runat="server" ID="validatorServiceAreas" ControlToValidate="dropDownListState" ErrorMessage="Service Areas are required." OnServerValidate="validatorServiceAreas_ServerValidate" Display="Dynamic" CssClass="validationMessage" />
                                            <table id="serviceAreas">
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
                                                            <asp:ListBox ID="listBoxCounties" runat="server" SelectionMode="Multiple" DataTextField="Value" DataValueField="Key" DataSource='<%# Counties %>' CssClass="src dropdown1wm" Height="150px" Width="150px"></asp:ListBox>
                                                        </td>
                                                        <td style="text-align: center; vertical-align: bottom;">
                                                            <input id="buttonAddServiceArea" type="button" value=">>" class="add formbutton1" />
                                                            
                                                        </td>
                                                        <td rowspan="2">
                                                            <asp:ListBox ID="listBoxServiceAreas" runat="server" SelectionMode="Multiple" CssClass="dst dropdown1wm" Height="150px" Width="150px" DataTextField="Value" DataValueField="Key" DataSource='<%# Counties %>'></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; vertical-align: top;">
                                                        <input id="buttonRemoveServiceArea" type="button" value="<<" class="remove formbutton1" />
                                                        </td>
                                                    </tr>
                                            </table>
                                            <asp:HiddenField runat="server" ID="hiddenServiceAreas" Value='<%# Bind("ServiceAreas") %>' />
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        
                        <br /><br />
                        <div class="formAltRow">
                        <table class="formTable">
                            <thead>
                                <td></td>
                                <td colspan="2" class="tdHeader"><h4>Physical Address</h4></td>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="tdFormRequiredLabel"><span class="required">*</span></td>
                                    <td  class="tdFormLabel">
                                        <asp:Label runat="server" ID="labelPhysicalAddress1" AssociatedControlID="textBoxPhysicalAddress1" Text="Address 1:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorPhysicalAddress1" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="PhysicalAddress1" ControlToValidate="textBoxPhysicalAddress1" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxPhysicalAddress1" MaxLength="100" Text='<%# Bind("PhysicalAddress1") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelPhysicalAddress2" AssociatedControlID="textBoxPhysicalAddress2" Text="Address 2:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorPhysicalAddress2" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="PhysicalAddress2" ControlToValidate="textBoxPhysicalAddress2" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxPhysicalAddress2" MaxLength="70" Text='<%# Bind("PhysicalAddress2") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="required">*</span></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelPhysicalCity" AssociatedControlID="textBoxPhysicalCity" Text="City:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorPhysicalCity" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="PhysicalCity" ControlToValidate="textBoxPhysicalCity" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxPhysicalCity" MaxLength="50" Text='<%# Bind("PhysicalCity") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="required">*</span></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelPhysicalZIP" AssociatedControlID="textBoxPhysicalZIP" Text="ZIP:" ></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorPhysicalZIP" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="PhysicalZIP" ControlToValidate="textBoxPhysicalZIP" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxPhysicalZIP" MaxLength="10" Text='<%# Bind("PhysicalZIP") %>'  CssClass="textfield3 zip" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="required">*</span></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelPhysicalCounty" AssociatedControlID="dropDownListPhysicalCounty" Text="County:"></asp:Label>
                                    </td>
                                    <td>
                                        <%--Bind the datasource to the control on the fly.--%>
                                        <pp:PropertyProxyValidator ID="proxyValidatorPhysicalCounty" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="PhysicalCounty" ControlToValidate="dropDownListPhysicalCounty" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:DropDownList runat="server" ID="dropDownListPhysicalCounty" CssClass="dropdown1wm" DataTextField="Value" DataValueField="Key" DataSource='<%# Counties %>' AppendDataBoundItems="true" SelectedValue='<%# Bind("PhysicalCounty") %>'>
                                            <asp:ListItem Text="-- Select County --" Value="" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <%--   Added By Lavanya--%>
                                <tr style="visibility:hidden;" >
                                    <td colspan="3"> 
                                        <asp:HiddenField ID="AgencyLongitude" runat="server" Value='<%# Bind("Longitude") %>' />
                                        <asp:HiddenField ID="AgencyLatitude" runat="server" Value='<%# Bind("Latitude") %>' />
                                    </td>
                                </tr>
                               <%-- end--%>
                            </tbody>
                        </table>
                        </div>
                        
                        <br /><br />
                        <table class="formTable">
                            <asp:CheckBox ID="CheckBox1" runat="server"  Text ="same as physical address" />
                     <br />
                            <thead>
                                <td>      </td>
                                <td colspan="3" class="tdHeader"><h4>Mailing Address</h4></td>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="tdFormRequiredLabel"><span class="required">*</span></td>
                                    <td class="tdFormLabel">
                                        <asp:Label runat="server" ID="labelMailingAddress1" AssociatedControlID="textBoxMailingAddress1" Text="Address 1:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorMailingAddress1" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="MailingAddress1" ControlToValidate="textBoxMailingAddress1" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxMailingAddress1" MaxLength="100" Text='<%# Bind("MailingAddress1") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="required">*</span></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelMailingAddress2" AssociatedControlID="textBoxMailingAddress2" Text="Address 2:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorMailingAddress2" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="MailingAddress2" ControlToValidate="textBoxMailingAddress2" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxMailingAddress2" MaxLength="70" Text='<%# Bind("MailingAddress2") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="required">*</span></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelMailingCity" AssociatedControlID="textBoxMailingCity" Text="City:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorMailingCity" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="MailingCity" ControlToValidate="textBoxMailingCity" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxMailingCity" MaxLength="50" Text='<%# Bind("MailingCity") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="required">*</span></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelMailingState" AssociatedControlID="dropDownListMailingState" Text="State:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorMailingState" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="MailingState" ControlToValidate="dropDownListMailingState" RulesetName="Data" CssClass="validationMessage" OnValueConvert="proxyValidatorMailingState_ValueConvert" />
                                        <asp:DropDownList runat="server" ID="dropDownListMailingState" CssClass="dropdown1wm" DataTextField="Value" DataValueField="Key" DataSource='<%# States %>' AppendDataBoundItems="true" SelectedValue='<%# Bind("MailingState") %>'>
                                            <asp:ListItem Text="-- Select State --" Value="" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="required">*</span></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelMailingZIP" AssociatedControlID="textBoxMailingZIP" Text="ZIP:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorMailingZIP" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="MailingZIP" ControlToValidate="textBoxMailingZIP" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxMailingZIP" MaxLength="10" Text='<%# Bind("MailingZIP") %>' CssClass="textfield3 zip"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        
                        <br /><br />
                        <div class="formAltRow">
                        <table class="formTable">
                            <thead>
                                <td></td>
                               <td colspan="2" class="tdHeader"><h4>Contact Person</h4></td>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="tdFormRequiredLabel"><span class="required">*</span></td>
                                    <td class="tdFormLabel">
                                        <asp:Label runat="server" ID="labelSponsorFirstName" AssociatedControlID="textBoxSponsorFirstName" Text="First Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorSponsorFirstName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="SponsorFirstName" ControlToValidate="textBoxSponsorFirstName" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxSponsorFirstName" MaxLength="50" Text='<%# Bind("SponsorFirstName") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelSponsorMiddleName" AssociatedControlID="textBoxSponsorMiddleName" Text="Middle Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorSponsorMiddleName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="SponsorMiddleName" ControlToValidate="textBoxSponsorMiddleName" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxSponsorMiddleName" MaxLength="50" Text='<%# Bind("SponsorMiddleName") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="required">*</span></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelSponsorLastName" AssociatedControlID="textBoxSponsorLastName" Text="Last Name:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorSponsorLastName" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="SponsorLastName" ControlToValidate="textBoxSponsorLastName" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxSponsorLastName" MaxLength="50" Text='<%# Bind("SponsorLastName") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="required">*</span></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelSponsorTitle" AssociatedControlID="textBoxSponsorTitle" Text="Title:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorSponsorTitle" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="SponsorTitle" ControlToValidate="textBoxSponsorTitle" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxSponsorTitle" MaxLength="50" Text='<%# Bind("SponsorTitle") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </div>
                        
                        <br /><br />
                        <table class="formTable">
                            <tbody>
                                <tr>
                                    <td class="tdFormRequiredLabel"></td>
                                    <td class="tdFormLabel">
                                        <asp:Label runat="server" ID="labelHoursOfOperation" AssociatedControlID="textBoxHoursOfOperation" Text="Hours of Operation:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorHoursOfOperation" runat="server" ControlToValidate="textBoxHoursOfOperation" CssClass="validationMessage" Display="Dynamic" DisplayMode="List" PropertyName="HoursOfOperation" RulesetName="Data" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" />
                                        <asp:TextBox ID="textBoxHoursOfOperation" runat="server" MaxLength="150" Text='<%# Bind("HoursOfOperation") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="required">*</span></td>
                                    <td>
                                        <asp:Label ID="labelPrimaryPhone" runat="server" AssociatedControlID="textBoxPrimaryPhone" Text="Primary Phone:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorPrimaryPhone" runat="server" ControlToValidate="textBoxPrimaryPhone" CssClass="validationMessage" Display="Dynamic" DisplayMode="List" PropertyName="PrimaryPhone" RulesetName="Data" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert"/>
                                        <asp:TextBox ID="textBoxPrimaryPhone" runat="server" MaxLength="20" Text='<%# Bind("PrimaryPhone") %>' CssClass="textfield3 phone"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelSecondaryPhone" AssociatedControlID="textBoxSecondaryPhone" Text="Secondary Phone:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorSecondaryPhone" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="SecondaryPhone" ControlToValidate="textBoxSecondaryPhone" RulesetName="Data" CssClass="validationMessage" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert" />
                                        <asp:TextBox runat="server" ID="textBoxSecondaryPhone" MaxLength="20" Text='<%# Bind("SecondaryPhone") %>' CssClass="textfield3 phone"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="required">*</span></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelPrimaryEmail" AssociatedControlID="textBoxPrimaryEmail" Text="Primary Email:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorPrimaryEmail" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="PrimaryEmail" ControlToValidate="textBoxPrimaryEmail" RulesetName="Data" CssClass="validationMessage" OnValueConvert="proxyValidatorEmptyStringToNull_ValueConvert" />
                                        <asp:TextBox runat="server" ID="textBoxPrimaryEmail" MaxLength="100" Text='<%# Bind("PrimaryEmail") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelSecondaryEmail" AssociatedControlID="textBoxSecondaryEmail" Text="Secondary Email:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorSecondaryEmail" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="SecondaryEmail" ControlToValidate="textBoxSecondaryEmail" RulesetName="Data" CssClass="validationMessage" OnValueConvert="proxyValidatorEmptyStringToNull_ValueConvert" />
                                        <asp:TextBox runat="server" ID="textBoxSecondaryEmail" MaxLength="100" Text='<%# Bind("SecondaryEmail") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelTollFreePhone" AssociatedControlID="textBoxTollFreePhone" Text="Toll Free Phone:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorTollFreePhone" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="TollFreePhone" ControlToValidate="textBoxTollFreePhone" RulesetName="Data" CssClass="validationMessage" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert" />
                                        <asp:TextBox runat="server" ID="textBoxTollFreePhone" MaxLength="20" Text='<%# Bind("TollFreePhone") %>' CssClass="textfield3 phone"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="labelTDD" AssociatedControlID="textBoxTDD" Text="TDD:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorTDD" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="TDD" ControlToValidate="textBoxTDD" RulesetName="Data" CssClass="validationMessage" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert" />
                                        <asp:TextBox runat="server" ID="textBoxTDD" MaxLength="20" Text='<%# Bind("TDD") %>' CssClass="textfield3 phone"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelTollFreeTDD" AssociatedControlID="textBoxTollFreeTDD" Text="Toll Free TDD:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorTollFreeTDD" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="TollFreeTDD" ControlToValidate="textBoxTollFreeTDD" RulesetName="Data" CssClass="validationMessage" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert" />
                                        <asp:TextBox runat="server" ID="textBoxTollFreeTDD" MaxLength="20" Text='<%# Bind("TollFreeTDD") %>' CssClass="textfield3 phone"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelFax" AssociatedControlID="textBoxFax" Text="Fax:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorFax" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="Fax" ControlToValidate="textBoxFax" RulesetName="Data" CssClass="validationMessage" OnValueConvert="proxyValidatorPhoneNumber_ValueConvert" />
                                        <asp:TextBox runat="server" ID="textBoxFax" MaxLength="20" Text='<%# Bind("Fax") %>' CssClass="textfield3 phone"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelURL" AssociatedControlID="textBoxURL" Text="Website URL:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorURL" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="URL" ControlToValidate="textBoxURL" RulesetName="Data" CssClass="validationMessage" OnValueConvert="proxyValidatorEmptyStringToNull_ValueConvert" />
                                        <asp:TextBox runat="server" ID="textBoxURL" MaxLength="150" Text='<%# Bind("URL") %>' CssClass="textfield3"></asp:TextBox>
                                    </td>
                                </tr>
                                  <%--Added by Lavanya--%>
                                <tr>
                                <td></td>
                                    <td>
                                        <span class="required">*</span>&nbsp;<asp:Label runat="server" ID="labelAvailableLanguages" AssociatedControlID="textBoxAvailableLanguages" Text="Available Languages:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorAvailableLanguages" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="AvailableLanguages" ControlToValidate="textBoxAvailableLanguages" RulesetName="Data" CssClass="required" />
                                        <asp:TextBox runat="server" ID="textBoxAvailableLanguages" MaxLength="150" Text='<%# Bind("AvailableLanguages") %>'></asp:TextBox>                                       
                                    </td>
                                </tr>
                                <%--end--%>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Label runat="server" ID="labelComments" AssociatedControlID="textBoxComments" Text="Comments:"></asp:Label>
                                    </td>
                                    <td>
                                        <pp:PropertyProxyValidator ID="proxyValidatorComments" runat="server" Display="Dynamic" DisplayMode="List" SetFocusOnError="true" SourceTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" PropertyName="Comments" ControlToValidate="textBoxComments" RulesetName="Data" CssClass="validationMessage" />
                                        <asp:TextBox runat="server" ID="textBoxComments" Text='<%# Bind("Comments") %>' TextMode="MultiLine" Columns="20" Rows="2" CssClass="textfield3 comment"></asp:TextBox>
                                    </td>
                                </tr>
                                  <tr>
                                    <td nowrap>
                                        <asp:Label runat="server" ID="labelHide" AssociatedControlID="chkHideAgency" Text="Hide Agency From Public:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkHideAgency" runat="server" Checked='<%# Bind("HideAgencyFromPublic") %>' />
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
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:Button runat="server" ID="buttonSubmit" Text="Submit" CssClass="formbutton1a" OnClick="buttonSubmit_Click" CausesValidation="false" />   
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </div>
                    </InsertItemTemplate>
                </asp:FormView>
            </div>
        </div>
    </div>
    <pp:ObjectContainerDataSource ID="dataSourceAgency" runat="server" DataObjectTypeName="ShiptalkLogic.BusinessObjects.UI.RegisterAgencyViewData" OnInserting="dataSourceAgency_Inserting" OnInserted="dataSourceAgeny_Inserted" />
</asp:Content>
