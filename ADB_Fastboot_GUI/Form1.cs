using ReaLTaiizor.Controls;
using System.Configuration;
using Timer = System.Windows.Forms.Timer;
using System.Linq;
using System.Diagnostics;

namespace ADB_Fastboot_GUI
{
    public partial class Form1 : Form
    {
        private Timer clearLogTimer;

        private readonly AdbCommandRunner adbCommandRunner;
        private readonly FastbootCommandRunner fastbootCommandRunner;

        // Add the missing AdbPath field
        private string AdbPath;

        // Add the missing FastbootPath field
        private string FastbootPath;

        public Form1()
        {
            InitializeComponent();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            if (!Console.IsOutputRedirected)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
            }

            this.MaximizeBox = false;
            this.AutoScaleMode = AutoScaleMode.None; //Disable Form scaling

            // Initialize the timer
            clearLogTimer = new Timer();
            clearLogTimer.Interval = 180000; // 3 minutes
            clearLogTimer.Tick += ClearLogTimer_Tick;

            //Load last ADB path
            LoadLastUseddAdbPath();

            // Ensure AdbPath is initialized
            if (string.IsNullOrEmpty(AdbPath))
            {
                AdbPath = System.IO.Path.Combine(ADBBox.Text, "adb.exe");
            }

            // Ensure FastbootPath is initialized
            if (string.IsNullOrEmpty(FastbootPath))
            {
                FastbootPath = System.IO.Path.Combine(ADBBox.Text, "fastboot.exe");
            }

            // Fix the argument types for AdbCommandRunner
            adbCommandRunner = new AdbCommandRunner(AdbPath, clearLogTimer, DeviceList as CrownComboBox, APKList as CheckedListBox, ADBLog as CrownTextBox);
            fastbootCommandRunner = new FastbootCommandRunner(FastbootPath, DeviceList as CrownComboBox, FastbootLog as CrownTextBox);
        }

        private void LoadLastUseddAdbPath()
        {
            string? lastUsedAdbPath = ConfigurationManager.AppSettings["LastUsedAdbPath"];
            if (!string.IsNullOrEmpty(lastUsedAdbPath))
            {
                ADBBox.Text = lastUsedAdbPath;
                AdbPath = System.IO.Path.Combine(ADBBox.Text, "adb.exe");
            }
        }

