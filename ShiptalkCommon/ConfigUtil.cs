using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Web.Configuration;

using System.ComponentModel;

namespace ShiptalkCommon
{

    public static class ConfigUtil
    {

        //One place to refer to all Web.config keys used by the class.
        private enum ConfigUtilKeys
        {
            [Description("HostingPlace")]
            HostingPlace,
            [Description("PasswordMinLength")]
            PasswordMinLength,
            [Description("PasswordMaxLength")]
            PasswordMaxLength,
             [Description("PasswordWarningAfterHowManyDays")]
            PasswordWarningAfterHowManyDays,
            [Description("GoogleApiKey")]
            GoogleApiKey,
            [Description("OverrideEmail")]
            OverrideEmail,
            [Description("Email_Server")]
            EmailServer,
            [Description("Email_TechSupport")]
            EmailOfTechSupport,
            [Description("CriticalErrorEmailsCC")]
            EmailOfCriticalErrorCC,
            [Description("Email_ResourceCenter")]
            EmailOfResourceCenter,
            [Description("WebEnvironment")]
            WebEnvironment,
            [Description("ShiptalkUrl")]
            ShiptalkUrl,
            [Description("EmailServerRequiresAuthentication")]
            EmailServerRequiresAuthentication,
            [Description("EmailServerUserName")]
            EmailServerUserName,
            [Description("EmailServerPassword")]
            EmailServerPassword,
            [Description("support_phone")]
            SHIPtalkSupportPhone,
            [Description("Session_TimeOut")]
            SessionTimeOut,
            [Description("PendingUserRegistrationDays")]
            PendingUserRegistrationDays,
            [Description("SecureAllPages")]
            SecureAllPages,
            [Description("EmailValidationRegEx")]
            EmailValidationRegEx,
            [Description("InfoLibLinkAllowedExtensions")]
            InfoLibLinkAllowedExtensions,
            [Description("InfoLibHeaderAllowedExtensions")]
            InfoLibHeaderAllowedExtensions
        }

        private static readonly int SESSION_TIMEOUT_DEFAULT_IN_MINS = 40;

        static ConfigUtil()
        {
            //All Non-Nullable values, meaning key/value pair that MUST exist in config file are loaded here.
            GoogleApiKey = GetNonNullableParamValue<string>(ConfigUtilKeys.GoogleApiKey);

            //User Registration; Login related
            PasswordMinLength = GetNonNullableParamValue<int>(ConfigUtilKeys.PasswordMinLength);
            PasswordMaxLength = GetNonNullableParamValue<int>(ConfigUtilKeys.PasswordMaxLength);
            PasswordWarningAfterHowManyDays = GetNonNullableParamValue<int>(ConfigUtilKeys.PasswordWarningAfterHowManyDays);
            PendingUserRegistrationDays = GetNonNullableParamValue<int>(ConfigUtilKeys.PendingUserRegistrationDays);

            //Emai related
            HostingPlace = GetNonNullableParamValue<string>(ConfigUtilKeys.HostingPlace);
            OverrideEmailAddress = GetNullableReferenceTypeValue<string>(ConfigUtilKeys.OverrideEmail) + string.Empty;
            MustOverrideEmail = !(OverrideEmailAddress == string.Empty);

            EmailServer = GetNonNullableParamValue<string>(ConfigUtilKeys.EmailServer);
            EmailServerUserName = GetNonNullableParamValue<string>(ConfigUtilKeys.EmailServerUserName);
            EmailServerPassword = GetNonNullableParamValue<string>(ConfigUtilKeys.EmailServerPassword);
            EmailOfTechSupport = GetNonNullableParamValue<string>(ConfigUtilKeys.EmailOfTechSupport);
            EmailOfTechSupport = GetNonNullableParamValue<string>(ConfigUtilKeys.EmailOfCriticalErrorCC);
            
            EmailOfResourceCenter = GetNonNullableParamValue<string>(ConfigUtilKeys.EmailOfResourceCenter);

            //Regular expression for validating email addresses
            EmailValidationRegex = GetNonNullableParamValue<string>(ConfigUtilKeys.EmailValidationRegEx);

            //Web site environment related
            WebEnvironment = GetNonNullableParamValue<string>(ConfigUtilKeys.WebEnvironment);
            ShiptalkUrl = GetNonNullableParamValue<string>(ConfigUtilKeys.ShiptalkUrl);
            EmailServerRequiresAuthentication = GetNonNullableParamValue<bool>(ConfigUtilKeys.EmailServerRequiresAuthentication);
            SecureAllPages = GetNonNullableParamValue<bool>(ConfigUtilKeys.SecureAllPages);

            //Registration / Email Confirmation / Password Reset etc.,
            PasswordResetUrl = ShiptalkUrl + (ShiptalkUrl.EndsWith("/") ? "" : "/") + "NewPass.aspx";
            EmailConfirmationUrl = ShiptalkUrl + (ShiptalkUrl.EndsWith("/") ? "" : "/") + "EmailConf.aspx";

            //Contact Shiptalk, Support
            ShiptalkSupportPhone = GetNonNullableParamValue<string>(ConfigUtilKeys.SHIPtalkSupportPhone);

            //Session stuff
            SessionTimeOutInMinutes = GetNullableValueTypeValue<int>(ConfigUtilKeys.SessionTimeOut) ?? SESSION_TIMEOUT_DEFAULT_IN_MINS;

            //Infolib stuff
            InfolibLinkAllowedExtensions = GetNullableReferenceTypeValue<string>(ConfigUtilKeys.InfoLibLinkAllowedExtensions) ?? string.Empty;
            InfolibHeaderAllowedExtensions = GetNullableReferenceTypeValue<string>(ConfigUtilKeys.InfoLibHeaderAllowedExtensions) ?? string.Empty;

            
        }



