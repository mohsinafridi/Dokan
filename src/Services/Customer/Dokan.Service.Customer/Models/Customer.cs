using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dokan.Service.Customer.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("customer_name")]
        public string? CustomerName { get;set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("phone")]
        public string? Phone{ get; set; }
    }
}
