using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class QuestionnaireRelationRepository :
    BaseEntityRepository<QuestionnaireRelation, Domain.QuestionnaireRelation, AppDbContext>,
    IQuestionnaireRelationRepository
{
    public QuestionnaireRelationRepository(AppDbContext dbContext,
        IMapper<QuestionnaireRelation, Domain.QuestionnaireRelation> mapper) : base(dbContext, mapper)
    {
    }

    protected override IQueryable<Domain.QuestionnaireRelation> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking)
            .Include(relation => relation.Event)
            .Include(relation => relation.Meeting)
            .Include(relation => relation.Questionnaire)
            .Include(relation => relation.Requirement);
    }
}