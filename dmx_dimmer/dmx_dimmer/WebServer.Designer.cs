namespace dmx_dimmer
{
    partial class WebServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebServer));
            panel1 = new Panel();
            lblPwdStatus = new Label();
            linkAddress = new LinkLabel();
            label3 = new Label();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            label4 = new Label();
            button2 = new Button();
            button3 = new Button();
            label5 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(lblPwdStatus);
            panel1.Controls.Add(linkAddress);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(447, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(247, 200);
            panel1.TabIndex = 0;
            // 
            // lblPwdStatus
            // 
            lblPwdStatus.AutoSize = true;
            lblPwdStatus.Location = new Point(119, 55);
            lblPwdStatus.Name = "lblPwdStatus";
            lblPwdStatus.Size = new Size(0, 15);
            lblPwdStatus.TabIndex = 5;
            // 
            // linkAddress
            // 
            linkAddress.Location = new Point(67, 16);
            linkAddress.Name = "linkAddress";
            linkAddress.Size = new Size(159, 23);
            linkAddress.TabIndex = 4;
            linkAddress.TabStop = true;
            linkAddress.Text = "http://192.168.255.255:8080\r\n";
            linkAddress.LinkClicked += linkAddress_LinkClicked;
            // 
            // label3
            // 
            label3.Location = new Point(3, 55);
            label3.Name = "label3";
            label3.Size = new Size(110, 15);
            label3.TabIndex = 3;
            label3.Text = "Kennwort aktiviert:";
            // 
            // label1
            // 
            label1.Location = new Point(3, 16);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 2;
            label1.Text = "Adresse:";
            // 
            // label2
            // 
            label2.Location = new Point(447, 69);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 1;
            label2.Text = "label2";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(0, 192, 0);
            button1.Location = new Point(213, 5);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Starten";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 9);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 3;
            label4.Text = "Server";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // button2
            // 
            button2.Location = new Point(322, 189);
            button2.Name = "button2";
            button2.Size = new Size(119, 23);
            button2.TabIndex = 4;
            button2.Text = "Canva bearbeiten";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(0, 192, 0);
            button3.Location = new Point(213, 47);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 5;
            button3.Text = "Aktiv";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 51);
            label5.Name = "label5";
            label5.Size = new Size(95, 15);
            label5.TabIndex = 6;
            label5.Text = "Kennwort schutz";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // WebServer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(706, 224);
            Controls.Add(label5);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(button1);
            Controls.Add(panel1);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "WebServer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Server Steuerung";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private LinkLabel linkAddress;
        private Button button1;
        private Label label4;
        private Button button2;
        private Button button3;
        private Label label5;
        private Label lblPwdStatus;
    }
}