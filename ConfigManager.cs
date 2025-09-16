using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FAB_CONFIRM
{
    public partial class ConfigManager
    {
        private readonly string configPath;

        public ConfigManager(string configDir, string fileName)
        {
            this.configPath = Path.Combine(configDir, fileName);
            EnsureDirectoryExists(configDir);
        }

        private void EnsureDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public void InitializeConfigFile(string defaultList, string sectionName)
        {
            if (!File.Exists(configPath))
            {
                var content = new List<string>
                {
                    $"[{sectionName}]",
                    "list = " + defaultList
                };
                File.WriteAllLines(configPath, content);
            }
        }

        public List<string> ReadList(string sectionName)
        {
            if (!File.Exists(configPath))
            {
                return new List<string>();
            }

            var lines = File.ReadAllLines(configPath);
            bool inSection = false;
            foreach (var line in lines)
            {
                if (line.Trim() == $"[{sectionName}]")
                {
                    inSection = true;
                }
                else if (inSection && line.StartsWith("list ="))
                {
                    var values = line.Substring("list =".Length).Trim().Split(',');
                    return values.Select(v => v.Trim()).ToList();
                }
            }
            return new List<string>();
        }
    }
}