using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallBusinessSystem.Models

{
    public class Candy
    {
        [Key]
        public int CandyId { get; set; }

        [DisplayName("Candy Name")]
        public string CandyName { get; set; }
        public string Description { get; set; }
        [DisplayName("Price")]
        public decimal CandyPrice { get; set; }
        public string ImgUrl { get; set; }

        [DisplayName("Quantity")]
        public int CandyQty { get; set; }

    }
}
