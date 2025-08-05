using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatLogicaAsignacionQuebrantadaHistorial
{
  public DateTime FechaAlta { get; set; }
  public int BaseDatosOrigen { get; set; }
  public string ClvPuntualidad { get; set; } = null!;
  public string ClvPuntualidadactiva { get; set; } = null!;
  public short NumAbonovencidosInicio { get; set; }
  public short NumAbonovencidosFin { get; set; }
  public decimal PrcInteres { get; set; }
  public byte ClvDespacho { get; set; }
  public int? NumPlazo { get; set; }
  public double? NumTasa { get; set; }
  public int? FlagVentaEspecial { get; set; }

  public virtual CatBasesDeDato BaseDatosOrigenNavigation { get; set; } = null!;
}
