using Base.Contracts;
using Base.Contracts.BLL;
using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Base.BLL;

public class BaseEntityUserService<TBllEntity, TDalEntity, TBllUser, TDalUser, TRepository> :
    BaseEntityUserService<TBllEntity, TDalEntity, TBllUser, TDalUser, TRepository, Guid>
    where TBllEntity : class, IDomainEntityId, IDomainEntityUser<TBllUser>
    where TDalEntity : class, IDomainEntityId, IDomainEntityUser<TDalUser>
    where TBllUser : IdentityUser<Guid>
    where TDalUser : IdentityUser<Guid>
    where TRepository : IEntityUserRepository<TDalEntity, TDalUser>
{
    public BaseEntityUserService(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper) : base(repository, mapper)
    {
    }
}

public class BaseEntityUserService<TBllEntity, TDalEntity, TBllUser, TDalUser, TRepository, TKey> : 
    IEntityUserService<TBllEntity, TKey, TBllUser>
    where TBllEntity : class, IDomainEntityId<TKey>, IDomainEntityUser<TKey, TBllUser>
    where TDalEntity : class, IDomainEntityId<TKey>, IDomainEntityUser<TKey, TDalUser>
    where TBllUser : IdentityUser<TKey>
    where TDalUser : IdentityUser<TKey>
    where TRepository : IEntityUserRepository<TDalEntity, TKey, TDalUser>
    where TKey : IEquatable<TKey>
{
    protected readonly TRepository Repository;
    protected readonly IMapper<TBllEntity, TDalEntity> Mapper;

    public BaseEntityUserService(TRepository repository, IMapper<TBllEntity, TDalEntity> mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }

    public TBllEntity Add(TBllEntity entity)
    {
        return Mapper.Map(Repository.Add(Mapper.Map(entity)!))!;
    }

    public TBllEntity Update(TBllEntity entity, TKey userId)
    {
        return Mapper.Map(Repository.Update(Mapper.Map(entity)!, userId))!;
    }
    
    public async Task<TBllEntity> UpdateAsync(TBllEntity entity, TKey userId)
    {
        return Mapper.Map(await Repository.UpdateAsync(Mapper.Map(entity)!, userId))!;
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
        return Mapper.Map(await Repository.GetAllAsync(userId));
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