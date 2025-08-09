namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.ConversionLineaCredito;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class ValorVM
{
	public int porcentajeParaCalcularCSA { get; set; }
	public int numeroMesesCalcularLRC { get; set; }
	public string puntualidad { get; set; } = string.Empty;
	public int importeDeVencido { get; set; }
	public int numeroMesesMinimoDesdeLaPrimeraCompra { get; set; }
	public int topeEdadMaxima { get; set; }
}
