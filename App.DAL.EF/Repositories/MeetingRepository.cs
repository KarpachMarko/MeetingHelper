using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class MeetingRepository : BaseEntityUserDependentRepository<Meeting, Domain.Meeting, AppDbContext>,
    IMeetingRepository
{
    public MeetingRepository(AppDbContext dbContext,
        IMapper<Meeting, Domain.Meeting> mapper) : base(dbContext, CheckOwnership, mapper)
    {
    }

    public static bool CheckOwnership(Meeting meeting, Guid userId)
    {
        // TODO
        return true;
    }
}