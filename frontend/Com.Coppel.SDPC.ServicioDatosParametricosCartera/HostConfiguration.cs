using Com.Coppel.SDPC.Application.Features.AsignacionDeLinea;
using Com.Coppel.SDPC.Application.Features.PuntosConsumo;
using Com.Coppel.SDPC.Application.Features.Token;
using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Application.Models.Persistence;
using Com.Coppel.SDPC.Application.Models.Services;
using Com.Coppel.SDPC.Cqrs;
using Com.Coppel.SDPC.Cqrs.Commons;
using Com.Coppel.SDPC.Infrastructure;
using Com.Coppel.SDPC.Infrastructure.Commons;
using Com.Coppel.SDPC.ServicioDatosParametricosCartera.Requests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using System.Collections.Immutable;

namespace Com.Coppel.SDPC.ServicioDatosParametricosCartera;

public class HostConfiguration
{
	private readonly string _token = string.Empty;
	private readonly List<int> _tasksList = [
			(int)PuntoDeConsumoTypeVM.SMLC,
			(int)PuntoDeConsumoTypeVM.SMCP,
			(int)PuntoDeConsumoTypeVM.DECREMENTO_LINEA_CREDITO,
			(int)PuntoDeConsumoTypeVM.TDI_CP,
			(int)PuntoDeConsumoTypeVM.TDI_CPC,
			(int)PuntoDeConsumoTypeVM.TDI_MORATORIO_CARTERA,
			(int)PuntoDeConsumoTypeVM.TDI_MORATORIO_DCP,
			(int)PuntoDeConsumoTypeVM.FACTORES_SATURACION_CARTERA,
			(int)PuntoDeConsumoTypeVM.TDI_CPPRESTAMO,
			(int)PuntoDeConsumoTypeVM.BONIFICACIONES,
			(int)PuntoDeConsumoTypeVM.CONVERSION_LINEA_CREDITO,
			(int)PuntoDeConsumoTypeVM.ASIGNACION_LINEAS,
			(int)PuntoDeConsumoTypeVM.ASIGNACION_GRUPO,
		];

	private readonly PuntosDeConsumoRequest _puntosDeConsumoRequest = null!;
	private readonly AsignacionDeLineaRequest _asignacionDeLineaRequest = null!;
	
	public readonly ILogger _log;
	public readonly ImmutableList<ProcessEventVM> _dailyEvents = [];
	public readonly ImmutableList<ProcessEventVM> _after20Events = [];
	public readonly int _minutsTowait = 0;

	public HostConfiguration(int idFuncionalidad)
	{		
		IHost host = InitializeLocalHost().Build();
		Log.Logger = LogConfiguration();
		_log = Log.Logger;

		IQueryDispatcher queryDispatcher = host.Services.GetService<IQueryDispatcher>()!;
		TokenRequest tokenRequest = new(queryDispatcher);
				
		string path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)!.Replace("file:\\", "");
		string configFilePath = $"{path}\\connections.{Utils.GetProfileName()}.json";

		if (File.Exists(configFilePath))
		{
			AesCipher.EncryptFile(configFilePath);
		}
		else
		{
			_log.Fatal(SystemMessages.ERROR_TEST_DECRYPT);
			return;
		}

		if (!tokenRequest.CanConnectToCatalogos())
		{
			_log.Fatal(SystemMessages.ERROR_TEST_CONNECTION);
			return;
		}

		_log.Information("Obteniendo token");
		_token = tokenRequest.GetToken();

		if (_token.IsNullOrEmpty())
		{
			_log.Fatal(SystemMessages.ERROR_GET_TOKEN);
			return;
		}

		_log.Information("Cargando funcionalidades");

		_puntosDeConsumoRequest = new(queryDispatcher);
		_asignacionDeLineaRequest = new(queryDispatcher);

		_minutsTowait = tokenRequest.GetMinuts();
		List<int> excludedTasksList = [];
		if (idFuncionalidad > 0)
		{
			_tasksList.Remove(idFuncionalidad);
			excludedTasksList = _tasksList;
		}

