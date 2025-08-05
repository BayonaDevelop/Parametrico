using System;

namespace Com.Coppel.SDPC.Core.Carteras;

public partial class Catnegocio
{
	public short ClaveNegocio { get; set; }
	public string? DescripcionNegocio { get; set; }
	public int? InteresNegocio { get; set; }
	public string? InicialNegocio { get; set; }
	public short? CiudadNegocio { get; set; }
	public string? NegocioActivo { get; set; }
	public byte? TipoCta { get; set; }
	public decimal? Comision { get; set; }
	public int? Subcuenta { get; set; }
	public int? Iva { get; set; }
	public int? IdEmpresa { get; set; }
	public int? TdaAnterior { get; set; }
	public DateTime? FechaApertura { get; set; }
	public int? TipoProducto { get; set; }
	public int? TdaOrigen { get; set; }
	public byte? PrcTasaInteres { get; set; }
	public byte FlagTipoNegocio { get; set; }
}
