using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class EventMapper : BaseMapper<Event, BLL.DTO.Event>
{
    public EventMapper(IMapper mapper) : base(mapper)
    {
    }
}