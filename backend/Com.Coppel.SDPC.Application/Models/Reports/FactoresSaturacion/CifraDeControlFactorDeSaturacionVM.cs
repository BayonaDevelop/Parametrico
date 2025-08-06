using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.FactoresSaturacion;

public class CifraDeControlFactorDeSaturacionVM
{
  [DataMember(Name = "Campo Afectado")] ///Campo Afectado
  public string CampoAfectado { get; set; } = string.Empty;

  [DataMember(Name = "Cartera")]
  public string Cartera { get; set; } = string.Empty;

  [DataMember(Name = "Plazo")]
  public string Plazo { get; set; } = string.Empty;

  [DataMember(Name = "Tipo Línea de Crédito")]
  public string TipoLineaCredito { get; set; } = string.Empty;

  [DataMember(Name = "Valor Antes prc_factor")] ///Valor Antes del Cambio
  public string ValorPrevio { get; set; } = string.Empty;

  [DataMember(Name = "Valor Despues prc_factor")] ///Valor Después del Cambio
  public string ValorNuevo { get; set; } = string.Empty;

  [DataMember(Name = "Valor ADP")] ///"Valor de *ADP
  public string ValorADP { get; set; } = string.Empty;
}