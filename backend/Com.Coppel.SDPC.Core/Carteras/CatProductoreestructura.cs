using System;
using System.Collections.Generic;

namespace Com.Coppel.SDPC.Core.Carteras;

/// <summary>
/// Configuración de los diferentes productos de reestructura que se ofrecen a los clientes. En base a esta configuración se aplicara las fórmulas para generar la información mensual de candidatos.
/// </summary>
public partial class CatProductoreestructura
{
	public CatProductoreestructura()
	{
		CatTasareestructuras = [];
	}

	/// <summary>
	/// Identificador del producto de reestructura de crédito, es auto incrementable.
	/// </summary>
	public int IduProducto { get; set; }
	/// <summary>
	/// Nombre del producto de la reestructura.
	/// </summary>
	public string NomProducto { get; set; } = null!;
	/// <summary>
	/// Identifica la lógica o formula para calcular el importe a financiar para la reestructura del crédito. 
	/// </summary>
	public string OpcFormula { get; set; } = null!;
	/// <summary>
	/// Si la reestructura perdona los moratorios no pagados.
	/// </summary>
	public bool? OpcCondonacionMoratorio { get; set; }
	/// <summary>
	/// Indica el número de meses que no deben pasar sin que el cliente registre un abono para poder ofrecer la reestructuración.
	/// </summary>
	public int NumUltimoAbono { get; set; }
	/// <summary>
	/// Cuantos abonos debe dar el cliente para que pueda hacer nuevas compras.
	/// </summary>
	public int NumAbonoDesbloqueo { get; set; }
	/// <summary>
	/// A partir de cuantos abonos cumplidos puntualmente, el cliente puede jugar nuevamente en los cambios de puntualidad.
	/// </summary>
	public int? NumAbonoNormalizaCredito { get; set; }
	/// <summary>
	/// Indica si el sistema debe solicitar al cliente dar el abono inicial para formalizar la reestructura del crédito.
	/// </summary>
	public bool? OpcCondicionPago { get; set; }
	/// <summary>
	/// Es la cantidad mínima que el cliente debe tener como saldo total para tener acceso a la reestructura.
	/// </summary>
	public int ImpSaldoMinimo { get; set; }
	/// <summary>
	/// Indica que si un cliente catalogado como conflicto puede ser candidato al producto de reestructura.
	/// </summary>
	public bool OpcClienteConflicto { get; set; }
	/// <summary>
	/// Indica si el producto de reestructuración esta activó.
	/// </summary>
	public bool? OpcEstatus { get; set; }
	/// <summary>
	/// fecha en la que se registra el producto de reestructura
	/// </summary>
	public DateTime? FecCreacion { get; set; }
	/// <summary>
	/// Fecha en la que sufrió cambio la configuración del producto.
	/// </summary>
	public DateTime? FecModificacion { get; set; }

	public virtual ICollection<CatTasareestructura> CatTasareestructuras { get; set; }
}
