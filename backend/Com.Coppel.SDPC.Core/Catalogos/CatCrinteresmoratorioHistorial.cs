using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatCrinteresmoratorioHistorial
{
	public DateTime FechaAlta { get; set; }
	public int BaseDatosOrigen { get; set; }
	public short CtlProceso { get; set; }
	public short? IduCiudad { get; set; }
	public short? IduCuenta { get; set; }
	public int? NumPorcentajeint { get; set; }
	public DateTime? FecMovto { get; set; }
}
