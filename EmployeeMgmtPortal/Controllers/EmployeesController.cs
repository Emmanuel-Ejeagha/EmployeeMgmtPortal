using EmployeeMgmtPortal.Data;
using EmployeeMgmtPortal.DTOs;
using EmployeeMgmtPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EmployeeMgmtPortal.Controllers
{
    /// <summary>
    /// API endpoints for managing employee records
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public EmployeesController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>Retrieves all employees</summary>
        /// <returns>List of all employees in the system</returns>
        /// <response code="200">Returns the list of employees</response>
        /// <response code="500"> if there was an internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Get all employees",
            Description = "Returns a complete list of all employees in the system.",
            OperationId = "GetAllEmployees",
            Tags = new[] { "Employee Management" }
        )]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList();

            return Ok(allEmployees);
        }

        /// <summary>
        ///   Retrieves a specific employee by their unique identifier
        /// </summary>
        /// <param name="id">Employee ID (GUID format)</param>
        /// <returns>The requested employee details</returns>
        /// <response code="200">Returns the employee record</response>
        /// <response code="404">If the employee with the specific ID dosen't exist</response>
        /// <response code="404">If the provided ID format is invalid</response>
        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Get employee by ID",
            Description = "Returns a single employee record using their unique identifier",
            OperationId = "GetEmployeeById",
            Tags = new[] { "Employee Management" }
        )]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound($"Employee with ID '{id}' not found.");
            }
            return Ok(employee);
        }

 /// <summary>
        /// Creates a new employee record
        /// </summary>
        /// <param name="addEmployeeDto">Employee data transfer object</param>
        /// <returns>The newly created employee record</returns>
        /// <response code="200">Employee created successfully</response>
        /// <response code="400">If the input data is invalid</response>
        /// <response code="409">If an employee with the same email already exists</response>
        [HttpPost]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [SwaggerOperation(
            Summary = "Create a new employee",
            Description = "Adds a new employee record to the system. Email must be unique.",
            OperationId = "CreateEmployee",
            Tags = new[] { "Employee Management" }
        )]        
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                PhoneNumber = addEmployeeDto.PhoneNumber,
                Salary = addEmployeeDto.Salary
            };

            if (dbContext.Employees.Any(e => e.Email == addEmployeeDto.Email))
            {
                return Conflict($"Employee with email '{addEmployeeDto.Email} already exists");
            }

            dbContext.Employees.Add(employeeEntity);
            dbContext.SaveChanges();

            return CreatedAtAction(
                nameof(GetEmployeeById),
                new { id = employeeEntity.Id },
                employeeEntity);
        }

         /// <summary>
        /// Updates an existing employee record
        /// </summary>
        /// <param name="id">Employee ID to update</param>
        /// <param name="updateEmployeeDto">Updated employee data</param>
        /// <returns>The updated employee record</returns>
        /// <response code="200">Employee updated successfully</response>
        /// <response code="400">If the input data is invalid</response>
        /// <response code="404">If the employee doesn't exist</response>
        /// <response code="409">If trying to change to an email that already exists</response>
        [HttpPut]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [SwaggerOperation(
            Summary = "Update an existing employee",
            Description = "Updates the details of an existing employee. All fields except ID can be updated.",
            OperationId = "UpdateEmployee",
            Tags = new[] { "Employee Management" }
        )]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee is null)
            {
               return NotFound($"Employee with ID '{id}' not found.");
            }

            if (employee.Email != updateEmployeeDto.Email && 
                dbContext.Employees.Any(e => e.Email == updateEmployeeDto.Email))
            {
                 return Conflict($"Another employee with email '{updateEmployeeDto.Email}' already exists.");
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.PhoneNumber = updateEmployeeDto.PhoneNumber;
            employee.Salary = updateEmployeeDto.Salary;

            dbContext.SaveChanges();

            return Ok(employee);
        }

        /// <summary>
        /// Deletes an employee record
        /// </summary>
        /// <param name="id">Employee ID to delete</param>
        /// <returns>No content on successful deletion</returns>
        /// <response code="204">Employee deleted successfully</response>
        /// <response code="404">If the employee doesn't exist</response>
        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Delete an employee",
            Description = "Permanently removes an employee record from the system.",
            OperationId = "DeleteEmployee",
            Tags = new[] { "Employee Management" }
        )]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound($"Employee with ID '{id}' not found.");
            }

            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}

