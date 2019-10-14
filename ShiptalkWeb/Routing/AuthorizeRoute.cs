using System.Web.Routing;

namespace ShiptalkWeb.Routing
{
    public class AuthorizeRoute : Route
    {
        public AuthorizeRoute(string routeName, string url, string virtualPath)
            : base(url, new AuthorizeRouteHandler(routeName, virtualPath))
        {
        }

        public AuthorizeRoute(string routeName, string url, string virtualPath, bool checkPhysicalUrlAccess)
            : base(url, new AuthorizeRouteHandler(routeName, virtualPath, checkPhysicalUrlAccess))
        {
        }
    }
}