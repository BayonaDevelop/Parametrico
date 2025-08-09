using Com.Coppel.SDPC.Application.Models.ApiModels.Resposes;
using Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.TasaInteres;
using Com.Coppel.SDPC.Application.Models.Persistence;
using Com.Coppel.SDPC.Core.Catalogos;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Globalization;

namespace Com.Coppel.SDPC.Infrastructure.Commons.ApiBase;

public static class DataFix
{
	private const string DATE_FORMAT = "dd-MM-yyyy";

	public static List<CtlFactoresSaturacionCartera> ConvertFactoresDeSaturacionToEntities(List<FactorDeSaturacionVM> result, PuntoDeConsumoVM puntoDeConsumo, string cartera)
	{
		try
		{
			List<CtlFactoresSaturacionCartera> factoresSaturacion = [];
			foreach (FactorDeSaturacionVM item in result)
			{
				CtlFactoresSaturacionCartera regFactore = new()
				{
					FechaAlta = System.DateTime.Now,
					FechaActualizacion = null,
					Cartera = cartera,
					Estatus = 1,
					Plazo = item.plazo,
					FactorNormal = item.factornormal,
					FactorEspecial = item.factorespecial,
					FactorInicial = item.factorinicial,
					FactorMinima = item.factorminima,
					FechaArranque = DateTime.ParseExact(item.fechaArranque, DATE_FORMAT, CultureInfo.InvariantCulture)
				};
				factoresSaturacion.Add(regFactore);

			}
			return factoresSaturacion;
		}
		catch (Exception)
		{
			Debug.WriteLine($"{puntoDeConsumo.NomFuncionalidad}/{cartera}. No se obtuvieron datos del API");
			throw;
		}
	}

	public static CtlSalarioMinimoCp ConvertSalarioMinimoCpToEntity(SalarioMinimoVM result)
	{
		try
		{
			DateTime fechaActual = DateTime.Now;

			return new CtlSalarioMinimoCp
			{
				FechaAlta = fechaActual,
				FechaActualizacion = null,
				Estatus = 1,
				EstatusCl = 1,
				NombreSalario = result.nombreSalario,
				ValorSalario = result.valorSalario,
				FechaArranque = DateTime.ParseExact(result.fechaArranque, DATE_FORMAT, CultureInfo.InvariantCulture)
			};
		}
		catch (Exception)
		{
			Debug.WriteLine("SalarioMinimoVM. Error al convertir datos.");
			throw;
		}
	}

	public static CtlSalarioMinimoLc ConvertSalarioMinimoLcToEntity(SalarioMinimoVM result)
	{
		try
		{
			DateTime fechaActual = DateTime.Now;

			return new CtlSalarioMinimoLc
			{
				FechaAlta = fechaActual,
				FechaActualizacion = null,
				Estatus = 1,
				NombreSalario = result.nombreSalario,
				ValorSalario = result.valorSalario,
				FechaArranque = DateTime.ParseExact(result.fechaArranque, DATE_FORMAT, CultureInfo.InvariantCulture)
			};
		}
		catch (Exception)
		{
			Debug.WriteLine("SalarioMinimoVM. Error al convertir datos.");
			throw;
		}
	}

	public static List<CtlTasasInteresCp> ConvertTdiToEntity(List<TasaInteresVM> data, PuntoDeConsumoVM puntoDeConsumo, string cartera)
	{
		try
		{
			DateTime fechaActual = DateTime.Now;
			List<CtlTasasInteresCp> result = [];

			foreach (TasaInteresVM item in data)
			{
				result.Add(
					new CtlTasasInteresCp
					{
						FechaAlta = fechaActual,
						FechaActualizacion = null,
						Cartera = cartera,
						Estatus = 1,
						Plazo = item.plazo,
						TasaDeInteres = item.tasaDeInteres,
						FechaArranque = DateTime.ParseExact(item.fechaArranque, DATE_FORMAT, CultureInfo.InvariantCulture)
					}
				);
			}

			return result;
		}
		catch (Exception)
		{
			Debug.WriteLine($"{puntoDeConsumo.NomFuncionalidad}/{cartera}. No se obtuvieron datos del API");
			throw;
		}
	}

	public static List<CtlInteresMoratorioDcp> ConvertTdimPorDiaToEntities(JArray response, PuntoDeConsumoVM puntoDeConsumo)
	{
		try
		{
			List<CtlInteresMoratorioDcp> conversionMoratorioDCP = [];

			List<TasaInteresMoratoriaPrestamosDiaVM> groupsList = [];

			JArray jsonResponse = response;
			foreach (JToken item in jsonResponse)
			{
				JToken[] values = [.. item["tasaInteresDiario"]!];

				foreach (JToken details in values)
				{
					List<JToken> detail = [.. details.Values()];

					groupsList.Add(new TasaInteresMoratoriaPrestamosDiaVM
					{
						diasTranscurridos = detail[0].Value<int>(),
						tasaMoratoria = detail[1].Value<decimal>(),
						fechaIns = item["fechains"]!.ToString(),
						userModifico = item["usermodifico"]!.ToString()
					});
				}
			}

			DateTime fechaActual = DateTime.Now;

			foreach (TasaInteresMoratoriaPrestamosDiaVM item in groupsList)
			{
				CtlInteresMoratorioDcp regConversionDCP = new()
				{
					FechaAlta = fechaActual,
					Estatus = 1,
					EstatusCl = 1,
					DiasTranscurridos = item.diasTranscurridos,
					TasaMoratoria = item.tasaMoratoria,
					Fechains = DateTime.ParseExact(item.fechaIns, DATE_FORMAT, CultureInfo.InvariantCulture),
					Usermodifico = item.userModifico,
				};
				conversionMoratorioDCP.Add(regConversionDCP);
			}

			return conversionMoratorioDCP;
		}
		catch (Exception)
		{
			Debug.WriteLine($"{puntoDeConsumo.NomFuncionalidad}. No se obtuvieron datos del API");
			throw;
		}
	}

