namespace Com.Coppel.SDPC.Core.Catalogos;

/// <summary>
/// Configuración de la tasa de interés a cobrar de acuerdo por cada uno de los plazos. Aquí se configura por producto los plazos que este manejara y además el porcentaje de interés que se va a cobrar por el re-financiamiento de la deuda.
/// </summary>
public partial class CatTasareestructura
{
  /// <summary>
  /// Identificador del producto de reestructura de crédito al que pertenece el plazo y la tasa interés.
  /// </summary>
  public int IduProducto { get; set; }
  /// <summary>
  /// Plazo en meses que se otorga al cliente de acuerdo al producto de reestructura al que aplica dicho cliente.
  /// </summary>
  public int NumPlazo { get; set; }
  /// <summary>
  /// Porcentaje de la tasa interés que se aplicara al producto de reestructura a la que aplica el cliente, según el plazo .
  /// </summary>
  public int NumTasa { get; set; }
}
