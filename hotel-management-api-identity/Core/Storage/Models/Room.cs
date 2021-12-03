using hotel_management_api_identity.Core.Helpers.Models;
using System.ComponentModel.DataAnnotations.Schema;
using TableNameAttribute = hotel_management_api_identity.Core.Helpers.Models.TableNameAttribute;

namespace hotel_management_api_identity.Core.Storage.Models
{
    [Table("Room")]
    [TableName("Room")]
    [Trailable]
    [Serializable]
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}