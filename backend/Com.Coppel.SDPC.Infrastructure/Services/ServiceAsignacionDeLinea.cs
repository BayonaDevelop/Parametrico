using Com.Coppel.SDPC.Application.Commons.Files;
using Com.Coppel.SDPC.Application.Infrastructure.ApiClients;
using Com.Coppel.SDPC.Application.Infrastructure.Services;
using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Application.Models.Persistence;
using Com.Coppel.SDPC.Application.Models.Services;
using Com.Coppel.SDPC.Infrastructure.Commons;
using Com.Coppel.SDPC.Infrastructure.Commons.DataContexts;
using Serilog;

namespace Com.Coppel.SDPC.Infrastructure.Services;

public class ServiceAsignacionDeLinea: ServiceUtils, IServiceAsignacionDeLinea
{
	/// Inyección de servicios
	private readonly Serilog.ILogger _log = Log.Logger;
	private readonly CarterasDbContext _carterasDbContext;
	private readonly IServiceApiAsignacionDeLinea _serviceApi;
	private readonly IServiceEmail _serviceEmail;
	private readonly IServiceExcel _serviceExcel;
	private readonly IServicePdfAignacionLineasCredito _servicePdf;

	private readonly PuntoDeConsumoVM _puntoDeConsumo;
	private readonly TestDatesVM _testDates;
	private readonly string _contactsOfCarteraCentral;
	private readonly List<int> _listOfValidStatusToBeCensed;
	private readonly DateTime _today;
	
	public ServiceAsignacionDeLinea
	(
		IServiceApiAsignacionDeLinea serviceApi,
		IServiceEmail serviceEmail, 
		IServiceExcel serviceExcel, 
		IServicePdfAignacionLineasCredito servicePdf
	) : base(new())
	{
		_carterasDbContext = new();
		_serviceApi = serviceApi;
		_serviceEmail = serviceEmail;
		_serviceExcel = serviceExcel;
		_servicePdf = servicePdf;

		_puntoDeConsumo = GetPuntoConsumo((int)PuntoDeConsumoTypeVM.ASIGNACION_LINEAS);
		_testDates = GetDatesForDebug();
		_contactsOfCarteraCentral = GetListOfEmails(MailType.CarteraCentral);
		_listOfValidStatusToBeCensed =
		[
			(int) EstatusType.PorActualizar,
			(int) EstatusType.EnProceso,
			(int) EstatusType.Replicando,
			(int) EstatusType.Fallido
		];
		_today = DateTime.Now;
	}

	public bool ProcessParamsDaily(string token)
	{
		string message;
		bool result;

		try
		{
			message = string.Format(SystemMessages.INICIO_PROCESO, _puntoDeConsumo.NomFuncionalidad);
			_log.Information(message);
			DownloadParameters(_serviceApi, token, _puntoDeConsumo);

			result = true;
		}
		catch (Exception)
		{
			result = false;
		}

		return result;
	}

	public bool ProcessParametersCarterasAfter20(string token)
	{
		throw new NotImplementedException();
	}
}
