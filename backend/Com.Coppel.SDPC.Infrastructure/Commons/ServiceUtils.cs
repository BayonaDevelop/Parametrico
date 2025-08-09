using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Application.Models.Persistence;
using Com.Coppel.SDPC.Application.Models.Services;
using Com.Coppel.SDPC.Core.Catalogos;
using Com.Coppel.SDPC.Infrastructure.Commons.ApiBase;
using Com.Coppel.SDPC.Infrastructure.Commons.DataContexts;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;

namespace Com.Coppel.SDPC.Infrastructure.Commons;

public class ServiceUtils(CatalogosDbContext catalogosDbContext) : ApiClient
{
	private readonly Serilog.ILogger _log = Log.Logger;
	protected readonly CatalogosDbContext _catalogosDbContext = catalogosDbContext;

	private void LogParametersDownload(string message, ApiResultType downloadResult)
	{
		switch (downloadResult)
		{
			case ApiResultType.SUCCESS:
				message += "Ok";
				_log.Information(message);
				break;

			case ApiResultType.NO_DATA:
				message += "No retorna información";
				_log.Warning(message);
				break;

			case ApiResultType.ALREADY_SAVED:
				message += $"Los datos ya han sido guardados previamente";
				_log.Warning(message);
				break;

			case ApiResultType.URI_NOT_FOUND:
				message += "Ruta inválida";
				_log.Error(message);
				break;

			default:
				message += "Tabla inválida";
				_log.Error(message);
				break;
		}
	}

	protected async Task<dynamic> GetDataFromApi(PuntoDeConsumoVM puntoDeConsumo, string cartera = null!, string jsonTestData = null!)
	{
		dynamic response;

		string uri = $"{puntoDeConsumo.RutaServicio}{cartera ?? string.Empty}";
		
		if (Utils.IsInTesting())
		{
			response = await FetchStringJsonFromGetAsync(uri, jsonTestData, new CancellationToken(false));
		}
		else
		{
			response = await FetchStringJsonFromGetAsync(uri, null!, new CancellationToken(false));
		}

		return response;
	}

	protected PuntoDeConsumoVM GetPuntoConsumo(int id)
	{
		try
		{
			return _catalogosDbContext.CtlPuntosdeconsumos.AsNoTracking().Select(i => new PuntoDeConsumoVM
			{
				IdFuncionalidad = i.IdFuncionalidad,
				Flag = i.Flag!.Value,
				AllowAfter20 = i.AllowAfter20!.Value,
				NomFuncionalidad = i.NomFuncionalidad!,
				RutaServicio = i.RutaServicio!,
				NomTbDestino = i.NomTbDestino!
			})
		.FirstOrDefault(i => i.IdFuncionalidad == id)!;
		}
		catch (Exception)
		{
			return null!;
		}
	}

	protected TestDatesVM GetDatesForDebug()
	{
		try
		{
			DateTime todayDate = DateTime.MinValue;
			DateTime after20Date = DateTime.MinValue;

			CtlParametrosautenticacion today = _catalogosDbContext.CtlParametrosautenticacions.FirstOrDefault(i => i.NombreParametro!.CompareTo("DEBUG_TODAY") == 0)!;
			CtlParametrosautenticacion after20 = _catalogosDbContext.CtlParametrosautenticacions.FirstOrDefault(i => i.NombreParametro!.CompareTo("DEBUG_TODAY_AFTER20") == 0)!;

			if (today != null)
			{
				string[] aux = today.ValorParametro!.Split('-');
				string day = aux[2].Split(' ')[0];
				todayDate = new DateTime(int.Parse(aux[0]), int.Parse(aux[1]), int.Parse(day), 0, 0, 0, DateTimeKind.Unspecified);
			}

			if (after20 != null)
			{
				string[] aux = after20.ValorParametro!.Split('-');
				string day = aux[2].Split(' ')[0];
				after20Date = new DateTime(int.Parse(aux[0]), int.Parse(aux[1]), int.Parse(day), 0, 0, 0, DateTimeKind.Unspecified);
			}

			return new TestDatesVM
			{
				Today = todayDate,
				After20 = after20Date
			};
		}
		catch (Exception)
		{
			return new TestDatesVM();
		}
	}

