namespace Com.Coppel.SDPC.Application.Infrastructure.Services;

public interface IServiceAsignacionDeLinea
{
	bool ProcessParams(string token);

	bool ProcessCarterasAfter20(string token);
}
