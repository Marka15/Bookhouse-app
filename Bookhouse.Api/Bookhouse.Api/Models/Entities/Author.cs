using System.ComponentModel.DataAnnotations;

namespace Bookhouse.Api.Models.Entities
{
    public class Author
    {
        public int Id { get; set; }
       
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = null!;
       
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = null!;
        public string? Bio { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
