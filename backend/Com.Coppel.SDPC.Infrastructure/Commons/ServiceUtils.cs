using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Application.Models.Persistence;
using Com.Coppel.SDPC.Application.Models.Services;
using Com.Coppel.SDPC.Core.Catalogos;
using Com.Coppel.SDPC.Infrastructure.Commons.ApiBase;
using Com.Coppel.SDPC.Infrastructure.Commons.DataContexts;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;

namespace Com.Coppel.SDPC.Infrastructure.Commons;

public class ServiceUtils(CatalogosDbContext catalogosDbContext) : ApiClient
{
	private readonly Serilog.ILogger _log = Log.Logger;
	protected readonly CatalogosDbContext _dbContext = catalogosDbContext;

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

	protected dynamic GetDataFromApi(PuntoDeConsumoVM puntoDeConsumo, string cartera = null!)
	{
		dynamic response;

		if (Utils.IsInTesting())
		{
			string basePath = $"{Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)!.Replace("file:\\", "")!}\\Assets\\";
			string dataPath = $"{basePath}Data\\Catalogos\\{puntoDeConsumo.IdFuncionalidad}\\1-Api.json";

			string jsonData = File.ReadAllText(dataPath);

			response = FetchStringJsonFromGetAsync(puntoDeConsumo.RutaServicio, jsonData, new CancellationToken())
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}
		else
		{
			string carteraString = cartera ?? string.Empty;
			string uri = $"{puntoDeConsumo.RutaServicio}{(cartera == null ? "" : carteraString)}";
			response = FetchStringJsonFromGetAsync(uri, null!, new CancellationToken())
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		return response;
	}

	protected PuntoDeConsumoVM GetPuntoConsumo(int id)
	{
		try
		{
			return _dbContext.CtlPuntosdeconsumos.AsNoTracking().Select(i => new PuntoDeConsumoVM
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

			CtlParametrosautenticacion today = _dbContext.CtlParametrosautenticacions.FirstOrDefault(i => i.NombreParametro!.CompareTo("DEBUG_TODAY") == 0)!;
			CtlParametrosautenticacion after20 = _dbContext.CtlParametrosautenticacions.FirstOrDefault(i => i.NombreParametro!.CompareTo("DEBUG_TODAY_AFTER20") == 0)!;

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
			List<string> listOfEmails = [.. _dbContext.CatCorreosOperaciones
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
		_log.Information(message);
		ApiResultType getDataResult = serviceApi.GetDataFromApi(token, puntoDeConsumo);
		LogParametersDownload(message, getDataResult);
	}
}
