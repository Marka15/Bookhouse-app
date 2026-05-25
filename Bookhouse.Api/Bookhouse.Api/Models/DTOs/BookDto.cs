using System.ComponentModel.DataAnnotations;

namespace Bookhouse.Api.Models.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Isbn { get; set; }
        public int? PublicationYear { get; set; }
        public int? Pages { get; set; }
        public decimal? Price { get; set; }
        public string? CoverUrl { get; set; }
        public List<string> Authors { get; set; } = new();
        public List<string> Genres { get; set; } = new();
    }

    public class CreateBookDto
    {
        [Required]
        public string Title { get; set; } = null!;
        public string? Isbn { get; set; }
        public int? PublicationYear { get; set; }
        public int? Pages { get; set; }
        public decimal? Price { get; set; }
        public string? CoverUrl { get; set; }
        public List<int> AuthorIds { get; set; } = new();
        public List<int> GenreIds { get; set; } = new();
    }

    public class UpdateBookDto
    {
        public string? Title { get; set; } 
        public string? Isbn { get; set; }
        public int? PublicationYear { get; set; }
        public int? Pages { get; set; }
        public decimal? Price { get; set; }
        public string? CoverUrl { get; set; }
        public List<int>? AuthorIds { get; set; }
        public List<int>? GenreIds { get; set; }
    }
}
