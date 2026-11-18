using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace frmbootobios
{
    public partial class Form1 : Form
    {
        // Thư viện để tắt cơ chế điều hướng file của Windows
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        public Form1()
        {
            InitializeComponent();
            lblStatusText.Text = "Ready to run system check...";
            btnCreateShortcut.Visible = false;
        }

        private void btnStartCheck_Click(object sender, EventArgs e)
        {
            bool hardwareSupport = CheckHardwareCapability();

            if (!hardwareSupport)
            {
                UpdateUI("Legacy Only", "Hardware does not support UEFI.", Color.LightCoral);
                btnCreateShortcut.Visible = false;
                return;
            }

            bool isWindowsUEFI = CheckWindowsBootMode();

            if (isWindowsUEFI)
            {
                UpdateUI("UEFI Mode", "System is running UEFI. You can create a shortcut.", Color.LightGreen);
                btnCreateShortcut.Visible = true;
                btnStartCheck.Enabled = false;
            }
            else
            {
                UpdateUI("MBR/Legacy", "Hardware supports UEFI but Windows is running in Legacy (MBR) mode.", Color.Khaki);
                btnCreateShortcut.Visible = false;
                MessageBox.Show("To use the shortcut, convert the disk to GPT and reinstall Windows in UEFI mode.", "Note");
                return;
            }
        }

        // --- HÀM LOGIC CHI TIẾT ---
        private void UpdateUI(string mainText, string detailText, Color backColor)
        {
           //lblHeader.Text = mainText;
            lblStatusText.Text = detailText;
            this.BackColor = backColor;
        }

        private bool CheckHardwareCapability()
        {
            try
            {
                // Cách 1: Quét WMI (Không bị ảnh hưởng bởi redirection)
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT BiosCharacteristics FROM Win32_BIOS");
                foreach (ManagementObject obj in searcher.Get())
                {
                    ushort[] characteristics = (ushort[])obj["BiosCharacteristics"];
                    int[] uefiCodes = { 39, 40, 41, 42, 43 };
                    if (characteristics != null && characteristics.Any(c => uefiCodes.Contains(c))) return true;
                }

                // Cách 2: Chạy bcdedit với việc tắt redirection
                string bcdPath = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "System32", "bcdedit.exe");
                string output = RunCommand(bcdPath, "/enum firmware");

                if (output.Contains(".efi") || output.Contains("{fwbootmgr}")) return true;
            }
            catch { }
            return false;
        }

        private bool CheckWindowsBootMode()
        {
            string bcdPath = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "System32", "bcdedit.exe");
            string output = RunCommand(bcdPath, "");
            return output.Contains("winload.efi");
        }

        // Hàm chạy lệnh hệ thống an toàn, tránh lỗi "File Not Found"
        private string RunCommand(string fileName, string args)
        {
            IntPtr wow64Value = IntPtr.Zero;
            string output = "";
            try
            {
                // Tắt redirection để tìm thấy bcdedit trong System32
                Wow64DisableWow64FsRedirection(ref wow64Value);

                ProcessStartInfo psi = new ProcessStartInfo(fileName, args)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process p = Process.Start(psi))
                {
                    output = p.StandardOutput.ReadToEnd();
                    p.WaitForExit();
                }
            }
            finally
            {
                // Bật lại redirection sau khi xong
                Wow64RevertWow64FsRedirection(wow64Value);
            }
            return output;
        }

        private void btnCreateShortcut_Click(object sender, EventArgs e)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string shortcutPath = Path.Combine(desktopPath, "Access to BIOS.lnk");

                // .NET 3.5 không có 'dynamic' — dùng Reflection để gọi WScript.Shell COM
                Type shellType = Type.GetTypeFromProgID("WScript.Shell");
                object shell = Activator.CreateInstance(shellType);
                object shortcut = shellType.InvokeMember("CreateShortcut", BindingFlags.InvokeMethod, null, shell,
                    new object[] { shortcutPath });
                Type shortcutType = shortcut.GetType();

                shortcutType.InvokeMember("TargetPath", BindingFlags.SetProperty, null, shortcut,
                    new object[] { Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "System32", "shutdown.exe") });
                shortcutType.InvokeMember("Arguments", BindingFlags.SetProperty, null, shortcut,
                    new object[] { "/r /fw /t 0" });
                shortcutType.InvokeMember("Description", BindingFlags.SetProperty, null, shortcut,
                    new object[] { "Restart the system and enter BIOS directly" });
                shortcutType.InvokeMember("IconLocation", BindingFlags.SetProperty, null, shortcut,
                    new object[] { Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "System32", "shell32.dll") + ",238" });
                shortcutType.InvokeMember("Save", BindingFlags.InvokeMethod, null, shortcut, null);

                MessageBox.Show("Shortcut created on Desktop!", "Success!");
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Shortcut creation failed: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnStartCheck.PerformClick();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            this.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
