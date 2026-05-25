using Bookhouse.Api.Models.DTOs;

namespace Bookhouse.Api.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAllAuthors();
        Task<AuthorDto> CreateAuthor(CreateAuthorDto dto);
        Task<bool> DeleteAuthor(int id);
    }
}
