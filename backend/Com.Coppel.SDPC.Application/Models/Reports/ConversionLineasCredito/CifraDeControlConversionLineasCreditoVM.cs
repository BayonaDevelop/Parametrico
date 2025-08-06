using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.ConversionLineasCredito;

[DataContract]
public class CifraDeControlConversionLineasCreditoVM
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
	public string ValorPrevio { get; set; } = string.Empty;

	[DataMember(Name = "Valor Después del Cambio")]
	public string ValorNuevo { get; set; } = string.Empty;

	[DataMember(Name = "Valor ADP")] 
	public string ValorADP { get; set; } = string.Empty;


}
