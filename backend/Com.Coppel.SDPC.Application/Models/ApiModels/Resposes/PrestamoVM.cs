namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class PrestamoVM
{
	public string puntualidad { get; set; } = string.Empty;
	public string grupo { get; set; } = string.Empty;
	public int puntajeInicial { get; set; }
	public int puntajeFinal { get; set; }
	public int plazo { get; set; }
	public decimal tasaDeInteres { get; set; }
	public string fechaArranque { get; set; } = string.Empty;
}
