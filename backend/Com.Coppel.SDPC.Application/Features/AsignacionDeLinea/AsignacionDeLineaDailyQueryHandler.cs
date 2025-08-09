using Com.Coppel.SDPC.Application.Infrastructure.Services;
using Com.Coppel.SDPC.Cqrs.Commons;

namespace Com.Coppel.SDPC.Application.Features.AsignacionDeLinea;

public class AsignacionDeLineaDailyQueryHandler(IServiceAsignacionDeLinea service) : IQueryHandler<AsignacionDeLineaDailyQuery, bool>
{
	public async Task<bool> HandleAsync(AsignacionDeLineaDailyQuery query) =>
		await Task.FromResult(service.ProcessParamsDaily(query.Token));
}
