using Com.Coppel.SDPC.Application.Features.PuntosConsumo;
using Com.Coppel.SDPC.Application.Models.Persistence;
using Com.Coppel.SDPC.Cqrs.Commons;

namespace Com.Coppel.SDPC.ServicioDatosParametricosCartera.Requests;

public class PuntosDeConsumoRequest(IQueryDispatcher queryDispatcher)
{
	public IEnumerable<PuntoDeConsumoVM> GetAll() =>
		queryDispatcher
			.DispatchAsync<GetPuntosDeConsumoQuery, IEnumerable<PuntoDeConsumoVM>>(new GetPuntosDeConsumoQuery())
			.GetAwaiter()
			.GetResult();
}
