namespace Com.Coppel.SDPC.Core.Catalogos;
public partial class CtlEnvioDeCifrasControl
{
	public long Id { get; set; }

	public int IdPuntodeconsumo { get; set; }

	public DateTime FechaArranque { get; set; }

	public bool Estatus { get; set; }

	public bool TipoEjecucion { get; set; }
}
