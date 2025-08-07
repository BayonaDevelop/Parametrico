namespace Com.Coppel.SDPC.Application.ViewModels.ApiModels.Resposes;

public class FactorDeSaturacionVM
{
  public int plazo { get; set; }
  public decimal factornormal { get; set; }
  public decimal factorespecial { get; set; }
  public decimal factorinicial { get; set; }
  public decimal factorminima { get; set; }
  public string fechaArranque { get; set; } = string.Empty;
}
