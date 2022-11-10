using ComponentsLibraryNet60.DocumentWithChart;
using ComponentsLibraryNet60.DocumentWithTable;
using ControlsLibraryNet60.Data;
using ControlsLibraryNet60.Models;
using LogicDB.Logics;
using PluginsConventionLibrary.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugins.Forms;
using WinFormsControlLibrarySergeev.UnvisualComponents;

namespace Plugin.Plugin
{
    [Export(typeof(IPluginsConvention))]
    public class MainPluginConvention : IPluginsConvention
    {
        private ControlDataTableTable dataTableView;
        private OrderLogic orderLogic;
        private List<DataTableColumnConfig> config;

        public MainPluginConvention()
        {
            dataTableView = new ControlDataTableTable();

            var menu = new ContextMenuStrip();
            var productMenuItem = new ToolStripMenuItem("Продукты");
            menu.Items.Add(productMenuItem);
            productMenuItem.Click += (sender, e) =>
            {
                var formProduct = new FormProduct();
                formProduct.ShowDialog();
            };
            dataTableView.ContextMenuStrip = menu;

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
                    ColumnHeader = "ФИО покупателя",
                    PropertyName = "CustomerFIO",
                    Visible = true,
                    Width = 120
                },
                new DataTableColumnConfig
                {
                    ColumnHeader = "Товар",
                    PropertyName = "Product",
                    Visible = true,
                    Width = 140
                },
                new DataTableColumnConfig
                {
                    ColumnHeader = "Почта",
                    PropertyName = "Mail",
                    Visible = true,
                    Width = 200
                }
            };
            dataTableView.LoadColumns(config);
            orderLogic = new OrderLogic();
            ReloadData();
        }

        public string PluginName => "Заказы";

        public UserControl GetControl => dataTableView;

        public PluginsConventionElement GetElement => dataTableView.GetSelectedObject<PluginsConventionElement>();

        public bool CreateChartDocument(PluginsConventionSaveDocument saveDocument)
        {
            try
            {
                ComponentDocumentWithChartBarPdf component = new ComponentDocumentWithChartBarPdf();
                component.CreateDoc(new ComponentsLibraryNet60.Models.ComponentDocumentWithChartConfig
                {
                    FilePath = saveDocument.FileName,
                    Header = "Товары",
                    ChartTitle = "Купленные товары",
                    LegendLocation = ComponentsLibraryNet60.Models.Location.Bottom,
                    Data = PdfData()
                });
            }
            catch (Exception)
            {
                return false;
            }
            return true;
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
            foreach (var item in list_product)
            {
                list_changed.Add(item.Key, new List<(int, double)> { (1, item.Value) });
            }
            return list_changed;
        }

        public bool CreateSimpleDocument(PluginsConventionSaveDocument saveDocument)
        {
            try
            {
                ExcelImageComponent component = new ExcelImageComponent();
                component.CreateFile(saveDocument.FileName, "Фото заказов " + DateTime.Now.ToShortDateString(), ExcelImages());
            }
            catch (Exception)
            {
                return false;
            }
            return true;
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

        public bool CreateTableDocument(PluginsConventionSaveDocument saveDocument)
        {
            try
            {
                ComponentDocumentWithTableHeaderRowWord component = new ComponentDocumentWithTableHeaderRowWord();
                component.CreateDoc(new ComponentsLibraryNet60.Models.ComponentDocumentWithTableHeaderDataConfig<LogicDB.ViewModels.OrderViewModel>
                {
                    FilePath = saveDocument.FileName,
                    Header = "Заказы " + DateTime.Now.ToShortDateString(),
                    UseUnion = true,
                    ColumnsRowsWidth = new List<(int, int)> { (5, 0), (10, 0), (10, 0), (10, 0) },
                    ColumnUnion = new List<(int StartIndex, int Count)> { (1, 2) },
                    Headers = new List<(int ColumnIndex, int RowIndex, string Header, string PropertyName)>
                        {
                            (0, 0, "Идентификатор", "Id"),
                            (1, 0, "Личные данные", ""),
                            (1, 1, "ФИО", "CustomerFIO"),
                            (2, 1, "Эл.почта", "Mail"),
                            (3, 0, "Товар", "Product")
                        },
                    Data = orderLogic.Read(null)
                });
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public bool DeleteElement(PluginsConventionElement element)
        {
            try
            {
                orderLogic.Delete(new LogicDB.BindingModels.OrderBindingModel { Id = element.Id });
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public Form GetForm(PluginsConventionElement element)
        {
            var formOrder = new FormOrder();
            if (element != null)
            {
                formOrder.Id = element.Id;
            }
            return formOrder;
        }

        public void ReloadData()
        {
            dataTableView.Clear();
            var data = orderLogic.Read(null);
            if (data.Count > 0)
            {
                dataTableView.AddTable(data);
            }
        }
    }
}
