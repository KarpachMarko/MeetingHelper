using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class AnswerOptionMapper : BaseMapper<AnswerOption, BLL.DTO.AnswerOption>
{
    public AnswerOptionMapper(IMapper mapper) : base(mapper)
    {
    }
}