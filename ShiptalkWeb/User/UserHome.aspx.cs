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

using System.Security.Principal;
using System.Threading;
using System.Data.Sql;

using ShiptalkWeb.Routing;
using System.Web.Routing;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessLayer;
using ShiptalkCommon;

namespace ShiptalkWeb
{
    public partial class UserHome : System.Web.UI.Page, IRouteDataPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            SetupPermissiblePaths();
            Page.DataBind();
        }

        private void SetupPermissiblePaths()
        {
            if (this.AccountInfo.Scope == Scope.CMS)
                return;
            else
            {
                if (!this.AccountInfo.IsAdmin)
                {
                    //Hide non-Admin functions here
                    addUserLink.Visible = false;
                    pendingRegistrations.Visible = false;
                }

           }
            
        }

        #region Implementation of IRouteDataPage

        public UserAccount AccountInfo { get; set; }

        public RouteData RouteData { get; set; }

        #endregion
    }







}
