using Bookhouse.Api.Data;
using Bookhouse.Api.Interfaces;
using Bookhouse.Api.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Bookhouse.Api.Services
{
    public class GenreService : IGenreService
    {
        private readonly AppDbContext _context;

        public GenreService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GenreDto>> GetAllGenres()
        {
            return await _context.Genres
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToListAsync();
        }
    }
}
