using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsControlLibrarySergeev.UnvisualComponents.HelperEnums;
using WinFormsControlLibrarySergeev.UnvisualComponents.HelperModels;

namespace WinFormsControlLibrarySergeev.UnvisualComponents
{
    public abstract class AbstractSaveToExcel
    {
        /// <summary>
        /// Создание отчeта
        /// </summary>
        /// <param name="info"></param>
        public void CreateReportTable<T>(ExcelInfoTable<T> info) where T : class
        {
            //Создание файла
            CreateExcel(info);

            //Вставка заголовка
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });

            //Лист с названиями колонок
            var listCells = new List<string>();
            for (int j = 65; j < 91; j++)
            {
                char c = (char)j;
                listCells.Add(c.ToString());
            }

            //Вставка двойных заголовков
            uint rowIndex = 2;

            //Обход двойных заголовков
            var list = new List<int>();
            foreach (var value in info.mergeCells.Values)
            {
                foreach (var v in value)
                {
                    list.Add((int)(v + rowIndex));
                }
            }
            
            var keys_list_complex_titles = info.mergeCells.Keys.ToList();
            int i = 0;
            //Вставка всех заголовков
            for(var col_ = 0; col_ < info.MiniTitles.Count; ++col_)
            {
                if (!list.Contains((int)rowIndex))
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "A",
                        RowIndex = rowIndex,
                        Text = info.MiniTitles[col_],
                        StyleInfo = ExcelStyleInfoType.Title
                    });
                    MergeCells(new ExcelMergeParameters
                    {
                        CellFromName = "A" + rowIndex,
                        CellToName = "B" + rowIndex
                    });
                }
                else
                {
                    var new_row_index = rowIndex;
                    foreach(var v in info.mergeCells[keys_list_complex_titles[i]])
                    {
                        InsertCellInWorksheet(new ExcelCellParameters
                        {
                            ColumnName = "B",
                            RowIndex = new_row_index,
                            Text = info.MiniTitles[col_],
                            StyleInfo = ExcelStyleInfoType.Title
                        });
                        col_++;
                        new_row_index++;
                    }
                    new_row_index--;
                    col_--;
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "A",
                        RowIndex = rowIndex,
                        Text = keys_list_complex_titles[i],
                        StyleInfo = ExcelStyleInfoType.Title
                    });
                    MergeCells(new ExcelMergeParameters
                    {
                        CellFromName = "A" + rowIndex,
                        CellToName = "A" + (new_row_index)
                    });
                    rowIndex = new_row_index;
                    i++;
                }
                rowIndex++;
            }

            //Вставка данных
            rowIndex = 2;
            var properties = typeof(T).GetProperties();
            
            for(int j = 0; j < properties.Length; j++)
            {
                for(int k = 0; k < info.Objects.Count; ++k)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = listCells[k + 2],
                        RowIndex = rowIndex,
                        Text = properties[j].GetValue(info.Objects[k]).ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBorder
                    });
                }
                rowIndex++;
            }
            SaveExcel(info);
        }

        /// <summary>
        /// Создание отчeта
        /// </summary>
        /// <param name="info"></param>
        public void CreateReportImages(ExcelInfoImages info)
        {
            CreateExcel(info);
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "T",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "T1",
                CellToName = "X1"
            });

            //Вычисление размеров изображения
            int y = 0;
            for (int i = 0; i < info.Images.Count; i++)
            {
                InsertImageExcel(new ExcelImageParameters
                {
                    X = 0,
                    Y = y,
                    Image = info.Images[i]
                });
                using (MemoryStream ms = new MemoryStream(info.Images[i]))
                {
                    Bitmap image = (Bitmap)Image.FromStream(ms);
                    y += (int)(image.Height * 914400 / image.VerticalResolution) + 10;
                }
                
            }
            SaveExcel(info);
        }

        public void CreateReportPieChart(ExcelInfoPieChart info)
        {
            CreateExcel(info);
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });

            uint rowIndex = 2;
            foreach(var i in info.LegendData)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = i.Key,
                    StyleInfo = ExcelStyleInfoType.TextWithBorder
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "B",
                    RowIndex = rowIndex,
                    Text = i.Value.ToString() + '%',
                    StyleInfo = ExcelStyleInfoType.TextWithBorder
                });
                rowIndex++;
            }

            InsertChartPieExcel(new ExcelPieChartParameters
            {
                ChartName = info.ChartName,
                Legend = info.Legend,
                LegendData = info.LegendData
            });
            
            SaveExcel(info);
        }

        /// <summary>
        /// Создание excel-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateExcel(ExcelInfo info);

        /// <summary>
        /// Добавляем новую ячейку в лист
        /// </summary>
        /// <param name="excelCellParams"></param>
        protected abstract void InsertCellInWorksheet(ExcelCellParameters cellParams);

        /// <summary>
        /// Объединение ячеек
        /// </summary>
        /// <param name="mergeParams"></param>
        protected abstract void MergeCells(ExcelMergeParameters mergeParams);

        /// <summary>
        /// Вставка картинок
        /// </summary>
        /// <param name="info"></param>
        protected abstract void InsertImageExcel(ExcelImageParameters imageParameters);

        /// <summary>
        /// Вставка круговой диаграммы
        /// </summary>
        /// <param name="info"></param>
        protected abstract void InsertChartPieExcel(ExcelPieChartParameters excelParams);

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SaveExcel(ExcelInfo info);
    }
}
