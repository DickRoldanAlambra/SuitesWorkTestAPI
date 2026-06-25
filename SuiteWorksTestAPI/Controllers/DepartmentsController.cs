using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuiteWorksTestAPI.Data;
using SuiteWorksTestAPI.DTOs.Departments;
using SuiteWorksTestAPI.DTOs.Positions;
using SuiteWorksTestAPI.Models;

[Route("api/departments")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly EmployeeManagementDataContext _context;
    public DepartmentsController(EmployeeManagementDataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentResponseDTO>>> GetDepartments()
    {
        var departments = await _context.Departments.Select(d => new DepartmentResponseDTO
        {
            Id = d.Id,
            DepartmentName = d.DepartmentName,
            Description = d.Description,
            IsActive = d.IsActive,
            DateCreated = d.DateCreated,
        }).ToListAsync();

        return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentResponseDTO>> GetDepartment(int id)
    {
        var department = await _context.Departments
           .Where(d => d.Id == id)
           .Select(d => new DepartmentResponseDTO
           {
               Id = d.Id,
               DepartmentName = d.DepartmentName,
               Description = d.Description,
               IsActive = d.IsActive,
               DateCreated = d.DateCreated
           })
           .FirstOrDefaultAsync();

        if (department == null)
        {
            return NotFound();
        }

        return Ok(department);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int? id, UpdateDepartmentDTO dto)
    {
        var department = await _context.Departments.FindAsync(id);

        if (id != department.Id)
        {
            return BadRequest();
        }

        department.DepartmentName = dto.DepartmentName;
        department.Description = dto.Description;
        department.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Department>> CreateDepartment(CreateDepartmentDTO dto)
    {
        var department = new Department
        {
            DepartmentName = dto.DepartmentName,
            Description = dto.Description,
            IsActive = dto.IsActive,
        };
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDepartment", new { id = department.Id }, department);
      
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int? id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
