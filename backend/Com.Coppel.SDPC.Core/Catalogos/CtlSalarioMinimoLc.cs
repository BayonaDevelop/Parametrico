using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlSalarioMinimoLc
{
  public int IdSalario { get; set; }
  public DateTime FechaAlta { get; set; }
  public DateTime? FechaActualizacion { get; set; }
  public int Estatus { get; set; }
  public string NombreSalario { get; set; } = null!;
  public decimal ValorSalario { get; set; }
  public DateTime FechaArranque { get; set; }

  public virtual CtlEstatusParametrico EstatusNavigation { get; set; } = null!;
}
