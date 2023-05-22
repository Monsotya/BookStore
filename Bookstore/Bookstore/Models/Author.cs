using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The Surname field is required.")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "The DateOfBirth field is required.")]
        public DateTime DateOfBirth { get; set; }

        public DateTime? DateOfDeath { get; set; }

        public List<Book>? Books { get; set;}
    }
}
