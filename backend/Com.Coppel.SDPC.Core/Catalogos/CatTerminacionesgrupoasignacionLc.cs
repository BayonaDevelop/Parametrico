using System;
using System.Collections.Generic;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatTerminacionesgrupoasignacionLc
{
    public short IduGrupotestigo { get; set; }

    public string DesGrupotestigo { get; set; } = null!;

    public string NumTerminacioncliente { get; set; } = null!;

    public short NumProceso { get; set; }

    public DateTime FecMovimiento { get; set; }
}
