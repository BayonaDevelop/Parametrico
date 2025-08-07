namespace Com.Coppel.SDPC.Application.Infrastructure.Services;

public interface IServiceAsignacionDeLinea
{
	bool ProcessParamsDaily(string token);

	bool ProcessParametersCarterasAfter20(string token);
}
