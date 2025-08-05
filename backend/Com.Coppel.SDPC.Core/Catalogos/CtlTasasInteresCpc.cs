using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlTasasInteresCpc
{
  public int IdTasasCpc { get; set; }
  public DateTime FechaAlta { get; set; }
  public DateTime? FechaActualizacion { get; set; }
  public string Cartera { get; set; } = null!;
  public int Estatus { get; set; }
  public int Ciudad { get; set; }
  public string Articulo { get; set; } = null!;
  public int Plazo { get; set; }
  public decimal TasaDeInteres { get; set; }
  public DateTime FechaArranque { get; set; }

  public virtual CtlEstatusParametrico EstatusNavigation { get; set; } = null!;
}
