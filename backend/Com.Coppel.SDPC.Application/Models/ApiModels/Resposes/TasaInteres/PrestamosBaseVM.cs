namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.TasaInteres;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class PrestamosBaseVM
{
	public string fechaarranque { get; set; } = string.Empty;

	public List<TasaInteresPrestamoVM> data { get; set; } = [];
}
