namespace Com.Coppel.SDPC.Core.Catalogos;

/// <summary>
/// Contiene las tasas de interes que se cargan en las ventas a credito
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "We need to break PascalCase")]
public partial class Ctl_TasasIntere
{
  /// <summary>
  /// Identificador de la cartera
  /// </summary>
  public short IduCartera { get; set; }
  /// <summary>
  /// Descripcion de la tasa de interes
  /// </summary>
  public string? DesTasainteres { get; set; }
  /// <summary>
  /// Contiene el identificador del plazo
  /// </summary>
  public short IduPlazo { get; set; }
  /// <summary>
  /// Indica si es normal o fronteriza.
  /// </summary>
  public byte IduTipotasa { get; set; }
  /// <summary>
  /// Porcentaje de tasa de interes
  /// </summary>
  public decimal PrcTasainteres { get; set; }
  /// <summary>
  /// Indica si es tasa para celulares muebles
  /// </summary>
  public byte ClvCelulares { get; set; }
  /// <summary>
  /// Indica si es tasas para articulo de alto riesgo muebles
  /// </summary>
  public byte ClvAltoriesgo { get; set; }
  public byte ClvEspecial { get; set; }
}
