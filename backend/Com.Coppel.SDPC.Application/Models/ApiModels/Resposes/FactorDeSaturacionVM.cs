namespace Com.Coppel.SDPC.Application.Models.ApiModels.Resposes;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "We need to break Naming Styles")]
public class FactorDeSaturacionVM
{
	public int plazo { get; set; }
	public decimal factornormal { get; set; }
	public decimal factorespecial { get; set; }
	public decimal factorinicial { get; set; }
	public decimal factorminima { get; set; }
	public string fechaArranque { get; set; } = string.Empty;
}
