using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WinFormsControlLibrarySergeev.UnvisualComponents
{
    public partial class ExcelCustomizableTableComponent : Component
    {
        public ExcelCustomizableTableComponent()
        {
            InitializeComponent();
        }

        public ExcelCustomizableTableComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void CreateFile<T>(string filename, string title, Dictionary<string, int[]> mergedCells, List<string> mtitle, List<T> data) where T : class
        {
            if (filename != null && title != null && mergedCells != null && mtitle != null && data != null)
            {
                AbstractSaveToExcel save = new SaveToExcel();
                save.CreateReportTable(new HelperModels.ExcelInfoTable<T>
                {
                    FileName = filename,
                    Title = title,
                    mergeCells = mergedCells,
                    MiniTitles = mtitle,
                    Objects = data
                });
            }
            else
                throw new ArgumentException();
        }
    }
}
