namespace Com.Coppel.SDPC.Application.Models.Services;

public class ProcessEventVM
{
	public int IdFuncionalidad { get; set; }
	
	public string NomFuncionalidad { get; set; } = string.Empty;
	
	public string RutaServicio { get; set; } = string.Empty;
	
	public string NomTbDestino { get; set; } = string.Empty;
	
	public bool Flag { get; set; } = true;
	
	public bool AllowAfter20 { get; set; } = true;

	public bool Success { get; set; } = false;
}
