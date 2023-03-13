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
public class RequirementUsersController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<RequirementUser, App.BLL.DTO.RequirementUser> _mapper;

    public RequirementUsersController(IAppBll bll, IMapper mapper)
    {
        _bll = bll;
        _mapper = new RequirementUserMapper(mapper);
    }

    [HttpGet("requirement/{requirementId}")]
    public async Task<ActionResult<IEnumerable<RequirementUser>>> GetRequirementUsers(Guid requirementId)
    {
        var requirementUsers = await _bll.RequirementUsers.GetRequirementUsers(requirementId, User.GetUserId());
        return Ok(_mapper.Map(requirementUsers));
    }
    
    // GET: api/RequirementUsers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RequirementUser>>> GetRequirementUsers()
    {
        var requirementUsers = await _bll.RequirementUsers.GetAllAsync(User.GetUserId());
        return Ok(_mapper.Map(requirementUsers));
    }

    // GET: api/RequirementUsers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RequirementUser>> GetRequirementUser(Guid id)
    {
        var requirementUser = await _bll.RequirementUsers.FirstOrDefaultAsync(id, User.GetUserId());

        if (requirementUser == null)
        {
            return NotFound();
        }

        return _mapper.Map(requirementUser)!;
    }

    // PUT: api/RequirementUsers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRequirementUser(Guid id, RequirementUser requirementUser)
    {
        if (id != requirementUser.Id)
        {
            return BadRequest();
        }

        await _bll.RequirementUsers.UpdateAsync(_mapper.Map(requirementUser)!, User.GetUserId());

        try
        {
            await _bll.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await RequirementUserExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/RequirementUsers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<RequirementUser>> PostRequirementUser(RequirementUser requirementUser)
    {
        requirementUser.Id = Guid.NewGuid();
        requirementUser.UserId = User.GetUserId();
        _bll.RequirementUsers.Add(_mapper.Map(requirementUser)!);
        await _bll.SaveChangesAsync();

        return CreatedAtAction("GetRequirementUser", new { id = requirementUser.Id }, requirementUser);
    }

    // DELETE: api/RequirementUsers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRequirementUser(Guid id)
    {
        await _bll.RequirementUsers.RemoveAsync(id, User.GetUserId());
        await _bll.SaveChangesAsync();

        return NoContent();
    }

    private Task<bool> RequirementUserExists(Guid id)
    {
        return _bll.RequirementUsers.ExistsAsync(id);
    }
}