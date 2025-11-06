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
            password = new TextBox();
            label4 = new Label();
            button2 = new Button();
            label5 = new Label();
            label6 = new Label();
            panel1 = new Panel();
            label7 = new Label();
            webserver_port = new NumericUpDown();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)dmx_fps).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webserver_port).BeginInit();
            SuspendLayout();
            // 
            // ip_label
            // 
            ip_label.Location = new Point(159, 43);
            ip_label.Mask = "000\\.000\\.0\\.000";
            ip_label.Name = "ip_label";
            ip_label.Size = new Size(162, 23);
            ip_label.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 51);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 1;
            label1.Text = "ArtNet Node IP";
            // 
            // button1
            // 
            button1.Image = Properties.Resources.content_save_custom;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(808, 408);
            button1.Name = "button1";
            button1.Size = new Size(113, 41);
            button1.TabIndex = 2;
            button1.Text = "Speichern";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnSave_Click;
            // 
            // chkSendOnlyWhenDirty
            // 
            chkSendOnlyWhenDirty.Location = new Point(204, 143);
            chkSendOnlyWhenDirty.Name = "chkSendOnlyWhenDirty";
            chkSendOnlyWhenDirty.Size = new Size(17, 19);
            chkSendOnlyWhenDirty.TabIndex = 3;
            chkSendOnlyWhenDirty.UseVisualStyleBackColor = true;
            chkSendOnlyWhenDirty.CheckedChanged += chkSendOnlyWhenDirty_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 147);
            label2.Name = "label2";
            label2.Size = new Size(183, 15);
            label2.TabIndex = 4;
            label2.Text = "DMX nur bei Änderungen senden";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 82);
            label3.Name = "label3";
            label3.Size = new Size(73, 45);
            label3.TabIndex = 5;
            label3.Text = "DMX\r\nSenderate\r\npro Sekunde";
            // 
            // dmx_fps
            // 
            dmx_fps.Location = new Point(159, 82);
            dmx_fps.Maximum = new decimal(new int[] { 40, 0, 0, 0 });
            dmx_fps.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            dmx_fps.Name = "dmx_fps";
            dmx_fps.Size = new Size(163, 23);
            dmx_fps.TabIndex = 6;
            dmx_fps.Value = new decimal(new int[] { 30, 0, 0, 0 });
            dmx_fps.ValueChanged += dmx_fps_ValueChanged;
            // 
            // password
            // 
            password.Location = new Point(704, 57);
            password.Name = "password";
            password.Size = new Size(133, 23);
            password.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(557, 57);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 8;
            label4.Text = "Passwort";
            // 
            // button2
            // 
            button2.Image = Properties.Resources.eye_outline;
            button2.Location = new Point(843, 57);
            button2.Name = "button2";
            button2.Size = new Size(23, 23);
            button2.TabIndex = 9;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(12, 9);
            label5.Name = "label5";
            label5.Size = new Size(76, 21);
            label5.TabIndex = 10;
            label5.Text = "Ausgabe";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(557, 9);
            label6.Name = "label6";
            label6.Size = new Size(175, 21);
            label6.TabIndex = 11;
            label6.Text = "DMX_DIMMER Schutz";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Location = new Point(448, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(2, 437);
            panel1.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(12, 198);
            label7.Name = "label7";
            label7.Size = new Size(92, 21);
            label7.TabIndex = 13;
            label7.Text = "Webserver";
            // 
            // webserver_port
            // 
            webserver_port.Location = new Point(159, 238);
            webserver_port.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            webserver_port.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            webserver_port.Name = "webserver_port";
            webserver_port.Size = new Size(163, 23);
            webserver_port.TabIndex = 14;
            webserver_port.Value = new decimal(new int[] { 80, 0, 0, 0 });
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 238);
            label8.Name = "label8";
            label8.Size = new Size(29, 15);
            label8.TabIndex = 15;
            label8.Text = "Port";
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 461);
            Controls.Add(label8);
            Controls.Add(webserver_port);
            Controls.Add(label7);
            Controls.Add(panel1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(password);
            Controls.Add(dmx_fps);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(chkSendOnlyWhenDirty);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(ip_label);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Settings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Einstellungen";
            ((System.ComponentModel.ISupportInitialize)dmx_fps).EndInit();
            ((System.ComponentModel.ISupportInitialize)webserver_port).EndInit();
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
        private TextBox password;
        private Label label4;
        private Button button2;
        private Label label5;
        private Label label6;
        private Panel panel1;
        private Label label7;
        private NumericUpDown webserver_port;
        private Label label8;
    }
}