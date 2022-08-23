using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class PaymentMapper : BaseMapper<Payment, DAL.DTO.Payment>
{
    public PaymentMapper(IMapper mapper) : base(mapper)
    {
    }
}