using System;
using System.Collections.Generic;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatTerminacionesgrupoasignacionLcHistorial
{
    public DateTime FechaAlta { get; set; }

    public int BaseDatosOrigen { get; set; }

    public short IduGrupotestigo { get; set; }

    public string DesGrupotestigo { get; set; } = null!;

    public string NumTerminacioncliente { get; set; } = null!;

    public short NumProceso { get; set; }

    public DateTime FecMovimiento { get; set; }

    public virtual CatBasesDeDato BaseDatosOrigenNavigation { get; set; } = null!;
}
