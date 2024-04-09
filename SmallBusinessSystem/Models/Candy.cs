using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallBusinessSystem.Models

{
    public class Candy
    {
        [Key]
        public int CandyId { get; set; }
        
        public string CandyName { get; set; }
        public string Description { get; set; }
        public decimal CandyPrice { get; set; }
        public string? ImgUrl { get; set; }

        public int CandyQty { get; set; }

    }
}
