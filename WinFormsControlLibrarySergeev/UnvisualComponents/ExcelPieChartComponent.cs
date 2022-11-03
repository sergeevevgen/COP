using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsControlLibrarySergeev.UnvisualComponents
{
    public partial class ExcelPieChartComponent : Component
    {
        public ExcelPieChartComponent()
        {
            InitializeComponent();
        }

        public ExcelPieChartComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void CreateFile(string filename, string title, string chartName, Dictionary<string, decimal> dictionary, string location)
        {
            if (filename != null && title != null && chartName != null && dictionary != null)
            {
                AbstractSaveToExcel save = new SaveToExcel();
                switch (location)
                {
                    case "Bottom":
                        save.CreateReportPieChart(new HelperModels.ExcelInfoPieChart
                        {
                            FileName = filename,
                            Title = title,
                            ChartName = chartName,
                            LegendData = dictionary,
                            Legend = HelperEnums.ExcelLegendLocation.Bottom
                        });
                        break;
                    case "Top":
                        save.CreateReportPieChart(new HelperModels.ExcelInfoPieChart
                        {
                            FileName = filename,
                            Title = title,
                            ChartName = chartName,
                            LegendData = dictionary,
                            Legend = HelperEnums.ExcelLegendLocation.Top
                        });
                        break;
                    case "Right":
                        save.CreateReportPieChart(new HelperModels.ExcelInfoPieChart
                        {
                            FileName = filename,
                            Title = title,
                            ChartName = chartName,
                            LegendData = dictionary,
                            Legend = HelperEnums.ExcelLegendLocation.Right
                        });
                        break;
                    case "Left":
                        save.CreateReportPieChart(new HelperModels.ExcelInfoPieChart
                        {
                            FileName = filename,
                            Title = title,
                            ChartName = chartName,
                            LegendData = dictionary,
                            Legend = HelperEnums.ExcelLegendLocation.Left
                        });
                        break;
                }
            }
            else
                throw new ArgumentException();
        }
    }
}
