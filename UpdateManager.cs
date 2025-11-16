using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

#region THÔNG BÁO PHIÊN BẢN MỚI
public static class UpdateManager
{
    // 🔹 BIẾN LƯU TRẠNG THÁI KIỂM TRA
    private static System.Windows.Forms.Timer _updateCheckTimer;
    private static DateTime _lastCheckTime = DateTime.MinValue;
    private const int CHECK_INTERVAL_HOURS = 12;
    private const int HTTP_TIMEOUT_SECONDS = 8; // ✅ Tăng timeout
    private const int CIFS_TIMEOUT_SECONDS = 15;

    // 🔹 KHỞI TẠO TIMER KIỂM TRA TỰ ĐỘNG
    public static void InitializeAutoCheck(string exeName, string[] httpServers)
    {
        StopAutoCheck();
        CheckForUpdates(exeName, httpServers);
        _lastCheckTime = DateTime.Now;

        _updateCheckTimer = new System.Windows.Forms.Timer
        {
            Interval = CHECK_INTERVAL_HOURS * 60 * 60 * 1000
        };

        _updateCheckTimer.Tick += (s, e) =>
        {
            try
            {
                TimeSpan timeSinceLastCheck = DateTime.Now - _lastCheckTime;
                if (timeSinceLastCheck.TotalHours >= CHECK_INTERVAL_HOURS)
                {
                    Debug.WriteLine($"[Auto Check] Đã {timeSinceLastCheck.TotalHours:F1} giờ, kiểm tra cập nhật...");
                    CheckForUpdates(exeName, httpServers);
                    _lastCheckTime = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Auto Check] Lỗi trong timer: {ex.Message}");
                InitializeAutoCheck(exeName, httpServers);
            }
        };

        _updateCheckTimer.Start();
        Debug.WriteLine($"[Auto Check] Timer đã khởi động - kiểm tra mỗi {CHECK_INTERVAL_HOURS} giờ");
    }

    public static void RestartTimerIfStopped(string exeName, string[] httpServers)
    {
        if (_updateCheckTimer == null || !_updateCheckTimer.Enabled)
        {
            Debug.WriteLine("[Auto Check] Timer bị dừng, khởi động lại...");
            InitializeAutoCheck(exeName, httpServers);
        }
    }

    public static void StopAutoCheck()
    {
        if (_updateCheckTimer != null)
        {
            _updateCheckTimer.Stop();
            _updateCheckTimer.Dispose();
            _updateCheckTimer = null;
            Debug.WriteLine("[Auto Check] Timer đã dừng");
        }
    }

