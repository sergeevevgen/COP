using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using WinFormsControlLibrarySergeev.UnvisualComponentsK.HelperModels;

namespace WinFormsControlLibrarySergeev.UnvisualComponentsK
{
    public partial class WordCustomizableTableComponent : Component
    {
        public WordCustomizableTableComponent()
        {
            InitializeComponent();
        }

        public WordCustomizableTableComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void Report<T>(string fileName, string title, List<WordTitleColumn> titleColumns, List<WordMergedTitleColumn> mergedTitleColumns, List<T> data)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(title) || titleColumns.Count == 0 || data.Count == 0)
            {
                throw new Exception("Поля не заполнены");
            }
            if (!Check(titleColumns, mergedTitleColumns, data))
            {
                throw new Exception("Поля заполнены не верно");
            }
            CreateDoc(fileName, title, titleColumns, mergedTitleColumns, data);
        }
        private void CreateDoc<T>(string FileName, string Title, List<WordTitleColumn> titleColumns, List<WordMergedTitleColumn> mergedTitleColumns, List<T> data)
        {
            using (WordprocessingDocument wordDocument =
            WordprocessingDocument.Create(FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { (Title, new
                    WordTextProperties {Bold = true, Size = "28", } ) },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));

                Table table = new Table();
                TableProperties props = new TableProperties(
                new TableBorders(
                new TopBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 6
                },
                new BottomBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 6
                },
                new LeftBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 6
                },
                new RightBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 6
                },
                new InsideHorizontalBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 6
                },
                new InsideVerticalBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 6
                }));

                table.AppendChild<TableProperties>(props);
                TableRow tr = new TableRow();

                List<TableCell> firstRowCells = new List<TableCell>();
                List<TableCell> secondRowCells = new List<TableCell>();
                bool[] mergedCells = new bool[titleColumns.Count];
                for (int i = 0; i < mergedCells.Length; i++)
                {
                    mergedCells[i] = false;
                }
                foreach (var col in mergedTitleColumns)
                {
                    foreach (int index in col.Columns)
                    {
                        mergedCells[index] = true;
                    }
                }
                int p = 0;
                foreach (var col in titleColumns)
                {
                    TableCell tCellFirst = new TableCell();
                    firstRowCells.Add(tCellFirst);
                    TableCell tCellSecond = new TableCell();
                    secondRowCells.Add(tCellSecond);
                    tCellFirst.TableCellProperties = new TableCellProperties();
                    tCellSecond.TableCellProperties = new TableCellProperties();
                    if (!mergedCells[p])
                    {
                        tCellFirst.TableCellProperties.VerticalMerge = new VerticalMerge { Val = MergedCellValues.Restart };
                        tCellSecond.TableCellProperties.VerticalMerge = new VerticalMerge { Val = MergedCellValues.Continue };
                    }
                    else
                    {
                        tCellFirst.TableCellProperties.VerticalMerge = new VerticalMerge { Val = MergedCellValues.Restart };
                        tCellSecond.TableCellProperties.VerticalMerge = new VerticalMerge { Val = MergedCellValues.Restart };
                    }

                    if (mergedCells[p] && ((p > 0 && !mergedCells[p - 1]) || (p == 0)))
                    {
                        tCellFirst.TableCellProperties.HorizontalMerge = new HorizontalMerge { Val = MergedCellValues.Restart };
                    }
                    else if (mergedCells[p])
                    {
                        tCellFirst.TableCellProperties.HorizontalMerge = new HorizontalMerge { Val = MergedCellValues.Continue };
                    }
                    else
                    {
                        tCellFirst.TableCellProperties.HorizontalMerge = new HorizontalMerge { Val = MergedCellValues.Restart };

                    }
                    if (col.Equals(titleColumns.First()) && !mergedCells[p])
                    {
                        tCellFirst.TableCellProperties.HorizontalMerge = new HorizontalMerge { Val = MergedCellValues.Restart };

                    }
                    p++;
                }
                int h = 0;
                for (int i = 0; i < firstRowCells.Count; i++)
                {
                    bool mer = mergedCells[i];
                    TableCell tc = firstRowCells[i];

                    if (mergedCells[i] && ((i + 1 < firstRowCells.Count && mergedCells[i + 1]) || (i + 1 >= firstRowCells.Count)))
                    {
                        tc.Append(new Paragraph(new Run(new Text(mergedTitleColumns[h].Title))));
                    }
                    else if (mergedCells[i] && (i + 1 < firstRowCells.Count && !mergedCells[i + 1]))
                    {
                        tc.Append(new Paragraph(new Run(new Text(mergedTitleColumns[h].Title))));
                        h++;
                    }
                    else
                    {
                        tc.Append(new Paragraph(new Run(new Text(titleColumns[i].Name))));
                    }


                    tr.Append(tc);
                }
                table.Append(tr);
                tr = new TableRow();
                for (int i = 0; i < secondRowCells.Count; i++)
                {
                    TableCell tc = secondRowCells[i];
                    tc.Append(new Paragraph(new Run(new Text(titleColumns[i].Name))));
                    tr.Append(tc);
                }
                table.Append(tr);


                foreach (var d in data)
                {
                    tr = new TableRow();

                    foreach (var col in titleColumns)
                    {
                        Type type = d.GetType();

                        PropertyInfo pr;
                        FieldInfo fi;
                        string val = "";
                        if (col.PropertyInfo != null)
                        {
                            pr = col.PropertyInfo;
                            val = type.GetProperty(pr.Name).GetValue(d).ToString();
                        }
                        else
                        {
                            fi = col.FieldInfo;
                            val = type.GetField(fi.Name).GetValue(d).ToString();
                        }
                        TableCell tc = new TableCell();
                        tc.Append(new Paragraph(new Run(new Text(val))));
                        tr.Append(tc);
                    }
                    table.Append(tr);
                }

                docBody.Append(table);
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }
        //Настройки страницы
        private SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }
        //Создание абзаца
        private Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();

                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                foreach (var run in paragraph.Texts)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = run.Item2.Size });
                    if (run.Item2.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text
                    {
                        Text = run.Item1,
                        Space =
                   SpaceProcessingModeValues.Preserve
                    });
                    docParagraph.AppendChild(docRun);
                }
                return docParagraph;
            }
            return null;
        }
        //Форматирование для абзаца
        private ParagraphProperties CreateParagraphProperties(WordTextProperties
       paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new
               ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize
                    {
                        Val =
                   paragraphProperties.Size
                    });
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }
        //Метод для проверки
        private bool Check<T>(List<WordTitleColumn> titleColumns, List<WordMergedTitleColumn> mergedTitleColumns, List<T> data)
        {

            foreach (var t in titleColumns)
            {
                if (t.FieldInfo == null && t.PropertyInfo == null || t.FieldInfo != null && t.PropertyInfo != null)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(t.Name) || t.Width == 0)
                {
                    return false;
                }
            }
            List<decimal> merged = new List<decimal>();
            foreach (var c in mergedTitleColumns)
            {
                if (string.IsNullOrEmpty(c.Title))
                {
                    return false;
                }
                foreach (int mc in c.Columns)
                {
                    if (merged.Contains(mc))
                    {
                        return false;
                    }
                    if (merged.Count != 0 && mc != merged.Last() + 1)
                    {
                        return false;
                    }
                    if (mc >= titleColumns.Count)
                    {
                        return false;
                    }
                    merged.Add(mc);
                }

            }
            foreach (T d in data)
            {
                foreach (var t in titleColumns)
                {
                    Type type = d.GetType();
                    if (t.FieldInfo != null)
                    {
                        FieldInfo fieldInfo = type.GetField(t.FieldInfo.Name);
                        if (fieldInfo == null || fieldInfo.GetValue(d) == null)
                        {
                            return false;
                        }
                    }

                    if (t.PropertyInfo != null)
                    {
                        PropertyInfo propertyInfo = type.GetProperty(t.PropertyInfo.Name);
                        if (propertyInfo == null || propertyInfo.GetValue(d) == null)
                        {
                            return false;
                        }
                    }

                }
            }
            return true;
        }
    }
}
