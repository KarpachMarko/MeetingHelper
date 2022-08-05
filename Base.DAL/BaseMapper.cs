using Base.Contracts;

namespace Base.DAL;

public class BaseMapper<TOut, TIn> : IMapper<TOut, TIn>
{
    protected readonly AutoMapper.IMapper Mapper;

    public BaseMapper(AutoMapper.IMapper mapper)
    {
        Mapper = mapper;
    }

    public TOut? Map(TIn? entity)
    {
        return Mapper.Map<TOut>(entity);
    }

    public TIn? Map(TOut? entity)
    {
        return Mapper.Map<TIn>(entity);
    }

    public IEnumerable<TOut> Map(IEnumerable<TIn> entities)
    {
        return entities.Select(entity => Mapper.Map<TOut>(entity));
    }

    public IEnumerable<TIn> Map(IEnumerable<TOut> entities)
    {
        return entities.Select(entity => Mapper.Map<TIn>(entity));
    }
}