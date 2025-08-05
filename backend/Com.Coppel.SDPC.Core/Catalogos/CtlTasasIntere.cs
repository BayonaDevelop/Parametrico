using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlTasasIntere
{
  public byte? TipoTasa { get; set; }
  public byte? TasaRopa { get; set; }
  public double? TasaMueblesPlazo12 { get; set; }
  public double? TasaMueblesPlazo18 { get; set; }
  public byte? TasaPrestamos { get; set; }
  public byte? TasaCelularesPlazo12 { get; set; }
  public byte? TasaCelularesPlazo18 { get; set; }
  public byte? TasaTiempoAire { get; set; }
  public byte? TasaArtRiesgo { get; set; }
  public DateTime? Fecha { get; set; }
  public double? TasaAltoRiesgoPlazo18 { get; set; }
  public decimal TasaPrestamosPlazo18 { get; set; }
  public decimal TasaPrestamosPlazo24 { get; set; }
  public byte? TasaRevolvente { get; set; }
}