    // 🔹 ✅ KIỂM TRA CẬP NHẬT VỚI LOGIC TỐI ƯU
    public static async void CheckForUpdates(string exeName, string[] httpServers)
    {
        try
        {
            string currentVersion = Application.ProductVersion;
            string latestVersion = null;
            string changelog = "";
            string workingSource = null;
            string workingPath = null;

            Debug.WriteLine($"[Cập nhật] Phiên bản hiện tại: {currentVersion}");

            // ✅ BƯỚC 1: THỬ HTTP TRƯỚC (ƯU TIÊN)
            var httpResult = await TryCheckVersionViaHTTP(httpServers);
            if (httpResult.Success)
            {
                latestVersion = httpResult.Version;
                workingSource = "http";
                workingPath = httpResult.ServerUrl;
                Debug.WriteLine($"[Cập nhật] ✅ HTTP thành công! Phiên bản: {latestVersion}");
            }
            else
            {
                Debug.WriteLine("[Cập nhật] ❌ HTTP thất bại, chuyển sang CIFS...");

                // ✅ BƯỚC 2: NẾU HTTP THẤT BẠI, THỬ CIFS
                var cifsResult = await TryCheckVersionViaCIFS();
                if (cifsResult.Success)
                {
                    latestVersion = cifsResult.Version;
                    workingSource = "cifs";
                    workingPath = cifsResult.NasPath;
                    Debug.WriteLine($"[Cập nhật] ✅ CIFS thành công! Phiên bản: {latestVersion}");
                }
                else
                {
                    Debug.WriteLine("[Cập nhật] ❌ Tất cả nguồn thất bại!");
                    return;
                }
            }

            // ✅ BƯỚC 3: LẤY CHANGELOG (VỚI TIMEOUT)
            changelog = await GetChangelogSafe(workingSource, workingPath);

            // ✅ BƯỚC 4: SO SÁNH VERSION
            if (string.Compare(latestVersion, currentVersion, StringComparison.OrdinalIgnoreCase) > 0)
            {
                Debug.WriteLine($"[Cập nhật] Đã có phiên bản mới: {latestVersion} > {currentVersion}");
                ShowUpdatePrompt(latestVersion, changelog, workingSource, workingPath, exeName);
            }
            else
            {
                Debug.WriteLine($"[Cập nhật] Đã cập nhật: {currentVersion}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[Cập nhật bị lỗi] {ex.Message}");
        }
    }

    // ✅ PHƯƠNG THỨC KIỂM TRA HTTP (RETURN STRUCT)
    private static async Task<(bool Success, string Version, string ServerUrl)> TryCheckVersionViaHTTP(string[] servers)
    {
        using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(HTTP_TIMEOUT_SECONDS) })
        {
            foreach (var server in servers)
            {
                try
                {
                    string url = server.TrimEnd(new[] { '/' }) + "/version.txt";
                    Debug.WriteLine($"[HTTP] Đang thử: {url}");
                    string version = (await client.GetStringAsync(url)).Trim();
                    return (true, version, server.TrimEnd('/'));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[HTTP] Lỗi {server}: {ex.Message}");
                }
            }
        }
        return (false, null, null);
    }

    // ✅ PHƯƠNG THỨC KIỂM TRA CIFS (RETURN STRUCT)
    private static async Task<(bool Success, string Version, string NasPath)> TryCheckVersionViaCIFS()
    {
        return await Task.Run(() =>
        {
            var servers = SecureConfig.GetServers();
            foreach (var nas in servers.NasServers ?? new SecureConfig.NasConfig[0])
            {
                try
                {
                    var config = new NasConnectionInfo
                    {
                        Path = nas.Path,
                        Username = nas.Username,
                        Password = nas.Password
                    };

                    if (!ConnectToNas(config, msg => Debug.WriteLine($"[CIFS] {msg}")))
                        continue;

                    string versionFile = Path.Combine(nas.Path, "version.txt");
                    if (File.Exists(versionFile))
                    {
                        string version = File.ReadAllText(versionFile).Trim();
                        Debug.WriteLine($"[CIFS] ✅ Tìm thấy version: {version}");
                        return (true, version, nas.Path);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[CIFS] Lỗi {nas.Path}: {ex.Message}");
                }
                finally
                {
                    try { WNetCancelConnection2(nas.Path, 0, true); } catch { }
                }
            }
            return (false, null, null);
        });
    }

    // ✅ LẤY CHANGELOG AN TOÀN (VỚI TIMEOUT)
    private static async Task<string> GetChangelogSafe(string source, string path)
    {
        try
        {
            if (source == "http")
            {
                using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(HTTP_TIMEOUT_SECONDS) })
                {
                    return await client.GetStringAsync(path + "/changelog.txt");
                }
            }
            else if (source == "cifs")
            {
                return await Task.Run(() =>
                {
                    var servers = SecureConfig.GetServers();
                    var nas = servers.NasServers?.FirstOrDefault(n => n.Path == path);
                    if (nas == null) return "(Không tải được changelog)";

                    var config = new NasConnectionInfo
                    {
                        Path = nas.Path,
                        Username = nas.Username,
                        Password = nas.Password
                    };

                    if (!ConnectToNas(config, null)) return "(Không kết nối được NAS)";

                    try
                    {
                        string changelogFile = Path.Combine(path, "changelog.txt");
                        return File.Exists(changelogFile) ? File.ReadAllText(changelogFile) : "(Không tìm thấy changelog)";
                    }
                    finally
                    {
                        try { WNetCancelConnection2(path, 0, true); } catch { }
                    }
                });
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[Changelog] Lỗi: {ex.Message}");
        }
        return "(Không có thông tin thay đổi)";
    }

    // ✅ STRUCT THAY CHO CLASS NasConfig
    private class NasConnectionInfo
    {
        public string Path { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    // 🔹 KẾT NỐI NAS
    [DllImport("mpr.dll")]
    private static extern int WNetAddConnection2(ref NETRESOURCE lpNetResource, string lpPassword, string lpUsername, int dwFlags);

    [DllImport("mpr.dll")]
    private static extern int WNetCancelConnection2(string lpName, int dwFlags, bool fForce);

    [StructLayout(LayoutKind.Sequential)]
    private struct NETRESOURCE
    {
        public int dwScope;
        public int dwType;
        public int dwDisplayType;
        public int dwUsage;
        public string lpLocalName;
        public string lpRemoteName;
        public string lpComment;
        public string lpProvider;
    }

    private static bool ConnectToNas(NasConnectionInfo config, Action<string> progress)
    {
        try
        {
            WNetCancelConnection2(config.Path, 0, true);

            var netResource = new NETRESOURCE
            {
                dwType = 1,
                lpRemoteName = config.Path
            };

            progress?.Invoke($"🔐 Đang kết nối: {config.Path}");
            int result = WNetAddConnection2(ref netResource, config.Password, config.Username, 0);

            if (result == 0)
            {
                progress?.Invoke($"✅ Kết nối thành công: {config.Path}");
                return true;
            }
            else
            {
                progress?.Invoke($"❌ Lỗi kết nối {config.Path}: Error code {result}");
                return false;
            }
        }
        catch (Exception ex)
        {
            progress?.Invoke($"❌ Exception kết nối {config.Path}: {ex.Message}");
            return false;
        }
    }

    // ✅ TÌM FILE .BAT UNLOCK EPS
    private static async Task<List<string>> FindAllUnlockBatAsync(IProgress<string> progress)
    {
        return await Task.Run(() =>
        {
            var batFiles = new List<string>();
            var servers = SecureConfig.GetServers();

            var localPaths = new List<string>
            {
                Application.StartupPath,
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "EPS"),
            };

            foreach (var localPath in localPaths)
            {
                if (!Directory.Exists(localPath)) continue;

                try
                {
                    progress?.Report($"🔍 Đang quét local: {localPath}");
                    var foundFiles = Directory.GetFiles(localPath, "*.bat", SearchOption.AllDirectories)
                        .Where(f =>
                        {
                            string name = Path.GetFileName(f).ToLower();
                            return name.Contains("unlock") || name.Contains("eps") ||
                                   name.Contains("disable") || name.Contains("unblock");
                        })
                        .ToList();

                    if (foundFiles.Any())
                    {
                        progress?.Report($"✅ Tìm thấy {foundFiles.Count} file .bat trong {localPath}");
                        batFiles.AddRange(foundFiles);
                    }
                }
                catch (Exception ex)
                {
                    progress?.Report($"⚠️ Lỗi quét {localPath}: {ex.Message}");
                }
            }

            foreach (var nas in servers.NasServers ?? new SecureConfig.NasConfig[0])
            {
                var config = new NasConnectionInfo
                {
                    Path = nas.Path,
                    Username = nas.Username,
                    Password = nas.Password
                };

                if (!ConnectToNas(config, msg => progress?.Report(msg)))
                    continue;

                try
                {
                    if (!Directory.Exists(nas.Path))
                    {
                        progress?.Report($"❌ Không tồn tại: {nas.Path}");
                        continue;
                    }

                    progress?.Report($"🔍 Đang quét NAS: {nas.Path}");
                    var foundFiles = Directory.GetFiles(nas.Path, "*.bat", SearchOption.AllDirectories)
                        .Where(f =>
                        {
                            string name = Path.GetFileName(f).ToLower();
                            return name.Contains("unlock") || name.Contains("eps") ||
                                   name.Contains("disable") || name.Contains("unblock");
                        })
                        .ToList();

                    if (foundFiles.Any())
                    {
                        progress?.Report($"✅ Tìm thấy {foundFiles.Count} file .bat trong {nas.Path}");
                        batFiles.AddRange(foundFiles);
                    }
                }
                catch (Exception ex)
                {
                    progress?.Report($"⚠️ Lỗi quét {nas.Path}: {ex.Message}");
                }
                finally
                {
                    try { WNetCancelConnection2(nas.Path, 0, true); } catch { }
                }
            }

            if (!batFiles.Any())
            {
                progress?.Report("❌ Không tìm thấy file .bat unlock EPS");
                return batFiles;
            }

            batFiles = batFiles.OrderByDescending(f =>
            {
                string name = Path.GetFileName(f).ToLower();
                if (name.Contains("unlock") && name.Contains("eps")) return 2;
                if (name.Contains("unlock") || name.Contains("eps")) return 1;
                return 0;
            }).ToList();

            return batFiles;
        });
    }

    private static bool IsAllowedIPRange()
    {
        try
        {
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    string ipString = ip.ToString();
                    if (ipString.StartsWith("107.126.") || ipString.StartsWith("107.115."))
                    {
                        Debug.WriteLine($"[IP Check] ✅ IP được phép unlock: {ipString}");
                        return true;
                    }

                    if (ipString.StartsWith("107.125."))
                    {
                        Debug.WriteLine($"[IP Check] ❌ IP KHÔNG được phép unlock: {ipString}");
                        return false;
                    }
                }
            }

            Debug.WriteLine("[IP Check] ⚠️ Không tìm thấy IP phù hợp, mặc định KHÔNG unlock");
            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[IP Check] ⚠️ Lỗi kiểm tra IP: {ex.Message}");
            return false;
        }
    }

    private static async Task<bool> RunBatFileAsync(string batFilePath, IProgress<string> progress)
    {
        try
        {
            progress?.Report($"▶️ Đang chạy: {Path.GetFileName(batFilePath)}");

            var processInfo = new ProcessStartInfo
            {
                FileName = batFilePath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(batFilePath)
            };

            using (var process = new Process { StartInfo = processInfo })
            {
                var outputBuilder = new System.Text.StringBuilder();
                var errorBuilder = new System.Text.StringBuilder();

                process.OutputDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        outputBuilder.AppendLine(e.Data);
                        progress?.Report($"  📄 {e.Data}");
                    }
                };

                process.ErrorDataReceived += (s, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        errorBuilder.AppendLine(e.Data);
                        progress?.Report($"  ⚠️ {e.Data}");
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                bool completed = await Task.Run(() => process.WaitForExit(30000));

                if (!completed)
                {
                    progress?.Report("⏱️ Timeout - Dừng process");
                    try { process.Kill(); } catch { }
                    return false;
                }

                bool success = process.ExitCode == 0;
                progress?.Report(success
                    ? "✅ Unlock EPS thành công!"
                    : $"⚠️ Exit code: {process.ExitCode}");

                return success;
            }
        }
        catch (Exception ex)
        {
            progress?.Report($"❌ Lỗi chạy .bat: {ex.Message}");
            return false;
        }
    }

    // ✅ HIỂN THỊ FORM THÔNG BÁO
    private static void ShowUpdatePrompt(string latestVersion, string changelog, string source, string path, string exeName)
    {
        int cornerRadius = 20;
        var updateForm = new Form
        {
            Text = "Cập nhật phần mềm",
            Size = new Size(450, 300),
            StartPosition = FormStartPosition.Manual,
            FormBorderStyle = FormBorderStyle.None,
            TopMost = true,
            BackColor = Color.White,
            Icon = Application.OpenForms.Count > 0 ? Application.OpenForms[0].Icon : SystemIcons.Application
        };
        updateForm.Location = new Point(
            Screen.PrimaryScreen.WorkingArea.Right - updateForm.Width - 20,
            Screen.PrimaryScreen.WorkingArea.Bottom - updateForm.Height - 20
        );

        IntPtr hRgn = CreateRoundRectRgn(0, 0, updateForm.Width, updateForm.Height, cornerRadius, cornerRadius);
        updateForm.Region = Region.FromHrgn(hRgn);

        int val = 2;
        DwmSetWindowAttribute(updateForm.Handle, 2, ref val, 4);
        MARGINS margins = new MARGINS() { cxLeftWidth = 1, cxRightWidth = 1, cyTopHeight = 1, cyBottomHeight = 1 };
        DwmExtendFrameIntoClientArea(updateForm.Handle, ref margins);

        updateForm.Paint += (s, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(Color.FromArgb(128, Color.LightGray)))
            {
                pen.Width = 1f;
                e.Graphics.DrawRectangle(pen, 0.5f, 0.5f, updateForm.Width - 1, updateForm.Height - 1);
            }
        };

        var assembly = Assembly.GetExecutingAssembly();
        string resourceName = "FAB CONFIRM.src.update_icon.png";
        Image iconImage = SystemIcons.Shield.ToBitmap();

        try
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null && stream.Length > 0)
                {
                    using (var tempImage = new Bitmap(stream))
                    {
                        iconImage = new Bitmap(tempImage);
                    }
                }
            }
        }
        catch { }

        var picIcon = new PictureBox
        {
            Size = new Size(40, 40),
            Location = new Point(20, 20),
            Image = iconImage,
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = Color.Transparent
        };

        string appName = exeName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)
            ? exeName.Substring(0, exeName.Length - 4)
            : exeName;

        string currentVersion = Application.ProductVersion;
        var lblVersion = new Label
        {
            Text = $"{appName} đã có phiên bản mới: {latestVersion}\nPhiên bản hiện tại: {currentVersion}",
            Location = new Point(70, 15),
            Width = updateForm.Width - 90,
            Height = 40,
            TextAlign = ContentAlignment.TopLeft,
            Font = new Font("Segoe UI", 10, FontStyle.Bold)
        };

        var rtbChangelog = new RichTextBox
        {
            Text = changelog,
            Location = new Point(50, 60),
            Width = updateForm.Width - 60,
            Height = 170,
            BorderStyle = BorderStyle.None,
            BackColor = Color.White,
            Font = new Font("Segoe UI", 9),
            ScrollBars = RichTextBoxScrollBars.Vertical,
            ReadOnly = true,
            WordWrap = true,
        };

        var txtLog = new TextBox
        {
            Location = new Point(20, 80),
            Width = updateForm.Width - 40,
            Height = 150,
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            ReadOnly = true,
            Font = new Font("Consolas", 8),
            BackColor = Color.FromArgb(240, 240, 240),
            Visible = false
        };

        var panelButtons = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 90,
            BackColor = Color.White,
            Padding = new Padding(0, 0, 0, 10)
        };

        var btnUpdate = new Button
        {
            Text = "Cập nhật",
            Width = 100,
            Height = 35,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI Semibold", 10F),
            BackColor = Color.FromArgb(0, 120, 0),
            ForeColor = Color.White,
            Cursor = Cursors.Hand
        };
        btnUpdate.FlatAppearance.BorderSize = 0;
        btnUpdate.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 150, 255);
        btnUpdate.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnUpdate.Width, btnUpdate.Height, 10, 10));

        var btnSkip = new Button
        {
            Text = "Bỏ qua",
            Width = 100,
            Height = 35,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI Semibold", 10F),
            BackColor = Color.FromArgb(200, 200, 200),
            ForeColor = Color.Black,
            Cursor = Cursors.Hand
        };
        btnSkip.FlatAppearance.BorderSize = 0;
        btnSkip.FlatAppearance.MouseOverBackColor = Color.FromArgb(170, 170, 170);
        btnSkip.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSkip.Width, btnSkip.Height, 10, 10));

        btnUpdate.Location = new Point(70, 30);
        btnSkip.Location = new Point(panelButtons.Width - btnSkip.Width - 70, 30);
        btnSkip.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        var lblWarning = new Label
        {
            Text = $"Tip: Nếu không tự động unlock EPS hãy unlock rồi cập nhật lại",
            ForeColor = Color.Red,
            Font = new Font("Segoe UI", 9, FontStyle.Italic),
            AutoSize = true,
            TextAlign = ContentAlignment.MiddleCenter
        };
        lblWarning.Location = new Point(
            (panelButtons.Width - lblWarning.Width) / 2,
            btnUpdate.Bottom + 5
        );
        lblWarning.Anchor = AnchorStyles.None;

        panelButtons.Resize += (s, e) =>
        {
            lblWarning.Left = (panelButtons.Width - lblWarning.Width) / 2;
        };

        btnSkip.Click += (s, e) => updateForm.Close();

        btnUpdate.Click += async (s, e) =>
        {
            btnUpdate.Enabled = false;
            btnSkip.Enabled = false;
            txtLog.Visible = true;

            var progress = new Progress<string>(msg =>
            {
                if (updateForm.InvokeRequired)
                {
                    updateForm.Invoke(new Action(() =>
                    {
                        txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
                        txtLog.SelectionStart = txtLog.Text.Length;
                        txtLog.ScrollToCaret();
                    }));
                }
                else
                {
                    txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
                    txtLog.SelectionStart = txtLog.Text.Length;
                    txtLog.ScrollToCaret();
                }
            });

            await DownloadAndUpdateAsync(source, path, exeName, btnUpdate, updateForm, progress);
        };

        panelButtons.Controls.Add(btnUpdate);
        panelButtons.Controls.Add(btnSkip);
        panelButtons.Controls.Add(lblWarning);
        updateForm.Controls.Add(picIcon);
        updateForm.Controls.Add(lblVersion);
        updateForm.Controls.Add(txtLog);
        updateForm.Controls.Add(rtbChangelog);
        updateForm.Controls.Add(panelButtons);
        updateForm.Show();
    }

    // ✅ TẢI VÀ CẬP NHẬT (ĐÃ SỬA LOGIC)
    private static async Task DownloadAndUpdateAsync(
        string source, string path, string exeName,
        Button btnUpdate, Form updateForm, IProgress<string> progress)
    {
        string baseName = Path.GetFileNameWithoutExtension(exeName);
        string tempFile = Path.Combine(Path.GetTempPath(), baseName + "_Update.exe");

        try
        {
            // === BƯỚC 1: UNLOCK EPS (NẾU ĐƯỢC PHÉP) ===
            bool unlockSuccess = false;

            if (IsAllowedIPRange())
            {
                progress?.Report("🔓 Bắt đầu quá trình unlock EPS...");
                var unlockBatFiles = await FindAllUnlockBatAsync(progress);

                if (unlockBatFiles != null && unlockBatFiles.Any())
                {
                    progress?.Report($"📋 Tìm thấy {unlockBatFiles.Count} file unlock, sẽ thử lần lượt...");

                    foreach (var unlockBatPath in unlockBatFiles)
                    {
                        progress?.Report($"🔄 Đang thử: {Path.GetFileName(unlockBatPath)}");
                        unlockSuccess = await RunBatFileAsync(unlockBatPath, progress);

                        if (unlockSuccess)
                        {
                            progress?.Report($"✅ Unlock EPS thành công với: {Path.GetFileName(unlockBatPath)}");
                            await Task.Delay(1000);
                            break;
                        }
                        else
                        {
                            progress?.Report($"❌ Unlock thất bại với file này, thử file tiếp theo...");
                            await Task.Delay(500);
                        }
                    }

                    if (!unlockSuccess)
                    {
                        progress?.Report("⚠️ Đã thử tất cả file nhưng unlock không thành công, tiếp tục cập nhật...");
                        await Task.Delay(2000);
                    }
                }
                else
                {
                    progress?.Report("ℹ️ Không tìm thấy script unlock, bỏ qua bước này");
                    await Task.Delay(1000);
                }
            }
            else
            {
                progress?.Report("ℹ️ Dải IP hiện tại không được phép unlock EPS, bỏ qua bước này");
                await Task.Delay(1000);
            }

            // === BƯỚC 2: TẢI FILE CẬP NHẬT ===
            progress?.Report("📥 Bắt đầu tải xuống bản cập nhật...");
            Debug.WriteLine($"[Tải xuống] Nguồn: {source}, Đường dẫn: {path}");

            bool downloadSuccess = false;

            if (source == "http")
            {
                downloadSuccess = await DownloadUpdateViaHTTP(path, exeName, tempFile, btnUpdate, progress);
            }
            else if (source == "cifs")
            {
                downloadSuccess = await DownloadUpdateViaCIFS(path, exeName, tempFile, progress);
            }
            else
            {
                throw new Exception($"Nguồn cập nhật không hợp lệ: {source}");
            }

            if (!downloadSuccess)
            {
                throw new Exception("Không thể tải file cập nhật từ " + source.ToUpper());
            }

            await Task.Delay(2000);

            // === BƯỚC 3: TẠO .BAT & CHẠY CẬP NHẬT ===
            progress?.Report("🔄 Đang chuẩn bị cập nhật...");

            string currentExe = Application.ExecutablePath;
            string oldExePath = currentExe + ".old";
            string batFile = Path.Combine(Path.GetTempPath(), "update.bat");

            string batContent = $@"@echo off
            chcp 65001 >nul
            echo Đang chờ ứng dụng đóng...
            :waitloop
            timeout /t 2 /nobreak >nul
            tasklist /FI ""IMAGENAME eq {Path.GetFileName(currentExe)}"" 2>NUL | find /I /N ""{Path.GetFileName(currentExe)}"">NUL
            if ""%ERRORLEVEL%""==""0"" goto waitloop

            echo Ứng dụng đã đóng, bắt đầu cập nhật...
            timeout /t 3 /nobreak >nul

            if exist ""{oldExePath}"" (
                echo Xóa file backup cũ...
                del /f /q ""{oldExePath}""
                timeout /t 2 /nobreak >nul
            )

            if exist ""{currentExe}"" (
                echo Đổi tên file cũ...
                ren ""{currentExe}"" ""{Path.GetFileName(oldExePath)}""
                timeout /t 2 /nobreak >nul
            )

            echo Sao chép file mới...
            copy /y ""{tempFile}"" ""{currentExe}""
            timeout /t 2 /nobreak >nul

            if exist ""{currentExe}"" (
                echo Khởi động ứng dụng...
                timeout /t 2 /nobreak >nul
                start """" ""{currentExe}""
    
                echo Dọn dẹp...
                del /f /q ""{tempFile}"" 2>nul
                timeout /t 2 /nobreak >nul
                del /f /q ""{oldExePath}"" 2>nul
                timeout /t 2 /nobreak >nul
                del /f /q ""%~f0""
            ) else (
                echo LỖI: Không thể copy file mới!
                pause
            )";

            await WriteAllTextAsyncCompat(batFile, batContent, new System.Text.UTF8Encoding(false));

            btnUpdate.Text = "Cập nhật xong ✔";
            progress?.Report("🎉 Cập nhật hoàn tất! Đang khởi động lại...");

            await Task.Delay(2000);

            Process.Start(new ProcessStartInfo
            {
                FileName = batFile,
                UseShellExecute = true,
                CreateNoWindow = false
            });

            await Task.Delay(2000);

            updateForm.Close();
            Application.Exit();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[Tải xuống bị lỗi] {ex.Message}");
            progress?.Report($"❌ LỖI: {ex.Message}");
            MessageBox.Show($"Cập nhật thất bại:\n\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            btnUpdate.Text = "Cập nhật";
            btnUpdate.Enabled = true;
        }
    }

    // ✅ TẢI FILE QUA HTTP (ĐÃ TỐI ƯU)
    private static async Task<bool> DownloadUpdateViaHTTP(string serverPath, string exeName, string tempFile, Button btnUpdate, IProgress<string> progress)
    {
        try
        {
            using (var client = new HttpClient { Timeout = TimeSpan.FromMinutes(10) })
            using (var response = await client.GetAsync(serverPath + "/" + exeName, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                var total = response.Content.Headers.ContentLength ?? -1L;

                using (var input = await response.Content.ReadAsStreamAsync())
                using (var output = new FileStream(tempFile, FileMode.Create, FileAccess.Write, FileShare.None, 81920, true))
                {
                    var buffer = new byte[8192];
                    long totalRead = 0;
                    int read;
                    int lastPercent = 0;

                    do
                    {
                        read = await input.ReadAsync(buffer, 0, buffer.Length);
                        if (read > 0)
                        {
                            await output.WriteAsync(buffer, 0, read);
                            totalRead += read;

                            if (total != -1)
                            {
                                int percent = (int)(totalRead * 100 / total);

                                if (btnUpdate.InvokeRequired)
                                {
                                    btnUpdate.Invoke(new Action(() =>
                                    {
                                        btnUpdate.Text = $"Đang tải... {percent}%";
                                    }));
                                }
                                else
                                {
                                    btnUpdate.Text = $"Đang tải... {percent}%";
                                }

                                if (percent != lastPercent && percent % 10 == 0)
                                {
                                    progress?.Report($"📊 Tiến độ: {percent}%");
                                    lastPercent = percent;
                                }
                            }
                        }
                    } while (read > 0);
                }
            }

            progress?.Report("✅ Tải xuống hoàn tất!");
            return true;
        }
        catch (Exception ex)
        {
            progress?.Report($"❌ Lỗi tải HTTP: {ex.Message}");
            return false;
        }
    }

    // ✅ TẢI FILE QUA CIFS (ĐÃ TỐI ƯU)
    private static async Task<bool> DownloadUpdateViaCIFS(string nasPath, string exeName, string tempFile, IProgress<string> progress)
    {
        return await Task.Run(() =>
        {
            try
            {
                var servers = SecureConfig.GetServers();
                var nas = servers.NasServers?.FirstOrDefault(n => n.Path == nasPath);
                if (nas == null)
                {
                    progress?.Report($"❌ Không tìm thấy cấu hình NAS: {nasPath}");
                    return false;
                }

                var config = new NasConnectionInfo
                {
                    Path = nas.Path,
                    Username = nas.Username,
                    Password = nas.Password
                };

                if (!ConnectToNas(config, msg => progress?.Report(msg)))
                {
                    progress?.Report($"❌ Không thể kết nối đến NAS: {nasPath}");
                    return false;
                }

                string remoteFile = Path.Combine(nasPath, exeName);
                if (!File.Exists(remoteFile))
                {
                    progress?.Report($"❌ File không tồn tại: {remoteFile}");
                    return false;
                }

                progress?.Report($"📥 Đang copy file từ NAS...");
                File.Copy(remoteFile, tempFile, true);
                progress?.Report($"✅ Đã tải file từ NAS: {Path.GetFileName(remoteFile)}");
                return true;
            }
            catch (Exception ex)
            {
                progress?.Report($"❌ Lỗi tải từ NAS: {ex.Message}");
                return false;
            }
            finally
            {
                try { WNetCancelConnection2(nasPath, 0, true); } catch { }
            }
        });
    }

    private static Task WriteAllTextAsyncCompat(string path, string contents, System.Text.Encoding encoding)
    {
        return Task.Run(() => File.WriteAllText(path, contents, encoding));
    }

    // 🪟 Win32 API
    [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
        int nWidthEllipse, int nHeightEllipse);

    [DllImport("dwmapi.dll")]
    private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }
}
#endregion