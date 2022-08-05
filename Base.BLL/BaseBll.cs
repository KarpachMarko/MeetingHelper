using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace Base.BLL;

public abstract class BaseBll<TUow> : IBll
    where TUow : IUnitOfWork
{
    public abstract int SaveChanges();

    public abstract Task<int> SaveChangesAsync();
}