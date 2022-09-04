using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class QuestionnaireRelationRepository :
    BaseEntityRepository<QuestionnaireRelation, Domain.QuestionnaireRelation, AppDbContext>,
    IQuestionnaireRelationRepository
{
    public QuestionnaireRelationRepository(AppDbContext dbContext,
        IMapper<QuestionnaireRelation, Domain.QuestionnaireRelation> mapper) : base(dbContext, mapper)
    {
    }
}