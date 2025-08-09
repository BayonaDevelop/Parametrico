namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.Base;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class MetaVM
{
	public Guid transactionID { get; set; }
	public string status { get; set; } = string.Empty;
	public int statusCode { get; set; }
	public DateTime time { get; set; }
	public string time_elapsed { get; set; } = string.Empty;
}
