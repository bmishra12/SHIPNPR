using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using ShiptalkLogic.BusinessLayer;

namespace ShiptalkWeb
{
    public partial class UserTab : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoggedInTabs.Visible = ShiptalkPrincipal.IsSessionActive;
                if (!string.IsNullOrEmpty(QueryStringHelper.SessionLockOutString))
                {
                    LoggedInTabs.Visible = false;
                }
            }
            catch{}
            
            AnonymousTabs.Visible = !LoggedInTabs.Visible;

            if (LoggedInTabs.Visible)
            {
                LoggedInTabs.DataBind();
            }
            
            //TopMenu.DataBind();
        }


        protected void LoginStatus1_LoggedOut(object sender, System.EventArgs e)
        {
            //Logout call to UserBLL will clear the session; Now, session unavailable means not logged in.
            try
            {
                Guid? SessionToken = null;
                UserBLL.UpdateUserSessionToken(ShiptalkPrincipal.UserId, SessionToken);
            }
            catch { }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            FormsAuthentication.SignOut();
            Session.Abandon();
        }


    }
}