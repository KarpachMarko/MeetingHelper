using Base.Contracts;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Base.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL.EF;

public class
    BaseEntityUserDependentRepository<TDalEntity, TDomainEntity, TDbContext> : BaseEntityUserDependentRepository<TDalEntity, TDomainEntity, Guid, TDbContext>
    where TDalEntity : class, IDomainEntityId<Guid>
    where TDomainEntity : class, IDomainEntityId<Guid>
    where TDbContext : DbContext
{
    public BaseEntityUserDependentRepository(TDbContext dbContext, Func<TDalEntity, Guid, bool> ownerShipCheck, IMapper<TDalEntity, TDomainEntity> mapper) : base(dbContext, ownerShipCheck, mapper)
    {
    }
}

public class BaseEntityUserDependentRepository<TDalEntity, TDomainEntity, TKey, TDbContext> : IEntityUserDependentRepository<TDalEntity, TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TDomainEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TDbContext : DbContext
{
    
    protected readonly TDbContext RepoDbContext;
    protected readonly DbSet<TDomainEntity> RepoDbSet;
    protected readonly Func<TDalEntity, TKey, bool> OwnerShipCheck;
    protected readonly IMapper<TDalEntity, TDomainEntity> Mapper;

    public BaseEntityUserDependentRepository(TDbContext dbContext, Func<TDalEntity, TKey, bool> ownerShipCheck, IMapper<TDalEntity, TDomainEntity> mapper)
    {
        RepoDbContext = dbContext;
        RepoDbSet = dbContext.Set<TDomainEntity>();
        OwnerShipCheck = ownerShipCheck;
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
        var entity =  CreateQuery(noTracking).FirstOrDefault(entity => entity.Id.Equals(id));

        if (entity == null) return null;

        var entityDto = Mapper.Map(entity)!;

        return OwnerShipCheck(entityDto, userId) ? entityDto : null;
    }

    public virtual IEnumerable<TDalEntity> GetAll(TKey userId, bool noTracking = true)
    {
        var result = CreateQuery(noTracking).ToList();

        return Mapper.Map(result)
            .Where(entity => OwnerShipCheck(entity, userId));
    }

    public virtual bool Exists(TKey id)
    {
        return RepoDbSet.Any(entity => entity.Id.Equals(id));
    }

    public virtual async Task<TDalEntity?> FirstOrDefaultAsync(TKey id, TKey userId, bool noTracking = true)
    {
        var entity =  await CreateQuery(noTracking).FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        
        if (entity == null) return null;

        var entityDto = Mapper.Map(entity)!;
        
        return OwnerShipCheck(entityDto, userId) ? entityDto : null;
    }

    public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(TKey userId, bool noTracking = true)
    {
        var result =  await CreateQuery(noTracking).ToListAsync();
        
        return Mapper.Map(result).Where(entity => OwnerShipCheck(entity, userId));
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