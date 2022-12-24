using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class EventNavigationMapper : BaseMapper<EventNavigation, DAL.DTO.EventNavigation>
{
    public EventNavigationMapper(IMapper mapper) : base(mapper)
    {
    }
}