	protected string GetListOfEmails(MailType mailType)
	{
		string result = string.Empty;
		try
		{
			List<string> listOfEmails = [.. _catalogosDbContext.CatCorreosOperaciones
			.AsNoTracking()
			.Where(i => i.Tipo == (int)mailType)
			.Select(i => i.Correo)];

			result = string.Join(",", listOfEmails);
		}
		catch (Exception)
		{
			Debug.WriteLine("Ocurrio un error al consultar [CatCorreosOperaciones]");
			return string.Empty;
		}

		return result;
	}

	protected void DownloadParameters(dynamic serviceApi, string token, PuntoDeConsumoVM puntoDeConsumo)
	{
		string message = string.Format(SystemMessages.DESCARGA_PARAMETROS, puntoDeConsumo.NomTbDestino);
		ApiResultType getDataResult = serviceApi.GetData(token, puntoDeConsumo);
		LogParametersDownload(message, getDataResult);
	}

	public bool ChangeIntermediateTableStatus(string tableName, EstatusType estatus, DateTime today, List<dynamic> parameters = null!, bool carteraEnLinea = false)
	{
		try
		{
			using SqlConnection connection = new(_catalogosDbContext.Database.GetConnectionString());
			string query = carteraEnLinea ? $"UPDATE {tableName} SET estatusCL = @estatus" : $"UPDATE {tableName} SET estatus = @estatus";
			if (estatus == EstatusType.Finalizado)
			{
				query += carteraEnLinea ? ", fechaActualizacionCL = @fechaActualizacion" : ", fechaActualizacion = @fechaActualizacion";
			}

			query += " WHERE YEAR(fechaArranque) = @year AND " +
				"MONTH(fechaArranque) = @month AND " +
				"DAY(fechaArranque) = @day";

			if (estatus == EstatusType.Finalizado)
			{
				connection.Execute(query, new
				{
					estatus = (int)estatus,
					year = today.Year,
					month = today.Month,
					day = today.Day,
					fechaActualizacion = DateTime.Now
				});
			}
			else
			{
				connection.Execute(query, new
				{
					estatus = (int)estatus,
					year = today.Year,
					month = today.Month,
					day = today.Day
				});
			}

			if (parameters != null)
			{
				foreach (var item in parameters)
				{
					if (carteraEnLinea)
					{
						item.EstatusCl = (int)estatus;
					}
					else
					{
						item.Estatus = (int)estatus;
					}
				}
			}

			return true;
		}
		catch (Exception)
		{
			Debug.WriteLine("Error al cambiar el estatus de los parámetros");
			return false;
		}
	}

	public bool ExistBackup(DatabaseType database, DateTime today, string tableName)
	{
		try
		{
			string query = $"SELECT COUNT(BaseDatosOrigen) FROM {tableName} WHERE BaseDatosOrigen = @baseDatos AND REPLACE(CONVERT(varchar, fechaAlta, 103), '-', '/') = @fecha";
			var parameters = new
			{
				baseDatos = (int)database,
				fecha = $"{(today.Day < 10 ? $"0{today.Day}" : today.Day)}/{(today.Month < 10 ? $"0{today.Month}" : today.Month)}/{today.Year}"
			};

			using SqlConnection connection = new(_catalogosDbContext.Database.GetConnectionString());

			return connection.ExecuteScalar<int>(query, parameters) > 0;
		}
		catch (Exception)
		{
			string message = $"\t\t° Ocurrio un error al consultar la tabla [{tableName}]";
			_log.Warning(message);
			throw;
		}
	}

	public bool UndoBackupByDatabase(DatabaseType database, DateTime today, string tableName)
	{
		try
		{
			using SqlConnection connection = new(_catalogosDbContext.Database.GetConnectionString());
			string query = $"DELETE FROM {tableName} WHERE BaseDatosOrigen = @baseDatos AND REPLACE(CONVERT(varchar, fechaAlta, 103), '-', '/') = @fecha";

			connection.Execute(query, new
			{
				baseDatos = (int)database,
				fecha = $"{(today.Day < 10 ? $"0{today.Day}" : today.Day)}/{(today.Month < 10 ? $"0{today.Month}" : today.Month)}/{today.Year}"
			});

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}
}
