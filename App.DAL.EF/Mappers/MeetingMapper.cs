using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class MeetingMapper : BaseMapper<Meeting, Domain.Meeting>
{
    public MeetingMapper(IMapper mapper) : base(mapper)
    {
    }
}