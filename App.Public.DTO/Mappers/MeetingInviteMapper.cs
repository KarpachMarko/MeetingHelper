using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class MeetingInviteMapper : BaseMapper<MeetingInvite, BLL.DTO.MeetingInvite>
{
    public MeetingInviteMapper(IMapper mapper) : base(mapper)
    {
    }
}