		_dailyEvents = GetEvents(excludedTasksList);
		_after20Events = _dailyEvents.Where(i => i.AllowAfter20).ToImmutableList();
	}

	public async Task RunDailyEvents()
	{
		foreach (ProcessEventVM item in _dailyEvents)
		{
			if (!item.Success)
			{
				item.Success = await ExecuteDaylyEvent(item);
			}
		}
	}

	public async Task RunAfter20Events()
	{
		foreach (ProcessEventVM item in _after20Events)
		{
			if (!item.Success)
			{
				item.Success = await ExecuteAfter20Event(item);
			}
		}
	}

	private static IHostBuilder InitializeLocalHost()
	{
		Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Verbose()
			.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Verbose)
			.WriteTo.Console(
				outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u5}] {Message:lj}{NewLine}{Exception}",
				theme: new AppLogTheme()
			)
			.CreateLogger();

		return Host.CreateDefaultBuilder()
			.UseSerilog()
			.ConfigureServices(services => {
				services.AddInfrastructureServices();

				// Register Dispatchers
				services.AddSingleton<ICommandDispatcher, ChannelDispatcher>();
				services.AddSingleton<IQueryDispatcher, ChannelDispatcher>();

				// Registro de acciones
				services.AddScoped<IQueryHandler<CanConnectQuery, bool>, CanConnectQueryHandler>();
				services.AddScoped<IQueryHandler<GetTokenQuery, string>, GetTokenQueryHandler>();
				services.AddScoped<IQueryHandler<GetMinutsQuery, int>, GetMinutsQueryHandler>();
				services.AddScoped<IQueryHandler<GetPuntosDeConsumoQuery, IEnumerable<PuntoDeConsumoVM>>, GetPuntosDeConsumoQueryHandler>();

				services.AddScoped<IQueryHandler<AsignacionDeLineaDailyQuery, bool>,  AsignacionDeLineaDailyQueryHandler>();
				services.AddScoped<IQueryHandler<AsignacionDeLineaAfter20Query, bool>, AsignacionDeLineaAfter20QueryHandler>();
			});
	}

	private static Logger LogConfiguration() =>
		new LoggerConfiguration()
			.MinimumLevel.Verbose()
			.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Verbose)
			.WriteTo.Console(
				outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u5}] {Message:lj}{NewLine}{Exception}",
				theme: new AppLogTheme()
			)
			
			.CreateLogger();

	private ImmutableList<ProcessEventVM> GetEvents(List<int> excludedTasks) =>
		_puntosDeConsumoRequest.GetAll()
			.Where(i => !excludedTasks.Contains(i.IdFuncionalidad))
			.Select(i => new ProcessEventVM
			{
				IdFuncionalidad = i.IdFuncionalidad,
				NomFuncionalidad = i.NomFuncionalidad,
				NomTbDestino = i.NomTbDestino,
				RutaServicio = i.RutaServicio,
				AllowAfter20 = i.AllowAfter20,
				Flag = i.Flag,
				Success = false
			})
			.ToImmutableList();

	private async Task<bool> ExecuteDaylyEvent(ProcessEventVM task)
	{
		bool result;

		switch(task.IdFuncionalidad)
		{
			case (int)PuntoDeConsumoTypeVM.ASIGNACION_LINEAS:
				result = _asignacionDeLineaRequest.ProcessDaily(_token);
				break;

			default:
				result = false;
				break;
		}

		return await Task.FromResult(result);
	}

	private async Task<bool> ExecuteAfter20Event(ProcessEventVM task)
	{
		bool result;

		switch (task.IdFuncionalidad)
		{
			case (int)PuntoDeConsumoTypeVM.ASIGNACION_LINEAS:
				result = _asignacionDeLineaRequest.ProcessAfter20(_token);
				break;

			default:
				result = false;
				break;
		}

		return await Task.FromResult(result);
	}
}
