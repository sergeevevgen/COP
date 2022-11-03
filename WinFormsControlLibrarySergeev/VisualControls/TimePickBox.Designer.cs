namespace WinFormsControlLibrarySergeev
{
    partial class TimePickBox
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.labelError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(0, 0);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 23);
            this.dateTimePicker.TabIndex = 0;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // labelError
            // 
            this.labelError.Location = new System.Drawing.Point(0, 23);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(200, 20);
            this.labelError.TabIndex = 1;
            // 
            // TimePickBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dateTimePicker);
            this.Name = "TimePickBox";
            this.Size = new System.Drawing.Size(200, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label labelError;
    }
}
