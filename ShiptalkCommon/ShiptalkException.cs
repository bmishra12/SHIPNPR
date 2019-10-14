using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShiptalkCommon
{

    /// <summary>
    /// When intentionally thrown, Shiptalk Exception must be thrown so that the appropriate action could be taken
    /// by the error handling module.
    /// </summary>
    public sealed class ShiptalkException : Exception
    {

        public enum ShiptalkExceptionTypes
        {
            SESSION_EXPIRED_OR_UNAVAILABLE = 1,
            UN_AUTHORIZED_EXCEPTION = 2
        }

        public bool HasFriendlyMessage() {
            return !string.IsNullOrEmpty(FriendlyMessage);
        }

        public string FriendlyMessage { get; set; }
        
        public bool IsServerFaultCode { get; set; }

        public ShiptalkExceptionTypes? ExceptionType { get; set; }


        public ShiptalkException(string error, bool bIsServerFaultCode) 
            : base(error) 
        { 
            IsServerFaultCode = bIsServerFaultCode; 
        }

        public ShiptalkException(string error, bool bIsServerFaultCode, string sFriendlyMessage)
            : this(error,bIsServerFaultCode)
        {
            FriendlyMessage = sFriendlyMessage;
        }

        public ShiptalkException(string error, bool bIsServerFaultCode, Exception innerEx) 
            : base(error, innerEx) 
        {
            IsServerFaultCode = bIsServerFaultCode;
        }

        public ShiptalkException(string error, bool bIsServerFaultCode, Exception innerEx, string sFriendlyMessage)
            : this(error, bIsServerFaultCode, innerEx)
        {
            FriendlyMessage = sFriendlyMessage;
        }

        public static void ThrowSecurityException(string error, string UserFriendlyMessage)
        {
            //ShiptalkException ex = new ShiptalkCommon.ShiptalkException(error, true, new System.Security.SecurityException(error), UserFriendlyMessage);
            //ex.ExceptionType = ShiptalkExceptionTypes.UN_AUTHORIZED_EXCEPTION;
            //throw ex;
            ThrowSecurityException(error, UserFriendlyMessage, null);
        }
        public static void ThrowSecurityException(string error, string UserFriendlyMessage, ShiptalkExceptionTypes? ExceptionType)
        {
            ShiptalkException ex = new ShiptalkCommon.ShiptalkException(error, true, new System.Security.SecurityException(error), UserFriendlyMessage);
            if(ExceptionType.HasValue)
                ex.ExceptionType = ShiptalkExceptionTypes.UN_AUTHORIZED_EXCEPTION;
            throw ex;
        }


        
    }





    //public class ShiptalkSessionExpiredException : ApplicationException
    //{


    //    public ShiptalkSessionExpiredException(string error) : base(error) { }

    //    public ShiptalkSessionExpiredException(string error, Exception innerEx) : base(error, innerEx) { }


    //    public bool HasInnerException()
    //    { return base.InnerException != null ? true : false; }



    //    public new Exception InnerException
    //    {
    //        get { return base.InnerException; }
    //    }
    //}
}
