using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatSalariosminimosctasperdidasHistorial
{
  public DateTime Fechaalta { get; set; }
  public int Basedatosorigen { get; set; }
  public int IdSalario { get; set; }
  public int ImpSalarioctasperdidas { get; set; }
  public DateTime FecSalario { get; set; }

  public virtual CatBasesDeDato BasedatosorigenNavigation { get; set; } = null!;
}
