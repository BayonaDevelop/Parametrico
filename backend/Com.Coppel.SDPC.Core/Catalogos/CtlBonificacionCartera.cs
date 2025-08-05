using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlBonificacionCartera
{
  public int IdBonificacion { get; set; }
  public DateTime FechaAlta { get; set; }
  public DateTime FechaArranque { get; set; }
  public DateTime? FechaActualizacion { get; set; }
  public DateTime? FechaActualizacionCl { get; set; }
  public int Estatus { get; set; }
  public int EstatusCl { get; set; }
  public int? IdPlazo { get; set; }
  public short NumDiastranscurridos { get; set; }
  public decimal NumPorcentajebonificacion { get; set; }

  public virtual CtlEstatusParametrico EstatusClNavigation { get; set; } = null!;
  public virtual CtlEstatusParametrico EstatusNavigation { get; set; } = null!;
  public virtual CtlCatalogoPlazo? IdPlazoNavigation { get; set; }
}
