using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsControlLibrarySergeev.UnvisualComponentsK.HelperModels
{
    public class WordParagraph
    {
        public List<(string, WordTextProperties)> Texts { get; set; }
        public WordTextProperties TextProperties { get; set; }
    }
}
