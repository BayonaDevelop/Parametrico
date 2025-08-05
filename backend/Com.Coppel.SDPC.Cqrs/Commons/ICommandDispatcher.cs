namespace Com.Coppel.SDPC.Cqrs.Commons;

public interface ICommandDispatcher
{
	Task DispatchAsync<TCommand>(TCommand command) where TCommand : notnull;
}
