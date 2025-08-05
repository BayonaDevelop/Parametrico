using System;

namespace Com.Coppel.SDPC.Core.Carteras;

public partial class CatParametrosasignacionlinea
{
    public float NumLinearealinicial { get; set; }

    public float NumLinearealfinal { get; set; }

    public short IduLineadecredito { get; set; }

    public string NomLineadecredito { get; set; } = null!;

    public short IduPerfil { get; set; }

    public float NumValorperfil { get; set; }

    public DateTime FecMovimiento { get; set; }
}
