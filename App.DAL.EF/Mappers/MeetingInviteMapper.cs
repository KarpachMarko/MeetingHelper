using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class MeetingInviteMapper : BaseMapper<MeetingInvite, Domain.MeetingInvite>
{
    public MeetingInviteMapper(IMapper mapper) : base(mapper)
    {
    }
}