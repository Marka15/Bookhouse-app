using System.ComponentModel.DataAnnotations;

namespace Bookhouse.Api.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = null!;

        [MaxLength(20)]
        public string? Isbn { get; set; }

        public int? PublicationYear { get; set; }

        [Range(1, int.MaxValue)]
        public int? Pages { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }
        public string? CoverUrl { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();

    }
}
