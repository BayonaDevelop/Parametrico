using System.Collections.Generic;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlCatalogoPlazo
{
  public CtlCatalogoPlazo()
  {
    CtlBonificacionCarteras = [];
  }

  public int IdPlazo { get; set; }
  public int? IduCartera { get; set; }
  public short IduCuenta { get; set; }
  public short Plazo { get; set; }
  public string TipoTransaccion { get; set; } = null!;
  public string? Cartera { get; set; }

  public virtual ICollection<CtlBonificacionCartera> CtlBonificacionCarteras { get; set; }
}
