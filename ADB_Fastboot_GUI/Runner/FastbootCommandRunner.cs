using ReaLTaiizor.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADB_Fastboot_GUI
{
    public class FastbootCommandRunner
    {
        private readonly string fastbootPath;
        private readonly CrownComboBox deviceList;
        private readonly TextBox fastbootLog;

        public FastbootCommandRunner(string fastbootPath, CrownComboBox deviceList, TextBox fastbootLog)
        {
            this.fastbootPath = fastbootPath;
            this.deviceList = deviceList;
            this.fastbootLog = fastbootLog;
        }

        /// <summary>
        /// Chạy lệnh Fastboot đồng bộ (blocking)
        /// </summary>
        public string RunFastbootCommand(string arguments)
        {
            if (!File.Exists(fastbootPath))
            {
                return "⚠️ fastboot.exe not found.\n";
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = fastbootPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = System.Text.Encoding.UTF8, // ✅ Đọc output UTF-8
                StandardErrorEncoding = System.Text.Encoding.UTF8
            };

            Process? process = Process.Start(startInfo);
            if (process == null)
            {
                return "⚠️ Error: Cannot run fastboot.\n";
            }

            using (process)
            {
                string output = process.StandardOutput.ReadToEnd();
                string errorOutput = process.StandardError.ReadToEnd();
                process.WaitForExit();

                // Ghi output vào FastbootLog
                fastbootLog.AppendText(output + "\n");

                // Xử lý lỗi nếu có
                if (!string.IsNullOrEmpty(errorOutput))
                {
                    string errorMsg = $"\n❌ Error: {errorOutput}\n";
                    fastbootLog.AppendText(errorMsg);
                }

                // Nếu là lệnh "fastboot devices", cập nhật danh sách thiết bị
                if (arguments.Contains("devices"))
                {
                    UpdateDeviceList(output);
                }

                return output;
            }
        }

        /// <summary>
        /// Chạy lệnh Fastboot bất đồng bộ (async/await)
        /// </summary>
        public async Task<string> RunFastbootCommandAsync(string arguments)
        {
            if (!File.Exists(fastbootPath))
            {
                return "⚠️ fastboot.exe không tìm thấy.\n";
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = fastbootPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process? process = Process.Start(startInfo);
            if (process == null)
            {
                return "⚠️ Lỗi: Không thể khởi động fastboot.\n";
            }

            using (process)
            {
                string output = await process.StandardOutput.ReadToEndAsync();
                string errorOutput = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                // Ghi output vào FastbootLog
                fastbootLog.AppendText(output + "\n");

                // Xử lý lỗi nếu có
                if (!string.IsNullOrEmpty(errorOutput))
                {
                    string errorMsg = $"\n❌ Error: {errorOutput}\n";
                    fastbootLog.AppendText(errorMsg);
                }

                // Nếu là lệnh "fastboot devices", cập nhật danh sách thiết bị
                if (arguments.Contains("devices"))
                {
                    UpdateDeviceList(output);
                }

                return output;
            }
        }

        /// <summary>
        /// Cập nhật danh sách thiết bị Fastboot từ đầu ra của lệnh "fastboot devices".
        /// </summary>
        private void UpdateDeviceList(string output)
        {
            deviceList.Items.Clear();
            string[] lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (line.Contains("fastboot"))
                {
                    string[] parts = line.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0)
                    {
                        deviceList.Items.Add(parts[0]);
                    }
                }
            }

            if (deviceList.Items.Count == 0)
            {
                MessageBox.Show("🚫 Không tìm thấy thiết bị ở chế độ Fastboot.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                deviceList.SelectedIndex = 0; // Chọn thiết bị đầu tiên mặc định
            }
        }
    }
}
