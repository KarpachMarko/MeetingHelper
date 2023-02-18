using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.DAL.DTO.Identity;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PaymentRepository :
    BaseEntityUserRepository<Payment, Domain.Payment, AppUser, Domain.Identity.AppUser, AppDbContext>,
    IPaymentRepository
{
    public PaymentRepository(AppDbContext dbContext, IMapper<Payment, Domain.Payment> mapper) : base(dbContext, mapper)
    {
    }

    protected override IQueryable<Domain.Payment> CreateQuery(Guid userId, bool noTracking = true)
    {
        return base.CreateQuery(userId, noTracking)
        .Include(payment => payment.Requirement);
    }

    protected override IQueryable<Domain.Payment> CreateQueryUnsafe(bool noTracking = true)
    {
        return base.CreateQueryUnsafe(noTracking)
        .Include(payment => payment.Requirement);
    }

    public async Task<IEnumerable<Payment>> GetRequirementPayments(Guid requirementId)
    {
        var payments = await CreateQueryUnsafe()
            .Where(payment => payment.Requirement != null && payment.Requirement.Id.Equals(requirementId))
            .ToListAsync();

        return Mapper.Map(payments);
    }

    public async Task<double> GetEventTotalPayments(Guid eventId)
    {
        var payments = await CreateQueryUnsafe()
            .Where(payment => payment.Requirement!.EventId.Equals(eventId))
            .ToListAsync();
        
        return payments.Sum(payment => payment.Amount);
    }
}