using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ShiptalkLogic.BusinessObjects;
using ShiptalkLogic.BusinessObjects.UI;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Specialized;

namespace ShiptalkLogic
{
    public static class Extensions
    {
        /// <summary>
        /// Descriptions the specified @enum.
        /// </summary>
        /// <param name="enum">The @enum to get a description for.</param>
        /// <returns></returns>
        public static string Description(this Enum @enum)
        {
            string value = @enum.ToString();
            Type type = @enum.GetType();

            var descAttribute =
                (DescriptionAttribute[])type.GetField(value).GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descAttribute.Length > 0 ? descAttribute[0].Description : value;
        }

        public static string Description(this object @object)
        {
            string value = @object.ToString();
            Type type = @object.GetType();

            var descAttribute =
                (DescriptionAttribute[])type.GetField(value).GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descAttribute.Length > 0 ? descAttribute[0].Description : value;
        }

        public static IEnumerable<KeyValuePair<int, string>> Descriptions(this Type @enum)
        {
            var descriptions = new List<KeyValuePair<int, string>>();

            if (!(@enum.IsEnum))
                return null;

            string[] names = Enum.GetNames(@enum);

            foreach (string name in names)
            {
                object enumValue = Convert.ChangeType(Enum.Parse(@enum, name), @enum);
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

        public static Role GetRole(this UserAccount AccountInfo)
        {
            return BusinessLayer.LookupBLL.GetRole(AccountInfo.Scope, AccountInfo.IsAdmin);
        }

        /// <summary>
        /// Camel Cases as string. For example "csharp is good" is converted to "Csharp Is Good"
        /// Useful for creating emails such as "Dear Firstname Lastname," rather than "Dear firstname lastname,"
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToCamelCasing(this string input)// I think you mean Pascal Case.
        {
            //return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input);
            if (!string.IsNullOrEmpty(input))
                return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input);
            else
                return string.Empty;
        }

        /// <summary>
        /// Add specified number of html new line tags <BR />
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="NumberOfLines"></param>
        public static void AddNewHtmlLines(this StringBuilder builder, Int16 NumberOfLines)
        {
            string line = "<BR />";
            for (int i = 0; i < NumberOfLines; i++)
                builder.Append(line);
        }

        /// <summary>
        /// Add one html new line tag <BR />
        /// </summary>
        /// <param name="builder"></param>
        public static void AddNewHtmlLine(this StringBuilder builder)
        {
            string line = "<BR />";
            builder.Append(line);
        }


        /// <summary>
        /// Compare a scope to another scope.
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

        public static TResult GetDefaultIfDBNull<TResult>(this IDataRecord dataRecord, string name,
                                                          Func<IDataRecord, int, TResult> expression, TResult @default)
        {
            int ordinal = dataRecord.GetOrdinal(name);

            return dataRecord[ordinal] == DBNull.Value ? @default : expression(dataRecord, ordinal);
        }

        public static void SetLastUpdated(this IModified modified, int userId)
        {
            if (modified == null)
                return;

            modified.LastUpdatedBy = userId;
            modified.LastUpdatedDate = DateTime.Now;
        }

        public static void SetCreated(this IModified modified, int userId)
        {
            if (modified == null)
                return;

            modified.CreatedBy = userId;
            modified.CreatedDate = DateTime.Now;
        }

        public static void SetIsActive(this IIsActive isActive, bool active)
        {
            if (isActive == null)
                return;

            isActive.IsActive = active;
            isActive.ActiveInactiveDate = DateTime.Now;
        }

        public static string SerializeToXmlString(this object @object)
        {
            var builder = new StringBuilder();

            using (var writer = new XmlTextWriter(new StringWriter(builder)) { Formatting = Formatting.Indented })
            {
                var serializer = new XmlSerializer(@object.GetType());
                serializer.Serialize(writer, @object);

                return builder.ToString();
            }
        }

        public static T DeserializeToType<T>(this string xml) where T : class
        {
            //try
            //{
            var serilizer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(xml))
                return (T)serilizer.Deserialize(reader);
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
        }

        #region "Mapping Extensions"

        public static IList<SubStateRegionZIPFIPSForReport> ToSubStateRegionZIPFIPSForReportList(this string[] countyZipFIPSCodes)
        {
            if (countyZipFIPSCodes == null)
                return null;

            var p = new List<SubStateRegionZIPFIPSForReport>();

            for (int i = 0; i < countyZipFIPSCodes.Length; i++)
                p.Add(new SubStateRegionZIPFIPSForReport { ZIPFIPSCountyCode = countyZipFIPSCodes[i] });

            return p;
        }
        

