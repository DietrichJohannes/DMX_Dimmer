namespace dmx_dimmer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            tabPage2 = new TabPage();
            button1 = new Button();
            button7 = new Button();
            tabPage3 = new TabPage();
            button8 = new Button();
            tabPage4 = new TabPage();
            button2 = new Button();
            toolStrip2 = new ToolStrip();
            toolStripButton3 = new ToolStripButton();
            toolStripButton1 = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btn_start_stop_sheduler = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripSplitButton1 = new ToolStripSplitButton();
            zurWebseiteToolStripMenuItem = new ToolStripMenuItem();
            überDenEntwicklerToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            button9 = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.BackColor = SystemColors.Control;
            label1.Font = new Font("Bahnschrift", 30F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Turquoise;
            label1.Image = Properties.Resources.dmx_dimmer;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(12, 189);
            label1.Name = "label1";
            label1.Size = new Size(994, 68);
            label1.TabIndex = 7;
            label1.Text = "DMX_Dimmer";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Location = new Point(12, 28);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(994, 158);
            tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = SystemColors.Control;
            tabPage1.Controls.Add(button6);
            tabPage1.Controls.Add(button5);
            tabPage1.Controls.Add(button4);
            tabPage1.Controls.Add(button3);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(986, 130);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Geräte";
            // 
            // button6
            // 
            button6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button6.BackColor = Color.Yellow;
            button6.ForeColor = Color.Red;
            button6.Image = (Image)resources.GetObject("button6.Image");
            button6.Location = new Point(746, 28);
            button6.Name = "button6";
            button6.Size = new Size(75, 75);
            button6.TabIndex = 12;
            button6.Text = "Black Out";
            button6.TextAlign = ContentAlignment.BottomCenter;
            button6.TextImageRelation = TextImageRelation.ImageAboveText;
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click_1;
            // 
            // button5
            // 
            button5.Image = Properties.Resources.track_light_custom;
            button5.Location = new Point(6, 30);
            button5.Name = "button5";
            button5.Size = new Size(75, 75);
            button5.TabIndex = 7;
            button5.Text = "Geräte";
            button5.TextAlign = ContentAlignment.BottomCenter;
            button5.TextImageRelation = TextImageRelation.ImageAboveText;
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click_1;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button4.Image = (Image)resources.GetObject("button4.Image");
            button4.Location = new Point(470, 28);
            button4.Name = "button4";
            button4.Size = new Size(75, 75);
            button4.TabIndex = 10;
            button4.Text = "Fader ";
            button4.TextAlign = ContentAlignment.BottomCenter;
            button4.TextImageRelation = TextImageRelation.ImageAboveText;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.Location = new Point(885, 28);
            button3.Name = "button3";
            button3.Size = new Size(95, 75);
            button3.TabIndex = 11;
            button3.Text = "Einstellungen";
            button3.TextAlign = ContentAlignment.BottomCenter;
            button3.TextImageRelation = TextImageRelation.ImageAboveText;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = SystemColors.Control;
            tabPage2.Controls.Add(button1);
            tabPage2.Controls.Add(button7);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(986, 130);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Szenen";
            // 
            // button1
            // 
            button1.Image = Properties.Resources.play_circle_custom;
            button1.Location = new Point(135, 30);
            button1.Name = "button1";
            button1.Size = new Size(75, 75);
            button1.TabIndex = 16;
            button1.Text = "Audio\r\nSzenen";
            button1.TextAlign = ContentAlignment.BottomCenter;
            button1.TextImageRelation = TextImageRelation.ImageAboveText;
            button1.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Image = (Image)resources.GetObject("button7.Image");
            button7.Location = new Point(6, 30);
            button7.Name = "button7";
            button7.Size = new Size(75, 75);
            button7.TabIndex = 15;
            button7.Text = "Geräte\r\nSzenen";
            button7.TextAlign = ContentAlignment.BottomCenter;
            button7.TextImageRelation = TextImageRelation.ImageAboveText;
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // tabPage3
            // 
            tabPage3.BackColor = SystemColors.Control;
            tabPage3.Controls.Add(button8);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(986, 130);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Effekte";
            // 
            // button8
            // 
            button8.Image = Properties.Resources.play_circle_custom;
            button8.ImageAlign = ContentAlignment.TopCenter;
            button8.Location = new Point(3, 30);
            button8.Name = "button8";
            button8.Size = new Size(75, 75);
            button8.TabIndex = 0;
            button8.Text = "Effekte";
            button8.TextAlign = ContentAlignment.BottomCenter;
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // tabPage4
            // 
            tabPage4.BackColor = SystemColors.Control;
            tabPage4.Controls.Add(button9);
            tabPage4.Controls.Add(button2);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(986, 130);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Steuerung";
            // 
            // button2
            // 
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.Location = new Point(3, 30);
            button2.Name = "button2";
            button2.Size = new Size(75, 75);
            button2.TabIndex = 10;
            button2.Text = "Textbuch";
            button2.TextAlign = ContentAlignment.BottomCenter;
            button2.TextImageRelation = TextImageRelation.ImageAboveText;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // toolStrip2
            // 
            toolStrip2.Dock = DockStyle.Bottom;
            toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip2.Location = new Point(0, 257);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.RenderMode = ToolStripRenderMode.System;
            toolStrip2.Size = new Size(1018, 25);
            toolStrip2.TabIndex = 9;
            toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton3.Image = Properties.Resources.plus_box_custom;
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(23, 22);
            toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = Properties.Resources.content_save_custom;
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(23, 22);
            toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = Properties.Resources.folder_custom;
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(23, 22);
            toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // btn_start_stop_sheduler
            // 
            btn_start_stop_sheduler.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btn_start_stop_sheduler.Image = Properties.Resources.play;
            btn_start_stop_sheduler.ImageTransparentColor = Color.Magenta;
            btn_start_stop_sheduler.Name = "btn_start_stop_sheduler";
            btn_start_stop_sheduler.Size = new Size(23, 22);
            btn_start_stop_sheduler.Text = "Sheduler Starten";
            btn_start_stop_sheduler.Click += startStopSheduler_Click;
            // 
            // toolStripButton4
            // 
            toolStripButton4.BackColor = Color.Yellow;
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton4.Image = (Image)resources.GetObject("toolStripButton4.Image");
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(23, 22);
            toolStripButton4.Text = "toolStripButton4";
            toolStripButton4.Click += toolStripButton4_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolStripSplitButton1
            // 
            toolStripSplitButton1.DropDownItems.AddRange(new ToolStripItem[] { zurWebseiteToolStripMenuItem, überDenEntwicklerToolStripMenuItem });
            toolStripSplitButton1.Name = "toolStripSplitButton1";
            toolStripSplitButton1.Size = new Size(44, 22);
            toolStripSplitButton1.Text = "Info";
            // 
            // zurWebseiteToolStripMenuItem
            // 
            zurWebseiteToolStripMenuItem.Name = "zurWebseiteToolStripMenuItem";
            zurWebseiteToolStripMenuItem.Size = new Size(179, 22);
            zurWebseiteToolStripMenuItem.Text = "Zur Webseite";
            // 
            // überDenEntwicklerToolStripMenuItem
            // 
            überDenEntwicklerToolStripMenuItem.Name = "überDenEntwicklerToolStripMenuItem";
            überDenEntwicklerToolStripMenuItem.Size = new Size(179, 22);
            überDenEntwicklerToolStripMenuItem.Text = "Über den Entwickler";
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton3, toolStripButton1, toolStripButton2, toolStripSeparator1, btn_start_stop_sheduler, toolStripButton4, toolStripSeparator2, toolStripSplitButton1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.RenderMode = ToolStripRenderMode.System;
            toolStrip1.Size = new Size(1018, 25);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // button9
            // 
            button9.Image = (Image)resources.GetObject("button9.Image");
            button9.Location = new Point(84, 30);
            button9.Name = "button9";
            button9.Size = new Size(75, 75);
            button9.TabIndex = 11;
            button9.Text = "Textbuch";
            button9.TextAlign = ContentAlignment.BottomCenter;
            button9.TextImageRelation = TextImageRelation.ImageAboveText;
            button9.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1018, 282);
            Controls.Add(toolStrip2);
            Controls.Add(tabControl1);
            Controls.Add(label1);
            Controls.Add(toolStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "DMX_Dimmer";
            FormClosing += Form1_FormClosing;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Button button7;
        private Button button2;
        private ToolStrip toolStrip2;
        private Button button8;
        private Button button1;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btn_start_stop_sheduler;
        private ToolStripButton toolStripButton4;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripMenuItem zurWebseiteToolStripMenuItem;
        private ToolStripMenuItem überDenEntwicklerToolStripMenuItem;
        private ToolStrip toolStrip1;
        private Button button9;
    }
}
