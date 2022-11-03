using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using WinFormsControlLibrarySergeev.UnvisualComponents.HelperEnums;
using WinFormsControlLibrarySergeev.UnvisualComponents.HelperModels;
using Font = DocumentFormat.OpenXml.Spreadsheet.Font;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Drawing;
using Fonts = DocumentFormat.OpenXml.Spreadsheet.Fonts;
using Text = DocumentFormat.OpenXml.Spreadsheet.Text;
using BottomBorder = DocumentFormat.OpenXml.Spreadsheet.BottomBorder;
using TopBorder = DocumentFormat.OpenXml.Spreadsheet.TopBorder;
using RightBorder = DocumentFormat.OpenXml.Spreadsheet.RightBorder;
using LeftBorder = DocumentFormat.OpenXml.Spreadsheet.LeftBorder;
using PatternFill = DocumentFormat.OpenXml.Spreadsheet.PatternFill;
using FontScheme = DocumentFormat.OpenXml.Spreadsheet.FontScheme;
using Orientation = DocumentFormat.OpenXml.Drawing.Charts.Orientation;
using OrientationValues = DocumentFormat.OpenXml.Drawing.Charts.OrientationValues;
using NumberingFormat = DocumentFormat.OpenXml.Drawing.Charts.NumberingFormat;
using Values = DocumentFormat.OpenXml.Drawing.Charts.Values;
using NonVisualDrawingProperties = DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualDrawingProperties;
using NonVisualPictureDrawingProperties = DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureDrawingProperties;
using NonVisualPictureProperties = DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureProperties;
using BlipFill = DocumentFormat.OpenXml.Drawing.Spreadsheet.BlipFill;
using ShapeProperties = DocumentFormat.OpenXml.Drawing.Spreadsheet.ShapeProperties;
using Picture = DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture;
using Position = DocumentFormat.OpenXml.Drawing.Spreadsheet.Position;
using GraphicFrame = DocumentFormat.OpenXml.Drawing.Spreadsheet.GraphicFrame;
using Run = DocumentFormat.OpenXml.Spreadsheet.Run;
using RunProperties = DocumentFormat.OpenXml.Drawing.RunProperties;
using Outline = DocumentFormat.OpenXml.Drawing.Outline;
using Fill = DocumentFormat.OpenXml.Spreadsheet.Fill;

namespace WinFormsControlLibrarySergeev.UnvisualComponents
{
    public class SaveToExcel : AbstractSaveToExcel
    {
        private SpreadsheetDocument _spreadsheetDocument;

        private SharedStringTablePart _shareStringPart;

        private Worksheet _worksheet;

        /// <summary>
        /// Настройка стилей для файла
        /// </summary>
        /// <param name="workbookpart"></param>
        private static void CreateStyles(WorkbookPart workbookpart)
        {
            var sp = workbookpart.AddNewPart<WorkbookStylesPart>();
            sp.Stylesheet = new Stylesheet();
            var fonts = new Fonts() { Count = 2U, KnownFonts = true };
            var fontUsual = new Font();
            fontUsual.Append(new FontSize() { Val = 12D });
            fontUsual.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Theme = 1U
            });
            fontUsual.Append(new FontName() { Val = "Times New Roman" });
            fontUsual.Append(new FontFamilyNumbering() { Val = 2 });
            fontUsual.Append(new FontScheme() { Val = FontSchemeValues.Minor });

