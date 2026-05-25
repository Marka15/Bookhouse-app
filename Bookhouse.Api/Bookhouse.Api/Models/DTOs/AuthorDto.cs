using System.ComponentModel.DataAnnotations;

namespace Bookhouse.Api.Models.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Bio { get; set; }
        public int BookCount { get; set; }
    }


    public class CreateAuthorDto
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public string? Bio { get; set; }
    }
}
