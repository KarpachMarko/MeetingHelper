using Base.Contracts;
using Base.Contracts.BLL;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace Base.BLL;

public class BaseEntityService<TBllEntity, TDalEntity, TRepository> : BaseEntityService<TBllEntity, TDalEntity, TRepository, Guid>
    where TBllEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
    where TRepository : IEntityRepository<TDalEntity>
{
    public BaseEntityService(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper) : base(repository, mapper)
    {
    }
}

public class BaseEntityService<TBllEntity, TDalEntity, TRepository, TKey> : IEntityService<TBllEntity, TKey>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TRepository : IEntityRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
{
    protected readonly TRepository Repository;
    protected readonly IMapper<TBllEntity, TDalEntity> Mapper;

    public BaseEntityService(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper)
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
        var temp = Mapper.Map(entity)!;
        return Mapper.Map(Repository.Remove(temp))!;
    }

    public TBllEntity Remove(TKey id)
    {
        return Mapper.Map(Repository.Remove(id))!;
    }

    public TBllEntity? FirstOrDefault(TKey id, bool noTracking = true)
    {
        return Mapper.Map(Repository.FirstOrDefault(id, noTracking));
    }

    public IEnumerable<TBllEntity> GetAll(bool noTracking = true)
    {
        return Mapper.Map(Repository.GetAll(noTracking));
    }

    public bool Exists(TKey id)
    {
        return Repository.Exists(id);
    }

    public async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
    {
        var x = await Repository.FirstOrDefaultAsync(id, noTracking);
        var res = Mapper.Map(x);
        return res;
    }

    public async Task<IEnumerable<TBllEntity>> GetAllAsync(bool noTracking = true)
    {
        return Mapper.Map(await Repository.GetAllAsync(noTracking));
    }

    public async Task<bool> ExistsAsync(TKey id)
    {
        return await Repository.ExistsAsync(id);
    }

    public async Task<TBllEntity> RemoveAsync(TKey id)
    {
        return Mapper.Map(await Repository.RemoveAsync(id))!;
    }
}