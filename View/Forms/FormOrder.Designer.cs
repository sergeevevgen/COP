namespace View.Forms
{
    partial class FormOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.controlSelectedComboBoxList = new ControlsLibraryNet60.Selected.ControlSelectedComboBoxList();
            this.labelFIO = new System.Windows.Forms.Label();
            this.labelProduct = new System.Windows.Forms.Label();
            this.labelMail = new System.Windows.Forms.Label();
            this.labelImage = new System.Windows.Forms.Label();
            this.buttonAddImage = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxFIO = new System.Windows.Forms.TextBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.controlInputRegexEmail = new ControlsLibraryNet60.Input.ControlInputRegexEmail();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // controlSelectedComboBoxList
            // 
            this.controlSelectedComboBoxList.Location = new System.Drawing.Point(162, 74);
            this.controlSelectedComboBoxList.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.controlSelectedComboBoxList.Name = "controlSelectedComboBoxList";
            this.controlSelectedComboBoxList.SelectedElement = "";
            this.controlSelectedComboBoxList.Size = new System.Drawing.Size(160, 24);
            this.controlSelectedComboBoxList.TabIndex = 0;
            // 
            // labelFIO
            // 
            this.labelFIO.AutoSize = true;
            this.labelFIO.Location = new System.Drawing.Point(12, 22);
            this.labelFIO.Name = "labelFIO";
            this.labelFIO.Size = new System.Drawing.Size(100, 15);
            this.labelFIO.TabIndex = 2;
            this.labelFIO.Text = "ФИО покупателя";
            // 
            // labelProduct
            // 
            this.labelProduct.AutoSize = true;
            this.labelProduct.Location = new System.Drawing.Point(12, 83);
            this.labelProduct.Name = "labelProduct";
            this.labelProduct.Size = new System.Drawing.Size(53, 15);
            this.labelProduct.TabIndex = 3;
            this.labelProduct.Text = "Продукт";
            // 
            // labelMail
            // 
            this.labelMail.AutoSize = true;
            this.labelMail.Location = new System.Drawing.Point(12, 290);
            this.labelMail.Name = "labelMail";
            this.labelMail.Size = new System.Drawing.Size(41, 15);
            this.labelMail.TabIndex = 4;
            this.labelMail.Text = "Почта";
            // 
            // labelImage
            // 
            this.labelImage.AutoSize = true;
            this.labelImage.Location = new System.Drawing.Point(12, 153);
            this.labelImage.Name = "labelImage";
            this.labelImage.Size = new System.Drawing.Size(83, 15);
            this.labelImage.TabIndex = 5;
            this.labelImage.Text = "Изображение";
            // 
            // buttonAddImage
            // 
            this.buttonAddImage.Location = new System.Drawing.Point(12, 222);
            this.buttonAddImage.Name = "buttonAddImage";
            this.buttonAddImage.Size = new System.Drawing.Size(75, 23);
            this.buttonAddImage.TabIndex = 6;
            this.buttonAddImage.Text = "Добавить";
            this.buttonAddImage.UseVisualStyleBackColor = true;
            this.buttonAddImage.Click += new System.EventHandler(this.buttonAddImage_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 349);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxFIO
            // 
            this.textBoxFIO.Location = new System.Drawing.Point(162, 19);
            this.textBoxFIO.Name = "textBoxFIO";
            this.textBoxFIO.Size = new System.Drawing.Size(242, 23);
            this.textBoxFIO.TabIndex = 8;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(162, 141);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(196, 104);
            this.pictureBox.TabIndex = 9;
            this.pictureBox.TabStop = false;
            // 
            // controlInputRegexEmail
            // 
            this.controlInputRegexEmail.Location = new System.Drawing.Point(162, 282);
            this.controlInputRegexEmail.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.controlInputRegexEmail.Name = "controlInputRegexEmail";
            this.controlInputRegexEmail.Pattern = "^([\\w\\d\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";
            this.controlInputRegexEmail.Size = new System.Drawing.Size(214, 23);
            this.controlInputRegexEmail.TabIndex = 1;
            this.controlInputRegexEmail.Value = "";
            // 
            // FormOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 382);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.textBoxFIO);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonAddImage);
            this.Controls.Add(this.labelImage);
            this.Controls.Add(this.labelMail);
            this.Controls.Add(this.labelProduct);
            this.Controls.Add(this.labelFIO);
            this.Controls.Add(this.controlInputRegexEmail);
            this.Controls.Add(this.controlSelectedComboBoxList);
            this.Name = "FormOrder";
            this.Text = "Заказ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormOrder_FormClosing);
            this.Load += new System.EventHandler(this.FormOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ControlsLibraryNet60.Selected.ControlSelectedComboBoxList controlSelectedComboBoxList;
        private Label labelFIO;
        private Label labelProduct;
        private Label labelMail;
        private Label labelImage;
        private Button buttonAddImage;
        private Button buttonSave;
        private TextBox textBoxFIO;
        private PictureBox pictureBox;
        private ControlsLibraryNet60.Input.ControlInputRegexEmail controlInputRegexEmail;
    }
}