using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class HisCatBonificacionescartera
{
	public DateTime FechaAlta { get; set; }

	public int BaseDatosOrigen { get; set; }

	public short IduCartera { get; set; }

	public short IduPlazo { get; set; }

	public short NumMesestranscurridos { get; set; }

	public decimal PrcBonificacion { get; set; }

	public short NumDiastranscurridos { get; set; }

	public virtual CatBasesDeDato BaseDatosOrigenNavigation { get; set; } = null!;
}
