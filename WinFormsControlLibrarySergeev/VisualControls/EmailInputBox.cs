using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsControlLibrarySergeev.VisualControls
{
    public partial class EmailInputBox : UserControl
    {
        private event EventHandler textBoxTextChanged;
        public EmailInputBox()
        {
            InitializeComponent();
        }

        public string example = "example@gmail.com";
        public void SetToolTip(string example)
        {
            toolTipExamle.SetToolTip(textBox, example);
        }
        public string Pattern { get; set; } = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                                            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
        public string TextElement
        {
            get
            {
                if (Regex.IsMatch(textBox.Text, Pattern))
                {
                    return textBox.Text;
                }
                else
                {
                    throw new Exception("Wrong value");
                }
            }
            set
            {
                if (Regex.IsMatch(value, Pattern))
                {
                    textBox.Text = value;
                }
            }
        }
        public event EventHandler TextBoxTextChanged
        {
            add { textBoxTextChanged += value; }
            remove { textBoxTextChanged -= value; }
        }
    }
}
