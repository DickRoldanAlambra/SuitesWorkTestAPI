using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuiteWorksTestAPI.Data;
using SuiteWorksTestAPI.DTOs;
using SuiteWorksTestAPI.DTOs.Positions;
using SuiteWorksTestAPI.Models;

[Route("api/positions")]
[ApiController]
public class PositionsController : ControllerBase
{
    private readonly EmployeeManagementDataContext _context;
    public PositionsController(EmployeeManagementDataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PositionResponseDTO>>> GetPositions()
    {
        var positions = await _context.Positions.Select(p => new PositionResponseDTO
        {
            Id = p.Id,
            PositionName = p.PositionName,
            Description = p.Description,
            IsActive = p.IsActive,
            DateCreated = p.DateCreated,
        }).ToListAsync();

        return Ok(positions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PositionResponseDTO>> GetPosition(int id)
    {
        var position = await _context.Positions
        .Where(p => p.Id == id)
        .Select(p => new PositionResponseDTO
        {
            Id = p.Id,
            PositionName = p.PositionName,
            Description = p.Description,
            IsActive = p.IsActive,
            DateCreated = p.DateCreated
        })
        .FirstOrDefaultAsync();

        if (position == null)
        {
            return NotFound();
        }

        return Ok(position);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePosition(int? id, UpdatePositionDTO dto)
    {
        var position = await _context.Positions.FindAsync(id);

        if (id != position.Id)
        {
            return BadRequest();
        }
        
        position.PositionName = dto.PositionName;
        position.Description = dto.Description;
        position.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<CreatePositionDTO>> CreatePosition(CreatePositionDTO dto)
    {
        var position = new Position
        { 
            PositionName = dto.PositionName,
            Description = dto.Description,
            IsActive = dto.IsActive,
        };
        _context.Positions.Add(position);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPosition", new { id = position.Id }, position);
    }

    // DELETE: api/Positions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePosition(int? id)
    {
        var positions = await _context.Positions.FindAsync(id);
        if (positions == null)
        {
            return NotFound();
        }

        _context.Positions.Remove(positions);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
