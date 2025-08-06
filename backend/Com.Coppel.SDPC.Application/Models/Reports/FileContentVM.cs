using Com.Coppel.SDPC.Application.Models.Enums;
using Com.Coppel.SDPC.Application.Models.Persistence;

namespace Com.Coppel.SDPC.Application.Models.Reports;

public class FileContentVM
{
	public Type ViewModel { get; set; } = null!;
	public Type ViewModelExcel { get; set; } = null!;

	public AreaType Area { get; set; }

	public PuntoDeConsumoVM PuntoDeConsumo { get; set; } = null!;

	public bool ExtraFile { get; set; } = false;

	public List<dynamic> Data { get; set; } = null!;
	public List<dynamic> DataExcel { get; set; } = null!;

	public string TableName { get; set; } = string.Empty;

	public List<float> ColumnSizes { get; set; } = null!;

	public PageOrientationTypeVM PageOrientation { get; set; } = PageOrientationTypeVM.VERTICAL;
  public DateTime FechaArranque { get; set; }
  public DateTime FechaAlta { get; set; }
  public string Empleado { get; } = "Servicio de Administrador de Catálogo";
  public bool NewData { get; set; } = false;
  public bool NewTitleForFileBeforeUpdate { get; set; } = false;
  public bool EmptyFile { get; set; } = false;
  public bool ShowTableDates { get; set; } = false;
}
