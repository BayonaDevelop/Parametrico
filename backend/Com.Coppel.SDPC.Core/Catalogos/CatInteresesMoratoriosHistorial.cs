using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatInteresesMoratoriosHistorial
{
  public DateTime FechaAlta { get; set; }
  public int BaseDatosOrigen { get; set; }
  public short DiasVencidos { get; set; }
  public int? DiasVencidosAcumulados { get; set; }
  public string? PorcentajeInteresMoratorio { get; set; }

  public virtual CatBasesDeDato BaseDatosOrigenNavigation { get; set; } = null!;
}
