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
            ip_label = new MaskedTextBox();
            label1 = new Label();
            button1 = new Button();
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
            button1.Location = new Point(448, 363);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Anwenden";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnSave_Click;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(535, 398);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(ip_label);
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
    }
}