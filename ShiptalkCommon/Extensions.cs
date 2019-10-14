using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ShiptalkCommon
{
    /// <summary>
    /// A static class that contains extensions for aspects of web development.
    /// </summary>
    public static class Extensions
    {

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

        

    }
}