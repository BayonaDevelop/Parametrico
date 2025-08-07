namespace Com.Coppel.SDPC.Application.ViewModels.ApiModels.Resposes.TasaInteres;

public class TasaInteresMueblesVM
{
  public int ciudad { get; set; }
  public string articulo { get; set; } = string.Empty;
  public string fechaArranque { get; set; } = string.Empty;
  public List<TasaInteresVM> plazos { get; set; } = new();
}
