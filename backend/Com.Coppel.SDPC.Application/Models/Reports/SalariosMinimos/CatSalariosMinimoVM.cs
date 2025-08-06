using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.SalariosMinimos;

[DataContract]
public class CatSalariosMinimoVM
{
	[DataMember(Name = "SalarioZonaA")]
	public int SalarioZonaA { get; set; }
	[DataMember(Name = "SalarioZonaB")]
	public int SalarioZonaB { get; set; }
	[DataMember(Name = "SalarioZonaC")]
	public int SalarioZonaC { get; set; }
	[DataMember(Name = "FechaSalario")]
	public DateTime FechaSalario { get; set; }

}
