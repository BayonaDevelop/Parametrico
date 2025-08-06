using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.FactoresSaturacion;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "We need to break PascalCase")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class cat_factorportipoproductoVM
{
  [DataMember(Name = "idu_control")]
  public string IduControl { get; set; } = string.Empty;

  [DataMember(Name = "num_tipoproducto")]
  public string NumTipoProducto { get; set; } = string.Empty;

  [DataMember(Name = "des_tipoproducto")]
  public string DesTipoProducto { get; set; } = string.Empty;

  [DataMember(Name = "num_plazo")]
  public string NumPlazo { get; set; } = string.Empty;

  [DataMember(Name = "num_tipolineacredito")]
  public string NumTipoLineacredito { get; set; } = string.Empty;

  [DataMember(Name = "num_factor")]
  public string NumFactor { get; set; } = string.Empty;

  [DataMember(Name = "fec_movto")]
  public string FecMovto { get; set; } = string.Empty;

}