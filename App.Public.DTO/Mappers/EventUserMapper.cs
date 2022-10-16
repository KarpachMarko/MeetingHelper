using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class EventUserMapper : BaseMapper<EventUser, BLL.DTO.EventUser>
{
    public EventUserMapper(IMapper mapper) : base(mapper)
    {
    }
}