using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class MeetingInviteMapper : BaseMapper<MeetingInvite, DAL.DTO.MeetingInvite>
{
    public MeetingInviteMapper(IMapper mapper) : base(mapper)
    {
    }
}