using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class MeetingUserMapper : BaseMapper<MeetingUser, Domain.MeetingUser>
{
    public MeetingUserMapper(IMapper mapper) : base(mapper)
    {
    }
}