using Com.Coppel.SDPC.Cqrs.Commons;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;

namespace Com.Coppel.SDPC.Cqrs;

public class ChannelDispatcher : ICommandDispatcher, IQueryDispatcher
{
	private readonly IServiceProvider _serviceProvider;

	private readonly Channel<Func<IServiceProvider, Task>> _commandChannel = Channel.CreateUnbounded<Func<IServiceProvider, Task>>();

	private readonly Channel<Func<IServiceProvider, Task>> _queryChannel = Channel.CreateUnbounded<Func<IServiceProvider, Task>>();

	public ChannelDispatcher(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
		_ = StartProcessingCommands();
		_ = StartProcessingQueries();
	}

	// --- Command Dispatching ---
	async Task ICommandDispatcher.DispatchAsync<TCommand>(TCommand command)
	{
		await _commandChannel.Writer.WriteAsync(async sp =>
		{
			// Resolve handler from service provider for each command
			var handler = sp.GetRequiredService<ICommandHandler<TCommand>>();
			await handler.HandleAsync(command);
		});
	}

	private async Task StartProcessingCommands()
	{
		await foreach (var handlerFunc in _commandChannel.Reader.ReadAllAsync())
		{
			using var scope = _serviceProvider.CreateScope();
			try
			{
				await handlerFunc(scope.ServiceProvider);
			}
			catch (Exception ex)
			{
				// Log the exception. Consider a robust error handling strategy (e.g., dead-letter queue).
				Console.WriteLine($"Error processing command: {ex.Message}");
			}
		}
	}

	// --- Query Dispatching ---
	public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query)
			where TQuery : notnull
			where TResult : notnull
	{
		// For queries, we need to wait for the result immediately.
		// This approach makes queries synchronous in terms of waiting for the result,
		// but the underlying handler execution can still leverage async operations.
		var completionSource = new TaskCompletionSource<TResult>();

		await _queryChannel.Writer.WriteAsync(async sp =>
		{
			try
			{
				var handler = sp.GetRequiredService<IQueryHandler<TQuery, TResult>>();
				var result = await handler.HandleAsync(query);
				completionSource.SetResult(result);
			}
			catch (Exception ex)
			{
				completionSource.SetException(ex);
				Console.WriteLine($"Error processing query: {ex.Message}");
			}
		});

		return await completionSource.Task;
	}

	private async Task StartProcessingQueries()
	{
		await foreach (var handlerFunc in _queryChannel.Reader.ReadAllAsync())
		{
			using var scope = _serviceProvider.CreateScope();
			try
			{
				await handlerFunc(scope.ServiceProvider);
			}
			catch (Exception ex)
			{
				// Log the exception. Queries typically propagate exceptions back to the caller.
				Console.WriteLine($"Error processing query: {ex.Message}");
			}
		}
	}
}
