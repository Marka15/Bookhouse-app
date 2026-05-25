using Bookhouse.Api.Data;
using Bookhouse.Api.Interfaces;
using Bookhouse.Api.Models.DTOs;
using Bookhouse.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookhouse.Api.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorDto>> GetAllAuthors()
        {
            return await _context.Authors
                .Include(a => a.BookAuthors)
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Bio = a.Bio,
                    BookCount = a.BookAuthors.Count
                })
                .ToListAsync();
        }

        public async Task<AuthorDto> CreateAuthor(CreateAuthorDto dto)
        {
            var author = new Author
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Bio = dto.Bio
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return new AuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Bio = author.Bio,
                BookCount = 0
            };
        }
        public async Task<bool> DeleteAuthor(int id)
        {
            var author = await _context.Authors
                .Include(a => a.BookAuthors)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null) return false;

            var bookIds = author.BookAuthors.Select(ba => ba.BookId).ToList();
            var books = await _context.Books
                .Where(b => bookIds.Contains(b.Id))
                .ToListAsync();

            _context.Books.RemoveRange(books);
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
