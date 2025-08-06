using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Services;

[DataContract]
public class CifraDeControlVM
{
	[DataMember(Name = "Tipo")]
	public string Tipo { get; set; } = string.Empty;
	[DataMember(Name = "Fecha de Arranque")]
	public string FechaCambio { get; set; } = string.Empty;
	[DataMember(Name = "Fecha de Actualización de Cambio")]
	public string FechaAlta { set; get; } = string.Empty;
	[DataMember(Name = "Empleado que realiza el cambio")]
	public string Empleado { get; } = "Servicio de Administrador de Catálogo";
	[DataMember(Name = "Campo Afectado")]
	public string Campo { get; set; } = string.Empty;
	[DataMember(Name = "Valor Antes del Cambio")]
	public string ValorPrevio { get; set; } = string.Empty;
	[DataMember(Name = "Valor Después del Cambio")]
	public string ValorNuevo { get; set; } = string.Empty;
	[DataMember(Name = "Valor ADP")]
	public string ADP { get; set; } = string.Empty;
}
