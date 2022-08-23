using App.BLL.DTO;
using App.BLL.DTO.Identity;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class PaymentService :
    BaseEntityUserService<Payment, DAL.DTO.Payment, AppUser, DAL.DTO.Identity.AppUser, IPaymentRepository>,
    IPaymentService
{
    public PaymentService(IPaymentRepository repository, IMapper<Payment, DAL.DTO.Payment> mapper) : base(repository, mapper)
    {
    }
}