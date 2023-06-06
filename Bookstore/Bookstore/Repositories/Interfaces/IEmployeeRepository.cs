using Bookstore.Models;

namespace Bookstore.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<int> Create(Employee employee);
        Task<bool> Delete(int id);
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee?> GetById(int id);
        Task<bool> Update(int id, Employee employee);
    }
}