	public static List<CtlTasasInteresCpc> ConvertTdiMueblesToEntities(List<TasaInteresMueblesVM> result, PuntoDeConsumoVM puntoDeConsumo)
	{
		try
		{
			List<CtlTasasInteresCpc> TasasInteresMuebles = [];
			foreach (TasaInteresMueblesVM item in result)
			{
				foreach (TasaInteresVM subitem in item.plazos)
				{
					DateTime fechaActual = DateTime.Now;

					CtlTasasInteresCpc regTasasInteresMuebles = new()
					{
						FechaAlta = fechaActual,
						FechaActualizacion = null,
						Cartera = "Muebles",
						Estatus = 1,
						Ciudad = item.ciudad,
						Articulo = item.articulo,
						Plazo = subitem.plazo,
						TasaDeInteres = subitem.tasaDeInteres,
						FechaArranque = DateTime.ParseExact(item.fechaArranque, DATE_FORMAT, CultureInfo.InvariantCulture)
					};
					TasasInteresMuebles.Add(regTasasInteresMuebles);
				}
			}
			return TasasInteresMuebles;
		}
		catch (Exception)
		{
			Debug.WriteLine($"{puntoDeConsumo.NomFuncionalidad}. No se obtuvieron datos del API");
			throw;
		}
	}

	public static List<CtlTasasInteresCp> ConvertTdiToEntities(List<TasaInteresVM> result, PuntoDeConsumoVM puntoDeConsumo, string cartera)
	{
		try
		{
			List<CtlTasasInteresCp> TasasInteres = [];
			foreach (TasaInteresVM item in result)
			{
				DateTime fechaActual = DateTime.Now;
				CtlTasasInteresCp regTasas = new()
				{
					FechaAlta = fechaActual,
					FechaActualizacion = null,
					Cartera = cartera,
					Estatus = 1,
					Plazo = item.plazo,
					TasaDeInteres = item.tasaDeInteres,
					FechaArranque = DateTime.ParseExact(item.fechaArranque, DATE_FORMAT, CultureInfo.InvariantCulture)
				};
				TasasInteres.Add(regTasas);
			}
			return TasasInteres;
		}
		catch (Exception)
		{
			Debug.WriteLine($"{puntoDeConsumo.NomFuncionalidad}/{cartera}. No se obtuvieron datos del API");
			throw;
		}
	}

	public static CtlInteresMoratorioporCartera ConvertTdimToEntity(TasaInteresMoratorioVM result, PuntoDeConsumoVM puntoDeConsumo, string cartera)
	{
		try
		{
			DateTime fechaActual = DateTime.Now;

			return new CtlInteresMoratorioporCartera
			{
				FechaAlta = fechaActual,
				Estatus = 1,
				EstatusCl = 1,
				TasaTipoCiudad1 = result.tasatipociudad1,
				TasaTipoCiudad2 = result.tasatipociudad2,
				FechaArranque = DateTime.ParseExact(result.fechaArranque, DATE_FORMAT, CultureInfo.InvariantCulture)
			};
		}
		catch (Exception)
		{
			Debug.WriteLine($"{puntoDeConsumo.NomFuncionalidad}/{cartera}, No se obtuvieron datos del API");
			throw;
		}
	}

	public static List<CtlTasaInteresCpprestamo> ConvertTdiPrestamosToEntities(List<TasaInteresPrestamoVM> result, PuntoDeConsumoVM puntoDeConsumo, string cartera)
	{
		try
		{
			List<CtlTasaInteresCpprestamo> tasaInteresPrestamos = [];
			foreach (TasaInteresPrestamoVM item in result)
			{
				DateTime fechaActual = DateTime.Now;
				CtlTasaInteresCpprestamo regTasasPrestamo = new()
				{
					FechaAlta = fechaActual,
					FechaActualizacion = null,
					Cartera = "Prestamos",
					Estatus = 1,
					Puntualidad = cartera,
					Grupo = item.grupo,
					PuntajeInicial = item.puntajeInicial,
					PuntajeFinal = item.puntajeFinal,
					Plazo = item.plazo,
					TasaDeInteres = item.tasaDeInteres,
					FechaArranque = DateTime.ParseExact(item.fechaArranque, DATE_FORMAT, CultureInfo.InvariantCulture)
				};
				tasaInteresPrestamos.Add(regTasasPrestamo);
			}
			return tasaInteresPrestamos;
		}
		catch (Exception)
		{
			Debug.WriteLine($"{puntoDeConsumo.NomFuncionalidad}/{cartera}. No se obtuvieron datos del API");
			throw;
		}
	}
}
