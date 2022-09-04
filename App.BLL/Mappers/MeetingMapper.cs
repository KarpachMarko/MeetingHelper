using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class MeetingMapper : BaseMapper<Meeting, DAL.DTO.Meeting>
{
    public MeetingMapper(IMapper mapper) : base(mapper)
    {
    }
}