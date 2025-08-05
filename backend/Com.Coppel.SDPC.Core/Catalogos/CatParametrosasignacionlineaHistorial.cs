using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatParametrosasignacionlineaHistorial
{
    public DateTime FechaAlta { get; set; }

    public int BaseDatosOrigen { get; set; }

    public float NumLinearealinicial { get; set; }

    public float NumLinearealfinal { get; set; }

    public short IduLineadecredito { get; set; }

    public string NomLineadecredito { get; set; } = null!;

    public short IduPerfil { get; set; }

    public float NumValorperfil { get; set; }

    public DateTime FecMovimiento { get; set; }

    public virtual CatBasesDeDato BaseDatosOrigenNavigation { get; set; } = null!;
}
