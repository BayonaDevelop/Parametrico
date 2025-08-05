using System.Collections.Generic;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlEstatusParametrico
{
	public CtlEstatusParametrico()
	{
		CtlBonificacionCarteraEstatusClNavigations = [];
		CtlBonificacionCarteraEstatusNavigations = [];
		CtlConversionLineadeCreditos = [];
		CtlDecrementosLineadeCreditos = [];
		CtlFactoresSaturacionCarteras = [];
		CtlInteresMoratorioDcps = [];
		CtlInteresMoratorioporCarteraEstatusClNavigations = [];
		CtlInteresMoratorioporCarteraEstatusNavigations = [];
		CtlSalarioMinimoCpEstatusClNavigations = [];
		CtlSalarioMinimoCpEstatusNavigations = [];
		CtlSalarioMinimoLcs = [];
		CtlTasaInteresCpprestamoEstatusClNavigations = [];
		CtlTasaInteresCpprestamoEstatusNavigations = [];
		CtlTasasInteresCpcs = [];
		CtlTasasInteresCps = [];
		CtlPerfilRiesgoLrcs = [];
		CtlValidacionesLrcs = [];
	}

	public int IdEstatus { get; set; }
	public string? DescripionEstatus { get; set; }

	public virtual ICollection<CtlBonificacionCartera> CtlBonificacionCarteraEstatusClNavigations { get; set; }
	public virtual ICollection<CtlBonificacionCartera> CtlBonificacionCarteraEstatusNavigations { get; set; }
	public virtual ICollection<CtlConversionLineadeCredito> CtlConversionLineadeCreditos { get; set; }
	public virtual ICollection<CtlDecrementosLineadeCredito> CtlDecrementosLineadeCreditos { get; set; }
	public virtual ICollection<CtlFactoresSaturacionCartera> CtlFactoresSaturacionCarteras { get; set; }
	public virtual ICollection<CtlInteresMoratorioDcp> CtlInteresMoratorioDcps { get; set; }
	public virtual ICollection<CtlInteresMoratorioporCartera> CtlInteresMoratorioporCarteraEstatusClNavigations { get; set; }
	public virtual ICollection<CtlInteresMoratorioporCartera> CtlInteresMoratorioporCarteraEstatusNavigations { get; set; }
	public virtual ICollection<CtlSalarioMinimoCp> CtlSalarioMinimoCpEstatusClNavigations { get; set; }
	public virtual ICollection<CtlSalarioMinimoCp> CtlSalarioMinimoCpEstatusNavigations { get; set; }
	public virtual ICollection<CtlSalarioMinimoLc> CtlSalarioMinimoLcs { get; set; }
	public virtual ICollection<CtlTasaInteresCpprestamo> CtlTasaInteresCpprestamoEstatusClNavigations { get; set; }
	public virtual ICollection<CtlTasaInteresCpprestamo> CtlTasaInteresCpprestamoEstatusNavigations { get; set; }
	public virtual ICollection<CtlTasasInteresCpc> CtlTasasInteresCpcs { get; set; }
	public virtual ICollection<CtlTasasInteresCp> CtlTasasInteresCps { get; set; }
	public virtual ICollection<CtlPerfilRiesgoLrc> CtlPerfilRiesgoLrcs { get; set; }
	public virtual ICollection<CtlValidacionesLrc> CtlValidacionesLrcs { get; set; }
	public virtual ICollection<CtlAsignacionDeLinea> CtlAsignacionDeLineas { get; set; } = [];
}
