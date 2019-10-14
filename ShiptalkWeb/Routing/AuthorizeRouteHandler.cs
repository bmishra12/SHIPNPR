using System;
using System.Security;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;
using System.Web.UI;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;
using System.Web.Security;

using ShiptalkCommon;

namespace ShiptalkWeb.Routing
{
    public class AuthorizeRouteHandler : IRouteHandler
    {
        public AuthorizeRouteHandler(string routeName, string virtualPath)
            : this(routeName, virtualPath, true)
        {
        }

        public AuthorizeRouteHandler(string routeName, string virtualPath, bool checkUrlAccess)
        {
            if (virtualPath == null)
                throw new ArgumentNullException("virtualPath");

            if (!virtualPath.StartsWith("~/"))
                throw new ArgumentException("virtualPath must start with a tilde slash: \"~/\"", "virtualPath");

            CheckUrlAccess = checkUrlAccess;
            RouteName = routeName;
            VirtualPath = virtualPath;
        }

        public bool CheckUrlAccess { get; private set; }

        public string RouteName { get; private set; }

        public string VirtualPath { get; private set; }

        #region Implementation of IRouteHandler

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {

            //////if (HttpContext.Current.Request.Headers["WINDOW_GUID"] + string.Empty == string.Empty)
            //////{
            //////    //HttpContext.Current.Session.Clear();
            //////    FormsAuthentication.SignOut();
            //////    HttpContext.Current.Response.Redirect("~/ErrorPages/SessionExpired.aspx?" + QueryStringHelper.QueryStringParamNames.SESSION_LOCKOUT_STRING.Description() + "=1", true);
            //////}
            //////else
            //////{
            //////    HttpContext.Current.Response.AddHeader("WINDOW_GUID", HttpContext.Current.Session["WINDOW_GUID"].ToString());
            //////}

            IHttpHandler page = null;

            var contnr = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (contnr != null)
            {
                FormsAuthenticationTicket tkt = FormsAuthentication.Decrypt(contnr.Value);
                var lastLoginBLL = new LastLoginBLL(tkt);
                try
                {
                    switch (lastLoginBLL.ValidateAuthTicket())
                    {
                        case SessionValidationResult.Valid:
                            page = ExecutePageAuthorization(requestContext);
                            break;
                        case SessionValidationResult.Session_Expired:
                            FormsAuthentication.SignOut();
                            int UserLockedOutIndicator = 0;
                            HttpContext.Current.Server.Transfer(string.Format("~/ErrorPages/SessionExpired.aspx?{0}={1}", QueryStringHelper.QueryStringParamNames.SESSION_LOCKOUT_STRING.Description(), UserLockedOutIndicator));
                            //HttpContext.Current.Response.Redirect(string.Format("~/ErrorPages/SessionExpired.aspx?{0}={1}", QueryStringHelper.QueryStringParamNames.SESSION_LOCKOUT_STRING.Description(), UserLockedOutIndicator), true);
                            break;
                        case SessionValidationResult.Session_Concurrent:
                            FormsAuthentication.SignOut();
                            int LockedOutIndicator = 1;
                            HttpContext.Current.Server.Transfer(string.Format("~/ErrorPages/SessionExpired.aspx?{0}={1}", QueryStringHelper.QueryStringParamNames.SESSION_LOCKOUT_STRING.Description(), LockedOutIndicator));
                            //HttpContext.Current.Response.Redirect(string.Format("~/ErrorPages/SessionExpired.aspx?{0}={1}", QueryStringHelper.QueryStringParamNames.SESSION_LOCKOUT_STRING.Description(), LockedOutIndicator), true);
                            break;
                        default:
                            FormsAuthentication.SignOut();
                            HttpContext.Current.Response.Redirect("~/Default.aspx", true);
                            break;
                    }
                }
                catch (Exception lastLoginEx)
                {
                    if (lastLoginEx is ShiptalkException)
                        throw lastLoginEx;
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Default.aspx", true);
            }

            return page;
        }

        #endregion

        private IHttpHandler ExecutePageAuthorization(RequestContext requestContext)
        {
            UserAccount accountInfo = UserBLL.GetUserAccount(UserBLL.GetUserId());

            //Check against Authorized Routes in database
            if (CheckUrlAccess && !IsAuthorized(RouteName, accountInfo.UserId, accountInfo.ScopeId, IsAdminUser(accountInfo)))
                ShiptalkException.ThrowSecurityException(string.Format("The route {0} is not authorized.", RouteName), "You are not authorized to access this page.", ShiptalkCommon.ShiptalkException.ShiptalkExceptionTypes.UN_AUTHORIZED_EXCEPTION);

            var page = BuildManager.CreateInstanceFromVirtualPath(VirtualPath, typeof(Page)) as IHttpHandler;

            var routeDataPage = page as IRouteDataPage;
            var isAuthorizePage = page as IAuthorize;

            if (routeDataPage != null)
            {
                routeDataPage.RouteData = requestContext.RouteData;
                routeDataPage.AccountInfo = accountInfo;
            }

            //Call the Page's IsAuthorized implementation for page level business logic execution
            if (isAuthorizePage != null)
                if (!isAuthorizePage.IsAuthorized())
                    ShiptalkCommon.ShiptalkException.ThrowSecurityException(string.Format("The route {0} is not authorized.", RouteName), "You are not authorized to access this page.", ShiptalkCommon.ShiptalkException.ShiptalkExceptionTypes.UN_AUTHORIZED_EXCEPTION);

            return page;
        }

        private bool IsAuthorized(string routeName, int userId, short scopeId, bool isAdmin)
        {
            if (userId == 0 || scopeId == 0)
                return false;

            var routes = new RoutesBLL().GetAuthorizedRoutes(routeName);

            foreach (var route in routes)
                if (route.ScopeId == scopeId || route.UserId == userId)
                {
                    if (route.AdminRequired)
                        if (isAdmin)
                            return true;
                        else
                            continue;

                    return true;
                }

            return false;
        }

        private bool IsAdminUser(UserAccount AccountInfo)
        {
            //All Admins are Default Admins at their Scope Level.
            //However, for State Level, Ship Directors are Default Admins.
            if (AccountInfo.IsCMSLevel || AccountInfo.IsStateScope)
            {
                return AccountInfo.IsAdmin;
            }
            else
            {
                //For potential multi Regional Users such as Sub State and Agency Users 
                //Atleast at one agency, they are admin. Thats all we can do for generalized IsAdmin search.
                //For regional specific IsAdmin, this method must not be used.
                var UserData = UserBLL.GetUser(AccountInfo.UserId);
                foreach (var regionalProfile in UserData.RegionalProfiles)
                {
                    if (regionalProfile.IsAdmin)
                        return true;
                }
                return false;
            }

        }

        

    }
}
