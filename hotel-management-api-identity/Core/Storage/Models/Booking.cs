using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Core.Helpers.Models;
using System.ComponentModel.DataAnnotations.Schema;
using TableNameAttribute = hotel_management_api_identity.Core.Helpers.Models.TableNameAttribute;

namespace hotel_management_api_identity.Core.Storage.Models
{
    [Table("Booking")]
    [TableName("Booking")]
    [Trailable]
    [Serializable]
    public class Booking : BaseEntity
    {
        public bool HasDiscount { get; set; }
        public DateTimeOffset CheckInDate { get; set; }
        public DateTimeOffset CheckOutDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string Room { get; set; }
        public string BookingCode { get; set; } = Extensions.RandomBookingNumber();
        public string CustomerEmail { get; set; }
    }
}