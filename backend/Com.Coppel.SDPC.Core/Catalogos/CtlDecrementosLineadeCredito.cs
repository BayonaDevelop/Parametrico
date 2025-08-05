using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlDecrementosLineadeCredito
{
  public int IdDecremento { get; set; }
  public DateTime FechaAlta { get; set; }
  public DateTime? FechaActualizacion { get; set; }
  public int Estatus { get; set; }
  public int NumTipoLinea { get; set; }
  public string NomTipoLinea { get; set; } = null!;
  public decimal NumTopeLineaMinima { get; set; }
  public string NomPuntualidad { get; set; } = null!;
  public decimal NumEficienciaHis { get; set; }
  public decimal NumDecremento { get; set; }
  public DateTime FechaArranque { get; set; }

  public virtual CtlEstatusParametrico EstatusNavigation { get; set; } = null!;
}
