using App.Contracts.BLL;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1.Identity;
using AutoMapper;
using Base.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly IMapper<AppUser, App.BLL.DTO.Identity.AppUser> _mapper;

    public UsersController(IAppBll bll,
        IMapper mapper)
    {
        _bll = bll;
        _mapper = new AppUserMapper(mapper);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUser(Guid id)
    {
        var user = await _bll.Users.FirstOrDefaultAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return _mapper.Map(user)!;
    }
}