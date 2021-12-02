using hotel_management_api_identity.Core.Helpers.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TableNameAttribute = hotel_management_api_identity.Core.Helpers.Models.TableNameAttribute;

namespace hotel_management_api_identity.Core.Storage.Models
{
    [Table("Tokens")]
    [TableName("Tokens")]
    [Trailable]
    [Serializable]
    public class Tokens : BaseEntity
    {
        [Required]
        public string Token { get; set; }
        public DateTimeOffset ExpiryDate { get; set; }
        public Tokens()
        {

        }
    }
}