namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatTiposlineacredito
{
  public int NumTipolineacredito { get; set; }
  public string DesTipolineacredito { get; set; } = null!;
  public int ImpMinimolineacreditoreal { get; set; }
  public int ImpMaximolineacreditoreal { get; set; }
  public System.DateTime FecRegistro { get; set; }
}
