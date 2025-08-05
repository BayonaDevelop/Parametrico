namespace Com.Coppel.SDPC.Core.Carteras;

public partial class CatTasareestructura
{
	public int IduProducto { get; set; }
	public int NumPlazo { get; set; }
	public int NumTasa { get; set; }

	public virtual CatProductoreestructura IduProductoNavigation { get; set; } = null!;
}
