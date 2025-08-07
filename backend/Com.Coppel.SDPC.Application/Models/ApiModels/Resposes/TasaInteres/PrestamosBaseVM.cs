namespace Com.Coppel.SDPC.Application.ViewModels.ApiModels.Resposes.TasaInteres;

public class PrestamosBaseVM
{
  public string fechaarranque { get; set; } = string.Empty;

  public List<TasaInteresPrestamoVM> data { get; set; } = new();
}
