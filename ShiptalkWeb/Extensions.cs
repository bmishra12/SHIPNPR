using System.Text;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using ShiptalkLogic.BusinessObjects;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration;
using System.Text.RegularExpressions;

namespace ShiptalkWeb
{
    /// <summary>
    /// A static class that contains extensions for aspects of web development.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Sanitizes HTML by removing reserved characters.
        /// </summary>
        /// <param name="input">The HTML to be sanitized.</param>
        /// <returns></returns>
        public static string GetSafeHtml(this string input)
        {
            return AntiXss.GetSafeHtml(input);
        }

        /// <summary>
        /// Sanitizes HTML by removing reserved characters.
        /// </summary>
        /// <param name="input">The HTML to be sanitized.</param>
        /// <returns></returns>
        public static string GetSafeHtml(this object input)
        {
            return input != null ? AntiXss.GetSafeHtml(input.ToString()) : string.Empty;
        }

        /// <summary>
        /// Sanitizes an HTML fragment by removing reserved characters.
        /// </summary>
        /// <param name="input">The HTML value to be sanitized.</param>
        /// <returns></returns>
        public static string GetSafeHtmlFragment(this string input)
        {
            return AntiXss.GetSafeHtmlFragment(input);
        }

        /// <summary>
        /// Sanitizes an HTML fragment by removing reserved characters.
        /// </summary>
        /// <param name="input">The HTML value to be sanitized.</param>
        /// <returns></returns>
        public static string GetSafeHtmlFragment(this object input)
        {
            return input != null ? AntiXss.GetSafeHtmlFragment(input.ToString()) : string.Empty;
        }

        /// <summary>
        /// Encodes an HTML attribute.
        /// </summary>
        /// <param name="input">The HTML attribute value to be encoded.</param>
        /// <returns></returns>
        public static string EncodeHtmlAttr(this string input)
        {
            return AntiXss.HtmlAttributeEncode(input);
        }

        /// <summary>
        /// Encodes an HTML attribute.
        /// </summary>
        /// <param name="input">The HTML attribute value to be encoded.</param>
        /// <returns></returns>
        public static string EncodeHtmlAttr(this object input)
        {
            return input != null ? AntiXss.HtmlAttributeEncode(input.ToString()) : string.Empty;
        }

        /// <summary>
        /// Encodes HTML.
        /// </summary>
        /// <param name="input">The HTML value to be encoded.</param>
        /// <returns></returns>
        public static string EncodeHtml(this string input)
        {
            return AntiXss.HtmlEncode(input);
        }


