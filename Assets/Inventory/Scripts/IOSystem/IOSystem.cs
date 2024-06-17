using System.IO;
using UnityEngine;

namespace Assets.Inventory.Scripts.IOSystem
{
    public static class IOSystem
    {
        private static string Path = $"{Application.dataPath}/Editor/SaveData";
        private static readonly string fileName = "InventoryData";

        public static Tclass Load<Tclass>() where Tclass : class
        {
            var path = GetPathHistory();
            var content = ReadData(path);
            var datas = JsonUtility.FromJson<Tclass>(content);
            return datas;
        }
        private static string ReadData(string path)
        {
            if (!TryFileExist())
                return string.Empty;

            var fileStream = new FileStream(path, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string content = reader.ReadToEnd();
#if UNITY_EDITOR
                Debug.Log("<color=green> Successful load file </color>");
#endif
                return content;
            }
        }

        public static void Save<Tclass>(Tclass data) where Tclass : class
        {
            var content = JsonUtility.ToJson(data, true);
            if (!TryFileExist())
            {
                FolderUtilitles.CreateFolder(Path);
            }
            var path = GetPathHistory();
            WriteFile(content, path);
        }
        private static void WriteFile(string content, string path)
        {

            var fileStream = new FileStream(path, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(content);
#if UNITY_EDITOR
                Debug.Log("<color=green> Successful save file </color>");
#endif
            }
        }

        public static bool TryFileExist()
            => File.Exists(GetPathHistory());
        private static string GetPathHistory()
            => $"{Path}/{fileName}.json";
    }

    public static class FolderUtilitles
    {
        public static bool CreateFolder(string path)
        {
            try
            {
                CreateDirectory(path);
            }
            catch (DirectoryNotFoundException ex)
            {
                UnityEngine.Debug.LogError($"Не удалось создать директорию {ex}");
                return false;
            }

            return true;
        }

        private static void CreateDirectory(string path)
        {
            if (TryExistsDirectory(path))
            {
                return;
            }

            Directory.CreateDirectory(path);
        }

        private static bool TryExistsDirectory(string path)
        {
            return Directory.Exists(path);
        }
    }
}

