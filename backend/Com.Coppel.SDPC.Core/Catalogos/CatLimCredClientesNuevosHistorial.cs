using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatLimCredClientesNuevosHistorial
{
  public DateTime FechaAlta { get; set; }
  public int BaseDatosOrigen { get; set; }
  public byte LimiteDeCredito { get; set; }
  public byte? NumeroSalariosMinimos { get; set; }
  public int? CreditoAutorizado { get; set; }
  public byte? NumeroSalariosCtesNvos { get; set; }

  public virtual CatBasesDeDato BaseDatosOrigenNavigation { get; set; } = null!;
}
