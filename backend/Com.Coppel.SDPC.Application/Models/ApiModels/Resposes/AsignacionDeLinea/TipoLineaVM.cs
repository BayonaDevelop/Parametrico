namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.AsignacionDeLinea;

public class TipoLineaVM
{
	public int idu_lineadecredito { get; set; }
	public string tipoLinea { get; set; } = string.Empty;
	public List<PerfilVM> perfiles { get; set; } = new();
}
