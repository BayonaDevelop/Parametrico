using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatFactorportipoproductoHistorial
{
  public DateTime FechaAlta { get; set; }
  public int BaseDatosOrigen { get; set; }
  public int IduControl { get; set; }
  public short NumTipoproducto { get; set; }
  public string DesTipoproducto { get; set; } = null!;
  public short NumPlazo { get; set; }
  public short NumTipolineacredito { get; set; }
  public double NumFactor { get; set; }
  public DateTime? FecMovto { get; set; }

  public virtual CatBasesDeDato BaseDatosOrigenNavigation { get; set; } = null!;
}
