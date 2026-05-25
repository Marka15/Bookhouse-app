using Bookhouse.Api.Models.DTOs;

namespace Bookhouse.Api.Interfaces
{
    public interface IBookService
    {
        Task<PagedResultDto<BookDto>> GetAllBooks(string? search, string? sortBy, int page = 1,int pageSize = 8);
        Task<BookDto?> GetBookById(int id);
        Task<BookDto> CreateBook(CreateBookDto dto);
        Task<BookDto?> UpdateBook(int id, UpdateBookDto dto);
        Task<bool> DeleteBook(int id);
        Task<List<BookDto>> GetLatestBooksAsync(int count = 5);
    }
}
