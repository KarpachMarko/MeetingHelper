namespace Base.Contracts;

public interface IMapper<TOut, TIn>
{
    TOut? Map(TIn? entity);
    TIn? Map(TOut? entity);
    
    IEnumerable<TOut> Map(IEnumerable<TIn> entities);
    IEnumerable<TIn> Map(IEnumerable<TOut> entities);
}