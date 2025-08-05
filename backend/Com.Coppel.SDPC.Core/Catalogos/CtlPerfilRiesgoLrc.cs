using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlPerfilRiesgoLrc
{
	public int IdGrupo { get; set; }

	public DateTime FechaAlta { get; set; }

	public DateTime? FechaActualizacion { get; set; }

	public int Estatus { get; set; }

	public int Grupo { get; set; }

	public int EdadMinimaPerfil { get; set; }

	public int EdadMaximaPerfil { get; set; }

	public double TopeMinimoPerfil { get; set; }

	public double SmcminimoPerfil { get; set; }

	public double TopeMaximoPerfil { get; set; }

	public double SmcmaximoPerfil { get; set; }

	public int NumProceso { get; set; }

	public DateTime FechaArranque { get; set; }

	public virtual CtlEstatusParametrico EstatusNavigation { get; set; } = null!;
}
