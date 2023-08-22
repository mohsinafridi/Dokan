using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dokan.Service.Product.Models
{    
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]        
        public int ProductId { get; set; }

        public string? ProductName { get; set; }
        
        public string? ProductCode{ get; set; }
        public string? ProductDescription { get; set; }       
        public double Price { get; set; }
        public int ProductStock { get; set; }

    }
}
