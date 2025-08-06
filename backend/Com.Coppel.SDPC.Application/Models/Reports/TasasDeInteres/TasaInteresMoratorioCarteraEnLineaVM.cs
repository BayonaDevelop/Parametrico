using System.Runtime.Serialization;

namespace Com.Coppel.SDPC.Application.Models.Reports.TasasDeInteres;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class TasaInteresMoratorioCarteraEnLineaVM
{
	[DataMember(Name = "IduCiudad")]	
	public short? idu_ciudad { get; set; }

	[DataMember(Name = "IduCuenta")]	
	public short? idu_cuenta { get; set; }

	[DataMember(Name = "NumPorcentajeint")]	
	public int? num_porcentajeint { get; set; }

	[DataMember(Name = "FecMovto")]	
	public DateTime? fec_movto { get; set; }
}
