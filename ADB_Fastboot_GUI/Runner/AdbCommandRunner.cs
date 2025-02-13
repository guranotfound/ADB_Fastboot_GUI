using ReaLTaiizor.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ADB_Fastboot_GUI
{
    public class AdbCommandRunner
    {
        private readonly string adbPath;
        private readonly Timer clearLogTimer;
        private readonly CrownComboBox deviceList;
        private readonly CheckedListBox apkList;
        private readonly TextBox adbLog;

        public AdbCommandRunner(string adbPath, Timer clearLogTimer, CrownComboBox deviceList, CheckedListBox apkList, TextBox adbLog)
        {
            this.adbPath = adbPath;
            this.clearLogTimer = clearLogTimer;
            this.deviceList = deviceList;
            this.apkList = apkList;
            this.adbLog = adbLog;
        }

        public void RunAdbCommand(string arguments, bool updateAPKList = false, string? filter = null, bool checkForSideload = true)
        {
            if (File.Exists(adbPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = adbPath,
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
                            adbLog.Text = result;

                            if (arguments.Contains("devices") && (result.Contains("device") || result.Contains("sideload")))
                            {
                                deviceList.Items.Clear();
                                string[] lines = result.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string line in lines)
                                {
                                    if ((line.Contains("device") || line.Contains("sideload")) && !line.Contains("List of devices attached"))
                                    {
                                        string[] parts = line.Split('\t');
                                        if (parts.Length > 1 && (parts[1] == "device" || parts[1] == "sideload"))
                                        {
                                            deviceList.Items.Add(parts[0]);
                                        }
                                    }
                                }

                                // Auto-select the first device in the list
                                if (deviceList.Items.Count > 0)
                                {
                                    deviceList.SelectedIndex = 0;
                                }
                            }

                            if (updateAPKList && (filter == null || result.Contains(filter)))
                            {
                                apkList.Items.Clear();
                                string[] lines = result.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string line in lines)
                                {
                                    if (line.StartsWith("package:"))
                                    {
                                        apkList.Items.Add(line.Substring("package:".Length));
                                    }
                                }
                            }

                            // Check for sideload capability and add serial to deviceList
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
                                            deviceList.Items.Add(parts[0]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        adbLog.AppendText("\nError: Process could not be started.");
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
        public async Task<string> RunAdbCommandAsync(string arguments)
        {
            if (!File.Exists(adbPath))
            {
                return "⚠️ adb.exe không tìm thấy.\n";
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = adbPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = System.Text.Encoding.UTF8,
                StandardErrorEncoding = System.Text.Encoding.UTF8
            };

            Process? process = Process.Start(startInfo);
            if (process == null)
            {
                return "⚠️ Lỗi: Không thể khởi động tiến trình.\n";
            }

            using (process)
            {
                string output = await process.StandardOutput.ReadToEndAsync();
                string errorOutput = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                return string.IsNullOrEmpty(errorOutput) ? output : $"{output}\n❌ Lỗi: {errorOutput}";
            }

        }

    }
}
