using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class QuestionnaireRepository : BaseEntityRepository<Questionnaire, Domain.Questionnaire, AppDbContext>,
    IQuestionnaireRepository
{
    public QuestionnaireRepository(AppDbContext dbContext, IMapper<Questionnaire, Domain.Questionnaire> mapper) : base(
        dbContext, mapper)
    {
    }
}