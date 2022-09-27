using System.ComponentModel.DataAnnotations;

namespace Dokan.ApiGateway.Models
{
    public class AuthUser
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }   
    }
}
