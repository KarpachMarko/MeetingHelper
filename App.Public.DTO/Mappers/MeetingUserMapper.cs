using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class MeetingUserMapper : BaseMapper<MeetingUser, BLL.DTO.MeetingUser>
{
    public MeetingUserMapper(IMapper mapper) : base(mapper)
    {
    }
}