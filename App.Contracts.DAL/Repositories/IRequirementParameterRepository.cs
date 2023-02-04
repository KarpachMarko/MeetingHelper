﻿using App.DAL.DTO;
using Base.Contracts.DAL;
using Base.Contracts.Domain;

namespace App.Contracts.DAL.Repositories;

public interface IRequirementParameterRepository : IEntityUserDependentRepository<RequirementParameter>, IRequirementParameterRepositoryCustom<RequirementParameter>
{
    
}

public interface IRequirementParameterRepositoryCustom<TEntity>
    where TEntity : IDomainEntityId
{
    
}