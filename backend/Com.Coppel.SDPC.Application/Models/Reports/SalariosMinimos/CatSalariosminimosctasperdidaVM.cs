using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Com.Coppel.SDPC.Application.Models.Reports.SalariosMinimos;

[DataContract]
public class CatSalariosminimosctasperdidaVM
{
    [DataMember(Name = "ID_SALARIO")]
    public int IdSalario { get; set; }
    [DataMember(Name = "IMP_SALARIOCTASPERDIDAS")]
    public int ImpSalarioctasperdidas { get; set; }
    [DataMember(Name = "FEC_SALARIO")]
    public DateTime FecSalario { get; set; }
}
