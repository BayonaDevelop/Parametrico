namespace Com.Coppel.SDPC.Application.Infrastructure.ApiClients;

public interface IServiceApiToken
{
	bool CanConnectToCatalogos();

	bool UseIdc();

	string GetToken();

	string GetTokenIDC();

	int GetMinutsBeforeTry();
}
