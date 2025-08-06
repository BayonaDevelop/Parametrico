using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.TasaInteresPrestamosDia;

public class CifraDeControlTasaInteresPrestamosDiaVM
{
    [DataMember(Name = "Dias Vencidos")]
    public string DiasVencidos { get; set; } = string.Empty;

    [DataMember(Name = "Valor Antes           PorcentajeInteresMoratorio")]
    public string ValorAntesPorcentajeInteresMoratorio { set; get; } = string.Empty;

    [DataMember(Name = "Valor Después           PorcentajeInteresMoratorio")]
    public string ValorDespuesPorcentajeInteresMoratorio { get; set; } = string.Empty;
  
  [DataMember(Name = "Valor ADP")]
  public string ValorADP { get; set; } = string.Empty;

}
