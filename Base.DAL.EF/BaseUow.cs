using Base.Contracts.DAL;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseUow<TDbContext> : IUnitOfWork 
    where TDbContext : DbContext
{
    protected readonly TDbContext UowDbContext;
    
    public BaseUow(TDbContext uowDbContext)
    {
        UowDbContext = uowDbContext;
    }

    public int SaveChanges()
    {
        return UowDbContext.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await UowDbContext.SaveChangesAsync();
    }
}