        /// <summary>
        /// Camel Cases as string. For example "csharp is good" is converted to "Csharp Is Good"
        /// Useful for creating emails such as "Dear Firstname Lastname," rather than "Dear firstname lastname,"
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToCamelCasing(this string input)
        {
            if(!string.IsNullOrEmpty(input))
                return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input);
            else
                return string.Empty;
        }


        /// <summary>
        /// Encodes HTML.
        /// </summary>
        /// <param name="input">The HTML value to be encoded.</param>
        /// <returns></returns>
        public static string EncodeHtml(this object input)
        {
            return input != null ? AntiXss.HtmlEncode(input.ToString()) : string.Empty;
        }

        /// <summary>
        /// Encodes javascript.
        /// </summary>
        /// <param name="input">The javascript value to be encoded.</param>
        /// <returns></returns>
        public static string EncodeJS(this string input)
        {
            return AntiXss.JavaScriptEncode(input);
        }

        /// <summary>
        /// Encodes javascript.
        /// </summary>
        /// <param name="input">The javascript value to be encoded.</param>
        /// <returns></returns>
        public static string EncodeJS(this object input)
        {
            return input != null ? AntiXss.JavaScriptEncode(input.ToString()) : string.Empty;
        }

        /// <summary>
        /// Encodes Url. 
        /// </summary>
        /// <param name="input">The Url value to be encoded.</param>
        /// <returns></returns>
        public static string EncodeUrl(this string input)
        {
            return AntiXss.UrlEncode(input);
        }

        /// <summary>
        /// Encodes Url. 
        /// </summary>
        /// <param name="input">The Url value to be encoded.</param>
        /// <returns></returns>
        public static string EncodeUrl(this object input)
        {
            return input != null ? AntiXss.UrlEncode(input.ToString()) : string.Empty;
        }

        /// <summary>
        /// Encodes VB script.
        /// </summary>
        /// <param name="input">The VB script value to be encoded.</param>
        /// <returns></returns>
        public static string EncodeVB(this string input)
        {
            return AntiXss.VisualBasicScriptEncode(input);
        }

        /// <summary>
        /// Encodes VB script.
        /// </summary>
        /// <param name="input">The VB script value to be encoded.</param>
        /// <returns></returns>
        public static string EncodeVB(this object input)
        {
            return input != null ? AntiXss.VisualBasicScriptEncode(input.ToString()) : string.Empty;
        }

        /// <summary>
        /// Encodes an XML attribute.
        /// </summary>
        /// <param name="input">The XML attribute value to be encoded.</param>
        /// <returns></returns>
        public static string EncodeXmlAttr(this string input)
        {
            return AntiXss.XmlAttributeEncode(input);
        }

        /// <summary>
        /// Encodes an XML attribute.
        /// </summary>
        /// <param name="input">The XML attribute value to be encoded.</param>
        /// <returns></returns>
        public static string EncodeXmlAttr(this object input)
        {
            return input != null ? AntiXss.XmlAttributeEncode(input.ToString()) : string.Empty;
        }

        /// <summary>
        /// Encodes XML.
        /// </summary>
        /// <param name="input">The XML value to be encoded.</param>
        /// <returns></returns>
        public static string EncodeXml(this string input)
        {
            return AntiXss.XmlEncode(input);
        }

        /// <summary>
        /// Encodes XML.
        /// </summary>
        /// <param name="input">The XML value to be encoded.</param>
        /// <returns></returns>
        public static string EncodeXml(this object input)
        {
            return input != null ? AntiXss.XmlEncode(input.ToString()) : string.Empty;
        }


        public static bool IsNull(this object input)
        {
            if (input != null)
                return false;
            else
                return true;
        }


        /// <summary>
        /// Descriptions the specified @enum.
        /// </summary>
        /// <param name="enum">The @enum to get a description for.</param>
        /// <returns></returns>
        public static string Description(this Enum @enum)
        {
            var value = @enum.ToString();
            var type = @enum.GetType();

            var descAttribute =
                (DescriptionAttribute[])type.GetField(value).GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descAttribute.Length > 0 ? descAttribute[0].Description : value;
        }

        public static IEnumerable<KeyValuePair<int, string>> Descriptions(this Type @enum)
        {
            var descriptions = new List<KeyValuePair<int, string>>();

            if (!(@enum is Enum))
                return null;

            var type = @enum.GetType();
            var names = Enum.GetNames(type);

            foreach (var name in names)
            {
                var enumValue = Convert.ChangeType(Enum.Parse(type, name), type);
                descriptions.Add(new KeyValuePair<int, string>((int)enumValue, ((Enum)enumValue).Description()));
            }

            return descriptions;
        }

        /// <summary>
        /// Get the Enum member using its value (Reverse lookup).
        /// This method supports all value types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        // Could be overloaded for other types.
        public static T ToEnumObject<T>(this ValueType value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }


        /// <summary>
        /// Get the value for the Enum member.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static T EnumValue<T>(this Enum @enum)
        {
            return (T)Enum.Parse(@enum.GetType(), @enum.ToString());
        }

        /// <summary>
        /// Converts an enum value during validation.
        /// </summary>
        /// <typeparam name="T">The type of enum to convert.</typeparam>
        /// <param name="e">The <see cref="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.ValueConvertEventArgs"/> instance containing the event data.</param>
        /// <returns>The converted enum instance.</returns>
        public static T ValueConvertEnum<T>(this ValueConvertEventArgs e)
        {
            var result = default(T);
            var type = (typeof(T).IsGenericType) ? Nullable.GetUnderlyingType(typeof(T)) : typeof(T);
            var @enum = (T)Enum.Parse(type, e.ValueToConvert.ToString());
            var values = Enum.GetValues(type);

            foreach (var value in values)
            {
                if (!value.Equals(@enum)) continue;

                result = @enum;

                break;
            }

            return result;
        }


        /// <summary>
        /// Converts value to State instance during validation.
        /// </summary>
        /// <param name="e">The <see cref="Microsoft.Practices.EnterpriseLibrary.Validation.Integration.ValueConvertEventArgs"/> instance containing the event data.</param>
        /// <returns>The converted State instance.</returns>
        public static State? ValueConvertState(this ValueConvertEventArgs e)
        {
            if (string.IsNullOrEmpty(e.ValueToConvert.ToString()))
                return null;

            var state = new State(e.ValueToConvert.ToString());

            return (string.IsNullOrEmpty(state.Code)) ? (State?)null : state;
        }

        public static string ValueConvertPhoneNumber(this ValueConvertEventArgs e)
        {
            return e.ValueToConvert.ToString().FormatPhoneNumber();
        }

        public static string FormatPhoneNumber(this string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return null;

            phoneNumber = phoneNumber.Replace("_", "");

            if (phoneNumber.Length == 16)
                phoneNumber = phoneNumber.Substring(0, (phoneNumber.IndexOf('x') - 1));

            return phoneNumber;
        }

        public static string FormatZip(this string zip)
        {
            if (string.IsNullOrEmpty(zip)) return null;

            zip = zip.Replace("_", "");

            if ((zip.Length > 5) && (zip.Length < 10))
                zip = zip.Substring(0, zip.IndexOf('-'));

            return zip;
        }

        public static string FormattedAddress(this IAddress address)
        {
            var addressBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(address.Address1))
            {
                addressBuilder.AppendFormat("{0}", address.Address1.Trim());

                if (!string.IsNullOrEmpty(address.Address2))
                    addressBuilder.AppendFormat(", {0}", address.Address2.Trim());

                addressBuilder.Append("<br />");
            }

            if (!string.IsNullOrEmpty(address.City))
                addressBuilder.AppendFormat("{0}<br />", address.City.Trim());

            addressBuilder.AppendFormat("{0} {1}", address.State.Value.StateAbbr, address.Zip.Trim());

            return addressBuilder.ToString();
        }

        public static void AddNewHtmlLines(this StringBuilder builder, Int16 NumberOfLines)
        {
            string line = "<BR />";
            for (int i = 0; i < NumberOfLines; i++)
                builder.Append(line);
        }

        public static void AddNewHtmlLine(this StringBuilder builder)
        {
            string line = "<BR />";
            builder.Append(line);
        }

        public static bool IsHigher(this Scope CurrentScope, Scope ScopeToCheck)
        {
            return CurrentScope.CompareTo(ScopeToCheck, ComparisonCriteria.IsHigher);
        }
        public static bool IsLower(this Scope CurrentScope, Scope ScopeToCheck)
        {
            return CurrentScope.CompareTo(ScopeToCheck, ComparisonCriteria.IsLower);
        }
        public static bool IsHigherOrEqualTo(this Scope CurrentScope, Scope ScopeToCheck)
        {
            return CurrentScope.CompareTo(ScopeToCheck, ComparisonCriteria.IsHigherThanOrEqualTo);
        }
        public static bool IsLowerOrEqualTo(this Scope CurrentScope, Scope ScopeToCheck)
        {
            return CurrentScope.CompareTo(ScopeToCheck, ComparisonCriteria.IsLowerThanOrEqualTo);
        }
        public static bool IsEqual(this Scope CurrentScope, Scope ScopeToCheck)
        {
            return CurrentScope.CompareTo(ScopeToCheck, ComparisonCriteria.IsEqual);
        }



        /// <summary>
        /// Get the value for the Enum member.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static bool CompareTo(this Scope @scope, Scope scopeToCompare, ComparisonCriteria criteria)
        {
            bool result = false;
            switch (criteria)
            {
                case ComparisonCriteria.IsHigher:
                    result = (@scope < scopeToCompare);
                    break;
                case ComparisonCriteria.IsHigherThanOrEqualTo:
                    result = (@scope <= scopeToCompare);
                    break;
                case ComparisonCriteria.IsLower:
                    result = (@scope > scopeToCompare);
                    break;
                case ComparisonCriteria.IsLowerThanOrEqualTo:
                    result = (@scope >= scopeToCompare);
                    break;
                case ComparisonCriteria.IsEqual:
                    result = (@scope == scopeToCompare);
                    break;
                default:
                    ShiptalkCommon.ShiptalkException.ThrowSecurityException("Unspecific ComparisonCriteria.", "An error occured while execution of request. Please contact support for assistance.");
                    break;
            }
            return result;
        }
    }
}