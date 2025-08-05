using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlValidacionesLrc
{
	public int Id { get; set; }

	public DateTime FechaAlta { get; set; }

	public DateTime? FechaActualizacion { get; set; }

	public int Estatus { get; set; }

	public int PrcPerfil1 { get; set; }

	public int PrcPerfil2 { get; set; }

	public int PrcCalcularCsa { get; set; }

	public int NumMesesCalcularLrc { get; set; }

	public string Puntualidad { get; set; } = null!;

	public int ImporteVencido { get; set; }

	public int NumMesesMinimoPrimeraCompra { get; set; }

	public int TopeEdadMaxima { get; set; }

	public DateTime FechaArranque { get; set; }

	public virtual CtlEstatusParametrico EstatusNavigation { get; set; } = null!;
}
