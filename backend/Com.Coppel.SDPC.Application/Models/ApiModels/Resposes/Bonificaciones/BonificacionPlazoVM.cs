namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.Bonificaciones;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class BonificacionPlazoVM
{
	public int Plazo { get; set; }
	public int diastranscurridos { get; set; }
	public decimal porcentajebonificacion { get; set; }
}
