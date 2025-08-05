using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CtlInteresMoratorioporCartera
{
    public int IdIntMoratorio { get; set; }

    public DateTime FechaAlta { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public DateTime? FechaActualizacionCl { get; set; }

    public int Estatus { get; set; }

    public int EstatusCl { get; set; }

    public decimal TasaTipoCiudad1 { get; set; }

    public decimal TasaTipoCiudad2 { get; set; }

    public DateTime FechaArranque { get; set; }

    public virtual CtlEstatusParametrico EstatusClNavigation { get; set; } = null!;

    public virtual CtlEstatusParametrico EstatusNavigation { get; set; } = null!;
}
