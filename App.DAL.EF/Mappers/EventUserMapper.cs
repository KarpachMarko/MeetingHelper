using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class EventUserMapper : BaseMapper<EventUser, Domain.EventUser>
{
    public EventUserMapper(IMapper mapper) : base(mapper)
    {
    }
}