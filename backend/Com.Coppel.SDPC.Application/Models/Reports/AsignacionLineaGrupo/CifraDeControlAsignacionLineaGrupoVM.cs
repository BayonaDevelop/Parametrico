using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.AsignacionLineaGrupo;

[DataContract]
public class CifraDeControlAsignacionLineaGrupoVM
{
  [DataMember(Name = "Funcionalidad")]
  public string Funcionalidad { get; set; } = string.Empty;

  [DataMember(Name = "Fecha de Arranque")]
  public DateTime FechaArranque { get; set; }

  [DataMember(Name = "Fecha de Actualización")]
  public DateTime FechaActualizacion { set; get; }

  [DataMember(Name = "Empleado que realiza el cambio")]
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
