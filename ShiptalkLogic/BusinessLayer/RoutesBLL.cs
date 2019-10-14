using System.Collections.Generic;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.DataLayer;

namespace ShiptalkLogic.BusinessLayer
{
    public class RoutesBLL
    {
        public IEnumerable<Route> GetAuthorizedRoutes(string routeName)
        {
            return new RoutesDAL().GetAuthorizedRoutes(routeName);
        }
    }
}
