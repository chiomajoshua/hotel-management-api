using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Core.Helpers.Extension;
using hotel_management_api_identity.Core.Helpers.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TableNameAttribute = hotel_management_api_identity.Core.Helpers.Models.TableNameAttribute;

namespace hotel_management_api_identity.Core.Storage.Models
{
    [Table("Employee")]
    [TableName("Employee")]
    [Trailable]
    [Serializable]
    public class Employee : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Enums.Gender Gender { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public Enums.User UserType { get; set; }
        public string EmployeeCode { get; set; } = Extensions.RandomEmployeeNumber();
        public virtual Login Login {get; set;}


        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public Employee()
        {

        }
    }
}