using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.TasasDeInteres;

[DataContract]
public class CatCiudadesVM
{
  [DataMember(Name = "NumeroCiudad")]
  public short NumeroCiudad { get; set; }
  [DataMember(Name = "NombreCiudad")]
  public string NombreCiudad { get; set; } = string.Empty;
  [DataMember(Name = "InicialCiudad")]
  public string InicialCiudad { get; set; } = string.Empty;
  [DataMember(Name = "TasaInteres")]
  public int TasaInteres { get; set; }
  [DataMember(Name = "NumeroEstado")]
  public byte NumeroEstado { get; set; }
  [DataMember(Name = "InicialEstado")]
  public string InicialEstado { get; set; } = string.Empty;
  [DataMember(Name = "SalarioMinimo")]
  public int SalarioMinimo { get; set; }
  [DataMember(Name = "GerenteZona")]
  public short GerenteZona { get; set; }
  [DataMember(Name = "RegionCobranzas")]
  public short RegionCobranzas { get; set; }
  [DataMember(Name = "IvaCiudad")]
  public int IvaCiudad { get; set; }
  [DataMember(Name = "AntiguedadCiudad")]
  public DateTime AntiguedadCiudad { get; set; }
  [DataMember(Name = "UnificaCiudadesInformes")]
  public short UnificaCiudadesInformes { get; set; }
  [DataMember(Name = "UnificaCiudadesCobranzas")]
  public short UnificaCiudadesCobranzas { get; set; }
  [DataMember(Name = "GerenteCobranzas")]
  public short GerenteCobranzas { get; set; }
  [DataMember(Name = "GeneraJobCarteraTienda")]
  public string GeneraJobCarteraTienda { get; set; } = string.Empty;
  [DataMember(Name = "InicialCredito")]
  public string InicialCredito { get; set; } = string.Empty;
  [DataMember(Name = "RegionEstadoDeCuenta")]
  public string RegionEstadoDeCuenta { get; set; } = string.Empty;
  [DataMember(Name = "TasaInteresRopa")]
  public byte TasaInteresRopa { get; set; }
  [DataMember(Name = "TasaInteresMueble12")]
  public byte TasaInteresMueble12 { get; set; }
  [DataMember(Name = "TasaInteresMueble18")]
  public byte TasaInteresMueble18 { get; set; }
  [DataMember(Name = "TasaInteresPrestamo")]
  public byte TasaInteresPrestamo { get; set; }
  [DataMember(Name = "TasaInteresCelular1")]
  public byte TasaInteresCelular1 { get; set; }
  [DataMember(Name = "TasaInteresCelular2")]
  public byte TasaInteresCelular2 { get; set; }
  [DataMember(Name = "TipoZona")]
  public string TipoZona { get; set; } = string.Empty;
  [DataMember(Name = "FechaUltimaActualizacion")]
  public DateTime FechaUltimaActualizacion { get; set; }
  [DataMember(Name = "TiendaPrincipal")]
  public short TiendaPrincipal { get; set; }
  [DataMember(Name = "RegionTienda")]
  public short RegionTienda { get; set; }
  [DataMember(Name = "IpCiudad")]
  public string IpCiudad { get; set; } = string.Empty;
  [DataMember(Name = "TipoCiudad")]
  public int TipoCiudad { get; set; }
  [DataMember(Name = "TasaInteresPrestamo18")]
  public byte TasaInteresPrestamo18 { get; set; }
  [DataMember(Name = "TasaInteresPrestamo24")]
  public byte TasaInteresPrestamo24 { get; set; }
  [DataMember(Name = "PrcCtesbuenos")]
  public short PrcCtesbuenos { get; set; }
  [DataMember(Name = "TasaRevolvente")]
  public byte TasaRevolvente { get; set; }

}
