using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class SaveData
{
    public const string SAVEDATA_FILE_PATH = "PlayerData.es3";

    public static void Save<T>(string key,  T data)
    {
        ES3.Save(key, data, SAVEDATA_FILE_PATH);
    }

    public static T Load<T>(string key)
    {
        if (!DoesFileExist())
        {
            Debug.LogError("No File Exists, Adding key to create file");
            Save<T>(key, default);
            return default;
        }
        if(!DoesKeyExist(key))
        {
            Debug.LogError($"No key: {key} exists, Creating a default value");
            Save<T>(key, default);
            return default;
        }

        return (T)ES3.Load(key, SAVEDATA_FILE_PATH);
    }

    public static bool DoesFileExist() => ES3.FileExists(SAVEDATA_FILE_PATH);
    public static bool DoesKeyExist(string key) => ES3.KeyExists(key, SAVEDATA_FILE_PATH);

    public static void CreateDefaultData()
    {
        Save("HasBeenCreated", true);
    }
}
