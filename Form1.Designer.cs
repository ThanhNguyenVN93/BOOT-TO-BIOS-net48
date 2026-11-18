namespace frmbootobios
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblStatusText = new System.Windows.Forms.Label();
            this.btnStartCheck = new System.Windows.Forms.Button();
            this.btnCreateShortcut = new System.Windows.Forms.Button();
            this.btnCheckWin11 = new System.Windows.Forms.Button();
            this.lblW11Uefi = new System.Windows.Forms.Label();
            this.lblW11Tpm = new System.Windows.Forms.Label();
            this.lblW11SecureBoot = new System.Windows.Forms.Label();
            this.lblW11Gpt = new System.Windows.Forms.Label();
            this.lblW11Ram = new System.Windows.Forms.Label();
            this.lblW11Cpu = new System.Windows.Forms.Label();
            this.lblW11OsVersion = new System.Windows.Forms.Label();
            this.lblW11Result = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblHeader);
            this.groupBox1.Location = new System.Drawing.Point(3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 57);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            //
            // groupBox2
            //
            this.groupBox2.Controls.Add(this.btnCreateShortcut);
            this.groupBox2.Controls.Add(this.btnCheckWin11);
            this.groupBox2.Controls.Add(this.btnStartCheck);
            this.groupBox2.Location = new System.Drawing.Point(3, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 120);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblStatusText);
            this.groupBox4.Location = new System.Drawing.Point(3, 190);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(475, 55);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(47, 16);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(395, 24);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "BIOS MODE CHECKER AND SHORTCUT";
            // 
            // lblStatusText
            // 
            this.lblStatusText.AutoSize = true;
            this.lblStatusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblStatusText.Location = new System.Drawing.Point(66, 26);
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(151, 18);
            this.lblStatusText.TabIndex = 1;
            this.lblStatusText.Text = "Đang chờ kiểm tra...";
            //
            // btnStartCheck
            //
            this.btnStartCheck.Location = new System.Drawing.Point(8, 46);
            this.btnStartCheck.Name = "btnStartCheck";
            this.btnStartCheck.Size = new System.Drawing.Size(84, 44);
            this.btnStartCheck.TabIndex = 2;
            this.btnStartCheck.Text = "Kiểm tra BIOS";
            this.btnStartCheck.UseVisualStyleBackColor = true;
            this.btnStartCheck.Click += new System.EventHandler(this.btnStartCheck_Click);
            //
            // btnCheckWin11
            //
            this.btnCheckWin11.Location = new System.Drawing.Point(175, 46);
            this.btnCheckWin11.Name = "btnCheckWin11";
            this.btnCheckWin11.Size = new System.Drawing.Size(120, 44);
            this.btnCheckWin11.TabIndex = 4;
            this.btnCheckWin11.Text = "Kiểm tra Win 11";
            this.btnCheckWin11.UseVisualStyleBackColor = true;
            this.btnCheckWin11.Click += new System.EventHandler(this.btnCheckWin11_Click);
            //
            // btnCreateShortcut
            //
            this.btnCreateShortcut.Location = new System.Drawing.Point(380, 46);
            this.btnCreateShortcut.Name = "btnCreateShortcut";
            this.btnCreateShortcut.Size = new System.Drawing.Size(84, 44);
            this.btnCreateShortcut.TabIndex = 3;
            this.btnCreateShortcut.Text = "Tạo Shortcut";
            this.btnCreateShortcut.UseVisualStyleBackColor = true;
            this.btnCreateShortcut.Click += new System.EventHandler(this.btnCreateShortcut_Click);
            //
            // groupBox5
            //
            this.groupBox5.Controls.Add(this.lblW11Result);
            this.groupBox5.Controls.Add(this.lblW11Cpu);
            this.groupBox5.Controls.Add(this.lblW11Ram);
            this.groupBox5.Controls.Add(this.lblW11Gpt);
            this.groupBox5.Controls.Add(this.lblW11SecureBoot);
            this.groupBox5.Controls.Add(this.lblW11Tpm);
            this.groupBox5.Controls.Add(this.lblW11Uefi);
            this.groupBox5.Controls.Add(this.lblW11OsVersion);
            this.groupBox5.Location = new System.Drawing.Point(3, 251);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(475, 135);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Yêu cầu Windows 11";
            this.groupBox5.Visible = false;
            //
            // lblW11OsVersion
            //
            this.lblW11OsVersion.AutoSize = false;
            this.lblW11OsVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblW11OsVersion.Location = new System.Drawing.Point(5, 20);
            this.lblW11OsVersion.Name = "lblW11OsVersion";
            this.lblW11OsVersion.Size = new System.Drawing.Size(460, 18);
            this.lblW11OsVersion.TabIndex = 7;
            this.lblW11OsVersion.Text = "";
            //
            // lblW11Uefi
            //
            this.lblW11Uefi.AutoSize = true;
            this.lblW11Uefi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblW11Uefi.Location = new System.Drawing.Point(5, 44);
            this.lblW11Uefi.Name = "lblW11Uefi";
            this.lblW11Uefi.Size = new System.Drawing.Size(40, 15);
            this.lblW11Uefi.TabIndex = 0;
            this.lblW11Uefi.Text = "UEFI";
            //
            // lblW11Tpm
            //
            this.lblW11Tpm.AutoSize = true;
            this.lblW11Tpm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblW11Tpm.Location = new System.Drawing.Point(165, 44);
            this.lblW11Tpm.Name = "lblW11Tpm";
            this.lblW11Tpm.Size = new System.Drawing.Size(50, 15);
            this.lblW11Tpm.TabIndex = 1;
            this.lblW11Tpm.Text = "TPM 2.0";
            //
            // lblW11SecureBoot
            //
            this.lblW11SecureBoot.AutoSize = true;
            this.lblW11SecureBoot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblW11SecureBoot.Location = new System.Drawing.Point(325, 44);
            this.lblW11SecureBoot.Name = "lblW11SecureBoot";
            this.lblW11SecureBoot.Size = new System.Drawing.Size(80, 15);
            this.lblW11SecureBoot.TabIndex = 2;
            this.lblW11SecureBoot.Text = "Secure Boot";
            //
            // lblW11Gpt
            //
            this.lblW11Gpt.AutoSize = true;
            this.lblW11Gpt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblW11Gpt.Location = new System.Drawing.Point(5, 70);
            this.lblW11Gpt.Name = "lblW11Gpt";
            this.lblW11Gpt.Size = new System.Drawing.Size(55, 15);
            this.lblW11Gpt.TabIndex = 3;
            this.lblW11Gpt.Text = "GPT Disk";
            //
            // lblW11Ram
            //
            this.lblW11Ram.AutoSize = true;
            this.lblW11Ram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblW11Ram.Location = new System.Drawing.Point(165, 70);
            this.lblW11Ram.Name = "lblW11Ram";
            this.lblW11Ram.Size = new System.Drawing.Size(60, 15);
            this.lblW11Ram.TabIndex = 4;
            this.lblW11Ram.Text = "RAM ≥ 4GB";
            //
            // lblW11Cpu
            //
            this.lblW11Cpu.AutoSize = true;
            this.lblW11Cpu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblW11Cpu.Location = new System.Drawing.Point(325, 70);
            this.lblW11Cpu.Name = "lblW11Cpu";
            this.lblW11Cpu.Size = new System.Drawing.Size(65, 15);
            this.lblW11Cpu.TabIndex = 5;
            this.lblW11Cpu.Text = "CPU 64-bit";
            //
            // lblW11Result
            //
            this.lblW11Result.AutoSize = false;
            this.lblW11Result.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblW11Result.Location = new System.Drawing.Point(5, 100);
            this.lblW11Result.Name = "lblW11Result";
            this.lblW11Result.Size = new System.Drawing.Size(460, 22);
            this.lblW11Result.TabIndex = 6;
            this.lblW11Result.Text = "";
            this.lblW11Result.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 396);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnStartCheck;
        private System.Windows.Forms.Label lblStatusText;
        private System.Windows.Forms.Button btnCreateShortcut;
        private System.Windows.Forms.Button btnCheckWin11;
        private System.Windows.Forms.Label lblW11Uefi;
        private System.Windows.Forms.Label lblW11Tpm;
        private System.Windows.Forms.Label lblW11SecureBoot;
        private System.Windows.Forms.Label lblW11Gpt;
        private System.Windows.Forms.Label lblW11Ram;
        private System.Windows.Forms.Label lblW11Cpu;
        private System.Windows.Forms.Label lblW11Result;
        private System.Windows.Forms.Label lblW11OsVersion;
    }
}

