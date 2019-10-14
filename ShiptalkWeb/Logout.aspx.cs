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
using ShiptalkLogic.BusinessLayer;
namespace ShiptalkWeb
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //FormsAuthentication.SignOut();
            //Session.Abandon();
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
            Response.Redirect("Default.aspx");
        }
    }
}
