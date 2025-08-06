using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.TasasDeInteres;

[DataContract]
public class CifraDeControlTasasDeInteresMoratorioVM
{
  [DataMember(Name = "NumeroCiudad")]
  public string NumeroCiudad { get; set; } = string.Empty;

  [DataMember(Name = "TipoRiesgo")]
  public string Tiporiesgo { get; set; } = string.Empty;

  [DataMember(Name = "Valor Antes del Cambio")]
  public string ValorPrevio { get; set; } = string.Empty;

  [DataMember(Name = "Valor Después del Cambio")]
  public string ValorNuevo { get; set; } = string.Empty;

  [DataMember(Name = "Valor ADP")]
  public string ValorADP { get; set; } = string.Empty;

}