using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dokan.Service.Product.Models
{    
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("product_name")]
        public string? ProductName { get; set; }

        [Column("product_code")]
        public string? ProductCode{ get; set; }

        [Column("price")]
        public double Price { get; set; }
    }
}
