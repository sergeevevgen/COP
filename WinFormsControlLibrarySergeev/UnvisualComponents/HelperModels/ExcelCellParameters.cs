using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsControlLibrarySergeev.UnvisualComponents.HelperEnums;

namespace WinFormsControlLibrarySergeev.UnvisualComponents.HelperModels
{
    public class ExcelCellParameters
    {
        public string ColumnName { get; set; }
        public uint RowIndex { get; set; }
        public string Text { get; set; }
        public string CellReference => $"{ColumnName}{RowIndex}";
        public ExcelStyleInfoType StyleInfo { get; set; }
    }
}
