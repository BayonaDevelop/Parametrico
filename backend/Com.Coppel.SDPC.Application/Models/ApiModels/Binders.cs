using Com.Coppel.SDPC.Application.Models.ApiModels.SerializationBinders;
using Newtonsoft.Json.Serialization;

namespace Com.Coppel.SDPC.Application.Models.ApiModels;

public static class Binders
{
	public static readonly ISerializationBinder SalarioMinimoRecord = new SalarioMinimoSerializationBinder();
	public static readonly ISerializationBinder TasaInteresRecord = new TasaInteresSerializationBinder();
	public static readonly ISerializationBinder TasaInteresMueblesRecord = new TasaInteresMueblesSerializationBinder();
	public static readonly ISerializationBinder TasaInteresMoratorioRecord = new TasaInteresMoratorioSerializationBinder();
	public static readonly ISerializationBinder PrestamoRecord = new PrestamoSerializationBinder();
	public static readonly ISerializationBinder FactorSaturacionRecord = new FactorSaturacionSerializationBinder();
	public static readonly ISerializationBinder DecrementoLineaRecord = new DecrementoLineaSerializationBinder();
	public static readonly ISerializationBinder BonioficacionRecord = new BonificacionesSerializationBinder();
	public static readonly ISerializationBinder TokenParamsRecord = new ParametroAutenticacionSerializationBinder();
	public static readonly ISerializationBinder PuntosDeConsumoRecord = new PuntoDeConsumoSerializationBinder();
	public static readonly ISerializationBinder CatalogoPlazosRecord = new CatalogosPlazosSerializationBinder();
}
