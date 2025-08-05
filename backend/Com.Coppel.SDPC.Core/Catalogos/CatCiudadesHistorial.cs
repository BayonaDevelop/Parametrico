using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatCiudadesHistorial
{
	public DateTime FechaAlta { get; set; }
	public int BaseDatosOrigen { get; set; }
	public short NumeroCiudad { get; set; }
	public string? NombreCiudad { get; set; }
	public string? InicialCiudad { get; set; }
	public int? TasaInteres { get; set; }
	public byte? NumeroEstado { get; set; }
	public string? InicialEstado { get; set; }
	public int? SalarioMinimo { get; set; }
	public short? GerenteZona { get; set; }
	public short? RegionCobranzas { get; set; }
	public int? IvaCiudad { get; set; }
	public DateTime? AntiguedadCiudad { get; set; }
	public short? UnificaCiudadesInformes { get; set; }
	public short? UnificaCiudadesCobranzas { get; set; }
	public short? GerenteCobranzas { get; set; }
	public string? GeneraJobCarteraTienda { get; set; }
	public string? InicialCredito { get; set; }
	public string? RegionEstadoDeCuenta { get; set; }
	public byte? TasaInteresRopa { get; set; }
	public byte? TasaInteresMueble12 { get; set; }
	public byte? TasaInteresMueble18 { get; set; }
	public byte? TasaInteresPrestamo { get; set; }
	public byte? TasaInteresCelular1 { get; set; }
	public byte? TasaInteresCelular2 { get; set; }
	public string? TipoZona { get; set; }
	public DateTime? FechaUltimaActualizacion { get; set; }
	public short TiendaPrincipal { get; set; }
	public short? RegionTienda { get; set; }
	public string? IpCiudad { get; set; }
	public int? TipoCiudad { get; set; }
	public byte TasaInteresPrestamo18 { get; set; }
	public byte TasaInteresPrestamo24 { get; set; }
	public short? PrcCtesbuenos { get; set; }
	public byte? TasaRevolvente { get; set; }

	public virtual CatBasesDeDato BaseDatosOrigenNavigation { get; set; } = null!;
}
