using System;

namespace Com.Coppel.SDPC.Core.Cat;

public partial class CatBonificacionesPrestamo
{
	public short NumDiasTranscurridos { get; set; }
	public decimal PrcBonificacion { get; set; }
	public decimal PrcBonificacionNueva { get; set; }
	public short? NumPlazo { get; set; }
	public DateTime? FecCorte { get; set; }
	public DateTime FecMovimiento { get; set; }
}
