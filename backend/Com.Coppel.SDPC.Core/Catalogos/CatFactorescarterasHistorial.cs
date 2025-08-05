using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatFactorescarterasHistorial
{
  public DateTime FechaAlta { get; set; }
  public int BaseDatosOrigen { get; set; }
  public byte IduCartera { get; set; }
  public byte ClvPlazo { get; set; }
  public decimal PrcFactor { get; set; }
  public byte ClvLineacreditoespecial { get; set; }

  public virtual CatBasesDeDato BaseDatosOrigenNavigation { get; set; } = null!;
}
