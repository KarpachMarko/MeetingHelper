using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class EventUserMapper : BaseMapper<EventUser, DAL.DTO.EventUser>
{
    public EventUserMapper(IMapper mapper) : base(mapper)
    {
    }
}