namespace dmx_dimmer
{
    partial class GraphicStageView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphicStageView));
            button1 = new Button();
            panelStage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)panelStage).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Image = Properties.Resources.pencil_custom;
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(40, 36);
            button1.TabIndex = 1;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panelStage
            // 
            panelStage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelStage.Image = Properties.Resources.Bühnenansicht;
            panelStage.Location = new Point(12, 54);
            panelStage.Name = "panelStage";
            panelStage.Size = new Size(921, 474);
            panelStage.SizeMode = PictureBoxSizeMode.Zoom;
            panelStage.TabIndex = 2;
            panelStage.TabStop = false;
            // 
            // GraphicStageView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(945, 540);
            Controls.Add(panelStage);
            Controls.Add(button1);
            Cursor = Cursors.Hand;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GraphicStageView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bühnenansicht";
            ((System.ComponentModel.ISupportInitialize)panelStage).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private PictureBox panelStage;
    }
}