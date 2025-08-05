namespace Com.Coppel.SDPC.Core.Catalogos;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "We need to break PascalCase")]
public partial class Ctl_TasasInteresHistorial
{
  public DateTime FechaAlta { get; set; }
  public int BaseDatosOrigen { get; set; }
  public short IduCartera { get; set; }
  public string? DesTasainteres { get; set; }
  public short IduPlazo { get; set; }
  public byte IduTipotasa { get; set; }
  public decimal PrcTasainteres { get; set; }
  public byte ClvCelulares { get; set; }
  public byte ClvAltoriesgo { get; set; }
  public byte ClvEspecial { get; set; }

  public virtual CatBasesDeDato BaseDatosOrigenNavigation { get; set; } = null!;
}
