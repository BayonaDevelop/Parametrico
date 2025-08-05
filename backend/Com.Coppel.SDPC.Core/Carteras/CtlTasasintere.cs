namespace Com.Coppel.SDPC.Core.Carteras;

public partial class CtlTasasintere
{
	public short IduCartera { get; set; }
	public string DesTasainteres { get; set; } = null!;
	public short IduPlazo { get; set; }
	public byte IduTipotasa { get; set; }
	public decimal PrcTasainteres { get; set; }
	public byte ClvCelulares { get; set; }
	public byte ClvAltoriesgo { get; set; }
	public byte ClvEspecial { get; set; }
}
