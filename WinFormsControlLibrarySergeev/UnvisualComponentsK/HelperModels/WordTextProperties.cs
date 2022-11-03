using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;

namespace WinFormsControlLibrarySergeev.UnvisualComponentsK.HelperModels
{
    public class WordTextProperties
    {
        public string Size { get; set; }
        public bool Bold { get; set; }
        public JustificationValues JustificationValues { get; set; }
    }
}
