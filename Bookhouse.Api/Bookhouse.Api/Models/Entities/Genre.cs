using System.ComponentModel.DataAnnotations;

namespace Bookhouse.Api.Models.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}
