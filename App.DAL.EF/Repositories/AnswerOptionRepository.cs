using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class AnswerOptionRepository : BaseEntityRepository<AnswerOption, Domain.AnswerOption, AppDbContext>,
    IAnswerOptionRepository
{
    public AnswerOptionRepository(AppDbContext dbContext, IMapper<AnswerOption, Domain.AnswerOption> mapper) : base(
        dbContext, mapper)
    {
    }

    protected override IQueryable<Domain.AnswerOption> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking)
            .Include(option => option.Questionnaire);
    }
}