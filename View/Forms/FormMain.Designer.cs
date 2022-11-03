namespace View
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.действияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьCtrlAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьCtrlUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьCtrlDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.документыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.простойДокументCtrlSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.документСТаблицейCtrlTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.диаграммаCtrlCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.продуктыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlDataTableTable = new ControlsLibraryNet60.Data.ControlDataTableTable();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникToolStripMenuItem,
            this.действияToolStripMenuItem,
            this.документыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(471, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникToolStripMenuItem
            // 
            this.справочникToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.продуктыToolStripMenuItem});
            this.справочникToolStripMenuItem.Name = "справочникToolStripMenuItem";
            this.справочникToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникToolStripMenuItem.Text = "Справочники";
            // 
            // действияToolStripMenuItem
            // 
            this.действияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьCtrlAToolStripMenuItem,
            this.изменитьCtrlUToolStripMenuItem,
            this.удалитьCtrlDToolStripMenuItem});
            this.действияToolStripMenuItem.Name = "действияToolStripMenuItem";
            this.действияToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.действияToolStripMenuItem.Text = "Действия";
            // 
            // добавитьCtrlAToolStripMenuItem
            // 
            this.добавитьCtrlAToolStripMenuItem.Name = "добавитьCtrlAToolStripMenuItem";
            this.добавитьCtrlAToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.добавитьCtrlAToolStripMenuItem.Text = "Добавить    Ctrl + A";
            this.добавитьCtrlAToolStripMenuItem.Click += new System.EventHandler(this.добавитьCtrlAToolStripMenuItem_Click);
            // 
            // изменитьCtrlUToolStripMenuItem
            // 
            this.изменитьCtrlUToolStripMenuItem.Name = "изменитьCtrlUToolStripMenuItem";
            this.изменитьCtrlUToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.изменитьCtrlUToolStripMenuItem.Text = "Изменить   Ctrl + U";
            this.изменитьCtrlUToolStripMenuItem.Click += new System.EventHandler(this.изменитьCtrlUToolStripMenuItem_Click);
            // 
            // удалитьCtrlDToolStripMenuItem
            // 
            this.удалитьCtrlDToolStripMenuItem.Name = "удалитьCtrlDToolStripMenuItem";
            this.удалитьCtrlDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.удалитьCtrlDToolStripMenuItem.Text = "Удалить       Ctrl + D";
            this.удалитьCtrlDToolStripMenuItem.Click += new System.EventHandler(this.удалитьCtrlDToolStripMenuItem_Click);
            // 
            // документыToolStripMenuItem
            // 
            this.документыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.простойДокументCtrlSToolStripMenuItem,
            this.документСТаблицейCtrlTToolStripMenuItem,
            this.диаграммаCtrlCToolStripMenuItem});
            this.документыToolStripMenuItem.Name = "документыToolStripMenuItem";
            this.документыToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.документыToolStripMenuItem.Text = "Документы";
            // 
            // простойДокументCtrlSToolStripMenuItem
            // 
            this.простойДокументCtrlSToolStripMenuItem.Name = "простойДокументCtrlSToolStripMenuItem";
            this.простойДокументCtrlSToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.простойДокументCtrlSToolStripMenuItem.Text = "Простой документ          Ctrl + S";
            this.простойДокументCtrlSToolStripMenuItem.Click += new System.EventHandler(this.простойДокументCtrlSToolStripMenuItem_Click);
            // 
            // документСТаблицейCtrlTToolStripMenuItem
            // 
            this.документСТаблицейCtrlTToolStripMenuItem.Name = "документСТаблицейCtrlTToolStripMenuItem";
            this.документСТаблицейCtrlTToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.документСТаблицейCtrlTToolStripMenuItem.Text = "Документ с таблицей     Ctrl + T";
            this.документСТаблицейCtrlTToolStripMenuItem.Click += new System.EventHandler(this.документСТаблицейCtrlTToolStripMenuItem_Click);
            // 
            // диаграммаCtrlCToolStripMenuItem
            // 
            this.диаграммаCtrlCToolStripMenuItem.Name = "диаграммаCtrlCToolStripMenuItem";
            this.диаграммаCtrlCToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.диаграммаCtrlCToolStripMenuItem.Text = "Диаграмма                       Ctrl + C";
            this.диаграммаCtrlCToolStripMenuItem.Click += new System.EventHandler(this.диаграммаCtrlCToolStripMenuItem_Click);
            // 
            // продуктыToolStripMenuItem
            // 
            this.продуктыToolStripMenuItem.Name = "продуктыToolStripMenuItem";
            this.продуктыToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.продуктыToolStripMenuItem.Text = "Продукты";
            this.продуктыToolStripMenuItem.Click += new System.EventHandler(this.продуктыToolStripMenuItem_Click);
            // 
            // controlDataTableTable
            // 
            this.controlDataTableTable.Location = new System.Drawing.Point(13, 36);
            this.controlDataTableTable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.controlDataTableTable.Name = "controlDataTableTable";
            this.controlDataTableTable.SelectedRowIndex = -1;
            this.controlDataTableTable.Size = new System.Drawing.Size(444, 320);
            this.controlDataTableTable.TabIndex = 4;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 370);
            this.Controls.Add(this.controlDataTableTable);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Заказы";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem справочникToolStripMenuItem;
        private ToolStripMenuItem действияToolStripMenuItem;
        private ToolStripMenuItem документыToolStripMenuItem;
        private ToolStripMenuItem добавитьCtrlAToolStripMenuItem;
        private ToolStripMenuItem изменитьCtrlUToolStripMenuItem;
        private ToolStripMenuItem удалитьCtrlDToolStripMenuItem;
        private ToolStripMenuItem простойДокументCtrlSToolStripMenuItem;
        private ToolStripMenuItem документСТаблицейCtrlTToolStripMenuItem;
        private ToolStripMenuItem диаграммаCtrlCToolStripMenuItem;
        private ToolStripMenuItem продуктыToolStripMenuItem;
        private ControlsLibraryNet60.Data.ControlDataTableTable controlDataTableTable;
    }
}