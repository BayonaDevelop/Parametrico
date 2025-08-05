using System;

namespace Com.Coppel.SDPC.Core.Catalogos;

public partial class CatCorreosOperacione
{
	public int IdCorreo { get; set; }
	public DateTime FeAltaMail { get; set; }
	public string NomPersonaCorreo { get; set; } = null!;
	public string Correo { get; set; } = null!;
	public int Tipo { get; set; }
}
