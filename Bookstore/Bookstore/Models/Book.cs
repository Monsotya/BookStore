using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0, 1000000, ErrorMessage = "The Price field must be between 0 and 1000000.")]
        public double Price { get; set; }
        
        public DateTime PublishedDate { get; set; }

        [Required(ErrorMessage = "The PageNumber field is required.")]
        [Range(0, 9999, ErrorMessage = "The PageNumber field must be between 0 and 9999.")]
        public int PageNumber { get; set; }

        public List<Author>? Authors { get; set; }

        public List<Genre>? Genres { get; set; }
    }
}
