using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class EventUserService :
    BaseEntityUserService<EventUser, DAL.DTO.EventUser, AppUser, DAL.DTO.Identity.AppUser, IEventUserRepository>,
    IEventUserService
{
    public EventUserService(IEventUserRepository repository, IMapper<EventUser, DAL.DTO.EventUser> mapper) : base(repository, mapper)
    {
    }
}