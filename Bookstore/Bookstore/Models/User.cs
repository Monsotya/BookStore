using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The UserName field is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The EmailAddress field is required.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "The Role field is required.")]
        public string Role { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }


    }
}
