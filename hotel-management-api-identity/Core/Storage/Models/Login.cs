using hotel_management_api_identity.Core.Helpers.Models;
using System.ComponentModel.DataAnnotations.Schema;
using TableNameAttribute = hotel_management_api_identity.Core.Helpers.Models.TableNameAttribute;

namespace hotel_management_api_identity.Core.Storage.Models
{
    [Table("Login")]
    [TableName("Login")]
    [Trailable]
    [Serializable]
    public class Login : BaseEntity
    {
        public string Password { get; set; }
        public virtual Employee Employee { get; set; }
    }
}