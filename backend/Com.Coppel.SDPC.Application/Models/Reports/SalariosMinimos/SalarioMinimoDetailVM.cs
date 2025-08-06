using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.SalariosMinimos;

[DataContract]
public class SalarioMinimoDetailVM
{
	[DataMember(Name = "Tipo")]
	public string Tipo { get; set; } = string.Empty;

	[DataMember(Name = "Fecha de Arranque")]
	public DateTime FechaCambio { get; set; }

	[DataMember(Name = "Fecha de Actualización de Cambio")]
	public DateTime FechaAlta { set; get; }

	[DataMember(Name = "Empleado que realiza el cambio")]
	public string Empleado { get; } = "Servicio de Administrador de Catálogo";

	[DataMember(Name = "Campo Afectado")]
	public string Campo { get; set; } = string.Empty;

	[DataMember(Name = "Valor Antes del Cambio")]
	public decimal ValorPrevio { get; set; }

	[DataMember(Name = "Valor Después del Cambio")]
	public decimal ValorNuevo { get; set; }
}
