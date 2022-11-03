using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsControlLibrarySergeev.UnvisualComponents.HelperEnums;

namespace WinFormsControlLibrarySergeev.UnvisualComponents.HelperModels
{
    public class ExcelPieChartParameters
    {
        public string ChartName { get; set; }
        public ExcelLegendLocation Legend { get; set; }
        //string - название, decimal - значение (% - считать по значениям)
        public Dictionary<string, decimal> LegendData { get; set; }
    }
}
