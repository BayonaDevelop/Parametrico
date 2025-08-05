namespace Com.Coppel.SDPC.Core.Carteras;

/// <summary>
/// Contiene el factor de consumo de la linea de credito
/// </summary>
public partial class CatFactorescartera
{
	/// <summary>
	/// Identificador de la cartera
	/// </summary>
	public byte IduCartera { get; set; }
	/// <summary>
	/// Identificador del plazo
	/// </summary>
	public byte ClvPlazo { get; set; }
	/// <summary>
	/// Factor de la cartera
	/// </summary>
	public decimal PrcFactor { get; set; }
	/// <summary>
	/// Identifica si es cliente normal o tiene credito especial
	/// </summary>
	public byte ClvLineacreditoespecial { get; set; }
}
