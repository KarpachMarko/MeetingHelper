using Base.Contracts;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Base.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseEntityUserRepository<TDalEntity, TDomainEntity, TDalUser, TDomainUser, TDbContext> : BaseEntityUserRepository<TDalEntity, TDomainEntity, Guid, TDalUser, TDomainUser, TDbContext>
    where TDalEntity : class, IDomainEntityId<Guid>, IDomainEntityUser<TDalUser>
    where TDomainEntity : class, IDomainEntityId<Guid>, IDomainEntityUser<TDomainUser>
    where TDalUser : IdentityUser<Guid>
    where TDomainUser : IdentityUser<Guid>
    where TDbContext : DbContext
{
    public BaseEntityUserRepository(TDbContext dbContext, IMapper<TDalEntity, TDomainEntity> mapper) : base(dbContext, mapper)
    {
    }
}

public class BaseEntityUserRepository<TDalEntity, TDomainEntity, TKey, TDalUser, TDomainUser, TDbContext> : IEntityUserRepository<TDalEntity, TKey, TDalUser>
    where TDalEntity : class, IDomainEntityId<TKey>, IDomainEntityUser<TKey, TDalUser>
    where TDomainEntity : class, IDomainEntityId<TKey>, IDomainEntityUser<TKey, TDomainUser>
    where TKey : IEquatable<TKey>
    where TDalUser : IdentityUser<TKey>
    where TDomainUser : IdentityUser<TKey>
    where TDbContext : DbContext
{
    protected readonly TDbContext RepoDbContext;
    protected readonly DbSet<TDomainEntity> RepoDbSet;
    protected readonly IMapper<TDalEntity, TDomainEntity> Mapper;

    public BaseEntityUserRepository(TDbContext dbContext, IMapper<TDalEntity, TDomainEntity> mapper)
    {
        RepoDbContext = dbContext;
        RepoDbSet = dbContext.Set<TDomainEntity>();
        Mapper = mapper;
    }

    protected virtual IQueryable<TDomainEntity> CreateQuery(TKey userId, bool noTracking = true)
    {
        var query = RepoDbSet
            .AsQueryable()
            .Include(e => e.User)
            .Where(e => e.UserId.Equals(userId));
        if (noTracking)
        {
            query = query
                .AsNoTracking();
        }
        else
        {
            query = query
                .Where(e => e.UserId.Equals(userId));
        }

        return query;
    }

    public virtual TDalEntity Add(TDalEntity entity)
    {
        return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!;
    }
    
    public virtual TDalEntity Update(TDalEntity entity, TKey userId)
    {
        var entityFromDb = CreateQuery(userId).FirstOrDefault(x => x.Id.Equals(entity.Id))!;
        entityFromDb.UpdateWithTranslations(entity);

        return Mapper.Map(RepoDbSet.Update(entityFromDb).Entity)!;
    }
    
    public virtual async Task<TDalEntity> UpdateAsync(TDalEntity entity, TKey userId)
    {
        var entityFromDb = (await CreateQuery(userId).FirstOrDefaultAsync(x => x.Id.Equals(entity.Id)))!;
        entityFromDb.UpdateWithTranslations(entity);

        return Mapper.Map(RepoDbSet.Update(entityFromDb).Entity)!;
    }

    public virtual TDalEntity Remove(TDalEntity entity)
    {
        SetReferencesToNull(entity);
        
        return Mapper.Map(RepoDbSet.Remove(Mapper.Map(entity)!).Entity)!;
    }

    public virtual TDalEntity Remove(TKey id, TKey userId)
    {
        var entity = FirstOrDefault(id, userId);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TDalEntity).Name} with id {id} was not found");
        }

        return Remove(entity);
    }

    public virtual TDalEntity? FirstOrDefault(TKey id, TKey userId, bool noTracking = true)
    {
        return Mapper.Map(CreateQuery(userId, noTracking).FirstOrDefault(entity => entity.Id.Equals(id)));
    }

    public virtual IEnumerable<TDalEntity> GetAll(TKey userId, bool noTracking = true)
    {
        return Mapper.Map(CreateQuery(userId, noTracking).ToList());
    }

    public virtual bool Exists(TKey id)
    {
        return RepoDbSet.Any(entity => entity.Id.Equals(id));
    }

    public virtual async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, TKey userId, bool noTracking = true)
    {
        return Mapper.Map(await CreateQuery(userId, noTracking).FirstOrDefaultAsync(entity => entity.Id.Equals(id)));
    }

    public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(TKey userId, bool noTracking = true)
    {
        return Mapper.Map(await CreateQuery(userId, noTracking).ToListAsync());
    }

    public virtual async Task<bool> ExistsAsync(TKey id)
    {
        return await RepoDbSet.AnyAsync(entity => entity.Id.Equals(id));
    }

    public virtual async Task<TDalEntity> RemoveAsync(TKey userId, TKey id)
    {
        var entity = await FirstOrDefaultAsync(id, userId);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TDalEntity).Name} with id {id} was not found");
        }

        return Remove(entity);
    }
    
    private static void SetReferencesToNull(TDalEntity entity) {
        foreach (var propertyInfo in entity.GetType().GetProperties())
        {
            if (typeof(IDomainEntityId<TKey>).IsAssignableFrom(propertyInfo.PropertyType))
            {
                propertyInfo.SetValue(entity, null);
            }
        }
    }
}