using hotel_management_api_identity.Core.Helpers.Models;
using System.Reflection;

namespace hotel_management_api_identity.Core.Helpers.Extension
{
    public static class EnumExtensions
    {
        public static string ResponseCode(this Enum value)
        {
            string result = value.ToString("D").PadLeft(2, '0');
            return result;
        }

        public static string DisplayName(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            return Attribute.GetCustomAttribute(field, typeof(EnumDisplayAttribute)) is not EnumDisplayAttribute attribute ? value.ToString() : attribute.Name;
        }

        public static string Description(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            return Attribute.GetCustomAttribute(field, typeof(EnumDisplayAttribute)) is not EnumDisplayAttribute attribute ? value.ToString() : attribute.Description;
        }
    }
}