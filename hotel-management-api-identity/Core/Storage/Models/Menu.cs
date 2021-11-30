using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Core.Helpers.Models;
using System.ComponentModel.DataAnnotations.Schema;
using TableNameAttribute = hotel_management_api_identity.Core.Helpers.Models.TableNameAttribute;

namespace hotel_management_api_identity.Core.Storage.Models
{
    [Table("Menu")]
    [TableName("Menu")]
    [Trailable]
    [Serializable]
    public class Menu : BaseEntity
    {
        public decimal Price { get; set; }
        public Enums.Category Category { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
    }
}