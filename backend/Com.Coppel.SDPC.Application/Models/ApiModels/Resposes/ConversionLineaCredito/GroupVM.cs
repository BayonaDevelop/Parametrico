namespace Com.Coppel.SDPC.Application.ViewModels.ApiModels.Resposes.ConversionLineaCredito;

public class GroupVM
{
  public int porcentajePerfil { get; set; }
  public int edadMinimaPerfil { get; set; }
  public int edadMaximaPerfil { get; set; }
  public decimal topeMinimoPerfil { get; set; }
  public decimal topeMaximoPerfil { get; set; }
  public string fechArranque { get; set; } = string.Empty;
}
