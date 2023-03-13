using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class RequirementService :
    BaseEntityUserDependentService<Requirement, DAL.DTO.Requirement, IRequirementRepository>,
    IRequirementService
{
    public RequirementService(IRequirementRepository repository, IMapper<Requirement, DAL.DTO.Requirement> mapper) :
        base(repository, mapper)
    {
    }

    public async Task<IEnumerable<Requirement>> GetAllInMeeting(Guid meetingId)
    {
        return Mapper.Map(await Repository.GetAllInMeeting(meetingId));
    }

    public async Task<IEnumerable<Requirement>> GetAllInEvent(Guid eventId, Guid userId)
    {
        return Mapper.Map(await Repository.GetAllInEvent(eventId, userId));
    }
}