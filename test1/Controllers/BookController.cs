using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test1.Models;
using test1.Services;

namespace test1.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
    [Authorize]    
        public class BookController : ControllerBase
        {
            private readonly ILibraryService _libraryService;

            public BookController(ILibraryService libraryService)
            {
                _libraryService = libraryService;
            }

            [HttpGet]
            public async Task<IActionResult> GetBooks()
            {
                var books = await _libraryService.GetBooksAsync();
                if (books == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "No books in database.");
                }

                return StatusCode(StatusCodes.Status200OK, books);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetBooks(Guid id)
            {
                book book = await _libraryService.GetBookAsync(id);

                if (book == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, $"No book found for id: {id}");
                }

                return StatusCode(StatusCodes.Status200OK, book);
            }

            [HttpPost]
            public async Task<ActionResult<book>> AddBook(book book)
            {
                var dbBook = await _libraryService.AddBookAsync(book);

                if (dbBook == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{book.Title} could not be added.");
                }

                return CreatedAtAction("GetBook", new { id = book.Id }, book);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateBook(Guid id, book book)
            {
                if (id != book.Id)
                {
                    return BadRequest();
                }

                book dbBook = await _libraryService.UpdateBookAsync(book);

                if (dbBook == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{book.Title} could not be updated");
                }

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteBook(Guid id)
            {
                var book = await _libraryService.GetBookAsync(id);
                (bool status, string message) = await _libraryService.DeleteBookAsync(book);

                if (status == false)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, message);
                }

                return StatusCode(StatusCodes.Status200OK, book);
            }
        }
    }
