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
            proj_devices = new ListView();
            device_info = new Label();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.Location = new Point(606, 12);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(625, 236);
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
            add_device.Location = new Point(406, 254);
            add_device.Name = "add_device";
            add_device.Size = new Size(40, 25);
            add_device.TabIndex = 4;
            add_device.Tag = "";
            add_device.UseVisualStyleBackColor = true;
            add_device.Click += add_device_Click;
            // 
            // proj_devices
            // 
            proj_devices.Location = new Point(606, 254);
            proj_devices.Name = "proj_devices";
            proj_devices.Size = new Size(615, 277);
            proj_devices.TabIndex = 5;
            proj_devices.UseCompatibleStateImageBehavior = false;
            // 
            // device_info
            // 
            device_info.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            device_info.Location = new Point(406, 12);
            device_info.Name = "device_info";
            device_info.Size = new Size(194, 236);
            device_info.TabIndex = 6;
            device_info.Text = "Beschreibung";
            // 
            // Devices
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1243, 543);
            Controls.Add(device_info);
            Controls.Add(proj_devices);
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
        private ListView proj_devices;
        private Label device_info;
    }
}