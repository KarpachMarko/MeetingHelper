using App.DAL.DTO.Identity;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class AppUserMapper : BaseMapper<AppUser, Domain.Identity.AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}