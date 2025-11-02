namespace dmx_dimmer
{
    partial class AddDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDevice));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            device_name = new TextBox();
            device_universe = new NumericUpDown();
            device_chanal = new NumericUpDown();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)device_universe).BeginInit();
            ((System.ComponentModel.ISupportInitialize)device_chanal).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 0;
            label1.Text = "Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 54);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 1;
            label2.Text = "Universum:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 97);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 2;
            label3.Text = "Startkanal";
            // 
            // device_name
            // 
            device_name.Location = new Point(128, 9);
            device_name.Name = "device_name";
            device_name.Size = new Size(204, 23);
            device_name.TabIndex = 3;
            // 
            // device_universe
            // 
            device_universe.Location = new Point(128, 46);
            device_universe.Name = "device_universe";
            device_universe.Size = new Size(204, 23);
            device_universe.TabIndex = 4;
            // 
            // device_chanal
            // 
            device_chanal.Location = new Point(128, 86);
            device_chanal.Name = "device_chanal";
            device_chanal.Size = new Size(204, 23);
            device_chanal.TabIndex = 5;
            // 
            // button1
            // 
            button1.Location = new Point(257, 126);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 6;
            button1.Text = "Speichern";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button_save_Click;
            // 
            // AddDevice
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(344, 161);
            Controls.Add(button1);
            Controls.Add(device_chanal);
            Controls.Add(device_universe);
            Controls.Add(device_name);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddDevice";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gerät erstellen";
            ((System.ComponentModel.ISupportInitialize)device_universe).EndInit();
            ((System.ComponentModel.ISupportInitialize)device_chanal).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox device_name;
        private NumericUpDown device_universe;
        private NumericUpDown device_chanal;
        private Button button1;
    }
}