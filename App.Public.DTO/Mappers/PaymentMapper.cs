using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class PaymentMapper : BaseMapper<Payment, BLL.DTO.Payment>
{
    public PaymentMapper(IMapper mapper) : base(mapper)
    {
    }
}