namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.ConversionLineaCredito;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class CjsonVM
{
	public List<ValorVM> valores { get; set; } = [];

	public List<KeyValuePair<string, List<GroupVM>>> igrupo { get; set; } = [];
}
