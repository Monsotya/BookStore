using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "The UserName field is required.")]
        public string UserName { get; set;}

        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set;}
    }
}
