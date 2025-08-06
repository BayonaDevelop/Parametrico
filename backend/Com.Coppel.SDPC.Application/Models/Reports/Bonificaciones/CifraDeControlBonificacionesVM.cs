using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.Bonificaciones;

public class CifraDeControlBonificacionesVM
{
  [DataMember(Name = "Cartera")]
  public string Cartera { get; set; } = string.Empty;

  [DataMember(Name = "Plazo")]
  public string Plazo { get; set; } = string.Empty;

  [DataMember(Name = "Número Dias Transcurridos")]
  public string NumeroDiasTranscurridos { get; set; } = string.Empty;

  [DataMember(Name = "Valor Antes del Cambio")]
  public string ValorPrevio { get; set; } = string.Empty;

  [DataMember(Name = "Valor Después del Cambio")]
  public string ValorNuevo { get; set; } = string.Empty;

  [DataMember(Name = "Valor de ADP")]
  public string ValorADP { get; set; } = string.Empty;

}
