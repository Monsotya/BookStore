using Bookstore.Models;
using Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMemoryCache _cache;

        public BookController(IBookService bookService, IMemoryCache cache)
        {
            _bookService = bookService;
            _cache = cache;
        }

        /// <summary>
        /// Retrieves all books.
        /// </summary>
        /// <returns>A collection of books.</returns>
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Book>> GetAll() => await _bookService.GetAllBooks();

        /// <summary>
        /// Retrieves all books sorted by title.
        /// </summary>
        /// <param name="isDescending">Specifies whether to sort in descending order.</param>
        /// <returns>A collection of books sorted by title.</returns>
        [HttpGet("GetAllBooksSortedByTitle")]
        public async Task<IEnumerable<Book>> GetAllBooksSortedByTitle(bool isDescending) => await _bookService.GetAllBooksSortedByTitle(isDescending);

        /// <summary>
        /// Retrieves all books sorted by price.
        /// </summary>
        /// <param name="isDescending">Specifies whether to sort in descending order.</param>
        /// <returns>A collection of books sorted by price.</returns>
        [HttpGet("GetAllBooksSortedByPrice")]
        public async Task<IEnumerable<Book>> GetAllBooksSortedByPrice(bool isDescending) => await _bookService.GetAllBooksSortedByPrice(isDescending);

        /// <summary>
        /// Retrieves books by genre.
        /// </summary>
        /// <param name="genre">The genre of the books.</param>
        /// <returns>The books with the specified genre.</returns>
        [HttpGet("GetBooksByGenre")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBooksByGenre([FromBody] Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var books = await _bookService.GetBooksByGenre(genre);

            if (books.Count() > 0)
            {
                return Ok(books);
            }
            return NotFound();
        }

        /// <summary>
        /// Retrieves books by author.
        /// </summary>
        /// <param name="author">The author of the books.</param>
        /// <returns>The books by the specified author.</returns>
        [HttpGet("GetBooksByAuthor")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBooksByAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var books = await _bookService.GetBooksByAuthor(author);

            if (books.Count() > 0)
            {
                return Ok(books);
            }
            return NotFound();
        }

        /// <summary>
        /// Retrieves a book by ID.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The book with the specified ID.</returns>
        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var cacheKey = $"Book_{id}";
            if (_cache.TryGetValue(cacheKey, out Book cachedBook))
            {
                return Ok(cachedBook);
            }

            var book = await _bookService.GetBookById(id);

            if (book != null)
            {
                // Add the book to the cache
                _cache.Set(cacheKey, book, TimeSpan.FromMinutes(60));
                return Ok(book);
            }
            return NotFound();
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="book">The book to create.</param>
        /// <returns>The created book.</returns>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _bookService.AddBook(book);

            return CreatedAtAction(nameof(GetById), new { id = id }, book);
        }

        /// <summary>
        /// Updates a book.
        /// </summary>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="book">The updated book object.</param>
        /// <returns>No content.</returns>
        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bookService.UpdateBook(id, book);
            if (!result) return BadRequest();

            return NoContent();
        }

        /// <summary>
        /// Deletes a book by ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookService.DeleteBook(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
