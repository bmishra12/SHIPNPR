using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

namespace ShiptalkLogic.DataLayer
{

    internal static class QueryUtil
    {

        //public static readonly string SP_CreateUser = "CreateUser";
        //public static readonly string SP_AuthenticateUser = "AuthenticateUser";

        //public static readonly string SP_GetUserProfile = "GetUserProfile";
        //public static readonly string SP_GetUserAccount = "GetUserAccountInfo";
        //public static readonly string SP_ChangePassword = "ChangePassword";
        //public static readonly string SP_ActivateDeActivateUser = "ActivateDeActivateUser";

        public static IEnumerable<KeyValuePair<TKey, string>> GetKeyValuePairs<TKey, TEnum>()
        {
            var descriptions = new List<KeyValuePair<TKey, string>>();
            var names = Enum.GetNames(typeof(TEnum));

            foreach (var name in names)
            {
                var enumValue = Convert.ChangeType(Enum.Parse(typeof(TEnum), name), typeof(TEnum));
                descriptions.Add(new KeyValuePair<TKey, string>((TKey)enumValue, ((Enum)enumValue).Description()));
            }

            return descriptions;
        }

      
    }
}
