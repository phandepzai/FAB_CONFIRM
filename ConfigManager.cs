using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FAB_CONFIRM
{
    #region QUẢN LÝ FILE CẤU HÌNH
    public partial class ConfigManager
    {
        private readonly string configPath; // Đường dẫn đầy đủ đến file cấu hình.

        public ConfigManager(string configDir, string fileName)
        {
            this.configPath = Path.Combine(configDir, fileName); // Kết hợp đường dẫn thư mục và tên file.
            EnsureDirectoryExists(configDir); // Đảm bảo thư mục tồn tại.
        }

        // Phương thức kiểm tra và tạo thư mục nếu nó chưa tồn tại.
        private void EnsureDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        // Phương thức khởi tạo file cấu hình với danh sách mặc định nếu file chưa tồn tại.
        public void InitializeConfigFile(string defaultList, string sectionName)
        {
            if (!File.Exists(configPath))
            {
                var content = new List<string>
                {
                    $"[{sectionName}]" // Ghi tên section.
                };
                // Tách chuỗi mặc định thành các dòng riêng biệt và thêm vào danh sách nội dung.
                var linesToAdd = defaultList.Split(',').Select(s => s.Trim());
                content.AddRange(linesToAdd);

                // Ghi toàn bộ nội dung vào file.
                File.WriteAllLines(configPath, content);
            }
        }

        // Phương thức đọc danh sách từ một section cụ thể trong file.
        public List<string> ReadList(string sectionName)
        {
            if (!File.Exists(configPath))
            {
                return new List<string>(); // Trả về danh sách trống nếu file không tồn tại.
            }

            var lines = File.ReadAllLines(configPath);
            bool inSection = false;
            var resultList = new List<string>();

            foreach (var line in lines)
            {
                if (line.Trim() == $"[{sectionName}]")
                {
                    inSection = true; // Bắt đầu đọc khi tìm thấy section.
                    continue; // Bỏ qua dòng tên section.
                }

                if (inSection && line.Trim().StartsWith("[") && line.Trim().EndsWith("]"))
                {
                    break; // Dừng đọc nếu gặp một section khác.
                }

                if (inSection && !string.IsNullOrWhiteSpace(line))
                {
                    resultList.Add(line.Trim()); // Thêm dòng vào danh sách kết quả.
                }
            }
            return resultList;
        }
    }
    #endregion
}