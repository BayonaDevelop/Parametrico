using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlAsignacionDeGrupo
{
    public int Idterminaciones { get; set; }

    public DateTime FechaAlta { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public int Estatus { get; set; }

    public short Idgrupo { get; set; }

    public string Grupo { get; set; } = null!;

    public string Terminacioncliente { get; set; } = null!;

    public short Numproceso { get; set; }

    public DateTime FechaArranque { get; set; }
}
