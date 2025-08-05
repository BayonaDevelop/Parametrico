using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatCrbonificacionesHistorialCl
{
	public DateTime FechaAlta { get; set; }
	public int BaseDatosOrigen { get; set; }
	public short IduCuenta { get; set; }
	public short NumTiempotranscurrido { get; set; }
	public short NumPorcentaje { get; set; }
	public short NumPlazo { get; set; }
	public DateTime? FecMov { get; set; }
	public int SecKeyx { get; set; }
	public short NumFactorajuste { get; set; }
	public string? OpcTiempotranscurrido { get; set; }
	public string? OpcBonificacion { get; set; }
}
