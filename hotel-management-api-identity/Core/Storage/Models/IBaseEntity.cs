using System.ComponentModel.DataAnnotations;

namespace hotel_management_api_identity.Core.Storage.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
    }
}