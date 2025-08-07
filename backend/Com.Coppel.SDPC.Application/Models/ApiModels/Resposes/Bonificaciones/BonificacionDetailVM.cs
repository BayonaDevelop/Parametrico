namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.Bonificaciones
{
	public class BonificacionDetailVM
	{
		public DateTime fechaarranque { get; set; }
		public List<BonificacionPlazoVM> plazos { get; set; } = new();
	}
}
