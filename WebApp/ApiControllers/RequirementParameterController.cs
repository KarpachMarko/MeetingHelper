using App.Contracts.BLL;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Contracts;
using Base.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class RequirementParameterController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<RequirementParameter, App.BLL.DTO.RequirementParameter> _mapper;

    public RequirementParameterController(IAppBll bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new RequirementParameterMapper(mapper);
    }

    // GET: api/RequirementParameter
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RequirementParameter>>> GetRequirementParameters()
    {
        var requirementParameters = await _bll.RequirementParameters.GetAllAsync(User.GetUserId());
        return Ok(_mapper.Map(requirementParameters));
    }

    // GET: api/RequirementParameter/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RequirementParameter>> GetRequirementParameter(Guid id)
    {
        var requirementParameter = await _bll.RequirementParameters.FirstOrDefaultAsync(id, User.GetUserId());

        if (requirementParameter == null)
        {
            return NotFound();
        }

        return _mapper.Map(requirementParameter)!;
    }

    // PUT: api/RequirementParameter/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRequirementParameter(Guid id, RequirementParameter requirementParameter)
    {
        if (id != requirementParameter.Id)
        {
            return BadRequest();
        }

        await _bll.RequirementParameters.UpdateAsync(_mapper.Map(requirementParameter)!);

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await RequirementParameterExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/RequirementParameter
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<RequirementParameter>> PostRequirementParameter(RequirementParameter requirementParameter)
    {
        requirementParameter.Id = Guid.NewGuid();
        _bll.RequirementParameters.Add(_mapper.Map(requirementParameter)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetRequirementParameter", new { id = requirementParameter.Id }, requirementParameter);
    }

    // DELETE: api/RequirementParameter/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRequirementParameter(Guid id)
    {
        await _bll.RequirementParameters.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> RequirementParameterExists(Guid id)
    {
        return await _bll.RequirementParameters.ExistsAsync(id);
    }
}