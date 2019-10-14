using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Routing;

using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkWeb.Routing;


namespace ShiptalkWeb
{
    public partial class ucNavLeft : System.Web.UI.UserControl
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            SetPageControls();
        }

        protected void ucNavLeft_Init(object sender, EventArgs e)
        {
            
        }

        private void SetPageControls()
        {
            if (Page.User.Identity.IsAuthenticated && ShiptalkPrincipal.IsSessionActive)
            {
                loggedInNav.Visible = true;
                publicNav.Visible = false;
                if (ShiptalkPrincipal.IsSessionActive && ShiptalkPrincipal.UserId != 0)
                    welcomeText.Text = "Welcome! " + UserBLL.GetUser(ShiptalkPrincipal.UserId).FullName;
                
            }
            else
            {
                loggedInNav.Visible = false;
                publicNav.Visible = true;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            
            int UserId;
            string loginError;

            string sUsername = txtUser.Text.Trim();
            string Password = txtPassword.Text.Trim();

            Page.Validate("LoginForm");
            if (!Page.IsValid)
                return;                
            
            //Reset password.
            txtPassword.Text = string.Empty;
            //Successful Login: Take user to LoggedIn home.
            //Call BLL : Authenticate User.
            if (UserBLL.AuthenticateUser(sUsername, Password, out UserId, out loginError))
            {
                Session["LoggedIn"] = sUsername;
                //Commented below so user cannot login unless privacy agreement is accepted by user.
                ShiptalkPrincipal.InitializeCurrentUser(UserBLL.GetUserAccount(UserId));
                Server.Transfer("~/Privacy.aspx", true);
                /*
                LastLoginInfo loginInfo = UserBLL.GetLastLoginInfo(sUsername);
                FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(
                        2,
                        sUsername,
                        loginInfo.LastLoginAttempt.Value,
                        loginInfo.LastLoginAttempt.Value.AddMinutes(ShiptalkCommon.ConfigUtil.SessionTimeOutInMinutes),
                        false,
                        loginInfo.SessionToken.Value.ToString() + "|" + loginInfo.LastLoginAttempt.Value.ToString());
                
                string encryptedTkt = FormsAuthentication.Encrypt(tkt);
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTkt);
                //HttpCookie authCookie = FormsAuthentication.GetAuthCookie(sUsername, false);
                //authCookie.Expires = DateTime.Now.Add(new TimeSpan(0, ShiptalkCommon.ConfigUtil.SessionTimeOutInMinutes, 0));
                Response.Cookies.Add(authCookie);
                Response.Redirect(FormsAuthentication.GetRedirectUrl(sUsername, false));
                */

                //FormsAuthentication.RedirectFromLoginPage(sUsername, false);
                //RouteController.RouteTo(RouteController.AgencySearch());

                
                //SessionUtil.IsAdmin = true;
                //SessionUtil.ScopeId = 4;
                //SessionUtil.UserId = UserId;
                //SessionUtil.UserStateFIPS = "20";
                // Create the authentication ticket
                ////////FormsAuthenticationTicket authTicket = new
                ////////     FormsAuthenticationTicket(1,                          // version
                ////////                               sUsername,           // user name
                ////////                               DateTime.Now,               // creation
                ////////                               DateTime.Now.AddMinutes(15),// Expiration
                ////////                               false,                      // Persistent
                ////////                               "AIR|1|1|20");                    // User data

                ////////string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                ////////// Create a cookie and add the encrypted ticket to the cookie as data.
                ////////HttpCookie authCookie =
                ////////             new HttpCookie(FormsAuthentication.FormsCookieName,
                ////////                            encryptedTicket);
                ////////// Add the cookie to the outgoing cookies collection. 
                ////////Response.Cookies.Add(authCookie); 
                
                //Response.Redirect(FormsAuthentication.GetRedirectUrl(sUsername,false));
            }
            else
            // If failed; provide reason
            {
                DisplayLoginFailureMessage(loginError);
            }
        }


        private void DisplayLoginFailureMessage(string failureMessage)
        {
            //TODO: Display error message
            LoginError.Text = failureMessage;
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/ResetPassword.aspx", true);
        }
    }
}