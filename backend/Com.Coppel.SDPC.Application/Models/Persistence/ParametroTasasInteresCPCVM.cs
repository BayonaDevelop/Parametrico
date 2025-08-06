namespace Com.Coppel.SDPC.Application.Models.Persistence;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "We need to break PascalCase")]
public class ParametroTasasInteresCPCVM
{
	public int Ciudad { get; set; }

	public string Articulo { get; set; } = string.Empty;

	public int Plazo { get; set; }

	public double TasaDeInteres { get; set; }

	public DateTime FechaArranque { get; set; }
}
