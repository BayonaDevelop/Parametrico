namespace Com.Coppel.SDPC.Application.ViewModels.ApiModels.Resposes.ConversionLineaCredito;

public class ValorVM
{
  public int porcentajeParaCalcularCSA { get; set; }
  public int numeroMesesCalcularLRC { get; set; }
  public string puntualidad { get; set; } = string.Empty;
  public int importeDeVencido { get; set; }
  public int numeroMesesMinimoDesdeLaPrimeraCompra { get; set; }
  public int topeEdadMaxima { get; set; }
}
