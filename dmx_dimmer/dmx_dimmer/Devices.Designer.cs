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
            TreeNode treeNode1 = new TreeNode("RGBA-Leuchte");
            TreeNode treeNode2 = new TreeNode("Dimmer");
            TreeNode treeNode3 = new TreeNode("Generic", new TreeNode[] { treeNode1, treeNode2 });
            listView2 = new ListView();
            treeView1 = new TreeView();
            treeView2 = new TreeView();
            SuspendLayout();
            // 
            // listView2
            // 
            listView2.Location = new Point(606, 235);
            listView2.Name = "listView2";
            listView2.Size = new Size(347, 210);
            listView2.TabIndex = 1;
            listView2.UseCompatibleStateImageBehavior = false;
            listView2.SelectedIndexChanged += listView2_SelectedIndexChanged;
            // 
            // treeView1
            // 
            treeView1.Location = new Point(606, 12);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(347, 192);
            treeView1.TabIndex = 2;
            // 
            // treeView2
            // 
            treeView2.Location = new Point(12, 12);
            treeView2.Name = "treeView2";
            treeNode1.Name = "Knoten1";
            treeNode1.Text = "RGBA-Leuchte";
            treeNode2.Name = "Knoten2";
            treeNode2.Text = "Dimmer";
            treeNode3.Name = "Knoten0";
            treeNode3.Text = "Generic";
            treeView2.Nodes.AddRange(new TreeNode[] { treeNode3 });
            treeView2.Size = new Size(388, 433);
            treeView2.TabIndex = 3;
            // 
            // Devices
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(965, 457);
            Controls.Add(treeView2);
            Controls.Add(treeView1);
            Controls.Add(listView2);
            Name = "Devices";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Geräte";
            ResumeLayout(false);
        }

        #endregion
        private ListView listView2;
        private TreeView treeView1;
        private TreeView treeView2;
    }
}