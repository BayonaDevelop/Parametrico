using Com.Coppel.SDPC.Infrastructure.Commons;

namespace Com.Coppel.SDPC.ServicioDatosParametricosCartera;

public static class Program
{

	private static HostConfiguration? _configuration;

	public static async Task Main(string[] args)
	{
		int idFuncionalidad = args.Length == 0 ? 0 : int.Parse(args[0]);
		_configuration = new HostConfiguration(idFuncionalidad);

		_configuration._log.Information(SystemMessages.SEPARADOR);
		_configuration._log.Information(SystemMessages.DAILY_HEADER);
		await ExecuteDaily();
	}

	private static async Task ExecuteDaily()
	{
		for (int i = 0; i < 3; i++)
		{
			if (_configuration!._dailyEvents.Count < _configuration._dailyEvents.Count(i => i.Success))
			{
				break;
			}

			int minute = 1000 * 60;
			string messaage = string.Format(SystemMessages.ITERATION, (i + 1));
			_configuration._log.Information(messaage);
			await _configuration.RunDailyEvents();
			await Task.Delay(minute * _configuration._minutsTowait);
		}
	}
}
