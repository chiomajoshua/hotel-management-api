namespace hotel_management_api_identity.Core.Helpers.Models
{
    public class TableNameAttribute : Attribute
    {
        public string Name { get; set; }

        public TableNameAttribute(string name)
        {
            Name = name;
        }
    }
}