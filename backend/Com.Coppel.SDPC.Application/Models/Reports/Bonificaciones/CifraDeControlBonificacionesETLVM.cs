using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.Bonificaciones;

[DataContract]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "We need to break PascalCase")]
public class CifraDeControlBonificacionesETLVM
{
	[DataMember(Name = "Tipo")]
	public string Tipo { get; set; } = string.Empty;

	[DataMember(Name = "Fecha de Actualización de Cambio")]
	public string FechaCambio { get; set; } = string.Empty;

	[DataMember(Name = "Fecha de Arranque")]
	public string FechaArranque { set; get; } = string.Empty;

	[DataMember(Name = "Empleado que Realiza el Cambio")]
	public string Empleado { get; } = "Servicio de Administrador de Catálogo";

	[DataMember(Name = "Nombre Tabla Afectada")]
	public string NombreTabla { get; set; } = string.Empty;

	[DataMember(Name = "Campo Afectado")]
	public string Campo { get; set; } = string.Empty;

	[DataMember(Name = "Cartera")]
	public string Cartera { get; set; } = string.Empty;

	[DataMember(Name = "Plazo")]
	public string Plazo { get; set; } = string.Empty;

	[DataMember(Name = "Número Dias Transcurridos")]
	public string NumeroDiasTranscurridos { get; set; } = string.Empty;

	[DataMember(Name = "Valor Antes del Cambio")]
	public string ValorPrevio { get; set; } = string.Empty;

	[DataMember(Name = "Valor Después del Cambio")]
	public string ValorNuevo { get; set; } = string.Empty;

	[DataMember(Name = "Valor de *ADP")]
	public string TipoTransaccion { get; set; } = string.Empty;


}
