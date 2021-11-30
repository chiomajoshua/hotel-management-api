using hotel_management_api_identity.Core.Helpers.Models;
using System.Dynamic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace hotel_management_api_identity.Core.Helpers.Extension
{
    public static class Extensions
    {
        public static bool IgnoreProperty<T>(this Type typeObject, string propertyName)
        {
            return Attribute.IsDefined(typeObject.GetProperty(propertyName), typeof(IgnoreDuringInsertOrUpdateAttribute), false);
        }

        public static bool IgnoreTrailProperty<T>(this Type typeObject, string propertyName)
        {
            return Attribute.IsDefined(typeObject.GetProperty(propertyName), typeof(IgnoreDuringInsertOrUpdateAttribute), false);
        }

        public static string GetTableName<T>(this Type entity)
        {
            if (!Attribute.IsDefined(entity, typeof(TableNameAttribute), false))
            {
                if (Attribute.IsDefined(entity, typeof(TableNameAttribute), false))
                {
                    var tableNameAttribute1 = Attribute.GetCustomAttribute(entity, attributeType: typeof(TableNameAttribute)) as TableNameAttribute;
                    return tableNameAttribute1.Name;
                }
                return null;
            }

            var tableNameAttribute = Attribute.GetCustomAttribute(entity, typeof(TableNameAttribute)) as TableNameAttribute;
            return tableNameAttribute.Name;
        }

        public static T GetClassInstance<T>(this Dictionary<string, object> dict, Type type) where T : class
        {
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                var prop = type.GetProperty(kv.Key);
                if (prop == null) continue;

                prop.SetValue(obj, kv.Value, null);
            }
            return obj as T;
        }

        public static object ConvertToAnonymousObject(this Dictionary<string, object> dict)
        {
            var eo = new ExpandoObject();
            var eoColl = eo as ICollection<KeyValuePair<string, object>>;

            foreach (var kvp in dict)
            {
                eoColl.Add(kvp);
            }
            dynamic eoDynamic = eo;
            return eoDynamic;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                static string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}