using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

#region CONFIGURATION
public static class SecureConfig
{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("Your32ByteEncryptionKey123!!");
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("Your16ByteIVKey!");

    public class UpdateServers
    {
        public string[] HttpServers { get; set; }
        public NasConfig[] NasServers { get; set; }
    }

    public class NasConfig
    {
        public string Path { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public static UpdateServers GetServers()
    {
        try
        {
            string configFile = Path.Combine(Application.StartupPath, "update_config.enc");

            if (!File.Exists(configFile))
                return CreateDefaultConfig();

            string encryptedData = File.ReadAllText(configFile);
            string jsonData = Decrypt(encryptedData);

            return ParseJsonManually(jsonData);
        }
        catch
        {
            return CreateDefaultConfig();
        }
    }

    private static UpdateServers ParseJsonManually(string json)
    {
        var result = new UpdateServers();
        var httpList = new List<string>();
        var nasList = new List<NasConfig>();

        json = json.Trim();
        if (!json.StartsWith("{") || !json.EndsWith("}")) return CreateDefaultConfig();

        json = json.Substring(1, json.Length - 2);
        string[] parts = SplitJsonObject(json);

        foreach (string part in parts)
        {
            string trimmed = part.Trim();
            if (trimmed.StartsWith("\"HttpServers\""))
            {
                string arrayStr = ExtractJsonArray(trimmed);
                httpList.AddRange(ParseStringArray(arrayStr));
            }
            else if (trimmed.StartsWith("\"NasServers\""))
            {
                string arrayStr = ExtractJsonArray(trimmed);
                foreach (string obj in ParseJsonArrayObjects(arrayStr))
                {
                    nasList.Add(ParseNasObject(obj));
                }
            }
        }

        result.HttpServers = httpList.ToArray();
        result.NasServers = nasList.ToArray();
        return result;
    }

    private static string[] SplitJsonObject(string json)
    {
        var list = new List<string>();
        int braceCount = 0;
        int start = 0;

        for (int i = 0; i < json.Length; i++)
        {
            if (json[i] == '{' || json[i] == '[') braceCount++;
            if (json[i] == '}' || json[i] == ']') braceCount--;

            if (json[i] == ',' && braceCount == 0)
            {
                list.Add(json.Substring(start, i - start));
                start = i + 1;
            }
        }
        if (start < json.Length)
            list.Add(json.Substring(start));

        return list.ToArray();
    }

    private static string ExtractJsonArray(string field)
    {
        int colon = field.IndexOf(':');
        if (colon == -1) return "[]";
        string arrayPart = field.Substring(colon + 1).Trim();
        if (!arrayPart.StartsWith("[")) return "[]";
        int end = arrayPart.LastIndexOf(']');
        return end > 0 ? arrayPart.Substring(0, end + 1) : "[]";
    }

    private static string[] ParseStringArray(string arrayJson)
    {
        var list = new List<string>();
        if (!arrayJson.StartsWith("[") || !arrayJson.EndsWith("]")) return new string[0];
        string inner = arrayJson.Substring(1, arrayJson.Length - 2);

        string[] items = SplitJsonArray(inner);
        foreach (string item in items)
        {
            string trimmed = item.Trim();
            if (trimmed.StartsWith("\"") && trimmed.EndsWith("\""))
                list.Add(trimmed.Substring(1, trimmed.Length - 2));
        }
        return list.ToArray();
    }

    private static string[] ParseJsonArrayObjects(string arrayJson)
    {
        var list = new List<string>();
        if (!arrayJson.StartsWith("[") || !arrayJson.EndsWith("]")) return new string[0];
        string inner = arrayJson.Substring(1, arrayJson.Length - 2);

        int braceCount = 0;
        int start = 0;
        for (int i = 0; i < inner.Length; i++)
        {
            if (inner[i] == '{') braceCount++;
            if (inner[i] == '}') braceCount--;
            if (inner[i] == ',' && braceCount == 0)
            {
                list.Add(inner.Substring(start, i - start));
                start = i + 1;
            }
        }
        if (start < inner.Length)
            list.Add(inner.Substring(start));

        return list.ToArray();
    }

    private static NasConfig ParseNasObject(string objJson)
    {
        var nas = new NasConfig();
        string[] parts = SplitJsonObject(objJson);

        foreach (string part in parts)
        {
            string trimmed = part.Trim();
            if (trimmed.StartsWith("\"Path\"")) nas.Path = ExtractStringValue(trimmed);
            else if (trimmed.StartsWith("\"Username\"")) nas.Username = ExtractStringValue(trimmed);
            else if (trimmed.StartsWith("\"Password\"")) nas.Password = ExtractStringValue(trimmed);
        }
        return nas;
    }

    private static string ExtractStringValue(string field)
    {
        int colon = field.IndexOf(':');
        if (colon == -1) return "";
        string valuePart = field.Substring(colon + 1).Trim();
        if (valuePart.StartsWith("\"") && valuePart.EndsWith("\""))
            return valuePart.Substring(1, valuePart.Length - 2);
        return valuePart;
    }

    private static string[] SplitJsonArray(string json)
    {
        var list = new List<string>();
        int start = 0;
        bool inString = false;

        for (int i = 0; i < json.Length; i++)
        {
            if (json[i] == '"' && (i == 0 || json[i - 1] != '\\')) inString = !inString;
            if (json[i] == ',' && !inString)
            {
                list.Add(json.Substring(start, i - start));
                start = i + 1;
            }
        }
        if (start < json.Length)
            list.Add(json.Substring(start));

        return list.ToArray();
    }

    private static UpdateServers CreateDefaultConfig()
    {
        return new UpdateServers
        {
            HttpServers = new[]
            {
                "http://192.168.111.101:8888/update/FAB_CONF/",
                "http://107.125.221.79:8888/update/FAB_CONF/",
                "http://107.126.41.111:8888/update/FAB_CONF/"
            },
            NasServers = new[]
            {
                new NasConfig
                {
                    Path = @"\\107.126.41.111\share folder\update\FAB_CONF",
                    Username = @"admin",
                    Password = "insp2019@"
                }
            }
        };
    }

    private static string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                    sw.Write(plainText);

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    private static string Decrypt(string cipherText)
    {
        byte[] buffer = Convert.FromBase64String(cipherText);
        using (Aes aes = Aes.Create())
        {
            aes.Key = Key;
            aes.IV = IV;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream(buffer))
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(cs))
                return sr.ReadToEnd();
        }
    }
}
#endregion