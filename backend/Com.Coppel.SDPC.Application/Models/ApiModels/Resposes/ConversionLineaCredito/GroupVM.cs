namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.ConversionLineaCredito;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class GroupVM
{
	public int porcentajePerfil { get; set; }
	public int edadMinimaPerfil { get; set; }
	public int edadMaximaPerfil { get; set; }
	public decimal topeMinimoPerfil { get; set; }
	public decimal topeMaximoPerfil { get; set; }
	public string fechArranque { get; set; } = string.Empty;
}
