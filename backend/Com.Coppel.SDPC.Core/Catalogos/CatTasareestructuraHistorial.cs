using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatTasareestructuraHistorial
{
  public DateTime FechaAlta { get; set; }
  public int BaseDatosOrigen { get; set; }
  public int IduProducto { get; set; }
  public int NumPlazo { get; set; }
  public int NumTasa { get; set; }

  public virtual CatBasesDeDato BaseDatosOrigenNavigation { get; set; } = null!;
}
