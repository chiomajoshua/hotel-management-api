namespace hotel_management_api_identity.Core.Helpers.Models
{
    public class EnumDisplayAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}