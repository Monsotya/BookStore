using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using Bookstore.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMemoryCache _cache;

        public EmployeeController(IEmployeeService employeeService, IMemoryCache cache)
        {
            _employeeService = employeeService;
            _cache = cache;
        }

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A collection of employees.</returns>
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Employee>> GetAll() => await _employeeService.GetAllEmployees();

        /// <summary>
        /// Retrieves all employees sorted by surname.
        /// </summary>
        /// <param name="isDescending">Specifies whether to sort in descending order.</param>
        /// <returns>A collection of employees sorted by surname.</returns>
        [HttpGet("GetAllEmployeesSortedBySurname")]
        public async Task<IEnumerable<Employee>> GetAllEmployeesSortedBySurname(bool isDescending) => await _employeeService.GetAllEmployeesSortedBySurname(isDescending);

        /// <summary>
        /// Retrieves employees by status.
        /// </summary>
        /// <param name="status">The employee status.</param>
        /// <returns>The employees with the specified status.</returns>
        [HttpGet("GetEmployeeesByStatus")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployeeesByStatus(EmployeeStatus status)
        {
            var employees = await _employeeService.GetEmployeeesByStatus(status);

            if (employees.Count() > 0)
            {
                return Ok(employees);
            }
            return NotFound();
        }

        /// <summary>
        /// Retrieves employees by position.
        /// </summary>
        /// <param name="position">The employee position.</param>
        /// <returns>The employees with the specified position.</returns>
        [HttpGet("GetAllEmployeesByPosition")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllEmployeesByPosition(EmployeePosition position)
        {
            var employees = await _employeeService.GetAllEmployeesByPosition(position);

            if (employees.Count() > 0)
            {
                return Ok(employees);
            }
            return NotFound();
        }

        /// <summary>
        /// Retrieves an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee.</param>
        /// <returns>The employee with the specified ID.</returns>
        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            Employee employee = await _employeeService.GetEmployeeById(id);
            return employee == null ? NotFound() : Ok(employee);
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="employee">The employee object to create.</param>
        /// <returns>The created employee.</returns>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await _employeeService.AddEmployee(employee);
            return CreatedAtAction(nameof(GetById), new { id = id }, employee);
        }

        /// <summary>
        /// Updates an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to update.</param>
        /// <param name="employee">The updated employee object.</param>
        /// <returns>No content.</returns>
        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Task.Run(() => _employeeService.UpdateEmployee(id, employee).Result);
            if (!result) return BadRequest();

            return NoContent();
        }

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
