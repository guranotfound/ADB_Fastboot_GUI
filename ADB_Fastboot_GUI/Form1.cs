using System.Diagnostics;
using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using Timer = System.Windows.Forms.Timer;
using ReaLTaiizor.Controls;

namespace ADB_Fastboot_GUI
{
    public partial class Form1 : Form
    {
        private Timer clearLogTimer;

        public string AdbPath { get; private set; }
        public string FastbootPath => System.IO.Path.Combine(ADBBox.Text, "fastboot.exe");

        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.AutoScaleMode = AutoScaleMode.None; //Disable Form scaling

            //Disable auto size for materialTabControl1
            materialTabControl1.AutoSize = false;

            // Initialize the timer
            clearLogTimer = new Timer();
            clearLogTimer.Interval = 180000; // 3 seconds
            clearLogTimer.Tick += ClearLogTimer_Tick;

            //Load last ADB path
            LoadLastUseddAdbPath();

            // Ensure AdbPath is initialized
            if (string.IsNullOrEmpty(AdbPath))
            {
                AdbPath = System.IO.Path.Combine(ADBBox.Text, "adb.exe");
            }
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
            if (DeviceList.SelectedItem != null && APKList.SelectedItems.Count > 0)
            {
                string selectedDevice = DeviceList.SelectedItem?.ToString() ?? string.Empty;
                var selectedPackages = APKList.SelectedItems.Cast<string>().ToList();

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
            if (System.IO.File.Exists(AdbPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = AdbPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process? process = Process.Start(startInfo))
                {
                    if (process != null)
                    {
                        using (StreamReader reader = process.StandardOutput)
                        {
                            string result = reader.ReadToEnd();
                            ADBLog.Text = result;

                            if (arguments.Contains("devices") && (result.Contains("device") || result.Contains("sideload")))
                            {
                                DeviceList.Items.Clear();
                                string[] lines = result.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string line in lines)
                                {
                                    if ((line.Contains("device") || line.Contains("sideload")) && !line.Contains("List of devices attached"))
                                    {
                                        string[] parts = line.Split('\t');
                                        if (parts.Length > 1 && (parts[1] == "device" || parts[1] == "sideload"))
                                        {
                                            DeviceList.Items.Add(parts[0]);
                                        }
                                    }
                                }

                                // Auto-select the first device in the list
                                if (DeviceList.Items.Count > 0)
                                {
                                    DeviceList.SelectedIndex = 0;
                                }
                            }

                            if (updateAPKList && (filter == null || result.Contains(filter)))
                            {
                                APKList.Items.Clear();
                                string[] lines = result.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string line in lines)
                                {
                                    if (line.StartsWith("package:"))
                                    {
                                        APKList.Items.Add(line.Substring("package:".Length));
                                    }
                                }
                            }

                            // Check for sideload capability and add serial to DeviceList
                            if (checkForSideload)
                            {
                                string[] lines = result.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string line in lines)
                                {
                                    if (line.Contains("device") && line.Contains("sideload"))
                                    {
                                        string[] parts = line.Split('\t');
                                        if (parts.Length > 1 && parts[1] == "device")
                                        {
                                            DeviceList.Items.Add(parts[0]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ADBLog.AppendText("\nError: Process could not be started.");
                    }
                }

                // Start the timer to clear the log after 3 seconds
                clearLogTimer.Start();
            }
            else
            {
                MessageBox.Show("adb.exe not found in the selected directory.", "Error");
            }
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
            if (DeviceList.SelectedItem != null && !string.IsNullOrEmpty(ShellBox.Text))
            {
                string selectedDevice = DeviceList.SelectedItem?.ToString() ?? string.Empty;
                string shellCommand = ShellBox.Text;
                string arguments = $"-s {selectedDevice}";

                if (ShellCheck.Checked)
                {
                    arguments += " shell";
                }

                arguments += $" {shellCommand}";
                RunAdbCommand(arguments);
            }
            else
            {
                MessageBox.Show("No device or command empty.", "Error");
            }
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
            RunFastbootCommand("devices");
        }

        // Add this method to handle the Flash click event
        private void Flash_Click(object sender, EventArgs e)
        {
            if (FastbootList.SelectedItem != null && !string.IsNullOrEmpty(PartitionBox.Text) && !string.IsNullOrEmpty(ImageBox.Text))
            {
                string selectedDevice = FastbootList.SelectedItem?.ToString() ?? string.Empty;
                string partition = PartitionBox.Text;
                string image = ImageBox.Text;
                string arguments = $"-s {selectedDevice} flash";

                if (Verity.Checked)
                {
                    arguments += " --disable-verity --disable-verification";
                }

                arguments += $" {partition} {image}";

                // Log the command for debugging
                FastbootLog.Text = $"Running command: {FastbootPath} {arguments}\n";

                RunFastbootCommand(arguments);
            }
            else
            {
                MessageBox.Show("No device, partition, or image selected.", "Error");
            }
        }


        // Helper method to run fastboot commands and update FastbootLog
        private void RunFastbootCommand(string arguments)
        {
            if (System.IO.File.Exists(FastbootPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = FastbootPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                try
                {
                    using (Process? process = Process.Start(startInfo))
                    {
                        if (process != null)
                        {
                            using (StreamReader reader = process.StandardOutput)
                            {
                                string result = reader.ReadToEnd();
                                FastbootLog.AppendText(result);

                                // Parse the output and add serial numbers to FastbootList if "devices" command is run
                                if (arguments.Contains("devices"))
                                {
                                    FastbootList.Items.Clear();
                                    string[] lines = result.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (string line in lines)
                                    {
                                        if (line.Contains("fastboot"))
                                        {
                                            string[] parts = line.Split('\t');
                                            if (parts.Length > 0)
                                            {
                                                FastbootList.Items.Add(parts[0]);
                                            }
                                        }
                                    }
                                }
                            }

                            using (StreamReader errorReader = process.StandardError)
                            {
                                string errorResult = errorReader.ReadToEnd();
                                if (!string.IsNullOrEmpty(errorResult))
                                {
                                    FastbootLog.AppendText($"\nError: {errorResult}");
                                }
                            }
                        }
                        else
                        {
                            FastbootLog.AppendText("\nError: Process could not be started.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    FastbootLog.AppendText($"\nException: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("fastboot.exe not found in the selected directory.", "Error");
            }
        }


        private void SelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "All files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ImageBox.Text = ofd.FileName;
                }
            }
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
    }
    
}
