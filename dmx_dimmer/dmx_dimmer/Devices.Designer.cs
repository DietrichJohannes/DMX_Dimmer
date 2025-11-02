namespace dmx_dimmer
{
    partial class Devices
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Devices));
            treeView1 = new TreeView();
            device_tree = new TreeView();
            add_device = new Button();
            proj_device = new ListView();
            device_info = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.Location = new Point(589, 295);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(616, 236);
            treeView1.TabIndex = 2;
            // 
            // device_tree
            // 
            device_tree.Location = new Point(12, 12);
            device_tree.Name = "device_tree";
            device_tree.RightToLeft = RightToLeft.Yes;
            device_tree.Size = new Size(388, 519);
            device_tree.TabIndex = 3;
            // 
            // add_device
            // 
            add_device.Image = Properties.Resources.plus_custom;
            add_device.Location = new Point(406, 496);
            add_device.Name = "add_device";
            add_device.Size = new Size(55, 35);
            add_device.TabIndex = 4;
            add_device.Tag = "";
            add_device.UseVisualStyleBackColor = true;
            add_device.Click += add_device_Click;
            // 
            // proj_device
            // 
            proj_device.Location = new Point(589, 12);
            proj_device.Name = "proj_device";
            proj_device.Size = new Size(616, 277);
            proj_device.TabIndex = 5;
            proj_device.UseCompatibleStateImageBehavior = false;
            // 
            // device_info
            // 
            device_info.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            device_info.Location = new Point(406, 12);
            device_info.Name = "device_info";
            device_info.Size = new Size(177, 236);
            device_info.TabIndex = 6;
            device_info.Text = "Beschreibung";
            // 
            // button1
            // 
            button1.Image = Properties.Resources.pencil_custom;
            button1.Location = new Point(467, 496);
            button1.Name = "button1";
            button1.Size = new Size(55, 35);
            button1.TabIndex = 7;
            button1.Tag = "";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Image = Properties.Resources.delete_custom;
            button2.Location = new Point(528, 496);
            button2.Name = "button2";
            button2.Size = new Size(55, 35);
            button2.TabIndex = 8;
            button2.Tag = "";
            button2.UseVisualStyleBackColor = true;
            // 
            // Devices
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1217, 543);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(device_info);
            Controls.Add(proj_device);
            Controls.Add(add_device);
            Controls.Add(device_tree);
            Controls.Add(treeView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Devices";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Geräte";
            ResumeLayout(false);
        }

        #endregion
        private TreeView treeView1;
        private TreeView device_tree;
        private Button add_device;
        private ListView proj_device;
        private Label device_info;
        private Button button1;
        private Button button2;
    }
}