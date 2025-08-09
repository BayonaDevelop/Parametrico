using Com.Coppel.SDPC.Application.Commons.Files;
using Com.Coppel.SDPC.Application.Infrastructure.ApiClients;
using Com.Coppel.SDPC.Application.Infrastructure.Services;
using Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.AsignacionDeLinea;
using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Application.Models.Persistence;
using Com.Coppel.SDPC.Application.Models.Reports.AsignacionLinea;
using Com.Coppel.SDPC.Application.Models.Reports;
using Com.Coppel.SDPC.Application.Models.Services;
using Com.Coppel.SDPC.Core.Catalogos;
using Com.Coppel.SDPC.Infrastructure.Commons;
using Com.Coppel.SDPC.Infrastructure.Commons.DataContexts;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using System.Data;

namespace Com.Coppel.SDPC.Infrastructure.Services;

public class ServiceAsignacionDeLinea : ServiceUtils, IServiceAsignacionDeLinea
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
		List<dynamic> result = [];
		DateTime startDate = DateTime.Now;
		DateTime bannedDate = new(1900, 01, 01, 0, 0, 0, DateTimeKind.Local);
		DateTime cutAt20 = new(startDate.Year, startDate.Month, 20, 0, 0, 0, DateTimeKind.Local);

		if (!Utils.IsInProduction())
		{
			startDate = _testDates.Today;
			cutAt20 = new(startDate.Year, startDate.Month, 20, 0, 0, 0, DateTimeKind.Local);
		}

		try
		{
			DateTime mostRecentDate = _catalogosDbContext.CtlAsignacionDeLineas.Max(i => i.FechaArranque);
			List<CtlAsignacionDeLinea> records = _catalogosDbContext.CtlAsignacionDeLineas.AsNoTracking().Where(i => i.FechaArranque.Date == mostRecentDate.Date).ToList();
			CtlAsignacionDeLinea test = records.FirstOrDefault(i => _listOfValidStatusToBeCensed.Contains(i.Estatus) && i.FechaArranque.Date != bannedDate && i.FechaArranque.Date <= startDate.Date)!;

			if (test != null && startDate.Date == cutAt20.Date)
			{
				List<CtlAsignacionDeLinea> data = records
					.Where(i =>
						_listOfValidStatusToBeCensed.Contains(i.Estatus) &&
						i.FechaArranque.Date != bannedDate &&
						i.FechaArranque.Date <= startDate.Date
					)
					.ToList();

				data.ForEach(i => { result.Add(i); });

				if (result[0].Estatus < (int)EstatusType.EnProceso)
				{
					ChangeIntermediateTableStatus(_puntoDeConsumo.NomTbDestino, EstatusType.EnProceso, result[0].FechaArranque, _parameters);
				}

				return result;
			}
			else
			{
				return [];
			}
		}
		catch (Exception)
		{
			return [];
		}
	}

	private void PrepareBackup(DatabaseType database)
	{
		string messageCatalogos = string.Format(SystemMessages.PREPARAR_RESPALDO, Utils.GetTableName(typeof(CatParametrosasignacionlinea)), Enum.GetName(typeof(DatabaseType), DatabaseType.Catalogos)!);
		string messageCarteras = string.Format(SystemMessages.PREPARAR_RESPALDO, Utils.GetTableName(typeof(CatParametrosasignacionlinea)), Enum.GetName(typeof(DatabaseType), DatabaseType.Carteras)!);

		try
		{
			switch (database)
			{
				case DatabaseType.Catalogos:
					if (_catalogosDbContext.Database.CanConnect())
					{
						_catParametrosasignacionlineaBeforeUpdate = [.. _catalogosDbContext.CatParametrosasignacionlineas.AsNoTracking()]; //PARA SACAR LA INFO ANTES DEL CAMBIO PARA EL REPORTE
						_catParametrosasignacionlineasOldInCatalogos = [.. _catalogosDbContext.CatParametrosasignacionlineas
							.AsNoTracking()
							.Select(i => new CatParametrosasignacionlineaHistorial
							{
								BaseDatosOrigen = (int)DatabaseType.Catalogos,
								FechaAlta = _today,
								NumLinearealinicial = i.NumLinearealinicial,
								NumLinearealfinal = i.NumLinearealfinal,
								IduLineadecredito = i.IduLineadecredito,
								NomLineadecredito = i.NomLineadecredito,
								IduPerfil = i.IduPerfil,
								NumValorperfil = i.NumValorperfil,
								FecMovimiento = i.FecMovimiento
							})];
						messageCatalogos += "Ok";
						_log.Information(messageCatalogos);
					}
					else
					{
						messageCatalogos += "Error de conexión";
						_log.Error(messageCatalogos);
					}
					break;

				case DatabaseType.Carteras:
					if (_carterasDbContext.Database.CanConnect())
					{
						_catParametrosasignacionlineasOldInCarteras = [.. _carterasDbContext.CatParametrosasignacionlineas
							.AsNoTracking()
							.Select(i => new CatParametrosasignacionlineaHistorial
							{
								BaseDatosOrigen = (int)DatabaseType.Carteras,
								FechaAlta = _today,
								NumLinearealinicial = i.NumLinearealinicial,
								NumLinearealfinal = i.NumLinearealfinal,
								IduLineadecredito = i.IduLineadecredito,
								NomLineadecredito = i.NomLineadecredito,
								IduPerfil = i.IduPerfil,
								NumValorperfil = i.NumValorperfil,
								FecMovimiento = i.FecMovimiento
							})];

						messageCarteras += "Ok";
						_log.Information(messageCarteras);
					}
					else
					{
						messageCarteras += "Error de conexión";
						_log.Error(messageCarteras);
					}
					break;
			}
		}
		catch (Exception)
		{
			switch (database)
			{
				case DatabaseType.Catalogos:
					messageCatalogos += $"Error al intentar obtener los datos datos";
					_log.Error(messageCatalogos);
					break;

				case DatabaseType.Carteras:
					messageCarteras += $"Error al intentar obtener los datos";
					_log.Error(messageCarteras);
					break;
			}
		}
	}

	private void Backup(DatabaseType database)
	{
		string messageCatalogos = string.Format(SystemMessages.RESPALDO, Enum.GetName(typeof(DatabaseType), DatabaseType.Catalogos)!);
		string messageCarteras = string.Format(SystemMessages.RESPALDO, Enum.GetName(typeof(DatabaseType), DatabaseType.Carteras)!);

		try
		{
			switch (database)
			{
				case DatabaseType.Catalogos:
					if (_catParametrosasignacionlineasOldInCatalogos.Count > 0)
					{
						_catalogosDbContext.BulkInsert(_catParametrosasignacionlineasOldInCatalogos);
						messageCatalogos += "Ok";
						_log.Information(messageCatalogos);
					}
					else
					{
						messageCatalogos += "No hay datos que respaldar";
						_log.Information(messageCatalogos);
					}
					break;

				case DatabaseType.Carteras:
					if (_catParametrosasignacionlineasOldInCarteras.Count > 0)
					{
						_catalogosDbContext.BulkInsert(_catParametrosasignacionlineasOldInCarteras);
						messageCarteras += "Ok";
						_log.Information(messageCatalogos);
					}
					else
					{
						messageCarteras += "No hay datos que respaldar";
						_log.Error(messageCarteras);
					}
					break;
			}
		}
		catch (Exception)
		{
			switch (database)
			{
				case DatabaseType.Catalogos:
					messageCatalogos += $"Error al guardar los datos";
					_log.Error(messageCatalogos);
					break;

				case DatabaseType.Carteras:
					messageCarteras += $"Error al guardar los datos";
					_log.Error(messageCarteras);
					break;
			}
		}
	}

	private void UpdateCatalogosFromParameters()
	{
		using IDbContextTransaction transaction = _carterasDbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted);

		try
		{
			using SqlConnection connection = new(_catalogosDbContext.Database.GetConnectionString());
			
			connection.Execute("DELETE FROM cat_parametrosasignacionlinea");

			foreach (var item in _parameters)
			{
				string query = $"INSERT INTO cat_parametrosasignacionlinea (num_linearealinicial,num_linearealfinal,idu_lineadecredito,nom_lineadecredito,idu_perfil,num_valorperfil,fec_movimiento) VALUES (@rangoMin,@rangoMax,@idu_lineadecredito,@tipoLinea,@idu_perfil,@valor,@fechaArranque)";
				connection.Execute(query, new
				{
					rangoMin = item.RangoMin,
					rangoMax = item.RangoMax,
					idu_lineadecredito = item.IduLineadecredito,
					tipoLinea = item.TipoLinea,
					idu_perfil = item.IduPerfil,
					valor = item.Valor,
					fechaArranque = item.FechaArranque
				});
			}

			transaction.Commit();
		}
		catch (Exception)
		{
			transaction.Rollback();
			string message = string.Format(SystemMessages.ERROR_ACTUALIZAR_TABLA, Utils.GetTableName(typeof(CatParametrosasignacionlinea)));
			_serviceEmail.SendMailCarterasReplication(_puntoDeConsumo, _contactsOfCarteraCentral, Utils.GetTableName(typeof(CatParametrosasignacionlinea)), Enum.GetName(typeof(DatabaseType), DatabaseType.Catalogos)!, _parameters[0].FechaArranque, false);
			throw;
		}
	}

	private void ProcessParametersOfCatalogos()
	{
		string message = string.Empty;
		using var transaction = _catalogosDbContext.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
		try
		{
			message = string.Format(SystemMessages.INICIA_BD, Enum.GetName(typeof(DatabaseType), DatabaseType.Catalogos)!);
			_log.Information(message);

			if (!ExistBackup(DatabaseType.Catalogos, _today, "cat_parametrosasignacionlinea_Historial"))
			{
				PrepareBackup(DatabaseType.Catalogos);
				Backup(DatabaseType.Catalogos);
			}

			UpdateCatalogosFromParameters();
			_catParametrosasignacionlineas = [.. _catalogosDbContext.CatParametrosasignacionlineas];
			transaction.Commit();

			message = string.Format(SystemMessages.FINALIZA_BD, Enum.GetName(typeof(DatabaseType), DatabaseType.Catalogos)!);
			_log.Information(message);
		}
		catch (Exception)
		{
			transaction.Rollback();
			UndoBackupByDatabase(DatabaseType.Catalogos, _today, "cat_parametrosasignacionlinea_Historial");
			
			message = string.Format(SystemMessages.FINALIZA_BD, Enum.GetName(typeof(DatabaseType), DatabaseType.Catalogos)!);
			_log.Information(message);

			throw;
		}
	}

	private List<CifraDeControlAsignacionLineaVM> CifrasDeControlAntes()
	{
		try
		{
			List<CifraDeControlAsignacionLineaVM> cifraList = [];

			foreach (var valoresTabla in _catParametrosasignacionlineaBeforeUpdate)
			{
				CifraDeControlAsignacionLineaVM cifra = new()
				{
					NumLineaRealInicial = valoresTabla.NumLinearealinicial.ToString(),
					NumLineaRealFinal = valoresTabla.NumLinearealfinal.ToString(),
					IduLineaDeCredito = valoresTabla.IduLineadecredito.ToString(),
					NomLineaDeCRedito = valoresTabla.NomLineadecredito,
					IduPerfil = valoresTabla.IduPerfil.ToString(),
					NumValorPerfil = valoresTabla.NumValorperfil.ToString(),
					FecMovimiento = valoresTabla.FecMovimiento.ToShortDateString()
				};

				cifraList.Add(cifra);
			}

			return cifraList;
		}
		catch (Exception)
		{
			return [];
		}
	}

	private List<CifraDeControlAsignacionLineaVM> CifrasDeControl()
	{
		try
		{
			List<CifraDeControlAsignacionLineaVM> cifraList = [];

			foreach (var valoresTabla in _catParametrosasignacionlineas)
			{
				CifraDeControlAsignacionLineaVM cifra = new()
				{
					NumLineaRealInicial = valoresTabla.NumLinearealinicial.ToString(),
					NumLineaRealFinal = valoresTabla.NumLinearealfinal.ToString(),
					IduLineaDeCredito = valoresTabla.IduLineadecredito.ToString(),
					NomLineaDeCRedito = valoresTabla.NomLineadecredito,
					IduPerfil = valoresTabla.IduPerfil.ToString(),
					NumValorPerfil = valoresTabla.NumValorperfil.ToString(),
					FecMovimiento = valoresTabla.FecMovimiento.ToShortDateString()
				};

				cifraList.Add(cifra);
			}

			return cifraList;
		}
		catch (Exception)
		{
			return [];
		}
	}

	private void SendCifrasDeControl()
	{
		string message;
		try
		{
			var textTableCatParametrosAsignacionLinea = Utils.GetTableName(typeof(CatParametrosasignacionlinea));
			var nombreTablaCatParametrosAsignacionLinea = textTableCatParametrosAsignacionLinea.Split(".")[1];

			var hasRecords = _catParametrosasignacionlineaBeforeUpdate.Count == 0;

			var fechaArranque = _parameters[0].FechaArranque;
			var fechaActualizacion = new DateTime(_today.Year, _today.Month, _today.Day, 0, 0, 0, DateTimeKind.Local);

			FileContentVM archivoAntesRequest = new()
			{
				ViewModel = typeof(CifraDeControlAsignacionLineaVM),
				ViewModelExcel = typeof(CatParametrosasignacionlinea),
				Area = AreaType.CALIDAD,
				PuntoDeConsumo = _puntoDeConsumo,
				Data = [],
				DataExcel = [],
				TableName = nombreTablaCatParametrosAsignacionLinea,
				ColumnSizes = [0, 0, 0, 0, 0, 0, 0],
				PageOrientation = PageOrientationTypeVM.HORIZONTAL,
				NewTitleForFileBeforeUpdate = true,
				ExtraFile = true,
				EmptyFile = hasRecords,
				FechaArranque = fechaArranque,
				FechaAlta = fechaActualizacion,
				NewData = true
			};

			foreach (var item in CifrasDeControlAntes())
			{
				archivoAntesRequest.Data.Add(item);
			}

			var pdfAntesPath = _servicePdf.CreateFile(archivoAntesRequest, "Assets\\coppel.png");

			FileContentVM archivoRequest = new()
			{
				ViewModel = typeof(CifraDeControlAsignacionLineaVM),
				ViewModelExcel = typeof(CatParametrosasignacionlinea),
				Area = AreaType.CALIDAD,
				PuntoDeConsumo = _puntoDeConsumo,
				Data = [],
				DataExcel = [],
				TableName = nombreTablaCatParametrosAsignacionLinea,
				ColumnSizes = [0, 0, 0, 0, 0, 0, 0],
				PageOrientation = PageOrientationTypeVM.HORIZONTAL,
				FechaAlta = fechaActualizacion,
				FechaArranque = fechaArranque,
				NewData = true,
				ShowTableDates = true
			};

			foreach (var item in CifrasDeControl())
			{
				archivoRequest.Data.Add(item);
			}

			var _ExcelCatParametrosAsignacionLinea = _catalogosDbContext.CatParametrosasignacionlineas.ToList();
			foreach (var item in _ExcelCatParametrosAsignacionLinea)
			{
				archivoRequest.DataExcel.Add(item);
			}

			var pdfPath = _servicePdf.CreateFile(archivoRequest, "Assets\\coppel.png");
			var excelPath = _serviceExcel.CreateFile(archivoRequest, "Assets\\coppel.png");

			EmailVM mail = new()
			{
				Subject = $"Cifras de control de {_puntoDeConsumo.NomFuncionalidad}",
				To = _contactsOfCarteraCentral,
				Body = "",
				Files = [pdfPath]
			};

			if (!string.IsNullOrEmpty(excelPath))
				mail.Files.Add(excelPath);
			if (!string.IsNullOrEmpty(pdfAntesPath))
				mail.Files.Add(pdfAntesPath);

			var sendOk = _serviceEmail.SendEmail(mail);
			if (sendOk)
			{
				message = string.Format(SystemMessages.SEND_MAIL, _puntoDeConsumo.NomFuncionalidad);
				_log.Information(message);
			}
			else
			{
				message = string.Format(SystemMessages.ERROR_SEND_MAIL, _puntoDeConsumo.NomFuncionalidad);
				_log.Warning(message);
			}

		}
		catch (Exception)
		{
			message = string.Format(SystemMessages.ERROR_SEND_MAIL, _puntoDeConsumo.NomFuncionalidad);
			_log.Warning(message);
		}
	}

	public bool ProcessParamsDaily(string token)
	{
		List<int> validStatus = [(int)EstatusType.PorActualizar, (int)EstatusType.EnProceso, (int)EstatusType.Actualizado, (int)EstatusType.Fallido];
		string message;
		bool result = false;

		try
		{			
			message = string.Format(SystemMessages.INICIO_PROCESO, _puntoDeConsumo.NomFuncionalidad);
			_log.Verbose(message);
			
			DownloadParameters(_serviceApi, token, _puntoDeConsumo);
			_parameters = GetSensedParameters();
			if (_parameters.Any(i => validStatus.Contains(i.Estatus)))
			{
				message = $"{SystemMessages.CENSADO_PARAMETROS}Ok";
				_log.Information(message);

				ProcessParametersOfCatalogos();
				ChangeIntermediateTableStatus(_puntoDeConsumo.NomTbDestino, EstatusType.Actualizado, _parameters[0].FechaArranque, _parameters);
				SendCifrasDeControl();
				ChangeIntermediateTableStatus(_puntoDeConsumo.NomTbDestino, EstatusType.Replicando, _parameters[0].FechaArranque, _parameters);
				
			}
			else
			{
				if (_parameters.Any(i => i.Estatus == (int)EstatusType.Replicando))
				{
					message = $"{SystemMessages.CENSADO_PARAMETROS}Ok";
					_log.Information(message);
				}
				else
				{
					message = $"{SystemMessages.CENSADO_PARAMETROS}No hay parámetros pendientes de procesar en [ctl_AsignacionDeLinea]";
					_log.Warning(message);

					message = string.Format(SystemMessages.FIN_PROCESO, _puntoDeConsumo.NomFuncionalidad);
					_log.Verbose(message);
					return false;
				}
			}

			message = string.Format(SystemMessages.FIN_PROCESO, _puntoDeConsumo.NomFuncionalidad);
			_log.Verbose(message);

			result = true;
		}
		catch (Exception)
		{
			message = string.Format(SystemMessages.FINALIZA_BD, Enum.GetName(typeof(DatabaseType), DatabaseType.Catalogos)!);
			_log.Information(message);

			ChangeIntermediateTableStatus(_puntoDeConsumo.NomTbDestino, EstatusType.Fallido, _parameters[0].FechaArranque, _parameters);
			return false;
		}

		return result;
	}

	public bool ProcessParametersCarterasAfter20(string token)
	{
		throw new NotImplementedException();
	}
}
