namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.TasaInteres;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class TasaInteresMoratorioVM
{
	public decimal tasatipociudad1 { get; set; }
	public decimal tasatipociudad2 { get; set; }
	public string fechaArranque { get; set; } = string.Empty;
}
