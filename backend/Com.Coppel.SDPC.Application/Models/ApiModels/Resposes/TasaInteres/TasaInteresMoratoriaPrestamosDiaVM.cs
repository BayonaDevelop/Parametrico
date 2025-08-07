namespace Com.Coppel.SDPC.Application.ViewModels.ApiModels.Resposes.TasaInteres;

public class TasaInteresMoratoriaPrestamosDiaVM
{
  public string titulo { get; set; } = string.Empty;
  public int diasTranscurridos { get; set; } = 0;
  public decimal tasaMoratoria { get; set; } = 0m;
  public string fechaIns { get; set; } = string.Empty;
  public string userModifico { get; set; } = string.Empty;
}
