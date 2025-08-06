using Com.Coppel.SDPC.Infrastructure.Commons.DataContexts;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Coppel.SDPC.Infrastructure;

public static class InfrastructureServiceRegistration
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
	{
		/// Registro de DbContext
		services.AddDbContext<CatDbContext>();
		services.AddDbContext<CatalogosDbContext>();
		services.AddDbContext<CarterasDbContext>();
		services.AddDbContext<ControlTiendasDbContext>();
		services.AddDbContext<Emision20DbContext>();
		services.AddDbContext<ListadosCobranzaDbContext>();



		return services;
	}
}
