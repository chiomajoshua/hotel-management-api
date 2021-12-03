using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Core.Helpers.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TableNameAttribute = hotel_management_api_identity.Core.Helpers.Models.TableNameAttribute;

namespace hotel_management_api_identity.Core.Storage.Models
{
    [Table("Customer")]
    [TableName("Customer")]
    [Trailable]
    [Serializable]
    public class Customer : BaseEntity
    {
        [StringLength(15)]
        [Required]
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public Enums.Id IdType { get; set; }
        public string IdNumber { get; set; }
        [Required]
        public string Email { get; set; }        
        public Enums.Gender Gender { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

        public Customer()
        {

        }
    }
}