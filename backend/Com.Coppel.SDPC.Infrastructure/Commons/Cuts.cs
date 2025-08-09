using Com.Coppel.SDPC.Application.Models.Services;
using Com.Coppel.SDPC.Core.Carteras;
using Com.Coppel.SDPC.Core.Catalogos;
using Com.Coppel.SDPC.Infrastructure.Commons.DataContexts;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;

namespace Com.Coppel.SDPC.Infrastructure.Commons;
public static class Cuts
{
	private static readonly ILogger _log = Log.Logger;
	private static readonly CarterasDbContext _carterasContext = new();
	private static readonly CatalogosDbContext _catalogosContext = new();

	public static bool CutAt20sCarteras(TestDatesVM testDates = null!)
	{
		try
		{
			DateTime startDate = DateTime.Now;
			DateTime cut = new(startDate.Year, startDate.Month, 20, 0, 0, 0, DateTimeKind.Local);

			if (!Utils.IsInProduction())
			{
				startDate = testDates.Today;
				cut = new(startDate.Year, startDate.Month, 20, 0, 0, 0, DateTimeKind.Local);
			}

			var MasterDate = _carterasContext.CatFechas.OrderByDescending(i => i.Fecha).FirstOrDefault(i => i.Fecha.Date >= cut.Date);
			if (MasterDate == null)
			{
				string message = string.Format(SystemMessages.VIGOR_CARTERA, startDate);
				_log.Warning(message);
			}

			return MasterDate != null;
		}
		catch (Exception)
		{
			Debug.WriteLine("Error al obtener un corte valido para [Carteras]");
			throw;
		}
	}

	public static bool CutAfter20Carteras(Type table, TestDatesVM testDates = null!)
	{
		CtlParametrosautenticacion parameter = _catalogosContext.CtlParametrosautenticacions.FirstOrDefault(i => i.NombreParametro!.CompareTo("DiasDespuesDel21") == 0)!;
		int daysAfter21 = parameter == null ? 0 : int.Parse(parameter!.ValorParametro!);
		DateTime today = DateTime.Today;
		DateTime startDate = new(DateTime.Now.Year, DateTime.Now.Month, 21, 0, 0, 0, DateTimeKind.Local);
		DateTime endDate = startDate.AddDays(daysAfter21);
		bool aux = false;

		if (!Utils.IsInProduction())
		{
			today = testDates.After20;
			startDate = new(today.Year, today.Month, 21, 0, 0, 0, DateTimeKind.Local);
			endDate = startDate.AddDays(daysAfter21);
		}

		int down = DateTime.Compare(today, startDate);
		int up = DateTime.Compare(today, endDate);


		var result = (
			down == 0 || down == 1) &&  /// >=
			(up == 0 || up == -1        /// <=
		);

		if (HasIntermediateRecordsForCutAfter21(table, testDates))
		{
			return false;
		}

		try
		{
			CatFecha catFecha = _carterasContext.CatFechas.OrderByDescending(i => i.Fecha).FirstOrDefault()!;
			aux = catFecha!.Fecha.Year == today.Year && catFecha!.Fecha.Month == today.Month && catFecha!.Fecha.Day > 19;
		}
		catch (Exception)
		{
			aux = false;
		}

		return result && aux;
	}

	public static bool CurrentDateIsInRange(TestDatesVM testDates)
	{
		CtlParametrosautenticacion parameter = _catalogosContext.CtlParametrosautenticacions.FirstOrDefault(i => i.NombreParametro!.CompareTo("DiasDespuesDel21") == 0)!;
		int daysAfter21 = parameter == null ? 0 : int.Parse(parameter!.ValorParametro!);
		DateTime today = DateTime.Today;
		DateTime startDate = new(DateTime.Now.Year, DateTime.Now.Month, 21, 0, 0, 0, DateTimeKind.Local);
		DateTime endDate = startDate.AddDays(daysAfter21);

		if (!Utils.IsInProduction())
		{
			today = testDates.After20;
			startDate = new(today.Year, today.Month, 21, 0, 0, 0, DateTimeKind.Local);
			endDate = startDate.AddDays(daysAfter21);
		}

		int down = DateTime.Compare(today, startDate);
		int up = DateTime.Compare(today, endDate);
		bool dateInRange = (
			down == 0 || down == 1) &&  /// >=
			(up == 0 || up == -1        /// <=
		);

		if (!dateInRange)
		{
			string message = string.Format(SystemMessages.PARAMETROS_FUERA_RANGO, startDate.Day, endDate.Day);
			_log.Warning(message);
		}

		return dateInRange;
	}

	private static bool HasIntermediateRecordsForCutAfter21(Type table, TestDatesVM testDates)
	{
		try
		{
			DateTime today = DateTime.Today;

			if (!Utils.IsInProduction())
			{
				today = testDates.After20;
			}

			DateTime previuosMont = new(today.Year, (today.Month - 1), 21, 0, 0, 0, DateTimeKind.Local);
			DateTime currentMont = new(today.Year, today.Month, 21, 0, 0, 0, DateTimeKind.Local);
			int rowsCounter = 0;

			string query = $"SELECT COUNT(fechaArranque) FROM {Utils.GetTableName(table)} WHERE CONVERT(DATE, fechaArranque) BETWEEN CONVERT(DATE, @previuosMont) AND CONVERT(DATE, @currentMont)";
			using (SqlConnection cn = new(_catalogosContext.Database.GetConnectionString()))
			{
				var parameter1 = $"{previuosMont.Year}-{(previuosMont.Month < 10 ? $"0{previuosMont.Month}" : previuosMont.Month)}-{(previuosMont.Day < 10 ? $"0{previuosMont.Day}" : previuosMont.Day)}";
				var parameter2 = $"{currentMont.Year}-{(currentMont.Month < 10 ? $"0{currentMont.Month}" : currentMont.Month)}-{(currentMont.Day < 10 ? $"0{currentMont.Day}" : currentMont.Day)}";
				rowsCounter = cn.ExecuteScalar<int>(query, new { previuosMont = parameter1, currentMont = parameter2 });
			}

			if (rowsCounter < 1)
			{
				_log.Warning(SystemMessages.PARAMETROS_SIN_CORTE);
			}

			return rowsCounter > 0;
		}
		catch (Exception exception)
		{
			_log.Warning(SystemMessages.PARAMETROS_SIN_CORTE, exception);
			return false;
		}
	}
}
