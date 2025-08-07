using Com.Coppel.SDPC.Application.Infrastructure.ApiClients;
using Com.Coppel.SDPC.Application.Infrastructure.Services;
using Com.Coppel.SDPC.Infrastructure.ApiClients;
using Com.Coppel.SDPC.Infrastructure.Commons.DataContexts;
using Com.Coppel.SDPC.Infrastructure.Services;
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

		/// Servicios de API
		_ = services.AddTransient<IServiceApiToken, ServiceApiToken>();
		_ = services.AddTransient<IServiceApiAsignacionDeLinea, ServiceApiAsignacionDeLinea>();

		/// Servicios de Procesos
		_ = services.AddTransient<IServiceAsignacionDeLinea, ServiceAsignacionDeLinea>();

		return services;
	}
}
