using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatCiudadTipoRiesgo
{

	public DateTime FechaActualizacion { get; set; }
	public DateTime FechaAlta { get; set; }

	public int IdCiudad { get; set; }

	public short IdTipoRiesgo { get; set; }
}
