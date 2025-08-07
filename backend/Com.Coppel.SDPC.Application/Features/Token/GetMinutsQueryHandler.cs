using Com.Coppel.SDPC.Application.Infrastructure.ApiClients;
using Com.Coppel.SDPC.Cqrs.Commons;

namespace Com.Coppel.SDPC.Application.Features.Token;

public class GetMinutsQueryHandler(IServiceApiToken service) : IQueryHandler<GetMinutsQuery, int>
{
	public async Task<int> HandleAsync(GetMinutsQuery query) =>
		await Task.FromResult(service.GetMinutsBeforeTry());
}
