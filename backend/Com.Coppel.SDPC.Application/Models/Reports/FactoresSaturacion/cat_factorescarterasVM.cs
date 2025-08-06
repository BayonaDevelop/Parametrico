using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.FactoresSaturacion;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "We need to break PascalCase")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class cat_factorescarterasVM
{
  [DataMember(Name = "idu_cartera")]
  public string IduCartera {  get; set; } = string.Empty;

  [DataMember(Name = "clv_plazo")]
  public string ClvPlazo {  get; set; } = string.Empty;

  [DataMember(Name = "prc_factor")]
  public string PrcFactor {  get; set; } = string.Empty;

  [DataMember(Name = "clv_lineacreditoespecial")]
  public string ClvLineaCreditoEspecial {  get; set; } = string.Empty;

}