        public static IList<County> ToCountyList(this string[] countyFIPSCodes)
        {
            if (countyFIPSCodes == null)
                return null;

            var counties = new List<County>();

            for (int i = 0; i < countyFIPSCodes.Length; i++)
                counties.Add(new County { Code = countyFIPSCodes[i] });

            return counties;
        }

        public static IList<Agency> ToAgencyList(this string[] agencyIds)
        {
            if (agencyIds == null)
                return null;

            var agencies = new List<Agency>();

            for (int i = 0; i < agencyIds.Length; i++)
                agencies.Add(new Agency { Id = Convert.ToInt32(agencyIds[i]) });

            return agencies;
        }

        public static AgencyLocation ToAgencyLocation(this RegisterAgencyViewData viewData)
        {
            if (viewData == null)
                return null;

            var agencyLocation = new AgencyLocation
                                     {
                                         ActiveInactiveDate = viewData.ActiveInactiveDate,
                                         Comments = viewData.Comments,
                                         ContactFirstName = viewData.SponsorFirstName,
                                         ContactMiddleName = viewData.SponsorMiddleName,
                                         ContactLastName = viewData.SponsorLastName,
                                         ContactTitle = viewData.SponsorTitle,
                                         CreatedBy = viewData.CreatedBy,
                                         CreatedDate = viewData.CreatedDate,
                                         Fax = viewData.Fax,
                                         HoursOfOperation = viewData.HoursOfOperation,
                                         IsActive = viewData.IsActive,
                                         IsMainOffice = true,
                                         LastUpdatedBy = viewData.LastUpdatedBy,
                                         LastUpdatedDate = viewData.LastUpdatedDate,
                                         LocationName = viewData.Name,
                                         PrimaryEmail = viewData.PrimaryEmail,
                                         PrimaryPhone = viewData.PrimaryPhone,
                                         SecondaryEmail = viewData.SecondaryEmail,
                                         SecondaryPhone = viewData.SecondaryPhone,
                                         //State = viewData.State,
                                         TDD = viewData.TDD,
                                         TollFreePhone = viewData.TollFreePhone,
                                         TollFreeTDD = viewData.TollFreeTDD,
                                         //Added by Lavanya
                                         AvailableLanguages = viewData.AvailableLanguages,
                                         HideAgencyFromSearch = viewData.HideAgencyFromSearch,
                                         //end
                                         PhysicalAddress = new AgencyAddress
                                                               {
                                                                   Address1 = viewData.PhysicalAddress1,
                                                                   Address2 = viewData.PhysicalAddress2,
                                                                   City = viewData.PhysicalCity,
                                                                   County = new County { Code = viewData.PhysicalCounty },
                                                                   CreatedBy = viewData.CreatedBy,
                                                                   CreatedDate = viewData.CreatedDate,
                                                                   LastUpdatedBy = viewData.LastUpdatedBy,
                                                                   LastUpdatedDate = viewData.LastUpdatedDate,
                                                                   //State = viewData.State 
                                                                   Zip = viewData.PhysicalZIP,
                                                                   //added by Lavanya
                                                                   Longitude = viewData.Longitude,
                                                                   Latitude = viewData.Latitude
                                                                   //end
                                                                    
                                                               },
                                         MailingAddress = new AgencyAddress
                                                              {
                                                                  Address1 = viewData.MailingAddress1,
                                                                  Address2 = viewData.MailingAddress2,
                                                                  City = viewData.MailingCity,
                                                                  CreatedBy = viewData.CreatedBy,
                                                                  CreatedDate = viewData.CreatedDate,
                                                                  LastUpdatedBy = viewData.LastUpdatedBy,
                                                                  LastUpdatedDate = viewData.LastUpdatedDate,
                                                                  State = viewData.MailingState,
                                                                  Zip = viewData.MailingZIP
                                                              },
                                     };

            return agencyLocation;
        }

