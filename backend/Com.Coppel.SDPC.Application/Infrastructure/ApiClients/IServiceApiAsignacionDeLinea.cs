using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Application.Models.Persistence;

namespace Com.Coppel.SDPC.Application.Infrastructure.ApiClients;

public interface IServiceApiAsignacionDeLinea
{
	ApiResultType GetData(string token, PuntoDeConsumoVM puntoDeConsumo);
}
