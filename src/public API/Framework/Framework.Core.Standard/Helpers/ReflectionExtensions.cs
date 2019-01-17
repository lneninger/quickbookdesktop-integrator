using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Framework.Core.ReflectionHelpers
{
    public static class ReflectionExtensions
    {
        public static object CreateInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static T CreateInstance<T>()
        {
            return (T)Activator.CreateInstance<T>();
        }

        public static string GetPropertyName(this LambdaExpression expression)
        {
            if (expression.Body is UnaryExpression)
            {
                UnaryExpression unex = (UnaryExpression)expression.Body;
                if (unex.NodeType == ExpressionType.Convert)
                {
                    Expression ex = unex.Operand;
                    MemberExpression mex = (MemberExpression)ex;
                    return mex.Member.Name;
                }
            }

            MemberExpression memberExpression = (MemberExpression)expression.Body;
            MemberExpression memberExpressionOrg = memberExpression;

            string Path = "";

            while (memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
            {
                var propInfo = memberExpression.Expression
                  .GetType().GetProperty("Member");
                var propValue = propInfo.GetValue(memberExpression.Expression, null)
                  as PropertyInfo;
                Path = propValue.Name + "." + Path;

                memberExpression = memberExpression.Expression as MemberExpression;
            }

            return Path + memberExpressionOrg.Member.Name;
        }

        public static object GetObjectPropertyValue<T>(this T objRef, string propertyName, bool throwException = false)
        {
            var type = objRef == null ? typeof(T) : objRef.GetType();
            var propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo == null)
            {
                if (throwException)
                {
                    throw new ArgumentException($"Property {propertyName} was not found in the type {type.FullName}");
                }

                return null;
            }

            var result = propertyInfo.GetValue(objRef);

            return result;
        }

        public static object GetStaticPropertyValue(this Type objRef, string propertyName)
        {
            var propertyInfo = objRef.GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property {propertyName} was not found in the type {objRef.FullName}");
            }

            var result = propertyInfo.GetValue(null);

            return result;
        }


        public static string ToPascalCase(this string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null) return the_string;
            if (the_string.Length < 2) return the_string.ToUpper();

            // Split the string into words.
            string[] words = the_string.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = "";
            foreach (string word in words)
            {
                result +=
                    word.Substring(0, 1).ToUpper() +
                    word.Substring(1);
            }

            return result;
        }


        public static bool IsCustomPrimitive(this Type objRef)
        {
            if (objRef.IsPrimitive || objRef == typeof(Decimal) || objRef == typeof(String) || objRef == typeof(DateTime))
            {
                return true;
            }

            return false;
        }
    }
}
