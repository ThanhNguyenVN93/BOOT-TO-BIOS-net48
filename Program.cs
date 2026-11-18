using System;
using System.Threading;
using System.Windows.Forms;

namespace frmbootobios
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            bool createdNew;
            using (var mutex = new Mutex(true, "BootToBIOS_NET40_SingleInstance", out createdNew))
            {
                if (!createdNew)
                {
                    MessageBox.Show("Ứng dụng đã đang chạy!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
    }
}
