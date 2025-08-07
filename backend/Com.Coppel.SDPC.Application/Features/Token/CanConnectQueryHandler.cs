using Com.Coppel.SDPC.Application.Infrastructure.ApiClients;
using Com.Coppel.SDPC.Cqrs.Commons;

namespace Com.Coppel.SDPC.Application.Features.Token;

public class CanConnectQueryHandler(IServiceApiToken service) : IQueryHandler<CanConnectQuery, bool>
{
	public async Task<bool> HandleAsync(CanConnectQuery query) =>
		await Task.FromResult(service.CanConnectToCatalogos());
}
