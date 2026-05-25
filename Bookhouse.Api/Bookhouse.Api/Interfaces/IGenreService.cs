using Bookhouse.Api.Models.DTOs;

namespace Bookhouse.Api.Interfaces
{
    public interface IGenreService
    {
        Task<List<GenreDto>> GetAllGenres();

    }
}
