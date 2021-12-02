using System.ComponentModel.DataAnnotations;

namespace hotel_management_api_identity.Core.Storage.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset ModifiedOn { get; set; } = DateTimeOffset.UtcNow;
        public string CreatedById { get; set; } = "System";
        public string ModifiedById { get; set; } = "System";
    }
}