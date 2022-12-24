using App.DAL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class AnswerOptionMapper : BaseMapper<AnswerOption, Domain.AnswerOption>
{
    public AnswerOptionMapper(IMapper mapper) : base(mapper)
    {
    }
}