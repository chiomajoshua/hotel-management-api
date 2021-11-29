using hotel_management_api_identity.Core.Helpers.Models;
using System.Dynamic;

namespace hotel_management_api_identity.Core.Helpers.Extension
{
    public static class Extensions
    {
        public static bool IgnoreProperty<T>(this Type typeObject, string propertyName)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return Attribute.IsDefined(typeObject.GetProperty(propertyName), typeof(IgnoreDuringInsertOrUpdateAttribute), false);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public static bool IgnoreTrailProperty<T>(this Type typeObject, string propertyName)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return Attribute.IsDefined(typeObject.GetProperty(propertyName), typeof(IgnoreDuringInsertOrUpdateAttribute), false);
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public static string? GetTableName<T>(this Type entity)
        {
            if (!Attribute.IsDefined(entity, typeof(TableNameAttribute), false))
            {
                if (Attribute.IsDefined(entity, typeof(TableNameAttribute), false))
                {
                    var tableNameAttribute1 = Attribute.GetCustomAttribute(entity, attributeType: typeof(TableNameAttribute)) as TableNameAttribute;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    return tableNameAttribute1.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
                return null;
            }

            var tableNameAttribute = Attribute.GetCustomAttribute(entity, typeof(TableNameAttribute)) as TableNameAttribute;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return tableNameAttribute.Name;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public static T? GetClassInstance<T>(this Dictionary<string, object> dict, Type type) where T : class
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
    }
}