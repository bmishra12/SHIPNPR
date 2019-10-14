using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.ComponentModel;
using ShiptalkLogic.BusinessObjects;

namespace ShiptalkWeb
{

    public sealed class QueryStringHelper
    {
        public enum QueryStringParamNames
        {
            [Description("evt")]
            EMAIL_VERIFICATION_TOKEN_STRING,
            [Description("prt")]
            PASSWORD_RESET_TOKEN_STRING,
            [Description("lo")]     
            SESSION_LOCKOUT_STRING,
            [Description("evty")]
            EMAIL_VERIFICATION_TYPE_STRING,
            [Description("ilpid")]  //InfoLib Parent Id
            INFOLIB_PARENTID_INT,
            [Description("ilid")]  //InfoLib Item Id
            INFOLIB_ITEMID_INT,
            [Description("ilfid")]  //InfoLib requested File Identifier
            INFOLIB_REQ_FILE_IDEN_INT,
            [Description("ilst")]    //InfoLib search text
            INFOLIB_SEARCH_TEXT,
            [Description("ilsi")]    //InfoLib special identifier
            INFOLIB_SPECIAL_IDENTIFIER
        }

        /// <summary>
        /// Use for value types such as int
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramName"></param>
        /// <returns></returns>
        private static Nullable<T> GetNullableValueTypeValue<T>(QueryStringParamNames paramName) where T : struct, IConvertible
        {
            if (HttpContext.Current.Request.QueryString[paramName.Description()] != null)
                //return (T)HttpContext.Current.Request.QueryString[paramName.Description()];
                return (T)Convert.ChangeType(HttpContext.Current.Request.QueryString[paramName.Description()], typeof(T));
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Use for non-Value types such as strings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramName"></param>
        /// <returns></returns>
        private static T GetNullableReferenceTypeValue<T>(QueryStringParamNames paramName) where T : class, IConvertible
        {
            if (HttpContext.Current.Request.QueryString[paramName.Description()] != null)
                //return (T)HttpContext.Current.Request.QueryString[paramName.Description()];
                return (T)Convert.ChangeType(HttpContext.Current.Request.QueryString[paramName.Description()], typeof(T));
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Use for non-nullable values. If the value is null, exception is thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramName"></param>
        /// <returns></returns>
        private static T GetNonNullableParamValue<T>(QueryStringParamNames paramName) 
        {
            if (HttpContext.Current.Request.QueryString[paramName.Description()] != null)
                return (T)Convert.ChangeType(HttpContext.Current.Request.QueryString[paramName.Description()], typeof(T));
            else
            {
                throw new ShiptalkCommon.ShiptalkException("Attempt to access value in QueryString when not present. Param: " + paramName.Description(), false);
            }
        }

        #region Public methods
        
        public static string EmailVerificationTokenString
        {
            get
            {
                 //return GetParamValue<String>(QueryStringParamNames.EMAIL_VERIFICATION_TOKEN_STRING);
                return GetNullableReferenceTypeValue<string>(QueryStringParamNames.EMAIL_VERIFICATION_TOKEN_STRING) + string.Empty;
                
            }
        }

        public static string PasswordResetTokenString
        {
            get
            {
                return GetNullableReferenceTypeValue<string>(QueryStringParamNames.PASSWORD_RESET_TOKEN_STRING) + string.Empty;

            }
        }

        public static string SessionLockOutString
        {
            get
            {
                return GetNullableReferenceTypeValue<string>(QueryStringParamNames.SESSION_LOCKOUT_STRING) + string.Empty;

            }
        }
        //Added - Lavanya:06/25/2012
        public static string EmailVerificationTypeString
        {
            get
            {
                return GetNullableReferenceTypeValue<string>(QueryStringParamNames.EMAIL_VERIFICATION_TYPE_STRING) + string.Empty;

            }
        }
        public static int InfoLibParentId
        {
            get
            {
                int? val = GetNullableValueTypeValue<int>(QueryStringParamNames.INFOLIB_PARENTID_INT);
                return val.HasValue ? val.Value : 0;

            }
        }

        public static int? InfoLibItemId
        {
            get
            {
                int? val = GetNullableValueTypeValue<int>(QueryStringParamNames.INFOLIB_ITEMID_INT);
                return val;

            }
        }

        public static int? InfoLibRequestedFileIdentifier
        {
            get
            {
                int? val = GetNullableValueTypeValue<int>(QueryStringParamNames.INFOLIB_REQ_FILE_IDEN_INT);
                return val;

            }
        }

        public static string InfoLibSearchText
        {
            get
            {
                return GetNullableReferenceTypeValue<string>(QueryStringParamNames.INFOLIB_SEARCH_TEXT) + string.Empty;

            }
        }

        public static InfoLibSpecialIdentifiers? InfoLibSpecialIdentifier
        {
            get
            {
                string sSpecialIdentifier = GetNullableReferenceTypeValue<string>(QueryStringParamNames.INFOLIB_SPECIAL_IDENTIFIER) + string.Empty;
                return string.IsNullOrEmpty(sSpecialIdentifier) ? (InfoLibSpecialIdentifiers?)null : (InfoLibSpecialIdentifiers) Enum.Parse(typeof(InfoLibSpecialIdentifiers), sSpecialIdentifier);
            }
        }

        #endregion
    }
}
