namespace Com.Coppel.SDPC.Core.Carteras;

public partial class CatTerminacionesgrupoasignacionLc
{
	public short IduGrupotestigo { get; set; }

	public string DesGrupotestigo { get; set; } = null!;

	public string NumTerminacioncliente { get; set; } = string.Empty;

	public short NumProceso { get; set; }

	public System.DateTime FecMovimiento { get; set; }
}
