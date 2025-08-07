using Com.Coppel.SDPC.Application.Features.AsignacionDeLinea;
using Com.Coppel.SDPC.Cqrs.Commons;

namespace Com.Coppel.SDPC.ServicioDatosParametricosCartera.Requests;

public class AsignacionDeLineaRequest(IQueryDispatcher queryDispatcher)
{
	public bool ProcessDaily(string token) =>
		queryDispatcher
			.DispatchAsync<AsignacionDeLineaDailyQuery, bool>(new AsignacionDeLineaDailyQuery(token))
			.GetAwaiter()
			.GetResult();

	public bool ProcessAfter20(string token) =>
		queryDispatcher
			.DispatchAsync<AsignacionDeLineaAfter20Query, bool>(new AsignacionDeLineaAfter20Query(token))
			.GetAwaiter()
			.GetResult();
}
