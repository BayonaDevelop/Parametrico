using Com.Coppel.SDPC.Application.Models.ApiModels.Resposes.Base;

namespace Com.Coppel.SDPC.Application.ViewModels.ApiModels.Resposes.Base;

public class ResponseVM
{
  public MetaVM meta { get; set; } = new();
  public dynamic data { get; set; } = new { };
}
