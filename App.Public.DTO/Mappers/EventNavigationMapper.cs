using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class EventNavigationMapper : BaseMapper<EventNavigation, BLL.DTO.EventNavigation>
{
    public EventNavigationMapper(IMapper mapper) : base(mapper)
    {
    }
}