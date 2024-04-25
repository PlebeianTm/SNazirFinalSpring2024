using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallBusinessSystem.Models
{
    public class OrderDetail
    {
            public int OrderDetailId { get; set; }
            public int OrderId { get; set; }
            [ForeignKey("OrderId")]
            [ValidateNever]
            public Order Order { get; set; }
            public int CandyId { get; set; }
            [ValidateNever]
            [ForeignKey("CandyId")]
            public Candy Candy{ get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
    }
}
