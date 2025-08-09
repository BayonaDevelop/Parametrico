using Com.Coppel.SDPC.Application.Infrastructure.Services;
using Com.Coppel.SDPC.Cqrs.Commons;

namespace Com.Coppel.SDPC.Application.Features.AsignacionDeLinea;

public class AsignacionDeLineaAfter20QueryHandler(IServiceAsignacionDeLinea service) : IQueryHandler<AsignacionDeLineaAfter20Query, bool>
{
	public async Task<bool> HandleAsync(AsignacionDeLineaAfter20Query query) =>
		await Task.FromResult(service.ProcessParametersCarterasAfter20(query.Token));
}
