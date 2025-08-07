using Com.Coppel.SDPC.Application.Infrastructure.ApiClients;
using Com.Coppel.SDPC.Cqrs.Commons;

namespace Com.Coppel.SDPC.Application.Features.Token;

public class GetTokenQueryHandler(IServiceApiToken service) : IQueryHandler<GetTokenQuery, string>
{
	public Task<string> HandleAsync(GetTokenQuery query) =>
		Task.Run(() => service.UseIdc() ?
			service.GetTokenIDC() :
			service.GetToken());
}
