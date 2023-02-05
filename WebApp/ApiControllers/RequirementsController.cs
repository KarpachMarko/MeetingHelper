using App.Contracts.BLL;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Contracts;
using Base.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "TelegramAuth")]
public class RequirementsController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<Requirement, App.BLL.DTO.Requirement> _mapper;

    public RequirementsController(IAppBll bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new RequirementMapper(mapper);
    }

    [HttpGet("event/{eventId}")]
    public async Task<ActionResult<IEnumerable<Requirement>>> GetRequirementsInEvent(Guid eventId)
    {
        var requirements = await _bll.Requirements.GetAllInEvent(eventId, User.GetUserId());
        return Ok(_mapper.Map(requirements));
    }
    
    // GET: api/Requirements
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Requirement>>> GetRequirements()
    {
        var requirements = await _bll.Requirements.GetAllAsync(User.GetUserId());
        return Ok(_mapper.Map(requirements));
    }

    // GET: api/Requirements/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Requirement>> GetRequirement(Guid id)
    {
        var requirement = await _bll.Requirements.FirstOrDefaultAsync(id, User.GetUserId());

        if (requirement == null)
        {
            return NotFound();
        }

        return _mapper.Map(requirement)!;
    }

    // PUT: api/Requirements/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRequirement(Guid id, Requirement requirement)
    {
        if (id != requirement.Id)
        {
            return BadRequest();
        }

        await _bll.Requirements.UpdateAsync(_mapper.Map(requirement)!);

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await RequirementExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/Requirements
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Requirement>> PostRequirement(Requirement requirement)
    {
        requirement.Id = Guid.NewGuid();
        _bll.Requirements.Add(_mapper.Map(requirement)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetRequirement", new { id = requirement.Id }, requirement);
    }

    // DELETE: api/Requirements/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRequirement(Guid id)
    {
        await _bll.Requirements.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private Task<bool> RequirementExists(Guid id)
    {
        return _bll.Requirements.ExistsAsync(id);
    }
}