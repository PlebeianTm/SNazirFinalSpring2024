using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallBusinessSystem.Models
{
    public class Cart 
    {
        
            public int CartId { get; set; }
            public int CandyId { get; set; }

            [ForeignKey("CandyId")]
            [ValidateNever]
            public Candy Candy{ get; set; }
           // public string UserId { get; set; }
            //[ForeignKey("UserId")]
            //[ValidateNever]
            //public ApplicationUser ApplicationUser { get; set; } // navigational property
            public int Quantity { get; set; }
            [NotMapped]
            public decimal SubTotal { get; set; }
        
    }
}
