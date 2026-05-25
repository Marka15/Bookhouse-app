using Bookhouse.Api.Models.Entities;
using System.ComponentModel.DataAnnotations;
namespace Bookhouse.Api.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context) {
            if (!context.Users.Any()) {
                context.Users.Add(new User
                {
                    Email = "admin@bookhouse.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                    CreatedAt = DateTime.UtcNow,
                });
                context.SaveChanges();
            }

            if (!context.Genres.Any()) {
                context.Genres.AddRange(
                  new Genre { Name = "Action" },
                  new Genre { Name = "Drama" },
                  new Genre { Name = "Comedy" },
                  new Genre { Name = "Horror" },
                  new Genre { Name = "Romance" },
                  new Genre { Name = "Thriller" },
                  new Genre { Name = "Musical" },
                  new Genre { Name = "Documentary" }
                );
            context.SaveChanges();
            }

            if (!context.Authors.Any())
            {
                context.Authors.AddRange(
                    new Author
                    {
                        FirstName = "Simon",
                        LastName = "Sinek",
                        Bio = "British-American author and motivational speaker, known for his work on leadership."
                    },
                    new Author
                    {
                        FirstName = "James",
                        LastName = "Clear",
                        Bio = "American author and speaker, known for his work on habits and decision making."
                    },
                    new Author
                     {
                        FirstName = "Nir",
                        LastName = "Eyal",
                        Bio = "Author and lecturer who writes about the intersection of psychology and technology."
                    },
                    new Author
                    {
                        FirstName = "Daniel",
                        LastName = "Kahneman",
                        Bio = "Israeli-American psychologist and economist, Nobel Prize winner."
                    },
                   new Author
                   {
                        FirstName = "Dale",
                        LastName = "Carnegie",
                        Bio = "American writer and lecturer known for self-improvement books."
                   }
            );
                context.SaveChanges();
            }

            if (!context.Books.Any())
            {
                var simonSinek = context.Authors.First(a => a.LastName == "Sinek");
                var jamesClear = context.Authors.First(a => a.LastName == "Clear");
                var nirEyal = context.Authors.First(a => a.LastName == "Eyal");
                var kahneman = context.Authors.First(a => a.LastName == "Kahneman");
                var carnegie = context.Authors.First(a => a.LastName == "Carnegie");

                var drama = context.Genres.First(g => g.Name == "Drama");
                var thrilled = context.Genres.First(g => g.Name == "Thriller");

                var books = new List<Book>
            {
                new Book
                {
                    Title = "Start With Why",
                    Isbn = "9781591846444",
                    PublicationYear = 2009,
                    Pages = 256,
                    Price = 24.99m,
                    CoverUrl = "https://covers.openlibrary.org/b/isbn/9781591846444-L.jpg",
                    BookAuthors = new List<BookAuthor>
                    {
                        new BookAuthor { Author = simonSinek }
                    },
                    BookGenres = new List<BookGenre>
                    {
                        new BookGenre { Genre = drama }
                    }
                },
                new Book
                {
                    Title = "Atomic Habits",
                    Isbn = "9780735211292",
                    PublicationYear = 2018,
                    Pages = 320,
                    Price = 19.99m,
                    CoverUrl = "https://covers.openlibrary.org/b/isbn/9780735211292-L.jpg",
                    BookAuthors = new List<BookAuthor>
                    {
                        new BookAuthor { Author = jamesClear }
                    },
                    BookGenres = new List<BookGenre>
                    {
                        new BookGenre { Genre = drama }
                    }
                },
                new Book
                {
                    Title = "Hooked",
                    Isbn = "9781591847786",
                    PublicationYear = 2014,
                    Pages = 256,
                    Price = 17.99m,
                    CoverUrl = "https://covers.openlibrary.org/b/isbn/9781591847786-L.jpg",
                    BookAuthors = new List<BookAuthor>
                    {
                        new BookAuthor { Author = nirEyal }
                    },
                    BookGenres = new List<BookGenre>
                    {
                        new BookGenre { Genre = thrilled }
                    }
                },
                new Book
                {
                    Title = "Thinking, Fast and Slow",
                    Isbn = "9780374533557",
                    PublicationYear = 2011,
                    Pages = 499,
                    Price = 22.99m,
                    CoverUrl = "https://covers.openlibrary.org/b/isbn/9780374533557-L.jpg",
                    BookAuthors = new List<BookAuthor>
                    {
                        new BookAuthor { Author = kahneman }
                    },
                    BookGenres = new List<BookGenre>
                    {
                        new BookGenre { Genre = thrilled}
                    }
                },
                new Book
                {
                    Title = "How to Win Friends and Influence People",
                    Isbn = "9780671027032",
                    PublicationYear = 1936,
                    Pages = 288,
                    Price = 15.99m,
                    CoverUrl = "https://covers.openlibrary.org/b/isbn/9780671027032-L.jpg",
                    BookAuthors = new List<BookAuthor>
                    {
                        new BookAuthor { Author = carnegie }
                    },
                    BookGenres = new List<BookGenre>
                    {
                        new BookGenre { Genre = drama }
                    }
                },
                new Book
                {
                    Title = "Leaders Eat Last",
                    Isbn = "9781591845324",
                    PublicationYear = 2014,
                    Pages = 368,
                    Price = 21.99m,
                    CoverUrl = "https://covers.openlibrary.org/b/isbn/9781591845324-L.jpg",
                    BookAuthors = new List<BookAuthor>
                    {
                        new BookAuthor { Author = simonSinek }
                    },
                    BookGenres = new List<BookGenre>
                    {
                        new BookGenre { Genre = drama }
                    }
                }
            };

                context.Books.AddRange(books);
                context.SaveChanges();
            }


        }
    }
}
