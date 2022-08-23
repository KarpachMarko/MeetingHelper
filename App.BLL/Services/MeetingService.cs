using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class MeetingService : BaseEntityUserDependentService<Meeting, DAL.DTO.Meeting, IMeetingRepository>,
    IMeetingService
{
    public MeetingService(IMeetingRepository repository, IMapper<Meeting, DAL.DTO.Meeting> mapper) : base(repository, mapper)
    {
    }
}