using Bookhouse.Api.Data;
using Bookhouse.Api.Interfaces;
using Bookhouse.Api.Models.DTOs;
using Bookhouse.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookhouse.Api.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;
        public BookService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResultDto<BookDto>> GetAllBooks(string? search, string? sortBy, int page, int pageSize) {

            var query = _context.Books
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .AsQueryable();


            if (!string.IsNullOrEmpty(search))
            {
                var searchLower = search.ToLower();

                query = query.Where(b =>
                    b.Title.ToLower().Contains(searchLower) ||
                    b.BookAuthors.Any(ba =>
                        ba.Author.FirstName.ToLower().Contains(searchLower) ||
                        ba.Author.LastName.ToLower().Contains(searchLower) ||
                        (ba.Author.FirstName + " " + ba.Author.LastName)
                            .ToLower().Contains(searchLower)
                    )
                );
            }

            query = sortBy switch
            {
                "title" => query.OrderBy(b => b.Title),
                "price_asc" => query.OrderBy(b => b.Price),
                "price_desc" => query.OrderByDescending(b => b.Price),
                "year" => query.OrderByDescending(b => b.PublicationYear),
                _ => query.OrderBy(b => b.Id)
            };

            var totalCount = await query.CountAsync();

            var items = await query
             .Skip((page - 1) * pageSize)
             .Take(pageSize)
             .Select(b => new BookDto
             {
                Id = b.Id,
                Title = b.Title,
                Isbn = b.Isbn,
                PublicationYear = b.PublicationYear,
                Pages = b.Pages,
                Price = b.Price,
                CoverUrl = b.CoverUrl,
                Authors = b.BookAuthors
                   .Select(ba => ba.Author.FirstName + " " + ba.Author.LastName)
                   .ToList(),
                Genres = b.BookGenres
                    .Select(bg => bg.Genre.Name)
                    .ToList()
             }).ToListAsync();

            return new PagedResultDto<BookDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };

        }

        public async Task<BookDto?> GetBookById(int id)
        {

            var book = await _context.Books
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return null;

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Isbn = book.Isbn,
                PublicationYear = book.PublicationYear,
                Pages = book.Pages,
                Price = book.Price,
                CoverUrl = book.CoverUrl,
                Authors = book.BookAuthors
               .Select(ba => ba.Author.FirstName + " " + ba.Author.LastName)
               .ToList(),
                Genres = book.BookGenres
               .Select(bg => bg.Genre.Name)
               .ToList()
            };
        }

        public async Task<BookDto> CreateBook(CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Isbn = dto.Isbn,
                PublicationYear = dto.PublicationYear,
                Pages = dto.Pages,
                Price = dto.Price,
                CoverUrl = dto.CoverUrl
            };
            
            
            foreach (var authorId in dto.AuthorIds)
            {
                book.BookAuthors.Add(new BookAuthor
                {
                    AuthorId = authorId
                });
            }

            foreach (var genreId in dto.GenreIds)
            {
                book.BookGenres.Add(new BookGenre
                {
                    GenreId = genreId
                });
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return await GetBookById(book.Id) ?? throw new Exception("Book not found after creation");
        }

        public async Task<BookDto?> UpdateBook(int id, UpdateBookDto dto)
        {
            var book = await _context.Books
           .Include(b => b.BookAuthors)
           .Include(b => b.BookGenres)
           .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return null;

            book.Title = dto.Title ?? book.Title;
            book.Isbn = dto.Isbn ?? book.Isbn;
            book.PublicationYear = dto.PublicationYear ?? book.PublicationYear;
            book.Pages = dto.Pages ?? book.Pages;
            book.Price = dto.Price ?? book.Price;
            book.CoverUrl = dto.CoverUrl ?? book.CoverUrl;

            if (dto.AuthorIds != null)
            {
                book.BookAuthors.Clear();
                foreach (var authorId in dto.AuthorIds)
                {
                    book.BookAuthors.Add(new BookAuthor { AuthorId = authorId });
                }
            }

            if (dto.GenreIds != null)
            {
                book.BookGenres.Clear();
                foreach (var genreId in dto.GenreIds)
                {
                    book.BookGenres.Add(new BookGenre { GenreId = genreId });
                }
            }

            await _context.SaveChangesAsync();
            return await GetBookById(id);
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BookDto>> GetLatestBooksAsync(int count = 6)
        {
            return await _context.Books
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .OrderByDescending(b => b.Id)
                .Take(count)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    CoverUrl = b.CoverUrl,
                    Price = b.Price,
                    Authors = b.BookAuthors
                        .Select(ba => ba.Author.FirstName + " " + ba.Author.LastName)
                        .ToList(),
                    Genres = b.BookGenres
                        .Select(bg => bg.Genre.Name)
                        .ToList()
                })
                .ToListAsync();
        }

    }
}
