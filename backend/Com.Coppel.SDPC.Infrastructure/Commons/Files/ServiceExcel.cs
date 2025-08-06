using Com.Coppel.SDPC.Application.Commons.Files;
using Com.Coppel.SDPC.Application.Models.Reports;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using ImageMagick;

using System.Data;
using System.Reflection;

using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using A = DocumentFormat.OpenXml.Drawing;
namespace Com.Coppel.SDPC.Infrastructure.Commons.Files;

public class ServiceExcel : IServiceExcel
{
	private readonly string _basePath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)!.Replace("file:\\", "")!;
	private DataTable _table = new();
	private FileContentVM _request = new();

	public string CreateFile(FileContentVM request, string logoPath)
	{
		_request = request;
		_table = Utils.ConvertDataToTable(_request.ViewModelExcel, _request.DataExcel);

		string result = Utils.CheckFile(_request.PuntoDeConsumo, _request.TableName, _request.Area, true, _request.ExtraFile);

		using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(result, SpreadsheetDocumentType.Workbook))
		{
			WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
			workbookPart.Workbook = new Workbook();
			WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
			worksheetPart.Worksheet = new(new SheetData());
			Sheets sheets = spreadsheetDocument.WorkbookPart!.Workbook.AppendChild(new Sheets());
			Sheet sheet = new()
			{
				Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
				SheetId = 1,
				Name = "Sheet1"
			};
			sheets.AppendChild(sheet);

			WorkbookStylesPart stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
			stylesPart.Stylesheet = CreateStylesheet();
			stylesPart.Stylesheet.Save();

			SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>()!;

			AppendData(worksheetPart, sheetData);
			DefineMergedCells(worksheetPart, ["A1:B2", "C1:E1", "C2:E2"]);
			DefineStaticTitle(workbookPart, sheet.Name!);
			DefineFooter(worksheetPart);
			AppendImage(worksheetPart, @$"{_basePath}\\{logoPath}");
			AddTable(worksheetPart, $"A4:{GetLetter(_table.Columns.Count, _table.Columns.Count)}{_table.Rows.Count}");

			// Save the worksheet
			worksheetPart.Worksheet.Save();
		}

		return result;
	}

	private void AppendData(WorksheetPart worksheetPart, SheetData sheetData)
	{
		DocumentFormat.OpenXml.Spreadsheet.Columns columns = worksheetPart.Worksheet.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Columns>()!;
		if (columns == null)
		{
			columns = new DocumentFormat.OpenXml.Spreadsheet.Columns();
			worksheetPart.Worksheet.InsertAt(columns, 0);
		}

		for (int i = 0; i < _table.Columns.Count; i++)
		{
			DocumentFormat.OpenXml.Spreadsheet.Column column = new()
			{
				Min = 10, // The starting column index (1-based)
				Max = 30, // The ending column index (1-based)
				Width = 30,     // The width in terms of character count
				CustomWidth = true, // Indicates that the width is custom
			};

			columns.AppendChild(column);
		}

		Row headerRow1 = new() { RowIndex = 1 };
		headerRow1.AppendChild(CreateCell("A1", "", 0));
		headerRow1.AppendChild(CreateCell("B1", "", 0));
		headerRow1.AppendChild(CreateCell("C1", _request.PuntoDeConsumo.NomFuncionalidad, 0));
		headerRow1.AppendChild(CreateCell("D1", "", 0));
		headerRow1.AppendChild(CreateCell("E1", "", 0));
		sheetData.AppendChild(headerRow1);

		Row headerRow2 = new() { RowIndex = 2 };
		headerRow2.AppendChild(CreateCell("A2", "", 0));
		headerRow2.AppendChild(CreateCell("B2", "", 0));
		headerRow2.AppendChild(CreateCell("C2", $"Tabla: {_request.TableName}", 0));
		headerRow2.AppendChild(CreateCell("D2", "", 0));
		headerRow2.AppendChild(CreateCell("E2", "", 0));
		sheetData.AppendChild(headerRow2);

		Row headerRow3 = new() { RowIndex = 3 };
		headerRow3.AppendChild(CreateCell("A3", "", 0));
		headerRow3.AppendChild(CreateCell("B3", "", 0));
		headerRow3.AppendChild(CreateCell("C3", "", 0));
		headerRow3.AppendChild(CreateCell("D3", "", 0));
		headerRow3.AppendChild(CreateCell("E3", "", 0));
		sheetData.AppendChild(headerRow3);


		UInt32Value headerIndex = 4;
		Row headerRow = new()
		{
			RowIndex = headerIndex,
			CustomHeight = true,
			Height = 60
		};

		for (int i = 0; i < _table.Columns.Count; i++)
		{
			headerRow.AppendChild(CreateCell($"{GetLetter(i, _table.Columns.Count)}{headerIndex}", _table.Columns[i].ColumnName, 1));
		}
		sheetData.AppendChild(headerRow);

		UInt32Value counter = 5;
		foreach (DataRow row in _table.Rows)
		{
			Row dataRow = new() { RowIndex = counter };
			for (int i = 0; i < row.ItemArray.Length; i++)
			{
				dataRow.AppendChild(CreateCellFromDataColumn($"{GetLetter(i, _table.Columns.Count)}{counter}", _table.Columns[i], row.ItemArray[i]!.ToString()!, 2));
			}
			counter++;
			sheetData.AppendChild(dataRow);
		}
	}

	static Stylesheet CreateStylesheet()
	{
		Stylesheet stylesheet = new();

		Fonts fonts = new();
		fonts.AppendChild(new Font());

		Fills fills = new();
		fills.AppendChild(new Fill() { PatternFill = new PatternFill() { PatternType = PatternValues.None } }); // Default fill
		fills.AppendChild(new Fill() /// Header fill (background color)
		{
			PatternFill = new PatternFill()
			{
				PatternType = PatternValues.Solid,
				ForegroundColor = new ForegroundColor() { Rgb = "FFD3D3D3" } /// Light gray
			}
		});

		/// Borders
		Borders borders = new();
		borders.AppendChild(new Border() // Default border
		{
			LeftBorder = new LeftBorder(),
			RightBorder = new RightBorder(),
			TopBorder = new TopBorder(),
			BottomBorder = new BottomBorder(),
			DiagonalBorder = new DiagonalBorder()
		});
		borders.AppendChild(new Border() // Border with thin lines
		{
			LeftBorder = new LeftBorder() { Style = BorderStyleValues.Thin },
			RightBorder = new RightBorder() { Style = BorderStyleValues.Thin },
			TopBorder = new TopBorder() { Style = BorderStyleValues.Thin },
			BottomBorder = new BottomBorder() { Style = BorderStyleValues.Thin },
			DiagonalBorder = new DiagonalBorder()
		});

		uint formatId = 164; /// Custom format IDs start at 164
		NumberingFormat numberingFormat = new()
		{
			NumberFormatId = formatId,
			FormatCode = "dd/MM/yyyy"
		};

		stylesheet.NumberingFormats ??= new NumberingFormats();
		stylesheet.NumberingFormats.AppendChild(numberingFormat);

		/// CellFormats
		CellFormats cellFormats = new();
		cellFormats.AppendChild(new CellFormat() { FontId = 0, FillId = 0, BorderId = 0 }); // Default style
		cellFormats.AppendChild(new CellFormat() { FontId = 0, FillId = 1, BorderId = 0, ApplyFill = true }); // Header style
		cellFormats.AppendChild(new CellFormat() { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true }); // Border style
		cellFormats.AppendChild(new CellFormat() { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true, NumberFormatId = 0 });

		stylesheet.AppendChild(fonts);
		stylesheet.AppendChild(fills);
		stylesheet.AppendChild(borders);
		stylesheet.AppendChild(cellFormats);

		return stylesheet;
	}

	static Cell CreateCell(string cellReference, string text, uint styleIndex = 0)
	{
		Cell cell = new()
		{
			CellReference = cellReference,
			DataType = CellValues.InlineString,
			StyleIndex = styleIndex
		};

		InlineString inlineString = new();
		Text t = new() { Text = text };
		inlineString.AppendChild(t);
		cell.AppendChild(inlineString);

		return cell;
	}

	static Cell CreateCellFromDataColumn(string cellReference, DataColumn column, object value, uint styleIndex = 0)
	{
		Cell cell = new()
		{
			CellReference = cellReference,
			StyleIndex = column.DataType == typeof(DateTime) ? 3 : styleIndex,
		};

		List<Type> numberTypes =
		[
			typeof(uint),
			typeof(int),
			typeof(long),
			typeof(float),
			typeof(double),
			typeof(decimal),
		];

		if (column.DataType == typeof(String))
		{
			cell.DataType = CellValues.String;
			cell.CellValue = new CellValue(value.ToString()!);
		}

		if (column.DataType == typeof(DateTime))
		{
			cell.DataType = CellValues.Date;
		}

		if (numberTypes.Contains(column.DataType))
		{
			cell.DataType = CellValues.Number;
			cell.CellValue = new CellValue(value.ToString()!);
		}

		cell.CellValue = new CellValue(value.ToString()!);

		return cell;
	}

	static char GetLetter(int index, int totalColumns, bool isUppercase = true)
	{
		if (index < 0 || index > totalColumns)
		{
			throw new ArgumentOutOfRangeException(nameof(index), "Index must be between 0 and 25.");
		}

		return isUppercase ? (char)('A' + index) : (char)('a' + index);
	}

	private void AddTable(WorksheetPart worksheetPart, string range)
	{
		TableDefinitionPart tableDefinitionPart = worksheetPart.AddNewPart<TableDefinitionPart>();
		Table table = new()
		{
			Id = 1,
			Name = "Table1",
			DisplayName = "Table1",
			Reference = range,
			TotalsRowShown = false
		};

		TableColumns tableColumns = new() { Count = Convert.ToUInt32(_table.Columns.Count) };
		UInt32Value colIndex = 1;
		foreach (DataColumn column in _table.Columns)
		{
			tableColumns.AppendChild(new TableColumn() { Id = colIndex++, Name = column.ColumnName });
		}

		TableStyleInfo tableStyleInfo = new()
		{
			Name = "TableStyleMedium2",
			ShowFirstColumn = false,
			ShowLastColumn = false,
			ShowRowStripes = true,
			ShowColumnStripes = false
		};
		table.AppendChild(tableStyleInfo);

		tableDefinitionPart.Table = table;

		// Add the table to the worksheet
		Tables tables = new();
		tables.AppendChild(table);
		worksheetPart.Worksheet.AppendChild(tables);
	}

	private static void AppendImage(WorksheetPart worksheetPart, string imagePath)
	{
		using var image = new MagickImage(imagePath);
		image.Scale(400, 200);
		byte[] imageBytes = image.ToByteArray();
		AddImageToWorksheet(worksheetPart, imageBytes, "A1", 400, 200);
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3220:Method calls should not resolve ambiguously to overloads with \"params\"", Justification = "Pending research for this warning")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0057:Use range operator", Justification = "We need to make tests")]
	private static void AddImageToWorksheet(WorksheetPart worksheetPart, byte[] imageBytes, string cellReference, int width, int height)
	{
		// Add a DrawingsPart to the WorksheetPart
		DrawingsPart drawingsPart = worksheetPart.AddNewPart<DrawingsPart>();
		worksheetPart.Worksheet.AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Drawing()
		{
			Id = worksheetPart.GetIdOfPart(drawingsPart)
		});

		// Add an ImagePart to the DrawingsPart
		ImagePart imagePart = drawingsPart.AddImagePart(ImagePartType.Png);
		using (MemoryStream stream = new(imageBytes))
		{
			imagePart.FeedData(stream);
		}

		// Add a WorksheetDrawing to the DrawingsPart
		WorksheetDrawing worksheetDrawing = new();
		drawingsPart.WorksheetDrawing = worksheetDrawing;

		// Create the anchor for the image
		TwoCellAnchor twoCellAnchor = new(
				new DocumentFormat.OpenXml.Drawing.Spreadsheet.FromMarker
				{
					ColumnId = new Xdr.ColumnId((cellReference[0] - 'A').ToString()),
					RowId = new Xdr.RowId((int.Parse(cellReference.Substring(1)) - 1).ToString()),
					ColumnOffset = new DocumentFormat.OpenXml.Drawing.Spreadsheet.ColumnOffset("0"),
					RowOffset = new DocumentFormat.OpenXml.Drawing.Spreadsheet.RowOffset("0")
				},
				new DocumentFormat.OpenXml.Drawing.Spreadsheet.ToMarker
				{
					ColumnId = new Xdr.ColumnId((cellReference[0] - 'A' + 1).ToString()),
					RowId = new Xdr.RowId((int.Parse(cellReference.Substring(1))).ToString()),
					ColumnOffset = new DocumentFormat.OpenXml.Drawing.Spreadsheet.ColumnOffset("0"),
					RowOffset = new DocumentFormat.OpenXml.Drawing.Spreadsheet.RowOffset("0")
				},
				new DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture(
						new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureProperties(
								new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualDrawingProperties()
								{
									Id = 1U,
									Name = "Picture 1"
								},
								new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureDrawingProperties()
						),
						new DocumentFormat.OpenXml.Drawing.Spreadsheet.BlipFill(
								new DocumentFormat.OpenXml.Drawing.Blip()
								{
									Embed = drawingsPart.GetIdOfPart(imagePart),
									CompressionState = DocumentFormat.OpenXml.Drawing.BlipCompressionValues.Print
								},
								new A.Stretch(
										new DocumentFormat.OpenXml.Drawing.FillRectangle()
								)
						),
						new DocumentFormat.OpenXml.Drawing.Spreadsheet.ShapeProperties(
								new DocumentFormat.OpenXml.Drawing.Transform2D(
										new DocumentFormat.OpenXml.Drawing.Offset()
										{
											X = 0L,
											Y = 0L
										},
										new DocumentFormat.OpenXml.Drawing.Extents()
										{
											Cx = ((long)width * 9525) * 5,
											Cy = ((long)height * 9525) * 5
										}
								),
								new DocumentFormat.OpenXml.Drawing.PresetGeometry(
										new DocumentFormat.OpenXml.Drawing.AdjustValueList()
								)
								{
									Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle
								}
						)
				),
				new DocumentFormat.OpenXml.Drawing.Spreadsheet.ClientData()
		);

		worksheetDrawing.AppendChild(twoCellAnchor);
	}

	private static void DefineMergedCells(WorksheetPart worksheetPart, List<string> mergeCellRanges)
	{
		MergeCells mergeCells = new();
		foreach (var range in mergeCellRanges)
		{
			mergeCells.AppendChild(new MergeCell() { Reference = range });
		}
		worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<SheetData>().First());
	}

	private static void DefineStaticTitle(WorkbookPart workbookPart, string sheetName)
	{
		Workbook workbook = workbookPart.Workbook;
		workbook.DefinedNames ??= new DefinedNames();

		// Define a range for the title rows. This example uses the first row ("Sheet1!$1:$1").
		string titleRange = $"{sheetName}!$1:$5";

		// Create a DefinedName for the print title (this tells Excel to repeat the first row as titles on each page)
		DefinedName definedName = new()
		{
			Name = "_xlnm.Print_Titles",
			Text = titleRange,
			LocalSheetId = 0
		};

		// Add the defined name to the workbook's DefinedNames collection
		workbook.DefinedNames.AppendChild(definedName);
	}

	private static void DefineFooter(WorksheetPart worksheetPart)
	{
		worksheetPart.Worksheet.AppendChild(new PageSetup()
		{
			Orientation = DocumentFormat.OpenXml.Spreadsheet.OrientationValues.Portrait, // Set to Landscape
			PaperSize = (UInt32Value)1, // Paper size: A4 (9 = A4, 1 = Letter)
			FitToWidth = 1, // Fit to 1 page wide
			FitToHeight = 1, // Fit to 1 page tall
		});

		HeaderFooter headerFooter = new()
		{
			OddFooter = new OddFooter() { Text = $"&LCreado el &D&RPágina &P de &N" }
		};
		worksheetPart.Worksheet.AppendChild(headerFooter);
	}
}
