using Com.Coppel.SDPC.Application.Models.Reports;

namespace Com.Coppel.SDPC.Application.Commons.Files;

public interface IServicePdfAignacionLineasCredito
{
	string CreateFile(FileContentVM request, string logoPath);
}
