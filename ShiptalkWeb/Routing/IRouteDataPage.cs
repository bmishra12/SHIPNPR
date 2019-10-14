using System.Web.Routing;
using ShiptalkLogic.BusinessObjects;

namespace ShiptalkWeb.Routing
{
    public interface IRouteDataPage
    {
        RouteData RouteData { get; set; }
        UserAccount AccountInfo { get; set; }
    }
}