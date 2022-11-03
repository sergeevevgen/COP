using ComponentsLibraryNet60.DocumentWithChart;
using ComponentsLibraryNet60.DocumentWithTable;
using ControlsLibraryNet60.Models;
using LogicDB.BindingModels;
using LogicDB.Logics;
using LogicDB.ViewModels;
using View.Forms;
using WinFormsControlLibrarySergeev.UnvisualComponents;
using WinFormsControlLibrarySergeev.UnvisualComponentsK.HelperModels;

namespace View
{
    public partial class FormMain : Form
    {
        private OrderLogic orderLogic;
        private List<DataTableColumnConfig> config;
        public FormMain()
        {
            InitializeComponent();
            config = new List<DataTableColumnConfig>
            {
                new DataTableColumnConfig
                {
                    ColumnHeader = "Id",
                    PropertyName = "Id",
                    Visible = false,
                },
                new DataTableColumnConfig
                {
                    ColumnHeader = "��� ����������",
                    PropertyName = "CustomerFIO",
                    Visible = true,
                    Width = 120
                },
                new DataTableColumnConfig
                {
                    ColumnHeader = "�����",
                    PropertyName = "Product",
                    Visible = true,
                    Width = 140
                },
                new DataTableColumnConfig
                {
                    ColumnHeader = "�����",
                    PropertyName = "Mail",
                    Visible = true,
                    Width = 200
                }
            };
            controlDataTableTable.LoadColumns(config);
            orderLogic = new OrderLogic();
            ReloadData();
        }

        private void ReloadData()
        {
            controlDataTableTable.Clear();
            var data = orderLogic.Read(null);
            if (data.Count > 0)
            {
                controlDataTableTable.AddTable(data);
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control)
            {
                return;
            }
            switch (e.KeyCode)
            {
                case Keys.A:
                    AddNewElement();
                    break;
                case Keys.U:
                    UpdateElement();
                    break;
                case Keys.D:
                    DeleteElement();
                    break;
                case Keys.S:
                    CreateSimpleDoc();
                    break;
                case Keys.T:
                    CreateTableDoc();
                    break;
                case Keys.C:
                    CreateChartDoc();
                    break;
            }
        }

        private void AddNewElement()
        {
            var form = new FormOrder();
            if (form.ShowDialog() == DialogResult.OK)
            {
                ReloadData();
            }
        }

        private void UpdateElement()
        {
            var element = controlDataTableTable.GetSelectedObject<OrderViewModel>();
            if (element == null)
            {
                MessageBox.Show("��� ���������� ��������", "������",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var form = new FormOrder { Id = element.Id };
            if (form.ShowDialog() == DialogResult.OK)
            {
                ReloadData();
            }
        }

        private void DeleteElement()
        {
            if (MessageBox.Show("������� ��������� �������", "��������",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            var element = controlDataTableTable.GetSelectedObject<OrderViewModel>();
            if (element == null)
            {
                MessageBox.Show("��� ���������� ��������", "������",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            orderLogic.Delete(new OrderBindingModel { Id = element.Id });
            ReloadData();
        }

        private void CreateSimpleDoc()
        {
            using var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ExcelImageComponent component = new ExcelImageComponent();
                component.CreateFile(dialog.FileName, "���� ������� " + DateTime.Now.ToShortDateString(), ExcelImages());
                MessageBox.Show("�������� ��������", "�������� ���������",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void CreateTableDoc()
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK) 
                {
                    ComponentDocumentWithTableHeaderRowWord component = new ComponentDocumentWithTableHeaderRowWord();
                    component.CreateDoc(new ComponentsLibraryNet60.Models.ComponentDocumentWithTableHeaderDataConfig<OrderViewModel>
                    {
                        FilePath = dialog.FileName,
                        Header = "������ " + DateTime.Now.ToShortDateString(),
                        UseUnion = true,
                        ColumnsRowsWidth = new List<(int, int)> { (5, 0), (10, 0), (10, 0), (10, 0) },
                        ColumnUnion = new List<(int StartIndex, int Count)> { (1, 2) },
                        Headers = new List<(int ColumnIndex, int RowIndex, string Header, string PropertyName)>
                        {
                            (0, 0, "�������������", "Id"),
                            (1, 0, "������ ������", ""),
                            (1, 1, "���", "CustomerFIO"),
                            (2, 1, "��.�����", "Mail"),
                            (3, 0, "�����", "Product")
                        },
                        Data = orderLogic.Read(null)
                    });
                    MessageBox.Show("�������� ��������", "�������� ���������",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private List<byte[]> ExcelImages()
        {
            var list = orderLogic.Read(null);
            var list_bytes = new List<byte[]>();
            foreach (var item in list)
            {
                list_bytes.Add(item.Image);
            }
            return list_bytes;
        }

        private void CreateChartDoc()
        {
            using var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ComponentDocumentWithChartBarPdf component = new ComponentDocumentWithChartBarPdf();
                component.CreateDoc(new ComponentsLibraryNet60.Models.ComponentDocumentWithChartConfig
                {
                    FilePath = dialog.FileName,
                    Header = "������",
                    ChartTitle = "��������� ������",
                    LegendLocation = ComponentsLibraryNet60.Models.Location.Bottom,
                    Data = PdfData()
                });
                MessageBox.Show("�������� ��������", "�������� ���������",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private Dictionary<string, List<(int, double)>> PdfData()
        {
            var list = orderLogic.Read(null);
            var list_product = new Dictionary<string, int>();
            foreach (var item in list)
            {
                if (list_product.ContainsKey(item.Product))
                    list_product[item.Product]++;
                else
                {
                    list_product.Add(item.Product, 1);
                }
            }
            var list_changed = new Dictionary<string, List<(int, double)>>();
            foreach(var item in list_product)
            {

                list_changed.Add(item.Key, new List<(int, double)> { (1, item.Value) });
            }
            return list_changed;
        }

        private void ���������������CtrlSToolStripMenuItem_Click(object sender, EventArgs e) =>
        CreateSimpleDoc();

        private void �����������������CtrlTToolStripMenuItem_Click(object sender, EventArgs e) =>
        CreateTableDoc();

        private void ���������CtrlCToolStripMenuItem_Click(object sender, EventArgs e) =>
        CreateChartDoc();

        private void ��������CtrlAToolStripMenuItem_Click(object sender, EventArgs e) =>
        AddNewElement();

        private void ��������CtrlUToolStripMenuItem_Click(object sender, EventArgs e) =>
        UpdateElement();

        private void �������CtrlDToolStripMenuItem_Click(object sender, EventArgs e) =>
        DeleteElement();

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormProduct();
            form.ShowDialog();
        }
    }
}