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
    public partial class TimePickBox : UserControl
    {
        public TimePickBox()
        {
            InitializeComponent();
            dateTimePicker.ValueChanged += (sender, e) => _event?.Invoke(sender, e);
        }

        private event EventHandler _event;

        public event EventHandler SelectedTimeChanged
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

        public DateTime? DateFrom;

        public DateTime? DateTo;
        private DateTime TimePrev { get; set; }

        public DateTime? TimePicked
        {
            get
            {
                if (!DateFrom.HasValue || !DateTo.HasValue || dateTimePicker.Value < DateFrom || dateTimePicker.Value > DateTo)
                {
                    labelError.Text = $"Enter granici";
                    labelError.BackColor = Color.Red;
                    return null;
                }
                labelError.Text = string.Empty;
                labelError.BackColor = BackColor;
                return dateTimePicker.Value;
            }
            set
            {
                if (!DateFrom.HasValue || !DateTo.HasValue || value < DateFrom || value > DateTo)
                {
                    labelError.Text = $"Enter parameters";
                    labelError.BackColor = Color.Red;
                    return;
                }
                dateTimePicker.Value = value.Value;
                labelError.Text = string.Empty;
                labelError.BackColor = BackColor;
            }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Random random = new Random();
            TimePicked = dateTimePicker.Value;
            if (TimePrev != TimePicked && TimePicked != null)
            {
                BackColor = Color.FromArgb(random.Next(256),
                    random.Next(256),
                    random.Next(256));
                TimePrev = TimePicked.Value;
            }
        }
    }
}
