
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ShiptalkCommon;
using ShiptalkLogic.BusinessLayer;
using ShiptalkLogic.BusinessObjects;

namespace ShiptalkWeb
{

    public enum SessionVars
    {
        UserId,
        IsAdmin,
        ScopeId,
        UserStateFIPS,
        UserRegionalAccessProfileList,
        IsShipDirector

    }


    /// <summary>
    /// All methods that are non-Nullable will throw System.Security.SecurityException if the Session value does not 
    /// exist in the Session. This implies that a User must have a valid session to access the Session variables.
    /// Nullable parameterized properties will not throw SecurityException; rather, will return a null value.
    /// </summary>
    public class SessionUtil
    {

        /// <summary>
        /// Gets or Sets the UserId in Session of the LoggedIn User.
        /// If Session expired, SecurityException will be thrown.
        /// </summary>
        public static int UserId
        {
            get
            {
                return GetNonNullSessionValue<int>(SessionVars.UserId);
            }

            set
            {
                SetSessionValue(SessionVars.UserId, value);
            }

        }


        /// <summary>
        /// Gets or Sets the IsAdmin value in Session.
        /// If Session expired, SecurityException will be thrown
        /// </summary>
        public static bool IsAdmin
        {
            get
            {
                return GetNonNullSessionValue<bool>(SessionVars.IsAdmin);
            }

            set
            {
                SetSessionValue<bool>(SessionVars.IsAdmin, value);
            }
        }


        /// <summary>
        /// Gets or Sets the User ScopeId in Session.
        /// If Session expired, SecurityException will be thrown
        /// </summary>
        public static Int16 ScopeId
        {
            get
            {
                return GetNonNullSessionValue<Int16>(SessionVars.ScopeId);
            }

            set
            {
                SetSessionValue<Int16>(SessionVars.ScopeId, value);
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
                bool _IsShipDirector = false;
                if (GetNullableSessionValue<bool>(SessionVars.IsShipDirector).HasValue)
                    _IsShipDirector = GetNullableSessionValue<bool>(SessionVars.IsShipDirector).Value;
                return _IsShipDirector;
            }
            set
            {
                SetSessionValue<bool>(SessionVars.IsShipDirector, value);
            }
        }

        /// <summary>
        /// Gets or Sets the User StateFIPS in Session.
        /// If Session expired, SecurityException will be thrown.
        /// It is to be noted that for CMS Level Users(including CMS Regional), UserStateFIPS could be null. However, there arise no
        /// need to access User StateFIPS for such Users.
        /// </summary>
        public static string UserStateFIPS
        {
            get
            {
                return GetNonNullSessionValue<string>(SessionVars.UserStateFIPS);
            }

            set
            {
                SetSessionValue<string>(SessionVars.UserStateFIPS, value);
            }
        }

        /// <summary>
        /// Private helper Generic function to facilitate retrieval of value from Session
        /// </summary>
        private static T GetNonNullSessionValue<T>(SessionVars variableName)
        {
            if (DoesSessionVariableExist(variableName))
                return (T)HttpContext.Current.Session[variableName.Description()];
            else
            {
                string error = "Unable to retrieve session value for : " + variableName.Description();
                throw new ShiptalkCommon.ShiptalkException(error, true, new System.Security.SecurityException(error), "Sorry. We lost some session information. Please login again.");
            }
        }


        /// <summary>
        /// Private helper Generic function to facilitate setting value to Session variable.
        /// </summary>
        private static void SetSessionValue<T>(SessionVars variableName, T value)
        {
            
            HttpContext.Current.Session[variableName.Description()] = value;
        }


        /// <summary>
        /// A relaxed utility function that doesn't impose mandatory requirements for Session values to exist in the Session.
        /// Returns a value from Session if it exists. Returns null if value isn't present.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="var"></param>
        /// <returns></returns>
        private static Nullable<T> GetNullableSessionValue<T>(SessionVars var) where T : struct
        {

            if (HttpContext.Current.Session[var.Description()] != null)
                return (T)HttpContext.Current.Session[var.Description()];
            else
            {
                return null;
            }


        }

        /// <summary>
        /// Verifies Session for the existence of a value for a Session variable.
        /// </summary>
        /// <param name="var"></param>
        /// <returns></returns>
        public static bool DoesSessionVariableExist(SessionVars var)
        {
            if (HttpContext.Current.Session != null)
                return !(HttpContext.Current.Session[var.Description()] == null);
            else
                return false;
        }

        //public static bool IsExpiredOrConcurrentSession(FormsAuthenticationTicket ticket)
        //{
        //    //Last logged in time from db
        //    var LastLoginInfo = ShiptalkLogic.BusinessLayer.UserBLL.GetLastLoginInfo(ticket.Name);
        //    var LastLoggedIn = Convert.ToDateTime(LastLoginInfo.LastLoginAttempt.Value.ToString("MM/dd/yyyy HH:mm:ss"));

        //    //logged in time per cookie
        //    var TicketIssuedActual = Convert.ToDateTime(ticket.IssueDate.ToString("MM/dd/yyyy HH:mm:ss"));
        //    var TicketIssuedAdjusted = TicketIssuedActual.AddSeconds(5);

        //    /*  Logic:
        //     *      Ticket is issued for A at machine 1
        //     *      Ticket is issued for A at machine 2
        //     *          When A access the system, last login time would have changed due to re-login at Machine B.
        //     *              If LastLoggedIn for same A is Active but LOWER than TicketIssued
        //     *                  Display Message: Your account has logged in on a different system; As a result you have been logged out.
        //     */ 

        //    //Ensure that the last logged-in time is approximately equal to or less than TicketIssuedAdjusted

        //    //If ticket issued is earlier than last logged in
        //    return (DateTime.Compare(TicketIssuedActual, LastLoggedIn) < 0 ? true : false);
            
        //}

    }
}

