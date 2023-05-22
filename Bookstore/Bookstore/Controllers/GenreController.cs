using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using Bookstore.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMemoryCache _cache;

        public GenreController(IGenreService genreService, IMemoryCache cache)
        {
            _genreService = genreService;
            _cache = cache;
        }

        /// <summary>
        /// Retrieves all genres.
        /// </summary>
        /// <returns>A collection of genres.</returns>
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Genre>> GetAll() => await _genreService.GetAllGenres();

        /// <summary>
        /// Retrieves a genre by ID.
        /// </summary>
        /// <param name="id">The ID of the genre.</param>
        /// <returns>The genre with the specified ID.</returns>
        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(Genre), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            Genre genre = await _genreService.GetGenreById(id);
            return genre == null ? NotFound() : Ok(genre);
        }

        /// <summary>
        /// Creates a new genre.
        /// </summary>
        /// <param name="genre">The genre object to create.</param>
        /// <returns>The created genre.</returns>
        [HttpPost("Create")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await Task.Run(() => _genreService.AddGenre(genre));

            return CreatedAtAction(nameof(GetById), new { id = id }, genre);
        }

        /// <summary>
        /// Updates a genre by ID.
        /// </summary>
        /// <param name="id">The ID of the genre to update.</param>
        /// <param name="genre">The updated genre object.</param>
        /// <returns>No content.</returns>
        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(int id, [FromBody] Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Task.Run(() => _genreService.UpdateGenre(id, genre).Result);
            if (!result) return BadRequest();

            return NoContent();
        }

        /// <summary>
        /// Deletes a genre by ID.
        /// </summary>
        /// <param name="id">The ID of the genre to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Task.Run(() => _genreService.DeleteGenre(id).Result);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
