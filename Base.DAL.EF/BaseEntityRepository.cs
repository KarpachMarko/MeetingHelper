using System.Collections;
using Base.Contracts;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Base.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class BaseEntityRepository<TDalEntity, TDomainEntity, TDbContext> : BaseEntityRepository<TDalEntity, TDomainEntity, Guid, TDbContext>
    where TDalEntity : class, IDomainEntityId<Guid>
    where TDomainEntity : class, IDomainEntityId<Guid>
    where TDbContext : DbContext
{
    public BaseEntityRepository(TDbContext dbContext, IMapper<TDalEntity, TDomainEntity> mapper) : base(dbContext, mapper)
    {
    }
}

public class BaseEntityRepository<TDalEntity, TDomainEntity, TKey, TDbContext> : IEntityRepository<TDalEntity, TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TDomainEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TDbContext : DbContext
{
    protected readonly TDbContext RepoDbContext;
    protected readonly DbSet<TDomainEntity> RepoDbSet;
    protected readonly IMapper<TDalEntity, TDomainEntity> Mapper;
    
    public BaseEntityRepository(TDbContext dbContext, IMapper<TDalEntity, TDomainEntity> mapper)
    {
        RepoDbContext = dbContext;
        RepoDbSet = dbContext.Set<TDomainEntity>();
        Mapper = mapper;
    }
    
    protected virtual IQueryable<TDomainEntity> CreateQuery(bool noTracking = true)
    {
        var query = RepoDbSet.AsQueryable();
        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public virtual TDalEntity Add(TDalEntity entity)
    {
        return Mapper.Map(RepoDbSet.Add(Mapper.Map(entity)!).Entity)!;
    }

    public virtual TDalEntity Update(TDalEntity entity)
    {
        var entityFromDb = CreateQuery().FirstOrDefault(x => x.Id.Equals(entity.Id))!;
        entityFromDb.UpdateWithTranslations(entity);

        return Mapper.Map(RepoDbSet.Update(entityFromDb).Entity)!;
    }
    
    public virtual async Task<TDalEntity> UpdateAsync(TDalEntity entity)
    {
        var entityFromDb = (await CreateQuery().FirstOrDefaultAsync(x => x.Id.Equals(entity.Id)))!;
        entityFromDb.UpdateWithTranslations(entity);

        return Mapper.Map(RepoDbSet.Update(entityFromDb).Entity)!;
    }

    public virtual TDalEntity Remove(TDalEntity entity)
    {
        SetReferencesToNull(entity);

        return Mapper.Map(RepoDbSet.Remove(Mapper.Map(entity)!).Entity)!;
    }

    public virtual TDalEntity Remove(TKey id)
    {
        var entity = FirstOrDefault(id);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TDalEntity).Name} with id {id} was not found");
        }
        return Remove(entity);
    }

    public virtual TDalEntity? FirstOrDefault(TKey id, bool noTracking = true)
    {
        return Mapper.Map(CreateQuery(noTracking).FirstOrDefault(entity => entity.Id.Equals(id)));
    }

    public virtual IEnumerable<TDalEntity> GetAll(bool noTracking = true)
    {
        return Mapper.Map(CreateQuery(noTracking).ToList());
    }

    public virtual bool Exists(TKey id)
    {
        return RepoDbSet.Any(entity => entity.Id.Equals(id));
    }

    public virtual async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
    {
        var x = await CreateQuery(noTracking).FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        var res = Mapper.Map(x);
        return res;
    }

    public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(bool noTracking = true)
    {
        return Mapper.Map(await CreateQuery(noTracking).ToListAsync());
    }

    public virtual async Task<bool> ExistsAsync(TKey id)
    {
        return await RepoDbSet.AnyAsync(entity => entity.Id.Equals(id));
    }

    public virtual async Task<TDalEntity> RemoveAsync(TKey id)
    {
        var entity = await FirstOrDefaultAsync(id);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TDalEntity).Name} with id {id} was not found");
        }
        return Remove(entity);
    }
    
    private static void SetReferencesToNull(IDomainEntityId<TKey> entity) {
        foreach (var propertyInfo in entity.GetType().GetProperties())
        {
            if (typeof(IDomainEntityId<TKey>).IsAssignableFrom(propertyInfo.PropertyType))
            {
                propertyInfo.SetValue(entity, null);
            }
        }
    }
}