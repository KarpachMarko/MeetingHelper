using App.Public.DTO.v1.Identity;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class AppUserMapper : BaseMapper<AppUser, BLL.DTO.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}