namespace dmx_dimmer
{
    partial class AddMissingPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddMissingPassword));
            label1 = new Label();
            label2 = new Label();
            password = new TextBox();
            btn_ok = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(226, 30);
            label1.TabIndex = 0;
            label1.Text = "Es scheint das kein Kennwort vergeben ist\r\nvergieb jetzt ein Kennwort!";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 83);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 1;
            label2.Text = "Kennwort";
            // 
            // password
            // 
            password.Location = new Point(108, 80);
            password.Name = "password";
            password.Size = new Size(259, 23);
            password.TabIndex = 2;
            // 
            // btn_ok
            // 
            btn_ok.Location = new Point(292, 109);
            btn_ok.Name = "btn_ok";
            btn_ok.Size = new Size(75, 23);
            btn_ok.TabIndex = 3;
            btn_ok.Text = "OK";
            btn_ok.UseVisualStyleBackColor = true;
            btn_ok.Click += btnOk_Click;
            // 
            // button2
            // 
            button2.Image = Properties.Resources.eye_outline;
            button2.Location = new Point(373, 80);
            button2.Name = "button2";
            button2.Size = new Size(23, 23);
            button2.TabIndex = 6;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // AddMissingPassword
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(403, 140);
            Controls.Add(button2);
            Controls.Add(btn_ok);
            Controls.Add(password);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddMissingPassword";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Passwort vergeben";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox password;
        private Button btn_ok;
        private Button button2;
    }
}