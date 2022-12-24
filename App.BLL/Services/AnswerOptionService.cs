using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class AnswerOptionService : BaseEntityService<AnswerOption, DAL.DTO.AnswerOption, IAnswerOptionRepository>, IAnswerOptionService
{
    public AnswerOptionService(IAnswerOptionRepository repository, IMapper<AnswerOption, DAL.DTO.AnswerOption> mapper) : base(repository, mapper)
    {
    }
}