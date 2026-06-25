using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuiteWorksTestAPI.Data;
using SuiteWorksTestAPI.DTOs.Departments;
using SuiteWorksTestAPI.DTOs.Employees;
using SuiteWorksTestAPI.Models;

[Route("api/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeManagementDataContext _context;
    public EmployeesController(EmployeeManagementDataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeResponseDTO>>> GetEmployees()
    {
        var employees = await _context.Employees.Select(e => new EmployeeResponseDTO
        {
            Id = e.Id,
            Name = e.Name,
            Email = e.Email,
            PositionId = e.PositionId,
            PositionName = e.Positions.PositionName,
            DepartmentId = e.DepartmentId,
            DepartmentName = e.Department.DepartmentName,
            DateHired = e.DateHired
        }).ToListAsync();

        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeResponseDTO>> GetEmployee(int id)
    {

       var employee = await _context.Employees
          .Where(e => e.Id == id)
          .Select(e => new EmployeeResponseDTO
          {
              Id = e.Id,
              Name = e.Name,
              Email = e.Email,
              PositionId = e.PositionId,
              PositionName = e.Positions.PositionName,
              DepartmentId = e.DepartmentId,
              DepartmentName = e.Department.DepartmentName,
              DateHired = e.DateHired
          })
          .FirstOrDefaultAsync();

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int? id, UpdateEmployeeDTO dto)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (id != employee.Id)
        {
            return BadRequest();
        }

        employee.Name = dto.Name;
        employee.Email = dto.Email;
        employee.PositionId = dto.PositionId;
        employee.DepartmentId = dto.DepartmentId;
        employee.DateHired = dto.DateHired;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(CreateEmployeeDTO dto)
    {
        var departmentExists = await _context.Departments
         .AnyAsync(d => d.Id == dto.DepartmentId);

        if (!departmentExists)
        {
            return BadRequest("Invalid DepartmentId.");
        }

        var positionExists = await _context.Positions
            .AnyAsync(p => p.Id == dto.PositionId);

        if (!positionExists)
        {
            return BadRequest("Invalid PositionId.");
        }

        var employee = new Employee
        {
            Name = dto.Name,
            Email = dto.Email,
            PositionId = dto.PositionId,
            DepartmentId = dto.DepartmentId,
            DateHired = dto.DateHired
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        var response = new EmployeeResponseDTO
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            PositionId = employee.PositionId,
            DepartmentId = employee.DepartmentId,
            DateHired = employee.DateHired
        };

        return CreatedAtAction(
            nameof(GetEmployee),
            new { id = employee.Id },
            response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int? id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
