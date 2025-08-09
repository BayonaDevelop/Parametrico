namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.Bonificaciones;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class BonificacionDetailVM
{
	public DateTime fechaarranque { get; set; }
	public List<BonificacionPlazoVM> plazos { get; set; } = [];
}
