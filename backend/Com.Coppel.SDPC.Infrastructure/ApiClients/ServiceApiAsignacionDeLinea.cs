using Com.Coppel.SDPC.Application.Infrastructure.ApiClients;
using Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.AsignacionDeLinea;
using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Application.Models.Persistence;
using Com.Coppel.SDPC.Core.Catalogos;
using Com.Coppel.SDPC.Infrastructure.Commons;
using Com.Coppel.SDPC.Infrastructure.Commons.DataContexts;
using Newtonsoft.Json.Linq;

namespace Com.Coppel.SDPC.Infrastructure.ApiClients;

public class ServiceApiAsignacionDeLinea : ServiceUtils, IServiceApiAsignacionDeLinea
{
	private readonly string _assetsPath;

	public ServiceApiAsignacionDeLinea() : base(new()) 
	{ 
		_assetsPath = $"{Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)!.Replace("file:\\", "")!}\\Assets\\";
	}

	public ApiResultType GetData(string token, PuntoDeConsumoVM puntoDeConsumo)
	{
		jwtToken = token;

		try
		{
			dynamic response = FetchStringJsonFromGetAsync($"{puntoDeConsumo.RutaServicio}", string.Empty, new CancellationToken())
			.ConfigureAwait(false)
			.GetAwaiter()
			.GetResult();


			if (response == null)
			{
				return ApiResultType.NO_DATA;
			}

			JObject json = (JObject)response;
			JEnumerable<JToken> data = json.SelectToken("data")!.Children();
			JArray tiposLineaJson = [];
			DateTime fechaArranque = DateTime.Today;

			foreach (var item in data)
			{
				if (item.SelectToken("tipoLinea") != null)
				{
					tiposLineaJson.Add(item);
				}
				else
				{
					fechaArranque = GetDate(item!.SelectToken("fechaArranque")!.Value<string>()!);
				}
			}

			List<CtlAsignacionDeLinea> entities = MapItems(tiposLineaJson, fechaArranque);

			if (Validate(fechaArranque))
			{
				_catalogosDbContext.CtlAsignacionDeLineas.AddRange(entities);
				_catalogosDbContext.SaveChanges();
				return ApiResultType.SUCCESS;
			}
			else
			{
				return ApiResultType.ALREADY_SAVED;
			}
		}
		catch (Microsoft.Data.SqlClient.SqlException)
		{
			return ApiResultType.TABLE_NOT_FOUND;
		}
		catch (Exception)
		{
			return ApiResultType.URI_NOT_FOUND;
		}
	}

	private static List<CtlAsignacionDeLinea> MapItems(JArray tiposLineaJson, DateTime fechaArranque)
	{
		List<CtlAsignacionDeLinea> result = [];
		foreach (var item in tiposLineaJson)
		{
			TipoLineaVM type = new()
			{
				idu_lineadecredito = short.Parse(item.SelectToken("idu_lineadecredito")!.ToString()),
				tipoLinea = item.SelectToken("tipoLinea")!.ToString(),
				perfiles = []
			};

			foreach (var subItem in item.SelectToken("perfiles")!.Children())
			{
				result.Add(new()
				{
					FechaAlta = DateTime.Today,
					Estatus = fechaArranque.Year == 1900 ? (int)EstatusType.Finalizado : (int)EstatusType.PorActualizar,
					TipoLinea = type.tipoLinea,
					IduLineadecredito = (short)type.idu_lineadecredito,
					IduPerfil = short.Parse(subItem.SelectToken("idu_perfil")!.ToString()),
					Rango = short.Parse(subItem.SelectToken("rango")!.ToString()),
					RangoMax = float.Parse(subItem.SelectToken("rangoMax")!.ToString()),
					RangoMin = float.Parse(subItem.SelectToken("rangoMin")!.ToString()),
					Valor = float.Parse(subItem.SelectToken("valor")!.ToString()),
					FechaArranque = fechaArranque
				});
			}
		}

		return result;
	}

	private static DateTime GetDate(string jsonValue)
	{
		string[] dateParts = jsonValue.Split("-");

		return new DateTime(int.Parse(dateParts[2]), int.Parse(dateParts[1]), int.Parse(dateParts[0]), 0, 0, 0, DateTimeKind.Local);
	}

	private bool Validate(DateTime fechaArranque)
	{
		var existRecord = _catalogosDbContext.CtlAsignacionDeLineas.FirstOrDefault(i =>
						i.FechaArranque.Year == fechaArranque.Year &&
						i.FechaArranque.Month == fechaArranque.Month &&
						i.FechaArranque.Day == fechaArranque.Day);

		if (existRecord == null)
		{
			return true;
		}

		return false;
	}
}
