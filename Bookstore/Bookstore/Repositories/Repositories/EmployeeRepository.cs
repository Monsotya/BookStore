using Microsoft.EntityFrameworkCore;
using Bookstore.Data;
using Bookstore.Models;
using Bookstore.Repositories.Interfaces;

namespace Bookstore.Repositories.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BookstoreDbContext _context;

        public EmployeeRepository(BookstoreDbContext context) => _context = context;

        public async Task<IEnumerable<Employee>> GetAll() => await _context.Employees.ToListAsync();

        public async Task<Employee?> GetById(int id)
        {
            Employee? employee = await _context.Employees.FindAsync(id);
            return employee;
        }

        public async Task<int> Create(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return (await _context.Employees.FindAsync(employee.Id)).Id;
        }

        public async Task<bool> Update(int id, Employee employee)
        {
            if (id != employee.Id) return false;

            var modifiedEmployee = _context.Employees.Find(id);
            if (modifiedEmployee != null)
            {
                modifiedEmployee.Name = employee.Name;
                modifiedEmployee.Surname = employee.Surname;
                modifiedEmployee.EmployeePosition = employee.EmployeePosition;
                modifiedEmployee.EmployeeStatus = employee.EmployeeStatus;
                modifiedEmployee.DateOfBirth = employee.DateOfBirth;

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            Employee? employee = await _context.Employees.FindAsync(id);

            if (employee == null) return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
