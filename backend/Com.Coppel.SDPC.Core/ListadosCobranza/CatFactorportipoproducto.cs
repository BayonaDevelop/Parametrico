using System;

namespace Com.Coppel.SDPC.Core.ListadosCobranza;

public partial class CatFactorportipoproducto
{
	public int IduControl { get; set; }
	public short NumTipoproducto { get; set; }
	public string DesTipoproducto { get; set; } = null!;
	public short NumPlazo { get; set; }
	public short NumTipolineacredito { get; set; }
	public double NumFactor { get; set; }
	public DateTime? FecMovto { get; set; }
}
