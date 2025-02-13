namespace ADB_Fastboot_GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            nightForm1 = new ReaLTaiizor.Forms.NightForm();
            materialTabControl1 = new ReaLTaiizor.Controls.MaterialShowTabControl();
            tabPage1 = new TabPage();
            APKList = new CheckedListBox();
            ADBLog = new ReaLTaiizor.Controls.CrownTextBox();
            crownGroupBox1 = new ReaLTaiizor.Controls.CrownGroupBox();
            SystemAPK = new ReaLTaiizor.Controls.CrownButton();
            SelectAPK = new ReaLTaiizor.Controls.CrownButton();
            Downgrade = new ReaLTaiizor.Controls.CrownCheckBox();
            ThirdAPK = new ReaLTaiizor.Controls.CrownButton();
            crownLabel1 = new ReaLTaiizor.Controls.CrownLabel();
            Uninstall = new ReaLTaiizor.Controls.CrownButton();
            Install = new ReaLTaiizor.Controls.CrownButton();
            APKBox = new ReaLTaiizor.Controls.CrownTextBox();
            ADBDevice = new ReaLTaiizor.Controls.CrownButton();
            DeviceList = new ReaLTaiizor.Controls.CrownComboBox();
            crownGroupBox2 = new ReaLTaiizor.Controls.CrownGroupBox();
            Shell = new ReaLTaiizor.Controls.CrownButton();
            Reboot = new ReaLTaiizor.Controls.CrownButton();
            crownLabel3 = new ReaLTaiizor.Controls.CrownLabel();
            ShellBox = new ReaLTaiizor.Controls.CrownTextBox();
            RunShell = new ReaLTaiizor.Controls.CrownButton();
            Sideload = new ReaLTaiizor.Controls.CrownButton();
            SelectFile = new ReaLTaiizor.Controls.CrownButton();
            crownLabel2 = new ReaLTaiizor.Controls.CrownLabel();
            FileBox = new ReaLTaiizor.Controls.CrownTextBox();
            tabPage2 = new TabPage();
            ABCheck = new ReaLTaiizor.Controls.CrownCheckBox();
            Firmware = new ReaLTaiizor.Controls.CrownButton();
            crownLabel4 = new ReaLTaiizor.Controls.CrownLabel();
            Lock = new ReaLTaiizor.Controls.CrownButton();
            Unlock = new ReaLTaiizor.Controls.CrownButton();
            FirmwareBox = new ReaLTaiizor.Controls.CrownTextBox();
            PartitionList = new CheckedListBox();
            crownTitle2 = new ReaLTaiizor.Controls.CrownTitle();
            FastbootReboot = new ReaLTaiizor.Controls.CrownButton();
            FastbootRun = new ReaLTaiizor.Controls.CrownButton();
            crownLabel6 = new ReaLTaiizor.Controls.CrownLabel();
            CommandBox = new ReaLTaiizor.Controls.CrownTextBox();
            Format = new ReaLTaiizor.Controls.CrownButton();
            FastbootList = new ReaLTaiizor.Controls.CrownComboBox();
            Flash = new ReaLTaiizor.Controls.CrownButton();
            FastbootLog = new ReaLTaiizor.Controls.CrownTextBox();
            FastbootDevice = new ReaLTaiizor.Controls.CrownButton();
            nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            ADBButton = new ReaLTaiizor.Controls.CrownButton();
            crownTitle1 = new ReaLTaiizor.Controls.CrownTitle();
            ADBBox = new ReaLTaiizor.Controls.CrownTextBox();
            Reinstall = new ReaLTaiizor.Controls.CrownCheckBox();
            SUCheck = new ReaLTaiizor.Controls.CrownCheckBox();
            nightForm1.SuspendLayout();
            materialTabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            crownGroupBox1.SuspendLayout();
            crownGroupBox2.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // nightForm1
            // 
            nightForm1.BackColor = Color.FromArgb(40, 48, 51);
            nightForm1.Controls.Add(materialTabControl1);
            nightForm1.Controls.Add(nightControlBox1);
            nightForm1.Controls.Add(ADBButton);
            nightForm1.Controls.Add(crownTitle1);
            nightForm1.Controls.Add(ADBBox);
            nightForm1.Dock = DockStyle.Fill;
            nightForm1.DrawIcon = false;
            nightForm1.Font = new Font("Segoe UI", 9F);
            nightForm1.HeadColor = Color.FromArgb(50, 58, 61);
            nightForm1.Location = new Point(0, 0);
            nightForm1.MinimumSize = new Size(100, 42);
            nightForm1.Name = "nightForm1";
            nightForm1.Padding = new Padding(0, 31, 0, 0);
            nightForm1.Size = new Size(800, 450);
            nightForm1.TabIndex = 0;
            nightForm1.Text = "ADB Fastboot GUI";
            nightForm1.TextAlignment = ReaLTaiizor.Forms.NightForm.Alignment.Left;
            nightForm1.TitleBarTextColor = Color.Gainsboro;
            nightForm1.Click += nightForm1_Click;
            // 
            // materialTabControl1
            // 
            materialTabControl1.Controls.Add(tabPage1);
            materialTabControl1.Controls.Add(tabPage2);
            materialTabControl1.Depth = 0;
            materialTabControl1.Location = new Point(4, 68);
            materialTabControl1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialTabControl1.Multiline = true;
            materialTabControl1.Name = "materialTabControl1";
            materialTabControl1.SelectedIndex = 0;
            materialTabControl1.Size = new Size(784, 370);
            materialTabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(APKList);
            tabPage1.Controls.Add(ADBLog);
            tabPage1.Controls.Add(crownGroupBox1);
            tabPage1.Controls.Add(ADBDevice);
            tabPage1.Controls.Add(DeviceList);
            tabPage1.Controls.Add(crownGroupBox2);
            tabPage1.Location = new Point(4, 25);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(776, 341);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "ADB";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // APKList
            // 
            APKList.FormattingEnabled = true;
            APKList.Location = new Point(422, 2);
            APKList.Name = "APKList";
            APKList.Size = new Size(348, 202);
            APKList.TabIndex = 9;
            // 
            // ADBLog
            // 
            ADBLog.BackColor = Color.FromArgb(69, 73, 74);
            ADBLog.BorderStyle = BorderStyle.FixedSingle;
            ADBLog.ForeColor = Color.FromArgb(220, 220, 220);
            ADBLog.Location = new Point(422, 213);
            ADBLog.Multiline = true;
            ADBLog.Name = "ADBLog";
            ADBLog.ReadOnly = true;
            ADBLog.Size = new Size(348, 123);
            ADBLog.TabIndex = 1;
            ADBLog.TextChanged += ADBLog_TextChanged;
            // 
            // crownGroupBox1
            // 
            crownGroupBox1.BackColor = SystemColors.Control;
            crownGroupBox1.BorderColor = Color.FromArgb(64, 64, 64);
            crownGroupBox1.Controls.Add(Reinstall);
            crownGroupBox1.Controls.Add(SystemAPK);
            crownGroupBox1.Controls.Add(SelectAPK);
            crownGroupBox1.Controls.Add(Downgrade);
            crownGroupBox1.Controls.Add(ThirdAPK);
            crownGroupBox1.Controls.Add(crownLabel1);
            crownGroupBox1.Controls.Add(Uninstall);
            crownGroupBox1.Controls.Add(Install);
            crownGroupBox1.Controls.Add(APKBox);
            crownGroupBox1.FlatStyle = FlatStyle.System;
            crownGroupBox1.ForeColor = SystemColors.ButtonHighlight;
            crownGroupBox1.Location = new Point(6, 27);
            crownGroupBox1.Name = "crownGroupBox1";
            crownGroupBox1.Size = new Size(402, 136);
            crownGroupBox1.TabIndex = 7;
            crownGroupBox1.TabStop = false;
            crownGroupBox1.Text = "Android Apps";
            crownGroupBox1.Enter += crownGroupBox1_Enter;
            // 
            // SystemAPK
            // 
            SystemAPK.Location = new Point(6, 22);
            SystemAPK.Name = "SystemAPK";
            SystemAPK.Padding = new Padding(5);
            SystemAPK.Size = new Size(123, 23);
            SystemAPK.TabIndex = 5;
            SystemAPK.Text = "Show System Apps";
            SystemAPK.Click += SystemAPK_Click;
            // 
            // SelectAPK
            // 
            SelectAPK.Location = new Point(178, 65);
            SelectAPK.Name = "SelectAPK";
            SelectAPK.Padding = new Padding(5);
            SelectAPK.Size = new Size(75, 23);
            SelectAPK.TabIndex = 10;
            SelectAPK.Text = "Select APK";
            SelectAPK.Click += SelectAPK_Click;
            // 
            // Downgrade
            // 
            Downgrade.AutoSize = true;
            Downgrade.Location = new Point(6, 94);
            Downgrade.Name = "Downgrade";
            Downgrade.Size = new Size(87, 19);
            Downgrade.TabIndex = 13;
            Downgrade.Text = "Downgrade";
            // 
            // ThirdAPK
            // 
            ThirdAPK.Location = new Point(146, 22);
            ThirdAPK.Name = "ThirdAPK";
            ThirdAPK.Padding = new Padding(5);
            ThirdAPK.Size = new Size(107, 23);
            ThirdAPK.TabIndex = 6;
            ThirdAPK.Text = "Show User Apps";
            ThirdAPK.Click += ThirdAPK_Click;
            // 
            // crownLabel1
            // 
            crownLabel1.AutoSize = true;
            crownLabel1.ForeColor = Color.White;
            crownLabel1.Location = new Point(6, 48);
            crownLabel1.Name = "crownLabel1";
            crownLabel1.Size = new Size(67, 15);
            crownLabel1.TabIndex = 12;
            crownLabel1.Text = "APK Locate";
            // 
            // Uninstall
            // 
            Uninstall.Location = new Point(271, 22);
            Uninstall.Name = "Uninstall";
            Uninstall.Padding = new Padding(5);
            Uninstall.Size = new Size(112, 23);
            Uninstall.TabIndex = 7;
            Uninstall.Text = "Uninstall Selected";
            Uninstall.Click += Uninstall_Click;
            // 
            // Install
            // 
            Install.Location = new Point(271, 65);
            Install.Name = "Install";
            Install.Padding = new Padding(5);
            Install.Size = new Size(75, 23);
            Install.TabIndex = 9;
            Install.Text = "Install APK";
            Install.Click += Install_Click;
            // 
            // APKBox
            // 
            APKBox.BackColor = Color.FromArgb(69, 73, 74);
            APKBox.BorderStyle = BorderStyle.FixedSingle;
            APKBox.ForeColor = Color.FromArgb(220, 220, 220);
            APKBox.Location = new Point(6, 65);
            APKBox.Name = "APKBox";
            APKBox.ReadOnly = true;
            APKBox.Size = new Size(156, 23);
            APKBox.TabIndex = 8;
            // 
            // ADBDevice
            // 
            ADBDevice.Location = new Point(12, 3);
            ADBDevice.Name = "ADBDevice";
            ADBDevice.Padding = new Padding(5);
            ADBDevice.Size = new Size(97, 23);
            ADBDevice.TabIndex = 2;
            ADBDevice.Text = "Refresh Device";
            ADBDevice.Click += ADBDevice_Click;
            // 
            // DeviceList
            // 
            DeviceList.DrawMode = DrawMode.OwnerDrawVariable;
            DeviceList.FormattingEnabled = true;
            DeviceList.Location = new Point(126, 2);
            DeviceList.Name = "DeviceList";
            DeviceList.Size = new Size(121, 24);
            DeviceList.TabIndex = 0;
            DeviceList.SelectedIndexChanged += DeviceList_SelectedIndexChanged;
            // 
            // crownGroupBox2
            // 
            crownGroupBox2.BorderColor = Color.FromArgb(51, 51, 51);
            crownGroupBox2.Controls.Add(SUCheck);
            crownGroupBox2.Controls.Add(Shell);
            crownGroupBox2.Controls.Add(Reboot);
            crownGroupBox2.Controls.Add(crownLabel3);
            crownGroupBox2.Controls.Add(ShellBox);
            crownGroupBox2.Controls.Add(RunShell);
            crownGroupBox2.Controls.Add(Sideload);
            crownGroupBox2.Controls.Add(SelectFile);
            crownGroupBox2.Controls.Add(crownLabel2);
            crownGroupBox2.Controls.Add(FileBox);
            crownGroupBox2.FlatStyle = FlatStyle.System;
            crownGroupBox2.Location = new Point(6, 169);
            crownGroupBox2.Name = "crownGroupBox2";
            crownGroupBox2.Size = new Size(402, 170);
            crownGroupBox2.TabIndex = 8;
            crownGroupBox2.TabStop = false;
            crownGroupBox2.Text = "ETC";
            // 
            // Shell
            // 
            Shell.Location = new Point(149, 140);
            Shell.Name = "Shell";
            Shell.Padding = new Padding(5);
            Shell.Size = new Size(92, 23);
            Shell.TabIndex = 17;
            Shell.Text = "Shell";
            Shell.Click += Shell_Click;
            // 
            // Reboot
            // 
            Reboot.Location = new Point(6, 140);
            Reboot.Name = "Reboot";
            Reboot.Padding = new Padding(5);
            Reboot.Size = new Size(92, 23);
            Reboot.TabIndex = 16;
            Reboot.Text = "Reboot";
            Reboot.Click += Reboot_Click;
            // 
            // crownLabel3
            // 
            crownLabel3.AutoSize = true;
            crownLabel3.ForeColor = Color.White;
            crownLabel3.Location = new Point(6, 73);
            crownLabel3.Name = "crownLabel3";
            crownLabel3.Size = new Size(90, 15);
            crownLabel3.TabIndex = 15;
            crownLabel3.Text = "ADB Command";
            // 
            // ShellBox
            // 
            ShellBox.BackColor = Color.FromArgb(69, 73, 74);
            ShellBox.BorderStyle = BorderStyle.FixedSingle;
            ShellBox.ForeColor = Color.FromArgb(220, 220, 220);
            ShellBox.Location = new Point(6, 91);
            ShellBox.Name = "ShellBox";
            ShellBox.Size = new Size(247, 23);
            ShellBox.TabIndex = 14;
            ShellBox.TextChanged += ShellBox_TextChanged;
            // 
            // RunShell
            // 
            RunShell.Location = new Point(271, 91);
            RunShell.Name = "RunShell";
            RunShell.Padding = new Padding(5);
            RunShell.Size = new Size(75, 23);
            RunShell.TabIndex = 13;
            RunShell.Text = "Run";
            RunShell.Click += RunShell_Click;
            // 
            // Sideload
            // 
            Sideload.Location = new Point(271, 38);
            Sideload.Name = "Sideload";
            Sideload.Padding = new Padding(5);
            Sideload.Size = new Size(75, 23);
            Sideload.TabIndex = 12;
            Sideload.Text = "Sideload";
            Sideload.Click += Sideload_Click;
            // 
            // SelectFile
            // 
            SelectFile.Location = new Point(178, 38);
            SelectFile.Name = "SelectFile";
            SelectFile.Padding = new Padding(5);
            SelectFile.Size = new Size(75, 23);
            SelectFile.TabIndex = 11;
            SelectFile.Text = "Select File";
            SelectFile.Click += SelectFile_Click;
            // 
            // crownLabel2
            // 
            crownLabel2.AutoSize = true;
            crownLabel2.ForeColor = Color.White;
            crownLabel2.Location = new Point(6, 19);
            crownLabel2.Name = "crownLabel2";
            crownLabel2.Size = new Size(111, 15);
            crownLabel2.TabIndex = 1;
            crownLabel2.Text = "Sideload File Locate";
            // 
            // FileBox
            // 
            FileBox.BackColor = Color.FromArgb(69, 73, 74);
            FileBox.BorderStyle = BorderStyle.FixedSingle;
            FileBox.ForeColor = Color.FromArgb(220, 220, 220);
            FileBox.Location = new Point(6, 38);
            FileBox.Name = "FileBox";
            FileBox.Size = new Size(156, 23);
            FileBox.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(ABCheck);
            tabPage2.Controls.Add(Firmware);
            tabPage2.Controls.Add(crownLabel4);
            tabPage2.Controls.Add(Lock);
            tabPage2.Controls.Add(Unlock);
            tabPage2.Controls.Add(FirmwareBox);
            tabPage2.Controls.Add(PartitionList);
            tabPage2.Controls.Add(crownTitle2);
            tabPage2.Controls.Add(FastbootReboot);
            tabPage2.Controls.Add(FastbootRun);
            tabPage2.Controls.Add(crownLabel6);
            tabPage2.Controls.Add(CommandBox);
            tabPage2.Controls.Add(Format);
            tabPage2.Controls.Add(FastbootList);
            tabPage2.Controls.Add(Flash);
            tabPage2.Controls.Add(FastbootLog);
            tabPage2.Controls.Add(FastbootDevice);
            tabPage2.Location = new Point(4, 25);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(776, 341);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Fastboot";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // ABCheck
            // 
            ABCheck.AutoSize = true;
            ABCheck.Location = new Point(114, 121);
            ABCheck.Name = "ABCheck";
            ABCheck.Size = new Size(46, 19);
            ABCheck.TabIndex = 25;
            ABCheck.Text = "A/B";
            // 
            // Firmware
            // 
            Firmware.Location = new Point(200, 74);
            Firmware.Name = "Firmware";
            Firmware.Padding = new Padding(5);
            Firmware.Size = new Size(84, 23);
            Firmware.TabIndex = 24;
            Firmware.Text = "Folder";
            Firmware.Click += Firmware_Click;
            // 
            // crownLabel4
            // 
            crownLabel4.AutoSize = true;
            crownLabel4.ForeColor = Color.White;
            crownLabel4.Location = new Point(7, 56);
            crownLabel4.Name = "crownLabel4";
            crownLabel4.Size = new Size(94, 15);
            crownLabel4.TabIndex = 23;
            crownLabel4.Text = "Firmware Locate";
            // 
            // Lock
            // 
            Lock.Location = new Point(234, 164);
            Lock.Name = "Lock";
            Lock.Padding = new Padding(5);
            Lock.Size = new Size(84, 23);
            Lock.TabIndex = 22;
            Lock.Text = "Lock";
            Lock.Click += Lock_Click;
            // 
            // Unlock
            // 
            Unlock.Location = new Point(131, 164);
            Unlock.Name = "Unlock";
            Unlock.Padding = new Padding(5);
            Unlock.Size = new Size(79, 23);
            Unlock.TabIndex = 21;
            Unlock.Text = "Unlock";
            Unlock.Click += Unlock_Click;
            // 
            // FirmwareBox
            // 
            FirmwareBox.BackColor = Color.FromArgb(69, 73, 74);
            FirmwareBox.BorderStyle = BorderStyle.FixedSingle;
            FirmwareBox.ForeColor = Color.FromArgb(220, 220, 220);
            FirmwareBox.Location = new Point(6, 74);
            FirmwareBox.Name = "FirmwareBox";
            FirmwareBox.Size = new Size(172, 23);
            FirmwareBox.TabIndex = 20;
            // 
            // PartitionList
            // 
            PartitionList.FormattingEnabled = true;
            PartitionList.Location = new Point(435, 6);
            PartitionList.Name = "PartitionList";
            PartitionList.Size = new Size(335, 148);
            PartitionList.TabIndex = 19;
            // 
            // crownTitle2
            // 
            crownTitle2.AutoSize = true;
            crownTitle2.Location = new Point(114, 305);
            crownTitle2.Name = "crownTitle2";
            crownTitle2.Size = new Size(314, 15);
            crownTitle2.TabIndex = 18;
            crownTitle2.Text = "Caution: Don't Run \"getvar all\". It will make fastboot break";
            // 
            // FastbootReboot
            // 
            FastbootReboot.Location = new Point(6, 297);
            FastbootReboot.Name = "FastbootReboot";
            FastbootReboot.Padding = new Padding(5);
            FastbootReboot.Size = new Size(75, 23);
            FastbootReboot.TabIndex = 17;
            FastbootReboot.Text = "Reboot";
            FastbootReboot.Click += FastbootReboot_Click;
            // 
            // FastbootRun
            // 
            FastbootRun.Location = new Point(266, 229);
            FastbootRun.Name = "FastbootRun";
            FastbootRun.Padding = new Padding(5);
            FastbootRun.Size = new Size(75, 23);
            FastbootRun.TabIndex = 16;
            FastbootRun.Text = "Run";
            FastbootRun.Click += FastbootRun_Click;
            // 
            // crownLabel6
            // 
            crownLabel6.AutoSize = true;
            crownLabel6.ForeColor = Color.White;
            crownLabel6.Location = new Point(7, 211);
            crownLabel6.Name = "crownLabel6";
            crownLabel6.Size = new Size(64, 15);
            crownLabel6.TabIndex = 15;
            crownLabel6.Text = "Command";
            // 
            // CommandBox
            // 
            CommandBox.BackColor = Color.FromArgb(69, 73, 74);
            CommandBox.BorderStyle = BorderStyle.FixedSingle;
            CommandBox.ForeColor = Color.FromArgb(220, 220, 220);
            CommandBox.Location = new Point(6, 229);
            CommandBox.Name = "CommandBox";
            CommandBox.Size = new Size(204, 23);
            CommandBox.TabIndex = 14;
            // 
            // Format
            // 
            Format.Location = new Point(6, 164);
            Format.Name = "Format";
            Format.Padding = new Padding(5);
            Format.Size = new Size(107, 23);
            Format.TabIndex = 13;
            Format.Text = "Format Data";
            Format.Click += Format_Click;
            // 
            // FastbootList
            // 
            FastbootList.DrawMode = DrawMode.OwnerDrawVariable;
            FastbootList.FormattingEnabled = true;
            FastbootList.Location = new Point(131, 9);
            FastbootList.Name = "FastbootList";
            FastbootList.Size = new Size(121, 24);
            FastbootList.TabIndex = 12;
            // 
            // Flash
            // 
            Flash.Location = new Point(6, 118);
            Flash.Name = "Flash";
            Flash.Padding = new Padding(5);
            Flash.Size = new Size(75, 23);
            Flash.TabIndex = 3;
            Flash.Text = "Flash";
            Flash.Click += Flash_Click;
            // 
            // FastbootLog
            // 
            FastbootLog.BackColor = Color.FromArgb(69, 73, 74);
            FastbootLog.BorderStyle = BorderStyle.FixedSingle;
            FastbootLog.ForeColor = Color.FromArgb(220, 220, 220);
            FastbootLog.Location = new Point(435, 164);
            FastbootLog.MaxLength = 1111111111;
            FastbootLog.Multiline = true;
            FastbootLog.Name = "FastbootLog";
            FastbootLog.ReadOnly = true;
            FastbootLog.Size = new Size(335, 172);
            FastbootLog.TabIndex = 2;
            FastbootLog.TextChanged += FastbootLog_TextChanged;
            // 
            // FastbootDevice
            // 
            FastbootDevice.Location = new Point(6, 9);
            FastbootDevice.Name = "FastbootDevice";
            FastbootDevice.Padding = new Padding(5);
            FastbootDevice.Size = new Size(97, 26);
            FastbootDevice.TabIndex = 0;
            FastbootDevice.Text = "Refresh Device";
            FastbootDevice.Click += FastbootDevice_Click;
            // 
            // nightControlBox1
            // 
            nightControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nightControlBox1.BackColor = Color.Transparent;
            nightControlBox1.CloseHoverColor = Color.FromArgb(199, 80, 80);
            nightControlBox1.CloseHoverForeColor = Color.White;
            nightControlBox1.DefaultLocation = true;
            nightControlBox1.DisableMaximizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.DisableMinimizeColor = Color.FromArgb(105, 105, 105);
            nightControlBox1.EnableCloseColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMaximizeButton = false;
            nightControlBox1.EnableMaximizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.EnableMinimizeButton = true;
            nightControlBox1.EnableMinimizeColor = Color.FromArgb(160, 160, 160);
            nightControlBox1.Location = new Point(661, 0);
            nightControlBox1.MaximizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MaximizeHoverForeColor = Color.White;
            nightControlBox1.MinimizeHoverColor = Color.FromArgb(15, 255, 255, 255);
            nightControlBox1.MinimizeHoverForeColor = Color.White;
            nightControlBox1.Name = "nightControlBox1";
            nightControlBox1.Size = new Size(139, 31);
            nightControlBox1.TabIndex = 4;
            // 
            // ADBButton
            // 
            ADBButton.Location = new Point(331, 42);
            ADBButton.Name = "ADBButton";
            ADBButton.Padding = new Padding(5);
            ADBButton.Size = new Size(75, 23);
            ADBButton.TabIndex = 3;
            ADBButton.Text = "Select";
            ADBButton.Click += ADBButton_Click;
            // 
            // crownTitle1
            // 
            crownTitle1.AutoSize = true;
            crownTitle1.Location = new Point(4, 46);
            crownTitle1.Name = "crownTitle1";
            crownTitle1.Size = new Size(117, 15);
            crownTitle1.TabIndex = 2;
            crownTitle1.Text = "ADB Fastboot Locate";
            // 
            // ADBBox
            // 
            ADBBox.BackColor = Color.FromArgb(69, 73, 74);
            ADBBox.BorderStyle = BorderStyle.FixedSingle;
            ADBBox.ForeColor = Color.FromArgb(220, 220, 220);
            ADBBox.Location = new Point(127, 42);
            ADBBox.Name = "ADBBox";
            ADBBox.ReadOnly = true;
            ADBBox.Size = new Size(188, 23);
            ADBBox.TabIndex = 1;
            // 
            // Reinstall
            // 
            Reinstall.AutoSize = true;
            Reinstall.BackColor = Color.Transparent;
            Reinstall.Location = new Point(120, 94);
            Reinstall.Name = "Reinstall";
            Reinstall.Size = new Size(70, 19);
            Reinstall.TabIndex = 14;
            Reinstall.Text = "Reinstall";
            // 
            // SUCheck
            // 
            SUCheck.AutoSize = true;
            SUCheck.BackColor = Color.Transparent;
            SUCheck.Location = new Point(259, 143);
            SUCheck.Name = "SUCheck";
            SUCheck.Size = new Size(78, 19);
            SUCheck.TabIndex = 18;
            SUCheck.Text = "Superuser";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(nightForm1);
            FormBorderStyle = FormBorderStyle.None;
            MaximumSize = new Size(1920, 1032);
            MinimumSize = new Size(261, 65);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "dungeonForm1";
            TransparencyKey = Color.Fuchsia;
            nightForm1.ResumeLayout(false);
            nightForm1.PerformLayout();
            materialTabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            crownGroupBox1.ResumeLayout(false);
            crownGroupBox1.PerformLayout();
            crownGroupBox2.ResumeLayout(false);
            crownGroupBox2.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Forms.NightForm nightForm1;
        private ReaLTaiizor.Controls.CrownButton ADBButton;
        private ReaLTaiizor.Controls.CrownTitle crownTitle1;
        private ReaLTaiizor.Controls.CrownTextBox ADBBox;
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
        private ReaLTaiizor.Controls.MaterialShowTabControl materialTabControl1;
        private TabPage tabPage1;
        private ReaLTaiizor.Controls.CrownTextBox ADBLog;
        private ReaLTaiizor.Controls.CrownGroupBox crownGroupBox1;
        private ReaLTaiizor.Controls.CrownButton SystemAPK;
        private ReaLTaiizor.Controls.CrownButton SelectAPK;
        private ReaLTaiizor.Controls.CrownCheckBox Downgrade;
        private ReaLTaiizor.Controls.CrownButton ThirdAPK;
        private ReaLTaiizor.Controls.CrownLabel crownLabel1;
        private ReaLTaiizor.Controls.CrownButton Uninstall;
        private ReaLTaiizor.Controls.CrownButton Install;
        private ReaLTaiizor.Controls.CrownTextBox APKBox;
        private ReaLTaiizor.Controls.CrownButton ADBDevice;
        private ReaLTaiizor.Controls.CrownComboBox DeviceList;
        private ReaLTaiizor.Controls.CrownGroupBox crownGroupBox2;
        private ReaLTaiizor.Controls.CrownButton Reboot;
        private ReaLTaiizor.Controls.CrownLabel crownLabel3;
        private ReaLTaiizor.Controls.CrownTextBox ShellBox;
        private ReaLTaiizor.Controls.CrownButton RunShell;
        private ReaLTaiizor.Controls.CrownButton Sideload;
        private ReaLTaiizor.Controls.CrownButton SelectFile;
        private ReaLTaiizor.Controls.CrownLabel crownLabel2;
        private ReaLTaiizor.Controls.CrownTextBox FileBox;
        private TabPage tabPage2;
        private ReaLTaiizor.Controls.CrownButton FastbootDevice;
        private ReaLTaiizor.Controls.CrownTextBox FastbootLog;
        private ReaLTaiizor.Controls.CrownButton Flash;
        private ReaLTaiizor.Controls.CrownComboBox FastbootList;
        private ReaLTaiizor.Controls.CrownButton Format;
        private ReaLTaiizor.Controls.CrownButton FastbootRun;
        private ReaLTaiizor.Controls.CrownLabel crownLabel6;
        private ReaLTaiizor.Controls.CrownTextBox CommandBox;
        private ReaLTaiizor.Controls.CrownButton FastbootReboot;
        private ReaLTaiizor.Controls.CrownTitle crownTitle2;
        private CheckedListBox APKList;
        private CheckedListBox PartitionList;
        private ReaLTaiizor.Controls.CrownTextBox FirmwareBox;
        private ReaLTaiizor.Controls.CrownButton Unlock;
        private ReaLTaiizor.Controls.CrownButton Lock;
        private ReaLTaiizor.Controls.CrownButton Firmware;
        private ReaLTaiizor.Controls.CrownLabel crownLabel4;
        private ReaLTaiizor.Controls.CrownCheckBox ABCheck;
        private ReaLTaiizor.Controls.CrownButton Shell;
        private ReaLTaiizor.Controls.CrownCheckBox Reinstall;
        private ReaLTaiizor.Controls.CrownCheckBox SUCheck;
    }
}
