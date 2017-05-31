using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;

namespace AppriseMobile
{
    public static class Extensions
    {
        #region MultipartFormDataContent
        /// <summary>
        /// Add a string to the MultipartFormDataContent
        /// </summary>
        /// <param name="stringContent">The value to add</param>
        /// <param name="name">The name of the value to add</param>
        /// <remarks>Wraps the string in a StringContent and adds it to the MultipartFormDataContent</remarks>
        public static void AddString(this MultipartFormDataContent content, string stringContent, string name)
        {
            if (stringContent != null) content.Add(new StringContent(stringContent), name);
        }

        /// <summary>
        /// Add a boolean to the MultipartFormDataContent
        /// </summary>
        /// <param name="boolContent">The value to add</param>
        /// <param name="name">The name of the value to add</param>
        /// <remarks>Converts the boolean into a lower-case string and adds it MultipartFormDataContent.AddString</remarks>
        public static void AddBool(this MultipartFormDataContent content, bool boolContent, string name)
        {
            content.AddString(boolContent.ToString().ToLowerInvariant(), name);
        }

        #endregion

        #region DateTime
        /// <summary>
        /// Converts the DateTime to universal time and the universal sortable format
        /// </summary>
        /// <returns>The formatted date/time string</returns>
        public static string ToUniversalISO(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("o");
        }
        public static string ToUniversalISO(this DateTime? dateTime)
        {
            if (dateTime != null) return dateTime.Value.ToUniversalTime().ToString("o");
            return null;
        }
        #endregion

        #region Enum
        public static string ToEnumString(this Enum value)
        {
            var enumMember = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(EnumMemberAttribute), false).FirstOrDefault() as EnumMemberAttribute;
            if (enumMember != null) return enumMember.Value;
            return value.ToString();
        }
        #endregion

        #region Array
        public static T GetValueOrDefault<T>(this T[] array, int index)
        {
            if (array.Length > index) return array[index];
            return default(T);
        }
        #endregion

        #region Dictionary
        public static TVal GetValueOrDefault<TKey, TVal>(this Dictionary<TKey, TVal> dictionary, TKey key)
        {
            if (dictionary.ContainsKey(key)) return dictionary[key];
            return default(TVal);
        }
        #endregion
    }
}
