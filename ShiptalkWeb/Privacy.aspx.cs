using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Security;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using ShiptalkWeb.Routing;
using ShiptalkCommon;

namespace ShiptalkWeb
{
    public partial class Privacy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //If User attempted login and login was success
            if (Session != null && (Session["LoggedIn"] + string.Empty) != string.Empty)
            {
                //Do nothing
            }
            else
                Response.Redirect("~/Default.aspx");
        }

        protected void buttonIAgree_Click(object sender, EventArgs e)
        {
           


            string sUsername = Session["LoggedIn"] as string;
            int UserId = ShiptalkPrincipal.UserId;
            //Since User agreed to the privacy agreement, redirect him to requested page.
            LastLoginInfo loginInfo = UserBLL.GetLastLoginInfo(sUsername);

            Guid NewSessionToken = Guid.NewGuid();
            UserBLL.UpdateUserSessionToken(UserId, NewSessionToken);

            FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(
                    2,
                    sUsername,
                    loginInfo.LastLoginAttempt.Value,
                    loginInfo.LastLoginAttempt.Value.AddMinutes(ShiptalkCommon.ConfigUtil.SessionTimeOutInMinutes),
                    false,
                    NewSessionToken.ToString() + "|" + loginInfo.LastLoginAttempt.Value.ToString());

            string encryptedTkt = FormsAuthentication.Encrypt(tkt);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTkt);
            authCookie.HttpOnly = true;

            Response.Cookies.Add(authCookie);


           //sammit: Crate a timespan of 1 sec
            System.TimeSpan diffResult = new TimeSpan(0,0,1);

            if (loginInfo.LastPasswordChangeDate != null)
            {
                diffResult = DateTime.Now.Subtract(loginInfo.LastPasswordChangeDate.Value);
                
            }


            if (diffResult.TotalDays >= ConfigUtil.PasswordWarningAfterHowManyDays)
            {
                pnlPrivacy.Visible = false;
                pnlPwdchange.Visible = true;
                lblNoOfDays.Text = (60 - Math.Floor(diffResult.TotalDays)).ToString();
                return;
            }

            else
            {


                //////string Unqiue_Browser_Window_Guid = Guid.NewGuid().ToString();
                //////Session.Add("WINDOW_GUID", Unqiue_Browser_Window_Guid);
                //////Response.AddHeader("WINDOW_GUID", Unqiue_Browser_Window_Guid);
                
               //Sammit commented out this line. Always go to usersearchpage
                ///// Response.Redirect(FormsAuthentication.GetRedirectUrl(sUsername, false));

                Response.Redirect("~/user/usersearch");
            }
        }

        protected void buttonCancel_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }

        protected void buttonPassChangeCancel_Click(object sender, EventArgs e)
        {
            string sUsername = Session["LoggedIn"] as string;

                Response.Redirect("~/user/usersearch");
            
               // Response.Redirect(FormsAuthentication.GetRedirectUrl(sUsername, false));
        }

        protected void buttonPassChangeOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/user/ChangePassword.aspx");
        }


    }
}
