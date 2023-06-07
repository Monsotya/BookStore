using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using Bookstore.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMemoryCache _cache;

        public AuthorController(IAuthorService authorService, IMemoryCache cache)
        {
            _authorService = authorService;
            _cache = cache;
        }

        /// <summary>
        /// Retrieves all authors.
        /// </summary>
        /// <returns>A collection of authors.</returns>
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Author>> GetAll() => await _authorService.GetAllAuthors();

        /// <summary>
        /// Retrieves all authors sorted by surname.
        /// </summary>
        /// <param name="isDescending">Specifies whether to sort in descending order.</param>
        /// <returns>A collection of authors sorted by surname.</returns>
        [HttpGet("GetAllAuthorsSortedBySurname")]
        public async Task<IEnumerable<Author>> GetAllAuthorsSortedBySurname(bool isDescending) => await _authorService.GetAllAuthorsSortedBySurname(isDescending);

        /// <summary>
        /// Retrieves all authors sorted by date of birth.
        /// </summary>
        /// <param name="isDescending">Specifies whether to sort in descending order.</param>
        /// <returns>A collection of authors sorted by date of birth.</returns>
        [HttpGet("GetAllAuthorsSortedByDateOfBirth")]
        public async Task<IEnumerable<Author>> GetAllAuthorsSortedByDateOfBirth(bool isDescending) => await _authorService.GetAllAuthorsSortedByDateOfBirth(isDescending);

        /// <summary>
        /// Retrieves an author by ID.
        /// </summary>
        /// <param name="id">The ID of the author.</param>
        /// <returns>The author with the specified ID.</returns>
        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(Author), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            Author? author = await _authorService.GetAuthorById(id);
            return author == null ? NotFound() : Ok(author);
        }

        /// <summary>
        /// Creates a new author.
        /// </summary>
        /// <param name="author">The author to create.</param>
        /// <returns>The created author.</returns>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await _authorService.AddAuthor(author);
            return CreatedAtAction(nameof(GetById), new { id = id }, author);
        }

        /// <summary>
        /// Updates an author.
        /// </summary>
        /// <param name="id">The ID of the author to update.</param>
        /// <param name="author">The updated author object.</param>
        /// <returns>No content.</returns>
        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(int id, [FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authorService.UpdateAuthor(id, author);
            if (!result) return BadRequest();

            return NoContent();
        }

        /// <summary>
        /// Deletes an author by ID.
        /// </summary>
        /// <param name="id">The ID of the author to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _authorService.DeleteAuthor(id);
            if (!result) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Retrieves all authors with count of their books.
        /// </summary>
        /// <returns>A collection of authors count of their books.</returns>
        [HttpGet("GetAuthorsWithBooksCount")]
        public async Task<IEnumerable<object>> GetAuthorsWithBooksCount() => await _authorService.GetAuthorsWithBooksCount();
    }
}
