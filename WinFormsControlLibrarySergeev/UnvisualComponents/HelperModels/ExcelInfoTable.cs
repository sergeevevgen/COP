using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsControlLibrarySergeev.UnvisualComponents.HelperModels
{
    public class ExcelInfoTable<T> : ExcelInfo where T : class
    {
        //string - название объединенных ячеек, int[] - ячейки, которые надо объединить
        public Dictionary<string, int[]> mergeCells { get; set; }
        //string - название, int - ширина
        public List<string> MiniTitles { get; set; }
        public List<T> Objects { get; set; }
    }
}
