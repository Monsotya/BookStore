using Bookstore.Models;
using Bookstore.Repositories.Interfaces;
using Bookstore.Services.Interfaces;

namespace Bookstore.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeRepository.GetById(id);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _employeeRepository.GetAll();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesSortedBySurname(bool isDescending)
        {
            if (isDescending)
            {
                return await Task.Run(() => _employeeRepository.GetAll().Result.OrderByDescending(x => x.Surname));
            }
            return await Task.Run(() => _employeeRepository.GetAll().Result.OrderBy(x => x.Surname));
        }

        public async Task<IEnumerable<Employee>> GetEmployeeesByStatus(EmployeeStatus status)
        {
            return await Task.Run(() => _employeeRepository.GetAll().Result.Where(x => x.EmployeeStatus == status));
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesByPosition(EmployeePosition position)
        {
            return await Task.Run(() => _employeeRepository.GetAll().Result.Where(x => x.EmployeePosition == position));
        }

        public async Task<int> AddEmployee(Employee employee)
        {
            return await _employeeRepository.Create(employee);
        }

        public async Task<bool> UpdateEmployee(int id, Employee employee)
        {
            return await _employeeRepository.Update(id, employee);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            return await _employeeRepository.Delete(id);
        }
    }
}
