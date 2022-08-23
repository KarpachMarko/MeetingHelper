using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class MeetingUserMapper : BaseMapper<MeetingUser, DAL.DTO.MeetingUser>
{
    public MeetingUserMapper(IMapper mapper) : base(mapper)
    {
    }
}