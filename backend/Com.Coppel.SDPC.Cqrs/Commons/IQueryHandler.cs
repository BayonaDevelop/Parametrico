namespace Com.Coppel.SDPC.Cqrs.Commons;

public interface IQueryHandler<in TQuery, TResult>
{
	Task<TResult> HandleAsync(TQuery query);
}
