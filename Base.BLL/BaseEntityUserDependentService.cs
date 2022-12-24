using Base.Contracts;
using Base.Contracts.BLL;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace Base.BLL;

public class BaseEntityUserDependentService<TBllEntity, TDalEntity, TRepositoty> : 
    BaseEntityUserDependentService<TBllEntity, TDalEntity, TRepositoty, Guid>
    where TBllEntity : class, IDomainEntityId 
    where TDalEntity : class, IDomainEntityId
    where TRepositoty : IEntityUserDependentRepository<TDalEntity>
{
    public BaseEntityUserDependentService(TRepositoty repository, IMapper<TBllEntity, TDalEntity> mapper) : base(repository, mapper)
    {
    }
}

public class BaseEntityUserDependentService<TBllEntity, TDalEntity, TRepository, TKey> : IEntityUserDependentService<TBllEntity, TKey>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TRepository : IEntityUserDependentRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
{
    protected readonly TRepository Repository;
    protected readonly IMapper<TBllEntity, TDalEntity> Mapper;

    public BaseEntityUserDependentService(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }

    public TBllEntity Add(TBllEntity entity)
    {
        return Mapper.Map(Repository.Add(Mapper.Map(entity)!))!;
    }

    public TBllEntity Update(TBllEntity entity)
    {
        return Mapper.Map(Repository.Update(Mapper.Map(entity)!))!;
    }
    
    public async Task<TBllEntity> UpdateAsync(TBllEntity entity)
    {
        return Mapper.Map(await Repository.UpdateAsync(Mapper.Map(entity)!))!;
    }

    public TBllEntity Remove(TBllEntity entity)
    {
        return Mapper.Map(Repository.Remove(Mapper.Map(entity)!))!;
    }

    public TBllEntity Remove(TKey id, TKey userId)
    {
        return Mapper.Map(Repository.Remove(id, userId))!;
    }

    public TBllEntity? FirstOrDefault(TKey id, TKey userId, bool noTracking = true)
    {
        return Mapper.Map(Repository.FirstOrDefault(id, userId, noTracking));
    }

    public IEnumerable<TBllEntity> GetAll(TKey userId, bool noTracking = true)
    {
        return Mapper.Map(Repository.GetAll(userId, noTracking));
    }

    public bool Exists(TKey id)
    {
        return Repository.Exists(id);
    }

    public async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, TKey userId, bool noTracking = true)
    {
        return Mapper.Map(await Repository.FirstOrDefaultAsync(id, userId, noTracking));
    }

    public async Task<IEnumerable<TBllEntity>> GetAllAsync(TKey userId, bool noTracking = true)
    {
        return Mapper.Map(await Repository.GetAllAsync(userId, noTracking));
    }

    public async Task<bool> ExistsAsync(TKey id)
    {
        return await Repository.ExistsAsync(id);
    }

    public async Task<TBllEntity> RemoveAsync(TKey id, TKey userId)
    {
        return Mapper.Map(await Repository.RemoveAsync(id, userId))!;
    }
}