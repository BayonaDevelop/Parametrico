using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.ConversionLineasCredito;

[DataContract]

public partial class ValidacionesLrcVM
{
	[DataMember(Name = "SalariosMinimos")]
	public int SalariosMinimos { get; set; }
	[DataMember(Name = "SalariosMaximos")]
	public int SalariosMaximos { get; set; }
	[DataMember(Name = "MesesParaAumento")]
	public int MesesParaAumento { get; set; }
	[DataMember(Name = "MesesParaDisminucion")]
	public int MesesParaDisminucion { get; set; }
	[DataMember(Name = "PorcentajeCompra")]
	public double PorcentajeCompra { get; set; }
	[DataMember(Name = "FactorLineaTopeDeCredito")]
	public int FactorLineaTopeDeCredito { get; set; }
	[DataMember(Name = "EficienciaParaAumento")]
	public int EficienciaParaAumento { get; set; }
	[DataMember(Name = "EficienciaParaDisminucion")]
	public int EficienciaParaDisminucion { get; set; }
	[DataMember(Name = "PorcentajeAumento")]
	public double PorcentajeAumento { get; set; }
	[DataMember(Name = "PorcentajeDisminucion")]
	public double PorcentajeDisminucion { get; set; }
	[DataMember(Name = "situacionAumento")]
	public string SituacionAumento { get; set; } = string.Empty;
	[DataMember(Name = "situaciondisminucion")]
	public string Situaciondisminucion { get; set; } = string.Empty;
	[DataMember(Name = "situaciondisminucionCteGol")]
	public string SituaciondisminucionCteGol { get; set; } = string.Empty;
	[DataMember(Name = "salariosMinimosDism")]
	public int SalariosMinimosDism { get; set; }
	[DataMember(Name = "num_EdadMaxima")]
	public int NumEdadMaxima { get; set; }
	[DataMember(Name = "num_PuntosParPrestamos")]
	public int NumPuntosParPrestamos { get; set; }
	[DataMember(Name = "num_CapacidadAbono")]
	public int NumCapacidadAbono { get; set; }
	[DataMember(Name = "num_MesesConSaldo")]
	public int NumMesesConSaldo { get; set; }
	[DataMember(Name = "num_mesesprimercompra")]
	public int NumMesesprimercompra { get; set; }
	[DataMember(Name = "opc_puntualidad")]
	public string OpcPuntualidad { get; set; } = null!;
	[DataMember(Name = "imp_vencido")]
	public int ImpVencido { get; set; }
	[DataMember(Name = "prc_ingresomensual")]
	public int PrcIngresomensual { get; set; }
	[DataMember(Name = "prc_perfilgrupo1")]
	public int PrcPerfilgrupo1 { get; set; }
	[DataMember(Name = "prc_perfilgrupo2")]
	public int PrcPerfilgrupo2 { get; set; }
	[DataMember(Name = "num_puntosparcn")]
	public int NumPuntosparcn { get; set; }
	[DataMember(Name = "num_mesescra")]
	public int NumMesescra { get; set; }
	[DataMember(Name = "num_factorlinea")]
	public long NumFactorlinea { get; set; }
	[DataMember(Name = "num_edadlrcmin")]
	public int NumEdadlrcmin { get; set; }
	[DataMember(Name = "imp_vencidolrcmin")]
	public int ImpVencidolrcmin { get; set; }
	[DataMember(Name = "num_mesesprimercompralrcmin")]
	public int NumMesesprimercompralrcmin { get; set; }
	[DataMember(Name = "num_mesesconsaldolrcmin")]
	public int NumMesesconsaldolrcmin { get; set; }
	[DataMember(Name = "num_mesescongelados")]
	public int NumMesescongelados { get; set; }
}
