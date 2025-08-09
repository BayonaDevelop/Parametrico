namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.AsignacionDeLinea;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class PerfilVM
{
	public int idu_perfil { get; set; }
	public short rango { get; set; }
	public double rangoMin { get; set; }
	public double rangoMax { get; set; }
	public double valor { get; set; }
}
