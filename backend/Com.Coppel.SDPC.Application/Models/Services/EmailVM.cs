using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Application.Models.Persistence;

namespace Com.Coppel.SDPC.Application.Models.Services;

public class EmailVM
{
	public PuntoDeConsumoVM PuntoDeConsumo { get; set; } = new();

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
	public MailType mailType { get; set; }
	
	public AreaType Type { get; set; }
	
	public string Cartera { get; set; } = string.Empty;
	
	public string To { get; set; } = string.Empty;
	
	public string Subject { get; set; } = string.Empty;
	
	public string Body { get; set; } = string.Empty;
	
	public List<string> Files { get; set; } = [];
}
