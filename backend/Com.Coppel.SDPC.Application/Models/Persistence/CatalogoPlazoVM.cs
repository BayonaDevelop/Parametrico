namespace Com.Coppel.SDPC.Application.Models.Persistence;

public class CatalogoPlazoVM
{
	public int IdPlazo { get; set; }

	public int? IduCartera { get; set; }

	public short IduCuenta { get; set; }

	public short Plazo { get; set; }

	public string TipoTransaccion { get; set; } = string.Empty;

	public string Cartera { get; set; } = string.Empty;
}
