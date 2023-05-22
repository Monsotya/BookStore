using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string? Name { get; set; }
    }
}
