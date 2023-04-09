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
public class RequirementOptionsController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<RequirementOption, App.BLL.DTO.RequirementOption> _mapper;

    public RequirementOptionsController(IAppBll bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new RequirementOptionMapper(mapper);
    }
    
    [HttpGet("requirement/{requirementId}")]
    public async Task<ActionResult<IEnumerable<RequirementOption>>> GetRequirementsInEvent(Guid requirementId)
    {
        var options = await _bll.RequirementOptions.GetRequirementOptions(requirementId);
        return Ok(_mapper.Map(options));
    }

    // GET: api/RequirementOptions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RequirementOption>>> GetRequirementOptions()
    {
        var requirementOptions = await _bll.RequirementOptions.GetAllAsync();
        return Ok(_mapper.Map(requirementOptions));
    }

    // GET: api/RequirementOptions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RequirementOption>> GetRequirementOption(Guid id)
    {
        var requirementOption = await _bll.RequirementOptions.FirstOrDefaultAsync(id);

        if (requirementOption == null)
        {
            return NotFound();
        }

        return _mapper.Map(requirementOption)!;
    }

    // PUT: api/RequirementOptions/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRequirementOption(Guid id, RequirementOption requirementOption)
    {
        if (id != requirementOption.Id)
        {
            return BadRequest();
        }

        await _bll.RequirementOptions.UpdateAsync(_mapper.Map(requirementOption)!);

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await RequirementOptionExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/RequirementOptions
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<RequirementOption>> PostRequirementOption(RequirementOption requirementOption)
    {
        requirementOption.Id = Guid.NewGuid();
        _bll.RequirementOptions.Add(_mapper.Map(requirementOption)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetRequirementOption", new { id = requirementOption.Id }, requirementOption);
    }

    // DELETE: api/RequirementOptions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRequirementOption(Guid id)
    {
        await _bll.RequirementOptions.RemoveAsync(id);
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private Task<bool> RequirementOptionExists(Guid id)
    {
        return _bll.RequirementOptions.ExistsAsync(id);
    }
}