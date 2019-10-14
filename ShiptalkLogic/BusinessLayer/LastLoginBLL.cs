using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;

using ShiptalkLogic.BusinessObjects;

namespace ShiptalkLogic.BusinessLayer
{

    public enum SessionValidationResult
    {
        Invalid_Ticket = 0,
        Session_Expired = 1,
        Session_Concurrent = 2,
        Valid = 3

    }

    public class LastLoginBLL
    {
        FormsAuthenticationTicket ticket = null;
        private LastLoginInfo DBLoginInfo { get; set; }

        public LastLoginBLL(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");
            else
            {
                this.ticket = ticket;
                DBLoginInfo = UserBLL.GetLastLoginInfo(ticket.Name);
            }
        }

        public SessionValidationResult ValidateAuthTicket()
        {
            if (!string.IsNullOrEmpty(ticket.UserData))
            {
                if (!ticket.Expired)
                {
                    var userData = ticket.UserData.Split('|');
                    if (userData != null && userData.Length > 0)
                    {
                        var UserDataSessionToken = userData[0];
                        DateTime UserDataTimeStamp;
                        if (!string.IsNullOrEmpty(UserDataSessionToken))
                        {
                            if (DateTime.TryParse(userData[1], out UserDataTimeStamp))
                            {
                                /* Security flaw Scenario: If User saves token before logout and re-uses it
                                    * Oct 21 2010
                                    * Fix: Lets check if user logged out by checking DB.
                                    * Check LastLoginInfo.IsSessionCurrentlyLive bit.
                                   */
                                //If Session was not established, ask user to login again.
                                if(!DBLoginInfo.SessionToken.HasValue)
                                    return SessionValidationResult.Session_Expired;

                                //If Session is available and Valid
                                if ((DBLoginInfo.SessionToken.Value.ToString() == UserDataSessionToken) && (DBLoginInfo.LastLoginAttempt.Value.ToString() == UserDataTimeStamp.ToString()))
                                    return SessionValidationResult.Valid;

                                //Find Invalid reasons
                                //When Guid mismatch
                                if (DBLoginInfo.SessionToken.Value.ToString() != UserDataSessionToken)
                                {
                                    if (IsConcurrentSession(UserDataTimeStamp))
                                        return SessionValidationResult.Session_Concurrent;
                                    else if (IsSessionExpired(UserDataTimeStamp))
                                        return SessionValidationResult.Session_Expired;
                                }
                                else //Guid match but timestamp mismatch
                                {
                                    if (IsSessionExpired(UserDataTimeStamp))
                                        return SessionValidationResult.Session_Expired;
                                }
                            }
                        }
                    }
                }
                else
                    return SessionValidationResult.Session_Expired;
            }

            return SessionValidationResult.Invalid_Ticket;

        }


        private bool IsConcurrentSession(DateTime UserTokenTimeStamp)
        {
            return (DateTime.Compare(UserTokenTimeStamp, DBLoginInfo.LastLoginAttempt.Value) < 0 ? true : false);
        }

        private bool IsSessionExpired(DateTime UserTokenTimeStamp)
        {
            //Check if auth Token is issued earlier than last logged in
            return (DateTime.Compare(UserTokenTimeStamp, DBLoginInfo.LastLoginAttempt.Value.AddMinutes(ShiptalkCommon.ConfigUtil.SessionTimeOutInMinutes)) < 0 ? true : false);
        }



    }
}
