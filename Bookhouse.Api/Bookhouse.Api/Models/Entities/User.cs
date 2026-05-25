using System.ComponentModel.DataAnnotations;

namespace Bookhouse.Api.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = null!;
       
        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
