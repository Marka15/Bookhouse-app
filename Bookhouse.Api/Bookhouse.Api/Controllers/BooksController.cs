using Bookhouse.Api.Interfaces;
using Bookhouse.Api.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookhouse.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] string? search,[FromQuery] string? sortBy,[FromQuery] int page = 1,[FromQuery] int pageSize = 8)
        {
            var result = await _bookService.GetAllBooks(search, sortBy, page, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdBook(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var book = await _bookService.CreateBook(dto);
            return CreatedAtAction(nameof(GetByIdBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var book = await _bookService.UpdateBook(id, dto);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBook(id);
            if (!result) return NotFound();
            return NoContent();
        }


        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest([FromQuery] int count = 6)
        {
            var books = await _bookService.GetLatestBooksAsync(count);
            return Ok(books);
        }
        
    }
}
