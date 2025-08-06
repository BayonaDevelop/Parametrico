namespace Com.Coppel.SDPC.Application.Models.Persistence;

public class PuntoDeConsumoVM
{
	public int IdFuncionalidad { get; set; }

	public bool Flag { get; set; } = true;

	public bool AllowAfter20 { get; set; } = true;

	public string NomFuncionalidad { get; set; } = string.Empty;

	public string RutaServicio { get; set; } = string.Empty;

	public string NomTbDestino { get; set; } = string.Empty;
}
