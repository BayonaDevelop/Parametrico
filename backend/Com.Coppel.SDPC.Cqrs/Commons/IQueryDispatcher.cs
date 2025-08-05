namespace Com.Coppel.SDPC.Cqrs.Commons;

public interface IQueryDispatcher
{
	Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : notnull where TResult : notnull;
}
