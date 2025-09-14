namespace dmx_dimmer
{
    partial class TextBook
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextBook));
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            richTextBox1 = new RichTextBox();
            listView1 = new ListView();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(122, 606);
            button1.Name = "button1";
            button1.Size = new Size(104, 51);
            button1.TabIndex = 0;
            button1.Text = "Go!";
            button1.TextAlign = ContentAlignment.MiddleRight;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.Location = new Point(232, 606);
            button2.Name = "button2";
            button2.Size = new Size(104, 51);
            button2.TabIndex = 1;
            button2.Text = "Nächster";
            button2.TextAlign = ContentAlignment.MiddleRight;
            button2.TextImageRelation = TextImageRelation.ImageBeforeText;
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.Location = new Point(12, 606);
            button3.Name = "button3";
            button3.Size = new Size(104, 51);
            button3.TabIndex = 2;
            button3.Text = "Zurück";
            button3.TextAlign = ContentAlignment.MiddleRight;
            button3.TextImageRelation = TextImageRelation.ImageBeforeText;
            button3.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.Location = new Point(12, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(896, 485);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            listView1.Location = new Point(12, 503);
            listView1.Name = "listView1";
            listView1.Size = new Size(896, 97);
            listView1.TabIndex = 4;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // TextBook
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(920, 686);
            Controls.Add(listView1);
            Controls.Add(richTextBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "TextBook";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Text Buch";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private RichTextBox richTextBox1;
        private ListView listView1;
    }
}