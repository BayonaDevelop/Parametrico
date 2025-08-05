using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlInteresMoratorioDcp
{
  public int IdInteresDcp { get; set; }
  public DateTime FechaAlta { get; set; }
  public DateTime? FechaActualizacion { get; set; }
  public DateTime? FechaActualizacionCl { get; set; }
  public int Estatus { get; set; }
  public int EstatusCl { get; set; }
  public int DiasTranscurridos { get; set; }
  public decimal TasaMoratoria { get; set; }
  public DateTime Fechains { get; set; }
  public string Usermodifico { get; set; } = null!;

  public virtual CtlEstatusParametrico EstatusNavigation { get; set; } = null!;
}
