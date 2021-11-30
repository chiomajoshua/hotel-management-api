using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Core.Helpers.Models;
using System.ComponentModel.DataAnnotations.Schema;
using TableNameAttribute = hotel_management_api_identity.Core.Helpers.Models.TableNameAttribute;

namespace hotel_management_api_identity.Core.Storage.Models
{
    [Table("Sales")]
    [TableName("Sales")]
    [Trailable]
    [Serializable]
    public class Sales : BaseEntity
    {
        public int Quantity { get; set; }        
        public decimal Price { get; set; }
        public Enums.Category Category { get; set; }
        public string Item { get; set; }
    }
}