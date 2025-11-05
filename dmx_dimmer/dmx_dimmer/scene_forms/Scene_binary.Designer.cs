namespace dmx_dimmer.scene_forms
{
    partial class Scene_binary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scene_binary));
            button1 = new Button();
            button2 = new Button();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Image = Properties.Resources.off;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Location = new Point(196, 93);
            button1.Name = "button1";
            button1.Size = new Size(96, 56);
            button1.TabIndex = 0;
            button1.Text = "    AUS";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Image = Properties.Resources.on;
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Location = new Point(12, 93);
            button2.Name = "button2";
            button2.Size = new Size(96, 56);
            button2.TabIndex = 1;
            button2.Text = "    EIN";
            button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Location = new Point(8, 32);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 3;
            label2.Text = "Adresse: ";
            // 
            // label1
            // 
            label1.Location = new Point(8, 9);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 2;
            label1.Text = "Gerät: ";
            // 
            // Scene_binary
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(304, 161);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Scene_binary";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gerät einrichten";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Label label2;
        private Label label1;
    }
}