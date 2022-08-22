using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class PaymentRepository :
    BaseEntityUserRepository<Payment, Domain.Payment, AppUser, Domain.Identity.AppUser, AppDbContext>,
    IPaymentRepository
{
    public PaymentRepository(AppDbContext dbContext, IMapper<Payment, Domain.Payment> mapper) : base(dbContext, mapper)
    {
    }
}