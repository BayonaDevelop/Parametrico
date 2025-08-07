namespace Com.Coppel.SDPC.Application.ViewModels.ApiModels.Resposes.Bonificaciones;

public class BonificacionesResponseVM
{
  public int iestado { get; set; }
  public string cmensaje { get; set; } = string.Empty;
  public dynamic cjson { get; set; } = new { };
}
