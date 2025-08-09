namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.TasaInteres;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class TasaInteresMoratoriaPrestamosDiaVM
{
	public string titulo { get; set; } = string.Empty;
	public int diasTranscurridos { get; set; } = 0;
	public decimal tasaMoratoria { get; set; } = 0m;
	public string fechaIns { get; set; } = string.Empty;
	public string userModifico { get; set; } = string.Empty;
}
