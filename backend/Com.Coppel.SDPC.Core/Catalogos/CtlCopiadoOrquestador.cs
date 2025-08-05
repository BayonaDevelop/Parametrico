using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlCopiadoOrquestador
{
	public int Id { get; set; }

	public DateTime FechaAlta { get; set; }

	public DateTime FechaActualizacion { get; set; }

	public string NombreProceso { get; set; } = null!;

	public string NombreTabla { get; set; } = null!;

	public int Flag { get; set; }
}
