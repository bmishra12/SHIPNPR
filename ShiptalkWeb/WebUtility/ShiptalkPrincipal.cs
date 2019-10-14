using System;
using System.Web;

using System.Security.Principal;

using ShiptalkLogic.BusinessObjects;

namespace ShiptalkWeb
{
    /// <summary>
    /// A make-up to the Principal Object of the HttpUserContext.
    /// This class provides static methods to access the HttpContext User Identity as well as several properties
    /// for the User profile and User access rights.
    /// </summary>
    public static class ShiptalkPrincipal
    {

        public static void InitializeCurrentUser(UserAccount userAccountInfo)
        {
            StateFIPS = userAccountInfo.StateFIPS;
            ScopeId = userAccountInfo.ScopeId;
            UserId = userAccountInfo.UserId;
            IsAdmin = userAccountInfo.IsAdmin;
            IsShipDirector = userAccountInfo.IsShipDirector;
        }


        #region "Public properties"

        /// <summary>
        /// The Username of the logged in User.
        /// For forms authentication, this is set automatically by the framework.
        /// The identity name is wrapped in this method.
        /// </summary>
        public static string Username
        {
            get
            {
                //In Forms authentication, this is readily available.
                return HttpContext.Current.User.Identity.Name;
            }
        }

        /// <summary>
        /// Wrapper for the HttpContext.Current.User.Identity object.
        /// </summary>
        public static IIdentity Identity
        {
            get {
                return HttpContext.Current.User.Identity;
            }
        }

        public static bool IsAdmin
        {
            get
            {
                return SessionUtil.IsAdmin;
            }
            private set
            {
                SessionUtil.IsAdmin = value;
            }
        }

        public static Int32 UserId
        {
            get
            {
                return SessionUtil.UserId;
            }
            private set
            {
                SessionUtil.UserId = value;
            }
        }


        /// <summary>
        /// If Scope is not available, will throw security exception
        /// so that user will be able to login and re-establish session.
        /// </summary>
        public static Int16 ScopeId
        {
            get {
                //return StrictSessionValue<Int16>(SessionVars.ScopeId);
                return SessionUtil.ScopeId;
            }
            private set {
                SessionUtil.ScopeId = value;
            }
        }


        /// <summary>
        /// Will identify if the User is a ship director of the state.
        /// Will return boolean value based on the status of the User.
        /// </summary>
        public static bool IsShipDirector
        {
            get
            {
                return SessionUtil.IsShipDirector;
            }
            private set
            {
                SessionUtil.IsShipDirector = value;
            }
        }


        /// <summary>
        /// Returns true if User is State Admin; Else false;
        /// </summary>
        public static bool IsStateAdmin
        {
            get {
                bool _IsStateAdmin = false  ;
                if (SessionUtil.ScopeId == (int)ShiptalkLogic.BusinessObjects.Scope.State)
                {
                    _IsStateAdmin = SessionUtil.IsAdmin;
                }
                return _IsStateAdmin;
            }
        }


        /// <summary>
        /// Returns true if the User belongs to State level Scope; Returns false otherwise.
        /// </summary>
        public static bool IsStateScope
        {
            get
            {
                return (SessionUtil.ScopeId == (int)ShiptalkLogic.BusinessObjects.Scope.State);
            }
        }


        /// <summary>
        /// Returns StateFIPS for the User
        /// </summary>
        public static string StateFIPS
        {
            get
            {
                return SessionUtil.UserStateFIPS;
            }
            private set
            {
                SessionUtil.UserStateFIPS = value;
            }
        }

        public static bool IsSessionActive
        {
            get
            {
                return HttpContext.Current.User != null && 
                    HttpContext.Current.User.Identity.IsAuthenticated 
                    && SessionUtil.DoesSessionVariableExist(SessionVars.UserId);
            }
        }

        #endregion



        #region "private utility methods"

       


        /// <summary>
        /// A relaxed utility function that doesn't impose mandatory requirements for Session values to exist in the Session.
        /// Returns a value from Session if it exists. Returns null if value isn't present.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="var"></param>
        /// <returns></returns>
        //private static Nullable<T> NullableSessionValue<T>(SessionVars var) where T : struct
        //{

        //    if (HttpContext.Current.Session[var.Description()] != null)
        //        return (T)HttpContext.Current.Session[var.Description()];
        //    else
        //    {
        //        return null;
        //    }
        //}
        
        ///// <summary>
        ///// A stricter utility function that makes it mandatory for a session value that it seeks, to be present in the Session.
        ///// If the value does not exist in the session, will throw SecurityException. The SecurityException
        ///// will help redirect User to login page to re-establish the vital information about the User in session.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="var"></param>
        ///// <returns></returns>
        //private static T StrictSessionValue<T>(SessionVars var)
        //{
        //    try
        //    {
        //        return (T)HttpContext.Current.Session[var.Description()];
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = "Unable to retrieve session value for : " + var.Description();
        //        throw new ShiptalkCommon.ShiptalkException(error, true, new System.Security.SecurityException(error), "Sorry. We lose some session information. Please login again.");
        //    }

        //}


        ///// <summary>
        ///// Throw a user friendly security exception. Called by Stricter methods that 
        ///// require the presence of Session value to be mandatory. This method uses the Session variable 
        ///// description to construct a user friendly message for exception logging.
        ///// </summary>
        ///// <param name="var"></param>
        //private static void ThrowSecurityException(SessionVars var)
        //{
        //    string error = "Unable to retrieve session value for : " + var.Description();
        //    throw new ShiptalkCommon.ShiptalkException(error, true, new System.Security.SecurityException(error), "Sorry. We lose some session information. Please login again.");
        //}

        #endregion

    }


  
}
