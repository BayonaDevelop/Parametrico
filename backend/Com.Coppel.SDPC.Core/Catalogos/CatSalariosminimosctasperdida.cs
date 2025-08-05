using System;
using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Core.Catalogos;

[DataContract]
public partial class CatSalariosminimosctasperdida
{
	[DataMember(Name = "ID_SALARIO")]
	public int IdSalario { get; set; }
	[DataMember(Name = "IMP_SALARIOCTASPERDIDAS")]
	public int ImpSalarioctasperdidas { get; set; }
	[DataMember(Name = "FEC_SALARIO")]
	public DateTime FecSalario { get; set; }
}
