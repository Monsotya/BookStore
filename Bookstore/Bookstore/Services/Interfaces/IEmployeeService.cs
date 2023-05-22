using Bookstore.Models;

namespace Bookstore.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> AddEmployee(Employee employee);
        Task<bool> DeleteEmployee(int id);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<IEnumerable<Employee>> GetAllEmployeesByPosition(EmployeePosition position);
        Task<IEnumerable<Employee>> GetAllEmployeesSortedBySurname(bool isDescending);
        Task<Employee> GetEmployeeById(int id);
        Task<IEnumerable<Employee>> GetEmployeeesByStatus(EmployeeStatus status);
        Task<bool> UpdateEmployee(int id, Employee employee);
    }
}