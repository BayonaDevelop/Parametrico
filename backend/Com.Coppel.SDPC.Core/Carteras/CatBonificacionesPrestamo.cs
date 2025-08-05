using System;

namespace Com.Coppel.SDPC.Core.Carteras;

/// <summary>
/// Catalogo de Bonificaciones de prestamos
/// </summary>
public partial class CatBonificacionesPrestamo
{
	/// <summary>
	/// Numero de dias transcurridos
	/// </summary>
	public short NumDiasTranscurridos { get; set; }
	/// <summary>
	/// Porcentaje de Bonificacion
	/// </summary>
	public decimal PrcBonificacion { get; set; }
	/// <summary>
	/// Porcentaje de Bonificacion Nueva
	/// </summary>
	public decimal PrcBonificacionNueva { get; set; }
	/// <summary>
	/// Numero de Plazos
	/// </summary>
	public short? NumPlazo { get; set; }
	/// <summary>
	/// Fecha corte
	/// </summary>
	public DateTime? FecCorte { get; set; }
	/// <summary>
	/// Fecha en la que se realizo el movimiento
	/// </summary>
	public DateTime FecMovimiento { get; set; }
}
