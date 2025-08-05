using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Core.Catalogos;

[DataContract]
public partial class CatPerfilriesgoLrc
{
	[DataMember(Name = "idu_perfil")]
	public int IduPerfil { get; set; }
	[DataMember(Name = "num_edadinicial")]
	public int NumEdadinicial { get; set; }
	[DataMember(Name = "num_edadfinal")]
	public int NumEdadfinal { get; set; }
	[DataMember(Name = "num_valormenor")]
	public double NumValormenor { get; set; }
	[DataMember(Name = "num_smcvalormenor")]
	public double NumSmcvalormenor { get; set; }
	[DataMember(Name = "num_valormayor")]
	public double NumValormayor { get; set; }
	[DataMember(Name = "num_smcvalormayor")]
	public double NumSmcvalormayor { get; set; }
	[DataMember(Name = "num_proceso")]
	public int NumProceso { get; set; }
}
