using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports;

public class CifraDeControlDecrementoLineaCreditoVM
{
	[DataMember(Name = "Funcionalidad")] ///Tipo
	public string Tipo { get; set; } = string.Empty;

	[DataMember(Name = "Fecha de Arranque")] ///Fecha de Actualización de Cambio
	public string FechaAlta { set; get; } = string.Empty;

	[DataMember(Name = "Fecha de Actualización de Cambio")] ///Fecha de Arranque
	public string FechaCambio { get; set; } = string.Empty;

	[DataMember(Name = "Empleado que Realiza el Cambio")]
	public string Empleado { get; } = "Servicio de Administrador de Catálogo";

	[DataMember(Name = "Campo Afectado")] ///Campo Afectado
	public string CampoAfectado { get; set; } = string.Empty;

	[DataMember(Name = "Valor Antes del Cambio")] ///Valor Antes del Cambio
	public string ValorPrevio { get; set; } = string.Empty;

	[DataMember(Name = "Valor Después del Cambio")] ///Valor Después del Cambio
	public string ValorNuevo { get; set; } = string.Empty;

	[DataMember(Name = "Valor de *ADP")] ///"Valor de *ADP
	public string ValorADP { get; set; } = string.Empty;
}
