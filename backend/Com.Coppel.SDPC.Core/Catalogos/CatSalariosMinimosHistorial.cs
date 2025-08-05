using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatSalariosMinimosHistorial
{
  public DateTime FechaAlta { get; set; }
  public int BaseDatosOrigen { get; set; }
  public int? SalarioZonaA { get; set; }
  public int? SalarioZonaB { get; set; }
  public int? SalarioZonaC { get; set; }
  public DateTime FechaSalario { get; set; }
  public int? Keyx { get; set; }

  public virtual CatBasesDeDato BaseDatosOrigenNavigation { get; set; } = null!;
}
