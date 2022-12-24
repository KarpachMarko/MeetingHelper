using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class MeetingMapper : BaseMapper<Meeting, BLL.DTO.Meeting>
{
    public MeetingMapper(IMapper mapper) : base(mapper)
    {
    }
}