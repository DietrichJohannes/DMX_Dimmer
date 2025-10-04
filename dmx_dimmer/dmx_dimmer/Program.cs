namespace dmx_dimmer
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var picker = new ProjectBrowser())
            {
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new Form1());
                }

            }
        }
    }
}
