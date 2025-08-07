using Com.Coppel.SDPC.Core.Catalogos;

namespace Com.Coppel.SDPC.Application.Infrastructure.Services;

public interface IServicePuntosDeConsumo
{
	IEnumerable<CtlPuntosdeconsumo> GetAll();
}
