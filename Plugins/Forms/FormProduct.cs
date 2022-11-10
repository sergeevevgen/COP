using LogicDB.BindingModels;
using LogicDB.Logics;
using LogicDB.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plugins.Forms
{
    public partial class FormProduct : Form
    {
        ProductLogic productLogic = new ProductLogic();
        List<ProductBindingModel> list;
        public FormProduct()
        {
            InitializeComponent();
            productLogic = new ProductLogic();
            list = new List<ProductBindingModel>();
        }

        private void LoadData()
        {
            try
            {
                var list1 = productLogic.Read(null);
                list.Clear();
                foreach (var item in list1)
                {
                    list.Add(new ProductBindingModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                    });
                }
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var typeName = (string)dataGridView.CurrentRow.Cells[1].Value;
            if (!string.IsNullOrEmpty(typeName))
            {
                if (dataGridView.CurrentRow.Cells[0].Value != null)
                {
                    productLogic.CreateOrUpdate(new ProductBindingModel()
                    {
                        Id = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value),
                        Name = (string)dataGridView.CurrentRow.Cells[1].EditedFormattedValue
                    });
                }
                else
                {
                    productLogic.CreateOrUpdate(new ProductBindingModel()
                    {
                        Name = (string)dataGridView.CurrentRow.Cells[1].EditedFormattedValue
                    });
                }
            }
            else
            {
                MessageBox.Show("Введена пустая строка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadData();
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Insert)
            {
                if (dataGridView.Rows.Count == 0)
                {
                    list.Add(new ProductBindingModel());
                    dataGridView.DataSource = new List<ProductBindingModel>(list);
                    dataGridView.CurrentCell = dataGridView.Rows[0].Cells[1];
                    return;
                }
                if (dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[1].Value != null)
                {
                    list.Add(new ProductBindingModel());
                    dataGridView.DataSource = new List<ProductBindingModel>(list);
                    dataGridView.CurrentCell = dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[1];
                    return;
                }
            }
            if (e.KeyData == Keys.Delete)
            {
                if (MessageBox.Show("Удалить выбранный элемент", "Удаление",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    productLogic.Delete(new ProductBindingModel() { Id = (int)dataGridView.CurrentRow.Cells[0].Value });
                    LoadData();
                }
            }
        }
    }
}
