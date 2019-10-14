using System;
using System.Collections.Generic;
using System.Data;
using ShiptalkLogic.BusinessObjects;
using T = ShiptalkLogic.Constants.Tables;

namespace ShiptalkLogic.DataLayer
{
    internal class RoutesDAL : DALBase
    {
        public IEnumerable<Route> GetAuthorizedRoutes(string routeName)
        {
            if (string.IsNullOrEmpty(routeName))
                throw new ArgumentNullException("routeName");

            var routes = new List<Route>();

            using (var command = database.GetStoredProcCommand("dbo.GetAuthorizedRoutes"))
            {
                database.AddInParameter(command, "@RouteName", DbType.String, routeName);

                using (var reader = database.ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        routes.Add(new Route { 
                            AdminRequired = reader.GetDefaultIfDBNull(T.AuthorizedRoutes.AdminRequired, GetBool, false),
                            RouteName = routeName,
                            ScopeId = reader.GetDefaultIfDBNull<short>(T.AuthorizedRoutes.ScopeId, GetInt16, 0),
                            UserId = reader.GetDefaultIfDBNull(T.AuthorizedRoutes.UserId, GetInt32, 0)
                        });
                    }
                }
            }

            return routes;
        }
    }
}
