using Com.Coppel.SDPC.Application.Commons.Files;
using Com.Coppel.SDPC.Application.Infrastructure.ApiClients;
using Com.Coppel.SDPC.Application.Infrastructure.Services;
using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Application.Models.Persistence;
using Com.Coppel.SDPC.Application.Models.Services;
using Com.Coppel.SDPC.Core.Catalogos;
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

	private List<dynamic> _parameters;
	private List<CatParametrosasignacionlinea> _catParametrosasignacionlineas;
	private List<CatParametrosasignacionlineaHistorial> _catParametrosasignacionlineasOldInCatalogos;
	private List<CatParametrosasignacionlineaHistorial> _catParametrosasignacionlineasOldInCarteras;
	private List<CatParametrosasignacionlinea> _catParametrosasignacionlineaBeforeUpdate;

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

		_parameters = [];
		_catParametrosasignacionlineas = [];
		_catParametrosasignacionlineasOldInCatalogos = [];
		_catParametrosasignacionlineasOldInCarteras = [];
		_catParametrosasignacionlineaBeforeUpdate = [];
	}

	private List<dynamic> GetSensedParameters()
	{

	}

	public bool ProcessParamsDaily(string token)
	{
		List<int> validStatus = [(int)EstatusType.PorActualizar, (int)EstatusType.EnProceso, (int)EstatusType.Actualizado, (int)EstatusType.Fallido];
		string message;
		bool result;

		try
		{			
			message = string.Format(SystemMessages.INICIO_PROCESO, _puntoDeConsumo.NomFuncionalidad);
			_log.Verbose(message);
			
			DownloadParameters(_serviceApi, token, _puntoDeConsumo);
			_parameters = CensusParameters();

			message = string.Format(SystemMessages.FIN_PROCESO, _puntoDeConsumo.NomFuncionalidad);
			_log.Verbose(message);

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
