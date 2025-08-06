using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.AsignacionLinea;

[DataContract]
public class CifraDeControlAsignacionLineaVM
{
  [DataMember(Name = "Num_LineaRealInicial")]
  public string NumLineaRealInicial { set; get; } = string.Empty;

  [DataMember(Name = "Num_LineaRealFinal")]
  public string NumLineaRealFinal { set; get; } = string.Empty;

	[DataMember(Name = "Idu_LineaDeCredito")]
  public string IduLineaDeCredito { set; get; } = string.Empty;

	[DataMember(Name = "Nom_LineaDeCRedito")]
  public string NomLineaDeCRedito { set; get; } = string.Empty;

	[DataMember(Name = "Idu_Perfil")]
  public string IduPerfil { set; get; } = string.Empty;

	[DataMember(Name = "Num_ValorPerfil")]
  public string NumValorPerfil { set; get; } = string.Empty;

	[DataMember(Name = "Fec_Movimiento")]
  public string FecMovimiento { set; get; } = string.Empty;
}
