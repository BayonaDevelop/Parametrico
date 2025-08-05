using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlFactoresSaturacionCartera
{
  public int IdFacSaturacion { get; set; }
  public DateTime FechaAlta { get; set; }
  public DateTime? FechaActualizacion { get; set; }
  public string Cartera { get; set; } = null!;
  public int Estatus { get; set; }
  public int Plazo { get; set; }
  public decimal FactorNormal { get; set; }
  public decimal FactorEspecial { get; set; }
  public decimal FactorInicial { get; set; }
  public decimal FactorMinima { get; set; }
  public DateTime FechaArranque { get; set; }

  public virtual CtlEstatusParametrico EstatusNavigation { get; set; } = null!;
}
