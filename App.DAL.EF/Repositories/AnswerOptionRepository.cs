using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class AnswerOptionRepository : BaseEntityRepository<AnswerOption, Domain.AnswerOption, AppDbContext>,
    IAnswerOptionRepository
{
    public AnswerOptionRepository(AppDbContext dbContext, IMapper<AnswerOption, Domain.AnswerOption> mapper) : base(
        dbContext, mapper)
    {
    }
}