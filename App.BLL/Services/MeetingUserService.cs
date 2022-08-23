using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class MeetingUserService :
    BaseEntityUserService<MeetingUser, DAL.DTO.MeetingUser, AppUser, DAL.DTO.Identity.AppUser, IMeetingUserRepository>,
    IMeetingUserService
{
    public MeetingUserService(IMeetingUserRepository repository, IMapper<MeetingUser, DAL.DTO.MeetingUser> mapper) : base(repository, mapper)
    {
    }
}