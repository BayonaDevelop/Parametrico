namespace Com.Coppel.SDPC.Application.ViewModels.ApiModels.Resposes.ConversionLineaCredito;

public class CjsonVM
{
  public List<ValorVM> valores { get; set; } = new();

  public List<KeyValuePair<string, List<GroupVM>>> igrupo { get; set; } = new();
}
