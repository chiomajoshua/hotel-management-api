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
        public decimal Total { get; set; }
        public string OrderCode { get; set; }
    }
}