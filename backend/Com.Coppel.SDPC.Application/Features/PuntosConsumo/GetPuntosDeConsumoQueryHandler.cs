using Com.Coppel.SDPC.Application.Infrastructure.Services;
using Com.Coppel.SDPC.Application.Models.Persistence;
using Com.Coppel.SDPC.Cqrs.Commons;

namespace Com.Coppel.SDPC.Application.Features.PuntosConsumo;

public class GetPuntosDeConsumoQueryHandler(IServicePuntosDeConsumo service) : IQueryHandler<GetPuntosDeConsumoQuery, IEnumerable<PuntoDeConsumoVM>>
{
	public async Task<IEnumerable<PuntoDeConsumoVM>> HandleAsync(GetPuntosDeConsumoQuery query)
	{
		IEnumerable<PuntoDeConsumoVM> result = [.. service.GetAll().Select(i => new PuntoDeConsumoVM {
			IdFuncionalidad = i.IdFuncionalidad,
			NomFuncionalidad = i.NomFuncionalidad!,
			NomTbDestino = i.NomTbDestino!,
			RutaServicio = i.RutaServicio!,
			AllowAfter20 = i.AllowAfter20!.Value,
			Flag = i.Flag!.Value
		})];

		return await Task.FromResult(result).ConfigureAwait(false);
	}
}
