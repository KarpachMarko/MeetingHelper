using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class QuestionnaireService : BaseEntityService<Questionnaire, DAL.DTO.Questionnaire, IQuestionnaireRepository>,
    IQuestionnaireService
{
    public QuestionnaireService(IQuestionnaireRepository repository, IMapper<Questionnaire, DAL.DTO.Questionnaire> mapper) : base(repository, mapper)
    {
    }
}