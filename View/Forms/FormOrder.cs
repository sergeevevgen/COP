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

namespace View.Forms
{
    public partial class FormOrder : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        private ProductLogic productLogic;
        private OrderLogic orderLogic;
        private byte[] image = null;

        public FormOrder()
        {
            InitializeComponent();
            productLogic = new ProductLogic();
            orderLogic = new OrderLogic();
            var productList = productLogic.Read(null);
            var list = new List<string>();
            foreach (var product in productList)
            {
                list.Add(product.Name);
            }
            selectedComboBox.AddList(list);
        }

        private void LoadData()
        {   
            if (id.HasValue)
            {
                try
                {
                    var view = orderLogic.Read(new OrderBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxFIO.Text = view.CustomerFIO;
                        pictureBox.Image = byteArrayToImage(view.Image);
                        selectedComboBox.SelectedElement = view.Product;
                        inputBox.TextElement = view.Mail;
                        image = view.Image;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private bool Save()
        {
            if (textBoxFIO.Text != string.Empty && selectedComboBox.SelectedElement != string.Empty && inputBox.TextElement != null && image != null)
            {
                if (id != null /*&& (textBoxFIO.Text != model.CustomerFIO || controlSelectedComboBoxList.SelectedElement != model.Product || inputBox.TextElement != model.Mail || flag_image)*/)
                {
                    if (MessageBox.Show("Сохранить изменения в заказе?", "Уведомление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        orderLogic.CreateOrUpdate(new OrderBindingModel()
                        {
                            Id = id,
                            CustomerFIO = textBoxFIO.Text,
                            Image = image,
                            Product = selectedComboBox.SelectedElement,
                            Mail = inputBox.TextElement
                        });
                        return true;
                    }
                    return false;
                }
                else
                {
                    if (MessageBox.Show("Сохранить заказ?", "Уведомление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        orderLogic.CreateOrUpdate(new OrderBindingModel()
                        {
                            CustomerFIO = textBoxFIO.Text,
                            Image = image,
                            Product = selectedComboBox.SelectedElement,
                            Mail = inputBox.TextElement
                        });
                        return true;
                    }
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Введите значения");
                return false;
            }
        }

        private void buttonAddImage_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.Title = "Выберите изображение";
            dialog.Filter = "jpg files (*.jpg)|*.jpg";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var image_new = new Bitmap(dialog.FileName);
                pictureBox.Image = image_new;
                image = ImageToByteArray(image_new);
            }

            dialog.Dispose();
        }

        private Image byteArrayToImage(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private void FormOrder_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void FormOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Save())
            {
                e.Cancel = true;
            }
            DialogResult = DialogResult.OK;
        }
    }
}
