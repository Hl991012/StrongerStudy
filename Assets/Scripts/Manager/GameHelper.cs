using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public static class GameHelper
{
    public static string DataSavePath
    {
        get
        {
#if UNITY_EDITOR_WIN
            return Application.streamingAssetsPath + "/Data/";
#else
            return Application.persistentDataPath + "/Data/";
#endif
        }
    }

    public static void SaveData<T>(string dataPath, T model)
    {
        using var fileStream = new FileStream(dataPath + ".json", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
        using var streamWriter = new StreamWriter(fileStream);
        streamWriter.WriteAsync(JsonConvert.SerializeObject(model)).Dispose();
    }

    public static async Task<T> LoadDate<T>(string dataPath) where T : new()
    {
        await using var fileStream = new FileStream(dataPath + ".json", FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
        using var streamRender = new StreamReader(fileStream);
        var dataJson = await streamRender.ReadToEndAsync();
        return  dataJson is not (null or "") ? JsonConvert.DeserializeObject<T>(dataJson) : new T();
    }

    public static string GetGUID()
    {
        return new Guid().ToString();
    }
}