            var fontTitle = new Font();
            fontTitle.Append(new Bold());
            fontTitle.Append(new FontSize() { Val = 14D });
            fontTitle.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Theme = 1U
            });
            fontTitle.Append(new FontName() { Val = "Times New Roman" });
            fontTitle.Append(new FontFamilyNumbering() { Val = 2 });
            fontTitle.Append(new FontScheme() { Val = FontSchemeValues.Minor });

            fonts.Append(fontUsual);
            fonts.Append(fontTitle);

            var fills = new Fills() { Count = 2U };

            var fill1 = new Fill();
            fill1.Append(new PatternFill() { PatternType = PatternValues.None });

            var fill2 = new Fill();
            fill2.Append(new PatternFill() { PatternType = PatternValues.Gray125 });

            fills.Append(fill1);
            fills.Append(fill2);

            var borders = new Borders() { Count = 2U };
            var borderNoBorder = new Border();
            borderNoBorder.Append(new LeftBorder());
            borderNoBorder.Append(new RightBorder());
            borderNoBorder.Append(new TopBorder());
            borderNoBorder.Append(new BottomBorder());
            borderNoBorder.Append(new DiagonalBorder());

            var borderThin = new Border();

            var leftBorder = new LeftBorder() { Style = BorderStyleValues.Thin };
            leftBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Indexed = 64U
            });

            var rightBorder = new RightBorder() { Style = BorderStyleValues.Thin };
            rightBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Indexed = 64U
            });

            var topBorder = new TopBorder() { Style = BorderStyleValues.Thin };
            topBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Indexed = 64U
            });

            var bottomBorder = new BottomBorder() { Style = BorderStyleValues.Thin };
            bottomBorder.Append(new DocumentFormat.OpenXml.Office2010.Excel.Color()
            {
                Indexed = 64U
            });

            borderThin.Append(leftBorder);
            borderThin.Append(rightBorder);
            borderThin.Append(topBorder);
            borderThin.Append(bottomBorder);
            borderThin.Append(new DiagonalBorder());

            borders.Append(borderNoBorder);
            borders.Append(borderThin);

            var cellStyleFormats = new CellStyleFormats() { Count = 1U };
            var cellFormatStyle = new CellFormat()
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U
            };

            cellStyleFormats.Append(cellFormatStyle);

            var cellFormats = new CellFormats() { Count = 3U };
            var cellFormatFont = new CellFormat()
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                ApplyFont = true
            };

            var cellFormatFontAndBorder = new CellFormat()
            {
                NumberFormatId = 0U,
                FontId = 0U,
                FillId = 0U,
                BorderId = 1U,
                FormatId = 0U,
                ApplyFont = true,
                ApplyBorder = true
            };

            var cellFormatTitle = new CellFormat()
            {
                NumberFormatId = 0U,
                FontId = 1U,
                FillId = 0U,
                BorderId = 0U,
                FormatId = 0U,
                Alignment = new Alignment()
                {
                    Vertical = VerticalAlignmentValues.Center,
                    WrapText = true,
                    Horizontal = HorizontalAlignmentValues.Center
                },
                ApplyFont = true
            };

            cellFormats.Append(cellFormatFont);
            cellFormats.Append(cellFormatFontAndBorder);
            cellFormats.Append(cellFormatTitle);

            var cellStyles = new CellStyles() { Count = 1U };

            cellStyles.Append(new CellStyle()
            {
                Name = "Normal",
                FormatId = 0U,
                BuiltinId = 0U
            });

            var differentialFormats = new
            DocumentFormat.OpenXml.Office2013.Excel.DifferentialFormats()
            { Count = 0U };

            var tableStyles = new TableStyles()
            {
                Count = 0U,
                DefaultTableStyle = "TableStyleMedium2",
                DefaultPivotStyle = "PivotStyleLight16"
            };

            var stylesheetExtensionList = new StylesheetExtensionList();
            var stylesheetExtension1 = new StylesheetExtension()
            {
                Uri = "{EB79DEF2-80B8-43e5 - 95BD - 54CBDDF9020C}"
            };
            stylesheetExtension1.AddNamespaceDeclaration("x14",
            "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            stylesheetExtension1.Append(new SlicerStyles()
            {
                DefaultSlicerStyle = "SlicerStyleLight1"
            });

            var stylesheetExtension2 = new StylesheetExtension()
            {
                Uri = "{9260A510-F301-46a8 - 8635 - F512D64BE5F5}"
            };
            stylesheetExtension2.AddNamespaceDeclaration("x15",
            "http://schemas.microsoft.com/office/spreadsheetml/2010/11/main");
            stylesheetExtension2.Append(new TimelineStyles()
            {
                DefaultTimelineStyle = "TimeSlicerStyleLight1"
            });

            stylesheetExtensionList.Append(stylesheetExtension1);
            stylesheetExtensionList.Append(stylesheetExtension2);
            sp.Stylesheet.Append(fonts);
            sp.Stylesheet.Append(fills);
            sp.Stylesheet.Append(borders);
            sp.Stylesheet.Append(cellStyleFormats);
            sp.Stylesheet.Append(cellFormats);
            sp.Stylesheet.Append(cellStyles);
            sp.Stylesheet.Append(differentialFormats);
            sp.Stylesheet.Append(tableStyles);
            sp.Stylesheet.Append(stylesheetExtensionList);
        }

        /// <summary>
        /// Получение номера стиля из типа
        /// </summary>
        /// <param name="styleInfo"></param>
        /// <returns></returns>
        private static uint GetStyleValue(ExcelStyleInfoType styleInfo)
        {
            return styleInfo switch
            {
                ExcelStyleInfoType.Title => 2U,
                ExcelStyleInfoType.TextWithBorder => 1U,
                ExcelStyleInfoType.Text => 0U,
                _ => 0U,
            };
        }

        protected override void CreateExcel(ExcelInfo info)
        {
            _spreadsheetDocument = SpreadsheetDocument.Create(info.FileName,
            SpreadsheetDocumentType.Workbook);
            // Создаем книгу (в ней хранятся листы)
            var workbookpart = _spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();
            CreateStyles(workbookpart);

            // Получаем/создаем хранилище текстов для книги
            _shareStringPart =
            _spreadsheetDocument.WorkbookPart
            .GetPartsOfType<SharedStringTablePart>().Any() ?

            _spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First()
            : _spreadsheetDocument.WorkbookPart.AddNewPart<SharedStringTablePart>();

            // Создаем SharedStringTable, если его нет
            if (_shareStringPart.SharedStringTable == null)
            {
                _shareStringPart.SharedStringTable = new SharedStringTable();
            }

            // Создаем лист в книгу
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Добавляем лист в книгу
            var sheets = _spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new
            Sheets());
            var sheet = new Sheet()
            {
                Id = _spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = info.Title
            };
            sheets.Append(sheet);
            _worksheet = worksheetPart.Worksheet;
        }

        protected override void InsertCellInWorksheet(ExcelCellParameters excelParams)
        {
            var sheetData = _worksheet.GetFirstChild<SheetData>();
            // Ищем строку, либо добавляем ее
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex ==
            excelParams.RowIndex).Any())
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex ==
                excelParams.RowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = excelParams.RowIndex };
                sheetData.Append(row);
            }
            // Ищем нужную ячейку
            Cell cell;
            if (row.Elements<Cell>().Where(c => c.CellReference.Value ==
            excelParams.CellReference).Any())
            {
                cell = row.Elements<Cell>().Where(c => c.CellReference.Value ==
                excelParams.CellReference).First();
            }
            else
            {
                // Все ячейки должны быть последовательно друг за другом расположены
                // нужно определить, после какой вставлять
                Cell refCell = null;
                foreach (Cell rowCell in row.Elements<Cell>())
                {
                    if (string.Compare(rowCell.CellReference.Value,
                    excelParams.CellReference, true) > 0)
                    {
                        refCell = rowCell;
                        break;
                    }
                }
                var newCell = new Cell() { CellReference = excelParams.CellReference };
                row.InsertBefore(newCell, refCell);
                cell = newCell;
            }
            // вставляем новый текст
            _shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new
            Text(excelParams.Text)));
            _shareStringPart.SharedStringTable.Save();
            cell.CellValue = new
            CellValue((_shareStringPart.SharedStringTable.Elements<SharedStringItem>().Count() -
            1).ToString());
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            cell.StyleIndex = GetStyleValue(excelParams.StyleInfo);
        }

        private void InsertImage(long x, long y, long? width, long? height, byte[] sImage)
        {
            try
            {
                WorksheetPart wsp = _worksheet.WorksheetPart;
                DrawingsPart dp;
                ImagePart imgp;
                WorksheetDrawing wsd;

                //ImagePartType ipt;
                //switch (sImagePath.Substring(sImagePath.LastIndexOf('.') + 1).ToLower())
                //{
                //    case "png":
                //        ipt = ImagePartType.Png;
                //        break;
                //    case "jpg":
                //    case "jpeg":
                //        ipt = ImagePartType.Jpeg;
                //        break;
                //    case "gif":
                //        ipt = ImagePartType.Gif;
                //        break;
                //    default:
                //        return;
                //}

                if (wsp.DrawingsPart == null)
                {
                    //----- no drawing part exists, add a new one

                    dp = wsp.AddNewPart<DrawingsPart>();
                    imgp = dp.AddImagePart(ImagePartType.Jpeg, wsp.GetIdOfPart(dp));
                    wsd = new WorksheetDrawing();
                }
                else
                {
                    //----- use existing drawing part

                    dp = wsp.DrawingsPart;
                    imgp = dp.AddImagePart(ImagePartType.Jpeg);
                    dp.CreateRelationshipToPart(imgp);
                    wsd = dp.WorksheetDrawing;
                }

                using (MemoryStream ms = new MemoryStream(sImage))
                {
                    imgp.FeedData(ms);
                }

                //using (FileStream fs = new FileStream(sImage, FileMode.Open))
                //{
                //    imgp.FeedData(fs);
                //}

                int imageNumber = dp.ImageParts.Count();
                if (imageNumber == 1)
                {
                    Drawing drawing = new Drawing();
                    drawing.Id = dp.GetIdOfPart(imgp);
                    _worksheet.Append(drawing);
                }

                NonVisualDrawingProperties nvdp = new NonVisualDrawingProperties();
                nvdp.Id = new UInt32Value((uint)(1024 + imageNumber));
                nvdp.Name = "Picture " + imageNumber.ToString();
                nvdp.Description = "";
                PictureLocks picLocks = new PictureLocks();
                picLocks.NoChangeAspect = true;
                picLocks.NoChangeArrowheads = true;
                NonVisualPictureDrawingProperties nvpdp = new NonVisualPictureDrawingProperties();
                nvpdp.PictureLocks = picLocks;
                NonVisualPictureProperties nvpp = new NonVisualPictureProperties();
                nvpp.NonVisualDrawingProperties = nvdp;
                nvpp.NonVisualPictureDrawingProperties = nvpdp;

                Stretch stretch = new Stretch();
                stretch.FillRectangle = new FillRectangle();

                BlipFill blipFill = new BlipFill();
                Blip blip = new Blip();
                blip.Embed = dp.GetIdOfPart(imgp);
                blip.CompressionState = BlipCompressionValues.Print;
                blipFill.Blip = blip;
                blipFill.SourceRectangle = new SourceRectangle();
                blipFill.Append(stretch);

                Transform2D t2d = new Transform2D();
                Offset offset = new Offset();
                offset.X = 0;
                offset.Y = 0;
                t2d.Offset = offset;
                Bitmap bm;
                using (MemoryStream ms = new MemoryStream(sImage))
                {
                    bm = (Bitmap)Image.FromStream(ms);
                }

                Extents extents = new Extents();

                if (width == null)
                    extents.Cx = bm.Width * (long)(914400 / bm.HorizontalResolution);
                else
                    extents.Cx = width;

                if (height == null)
                    extents.Cy = bm.Height * (long)(914400 / bm.VerticalResolution);
                else
                    extents.Cy = height;

                bm.Dispose();
                t2d.Extents = extents;
                ShapeProperties sp = new ShapeProperties();
                sp.BlackWhiteMode = BlackWhiteModeValues.Auto;
                sp.Transform2D = t2d;
                PresetGeometry prstGeom = new PresetGeometry();
                prstGeom.Preset = ShapeTypeValues.Rectangle;
                prstGeom.AdjustValueList = new AdjustValueList();
                sp.Append(prstGeom);
                sp.Append(new NoFill());

                Picture picture = new Picture();
                picture.NonVisualPictureProperties = nvpp;
                picture.BlipFill = blipFill;
                picture.ShapeProperties = sp;

                Position pos = new Position();
                pos.X = x;
                pos.Y = y;
                Extent ext = new Extent();
                ext.Cx = extents.Cx;
                ext.Cy = extents.Cy;
                AbsoluteAnchor anchor = new AbsoluteAnchor();
                anchor.Position = pos;
                anchor.Extent = ext;
                anchor.Append(picture);
                anchor.Append(new ClientData());
                wsd.Append(anchor);
                wsd.Save(dp);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void InsertImageExcel(ExcelImageParameters excelImageParameters)
        {
            InsertImage(excelImageParameters.X, excelImageParameters.Y, null, null, excelImageParameters.Image);
        }

        protected override void InsertChartPieExcel(ExcelPieChartParameters excelParams)
        {
            int x = 0, y = 0, kx = 0, ky = 0;
            switch (excelParams.Legend)
            {
                case ExcelLegendLocation.Left:
                    x = 9;
                    y = 3;
                    kx = 25;
                    ky = 11;
                    break;
                case ExcelLegendLocation.Right:
                    x = 9;
                    y = 10;
                    kx = 25;
                    ky = 18;
                    break;
                case ExcelLegendLocation.Top:
                    x = 2;
                    y = 7;
                    kx = 18;
                    ky = 15;
                    break;
                case ExcelLegendLocation.Bottom:
                    x = 18;
                    y = 7;
                    kx = 34;
                    ky = 15;
                    break;
            }
            InsertPieChartInSpreadSheet(excelParams.ChartName, excelParams.LegendData, x, y, kx, ky);
        }

        private void InsertPieChartInSpreadSheet(string chartTitle,
            Dictionary<string, decimal> data, int startRowIndex, int startColumnIndex, int endRowIndex,
            int endColumnIndex)
        {
            WorksheetPart ws = _worksheet.WorksheetPart;
            DrawingsPart drawingsPart = ws.AddNewPart<DrawingsPart>();

            ws.Worksheet.Append(new Drawing()
            {
                Id = ws.GetIdOfPart(drawingsPart)
            });

            ChartPart chartPart = drawingsPart.AddNewPart<ChartPart>();


            ChartSpace chartSpace = new ChartSpace();
            chartSpace.Append(new EditingLanguage() { Val = new StringValue("en-US") });
            C.Chart chart = chartSpace.AppendChild(
                new C.Chart());

            PlotArea plotArea = chart.AppendChild(new PlotArea());
            Layout layout = plotArea.AppendChild(new Layout());

            ManualLayout manualLayout1 = new ManualLayout();
            LayoutTarget layoutTarget1 = new LayoutTarget() { Val = LayoutTargetValues.Inner };
            LeftMode leftMode1 = new LeftMode() { Val = LayoutModeValues.Edge };
            TopMode topMode1 = new TopMode() { Val = LayoutModeValues.Edge };
            Left left1 = new Left() { Val = 0.5D };
            Top top1 = new Top() { Val = 0.2D };

            Width width1 = new Width() { Val = 0.95622038461448768D };
            Height height1 = new Height() { Val = 0.54928769841269842D };

            manualLayout1.Append(layoutTarget1);
            manualLayout1.Append(leftMode1);
            manualLayout1.Append(topMode1);
            manualLayout1.Append(left1);
            manualLayout1.Append(top1);
            manualLayout1.Append(width1);
            manualLayout1.Append(height1);



            layout.Append(manualLayout1);

            NoFill noFill = new NoFill();
            C.ShapeProperties shapeProperties = new C.ShapeProperties();

            Outline outline15 = new Outline();
            SolidFill noFill17 = new SolidFill();

            RgbColorModelHex schemeColor29 = new RgbColorModelHex() { Val = "FFFFFF" };

            noFill17.Append(schemeColor29);
            outline15.Append(noFill17);

            shapeProperties.Append(noFill);
            shapeProperties.Append(outline15);
            plotArea.Append(shapeProperties);


            PieChart pieChart = plotArea.AppendChild(new PieChart());

            PieChartSeries pieChartSeries = pieChart.AppendChild(new PieChartSeries(
                new C.Index() { Val = (UInt32Value)0U },
                new Order() { Val = (UInt32Value)0U },
                new SeriesText(new NumericValue() { Text = "PieChartSeries" })));

            CategoryAxisData catAx = new CategoryAxisData();


            StringReference stringReference = new StringReference();
            StringCache stringCache = new StringCache();

            PointCount pointCount = new PointCount() { Val = (uint)data.Count };

            stringCache.Append(pointCount);

            uint i = 0;
            foreach (var key in data.Keys)
            {
                stringCache.AppendChild(new StringPoint() { Index = new UInt32Value(i) }).Append(new NumericValue(key));
                i++;
            }

            stringReference.Append(stringCache);
            catAx.Append(stringReference);
            pieChartSeries.Append(catAx);



            Values values = new Values();
            NumberReference numberReference = new NumberReference();
            NumberingCache numberingCache = new NumberingCache();

            i = 0;
            foreach (var key in data.Keys)
            {
                numberingCache.AppendChild(new NumericPoint() { Index = new UInt32Value(i) }).Append(new NumericValue(data[key].ToString()));
                i++;
            }

            numberReference.Append(numberingCache);
            values.Append(numberReference);
            pieChartSeries.Append(values);

            AddChartTitle(chart, chartTitle);
            pieChart.Append(new AxisId() { Val = new UInt32Value(48650112u) });
            pieChart.Append(new AxisId() { Val = new UInt32Value(48672768u) });


            CategoryAxis catAx1 = plotArea.AppendChild<CategoryAxis>(new CategoryAxis(new AxisId()
            { Val = new UInt32Value(48650112u) }, new Scaling(new Orientation()
            {
                Val = new EnumValue<OrientationValues>(OrientationValues.MinMax)
            }),
                 new AxisPosition() { Val = new EnumValue<AxisPositionValues>(AxisPositionValues.Bottom) },
                 new TickLabelPosition() { Val = new EnumValue<TickLabelPositionValues>(TickLabelPositionValues.NextTo) },
                 new CrossingAxis() { Val = new UInt32Value(48672768U) },
                 new Crosses() { Val = new EnumValue<CrossesValues>(CrossesValues.AutoZero) },
                 new AutoLabeled() { Val = new BooleanValue(true) },
                 new LabelAlignment() { Val = new EnumValue<LabelAlignmentValues>(LabelAlignmentValues.Center) },
                 new LabelOffset() { Val = new UInt16Value((ushort)100) }));




            // Add the Value Axis.
            ValueAxis valAx = plotArea.AppendChild(new ValueAxis(new AxisId() { Val = new UInt32Value(48672768u) },
                new Scaling(new Orientation()
                {
                    Val = new EnumValue<OrientationValues>(
                    OrientationValues.MinMax)
                }),
                new AxisPosition() { Val = new EnumValue<AxisPositionValues>(AxisPositionValues.Left) },
                new MajorGridlines(),
                new NumberingFormat()
                {
                    FormatCode = new StringValue("General"),
                    SourceLinked = new BooleanValue(true)
                }, new TickLabelPosition()
                {
                    Val = new EnumValue<TickLabelPositionValues>
                    (TickLabelPositionValues.NextTo)
                }, new CrossingAxis() { Val = new UInt32Value(48650112U) },
                new Crosses() { Val = new EnumValue<CrossesValues>(CrossesValues.AutoZero) },
                new CrossBetween() { Val = new EnumValue<CrossBetweenValues>(CrossBetweenValues.Between) }));


            // Add the chart Legend.
            Legend legend = chart.AppendChild<Legend>(new Legend(new LegendPosition() { Val = new EnumValue<LegendPositionValues>(LegendPositionValues.Bottom) },
                new Layout()));

            chart.Append(new PlotVisibleOnly() { Val = new BooleanValue(true) });

            chartPart.ChartSpace = chartSpace;

            PositionChart(chartPart, drawingsPart, startRowIndex, startColumnIndex, endRowIndex, endColumnIndex);
        }

        private void PositionChart(ChartPart chartPart, DrawingsPart drawingsPart, int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex)
        {
            // Position the chart on the worksheet using a TwoCellAnchor object.
            drawingsPart.WorksheetDrawing = new WorksheetDrawing();
            TwoCellAnchor twoCellAnchor = drawingsPart.WorksheetDrawing.AppendChild<TwoCellAnchor>(new TwoCellAnchor());
            twoCellAnchor.Append(new DocumentFormat.OpenXml.Drawing.Spreadsheet.FromMarker(new ColumnId(startColumnIndex.ToString()),
                                            new ColumnOffset("581025"),
                                            new RowId(startRowIndex.ToString()),
                                            new RowOffset("114300")));
            twoCellAnchor.Append(new DocumentFormat.OpenXml.Drawing.Spreadsheet.ToMarker(new ColumnId(endColumnIndex.ToString()),
                new ColumnOffset("276225"),
                new RowId(endRowIndex.ToString()),
                new RowOffset("0")));

            // Append a GraphicFrame to the TwoCellAnchor object.
            GraphicFrame graphicFrame =
                twoCellAnchor.AppendChild<GraphicFrame>(new GraphicFrame());
            graphicFrame.Macro = "";

            graphicFrame.Append(new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualGraphicFrameProperties(
                new NonVisualDrawingProperties() { Id = new UInt32Value(2u), Name = "Chart 1" },
                new DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualGraphicFrameDrawingProperties()));

            graphicFrame.Append(new Transform(new Offset() { X = 0L, Y = 0L },
                                                                    new Extents() { Cx = 0L, Cy = 0L }));

            graphicFrame.Append(new Graphic(new GraphicData(new ChartReference() { Id = drawingsPart.GetIdOfPart(chartPart) })
            { Uri = "http://schemas.openxmlformats.org/drawingml/2006/chart" }));

            twoCellAnchor.Append(new ClientData());
        }

        private void AddChartTitle(C.Chart chart, string title)
        {
            var ctitle = chart.AppendChild(new Title());
            var chartText = ctitle.AppendChild(new ChartText());
            var richText = chartText.AppendChild(new RichText());

            var bodyPr = richText.AppendChild(new BodyProperties());
            var lstStyle = richText.AppendChild(new ListStyle());
            var paragraph = richText.AppendChild(new Paragraph());

            var apPr = paragraph.AppendChild(new ParagraphProperties());
            apPr.AppendChild(new DefaultRunProperties());

            var run = paragraph.AppendChild(new Run());
            run.AppendChild(new RunProperties() { Language = "en-CA" });
            run.AppendChild(new Text() { Text = title });
        }

        protected override void MergeCells(ExcelMergeParameters excelParams)
        {
            MergeCells mergeCells;
            if (_worksheet.Elements<MergeCells>().Any())
            {
                mergeCells = _worksheet.Elements<MergeCells>().First();
            }
            else
            {
                mergeCells = new MergeCells();
                if (_worksheet.Elements<CustomSheetView>().Any())
                {
                    _worksheet.InsertAfter(mergeCells,
                   _worksheet.Elements<CustomSheetView>().First());
                }
                else
                {
                    _worksheet.InsertAfter(mergeCells,
                   _worksheet.Elements<SheetData>().First());
                }
            }
            var mergeCell = new MergeCell()
            {
                Reference = new StringValue(excelParams.Merge)
            };
            mergeCells.Append(mergeCell);
        }
        protected override void SaveExcel(ExcelInfo info)
        {
            _spreadsheetDocument.WorkbookPart.Workbook.Save();
            _spreadsheetDocument.Close();
        }
    }
}
