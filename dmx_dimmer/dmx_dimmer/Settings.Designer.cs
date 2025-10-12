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
            chkSendOnlyWhenDirty.Location = new Point(275, 54);
            chkSendOnlyWhenDirty.Name = "chkSendOnlyWhenDirty";
            chkSendOnlyWhenDirty.Size = new Size(82, 19);
            chkSendOnlyWhenDirty.TabIndex = 3;
            chkSendOnlyWhenDirty.UseVisualStyleBackColor = true;
            chkSendOnlyWhenDirty.CheckedChanged += chkSendOnlyWhenDirty_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 53);
            label2.Name = "label2";
            label2.Size = new Size(183, 15);
            label2.TabIndex = 4;
            label2.Text = "DMX nur bei Änderungen senden";
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(397, 206);
            Controls.Add(label2);
            Controls.Add(chkSendOnlyWhenDirty);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(ip_label);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Settings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Einstellungen";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaskedTextBox ip_label;
        private Label label1;
        private Button button1;
        private CheckBox chkSendOnlyWhenDirty;
        private Label label2;
    }
}