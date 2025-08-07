using Com.Coppel.SDPC.Application.Infrastructure.Services;
using Com.Coppel.SDPC.Core.Catalogos;
using Com.Coppel.SDPC.Infrastructure.Commons;
using Microsoft.EntityFrameworkCore;

namespace Com.Coppel.SDPC.Infrastructure.Services;
public class ServicePuntosDeConsumo : ServiceUtils, IServicePuntosDeConsumo
{
	public ServicePuntosDeConsumo() : base(new()) { }

	public IEnumerable<CtlPuntosdeconsumo> GetAll()
	{
		return [.. _catalogosDbContext.CtlPuntosdeconsumos.AsNoTracking().Where(i => i.Flag!.Value)];
	}
}

