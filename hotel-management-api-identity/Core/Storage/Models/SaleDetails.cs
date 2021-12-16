using hotel_management_api_identity.Core.Constants;
using hotel_management_api_identity.Core.Helpers.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_management_api_identity.Core.Storage.Models
{
    [Table("SaleDetails")]
    [TableName("SaleDetails")]
    [Trailable]
    [Serializable]
    public class SaleDetails : BaseEntity
    {
        public int Quantity { get; set; }
        public Enums.Category Category { get; set; }
        public string Item { get; set; }
        public decimal Price { get; set; }
        public string OrderCode { get; set; }
    }
}