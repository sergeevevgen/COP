using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsControlLibrarySergeev
{
    public partial class SelectListBox : UserControl
    {
        public SelectListBox()
        {
            InitializeComponent();
            checkedListBox.SelectedIndexChanged += (sender, e) => _event?.Invoke(sender, e);
        }

        private int checkI = -1;
        private event EventHandler _event;

        public event EventHandler SelectedIndexChanged
        {
            add
            {
                _event += value;
            }
            remove
            {
                _event -= value;
            }
        }

        public string SelectedElement
        {
            get
            {
                if (checkedListBox.SelectedItem == null)
                {
                    return string.Empty;
                }

                return checkedListBox.SelectedItem.ToString();
            }
            set
            {
                checkedListBox.SelectedItem = value;
            }
        }

        private void checkedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Random random = new Random();
            if (checkI != checkedListBox.SelectedIndex && checkI != -1)
            {
                checkedListBox.SetItemChecked(checkI, false);
                checkedListBox.SetItemChecked(checkedListBox.SelectedIndex, true);
            }
            checkedListBox.BackColor =
                Color.FromArgb(random.Next(256),
                random.Next(256), random.Next(256));
            checkI = checkedListBox.SelectedIndex;
        }

        public void FillList(List<string> acceptedText)
        {
            checkedListBox.DataSource = acceptedText;
        }

        public void Clear()
        {
            checkedListBox.DataSource = null;
        }
    }
}
