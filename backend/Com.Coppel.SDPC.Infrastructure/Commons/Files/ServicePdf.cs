using System.Data;
using System.Diagnostics;
using Com.Coppel.SDPC.Application.Commons.Files;
using Com.Coppel.SDPC.Application.Models.Enums; 
using Com.Coppel.SDPC.Application.Models.Reports;
using ImageMagick;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Event;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Com.Coppel.SDPC.Infrastructure.Commons.Files;

public class ServicePdf : IServicePdf
{
	private string _logoPath = string.Empty;
	private DataTable _table = new();
	private FileContentVM _request = new();
	public string Empleado { get; set; } = string.Empty;

	public string CreateFile(FileContentVM request, string logoPath)
	{
		_request = request;
		_logoPath = logoPath;
		_table = Utils.ConvertDataToTable(_request.ViewModel, _request.Data);
		Empleado = _request.Empleado;

		string result = Utils.CheckFile(_request.PuntoDeConsumo, request.TableName, _request.Area, false, _request.ExtraFile);

		PageSize pageSize;

		if (request.PageOrientation == PageOrientationTypeVM.VERTICAL)
		{
			pageSize = PageSize.LETTER;
		}
		else
		{
			pageSize = PageSize.LETTER.Rotate();
		}

		Directory.CreateDirectory(System.IO.Path.GetDirectoryName(result)!);
		PdfWriter writer = new(result);
		PdfDocument pdf = new(writer);
		Document document = new(pdf, pageSize);

		pdf.AddEventHandler(PdfDocumentEvent.INSERT_PAGE, new EndPageEventHandler());

		CreateTable(document);

		document.Close();

		return result;
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of methods should not be too high", Justification = "We will review Cognitive Complexity later")]
	private void CreateTable(Document document)
	{
		string Process = string.Empty;

		switch (_request.Area)
		{
			case AreaType.CALIDAD:
				Process = "Calidad";
				break;
			case AreaType.CARTERA_EN_LINEA:
				Process = "Cartera En Linea";
				break;
			case AreaType.ETL:
				Process = "ETL";
				break;

		}

		PdfFont font = PdfFontFactory.CreateRegisteredFont("Helvetica");
		Table table = new Table(_table.Columns.Count).UseAllAvailableWidth();
		Table tablaLogoTitulo = new Table(2).UseAllAvailableWidth();
		PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
		Table _tablaFechas = new Table(6).UseAllAvailableWidth();
		Table _tablaDatos = new Table(4).UseAllAvailableWidth();
		Style styleCell = new Style()
											.SetBackgroundColor(ColorConstants.LIGHT_GRAY)
											.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

		tablaLogoTitulo.AddHeaderCell(
			new Cell(1, 1)
				.Add(InsertLogo())
				.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
				.SetVerticalAlignment(VerticalAlignment.MIDDLE)
				.SetBorder(iText.Layout.Borders.Border.NO_BORDER)
				.SetFont(font)
		.SetFontSize(10)
		);

		if (_request.NewTitleForFileBeforeUpdate)
		{
			tablaLogoTitulo.AddHeaderCell(
				new Cell(2, 2).Add(new Paragraph($"VALORES DE LA {_request.PuntoDeConsumo.NomFuncionalidad} En {Process} ANTES DE LA ACTUALIZACIÓN EN BD:Catalogos.".ToUpper())).SetBorder(iText.Layout.Borders.Border.NO_BORDER)
							.SetFont(font)
							.SetFontSize(12)
							.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
							.SetVerticalAlignment(VerticalAlignment.MIDDLE)
			);
		}
		else
		{
			tablaLogoTitulo.AddHeaderCell(
				new Cell(2, 2).Add(new Paragraph($"CIFRA DE CONTROL PARA {_request.PuntoDeConsumo.NomFuncionalidad} En {Process} ".ToUpper())).SetBorder(iText.Layout.Borders.Border.NO_BORDER)
						.SetFont(font)
						.SetFontSize(12)
						.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
						.SetVerticalAlignment(VerticalAlignment.MIDDLE)
			);
		}

		if (_request.ShowTableDates)
		{
			_tablaFechas.AddHeaderCell(
				new Cell(1, 1).Add(new Paragraph($"Tabla:"))
					.SetBorder(iText.Layout.Borders.Border.NO_BORDER)
					.SetFont(bold)
					.SetFontSize(10)
					.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
					.SetVerticalAlignment(VerticalAlignment.MIDDLE)
			);
			_tablaFechas.AddHeaderCell(
				new Cell(1, 1).Add(new Paragraph(_request.TableName))
					.SetBorder(iText.Layout.Borders.Border.NO_BORDER)
					.SetFont(font)
					.SetFontSize(10)
					.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
					.SetVerticalAlignment(VerticalAlignment.MIDDLE)
			);
			_tablaFechas.AddHeaderCell(
				new Cell(1, 1).Add(new Paragraph($"Fecha Arranque:"))
					.SetBorder(iText.Layout.Borders.Border.NO_BORDER)
					.SetFont(bold)
					.SetFontSize(10)
					.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
					.SetVerticalAlignment(VerticalAlignment.MIDDLE)
			);
			_tablaFechas.AddHeaderCell(
				new Cell(1, 1).Add(new Paragraph(_request.FechaArranque.ToShortDateString()))
					.SetBorder(iText.Layout.Borders.Border.NO_BORDER)
					.SetFont(font)
					.SetFontSize(10)
					.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
					.SetVerticalAlignment(VerticalAlignment.MIDDLE)
			);
			_tablaFechas.AddHeaderCell(
				new Cell(1, 1).Add(new Paragraph($"Fecha de Actualización:"))
					.SetBorder(iText.Layout.Borders.Border.NO_BORDER)
					.SetFont(bold)
					.SetFontSize(10)
					.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
					.SetVerticalAlignment(VerticalAlignment.MIDDLE)
			);
			_tablaFechas.AddHeaderCell(
				new Cell(1, 1).Add(new Paragraph(_request.FechaAlta.ToShortDateString()))
					.SetBorder(iText.Layout.Borders.Border.NO_BORDER)
					.SetFont(font)
					.SetFontSize(10)
					.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
					.SetVerticalAlignment(VerticalAlignment.MIDDLE)
			);
		}

		if (!_request.NewData)
		{
			_tablaDatos.AddHeaderCell(new Cell(1, 6).Add(_tablaFechas).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
			_tablaDatos.AddHeaderCell(
				new Cell(2, 1).Add(new Paragraph($"Campo Afectado"))
					.SetFont(bold)
					.SetFontSize(11)
					.SetVerticalAlignment(VerticalAlignment.MIDDLE)
					.AddStyle(styleCell)
			);
			_tablaDatos.AddHeaderCell(
				new Cell(1, 3).Add(new Paragraph($"Valor"))
					.SetFont(bold)
					.SetFontSize(11)
					.SetVerticalAlignment(VerticalAlignment.MIDDLE)
					.AddStyle(styleCell)
			);
			_tablaDatos.AddHeaderCell(
				new Cell().Add(new Paragraph($"Antes del Cambio"))
					.SetFont(bold)
					.SetFontSize(11)
					.SetVerticalAlignment(VerticalAlignment.MIDDLE)
					.AddStyle(styleCell)
			);
			_tablaDatos.AddHeaderCell(
				new Cell().Add(new Paragraph($"Despues del Cambio"))
					.SetFont(bold)
					.SetFontSize(11)
					.SetVerticalAlignment(VerticalAlignment.MIDDLE)
					.AddStyle(styleCell)
			);
			_tablaDatos.AddHeaderCell(
				new Cell().Add(new Paragraph($"ADP"))
					.SetFont(bold)
					.SetFontSize(11)
					.SetVerticalAlignment(VerticalAlignment.MIDDLE)
					.AddStyle(styleCell)
			);

			foreach (DataRow row in _table.Rows)
			{
				for (int i = 0; i < row.ItemArray.Length; i++)
				{
					if (_table.Columns[i].ColumnName.CompareTo("Campo Afectado") == 0)
					{
						_tablaDatos.AddCell(
								new Cell()
									.Add(new Paragraph(row[i].ToString()))
									.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
									.SetVerticalAlignment(VerticalAlignment.MIDDLE)
									.SetFont(font)
									.SetFontSize(10)
								);
					}
					else
					{
						_tablaDatos.AddCell(
								new Cell()
									.Add(new Paragraph(row[i].ToString()))
									.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
									.SetVerticalAlignment(VerticalAlignment.MIDDLE)
									.SetFont(font)
									.SetFontSize(10)
							);
					}					
				}
			}
		}
		else
		{
			table.AddHeaderCell(new Cell(1, 6).Add(_tablaFechas).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
			foreach (DataColumn column in _table.Columns)
			{
				table.AddHeaderCell(
					new Cell()
						.Add(new Paragraph(column.ColumnName))
						.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
						.SetVerticalAlignment(VerticalAlignment.MIDDLE)
						.SetFont(bold)
						.SetFontSize(10)
						.SetBackgroundColor(new DeviceRgb(212, 212, 212))
				);
			}

			foreach (DataRow row in _table.Rows)
			{
				for (int i = 0; i < row.ItemArray.Length; i++)
				{
					if (_table.Columns[i].DataType == typeof(DateTime))
					{
						try
						{
							var data = Convert.ToDateTime(row[i].ToString()).ToString("dd/MM/yyy").ToString();
							table.AddCell(
								new Cell()
									.Add(new Paragraph(data))
									.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
									.SetVerticalAlignment(VerticalAlignment.MIDDLE)
									.SetFont(font)
									.SetFontSize(10)
							);
						}
						catch (Exception)
						{
							Debug.WriteLine("Error al obtener fecha");
						}
					}
					else if (
						_table.Columns[i].DataType == typeof(uint) || _table.Columns[i].DataType == typeof(ulong) || _table.Columns[i].DataType == typeof(uint) ||
						_table.Columns[i].DataType == typeof(long) || _table.Columns[i].DataType == typeof(int) ||
						_table.Columns[i].DataType == typeof(float) || _table.Columns[i].DataType == typeof(double) || _table.Columns[i].DataType == typeof(decimal))
					{
						table.AddCell(
							new Cell()
								.Add(new Paragraph(row.ItemArray[i]!.ToString()))
								.SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
								.SetVerticalAlignment(VerticalAlignment.MIDDLE)
								.SetFont(font)
								.SetFontSize(10)
						);
					}
					else
					{
						table.AddCell(
							new Cell()
							.Add(new Paragraph(row.ItemArray[i]!.ToString()))
							.SetFont(font)
							.SetFontSize(10)
							.SetVerticalAlignment(VerticalAlignment.MIDDLE)
						);
					}

				}
			}
			if (_request.EmptyFile)
			{
				table.AddHeaderCell(
					new Cell(8, 8).Add(new Paragraph($"*Actualmente la tabla no cuenta con registros."))
						.SetBorder(iText.Layout.Borders.Border.NO_BORDER)
						.SetFont(bold)
						.SetFontSize(12)
						.SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED)
						.SetVerticalAlignment(VerticalAlignment.MIDDLE)
				);
			}
		}

		document.Add(tablaLogoTitulo);

		if (!_request.NewData)
			document.Add(_tablaDatos);
		else
			document.Add(table);
	}

	private Image InsertLogo()
	{
		byte[] imageBytes;
		using (var magickImage = new MagickImage(_logoPath))
		{
			imageBytes = magickImage.ToByteArray(MagickFormat.Png);
		}

		iText.Layout.Element.Image image = new(iText.IO.Image.ImageDataFactory.Create(imageBytes));

		image.SetAutoScale(true);

		return image;
	}

	sealed class EndPageEventHandler : AbstractPdfDocumentEventHandler
	{
		public static void HandleEvent(iText.Kernel.Pdf.Event.PdfDocumentEvent @event)
		{
			PdfFont font = PdfFontFactory.CreateRegisteredFont("Helvetica");
			PdfDocumentEvent docEvent = @event;
			PdfPage page = docEvent.GetPage();

			PdfDictionary resources = new();
			resources.Put(PdfName.Font, font.GetPdfObject());

			if (page.GetResources() == null)
			{
				PdfResources pdfResources = new(resources);
				page.SetResources(pdfResources);
			}

			PdfCanvas pdfCanvas = new(page.NewContentStreamBefore(), page.GetResources(), docEvent.GetDocument());

			// Get page size
			Rectangle pageSize = page.GetPageSize();

			DateTime date = DateTime.Now;
			// Add page number at the bottom center of the page
			pdfCanvas.BeginText()
					.SetFontAndSize(font, 7)
					.MoveText(40, pageSize.GetBottom() + 20)
					.ShowText($"Creado por Servicio de Administrador de Catálogo el {date:dd/MM/yyyy}                                                                                                                                           Página {docEvent.GetDocument().GetPageNumber(page)}")
					.EndText();

			pdfCanvas.Release();
		}

		protected override void OnAcceptedEvent(AbstractPdfDocumentEvent @event)
		{
			HandleEvent((PdfDocumentEvent)@event);
		}
	}
}
