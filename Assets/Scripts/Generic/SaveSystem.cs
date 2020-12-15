using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SavePlayer(SaveLoadPlayer player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.neonarpg";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveScene(SwitchScenes scene)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/scene.neonarpg";
        FileStream stream = new FileStream(path, FileMode.Create);

        SceneData data = new SceneData(scene);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public static SceneData LoadScene()
    {
        string path = Application.persistentDataPath + "/scene.neonarpg";

        if (File.Exists(path))
        {
            BinaryFormatter binaryformatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SceneData data = binaryformatter.Deserialize(stream) as SceneData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Saved file does not exist in " + path);
            return null;
        }
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.neonarpg";

        if (File.Exists(path))
        {
            BinaryFormatter binaryformatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = binaryformatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Saved file does not exist in " + path);
            return null;
        }
    }
    
}
