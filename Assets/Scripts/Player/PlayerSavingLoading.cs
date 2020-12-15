using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSavingLoading : MonoBehaviour
{
    public const string SavePath = "Save";

    private void Awake()
    {
        Deserialize();
    }

    private void Deserialize()
    {
        var saveFile = PlayerPrefs.GetString(SavePath);
        if (saveFile == "") return;
        var json = JsonUtility.FromJson<SaveData>(saveFile);
        Debug.Log(saveFile);
        var saveables = GetComponents<ISaveable>();
        foreach (var saveable in saveables)
        {
            var i = 0;
            while (!saveable.Deserialize(json.data[i]) && i < saveables.Length - 1)
                i++;
        }
    }

    public static void Continue()
    {
        var saveFile = PlayerPrefs.GetString(SavePath);
    }

    private void OnDestroy()
    {
        Serialize();
    }

    private void Serialize()
    {
        var saveables = GetComponents<ISaveable>();
        var jsons = saveables.Select(saveable => saveable.Serialize()).ToList();
        var saveData = new SaveData(jsons.ToArray());
        var json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SavePath, json);
    }
}

[Serializable]
public class SaveData
{
    public string[] data;
    public SaveData(string[] data) => this.data = data;
}