using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatCrconfiguracionesplazosHistorial
{
	public DateTime FechaAlta { get; set; }
	public int BaseDatosOrigen { get; set; }
	public short CtlProceso { get; set; }
	public short IduCuenta { get; set; }
	public short NumPlazo { get; set; }
	public short PrcInteressobrecompra { get; set; }
	public short NumFactorlineacreditonormal { get; set; }
	public short NumFactorlineacreditoespecial { get; set; }
	public DateTime? FecMovto { get; set; }
	public short NumFactorlineacreditobasica { get; set; }
	public short NumFactorlineacreditominima { get; set; }
}
