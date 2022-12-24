using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class AnswerOptionMapper : BaseMapper<AnswerOption, DAL.DTO.AnswerOption>
{
    public AnswerOptionMapper(IMapper mapper) : base(mapper)
    {
    }
}