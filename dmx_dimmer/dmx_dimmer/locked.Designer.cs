namespace dmx_dimmer
{
    partial class locked
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
            label1 = new Label();
            entered_password = new TextBox();
            label2 = new Label();
            hint = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(386, 41);
            label1.TabIndex = 0;
            label1.Text = "DMX_DIMMER gesperrt!";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // entered_password
            // 
            entered_password.Location = new Point(137, 88);
            entered_password.Name = "entered_password";
            entered_password.Size = new Size(261, 23);
            entered_password.TabIndex = 1;
            // 
            // label2
            // 
            label2.Location = new Point(12, 88);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 2;
            label2.Text = "Kennwort: ";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // hint
            // 
            hint.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            hint.ForeColor = Color.Red;
            hint.Location = new Point(137, 114);
            hint.Name = "hint";
            hint.Size = new Size(180, 21);
            hint.TabIndex = 3;
            hint.Text = "label";
            hint.TextAlign = ContentAlignment.MiddleLeft;
            hint.Visible = false;
            // 
            // button1
            // 
            button1.Location = new Point(323, 117);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Image = Properties.Resources.eye_outline;
            button2.Location = new Point(404, 88);
            button2.Name = "button2";
            button2.Size = new Size(23, 23);
            button2.TabIndex = 5;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // locked
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(439, 150);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(hint);
            Controls.Add(label2);
            Controls.Add(entered_password);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "locked";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gesperrt";
            FormClosing += locked_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox entered_password;
        private Label label2;
        private Label hint;
        private Button button1;
        private Button button2;
    }
}