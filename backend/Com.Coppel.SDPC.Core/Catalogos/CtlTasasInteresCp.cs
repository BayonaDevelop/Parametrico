using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlTasasInteresCp
{
  public int IdTasas { get; set; }
  public DateTime FechaAlta { get; set; }
  public DateTime? FechaActualizacion { get; set; }
  public DateTime? FechaActualizacionCl { get; set; }
  public string Cartera { get; set; } = null!;
  public int Estatus { get; set; }
  public int EstatusCl { get; set; }
  public int Plazo { get; set; }
  public decimal TasaDeInteres { get; set; }
  public DateTime FechaArranque { get; set; }

  public virtual CtlEstatusParametrico EstatusNavigation { get; set; } = null!;
}
