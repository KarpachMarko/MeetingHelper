using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class EventMapper : BaseMapper<Event, DAL.DTO.Event>
{
    public EventMapper(IMapper mapper) : base(mapper)
    {
    }
}