        #region Public Gets/Private sets

        public static int PasswordMinLength { get; private set; }

        public static int PasswordMaxLength { get; private set; }

        public static int PasswordWarningAfterHowManyDays { get; private set; } //sammit 60days password expiration date value can be changed from web.config


        public static string GoogleApiKey { get; private set; }

        public static string OverrideEmailAddress { get; private set; }

        public static string HostingPlace { get; private set; }
        public static string EmailServer { get; private set; }
        public static string EmailServerUserName { get; private set; }
        public static string EmailServerPassword { get; private set; }

        public static string EmailOfTechSupport { get; private set; }
        public static string EmailOfCriticalErrorCC { get; private set; }

        public static string EmailOfResourceCenter { get; private set; }
        public static string ShiptalkSupportPhone { get; private set; }
        public static string ShiptalkUrl { get; private set; }
        public static string WebEnvironment { get; private set; }

        public static string EmailConfirmationUrl { get; private set; }
        public static string PasswordResetUrl { get; private set; }

        public static string EmailValidationRegex { get; private set; }

        public static bool MustOverrideEmail { get; private set; }
        public static bool EmailServerRequiresAuthentication { get; private set; }
        public static bool SecureAllPages { get; private set; }
        public static int PendingUserRegistrationDays { get; private set; }

        //Session stuff
        public static int SessionTimeOutInMinutes { get; private set; }


        //Infolib stuff
        public static string InfolibLinkAllowedExtensions { get; private set; }
        public static string InfolibHeaderAllowedExtensions { get; private set; }

        #endregion



        #region Private Methods used to retrieve from config file. All public properties demand nullable or non-nullable values.
        /// <summary>
        /// This method returns null if value not found. Does not throw an exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramName"></param>
        /// <returns></returns>
        private static Nullable<T> GetNullableValueTypeValue<T>(ConfigUtilKeys paramName) where T : struct, IConvertible
        {
            string val = GetVal(paramName.Description()) + string.Empty;
            if (val != string.Empty)
                return (T)Convert.ChangeType(val, typeof(T));
            else
                return null;

        }

        /// <summary>
        /// This method will return null if value is not found. Does not throw exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramName"></param>
        /// <returns></returns>
        private static T GetNullableReferenceTypeValue<T>(ConfigUtilKeys paramName) where T : class, IConvertible
        {
            string val = GetVal(paramName.Description()) + string.Empty;
            if (val != string.Empty)
                return (T)Convert.ChangeType(val, typeof(T));
            else
                return null;

        }

        /// <summary>
        /// Use for non-nullable values. If the value is null, exception is thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramName"></param>
        /// <returns></returns>
        private static T GetNonNullableParamValue<T>(ConfigUtilKeys paramName)
        {
            string val = GetVal(paramName.Description()) + string.Empty;
            if (val != string.Empty)
                return (T)Convert.ChangeType(val, typeof(T));
            else
                throw new ConfigurationErrorsException(
                    string.Format("Application Setting \"{0}\" is required for the application to function correctly.", paramName.Description()));
        }

        /// <summary>
        /// The one and only method or access point for the actual Config value.
        /// Any API change in .Net framework to access AppSettinsg will affect this method alone.
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        private static string GetVal(string Key)
        {
            return ConfigurationManager.AppSettings[Key] + string.Empty;
        }

        #endregion
    }
}
