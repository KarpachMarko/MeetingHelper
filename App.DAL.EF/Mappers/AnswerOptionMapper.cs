using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class AnswerOptionMapper : BaseMapper<DAL.DTO.AnswerOption, Domain.AnswerOption>
{
    public AnswerOptionMapper(IMapper mapper) : base(mapper)
    {
    }
}