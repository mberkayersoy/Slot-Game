using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class SaveLoadManager<T>
{
    private static readonly string savePath = Application.persistentDataPath;

    public static void Save(T data, string fileName)
    {
        string filePath = Path.Combine(savePath, fileName);

        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(filePath, json);
    }

    public static T Load(string fileName)
    {
        Debug.Log(Application.persistentDataPath);
        string filePath = Path.Combine(savePath, fileName);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            return default;
        }
    }
}
