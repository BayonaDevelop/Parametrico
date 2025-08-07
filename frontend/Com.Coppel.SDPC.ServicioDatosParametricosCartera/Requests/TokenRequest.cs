using Com.Coppel.SDPC.Application.Features.Token;
using Com.Coppel.SDPC.Cqrs.Commons;

namespace Com.Coppel.SDPC.ServicioDatosParametricosCartera.Requests;

public class TokenRequest(IQueryDispatcher queryDispatcher)
{
	public bool CanConnectToCatalogos() =>
		queryDispatcher
			.DispatchAsync<CanConnectQuery, bool>(new CanConnectQuery())
			.GetAwaiter()
			.GetResult();

	public string GetToken() =>
		queryDispatcher
			.DispatchAsync<GetTokenQuery, string>(new GetTokenQuery())
			.GetAwaiter()
			.GetResult();

	public int GetMinuts() =>
		queryDispatcher
			.DispatchAsync<GetMinutsQuery, int>(new GetMinutsQuery())
			.GetAwaiter()
			.GetResult();
}
