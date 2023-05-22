using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The Surname field is required.")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "The EmployeePosition field is required.")]
        public EmployeePosition EmployeePosition { get; set; }

        [Required(ErrorMessage = "The EmployeeStatus field is required.")]
        public EmployeeStatus EmployeeStatus { get; set; }

        [Required(ErrorMessage = "The DateOfBirth field is required.")]
        public DateTime DateOfBirth { get; set; }
    }

    public enum EmployeeStatus
    {
        Active,
        Inactive,
        Retired
    }

    public enum EmployeePosition
    {
        Seller,
        Janitor,
        Manager
    }
}
