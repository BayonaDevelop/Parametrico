namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.ConversionLineasCredito;

public class ValoresVM
{
	public int prcCalcularCSA { get; set; }
	public int numMesesCalcularLRC { get; set; }
	public string Puntualidad { get; set; } = string.Empty;
	public int importeVencido { get; set; }
	public int numMesesMinimoPrimeraCompra { get; set; }
	public int topeEdadMaxima { get; set; }
}
