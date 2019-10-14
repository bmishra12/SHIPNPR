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
using System.Web.Routing;

using ShiptalkWeb.Routing;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;



namespace ShiptalkWeb
{
    public partial class ucInfoLibParentNavigation : System.Web.UI.UserControl
    {
        private bool RETRIEVE_LINK_FILE_BINARY_ON_LOAD = false;
        private string INFOLIBITEM_PARENT_ID_ROUTE_KEY = "ParentId";
        private string INFOLIBITEM_ID_ROUTE_KEY = "InfoLibItemId";
        //private const string InfoLib_Items_Url = "~/InfoLib/InfoLibItems.aspx?ParentId={0}";

        protected int? ParentId = null;
        private int? InfoLibItemId = null;
        private bool bIsLoggedInUser = false;

        protected string topLevelParentLink = string.Empty;
        protected string parentLink = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            bIsLoggedInUser = IsLoggedInUserAdmin();
            if (!bIsLoggedInUser) return;

            InitializeData();

            //Cannot operate without either of the Ids
            if (!InfoLibItemId.HasValue && !ParentId.HasValue)
                return;

            InitializeView();
        }

        private void InitializeData()
        {
            IHttpHandler handler = this.Page as IHttpHandler;
            IRouteDataPage routeDataPage = null;
            if (handler != null) routeDataPage = (handler as IRouteDataPage);
            if (routeDataPage != null)
            {
                if (routeDataPage.RouteData.Values[INFOLIBITEM_PARENT_ID_ROUTE_KEY] + string.Empty != string.Empty)
                    ParentId = Convert.ToInt32(routeDataPage.RouteData.Values[INFOLIBITEM_PARENT_ID_ROUTE_KEY]);

                if (routeDataPage.RouteData.Values[INFOLIBITEM_ID_ROUTE_KEY] + string.Empty != string.Empty)
                    InfoLibItemId = Convert.ToInt32(routeDataPage.RouteData.Values[INFOLIBITEM_ID_ROUTE_KEY]);
            }

            //Gets immediate parent and the top most parent for the current item
            topLevelParentLink = GetTopLevelParentLink();
            parentLink = GetParentLink();

        }
        private void InitializeView()
        {
            //Navigation, to list of Parent Items
            hlTopLevelItems.DataBind();
            hlTopLevelParentLink.DataBind();
            hlViewParentLink.DataBind();
        }

        private string GetTopLevelParentLink()
        {
            if (ParentId != 0)
            {
                InfoLibItem item = InfoLibBLL.GetInfoLibTopLevelParent(ParentId.Value, false);
                if (item != null && ParentItem.ParentId != 0)
                {
                    //return RouteController.ViewInfoLibItems(item.InfoLibItemId);
                    return InfoLibUtil.ConstructInfoLibItemNavigationUrl(
                        QueryStringHelper.QueryStringParamNames.INFOLIB_PARENTID_INT.Description(), item.InfoLibItemId.ToString());
                }
            }

            return string.Empty;
        }

        private string GetParentLink()
        {
            if (ParentId.HasValue && ParentId.Value != 0)
            {
                if (ParentItem != null && ParentItem.InfoLibItemId != 0)
                {
                    return InfoLibUtil.ConstructInfoLibItemNavigationUrl(
                        QueryStringHelper.QueryStringParamNames.INFOLIB_PARENTID_INT.Description(), ParentId.Value.ToString());
                    //return RouteController.ViewInfoLibItems(ParentId.Value);
                }
            }
            return string.Empty;
        }

        protected bool IsLoggedInUserAdmin()
        {
            return (HttpContext.Current.User != null &&
                                       HttpContext.Current.User.Identity.IsAuthenticated &&
                                       ShiptalkPrincipal.IsSessionActive)
                                           ? ShiptalkPrincipal.IsAdmin && ShiptalkPrincipal.ScopeId == Scope.CMS.EnumValue<int>()
                                           : false;

        }

        private InfoLibItem _ParentItem = null;
        public InfoLibItem ParentItem
        {
            get
            {
                if (ParentId != 0)
                {
                    if (_ParentItem == null)
                        _ParentItem = InfoLibBLL.GetInfoLibItem(ParentId.Value, RETRIEVE_LINK_FILE_BINARY_ON_LOAD);
                }

                return _ParentItem;
            }
        }
    }
}