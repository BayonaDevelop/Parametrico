namespace Com.Coppel.SDPC.Core.Carteras;

public partial class ValidacionesLrc
{
	public int? SalariosMinimos { get; set; }
	public int? SalariosMaximos { get; set; }
	public int? MesesParaAumento { get; set; }
	public int? MesesParaDisminucion { get; set; }
	public double? PorcentajeCompra { get; set; }
	public int? FactorLineaTopeDeCredito { get; set; }
	public int? EficienciaParaAumento { get; set; }
	public int? EficienciaParaDisminucion { get; set; }
	public double? PorcentajeAumento { get; set; }
	public double? PorcentajeDisminucion { get; set; }
	public string? SituacionAumento { get; set; }
	public string? Situaciondisminucion { get; set; }
	public string? SituaciondisminucionCteGol { get; set; }
	public int? SalariosMinimosDism { get; set; }
	public int NumEdadMaxima { get; set; }
	public int NumPuntosParPrestamos { get; set; }
	public int NumCapacidadAbono { get; set; }
	public int NumMesesConSaldo { get; set; }
	public int NumMesesprimercompra { get; set; }
	public string OpcPuntualidad { get; set; } = null!;
	public int ImpVencido { get; set; }
	public int PrcIngresomensual { get; set; }
	public int PrcPerfilgrupo1 { get; set; }
	public int PrcPerfilgrupo2 { get; set; }
	public int NumPuntosparcn { get; set; }
	public int NumMesescra { get; set; }
	public long NumFactorlinea { get; set; }
	public int NumEdadlrcmin { get; set; }
	public int ImpVencidolrcmin { get; set; }
	public int NumMesesprimercompralrcmin { get; set; }
	public int NumMesesconsaldolrcmin { get; set; }
	public int NumMesescongelados { get; set; }
}
