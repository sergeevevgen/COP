using PluginsConventionLibrary.Plugins;

namespace View
{
    public partial class FormMain : Form
    {
        private readonly Dictionary<string, IPluginsConvention> _plugins;
        private string _selectedPlugin;
        public FormMain()
        {
            InitializeComponent();
            _plugins = LoadPlugins();
            _selectedPlugin = string.Empty;
        }

        private Dictionary<string, IPluginsConvention> LoadPlugins()
        {
            PluginsManager manager = new PluginsManager();
            Dictionary<string, IPluginsConvention> dic = manager.plugins_dictionary;


            ToolStripItem[] toolStripItems = new ToolStripItem[dic.Count];
            int i = 0;
            foreach(var item in dic)
            {
                ToolStripMenuItem itemMenu = new ToolStripMenuItem();
                itemMenu.Text = item.Value.PluginName;
                itemMenu.Click += (sender, e) =>
                {
                    _selectedPlugin = item.Value.PluginName;
                    panelControl.Controls.Clear();
                    panelControl.Controls.Add(_plugins[_selectedPlugin].GetControl);
                    panelControl.Controls[0].Dock = DockStyle.Fill;
                };
                toolStripItems[i] = itemMenu;
                i++;
            }

            ControlsStripMenuItem.DropDownItems.AddRange(toolStripItems);
            return dic;
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedPlugin) ||
            !_plugins.ContainsKey(_selectedPlugin))
            {
                return;
            }
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
            var form = _plugins[_selectedPlugin].GetForm(null);
            if (form != null && form.ShowDialog() == DialogResult.OK)
            {
                _plugins[_selectedPlugin].ReloadData();
            }
        }

        private void UpdateElement()
        {
            var element = _plugins[_selectedPlugin].GetElement;
            if (element == null)
            {
                MessageBox.Show("Нет выбранного элемента", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var form = _plugins[_selectedPlugin].GetForm(element);
            if (form != null && form.ShowDialog() == DialogResult.OK)
            {
                _plugins[_selectedPlugin].ReloadData();
            }
        }

        private void DeleteElement()
        {
            if (MessageBox.Show("Удалить выбранный элемент", "Удаление",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            var element = _plugins[_selectedPlugin].GetElement;
            if (element == null)
            {
                MessageBox.Show("Нет выбранного элемента", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_plugins[_selectedPlugin].DeleteElement(element))
            {
                _plugins[_selectedPlugin].ReloadData();
            }
        }

        private void CreateSimpleDoc()
        {
            using var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (_plugins[_selectedPlugin].CreateSimpleDocument(new
                PluginsConventionSaveDocument { FileName = dialog.FileName }))
                {
                    MessageBox.Show("Документ сохранен", "Создание документа",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при создании документа", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void CreateTableDoc()
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK) 
                {
                    if (_plugins[_selectedPlugin].CreateTableDocument(new
                    PluginsConventionSaveDocument { FileName = dialog.FileName }))
                    {
                        MessageBox.Show("Документ сохранен", "Создание документа",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при создании документа", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void CreateChartDoc()
        {
            using var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (_plugins[_selectedPlugin].CreateChartDocument(new
                PluginsConventionSaveDocument { FileName = dialog.FileName }))
                {
                    MessageBox.Show("Документ сохранен", "Создание документа",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при создании документа", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SimpleDocToolStripMenuItem_Click(object sender, EventArgs e) =>
        CreateSimpleDoc();

        private void TableDocToolStripMenuItem_Click(object sender, EventArgs e) =>
        CreateTableDoc();

        private void ChartDocToolStripMenuItem_Click(object sender, EventArgs e) =>
        CreateChartDoc();

        private void AddElementToolStripMenuItem_Click(object sender, EventArgs e) =>
        AddNewElement();

        private void UpdElementToolStripMenuItem_Click(object sender, EventArgs e) =>
        UpdateElement();

        private void DelElementToolStripMenuItem_Click(object sender, EventArgs e) =>
        DeleteElement();
    }
}