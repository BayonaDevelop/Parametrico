using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.TasaInteresPrestamosDia;

public class CifraDeControlTasasDeInteresMoratorioPrestamoDiaVM
{
    [DataMember(Name = "Funcionalidad")]
    public string Funcionalidad { get; set; } = string.Empty;

    [DataMember(Name = "Empleado que Realiza el Cambio")]
    public string Empleado { get; } = "Servicio de Administrador de Catálogo";

    [DataMember(Name = "Campo Afectado")]
    public string Campo { get; set; } = string.Empty;

    [DataMember(Name = "Valor Antes del Cambio")]
    public string ValorPrevio { get; set; } = string.Empty;

    [DataMember(Name = "Valor Después del Cambio")]
    public string ValorNuevo { get; set; } = string.Empty;

    [DataMember(Name = "Valor ADP")]
    public string ValorADP { get; set; } = string.Empty;
}
