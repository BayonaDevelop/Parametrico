using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.TasaInteresPrestamosDia;

public class CatInteresesMoratoriosVM
{
    [DataMember(Name = "DiasVencidos")]
    public string DiasVencidos { get; set; } = string.Empty;

    [DataMember(Name = "DiasVencidosAcumulados")]
    public string DiasVencidosAcumulados { set; get; } = string.Empty;

    [DataMember(Name = "PorcentajeInteresMoratorio")]
    public string PorcentajeInteresMoratorio { get; set; } = string.Empty;

}
