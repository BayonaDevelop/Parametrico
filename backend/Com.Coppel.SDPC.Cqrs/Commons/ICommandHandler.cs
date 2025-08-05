namespace Com.Coppel.SDPC.Cqrs.Commons;

public interface ICommandHandler<in TCommand>
{
	Task HandleAsync(TCommand command);
}
