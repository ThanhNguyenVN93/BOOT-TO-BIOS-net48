using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace frmbootobios
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);

        // Win8+ API: 1=Legacy/BIOS, 2=UEFI
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetFirmwareType(ref uint FirmwareType);
        private bool _vi = true;

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(220, 235, 250); // NET40: xanh dương nhạt
            lblStatusText.Text = "Sẵn sàng kiểm tra...";
            btnCreateShortcut.Visible = false;
        }

        private void btnLang_Click(object sender, EventArgs e)
        {
            _vi = !_vi;
            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            btnLang.Text = _vi ? "EN" : "VI";
            btnStartCheck.Text = _vi ? "Kiểm tra BIOS" : "Check BIOS";
            btnCheckWin11.Text = _vi ? "Kiểm tra Win 11" : "Check Win 11";
            btnCreateShortcut.Text = _vi ? "Tạo Shortcut" : "Create Shortcut";
            groupBox5.Text = _vi ? "Yêu cầu Windows 11" : "Windows 11 Requirements";
        }

        private void lnkDonate_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://nguyenthanhdev.com/donate");
        }

        private void btnStartCheck_Click(object sender, EventArgs e)
        {
            bool hardwareSupport = CheckHardwareCapability();

            if (!hardwareSupport)
            {
                UpdateUI("Legacy Only", "Phần cứng không hỗ trợ UEFI.", Color.FromArgb(220, 170, 170));
                btnCreateShortcut.Visible = false;
                return;
            }

            bool isWindowsUEFI = CheckWindowsBootMode();

            if (isWindowsUEFI)
            {
                UpdateUI("UEFI Mode", "Hệ thống đang chạy UEFI. Có thể tạo shortcut.", Color.FromArgb(170, 220, 170));
                btnCreateShortcut.Visible = true;
                
                btnStartCheck.Enabled = false;
            }
            else
            {
                UpdateUI("MBR/Legacy", "Phần cứng hỗ trợ UEFI nhưng Windows đang chạy ở chế độ Legacy (MBR).", Color.FromArgb(220, 210, 150));
                btnCreateShortcut.Visible = false;
                MessageBox.Show("Để dùng shortcut, cần chuyển đĩa sang GPT và cài lại Windows ở chế độ UEFI.", "Lưu ý");
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
                string bcdPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "System32", "bcdedit.exe");
                string output = RunCommand(bcdPath, "/enum firmware");

                if (output.Contains(".efi") || output.Contains("{fwbootmgr}")) return true;
            }
            catch { }
            return false;
        }
        private bool CheckWindowsBootMode()
        {
            uint firmwareType = 0;
            if (GetFirmwareType(ref firmwareType))
                return firmwareType == 2;
            // Fallback for pre-Win8
            string bcdPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "System32", "bcdedit.exe");
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

                Type shellType = Type.GetTypeFromProgID("WScript.Shell");
                dynamic shell = Activator.CreateInstance(shellType);
                var shortcut = shell.CreateShortcut(shortcutPath);

                shortcut.TargetPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "System32", "shutdown.exe");
                shortcut.Arguments = "/r /fw /t 0";
                shortcut.Description = "Restart the system and enter BIOS directly";
                shortcut.IconLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "System32", "shell32.dll") + ",238";
                shortcut.Save();

                MessageBox.Show(
                    _vi ? "Đã tạo shortcut trên Desktop!" : "Shortcut created on Desktop!",
                    _vi ? "Thành công!" : "Success!");
                var boot = MessageBox.Show(
                    _vi ? "Bạn có muốn boot vào BIOS ngay bây giờ không?" : "Do you want to boot into BIOS right now?",
                    _vi ? "Boot vào BIOS?" : "Boot to BIOS?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (boot == DialogResult.Yes)
                {
                    string shutdownPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "System32", "shutdown.exe");
                    RunCommand(shutdownPath, "/r /fw /t 0");
                }
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tạo shortcut thất bại: " + ex.Message);
            }
        }

        private void btnCheckWin11_Click(object sender, EventArgs e)
        {
            // Source - https://stackoverflow.com/a/50224392 (Rovann Linhalis, CC BY-SA 4.0)
            string osCaption = "";
            string osArch = "";
            try
            {
                using (var osSearcher = new ManagementObjectSearcher("SELECT Caption, OSArchitecture FROM Win32_OperatingSystem"))
                {
                    foreach (ManagementObject obj in osSearcher.Get())
                    {
                        osCaption = obj["Caption"]?.ToString() ?? "";
                        osArch = obj["OSArchitecture"]?.ToString() ?? "";
                        break;
                    }
                }
            }
            catch { }
            lblW11OsVersion.Text = "HĐH: " + osCaption + (osArch.Length > 0 ? " – " + osArch : "");

            bool alreadyWin11 = osCaption.IndexOf("Windows 11", StringComparison.OrdinalIgnoreCase) >= 0;

            bool uefi = CheckWindowsBootMode();

            bool tpm2 = false;
            try
            {
                var searcher = new ManagementObjectSearcher(@"root\CIMv2\Security\MicrosoftTpm", "SELECT * FROM Win32_Tpm");
                foreach (ManagementObject obj in searcher.Get())
                {
                    string spec = obj["SpecVersion"]?.ToString() ?? "";
                    tpm2 = spec.StartsWith("2.");
                    break;
                }
            }
            catch { }

            bool secureBoot = false;
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\SecureBoot\State"))
                    if (key != null)
                        secureBoot = (int)key.GetValue("UEFISecureBootEnabled", 0) == 1;
            }
            catch { }

            bool gpt = false;
            try
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskPartition WHERE DiskIndex=0");
                foreach (ManagementObject obj in searcher.Get())
                {
                    if ((obj["Type"]?.ToString() ?? "").StartsWith("GPT:")) { gpt = true; break; }
                }
            }
            catch { }

            bool ram4gb = false;
            try
            {
                var searcher = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
                foreach (ManagementObject obj in searcher.Get())
                {
                    ram4gb = (ulong)obj["TotalPhysicalMemory"] >= 4UL * 1024 * 1024 * 1024;
                    break;
                }
            }
            catch { }

            bool cpu64 = false;
            try
            {
                var searcher = new ManagementObjectSearcher("SELECT AddressWidth FROM Win32_Processor");
                foreach (ManagementObject obj in searcher.Get())
                {
                    cpu64 = (ushort)obj["AddressWidth"] == 64;
                    break;
                }
            }
            catch { }

            SetCheck(lblW11Uefi, uefi, "UEFI");
            SetCheck(lblW11Tpm, tpm2, "TPM 2.0");
            SetCheck(lblW11SecureBoot, secureBoot, "Secure Boot");
            SetCheck(lblW11Gpt, gpt, "Ổ đĩa GPT");
            SetCheck(lblW11Ram, ram4gb, "RAM ≥ 4 GB");
            SetCheck(lblW11Cpu, cpu64, "CPU 64-bit");

            bool ready = uefi && tpm2 && secureBoot && gpt && ram4gb && cpu64;
            if (alreadyWin11)
            {
                lblW11Result.Text = "✓  Máy đang chạy Windows 11";
                lblW11Result.ForeColor = Color.DarkGreen;
            }
            else if (ready)
            {
                lblW11Result.Text = "✓  Máy có thể nâng cấp Windows 11";
                lblW11Result.ForeColor = Color.DarkGreen;
            }
            else
            {
                lblW11Result.Text = "✗  Chưa đủ điều kiện nâng cấp Windows 11";
                lblW11Result.ForeColor = Color.DarkRed;
            }

            groupBox5.Visible = true;
            this.ClientSize = new System.Drawing.Size(480, 430);
        }

        private void SetCheck(Label lbl, bool ok, string name)
        {
            lbl.Text = (ok ? "✓ " : "✗ ") + name;
            lbl.ForeColor = ok ? Color.DarkGreen : Color.Red;
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
