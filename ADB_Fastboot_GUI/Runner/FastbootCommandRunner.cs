using ReaLTaiizor.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ADB_Fastboot_GUI
{
    public class FastbootCommandRunner
    {
        private readonly string fastbootPath;
        private readonly CrownComboBox fastbootList;
        private readonly TextBox fastbootLog;

        public FastbootCommandRunner(string fastbootPath, CrownComboBox fastbootList, TextBox fastbootLog)
        {
            this.fastbootPath = fastbootPath;
            this.fastbootList = fastbootList;
            this.fastbootLog = fastbootLog;
        }

        public void RunFastbootCommand(string arguments)
        {
            if (File.Exists(fastbootPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = fastbootPath,
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
                                fastbootLog.AppendText(result);

                                // Parse the output and add serial numbers to fastbootList if "devices" command is run
                                if (arguments.Contains("devices"))
                                {
                                    fastbootList.Items.Clear();
                                    string[] lines = result.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                    foreach (string line in lines)
                                    {
                                        if (line.Contains("fastboot"))
                                        {
                                            string[] parts = line.Split('\t');
                                            if (parts.Length > 0)
                                            {
                                                fastbootList.Items.Add(parts[0]);
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
                                    fastbootLog.AppendText($"\nError: {errorResult}");
                                }
                            }
                        }
                        else
                        {
                            fastbootLog.AppendText("\nError: Process could not be started.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    fastbootLog.AppendText($"\nException: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("fastboot.exe not found in the selected directory.", "Error");
            }
        }
    }
}
