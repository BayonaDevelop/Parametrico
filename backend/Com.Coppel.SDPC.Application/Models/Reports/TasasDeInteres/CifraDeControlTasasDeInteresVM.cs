using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.TasasDeInteres;

public class CifraDeControlTasasDeInteresVM
{
	[DataMember(Name = "")]
	public int IduTipoTasa { get; set; }

	[DataMember(Name = "Tipo")]
	public string Tipo { get; set; } = string.Empty;

	[DataMember(Name = "Fecha de Actualización de Cambio")]
	public string FechaAlta { set; get; } = string.Empty;

	[DataMember(Name = "Fecha de Arranque")]
	public string FechaCambio { get; set; } = string.Empty;

	[DataMember(Name = "Empleado que Realiza el Cambio")]
	public string Empleado { get; } = "Servicio de Administrador de Catálogo";

	[DataMember(Name = "Nombre Tabla Afectada")]
	public string NombreTabla { get; set; } = string.Empty;

	[DataMember(Name = "Campo Afectado")]
	public string CampoAfectado { get; set; } = string.Empty;

	[DataMember(Name = "CARTERA")]
	public string Cartera { get; set; } = string.Empty;

	[DataMember(Name = "Artículo")]
	public string Articulo { get; set; } = string.Empty;

	[DataMember(Name = "PLAZO")]
	public string Plazo { get; set; } = string.Empty;

	[DataMember(Name = "Ciudad")]
	public string Ciudad { get; set; } = string.Empty;

	[DataMember(Name = "Valor Antes del Cambio")]
	public string ValorPrevio { get; set; } = string.Empty;

	[DataMember(Name = "Valor Después del Cambio")]
	public string ValorNuevo { get; set; } = string.Empty;

	[DataMember(Name = "Valor De *ADP")]
	public string ValorADP { get; set; } = string.Empty;
}
