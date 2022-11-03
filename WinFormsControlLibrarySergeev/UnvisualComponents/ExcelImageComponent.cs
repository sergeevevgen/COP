using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WinFormsControlLibrarySergeev.UnvisualComponents
{
    public partial class ExcelImageComponent : Component
    {
        public ExcelImageComponent()
        {
            InitializeComponent();
        }

        public ExcelImageComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void CreateFile(string filename, string title, List<byte[]> images)
        {
            if (filename != null && title != null && images != null)
            {
                AbstractSaveToExcel save = new SaveToExcel();
                save.CreateReportImages(new HelperModels.ExcelInfoImages
                {
                    FileName = filename,
                    Title = title,
                    Images = images
                });
            }
            else
                throw new ArgumentException();
        }
    }
}
