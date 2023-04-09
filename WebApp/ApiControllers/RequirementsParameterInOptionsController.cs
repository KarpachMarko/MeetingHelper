using App.Contracts.BLL;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using AutoMapper;
using Base.Contracts;
using Base.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "TelegramAuth")]
    public class RequirementsParameterInOptionsController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly IMapper<RequirementParameterInOption, App.BLL.DTO.RequirementParameterInOption> _mapper;

        public RequirementsParameterInOptionsController(IAppBll bll, IMapper mapper)
        {
            _bll = bll;
            _mapper = new RequirementParameterInOptionMapper(mapper);
        }

        [HttpGet("options/{optionId}")]
        public async Task<ActionResult<IEnumerable<Guid>>> GetRequirementParameters(Guid optionId)
        {
            var parameters = await _bll.RequirementsParameterInOptions.GetOptionParametersId(optionId);
            return Ok(parameters);
        }
        
        [HttpPut("options/{optionId}")]
        public async Task<ActionResult<IEnumerable<RequirementParameter>>> SetRequirementParameters(Guid optionId, IEnumerable<RequirementParameterInOption> parameters)
        {
            await _bll.RequirementsParameterInOptions.SetParameters(optionId, _mapper.Map(parameters), User.GetUserId());
        
            await _bll.SaveChangesAsync();
        
            return NoContent();
        }
        
        // GET: api/RequirementParameterInOption
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequirementParameterInOption>>> GetRequirementsParameterInOptions()
        {
            var requirementParameterInOptions = await _bll.RequirementsParameterInOptions.GetAllAsync();
            return Ok(_mapper.Map(requirementParameterInOptions));
        }

        // GET: api/RequirementParameterInOption/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequirementParameterInOption>> GetRequirementParameterInOption(Guid id)
        {
            var requirementParameterInOption = await _bll.RequirementsParameterInOptions.FirstOrDefaultAsync(id);

            if (requirementParameterInOption == null)
            {
                return NotFound();
            }

            return _mapper.Map(requirementParameterInOption)!;
        }

        // PUT: api/RequirementParameterInOption/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequirementParameterInOption(Guid id, RequirementParameterInOption requirementParameterInOption)
        {
            if (id != requirementParameterInOption.Id)
            {
                return BadRequest();
            }

            await _bll.RequirementsParameterInOptions.UpdateAsync(_mapper.Map(requirementParameterInOption)!);

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RequirementParameterInOptionExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/RequirementParameterInOption
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RequirementParameterInOption>> PostRequirementParameterInOption(RequirementParameterInOption requirementParameterInOption)
        {
            requirementParameterInOption.Id = Guid.NewGuid();
            _bll.RequirementsParameterInOptions.Add(_mapper.Map(requirementParameterInOption)!);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetRequirementParameterInOption", new { id = requirementParameterInOption.Id }, requirementParameterInOption);
        }

        // DELETE: api/RequirementParameterInOption/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequirementParameterInOption(Guid id)
        {
            await _bll.RequirementsParameterInOptions.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> RequirementParameterInOptionExists(Guid id)
        {
            return await _bll.RequirementsParameterInOptions.ExistsAsync(id);
        }
    }
}
