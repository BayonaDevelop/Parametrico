using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlConversionLineadeCredito
{
  public int IdConversion { get; set; }
  public DateTime FechaAlta { get; set; }
  public DateTime? FechaActualizacion { get; set; }
  public int Estatus { get; set; }
  public decimal PorcentajecCalculoCsa { get; set; }
  public int NumMesesCalcularLcr { get; set; }
  public string? Puntualidad { get; set; }
  public decimal ImporteVencido { get; set; }
  public int NumMesMinDesdePrimerCompra { get; set; }
  public int TopeEdadMax { get; set; }
  public string NomGrupo { get; set; } = null!;
  public decimal PorcentajePerfil { get; set; }
  public int EdadMinPerfil { get; set; }
  public int EdadMaxPerfil { get; set; }
  public decimal TopeMinPerfil { get; set; }
  public decimal TopeMaxPerfil { get; set; }
  public DateTime FechaVigenteDesde { get; set; }

  public virtual CtlEstatusParametrico EstatusNavigation { get; set; } = null!;
}
