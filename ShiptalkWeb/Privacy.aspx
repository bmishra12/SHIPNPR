<%@ Page Language="C#" MasterPageFile="~/ShiptalkWebWideNoTab.Master" AutoEventWireup="true"
    CodeBehind="Privacy.aspx.cs" Inherits="ShiptalkWeb.Privacy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body1" runat="server">

    <div id="maincontent">
        <asp:Panel ID="pnlPrivacy" runat = "server" Visible ="true">
        <div class="dv3col">
            <h1 style="text-align: center">
                *****************WARNING*****************</h1>
            <br />
            <li style="margin-left: 15px; list-style-position: outside;">You are accessing a U.S. Government information system, which includes: </li>
            <ol style="margin-left: 40px; margin-top: 8px">
                <li>this computer, </li>
                <li>this computer network, </li>
                <li>all computers connected to this network, and </li>
                <li>all devices and storage media attached to this network or to a computer on this
                    network.</li>
            </ol>
            <div style="margin-top: 5px; padding-left: 40px">This information system is provided for U.S. Government-authorized use only.</div>
            
            <br />
            <br />
            
            <li style="margin-left: 15px; list-style-position: outside;">Unauthorized or improper
                use of this system may result in disciplinary action, as well as civil and criminal
                penalties. </li>
            
            <br />
            <br />    
            
            <li style="margin-left: 15px; list-style-position: outside;">By using this information system, you understand and consent to the following:
            </li>
            <ul style="margin-left: 30px; margin-top: 8px">
                <li>You have no reasonable expectation of privacy regarding any communication or data
                    transiting or stored on this information system. At any time, and for any lawful
                    Government purpose, the government may monitor, intercept, and search and seize
                    any communication or data transiting or stored on this information system. </li>
                <li>Any communication or data transiting or stored on this information system may be
                    disclosed or used for any lawful government purpose. </li>
                <%--<li>Retains the notification message or banner on the screen until users take explicit
                    actions to log on to or further access the information system; and </li>
                <li>For publicly accessible systems:
                    <ol type="i">
                        <li>displays the system use information when appropriate,
                    before granting further access;</li>
                        <li>displays references, if any, to monitoring,
                    recording, or auditing that are consistent with privacy accommodations for such
                    systems that generally prohibit those activities; and </li>
                        <li>includes in the notice
                    given to public users of the information system, a description of the authorized
                    uses of the system.</li>
                    </ol>
                </li>--%>
            </ul>
            
            <br />
            <br />
            
            <h1 style="text-align: center">
                *****************WARNING*****************</h1>
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="buttonCancel" Text="Cancel" CssClass="formbutton3"
                                OnClick="buttonCancel_Click" CausesValidation="false" align="right" />
                        </td>
                        <td>
                            <asp:Button runat="server" ID="buttonIAgree" Text="I Agree" CssClass="formbutton3"
                                OnClick="buttonIAgree_Click" CausesValidation="false" align="left" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        </asp:Panel>

              <asp:Panel ID="pnlPwdchange" runat = "server" Visible ="false">

    <div id="PasswordReset">
    
                <h1 style="text-align: center">
                *******Your Password will expire in <asp:Label ID="lblNoOfDays" runat="Server"></asp:Label> Days. *******</h1>
                 <br />

               <h4 style="text-align: center">
               Do you want to change your password now?
                </h4>
                 <br />
            <div align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="button1" Text="OK" CssClass="formbutton3"
                                OnClick="buttonPassChangeOK_Click" CausesValidation="false" align="right" />
                        </td>
                        <td>
                            <asp:Button runat="server" ID="button2" Text="Cancel" CssClass="formbutton3"
                                OnClick="buttonPassChangeCancel_Click" CausesValidation="false" align="left" />
                        </td>
                    </tr>
                </table>
            </div>
    </div>
        </asp:Panel>
    </div>

 
</asp:Content>
