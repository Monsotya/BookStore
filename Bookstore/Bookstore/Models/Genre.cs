using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    [Index(nameof(Id), IsUnique = true)]
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string? Name { get; set; }
    }
}
