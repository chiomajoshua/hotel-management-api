namespace hotel_management_api_identity.Core.Helpers.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreDuringInsertOrUpdateAttribute : Attribute
    {
    }
}