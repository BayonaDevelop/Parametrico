namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.AsignacionDeLinea;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class TipoLineaVM
{
	public int idu_lineadecredito { get; set; }
	public string tipoLinea { get; set; } = string.Empty;
	public List<PerfilVM> perfiles { get; set; } = [];
}
