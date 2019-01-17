using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Core.ReflectionHelpers
{
    public static class ValueExtensions
    {

        public static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // returns the default value for the type
            }
            else
            {
                return (T)obj;
            }
        }


        public static string RemoveHtmlTags(this string objRef)
        {
            if (string.IsNullOrWhiteSpace(objRef))
            {
                return objRef;
            }

            return Regex.Replace(objRef, @"<[^>]*>", String.Empty);
        }

       


        public static string DescriptionAttr<T>(this T source)
        {
            string descript = "";

            FieldInfo fi = source.GetType().GetField(source.ToString());

            try
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    descript = attributes[0].Description;
                }
                else
                {
                    descript = source.ToString();
                }
            }
            catch (Exception)
            { //do nothing   
            }

            return descript;
        }

        public static T? ConvertToNullable<T>(this String s) where T : struct
        {
            try
            {
                return (T?)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(s);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool ContainsCaseInsensitive(this string text, string value, bool nullSafe = true)
        {
            if (nullSafe && text == null)
                return false;

            return text.IndexOf(value, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
    }
}