        public static AgencyLocation ToAgencyLocation(this EditAgencyViewData viewData)
        {
            if (viewData == null)
                return null;

            var agencyLocation = new AgencyLocation
            {
                ActiveInactiveDate = viewData.ActiveInactiveDate,
                Comments = viewData.Comments,
                ContactFirstName = viewData.SponsorFirstName,
                ContactMiddleName = viewData.SponsorMiddleName,
                ContactLastName = viewData.SponsorLastName,
                ContactTitle = viewData.SponsorTitle,
                CreatedBy = viewData.CreatedBy,
                CreatedDate = viewData.CreatedDate,
                Fax = viewData.Fax,
                HoursOfOperation = viewData.HoursOfOperation,
                IsActive = viewData.IsActive,
                IsMainOffice = true,
                LastUpdatedBy = viewData.LastUpdatedBy,
                LastUpdatedDate = viewData.LastUpdatedDate,
                LocationName = viewData.Name,
                PrimaryEmail = viewData.PrimaryEmail,
                PrimaryPhone = viewData.PrimaryPhone,
                SecondaryEmail = viewData.SecondaryEmail,
                SecondaryPhone = viewData.SecondaryPhone,
                TDD = viewData.TDD,
                TollFreePhone = viewData.TollFreePhone,
                TollFreeTDD = viewData.TollFreeTDD,
                //Added by Lavanya
                AvailableLanguages = viewData.AvailableLanguages,
                HideAgencyFromSearch = viewData.HideAgencyFromSearch,
                //end
                PhysicalAddress = new AgencyAddress
                {
                    Address1 = viewData.PhysicalAddress1,
                    Address2 = viewData.PhysicalAddress2,
                    City = viewData.PhysicalCity,
                    County = new County { Code = viewData.PhysicalCountyFIPS },
                    CreatedBy = viewData.CreatedBy,
                    CreatedDate = viewData.CreatedDate,
                    LastUpdatedBy = viewData.LastUpdatedBy,
                    LastUpdatedDate = viewData.LastUpdatedDate,
                    State = viewData.State,
                    Zip = viewData.PhysicalZip,
                    //Added by Lavanya
                    Longitude = viewData.Longitude,
                    Latitude = viewData.Latitude
                    //end
                },
                MailingAddress = new AgencyAddress
                {
                    Address1 = viewData.MailingAddress1,
                    Address2 = viewData.MailingAddress2,
                    City = viewData.MailingCity,
                    CreatedBy = viewData.CreatedBy,
                    CreatedDate = viewData.CreatedDate,
                    LastUpdatedBy = viewData.LastUpdatedBy,
                    LastUpdatedDate = viewData.LastUpdatedDate,
                    State = viewData.MailingState,
                    Zip = viewData.MailingZip
                },
            };

            return agencyLocation;
        }
        public static AgencyLocation ToAgencyLocation(this ViewAgencyProfileView viewData)
        {
            if (viewData == null)
                return null;

            var agencyLocation = new AgencyLocation
            {
                HoursOfOperation = viewData.HoursOfOperation,
                PrimaryEmail = viewData.PrimaryEmail,
                PrimaryPhone = viewData.PrimaryPhone,
                PhysicalAddress = new AgencyAddress
                {
                    Address1 = viewData.PhysicalAddress1,
                    Address2 = viewData.PhysicalAddress2,
                    City = viewData.PhysicalCity,
                    County = new County { Code = viewData.PhysicalCountyFIPS },
                    State = viewData.State,
                    Zip = viewData.PhysicalZip
                },
            };

            return agencyLocation;
        }

        public static IList<SpecialFieldValue> ToSpecialFieldValueList(this IDictionary dictionary)
        {
            if (dictionary == null)
                return null;

            var specialFieldValueList = new List<SpecialFieldValue>();

            foreach (DictionaryEntry entry in dictionary)
                specialFieldValueList.Add(new SpecialFieldValue
                                            {   Id = Convert.ToInt32(entry.Key.ToString()),
                                                Value = entry.Value.ToString().Replace("_", "") 
                                            });

            return specialFieldValueList;
        }

        public static IDictionary ToDictionary(this IList<SpecialFieldValue> specialFieldValues)
        {
            if (specialFieldValues == null)
                return null;

            var dictionary = new HybridDictionary();

            foreach (var specialFieldValue in specialFieldValues)
                dictionary.Add(specialFieldValue.Id, specialFieldValue.Value);

            return dictionary;
        }

        public static IList<T> ToIdList<T>(this IList<KeyValuePair<string, string>> list)
        {
            if (list == null)
                return null;

            var idList = new List<T>();
            var t = typeof(T);

            foreach (var item in list)
                idList.Add((T)Enum.Parse(t, item.Key));

            return idList;
        }

        public static IList<KeyValuePair<string, string>> ToKeyValuePairList<T>(this IList<T> list)
        {
            if (list == null)
                return null;

            if (!typeof(T).IsEnum)
                return null;

            var kvpList = new List<KeyValuePair<string, string>>();

            foreach (var value in list)
                kvpList.Add(new KeyValuePair<string, string>(Convert.ToInt32(value).ToString(), value.Description()));

            return kvpList;
        }

        #endregion

        #region Scope Related 
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

        #endregion
    
    }
}