        private void SaveLastUsedAdbPath(string path)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings["LastUsedAdbPath"] == null)
            {
                config.AppSettings.Settings.Add("LastUsedAdbPath", ADBBox.Text);
            }
            else
            {
                config.AppSettings.Settings["LastUsedAdbPath"].Value = ADBBox.Text;
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void MaterialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (materialTabControl1.SelectedIndex == 0)
            {
                // Handle tab 0 selected
            }
            else if (materialTabControl1.SelectedIndex == 1)
            {
                // Handle tab 1 selected
            }
        }

        private void nightForm1_Click(object sender, EventArgs e)
        {

        }

        //Open ADB Folder
        private void ADBButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    ADBBox.Text = fbd.SelectedPath;
                    AdbPath = System.IO.Path.Combine(ADBBox.Text, "adb.exe");
                    FastbootPath = System.IO.Path.Combine(ADBBox.Text, "fastboot.exe");
                    SaveLastUsedAdbPath(ADBBox.Text);
                }
            }
        }

        //Quit App
        private void Quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void ShowADBOutput_Click(object sender, EventArgs e)
        {
            RunAdbCommand("devices");
        }

        //Refrest Device List
        private void ADBDevice_Click(object sender, EventArgs e)
        {
            RunAdbCommand("devices", checkForSideload: true);
        }

        //Show System Apps
        private void SystemAPK_Click(object sender, EventArgs e)
        {
            if (DeviceList.SelectedItem != null)
            {
                string selectedDevice = DeviceList.SelectedItem?.ToString() ?? string.Empty;
                string arguments = $"-s {selectedDevice} shell pm list packages -s";
                RunAdbCommand(arguments, true);
            }
            else
            {
                MessageBox.Show("No device selected.", "Error");
            }
        }

        //Show User Apps
        private void ThirdAPK_Click(object sender, EventArgs e)
        {
            if (DeviceList.SelectedItem != null)
            {
                string selectedDevice = DeviceList.SelectedItem?.ToString() ?? string.Empty;
                string arguments = $"-s {selectedDevice} shell pm list packages -3";
                RunAdbCommand(arguments, true, "package:");
            }
            else
            {
                MessageBox.Show("No device selected.", "Error");
            }
        }


        //Uninstall Selected Apps in App List
        private void Uninstall_Click(object sender, EventArgs e)
        {
            if (DeviceList.SelectedItem != null && APKList.Items.Count > 0)
            {
                string selectedDevice = DeviceList.SelectedItem?.ToString() ?? string.Empty;
                var selectedPackages = APKList.CheckedItems
                    .Cast<string>()
                    .ToList();

                var confirmResult = MessageBox.Show(
                    $"Are you sure you want to uninstall the selected packages from {selectedDevice}?",
                    "Confirm Uninstall",
                    MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    foreach (var package in selectedPackages)
                    {
                        string arguments = $"-s {selectedDevice} shell pm uninstall -k --user 0 {package}";
                        RunAdbCommand(arguments);
                    }
                }
            }
            else
            {
                MessageBox.Show("No device or package selected.", "Error");
            }
        }

        //Select APK Files in Folder
        private void SelectAPK_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "APK files (*.apk)|*.apk";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    APKBox.Text = ofd.FileName;
                }
            }
        }

        //Install APK Files Selected
        private void Install_Click(object sender, EventArgs e)
        {
            if (DeviceList.SelectedItem != null && !string.IsNullOrEmpty(APKBox.Text))
            {
                string selectedDevice = DeviceList.SelectedItem?.ToString() ?? string.Empty;
                string apkPath = APKBox.Text;
                string arguments = $"-s {selectedDevice} install";

                if (Downgrade.Checked)
                {
                    arguments += " -d";
                }

                if (Reinstall.Checked)
                {
                    arguments += " -r";
                }

                arguments += $" {apkPath}";
                RunAdbCommand(arguments);
            }
            else
            {
                MessageBox.Show("No device or APK file selected.", "Error");
            }
        }

        //Run ADB Form File Located
        private void RunAdbCommand(string arguments, bool updateAPKList = false, string? filter = null, bool checkForSideload = true)
        {
            adbCommandRunner.RunAdbCommand(arguments, updateAPKList: true);
        }


        private void ClearLogTimer_Tick(object? sender, EventArgs e)
        {
            // Stop the timer and clear the log
            clearLogTimer.Stop();
            ADBLog.Clear();
        }

        private void ADBLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void DeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Select File for Sideload
        private void SelectFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileBox.Text = ofd.FileName;
                }
            }
        }

        //Sideload File Selected
        private void Sideload_Click(object sender, EventArgs e)
        {
            if (DeviceList.SelectedItem != null && !string.IsNullOrEmpty(FileBox.Text))
            {
                string selectedDevice = DeviceList.SelectedItem?.ToString() ?? string.Empty;
                string filePath = FileBox.Text;
                string arguments = $"-s {selectedDevice} sideload {filePath}";
                RunAdbCommand(arguments);
            }
            else
            {
                MessageBox.Show("No device or file selected.", "Error");
            }
        }

        //Run Command Form ShellBox
        private void RunShell_Click(object sender, EventArgs e)
        {

        }

        //Reboot Devices
        private void Reboot_Click(object sender, EventArgs e)
        {
            if (DeviceList.SelectedItem != null)
            {
                var confirmResult = MessageBox.Show(
                    "Are you sure you want to reboot the selected device?",
                    "Confirm Reboot",
                    MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    var rebootModeResult = MessageBox.Show(
                        "Select Reboot Mode:\nYes - Recovery\nNo - Bootloader\nCancel - Normal",
                        "Reboot Mode",
                        MessageBoxButtons.YesNoCancel);

                    string selectedDevice = DeviceList.SelectedItem?.ToString() ?? string.Empty;
                    string arguments = $"-s {selectedDevice} reboot";

                    if (rebootModeResult == DialogResult.Yes)
                    {
                        arguments += " recovery";
                    }
                    else if (rebootModeResult == DialogResult.No)
                    {
                        arguments += " bootloader";
                    }

                    RunAdbCommand(arguments);
                }
            }
            else
            {
                MessageBox.Show("No device selected.", "Error");
            }
        }

        //Press Enter for run Shell Command
        private void ShellBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                RunShell_Click(sender, e);
            }
        }

        private void ShellBox_TextChanged(object sender, EventArgs e)
        {

        }

        // Add this method to handle the FastbootDevice click event
        private void FastbootDevice_Click(object sender, EventArgs e)
        {
            string output = fastbootCommandRunner.RunFastbootCommand("devices");

            // Ki?m tra n?u output th?c s? có d? li?u
            if (string.IsNullOrWhiteSpace(output))
            {
                MessageBox.Show("?? Không tìm th?y thi?t b? ? ch? ?? Fastboot.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FastbootList.Items.Clear();

            // X? lý output t? fastboot devices
            string[] lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            bool deviceFound = false;

            foreach (string line in lines)
            {
                // Ki?m tra xem dòng có ch?a "fastboot" hay không
                if (line.Contains("fastboot"))
                {
                    string[] parts = line.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 1 && parts[1] == "fastboot")
                    {
                        FastbootList.Items.Add(parts[0]); // Thêm serial number vào danh sách
                        deviceFound = true;
                    }
                }
            }

            if (!deviceFound)
            {
                MessageBox.Show("?? Cannot Find Device in Fastboot Mode.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                FastbootList.SelectedIndex = 0; // Ch?n thi?t b? ??u tiên m?c ??nh
            }
        }


        // Add this method to handle the Flash click event
        private async void Flash_Click(object sender, EventArgs e)
        {
            if (FastbootList.SelectedItem == null || PartitionList.SelectedItems.Count == 0)
            {
                MessageBox.Show("? Không có thi?t b? ho?c phân vùng nào ???c ch?n.", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedDevice = FastbootList.SelectedItem?.ToString() ?? string.Empty;
            string fastbootPath = System.IO.Path.Combine(ADBBox.Text, "fastboot.exe");
            bool isABChecked = ABCheck.Checked;
            string firmwareDir = FirmwareBox.Text;

            List<string> flashedFiles = new List<string>();

            foreach (var selectedItem in PartitionList.SelectedItems)
            {
                string partitionName = selectedItem.ToString() ?? string.Empty;
                string baseFileName = isABChecked ? partitionName + "_ab" : partitionName;

                // Tìm file th?c t? trong th? m?c firmware (.img ho?c .bin)
                string? imagePath = Directory.GetFiles(firmwareDir)
                    .FirstOrDefault(file => Path.GetFileNameWithoutExtension(file) == baseFileName);

                if (imagePath == null && isABChecked)
                {
                    imagePath = Directory.GetFiles(firmwareDir)
                        .FirstOrDefault(file => Path.GetFileNameWithoutExtension(file) == partitionName);
                }

                if (imagePath == null)
                {
                    FastbootLog.AppendText($"? Không tìm th?y file cho phân vùng: {baseFileName} (.img ho?c .bin)\n");
                    continue;
                }

                // Ki?m tra n?u file này ?ã ???c flash tr??c ?ó (tránh flash trùng)
                if (flashedFiles.Contains(imagePath))
                {
                    FastbootLog.AppendText($"?? B? qua: {baseFileName} ?ã ???c flash tr??c ?ó.\n");
                    continue;
                }

                // T?o l?nh fastboot flash
                string arguments = $"-s {selectedDevice} flash {partitionName} \"{imagePath}\"";

                // Ghi log tr??c khi ch?y l?nh
                FastbootLog.AppendText($"?? Ch?y l?nh: {fastbootPath} {arguments}\n");

                // Ch?y l?nh flash và ghi l?i toàn b? output
                string output = await fastbootCommandRunner.RunFastbootCommandAsync(arguments);
                FastbootLog.AppendText(output + "\n");

                // Thêm file ?ã flash vào danh sách
                flashedFiles.Add(imagePath);
            }

            FastbootLog.AppendText("\n? Hoàn t?t flash t?t c? các file!\n");
        }






        // Helper method to run fastboot commands and update FastbootLog
        private void RunFastbootCommand(string arguments)
        {
            fastbootCommandRunner.RunFastbootCommand(arguments);
        }


        private void SelectImage_Click(object sender, EventArgs e)
        {

        }

        private void Format_Click(object sender, EventArgs e)
        {
            {
                if (FastbootList.SelectedItem != null)
                {
                    var confirmResult = MessageBox.Show(
                        "Are you sure you want to format the selected device?",
                        "Confirm Format",
                        MessageBoxButtons.YesNo);

                    if (confirmResult == DialogResult.Yes)
                    {
                        string selectedDevice = FastbootList.SelectedItem?.ToString() ?? string.Empty;
                        string arguments = $"-s {selectedDevice} -w";

                        // Log the command for debugging
                        FastbootLog.Text = $"Running command: {FastbootPath} {arguments}\n";

                        RunFastbootCommand(arguments);
                    }
                }
                else
                {
                    MessageBox.Show("No device selected.", "Error");
                }
            }
        }

        private void FastbootRun_Click(object sender, EventArgs e)
        {
            if (FastbootList.SelectedItem != null && !string.IsNullOrEmpty(CommandBox.Text))
            {
                string selectedDevice = FastbootList.SelectedItem?.ToString() ?? string.Empty;
                string command = CommandBox.Text;
                string arguments = $"-s {selectedDevice} {command}";

                // Log the command for debugging
                FastbootLog.Text = $"Running command: {FastbootPath} {arguments}\n";

                RunFastbootCommand(arguments);
            }
            else
            {
                MessageBox.Show("No device or command specified.", "Error");
            }
        }


        private void FastbootReboot_Click(object sender, EventArgs e)
        {
            if (FastbootList.SelectedItem != null)
            {
                var confirmResult = MessageBox.Show(
                    "Are you sure you want to reboot the selected device?",
                    "Confirm Reboot",
                    MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    var rebootModeResult = MessageBox.Show(
                        "Select Reboot Mode:\nYes - Recovery\nNo - Bootloader\nCancel - System",
                        "Reboot Mode",
                        MessageBoxButtons.YesNoCancel);

                    string selectedDevice = FastbootList.SelectedItem?.ToString() ?? string.Empty;
                    string arguments = $"-s {selectedDevice} reboot";

                    if (rebootModeResult == DialogResult.Yes)
                    {
                        arguments += " recovery";
                    }
                    else if (rebootModeResult == DialogResult.No)
                    {
                        arguments += " bootloader";
                    }

                    // Log the command for debugging
                    FastbootLog.Text = $"Running command: {FastbootPath} {arguments}\n";

                    RunFastbootCommand(arguments);
                }
            }
            else
            {
                MessageBox.Show("No device selected.", "Error");
            }
        }

        private void APKList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void APKList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Verity_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void crownGroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Firmware_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = fbd.SelectedPath;

                    // Handle the selected folder path
                    FirmwareBox.Text = selectedPath;

                    // Clear the existing items in PartitionList
                    PartitionList.Items.Clear();

                    // Get all .img and .bin files in the selected folder
                    string[] imgFiles = System.IO.Directory.GetFiles(selectedPath, "*.img");
                    string[] binFiles = System.IO.Directory.GetFiles(selectedPath, "*.bin");

                    // Add each file to PartitionList
                    foreach (string file in imgFiles.Concat(binFiles))
                    {
                        PartitionList.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                    }
                }
            }

        }

        private void FastbootLog_TextChanged(object sender, EventArgs e)
        {

        }

        private async void Unlock_Click(object sender, EventArgs e)
        {
            if (FastbootList.SelectedItem == null)
            {
                MessageBox.Show("? Not device Selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedDevice = FastbootList.SelectedItem?.ToString() ?? string.Empty;

            // H?p tho?i c?nh báo
            DialogResult result = MessageBox.Show(
                "?? Warning: Unlocking the Bootloader may erase all data on the device and may void the warranty.\n\nAre you sure you want to continue?",
                "Confirm Unlock Bootloader",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                FastbootLog.AppendText("? Cancel Unlock Bootloader.\n");
                return;
            }

            string fastbootPath = System.IO.Path.Combine(ADBBox.Text, "fastboot.exe");
            string arguments = $"-s {selectedDevice} flashing unlock";

            // Ghi log tr??c khi ch?y l?nh
            FastbootLog.AppendText($"?? Running Command: {fastbootPath} {arguments}\n");

            // Ch?y l?nh Unlock và ghi l?i output
            string output = await fastbootCommandRunner.RunFastbootCommandAsync(arguments);
            FastbootLog.AppendText(output + "\n");

            FastbootLog.AppendText("\n? Complete Unlock Bootloader!\n");
        }

        private async void Lock_Click(object sender, EventArgs e)
        {
            if (FastbootList.SelectedItem == null)
            {
                MessageBox.Show("? Not device Selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedDevice = FastbootList.SelectedItem?.ToString() ?? string.Empty;

            // H?p tho?i c?nh báo
            DialogResult result = MessageBox.Show(
                "?? Warning: Relocking the Bootloader will block the ability to install custom software.\n" +
                "If the device is running unofficial firmware, re-locking the Bootloader may cause errors (soft brick).\n\n" +
                "Are you sure you want to continue?",
                "Confirm Lock Bootloader",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                FastbootLog.AppendText("? Cancel Lock Bootloader.\n");
                return;
            }

            string fastbootPath = System.IO.Path.Combine(ADBBox.Text, "fastboot.exe");
            string arguments = $"-s {selectedDevice} flashing lock";

            // Ghi log tr??c khi ch?y l?nh
            FastbootLog.AppendText($"?? Running Command: {fastbootPath} {arguments}\n");

            // Ch?y l?nh Lock và ghi l?i output
            string output = await fastbootCommandRunner.RunFastbootCommandAsync(arguments);
            FastbootLog.AppendText(output + "\n");

            FastbootLog.AppendText("\n? Complete Lock Bootloader!\n");
        }

        private async void Shell_Click(object sender, EventArgs e)
        {
            if (DeviceList.SelectedItem == null)
            {
                MessageBox.Show("❌ Không có thiết bị nào được chọn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedDevice = DeviceList.SelectedItem?.ToString() ?? string.Empty;
            string adbPath = Path.Combine(ADBBox.Text, "adb.exe");

            if (!File.Exists(adbPath))
            {
                MessageBox.Show($"⚠️ Không tìm thấy adb.exe tại {adbPath}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Nếu SUCheck được tick, kiểm tra xem thiết bị có quyền Superuser không
            if (SUCheck.Checked)
            {
                string suCheckCommand = $"-s {selectedDevice} shell su -c \"whoami\"";
                string output = await adbCommandRunner.RunAdbCommandAsync(suCheckCommand);

                if (!output.Trim().Equals("root", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("❌ Shell trên thiết bị chưa được cấp quyền Superuser.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Nếu SUCheck được tick, chạy shell với quyền Superuser
            string adbShellCommand = SUCheck.Checked ? "su" : "";
            string command = $"\"{adbPath}\" -s {selectedDevice} shell {adbShellCommand}";

            // Mở cửa sổ CMD và chạy lệnh
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/k {command}",  // /k: Giữ cửa sổ CMD mở sau khi chạy lệnh
                WorkingDirectory = Path.GetDirectoryName(adbPath), // Đặt thư mục làm việc
                UseShellExecute = true // Mở CMD bên ngoài thay vì trong tiến trình ẩn
            };

            Process.Start(startInfo);
        }

    }
}
