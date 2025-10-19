namespace dmx_dimmer.scene_forms
{
    partial class Scene_rgb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scene_rgb));
            colorPicker = new ColorDialog();
            label1 = new Label();
            label2 = new Label();
            panelColorPreview = new Panel();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            label1.Text = "Gerät: ";
            // 
            // label2
            // 
            label2.Location = new Point(12, 32);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 1;
            label2.Text = "Adresse: ";
            // 
            // panelColorPreview
            // 
            panelColorPreview.Location = new Point(207, 62);
            panelColorPreview.Name = "panelColorPreview";
            panelColorPreview.Size = new Size(81, 41);
            panelColorPreview.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(213, 109);
            button1.Name = "button1";
            button1.Size = new Size(75, 41);
            button1.TabIndex = 3;
            button1.Text = "Farbe ändern";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Scene_rgb
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(300, 162);
            Controls.Add(button1);
            Controls.Add(panelColorPreview);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Scene_rgb";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gerät einrichten";
            ResumeLayout(false);
        }

        #endregion

        private ColorDialog colorPicker;
        private Label label1;
        private Label label2;
        private Panel panelColorPreview;
        private Button button1;
    }
}