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
    public partial class UnAuthorizedAccess : System.Web.UI.Page
    {
        private readonly string QS_LOCKOUT_KEY = QueryStringHelper.SessionLockOutString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
