using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatPerfilriesgoLrcHistorial
{
	public DateTime FechaAlta { get; set; }

	public int BaseDatosOrigen { get; set; }

	public int IduPerfil { get; set; }

	public int NumEdadinicial { get; set; }

	public int NumEdadfinal { get; set; }

	public double NumValormenor { get; set; }

	public double NumSmcvalormenor { get; set; }

	public double NumValormayor { get; set; }

	public double NumSmcvalormayor { get; set; }

	public int NumProceso { get; set; }
}
