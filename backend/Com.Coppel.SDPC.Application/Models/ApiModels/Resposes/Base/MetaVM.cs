namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.Base;

public class MetaVM
{
	public Guid transactionID { get; set; }
	public string status { get; set; } = string.Empty;
	public int statusCode { get; set; }
	public DateTime time { get; set; }
	public string time_elapsed { get; set; } = string.Empty;
}
