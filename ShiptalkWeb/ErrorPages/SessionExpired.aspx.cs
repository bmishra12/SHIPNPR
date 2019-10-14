using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace ShiptalkWeb
{
    public partial class SessionExpired : System.Web.UI.Page
    {
        private readonly string QS_LOCKOUT_KEY = QueryStringHelper.SessionLockOutString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //AuthorizedRouteHandler is not able to call Session.Abandon
                //before redirecting to this page. So we'll call here. This will be helpful
                //for concurrent session expirations in particular.
                if (!IsPostBack)
                {
                    Response.RedirectLocation = Request.UrlReferrer.ToString();
                    Session.Abandon();
                }
            }
            catch{}
            //Default
            string message = "Detected an invalid session. Your session expired or you do not have a valid session. Please login to establish a new session.";

            if (Request.QueryString.HasKeys())
            {
                if (!string.IsNullOrEmpty(QueryStringHelper.SessionLockOutString))
                {
                    message = "A new session has been established with your account on a different system or browser. Your current session has been terminated due to security issues. Please contact support if you have questions.";
                }
            }

            lblMessage.Text = message;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
    }
}
