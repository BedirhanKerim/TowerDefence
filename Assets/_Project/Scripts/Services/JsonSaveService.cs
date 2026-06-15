using System.IO;
using _Project.Scripts.Data;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class JsonSaveService : ISaveService
    {
        private readonly string _path = Path.Combine(Application.persistentDataPath, "save.json");

        public SaveData Load()
        {
            if (!File.Exists(_path))
                return new SaveData();

            return JsonUtility.FromJson<SaveData>(File.ReadAllText(_path)) ?? new SaveData();
        }

        public void Save(SaveData data)
        {
            File.WriteAllText(_path, JsonUtility.ToJson(data));
        }
    }
}
