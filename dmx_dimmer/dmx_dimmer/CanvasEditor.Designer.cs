namespace dmx_dimmer
{
    partial class CanvasEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CanvasEditor));
            canvasPanel = new Panel();
            SuspendLayout();
            // 
            // canvasPanel
            // 
            canvasPanel.Location = new Point(12, 12);
            canvasPanel.Name = "canvasPanel";
            canvasPanel.Size = new Size(776, 426);
            canvasPanel.TabIndex = 0;
            // 
            // CanvasEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(canvasPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "CanvasEditor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Canva bearbeiten";
            ResumeLayout(false);
        }

        #endregion

        private Panel canvasPanel;
    }
}