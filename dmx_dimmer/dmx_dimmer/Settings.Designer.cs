namespace dmx_dimmer
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            ip_label = new MaskedTextBox();
            label1 = new Label();
            button1 = new Button();
            chkSendOnlyWhenDirty = new CheckBox();
            label2 = new Label();
            label3 = new Label();
            dmx_fps = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)dmx_fps).BeginInit();
            SuspendLayout();
            // 
            // ip_label
            // 
            ip_label.Location = new Point(157, 12);
            ip_label.Mask = "000\\.000\\.0\\.000";
            ip_label.Name = "ip_label";
            ip_label.Size = new Size(133, 23);
            ip_label.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 20);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 1;
            label1.Text = "ArtNet Node IP";
            // 
            // button1
            // 
            button1.Location = new Point(310, 171);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Speichern";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnSave_Click;
            // 
            // chkSendOnlyWhenDirty
            // 
            chkSendOnlyWhenDirty.Location = new Point(208, 109);
            chkSendOnlyWhenDirty.Name = "chkSendOnlyWhenDirty";
            chkSendOnlyWhenDirty.Size = new Size(17, 19);
            chkSendOnlyWhenDirty.TabIndex = 3;
            chkSendOnlyWhenDirty.UseVisualStyleBackColor = true;
            chkSendOnlyWhenDirty.CheckedChanged += chkSendOnlyWhenDirty_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 113);
            label2.Name = "label2";
            label2.Size = new Size(183, 15);
            label2.TabIndex = 4;
            label2.Text = "DMX nur bei Änderungen senden";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 53);
            label3.Name = "label3";
            label3.Size = new Size(73, 45);
            label3.TabIndex = 5;
            label3.Text = "DMX\r\nSenderate\r\npro Sekunde";
            // 
            // dmx_fps
            // 
            dmx_fps.Location = new Point(157, 53);
            dmx_fps.Maximum = new decimal(new int[] { 40, 0, 0, 0 });
            dmx_fps.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            dmx_fps.Name = "dmx_fps";
            dmx_fps.Size = new Size(133, 23);
            dmx_fps.TabIndex = 6;
            dmx_fps.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(397, 206);
            Controls.Add(dmx_fps);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(chkSendOnlyWhenDirty);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(ip_label);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Settings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Einstellungen";
            ((System.ComponentModel.ISupportInitialize)dmx_fps).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaskedTextBox ip_label;
        private Label label1;
        private Button button1;
        private CheckBox chkSendOnlyWhenDirty;
        private Label label2;
        private Label label3;
        private NumericUpDown dmx_fps;
    }
}