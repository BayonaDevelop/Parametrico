using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlAsignacionDeLinea
{
    public int IdAsignacion { get; set; }

    public DateTime FechaAlta { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public int Estatus { get; set; }

    public string TipoLinea { get; set; } = null!;

    public short IduLineadecredito { get; set; }

    public short IduPerfil { get; set; }

    public short Rango { get; set; }

    public float RangoMin { get; set; }

    public float RangoMax { get; set; }

    public float Valor { get; set; }

    public DateTime FechaArranque { get; set; }

    public virtual CtlEstatusParametrico EstatusNavigation { get; set; } = null!;
}
