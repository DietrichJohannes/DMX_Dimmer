namespace dmx_dimmer
{
    partial class ProjectBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectBrowser));
            listView1 = new ListView();
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Location = new Point(12, 92);
            listView1.Name = "listView1";
            listView1.Size = new Size(624, 214);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // button1
            // 
            button1.Image = Properties.Resources.plus_box_custom;
            button1.Location = new Point(12, 329);
            button1.Name = "button1";
            button1.Size = new Size(100, 100);
            button1.TabIndex = 1;
            button1.Text = "Neues Projekt";
            button1.TextAlign = ContentAlignment.BottomCenter;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Image = Properties.Resources.folder_custom1;
            button2.Location = new Point(536, 329);
            button2.Name = "button2";
            button2.Size = new Size(100, 100);
            button2.TabIndex = 2;
            button2.Text = "Projekt öffnen";
            button2.TextAlign = ContentAlignment.BottomCenter;
            button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.Font = new Font("Bahnschrift", 30F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(0, 192, 192);
            label1.Image = Properties.Resources.dmx_dimmer;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(624, 80);
            label1.TabIndex = 3;
            label1.Text = "DMX_DIMMER";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ProjectBrowser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(648, 443);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listView1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "ProjectBrowser";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Projekte";
            ResumeLayout(false);
        }

        #endregion

        private ListView listView1;
        private Button button1;
        private Button button2;
        private Label label1;
    }
}