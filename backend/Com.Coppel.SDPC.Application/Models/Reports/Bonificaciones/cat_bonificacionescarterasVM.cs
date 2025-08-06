using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.Bonificaciones;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "We need to break PascalCase")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
public class cat_bonificacionescarterasVM
{
  [DataMember(Name = "idu_cartera")]
  public string IduCartera { get; set; } = string.Empty;

  [DataMember(Name = "idu_plazo")]
  public string IduPlazo { get; set; } = string.Empty;

  [DataMember(Name = "num_mesestranscurridos")]
  public string NumMesestranscurridos { get; set; } = string.Empty;

  [DataMember(Name = "prc_bonificacion")]
  public string PrcBonificacion { get; set; } = string.Empty;

  [DataMember(Name = "num_diastranscurridos")]
  public string NumDiastranscurridos { get; set; } = string.Empty;

}
