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

namespace ShiptalkWeb
{
    public partial class ExceptionTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbtnUnhandled_Click(object sender, EventArgs e)
        {
            Object o = null;
            o.GetHashCode();
        }

        protected void lbtnSessionExpired_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Write("<h1>Logged in UserID: " + ShiptalkPrincipal.UserId.ToString() + "</h1>");
        }

        protected void lbtnImageNotFound_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/images/{0}.png", Guid.NewGuid().ToString()));
        }

        protected void lbtnFileNotFound_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/TestException/{0}.aspx", Guid.NewGuid().ToString()));
        }
    }
}
