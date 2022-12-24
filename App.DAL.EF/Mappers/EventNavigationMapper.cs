using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class EventNavigationMapper : BaseMapper<EventNavigation, Domain.EventNavigation>
{
    public EventNavigationMapper(IMapper mapper) : base(mapper)
    {
    }
}