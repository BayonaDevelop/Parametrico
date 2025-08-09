namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class SalarioMinimoVM
{
	public string nombreSalario { get; set; } = string.Empty;
	public decimal valorSalario { get; set; }
	public string fechaArranque { get; set; } = string.Empty;
}
